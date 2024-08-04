using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Enums
{
    public class Country
    {
        public string Code { get; set; } = default!;
        public string Prefix { get; set; }
        public string Name { get; set; }
        public static List<Country> Countries => [Honduras];

        public static readonly Country Honduras = new("HN", "+504", "Honduras");
        public Country(string code, string prefix, string name)
        {
            Code = code;
            Prefix = prefix;
            Name = name;
        }
    }
}
