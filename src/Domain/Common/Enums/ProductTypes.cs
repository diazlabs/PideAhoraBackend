
namespace Domain.Common.Enums
{
    public class ProductType
    {
        public string Type { get; set; }
        public string Label { get;set; }
        public static ProductType[] Types => [Product, Extra];

        public static ProductType Product = new("ProductoCompleto", "Producto Completo");
        public static ProductType Extra = new("Extra", "Extra");
        public ProductType(string type, string label)
        {
            Type = type;
            Label = label;
        }
    }
}
