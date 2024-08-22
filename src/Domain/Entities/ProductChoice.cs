using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class ProductChoice : IAudit
    {
        public int ProductChoiceId { get; set; }
        public int ProductId { get; set; }
        public string Choice { get; set; } = default!;
        public int Quantity { get; set; }
        public bool Required { get; set; }
        public bool Visible { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Product Product { get; set; } = default!;
        public List<ChoiceOption>? ChoiceOptions { get; set; }
    }
}
