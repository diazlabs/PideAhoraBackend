using Application.Common.Interfaces;
using Application.Common.Persistence;
using Ardalis.Result;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Services
{
    public class TenantConfigService : ITenantConfigService
    {
        private readonly ApplicationContext _context;
        public TenantConfigService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Result<TenantConfig>> Create(TenantConfig tenantConfig)
        {
            _context.TenantConfigs.Add(tenantConfig);

            int rows = await _context.SaveChangesAsync();
            if (rows > 0)
            {
                return tenantConfig;
            }

            return Result.Error("No se pudo crear la configuración, intenta de nuevo.");
        }

        public async Task<TenantConfig?> FindTenantConfigById(Guid tenantConfigId)
        {
            TenantConfig? config = await _context.TenantConfigs.FindAsync(tenantConfigId);
            return config;
        }

        public async Task<List<TenantConfig>> GetTenantConfigsByTenantId(Guid tenantId)
        {
            var configs = await _context.TenantConfigs
                .Where(x => x.TenantId == tenantId)
                .ToListAsync();

            return configs;
        }

        public async Task<Result> ToggleConfig(Guid tenantConfigId, bool enabled)
        {
            TenantConfig? config = await FindTenantConfigById(tenantConfigId);
            if (config == null)
            {
                return Result.NotFound();
            }

            config.Enabled = enabled;

            int rows = await _context.SaveChangesAsync();
            if (rows > 0)
            {
                return Result.Success();
            }

            return Result.Error("No se pudo guardar, intenta de nuevo.");
        }

        public async Task<Result> Update(TenantConfig tenantConfig)
        {
            TenantConfig? entityToUpdate = await FindTenantConfigById(tenantConfig.TenantConfigId);
            if (entityToUpdate == null)
            {
                return Result.NotFound();
            }

            entityToUpdate = tenantConfig;
            _context.TenantConfigs.Update(entityToUpdate);

            int rows = await _context.SaveChangesAsync();
            if (rows > 0)
            {
                return Result.Success();
            }

            return Result.Error("Error al actualziar, intente de nuevo.");
        }
    }
}
