using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.TenantConfigs.Commands.CreateTenantConfig
{
    public class CreateTenantConfigCommandHandler : IRequestHandler<CreateTenantConfigCommand, Result<CreateTenantConfigResponse>>
    {
        private readonly ITenantConfigRepository _tenantConfigRepository;
        public CreateTenantConfigCommandHandler(ITenantConfigRepository tenantConfigRepository)
        {
            _tenantConfigRepository = tenantConfigRepository;
        }
        public async Task<Result<CreateTenantConfigResponse>> Handle(CreateTenantConfigCommand request, CancellationToken cancellationToken)
        {
            if (await _tenantConfigRepository.ConfigExist(request.ConfigName))
            {
                return Result.Conflict();
            }

            TenantConfig newConfig = new()
            {
                ConfigName = request.ConfigName,
                ConfigValue = request.ConfigValue,
                CreatedAt = DateTime.Now,
                Creator = request.Creator,
                Enabled = request.Enabled,
                TenantId = request.TenantId,
                TenantConfigId = Guid.NewGuid(),
            };

            var result = await _tenantConfigRepository.Create(newConfig);
            if (result.IsSuccess)
            {
                return new CreateTenantConfigResponse(result.Value);
            }

            return (dynamic)result;
        }
    }
}
