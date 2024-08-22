
namespace Domain.Entities
{
    public class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public Guid OrderId { get; set; }
        public int ProductId { get; set; }
        public int? ProductDiscountId { get; set; }
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
        public Order Order { get; set; } = default!;
        public Product Product { get; set; } = default!;
        public ProductDiscount? ProductDiscount { get; set; }
        public ICollection<OrderDetailOption> OrderDetailOptions { get; set; } = [];
    }
}
