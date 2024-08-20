using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Result<CreateTenantResponse>>
    {
        private readonly ITenantService _tenantService;
        public CreateTenantCommandHandler(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        public async Task<Result<CreateTenantResponse>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            Tenant newTenant = new()
            {
                Path = request.Path,
                PageTitle = request.PageTitle,
                UserId = request.UserId,
                Name = request.Name,
                Logo = "",//request.Logo,
                TenantId = Guid.NewGuid(),
                Category = request.Category,
            };

            var result = await _tenantService.Create(newTenant);
            if (result.IsSuccess)
            {
                return new CreateTenantResponse(result.Value);
            }

            return (dynamic)result;
        }
    }
}
