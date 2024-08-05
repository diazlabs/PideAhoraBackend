using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class TenantTemplate : IAudit
    {
        public Guid TenantTemplateId { get; set; }
        public Guid TenantId { get; set; }
        public string Header { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Logo { get; set; } = default!;
        public bool Deleted { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Tenant Tenant { get; set; } = default!;
        public List<TemplateSection>? TemplateSections { get; set; }
    }
}
