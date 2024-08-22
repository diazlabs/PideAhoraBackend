using Domain.Entities;

namespace Application.Common.Interfaces
{
    public interface IProductService
    {
        Task<Product?> FindProductById(int productId, Guid tenantId, CancellationToken cancellationToken = default, bool includeChoices = false);
        Task<bool> ProductExist(int productId, Guid tenantId, CancellationToken cancellationToken = default);
    }
}
