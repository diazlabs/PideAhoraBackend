using Application.Common.Interfaces;
using Ardalis.Result;
using MediatR;

namespace Application.TenantConfigs.Commands.DeleteTenantConfig
{
    public class DeleteTenantConfigCommandHandler : IRequestHandler<DeleteTenantConfigCommand, Result<DeleteTenantConfigResponse>>
    {
        private readonly ITenantConfigRepository _tenantConfigRepository;
        public DeleteTenantConfigCommandHandler(ITenantConfigRepository tenantConfigRepository)
        {
            _tenantConfigRepository = tenantConfigRepository;
        }
        public async Task<Result<DeleteTenantConfigResponse>> Handle(DeleteTenantConfigCommand request, CancellationToken cancellationToken)
        {
            Result result = await _tenantConfigRepository.Delete(request.TenantConfigId);

            return result;
        }
    }
}
