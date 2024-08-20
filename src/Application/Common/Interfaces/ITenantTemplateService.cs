using Ardalis.Result;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITenantTemplateService
    {
        Task<TenantTemplate?> FindTenantTemplateById(Guid tenantTempalteId);
        Task<List<TenantTemplate>> GetTenantTemplatesByTenantId(Guid tenantId);
        Task<Result<TenantTemplate>> Create(TenantTemplate tenantTemplate);
        Task<Result> Update(TenantTemplate tenantTemplate);
        Task<Result> Delete(Guid tenantTempalteId);
    }
}
