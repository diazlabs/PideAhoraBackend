namespace Domain.Entities
{
    public class ProductChoice
    {
        public int ProductChoiceId { get; set; }
        public int ProductId { get; set; }
        public string Choice { get; set; } = default!;
        public int Quantity { get; set; }
        public bool Required { get; set; }
        public bool Visible { get; set; }
        public Product Product { get; set; } = default!;
        public ICollection<ChoiceOption> ChoiceOptions { get; set; } = [];
    }
}
