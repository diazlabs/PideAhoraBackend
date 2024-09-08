namespace Domain.Common.Enums
{
    public class TenantCategory
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public static TenantCategory[] Categories => [Supermarket, Bakery, Gifts, Other];
        public static readonly TenantCategory Supermarket = new("xd1", "Supermercado");
        public static readonly TenantCategory Bakery = new("xd2", "Pasteleria");
        public static readonly TenantCategory Gifts = new("xd3", "Regalos");
        public static readonly TenantCategory Other = new("xd3", "Otro");

        public TenantCategory(string code, string name)
        {
            Code = code;
            Name = name;
        }
    }
}
