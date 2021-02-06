using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VisitorsTracker.Core.DTOs;
using VisitorsTracker.Db.IBaseService;

namespace VisitorsTracker.Core.IServices
{
    public interface IAuthenticationService
    {
        Task Authenticate(string email, string password);

        Task FirstAuthenticate(UserDTO userDto);

        UserDTO GetCurrentUser(ClaimsPrincipal userClaims);
    }
}
