using Application.Common.Interfaces;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.TenantTemplates.Commands.UpdateTemplate
{
    public class UpdateTemplateCommandHandler : IRequestHandler<UpdateTemplateCommand, Result<UpdateTemplateResponse>>
    {
        private readonly ITenantTemplateService _tenantTemplateService;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IImageService _imageService;
        public UpdateTemplateCommandHandler(ITenantTemplateService tenantTemplateService, ICurrentUserProvider currentUserProvider, IImageService imageService)
        {
            _tenantTemplateService = tenantTemplateService;
            _currentUserProvider = currentUserProvider;
            _imageService = imageService;
        }

        public async Task<Result<UpdateTemplateResponse>> Handle(UpdateTemplateCommand request, CancellationToken cancellationToken)
        {
            if (request.Logo != null)
            {
                var response = await _imageService.UploadImageAsync(request.Logo, request.TenantTemplateId.ToString());
            }

            TenantTemplate tenantTemplate = new()
            {
                CreatedAt = DateTime.UtcNow,
                Description = request.Description,
                Logo = request.Logo != null ? request.TenantTemplateId.ToString() : request.TenantId.ToString(),
                Header = request.Header,
                TenantId = request.TenantId,
                Name = request.Name,
                Modifier = _currentUserProvider.GetUserId(),
                TenantTemplateId = request.TenantTemplateId,
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
