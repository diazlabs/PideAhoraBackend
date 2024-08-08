using Application.Common.Interfaces;
using Ardalis.Result;
using MediatR;

namespace Application.TenantConfigs.Commands.ToggleTenantConfig
{
    public class ToggleTenantConfigCommandHandler : IRequestHandler<ToggleTenantConfigCommand, Result<ToggleTenantConfigResponse>>
    {
        private readonly ITenantConfigRepository _tenantConfigRepository;
        public ToggleTenantConfigCommandHandler(ITenantConfigRepository tenantConfigRepository)
        {
            _tenantConfigRepository = tenantConfigRepository;
        }
        public async Task<Result<ToggleTenantConfigResponse>> Handle(ToggleTenantConfigCommand request, CancellationToken cancellationToken)
        {
            Result result = await _tenantConfigRepository.ToggleConfig(request.TenantConfigId, request.Enabled);

            return result;
        }
    }
}
