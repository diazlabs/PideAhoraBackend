using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Tenants.Commands.UpdateTenant
{
    public class UpdateTenantCommandHandler : IRequestHandler<UpdateTenantCommand, Result<UpdateTenantResponse>>
    {
        private readonly ITenantRepository _tenantRepository;
        public UpdateTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<Result<UpdateTenantResponse>> Handle(UpdateTenantCommand request, CancellationToken cancellationToken)
        {
            Tenant? tenant = await _tenantRepository.FindTenantById(request.TenantId);
            if (tenant == null)
            {
                return Result.NotFound();
            }

            tenant.UpdatedAt = DateTime.Now;
            tenant.Description = request.Description;
            tenant.TenantId = request.TenantId;
            tenant.Modifier = request.UserId;
            tenant.Category = request.Category;
            tenant.PageTitle = request.PageTitle;
            tenant.Path = request.Path;
            tenant.Name = request.Name;
            tenant.Logo = "logo";

            var result = await _tenantRepository.Update(tenant);
            if (result.IsSuccess)
            {
                return new UpdateTenantResponse();
            }

            return result;
        }
    }
}
