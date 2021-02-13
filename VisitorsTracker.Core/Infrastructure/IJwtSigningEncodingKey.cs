using Microsoft.IdentityModel.Tokens;

namespace VisitorsTracker.Core.Infrastructure
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }

        SecurityKey GetKey();
    }
}
