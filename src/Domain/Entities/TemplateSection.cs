﻿using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class TemplateSection : IAudit, ISoftDelete
    {
        public int TemplateSectionId { get; set; }
        public Guid TenantTemplateId { get; set; }
        public int SectionVariantId { get; set; }
        public string SectionName { get; set; } = default!;
        public string SectionDescription { get; set; } = default!;
        public int Order {  get; set; }
        public bool Visible { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public TenantTemplate TenantTemplate { get; set; } = default!;
        public ICollection<SectionProduct> SectionProducts { get; set; } = [];
        public ICollection<SectionConfig> SectionConfigs { get; set; } = [];
    }
}
