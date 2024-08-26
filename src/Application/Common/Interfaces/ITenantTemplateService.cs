using Ardalis.Result;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITenantTemplateService
    {
        Task<TenantTemplate?> FindTenantTemplateById(Guid tenantTempalteId, Guid tenantId, CancellationToken cancellationToken = default);
        Task<List<TenantTemplate>> GetTenantTemplatesByTenantId(Guid tenantId);
        Task<Result<TenantTemplate>> Create(TenantTemplate tenantTemplate);
        Task<Result> Update(TenantTemplate tenantTemplate);
        Task<Result> Delete(Guid tenantTempalteId, Guid tenantId, Guid deletedBy);
    }
}
