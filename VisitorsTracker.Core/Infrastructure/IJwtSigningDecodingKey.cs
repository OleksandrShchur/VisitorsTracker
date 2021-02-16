using Microsoft.IdentityModel.Tokens;

namespace VisitorsTracker.Core.Infrastructure
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
