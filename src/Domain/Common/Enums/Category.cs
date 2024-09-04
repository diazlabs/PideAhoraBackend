namespace Domain.Common.Enums
{
    public class Category
    {
        public string Name { get; set; } = default!;
        public static List<Category> Categories => [FastFood];

        public static readonly Category FastFood = new("Comida Rapida");
        public Category(string name)
        {
            Name = name;
        }
    }
}
