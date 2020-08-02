using System.Text.RegularExpressions;

namespace RaidGroupFinder.Helper
{
    public class RegexHelper
    {
        private static readonly Regex sWhitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(string input)
        {
            return sWhitespace.Replace(input, "");
        }
    }
}
