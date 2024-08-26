using Application.Common.Interfaces;
using Ardalis.Result;
using MediatR;

namespace Application.Tenants.Commands.SetActiveTenant
{
    public class SetActiveTemplateCommandHandler : IRequestHandler<SetActiveTemplateCommand, Result<SetActiveTemplateResponse>>
    {
        private readonly ITenantService _tenantService;
        public SetActiveTemplateCommandHandler(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        public async Task<Result<SetActiveTemplateResponse>> Handle(SetActiveTemplateCommand request, CancellationToken cancellationToken)
        {
            return await _tenantService.SetActiveTemplateForTenantId(request.TenantId, request.TemplateId);
        }
    }
}
