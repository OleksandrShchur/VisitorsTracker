using System.Threading.Tasks;
using AutoMapper;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VisitorsTracker.Core.DTOs;
using VisitorsTracker.Core.IServices;
using VisitorsTracker.ViewModels;

namespace VisitorsTracker.Controllers
{
    /// <summary>
    /// AuthenticationController using for Authenticate users.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authService;
        private readonly IMapper _mapper;
        private readonly ITokenService _tokenService;

        public AuthenticationController(
            IUserService userSrv,
            IMapper mapper,
            IAuthenticationService authSrv,
            ITokenService tokenService)
        {
            _userService = userSrv;
            _mapper = mapper;
            _authService = authSrv;
            _tokenService = tokenService;
        }

        /// <summary>
        /// This method is to login with google account.
        /// </summary>
        /// <param name="userView">Requireed.</param>
        /// <returns>UserInfo model.</returns>
        /// /// <response code="200">Return UserInfo model.</response>
        /// <response code="400">If login process failed.</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        public async Task<IActionResult> GoogleLogin([FromBody] UserViewModel userView)
        {
            var payload = await GoogleJsonWebSignature.ValidateAsync(
                userView.TokenId, new GoogleJsonWebSignature.ValidationSettings());
            UserDTO userExisting = _userService.GetByEmail(payload.Email);

            if (userExisting == null && !string.IsNullOrEmpty(payload.Email))
            {
                var user = _mapper.Map<UserViewModel, UserDTO>(userView);
                user.Email = payload.Email;
                user.Name = payload.Name;
                user.PhotoUrl = _userService.AddPhotoByURL(userView.PhotoUrl, userView.Id);
                await _userService.Create(user);
            }

            await SetPhoto(userExisting, userView.PhotoUrl);
            var authResponseModel = await _authService.AuthenticateUserFromExternalProvider(payload.Email);
            var userInfo = _mapper.Map<UserInfoViewModel>(_userService.GetByEmail(payload.Email));
            userInfo.Token = authResponseModel.JwtToken;
            _tokenService.SetTokenCookie(authResponseModel.RefreshToken);

            return Ok(userInfo);
        }

        private async Task<bool> SetPhoto(UserDTO userExisting, string urlPhoto)
        {
            if (userExisting != null)
            {
                if (userExisting.PhotoUrl == null)
                {
                    userExisting.PhotoUrl = _userService.AddPhotoByURL(urlPhoto, userExisting.Id);
                    await _userService.Update(userExisting);

                    return true;
                }
            }

            return false;
        }
    }
}
