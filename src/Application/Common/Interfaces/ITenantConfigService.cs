using Ardalis.Result;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITenantConfigService
    {
        Task<TenantConfig?> FindTenantConfigById(Guid tenantConfigId);
        Task<List<TenantConfig>> GetTenantConfigsByTenantId(Guid tenantId);
        Task<Result<TenantConfig>> Create(TenantConfig tenantConfig);
        Task<Result> ToggleConfig(Guid tenantConfigId, bool enabled);
        Task<Result> Update(TenantConfig tenantConfig);
    }
}
