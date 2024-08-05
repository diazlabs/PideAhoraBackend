using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Tenants.Queries.GetTenantById
{
    public class GetTenantByIdQueryHandler : IRequestHandler<GetTenantByIdQuery, Result<GetTenantByIdResponse>>
    {
        private readonly ITenantRepository _tenantRepository;
        public GetTenantByIdQueryHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<Result<GetTenantByIdResponse>> Handle(GetTenantByIdQuery request, CancellationToken cancellationToken)
        {
            Tenant? tenant = await _tenantRepository.FindTenantById(request.TenantId);

            if (tenant == null)
            {
                return Result.NotFound();
            }

            return new GetTenantByIdResponse(tenant);
        }
    }
}
