using Domain.Common.Enums;
using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class Order : IAudit
    {
        public Guid OrderId { get; set; }
        public Guid TenantId { get; set; }
        public string? Name { get; set; }
        public int? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public Guid? UserId { get; set; }
        public string? OrderNotes { get; set; }
        public string DeliveryType { get; set; } = default!;
        public string? DeliveryAddress { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public string? DeliveryNotes { get; set; }
        public DateTime PlacedAt { get; set; } = DateTime.Now;
        public string Status { get; set; } = OrderStatus.Created;
        public decimal Total { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Tenant Tenant { get; set; } = default!;
        public User? User { get; set; }
        public List<OrderDetail>? OrderDetails { get; set; }
    }
}
