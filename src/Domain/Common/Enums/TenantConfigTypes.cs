namespace Domain.Common.Enums
{
    public class TenantConfigTypes
    {
        public string Type { get; set; }
        public string Label { get; set; }
        public readonly static TenantConfigTypes Theming = new("Theming", "Theming");
        public readonly static TenantConfigTypes PaymentProcess = new("PaymentProcess", "Proceso de pago");
        public readonly static IEnumerable<TenantConfigTypes> All = [Theming, PaymentProcess];
        public TenantConfigTypes(string type, string label)
        {
            Type = type;
            Label = label;
        }
    }
}
