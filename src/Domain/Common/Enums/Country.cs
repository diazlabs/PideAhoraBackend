namespace Domain.Common.Enums
{
    public class Country
    {
        public string Code { get; set; }
        public string Prefix { get; set; }
        public string Name { get; set; }
        public string Mask { get; set; }
        public static List<Country> Countries => [Honduras];

        public static readonly Country Honduras = new("HN", "+504", "Honduras", "9999-9999");
        public Country(string code, string prefix, string name, string mask)
        {
            Code = code;
            Prefix = prefix;
            Name = name;
            Mask = mask;
        }
    }
}
