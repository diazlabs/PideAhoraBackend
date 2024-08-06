using Application.Common.Interfaces;
using Ardalis.Result;
using MediatR;

namespace Application.Tenants.Commands.SetActiveTenant
{
    public class SetActiveTenantCommandHandler : IRequestHandler<SetActiveTenantCommand, Result<SetActiveTenantResponse>>
    {
        private readonly ITenantRepository _tenantRepository;
        public SetActiveTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<Result<SetActiveTenantResponse>> Handle(SetActiveTenantCommand request, CancellationToken cancellationToken)
        {
            return await _tenantRepository.SetActiveTemplateForTenantId(request.TenantId, request.TemplateId);
        }
    }
}
