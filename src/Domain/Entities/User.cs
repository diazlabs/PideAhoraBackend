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
        public ICollection<Tenant> Tenants { get; set; } = [];
        public ICollection<Order> Orders { get; set; } = [];
    }
}