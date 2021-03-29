using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VisitorsTracker.Core.DTOs;
using VisitorsTracker.Core.IServices;
using VisitorsTracker.Db.Enums;
using VisitorsTracker.ViewModels;

namespace VisitorsTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authService;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public UsersController(
            IUserService userSrv,
            IAuthenticationService authSrv,
            IMapper mapper,
            IEmailService emailService)
        {
            _userService = userSrv;
            _authService = authSrv;
            _mapper = mapper;
            _emailService = emailService;
        }

        /// <summary>
        /// This method is to edit date of birthday.
        /// </summary>
        /// <param name="userBirthday">Required.</param>
        /// <response code="200">Edit is succesful.</response>
        /// <response code="400">Edit process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> EditBirthday(EditUserBirthViewModel userBirthday)
        {
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            user.Birthday = userBirthday.Birthday;
            await _userService.Update(user);

            return Ok();
        }

        /// <summary>
        /// This method is to edit gender.
        /// </summary>
        /// <param name="userGender">Required.</param>
        /// <response code="200">Edit is succesful.</response>
        /// <response code="400">Edit process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> EditGender(EditUserGenderViewModel userGender)
        {
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            user.Gender = (Gender)userGender.Gender;
            await _userService.Update(user);

            return Ok();
        }

        /// <summary>
        /// This metod is to change user avatar.
        /// </summary>
        /// <response code="200">Changing is succesful.</response>
        /// <response code="400">Changing process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> ChangeAvatar() // to do
        {
            var user = GetCurrentUser(HttpContext.User);
            if (user == null)
            {
                return BadRequest();
            }

            var newAva = HttpContext.Request.Form.Files[0];

            await _userService.ChangeAvatar(user.Id, newAva);

            //var updatedPhoto = _userService.GetById(user.Id).Photo.Thumb.ToRenderablePictureString();

            return Ok(/*updatedPhoto*/);
        }

        /// <summary>
        /// This method help to contact users with admins.
        /// </summary>
        /// <param name="model">ContactModel.</param>
        /// <response code="200">Sending is succesfull.</response>
        /// <response code="400">Sending process failed.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> ContactAdmins(ContactUsViewModel model)
        {
            var user = _authService.GetCurrentUser(HttpContext.User);

            var admins = _userService.GetUsersByRole("Admin");

            var emailBody = $"New request from <a href='mailto:{user.Email}?subject=re:{model.Type}'>{user.Email}</a> : <br />{model.Description}. ";

            try
            {
                foreach (var admin in admins)
                {
                    await _emailService.SendEmailAsync(new EmailDTO
                    {
                        Subject = model.Type,
                        RecepientEmail = admin.Email,
                        MessageText = emailBody,
                    });
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// This method is for get user.
        /// </summary>
        /// <param name="id">UserId.</param>
        /// <returns>User.</returns>
        /// <response code="200">Return UserProfileDto.</response>
        /// <response code="400">Attitude set failed.</response>
        [HttpGet("[action]")]
        public IActionResult GetUserProfileById(Guid id)
        {
            var user = GetCurrentUser(HttpContext.User);
            var res = _mapper.Map<ProfileViewModel>(_userService.GetProfileById(id, user.Id));

            return Ok(res);
        }

        // HELPERS:

        /// <summary>
        /// This method help to get current user from JWT.
        /// </summary>
        [NonAction]
        private UserDTO GetCurrentUser(ClaimsPrincipal userClaims) => _authService.GetCurrentUser(userClaims);
    }
}
