using Application.Common.Interfaces;
using Application.Common.Security;
using Ardalis.Result;
using MediatR;

namespace Application.Tenants.Commands.SetActiveTenant
{
    public class SetActiveTemplateCommandHandler : IRequestHandler<SetActiveTemplateCommand, Result<SetActiveTemplateResponse>>
    {
        private readonly ITenantService _tenantService;
        private readonly ICurrentUserProvider _currentUserProvider;
        public SetActiveTemplateCommandHandler(ITenantService tenantService, ICurrentUserProvider currentUserProvider)
        {
            _tenantService = tenantService;
            _currentUserProvider = currentUserProvider;
        }
        public async Task<Result<SetActiveTemplateResponse>> Handle(SetActiveTemplateCommand request, CancellationToken cancellationToken)
        {
            return await _tenantService
                .SetActiveTemplateForTenantId(request.TenantId, request.TenantTemplateId, _currentUserProvider.GetUserId());
        }
    }
}
