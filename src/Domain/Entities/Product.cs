using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class Product : IAudit
    {
        public int ProductId { get; set; }
        public Guid TenandId { get; set; }
        public string ProductName { get; set; } = default!;
        public string? ProdcutDescription { get; set; }
        public string? Image { get; set; }
        public double ProductPrice { get; set; }
        public bool Visible { get; set; }
        public bool Deleted { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Tenant Tenant { get; set; } = default!;
        public List<ProductCategory>? ProductCategories { get; set; }
        public List<ProductChoice>? ProductChoices { get; set; }
        public List<ChoiceOption>? ChoiceOptions { get; set; }
        public List<ProductDiscount>? ProductDiscounts { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
