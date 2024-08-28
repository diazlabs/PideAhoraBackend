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
        public UpdateTenantCommandHandler(ITenantService tenantService, ICurrentUserProvider currentUserProvider)
        {
            _tenantService = tenantService;
            _currentUserProvider = currentUserProvider;
        }
        public async Task<Result<UpdateTenantResponse>> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
        {
            Tenant? tenant = await _tenantService.FindTenantById(request.TenantId);
            if (tenant == null)
            {
                return Result.NotFound();
            }

            tenant.UpdatedAt = DateTime.Now;
            tenant.Description = request.Description;
            tenant.TenantId = request.TenantId;
            tenant.Modifier = _currentUserProvider.GetUserId();
            tenant.Category = request.Category;
            tenant.PageTitle = request.PageTitle;
            tenant.Path = request.Path;
            tenant.Name = request.Name;
            tenant.Logo = "logo";

            var result = await _tenantService.Update(tenant);
            if (result.IsSuccess)
            {
                return new UpdateTenantResponse();
            }

            return result;
        }
    }
}
