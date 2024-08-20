using Application.Common.Interfaces;
using Ardalis.Result;
using MediatR;

namespace Application.TenantConfigs.Commands.ToggleTenantConfig
{
    public class ToggleTenantConfigCommandHandler : IRequestHandler<ToggleTenantConfigCommand, Result<ToggleTenantConfigResponse>>
    {
        private readonly ITenantConfigService _tenantConfigService;
        public ToggleTenantConfigCommandHandler(ITenantConfigService tenantConfigService)
        {
            _tenantConfigService = tenantConfigService;
        }
        public async Task<Result<ToggleTenantConfigResponse>> Handle(ToggleTenantConfigCommand request, CancellationToken cancellationToken)
        {
            Result result = await _tenantConfigService.ToggleConfig(request.TenantConfigId, request.Enabled);

            return result;
        }
    }
}
