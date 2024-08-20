using Application.Common.Interfaces;
using Ardalis.Result;
using MediatR;

namespace Application.Tenants.Commands.DeleteTenant
{
    public class DeleteTenantCommandHandler : IRequestHandler<DeleteTenantCommand, Result<DeleteTenantResponse>>
    {
        private readonly ITenantService _tenantService;
        public DeleteTenantCommandHandler(ITenantService tenantService)
        {
            _tenantService = tenantService;
        }
        public async Task<Result<DeleteTenantResponse>> Handle(DeleteTenantCommand request, CancellationToken cancellationToken)
        {
            return await _tenantService.Delete(request.TenantId);
        }
    }
}
