using Application.Common.Interfaces;
using Ardalis.Result;
using MediatR;

namespace Application.Tenants.Queries.GetTenantsByUser
{
    public class GetTenantsByUserQueryHandler : IRequestHandler<GetTenantsByUserQuery, IEnumerable<GetTenantsByUserResponse>>
    {
        private readonly ITenantService _tenantService;
        public GetTenantsByUserQueryHandler(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        public async Task<IEnumerable<GetTenantsByUserResponse>> Handle(GetTenantsByUserQuery request, CancellationToken cancellationToken)
        {
            var tenants = await _tenantService.GetTenantsByUserId(request.UserId);

            return Enumerable.Empty<GetTenantsByUserResponse>();
        }
    }
}
