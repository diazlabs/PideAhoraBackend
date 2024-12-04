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
            TenantTemplate? tenantTemplate = await _tenantTemplateService
                .FindTenantTemplateById(request.TenantTemplateId, request.TenantId, cancellationToken);

            if (tenantTemplate == null)
            {
                return Result.NotFound();
            }

            if (request.Logo != null)
            {
                var response = await _imageService.UploadImageAsync(request.Logo, request.TenantTemplateId.ToString());
            }

            tenantTemplate.UpdatedAt = DateTime.UtcNow;
            tenantTemplate.Modifier = _currentUserProvider.GetUserId();
            tenantTemplate.Header = request.Header;
            tenantTemplate.Name = request.Name;
            tenantTemplate.Description = request.Description;

            var result = await _tenantTemplateService.Update(tenantTemplate);
            if (result.IsSuccess)
            {
                return new UpdateTemplateResponse();
            }

            return (dynamic)result;
        }
    }
}
