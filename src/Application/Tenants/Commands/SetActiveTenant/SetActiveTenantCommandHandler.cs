using Application.Common.Interfaces;
using Ardalis.Result;
using MediatR;

namespace Application.Tenants.Commands.SetActiveTenant
{
    public class SetActiveTenantCommandHandler : IRequestHandler<SetActiveTenantCommand, Result<SetActiveTenantResponse>>
    {
        private readonly ITenantService _tenantService;
        public SetActiveTenantCommandHandler(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        public async Task<Result<SetActiveTenantResponse>> Handle(SetActiveTenantCommand request, CancellationToken cancellationToken)
        {
            return await _tenantService.SetActiveTemplateForTenantId(request.TenantId, request.TemplateId);
        }
    }
}
