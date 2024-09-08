using Domain.Entities;

namespace Application.Tenants.Queries.GetTenantsByUser
{
    public record GetTenantsByUserResponse(
        Guid TenantId,
        Guid UserId,
        string Path,
        string Name,
        string PageTitle,
        string Description,
        string Logo,
        string Category
    );
}
