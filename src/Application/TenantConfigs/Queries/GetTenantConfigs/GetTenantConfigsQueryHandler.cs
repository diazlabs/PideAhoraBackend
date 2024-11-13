using Application.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TenantConfigs.Queries.GeTenantConfigs
{
    public class GetTenantConfigsQueryHandler : IRequestHandler<GetTenantConfigsQuery, IEnumerable<GetTenantConfigsResponse>>
    {
        private readonly ApplicationContext _context;

        public GetTenantConfigsQueryHandler(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GetTenantConfigsResponse>> Handle(GetTenantConfigsQuery request, CancellationToken cancellationToken)
        {
            var tenantConfigs = await _context.TenantConfigs
                .Where(x => x.TenantId == request.TenantId && x.Visible)
                .Select(x => new GetTenantConfigsResponse(
                    x.TenantId,
                    x.TenantConfigId,
                    x.ConfigName,
                    x.ConfigValue,
                    x.ConfigType,
                    x.Enabled
                )).ToListAsync(cancellationToken);

            return tenantConfigs;
        }
    }
}
