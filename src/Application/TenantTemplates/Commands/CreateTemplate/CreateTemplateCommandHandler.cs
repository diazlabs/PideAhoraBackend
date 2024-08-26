using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.TenantTemplates.Commands.CreateTemplate
{
    public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, Result<CreateTemplateResponse>>
    {
        private readonly ITenantTemplateService _tenantTemplateService;
        public CreateTemplateCommandHandler(ITenantTemplateService tenantTemplateService)
        {
            _tenantTemplateService = tenantTemplateService;
        }

        public async Task<Result<CreateTemplateResponse>> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            TenantTemplate tenantTemplate = new()
            {
                CreatedAt = DateTime.UtcNow,
                Creator = request.Creator,
                Description = request.Description,
                Logo = request.Logo,
                Header = request.Header,
                TenantId = request.TenantId,
            };

            var result = await _tenantTemplateService.Create(tenantTemplate);
            if (result.IsSuccess)
            {
                return new CreateTemplateResponse();
            }

            return (dynamic)result;
        }
    }
}
