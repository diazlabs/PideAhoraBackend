using System.Security.Claims;

namespace Application.Common.Security
{
    public record CurrentUser(
        Guid UserId,
        string FirstName,
        string LastName,
        string Email,
        IReadOnlyList<string> Tenants,
        IReadOnlyList<string> Roles,
        IReadOnlyList<Claim>? Claims = default
    );
}
