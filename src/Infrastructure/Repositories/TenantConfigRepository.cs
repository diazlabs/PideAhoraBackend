using Application.Common.Interfaces;
using Ardalis.Result;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TenantConfigRepository : ITenantConfigRepository
    {
        private readonly ApplicationContext _context;
        public TenantConfigRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> ConfigExist(string configName)
        {
            return await _context.TenantConfigs.AnyAsync(x => x.ConfigName == configName);
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

        public async Task<Result> Delete(Guid tenantConfigId)
        {
            TenantConfig? config = await FindTenantConfigById(tenantConfigId);
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
