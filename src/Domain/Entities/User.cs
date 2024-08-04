using Domain.Common.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<Guid>, IAudit
    {
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Country { get; set; } = default!;
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public List<Tenant>? Tenants { get; set; }
        public List<Order>? Orders { get; set; }
    }
}