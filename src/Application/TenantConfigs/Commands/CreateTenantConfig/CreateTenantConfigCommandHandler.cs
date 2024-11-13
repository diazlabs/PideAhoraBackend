using Application.Common.Interfaces;
using Application.Common.Persistence;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TenantConfigs.Commands.CreateTenantConfig
{
    public class CreateTenantConfigCommandHandler : IRequestHandler<CreateTenantConfigCommand, Result<CreateTenantConfigResponse>>
    {
        private readonly ITenantConfigService _tenantConfigService;
        private readonly ApplicationContext _context;
        private readonly ICurrentUserProvider _currentUserProvider;
        public CreateTenantConfigCommandHandler(ITenantConfigService tenantConfigService, ApplicationContext context, ICurrentUserProvider currentUserProvider)
        {
            _tenantConfigService = tenantConfigService;
            _context = context;
            _currentUserProvider = currentUserProvider;
        }
        public async Task<Result<CreateTenantConfigResponse>> Handle(CreateTenantConfigCommand request, CancellationToken cancellationToken)
        {
            bool existConfig = await _context.TenantConfigs.AnyAsync(x => x.ConfigName == request.ConfigName);

            if (existConfig)
            {
                return Result.Conflict();
            }

            TenantConfig newConfig = new()
            {
                ConfigName = request.ConfigName,
                ConfigValue = request.ConfigValue,
                CreatedAt = DateTime.UtcNow,
                Creator = _currentUserProvider.GetUserId(),
                Enabled = request.Enabled,
                Visible = request.Visible,
                TenantId = request.TenantId,
                TenantConfigId = Guid.NewGuid(),
                ConfigType = request.ConfigType,
            };

            var result = await _tenantConfigService.Create(newConfig);
            if (result.IsSuccess)
            {
                return new CreateTenantConfigResponse(result.Value);
            }

            return (dynamic)result;
        }
    }
}
