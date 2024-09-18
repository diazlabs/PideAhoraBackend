using Application.Common.Interfaces;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.TenantTemplates.Commands.CreateTemplate
{
    public class CreateTemplateCommandHandler : IRequestHandler<CreateTemplateCommand, Result<CreateTemplateResponse>>
    {
        private readonly ITenantTemplateService _tenantTemplateService;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IImageService _imageService;
        public CreateTemplateCommandHandler(ITenantTemplateService tenantTemplateService, ICurrentUserProvider currentUserProvider, IImageService imageService)
        {
            _tenantTemplateService = tenantTemplateService;
            _currentUserProvider = currentUserProvider;
            _imageService = imageService;
        }

        public async Task<Result<CreateTemplateResponse>> Handle(CreateTemplateCommand request, CancellationToken cancellationToken)
        {
            Guid templateId = Guid.NewGuid();

            if (request.Logo != null)
            {
                var response = await _imageService.UploadImageAsync(request.Logo, templateId.ToString());
            }

            TenantTemplate tenantTemplate = new()   
            {
                TenantTemplateId = templateId,
                Header = request.Header,
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                Logo = request.Logo != null ? templateId.ToString() : request.TenantId.ToString(),
                Creator = _currentUserProvider.GetUserId(),
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
