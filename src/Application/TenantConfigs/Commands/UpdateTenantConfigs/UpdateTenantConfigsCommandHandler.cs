using Application.Common.Persistence;
using Application.Common.Security;
using Ardalis.Result;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.TenantConfigs.Commands.UpdateTenantConfigs
{
    public class UpdateTenantConfigsCommandHandler : IRequestHandler<UpdateTenantConfigsCommand, Result<UpdateTenantConfigsResponse>>
    {
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly ApplicationContext _context;
        private readonly ILogger<UpdateTenantConfigsCommandHandler> _logger;

        public UpdateTenantConfigsCommandHandler(ICurrentUserProvider currentUserProvider, ApplicationContext context, ILogger<UpdateTenantConfigsCommandHandler> logger)
        {
            _currentUserProvider = currentUserProvider;
            _context = context;
            _logger = logger;
        }

        public async Task<Result<UpdateTenantConfigsResponse>> Handle(UpdateTenantConfigsCommand request, CancellationToken cancellationToken)
        {
            var configs = await _context.TenantConfigs
                .Where(x => x.TenantId == request.TenantId)
                .ToListAsync(cancellationToken);

            if (configs.Count == 0)
            {
                return Result<UpdateTenantConfigsResponse>.NotFound();
            }

            _logger.LogInformation(
                "Updating tenantConfigs {tenantId}, original {@originalConfigs}",
                request.TenantId,
                configs
            );


            foreach (var config in request.Configs)
            {
                var configToUpdate = configs.FirstOrDefault(x => x.TenantConfigId == config.Configid);

                if (configToUpdate == null)
                {
                    continue;
                }

                configToUpdate.ConfigValue = config.ConfigValue;
                configToUpdate.Enabled = config.Enabled;
                configToUpdate.Modifier = _currentUserProvider.GetUserId();
                configToUpdate.UpdatedAt = DateTime.UtcNow;
            }

            _logger.LogInformation(
                "Updated tenantConfigs {tenantId}, with new {@newConfigs}",
                request.TenantId,
                request.Configs
            );

            int rows = await _context.SaveChangesAsync(cancellationToken);
            if (rows > 0)
            {
                return new UpdateTenantConfigsResponse();
            }

            return Result.Error("Errora al actulizar las configuraciones");
        }
    }
}
