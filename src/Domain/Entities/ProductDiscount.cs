using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class ProductDiscount : IAudit
    {
        public int ProductDiscountId { get; set; }
        public int ProductId { get; set; }
        public double Discount { get; set; }
        public string DiscountCode { get; set; } = default!;
        public DateTime Since { get; set; }
        public DateTime Until { get; set;}
        public bool Enabled { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Product Product { get; set; } = default!;
        public ICollection<OrderDetail> OrderDetails { get; set; } = [];
    }
}
