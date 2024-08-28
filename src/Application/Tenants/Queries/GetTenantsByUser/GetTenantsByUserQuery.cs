using MediatR;

namespace Application.Tenants.Queries.GetTenantsByUser
{
    public record GetTenantsByUserQuery() : IRequest<IEnumerable<GetTenantsByUserResponse>>;
}
