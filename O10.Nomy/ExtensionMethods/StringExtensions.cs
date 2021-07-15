using System.Linq;
using System.Text.RegularExpressions;

namespace O10.Nomy.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string[] SplitCamelCase(this string source)
        {
            return Regex.Split(source, @"(?<!^)(?=[A-Z])");
        }

        public static string ToUnderscoreDelimited(this string source)
        {
            return string.Join('_', source.SplitCamelCase());
        }

        public static string ToCamelCase(this string source)
        {
            return string.Concat(source.Split("_").Select(s => s.Substring(0, 1).ToUpper() + (s.Length > 1 ? s[1..] : "")));
        }
    }
}
