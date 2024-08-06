using Application.Common.Interfaces;
using Ardalis.Result;
using MediatR;

namespace Application.Tenants.Commands.DeleteTenant
{
    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand, Result<DeleteTenantResponse>>
    {
        private readonly ITenantRepository _tenantRepository;
        public DeleteTenantCommandHandler(ITenantRepository tenantRepository)
        {
            _tenantRepository = tenantRepository;
        }
        public async Task<Result<DeleteTenantResponse>> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            return await _tenantRepository.Delete(request.TenantId);
        }
    }
}
