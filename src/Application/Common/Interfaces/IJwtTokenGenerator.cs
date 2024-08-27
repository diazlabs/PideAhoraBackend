using System.Security.Claims;

namespace Application.Common.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(
           Guid UserId,
           string firstName,
           string lastName,
           string email,
           IEnumerable<string> tenants,
           List<Claim> cliams,
           IEnumerable<string> roles);
    }
}
