using System.Security.Claims;
using System.Threading.Tasks;
using VisitorsTracker.Core.DTOs;
using VisitorsTracker.Core.Exceptions;
using VisitorsTracker.Core.IServices;

namespace VisitorsTracker.Core.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService _userService;

        public AuthenticationService(
            IUserService userServ)
        {
            _userService = userServ;
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
