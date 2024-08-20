using Application.Common.Interfaces;
using Application.Common.Persistence;
using Ardalis.Result;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.TenantConfigs.Commands.DeleteTenantConfig
{
    public class DeleteTenantConfigCommandHandler : IRequestHandler<DeleteTenantConfigCommand, Result<DeleteTenantConfigResponse>>
    {
        private readonly ITenantConfigService _tenantConfigService;
        private readonly ApplicationContext _context;
        public DeleteTenantConfigCommandHandler(ITenantConfigService tenantConfigService, ApplicationContext context)
        {
            _tenantConfigService = tenantConfigService;
            _context = context;
        }
        public async Task<Result<DeleteTenantConfigResponse>> Handle(DeleteTenantConfigCommand request, CancellationToken cancellationToken)
        {
            TenantConfig? config = await _tenantConfigService.FindTenantConfigById(request.TenantConfigId);
            if (config == null)
            {
                return Result.NotFound();
            }
            _context.TenantConfigs.Remove(config);

            int rows = await _context.SaveChangesAsync();
            if (rows > 0)
            {
                return Result.Success();
            }

            return Result.Error("No se pudo borrar la configuración, intentan de nuevo.");
        }
    }
}
