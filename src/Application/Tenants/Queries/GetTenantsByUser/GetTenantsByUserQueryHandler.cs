using Application.Common.Interfaces;
using Ardalis.Result;
using MediatR;

namespace Application.Tenants.Queries.GetTenantsByUser
{
    public class GetTenantsByUserQueryHandler : IRequestHandler<GetTenantsByUserQuery, Result<GetTenantsByUserResponse>>
    {
        private readonly ITenantRepository _tenantRepository;
        public GetTenantsByUserQueryHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<Result<GetTenantsByUserResponse>> Handle(GetTenantsByUserQuery request, CancellationToken cancellationToken)
        {
            var tenants = await _tenantRepository.GetTenantsByUserId(request.UserId);
            if (tenants == null)
            {
                return Result.NotFound();
            }

            return new GetTenantsByUserResponse(tenants);
        }
    }
}
