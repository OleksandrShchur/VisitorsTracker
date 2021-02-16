using System.Security.Claims;
using System.Threading.Tasks;
using VisitorsTracker.Core.DTOs;

namespace VisitorsTracker.Core.IServices
{
    public interface IAuthenticationService
    {
        Task Authenticate(string email);

        Task FirstAuthenticate(UserDTO userDto);

        UserDTO GetCurrentUser(ClaimsPrincipal userClaims);

        Task<AuthenticateResponseModel> AuthenticateUserFromExternalProvider(string email);
    }
}
