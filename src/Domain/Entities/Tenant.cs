using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class Tenant : IAudit
    {
        public Guid TenantId { get; set; }
        public Guid UserId { get; set; }
        public string Path { get; set; } = default!;
        public string PageTitle { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string Logo { get; set; } = default!;
        public bool Enabled { get; set; }
        public string Category { get; set; } = default!;
        public Guid ActiveTemplateId { get; set; }
        public DateTime LastPayment { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public User User { get; set; } = default!;
        public List<TenantConfig>? TenantConfigs { get; set; }
        public List<TenantTemplate>? TenantTemplates{ get; set; }
        public List<Order>? Orders { get; set; }
    }
}
