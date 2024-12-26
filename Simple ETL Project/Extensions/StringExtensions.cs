namespace Simple_ETL_Project.Extensions;

using System.Text.RegularExpressions;

public static class StringExtensions
{
    public static string ToSnakeCase(this string input)
    {
        return string.IsNullOrEmpty(input) ? input : Regex.Match(input, "^_+")?.ToString() + 
                                                     Regex.Replace(input, "([a-z0-9])([A-Z])", "$1_$2").ToLower();
    }
}