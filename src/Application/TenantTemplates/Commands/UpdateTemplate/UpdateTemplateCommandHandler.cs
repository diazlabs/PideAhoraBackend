using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.TenantTemplates.Commands.UpdateTemplate
{
    public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand, Result<UpdateTemplateResponse>>
    {
        private readonly ITenantTemplateService _tenantTemplateService;
        public UpdateTemplateCommandHandler(ITenantTemplateService tenantTemplateService)
        {
            _tenantTemplateService = tenantTemplateService;
        }

        public async Task<Result<UpdateTemplateResponse>> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {
            TenantTemplate tenantTemplate = new()
            {
                CreatedAt = DateTime.UtcNow,
                Creator = request.Modifier,
                Description = request.Description,
                Logo = request.Logo,
                Header = request.Header,
                TenantId = request.TenantId,
                Modifier = request.Modifier,
                TenantTemplateId = request.TemplateId,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await _tenantTemplateService.Update(tenantTemplate);
            if (result.IsSuccess)
            {
                return new UpdateTemplateResponse();
            }

            return (dynamic)result;
        }
    }
}
