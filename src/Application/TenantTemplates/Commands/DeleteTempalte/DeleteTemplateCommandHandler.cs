using Application.Common.Interfaces;
using Application.Common.Security;
using Ardalis.Result;
using MediatR;

namespace Application.TenantTemplates.Commands.DeleteTemplate
{
    public class DeleteTemplateCommandHandler : IRequestHandler<DeleteTemplateCommand, Result>
    {
        private readonly ITenantTemplateService _tenantTemplateService;
        private readonly ICurrentUserProvider _currentUserProvider;
        public DeleteTemplateCommandHandler(ITenantTemplateService tenantTemplateService, ICurrentUserProvider currentUserProvider)
        {
            _tenantTemplateService = tenantTemplateService;
            _currentUserProvider = currentUserProvider;
        }

        public async Task<Result> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            var result = await _tenantTemplateService
                .Delete(request.TenantTemplateId, request.TenantId, _currentUserProvider.GetUserId());

            return result;
        }
    }
}
