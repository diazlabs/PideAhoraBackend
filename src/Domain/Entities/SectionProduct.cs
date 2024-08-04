
namespace Domain.Entities
{
    public class SectionProduct
    {
        public int SectionProductId { get; set; }
        public int ProductId { get; set; }
        public int TemplateSectionId { get; set; }
        public int Order { get; set; }
        public Product Product { get; set; } = default!;
        public TemplateSection TemplateSection { get; set; } = default!;
    }
}
