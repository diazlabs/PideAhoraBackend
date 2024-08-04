using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class SectionConfig : IAudit
    {
        public int SectionConfigId { get; set; }
        public int TemplateSectionId { get; set; }
        public string SectionConfigName { get; set; } = default!;
        public string SectionConfigValue { get; set; } = default!;
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TemplateSection TemplateSection { get; set; } = default!;
    }
}
