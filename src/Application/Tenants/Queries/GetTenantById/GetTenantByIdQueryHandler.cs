using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Tenants.Queries.GetTenantById
{
    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, Result<GetTenantByIdResponse>>
    {
        private readonly ITenantService _tenantService;
        public GetTenantByIdQueryHandler(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        public async Task<Result<GetTenantByIdResponse>> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            Tenant? tenant = await _tenantService.FindTenantById(request.TenantId);

            if (tenant == null)
            {
                return Result.NotFound();
            }

            return new GetTenantByIdResponse(tenant);
        }
    }
}
