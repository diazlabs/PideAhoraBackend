using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using MediatR;

namespace Application.Tenants.Commands.CreateTenant
{
    public class CreateTenantCommandHandler : IRequestHandler<CreateTenantCommand, Result<CreateTenantResponse>>
    {
        private readonly ITenantRepository _tenantRepository;
        public CreateTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<Result<CreateTenantResponse>> Handle(CreateTenantCommand request, CancellationToken cancellationToken)
        {
            Tenant newTenant = new()
            {

            };

            var result = await _tenantRepository.Create(newTenant);
            if (result.IsSuccess)
            {
                return new CreateTenantResponse(result.Value);
            }

            return (dynamic)result;
        }
    }
}
