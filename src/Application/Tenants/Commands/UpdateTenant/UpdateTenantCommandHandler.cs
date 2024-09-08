using Application.Common.Interfaces;
using Application.Common.Security;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Tenants.Commands.UpdateTenant
{
    public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, Result<UpdateTenantResponse>>
    {
        private readonly ITenantService _tenantService;
        private readonly ICurrentUserProvider _currentUserProvider;
        private readonly IImageService _imageService;
        public UpdateTenantCommandHandler(ITenantService tenantService, ICurrentUserProvider currentUserProvider, IImageService imageService)
        {
            _tenantService = tenantService;
            _currentUserProvider = currentUserProvider;
            _imageService = imageService;
        }
        public async Task<Result<UpdateTenantResponse>> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
        {
            Tenant? tenant = await _tenantService.FindTenantById(request.TenantId);
            if (tenant == null)
            {
                return Result.NotFound();
            }

            if (request.Logo != null)
            {
                var logo = await _imageService.UploadImageAsync(request.Logo, tenant.TenantId.ToString());
                if (logo.Length <= 0)
                {
                    return Result.Error("Error al actuzliar el logo");
                }
            }

            tenant.UpdatedAt = DateTime.UtcNow;
            tenant.Description = request.Description;
            tenant.Modifier = _currentUserProvider.GetUserId();
            tenant.Category = request.Category;
            tenant.PageTitle = request.PageTitle;
            tenant.Path = request.Path;
            tenant.Name = request.Name;

            var result = await _tenantService.Update(tenant);
            if (result.IsSuccess)
            {
                return new UpdateTenantResponse();
            }

            return result;
        }
    }
}
