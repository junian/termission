using System;
namespace Juniansoft.Termission.Core.Extensions
{
    public static class StringExtension
    {
        public static string Truncate(this string value, int maxLength, string ending = "")
        {
            if (string.IsNullOrEmpty(value)) { return value; }

            if (value.Length > maxLength) { return $"{value.Substring(0, maxLength)}{ending}"; }

            return value;
        }
    }
}
