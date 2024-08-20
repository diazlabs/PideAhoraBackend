using Domain.Common.Interfaces;

namespace Domain.Entities
{
    public class ChoiceOption : IAudit
    {
        public int ChoiceOptionId { get; set; }
        public int ChoiceId { get; set; }
        public int ProductId { get; set; }
        public double OptionPrice { get; set; }
        public Guid Creator { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? Modifier { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Product Product { get; set; } = default!;    
        public ProductChoice ProductChoice { get; set; } = default!;
        public List<OrderDetailOption>? OrderDetailOptions { get; set; }
    }
}
