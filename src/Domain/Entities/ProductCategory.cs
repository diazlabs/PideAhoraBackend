
namespace Domain.Entities
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public int ProductId { get; set; }
        public string Category { get; set; } = default!;
        public Product Product { get; set; } = default!;
    }
}
