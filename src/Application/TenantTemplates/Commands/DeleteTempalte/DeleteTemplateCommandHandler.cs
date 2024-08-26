using Application.Common.Interfaces;
using Ardalis.Result;
using MediatR;

namespace Application.TenantTemplates.Commands.DeleteTemplate
{
    public class DeleteTemplateCommandHandler : IRequestHandler<DeleteTemplateCommand, Result>
    {
        private readonly ITenantTemplateService _tenantTemplateService;
        public DeleteTemplateCommandHandler(ITenantTemplateService tenantTemplateService)
        {
            _tenantTemplateService = tenantTemplateService;
        }

        public async Task<Result> Handle(DeleteTemplateCommand request, CancellationToken cancellationToken)
        {
            var result = await _tenantTemplateService.Delete(request.TemplateId, request.TenantId, request.DeletedBy);

            return result;
        }
    }
}
