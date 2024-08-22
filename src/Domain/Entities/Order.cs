using Domain.Common.Enums;
using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class Order : IAudit
    {
        public Guid OrderId { get; set; }
        public Guid TenantId { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Guid? UserId { get; set; }
        public string? OrderNotes { get; set; }
        public string DeliveryType { get; set; } = default!;
        public string? DeliveryAddress { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? DeliveryNotes { get; set; }
        public string Status { get; set; } = OrderStatus.Created;
        public double Total { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Tenant Tenant { get; set; } = default!;
        public User? User { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = [];
    }
}
