using Application.Orders.Commands.CreateOrder;

namespace Application.Common.Interfaces
{
    public interface IOrderService
    {
        Task<bool> ValidateDiscount(int productId, int discountId);
        Task<bool> ValidateDetailOptions(int productId, IEnumerable<(int optionId, int quantity)> options);
    }
}
