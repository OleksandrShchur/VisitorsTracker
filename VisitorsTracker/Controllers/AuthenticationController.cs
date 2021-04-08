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
        private readonly IPhotoService _photoService;

        public AuthenticationController(
            IUserService userSrv,
            IMapper mapper,
            IAuthenticationService authSrv,
            ITokenService tokenService,
            IPhotoService photoService)
        {
            _userService = userSrv;
            _mapper = mapper;
            _authService = authSrv;
            _tokenService = tokenService;
            _photoService = photoService;
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
                user.PhotoUrl = await _photoService.SavePhotoInFolder(userView.PhotoUrl);
                await _userService.Create(user);
            }

            await SetUserPhotoIfEmpty(userExisting, userView.PhotoUrl);
            var authResponseModel = await _authService.AuthenticateUserFromExternalProvider(payload.Email);
            var userInfo = _mapper.Map<UserInfoViewModel>(_userService.GetByEmail(payload.Email));
            userInfo.Token = authResponseModel.JwtToken;
            _tokenService.SetTokenCookie(authResponseModel.RefreshToken);

            return Ok(userInfo);
        }

        private async Task SetUserPhotoIfEmpty (UserDTO userExisting, string urlPhoto)
        {
            if (userExisting != null && userExisting?.PhotoUrl == null)
            {
                userExisting.PhotoUrl = await _photoService.SavePhotoInFolder(urlPhoto);

                await _userService.Update(userExisting);
            }
        }
    }
}
