namespace Domain.Entities
{
    public class ChoiceOption
    {
        public int ChoiceOptionId { get; set; }
        public int ChoiceId { get; set; }
        public int ProductId { get; set; }
        public double OptionPrice { get; set; }
        public bool Visible { get; set; }
        public Product Product { get; set; } = default!;    
        public ProductChoice ProductChoice { get; set; } = default!;
        public ICollection<OrderDetailOption> OrderDetailOptions { get; set; } = [];
    }
}
