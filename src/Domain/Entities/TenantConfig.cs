using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class TenantConfig : IAudit
    {
        public Guid TenantConfigId {  get; set; }
        public Guid TenantId { get; set; }
        public string ConfigName { get; set; } = default!;
        public string ConfigValue { get; set; } = default!;
        public bool Enabled { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Tenant Tenant { get; set; } = default!;
    }
}
