using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using VisitorsTracker.Core.DTOs;
using VisitorsTracker.Core.Infrastructure;
using VisitorsTracker.Core.IServices;
using VisitorsTracker.Db.Entities;

namespace VisitorsTracker.Core.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUserService _userService;
        private readonly IJwtSigningEncodingKey _signingEncodingKey;
        private readonly IOptions<JwtOptionsModel> _jwtOptions;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        const string key = "X-Forwarded-For";
        const string refreshToken = "refreshToken";

        public TokenService(
            IOptions<JwtOptionsModel> opt,
            IJwtSigningEncodingKey jwtSigningEncodingKey,
            IUserService userService,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _jwtOptions = opt;
            _signingEncodingKey = jwtSigningEncodingKey;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private string IpAddress
        {
            get
            {
                if (_httpContextAccessor.HttpContext.Request.Headers.ContainsKey(key))
                {
                    return _httpContextAccessor.HttpContext.Request.Headers[key];
                }
                else
                {
                    return _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
                }
            }
        }

        public string GenerateAccessToken(UserDTO user)
        {
            var lifeTime = _jwtOptions.Value.LifeTime;
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Id.ToString()),
            };
            var jwtToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddSeconds(lifeTime),
                signingCredentials: new SigningCredentials(
                        _signingEncodingKey.GetKey(),
                        _signingEncodingKey.SigningAlgorithm));

            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }

        public RefreshToken GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);

            return new RefreshToken
            {
                Token = Convert.ToBase64String(randomNumber),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = IpAddress,
            };
        }

        public ClaimsPrincipal GetPrincipalFromJwt(string token)
        {
            var signingKey = new SigningSymmetricKey(_jwtOptions.Value.SecretKey);

            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingDecodingKey.GetKey(),
                ValidateLifetime = false,
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public async Task<AuthenticateResponseModel> RefreshToken(string token)
        {
            var user = _userService.GetUserByRefreshToken(token);

            // return null if no user found with token
            if (user == null)
            {
                return null;
            }

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return null if token is no longer active
            if (!_mapper.Map<RefreshTokenDTO>(refreshToken).IsActive)
            {
                return null;
            }

            // replace old refresh token with a new one and save
            var newRefreshToken = GenerateRefreshToken();
            refreshToken.Revoked = DateTime.Now;
            refreshToken.RevokedByIp = IpAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens = new List<RefreshToken> { newRefreshToken, refreshToken };

            await _userService.Update(user);

            // generate new jwt
            var jwtToken = GenerateAccessToken(user);

            return new AuthenticateResponseModel(jwtToken, newRefreshToken.Token);
        }

        public async Task<bool> RevokeToken(string token)
        {
            var user = _userService.GetUserByRefreshToken(token);
            if (user == null)
            {
                return false;
            }

            var refreshTokens = user.RefreshTokens.SingleOrDefault(x => x.Token == token);

            // return false if token is not active
            if (refreshTokens == null || !_mapper.Map<RefreshTokenDTO>(refreshTokens).IsActive)
            {
                return false;
            }

            // revoke token and save
            refreshTokens.Revoked = DateTime.UtcNow;
            refreshTokens.RevokedByIp = IpAddress;
            user.RefreshTokens = new List<RefreshToken> { refreshTokens };
            await _userService.Update(user);
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(refreshToken);

            return true;
        }

        public void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7),
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Delete(refreshToken);
            _httpContextAccessor.HttpContext.Response.Cookies.Append(refreshToken, token, cookieOptions);
        }
    }
}
