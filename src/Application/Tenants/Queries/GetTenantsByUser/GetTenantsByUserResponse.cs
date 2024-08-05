using Domain.Entities;

namespace Application.Tenants.Queries.GetTenantsByUser
{
    public record GetTenantsByUserResponse(List<Tenant> Tenants);
}
