using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class TemplateSection : IAudit
    {
        public int TemplateSectionId { get; set; }
        public Guid TemplateId { get; set; }
        public int SectionVariantId { get; set; }
        public int Order {  get; set; }
        public bool Visible { get; set; }
        public bool Deleted { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TenantTemplate TenantTemplate { get; set; } = default!;
        public List<SectionConfig>? SectionConfigs { get; set; }
    }
}
