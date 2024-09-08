using Application.Common.Interfaces;
using Application.Common.Security;
using MediatR;

namespace Application.Tenants.Queries.GetTenantsByUser
{
    public class GetTenantsByUserQueryHandler : IRequestHandler<GetTenantsByUserQuery, IEnumerable<GetTenantsByUserResponse>>
    {
        private readonly ITenantService _tenantService;
        private readonly ICurrentUserProvider _currentUserProvider;
        public GetTenantsByUserQueryHandler(ITenantService tenantService, ICurrentUserProvider currentUserProvider)
        {
            _tenantService = tenantService;
            _currentUserProvider = currentUserProvider;
        }
        public async Task<IEnumerable<GetTenantsByUserResponse>> Handle(GetTenantsByUserQuery request, CancellationToken cancellationToken)
        {
            var tenants = await _tenantService.GetTenantsByUserId(_currentUserProvider.GetUserId());

            return tenants.Select(x => new GetTenantsByUserResponse(
                x.TenantId,
                x.UserId,
                x.Path,
                x.Name,
                x.PageTitle,
                x.Description,
                x.Logo,
                x.Category)
            );
        }
    }
}
