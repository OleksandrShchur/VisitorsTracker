using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using VisitorsTracker.Core.DTOs;
using VisitorsTracker.Core.Exceptions;
using VisitorsTracker.Core.IServices;
using VisitorsTracker.Db.Entities;

namespace VisitorsTracker.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthenticationService(
            IUserService userServ,
            ITokenService tokenService)
        {
            _userService = userServ;
            _tokenService = tokenService;
        }

        public async Task<AuthenticateResponseModel> AuthenticateUserFromExternalProvider(string email)
        {
            UserDTO user = _userService.GetByEmail(email);

            if (user == null)
            {
                throw new VisitorsTrackerException($"User with email: {email} not found");
            }

            var jwtToken = _tokenService.GenerateAccessToken(user);
            var refreshToken = _tokenService.GenerateRefreshToken();

            // save refresh token
            user.RefreshTokens = new List<RefreshToken> { refreshToken };
            await _userService.Update(user);
            return new AuthenticateResponseModel(jwtToken, refreshToken.Token);
        }

        public async Task Authenticate(string email)
        {
            var user = _userService.GetByEmail(email);
            if (user == null)
            {
                throw new VisitorsTrackerException("User not found");
            }

            // save
            await _userService.Update(user);
        }

        public async Task FirstAuthenticate(UserDTO userDto)
        {
            if (userDto == null)
            {
                throw new VisitorsTrackerException("User not found");
            }

            await _userService.Update(userDto);
        }

        public UserDTO GetCurrentUser(ClaimsPrincipal userClaims)
        {
            Claim emailClaim = userClaims.FindFirst(ClaimTypes.Email);

            if (string.IsNullOrEmpty(emailClaim?.Value))
            {
                return null;
            }

            return _userService.GetByEmail(emailClaim.Value);
        }
    }
}
