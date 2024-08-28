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
        public CreateTenantCommandHandler(ITenantService tenantService, ICurrentUserProvider currentUserProvider)
        {
            _tenantService = tenantService;
            _currentUserProvider = currentUserProvider;
        }
        public async Task<Result<CreateTenantResponse>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            Tenant newTenant = new()
            {
                Path = request.Path,
                PageTitle = request.PageTitle,
                UserId = _currentUserProvider.GetUserId(),
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
