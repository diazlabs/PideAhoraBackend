namespace Domain.Entities
{
    public class OrderDetailOption
    {
        public int OrderDetailOptionId { get; set; }
        public int OrderDetailId { get; set; }
        public int OptionId { get; set; }
        public int Quantity { get; set; }
        public double ProductPrice { get; set; }
        public OrderDetail OrderDetail { get; set; } = default!;
        public ChoiceOption ChoiceOption { get; set; } = default!;
    }
}
