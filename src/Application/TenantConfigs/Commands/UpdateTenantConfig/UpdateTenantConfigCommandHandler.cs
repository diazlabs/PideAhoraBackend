using Application.Common.Interfaces;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.TenantConfigs.Commands.UpdateTenantConfig
{
    public class UpdateTenantConfigCommandHandler : IRequestHandler<UpdateTenantConfigCommand, Result<UpdateTenantConfigResponse>>
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ITenantConfigService _tenantConfigService;

        public UpdateTenantConfigCommandHandler(ICurrentUserProvider currentUserProvider, ITenantConfigService tenantConfigService)
        {
            _currentUserProvider = currentUserProvider;
            _tenantConfigService = tenantConfigService;
        }

        public async Task<Result<UpdateTenantConfigResponse>> Handle(UpdateTenantConfigCommand request, CancellationToken cancellationToken)
        {
            TenantConfig? config = await _tenantConfigService
                .FindTenantConfigById(request.TenantId);

            if (config == null)
            {
                return Result.NotFound();
            }

            config.ConfigValue = request.ConfigValue;
            config.ConfigName = request.ConfigName;
            config.Enabled = request.Enabled;
            config.Modifier = _currentUserProvider.GetUserId();
            config.UpdatedAt = DateTime.UtcNow;

            var result = await _tenantConfigService.Update(config);

            if (result.IsSuccess)
            {
                return new UpdateTenantConfigResponse();
            }

            return result;
        }
    }
}
