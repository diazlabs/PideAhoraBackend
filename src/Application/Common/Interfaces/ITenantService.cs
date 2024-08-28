using Ardalis.Result;
using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface ITenantService
    {
        Task<Tenant?> FindTenantById(Guid tenantId);
        Task<List<Tenant>> GetTenantsByUserId(Guid userId);
        Task<Result<Tenant>> Create(Tenant tenant);
        Task<Result> Update(Tenant tenant);
        Task<Result> Delete(Guid tenantId);
        Task<Result> SetActiveTemplateForTenantId(Guid tenantId, Guid tenantTemplateId, Guid modifier);
    }
}
