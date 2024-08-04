using System.Text.RegularExpressions;

namespace Application.Common
{
    public partial class Regexs
    {
        public static readonly Regex PasswordRegex = PasswordRegexGenerator();
        public static readonly Regex PhoneRegex = PhoneNumberRegexGenerator();

        [GeneratedRegex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*[^a-zA-Z]).{8,16}$")]
        private static partial Regex PasswordRegexGenerator();
        [GeneratedRegex("[0-9]{8,10}")]
        public static partial Regex PhoneNumberRegexGenerator();
    }
}
