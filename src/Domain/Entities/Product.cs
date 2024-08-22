using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class Product : IAudit, ISoftDelete
    {
        public int ProductId { get; set; }
        public Guid TenantId { get; set; }
        public string ProductName { get; set; } = default!;
        public string? ProductDescription { get; set; }
        public string? Image { get; set; }
        public double ProductPrice { get; set; }
        public bool Visible { get; set; }
        public bool Deleted { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public Tenant Tenant { get; set; } = default!;
        public ICollection<ProductCategory> ProductCategories { get; set; } = [];
        public ICollection<ProductChoice> ProductChoices { get; set; } = [];
        public ICollection<ChoiceOption> ChoiceOptions { get; set; } = [];
        public ICollection<ProductDiscount> ProductDiscounts { get; set; } = [];
        public ICollection<OrderDetail> OrderDetails { get; set; } = [];
    }
}
