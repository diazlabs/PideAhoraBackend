using Ardalis.Result;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITenantConfigRepository
    {
        Task<TenantConfig?> FindTenantConfigById(Guid tenantConfigId);
        Task<List<TenantConfig>> GetTenantConfigsByTenantId(Guid tenantId);
        Task<Result<TenantConfig>> Create(TenantConfig tenantConfig);
        Task<Result> Update(TenantConfig tenantConfig);
        Task<Result> Delete(Guid tenantConfigId);
        Task<Result> ToggleConfig(Guid tenantConfigId);
    }
}
