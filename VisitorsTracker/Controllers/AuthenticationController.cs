using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public AuthenticationController()
        {

        }

        /// <summary>
        /// This method allows to log in to the API and generate an authentication token.
        /// </summary>
        /// <param name="authRequest">Required.</param>
        /// <returns>UserInfo model.</returns>
        /// <response code="200">Return UserInfo model.</response>
        /// <response code="400">If login process failed.</response>
        [AllowAnonymous]
        [HttpPost("[action]")]
        [Produces("application/json")]
        public async Task<IActionResult> Login(LoginViewModel authRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var authResponseModel = await _authService.Authenticate(authRequest.Email, authRequest.Password);
            var user = _userService.GetByEmail(authRequest.Email);
            var userInfo = _mapper.Map<UserInfoViewModel>(user);

            return Ok(userInfo);
        }
    }
}
