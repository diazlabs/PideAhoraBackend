using Application.Common.Interfaces;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Result<CreateTenantResponse>>
    {
        private readonly ITenantService _tenantService;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IImageService _imageService;
        public CreateTenantCommandHandler(ITenantService tenantService, ICurrentUserProvider currentUserProvider, IImageService imageService)
        {
            _tenantService = tenantService;
            _currentUserProvider = currentUserProvider;
            _imageService = imageService;
        }

        public async Task<Result<CreateTenantResponse>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            var tenantId = Guid.NewGuid();
            var logoId = await _imageService.UploadImageAsync(request.Logo, tenantId.ToString());

            Tenant newTenant = new()
            {
                Path = request.Path,
                PageTitle = request.PageTitle,
                UserId = _currentUserProvider.GetUserId(),
                Name = request.Name,
                Logo = logoId,
                TenantId = tenantId,
                Category = request.Category,
                CreatedAt = DateTime.UtcNow,
                Creator = _currentUserProvider.GetUserId(),
                Enabled = true,
                ActiveTenantTemplateId = Guid.Empty,
                LastPayment = DateTime.UtcNow,
                Description = request.Description,
            };

            var result = await _tenantService.Create(newTenant);
            if (result.IsSuccess)
            {
                return new CreateTenantResponse(
                    result.Value.TenantId,
                    result.Value.UserId,
                    result.Value.Path,
                    result.Value.Name,
                    result.Value.PageTitle,
                    result.Value.Description,
                    result.Value.Logo,
                    result.Value.Category
                );
            }

            return (dynamic)result;
        }
    }
}
