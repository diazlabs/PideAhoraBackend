using Application.Common.Interfaces;
using Ardalis.Result;
using MediatR;

namespace Application.Tenants.Queries.GetTenantsByUser
{
    public class GetTenantsByUserQueryHandler : IRequestHandler<GetTenantsByUserQuery, Result<GetTenantsByUserResponse>>
    {
        private readonly ITenantService _tenantService;
        public GetTenantsByUserQueryHandler(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        public async Task<Result<GetTenantsByUserResponse>> Handle(GetTenantsByUserQuery request, CancellationToken cancellationToken)
        {
            var tenants = await _tenantService.GetTenantsByUserId(request.UserId);
            if (tenants == null)
            {
                return Result.NotFound();
            }

            return new GetTenantsByUserResponse(tenants);
        }
    }
}
