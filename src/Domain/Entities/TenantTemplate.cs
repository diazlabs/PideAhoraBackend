﻿using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class TenantTemplate : IAudit, ISoftDelete
    {
        public Guid TenantTemplateId { get; set; }
        public Guid TenantId { get; set; }
        public string Name { get; set; } = default!;
        public string Header { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Logo { get; set; } = default!;
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Tenant Tenant { get; set; } = default!;
        public ICollection<TemplateSection> TemplateSections { get; set; } = [];
    }
}
