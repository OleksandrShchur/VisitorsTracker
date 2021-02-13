using System.Security.Claims;
using System.Threading.Tasks;
using VisitorsTracker.Core.DTOs;
using VisitorsTracker.Db.Entities;

namespace VisitorsTracker.Core.IServices
{
    public interface ITokenService
    {
        string GenerateAccessToken(UserDTO user);

        RefreshToken GenerateRefreshToken();

        ClaimsPrincipal GetPrincipalFromJwt(string token);

        Task<AuthenticateResponseModel> RefreshToken(string token);

        Task<bool> RevokeToken(string token);

        void SetTokenCookie(string token);
    }
}
