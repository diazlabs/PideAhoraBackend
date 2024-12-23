﻿using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class Tenant : IAudit, ISoftDelete
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public string Path { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string PageTitle { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Logo { get; set; } = default!;
        public bool Enabled { get; set; }
        public bool Deleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public Guid? DeletedBy { get; set; }
        public string Category { get; set; } = default!;
        public Guid ActiveTenantTemplateId { get; set; }
        public DateTime LastPayment { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public User User { get; set; } = default!;
        public ICollection<TenantConfig> TenantConfigs { get; set; } = [];
        public ICollection<TenantTemplate> TenantTemplates{ get; set; } = [];
        public ICollection<Order> Orders { get; set; } = [];
    }
}
