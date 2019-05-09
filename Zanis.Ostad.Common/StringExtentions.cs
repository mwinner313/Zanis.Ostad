using System.Text.RegularExpressions;

namespace Zanis.Ostad.Common
{
    public static class StringExtentions
    {
        public static string ToUrlSegment(this string str)
        {
            str = Regex.Replace(str.ToLower(), @"[^آ-یA-Za-z0-9\s-]", "");
            str = Regex.Replace(str, @"\s+", " ").Trim();
            str = Regex.Replace(str, @"\s", "-");  
            str = Regex.Replace(str, @"-+", "-");
            return str;
        }
    }
}