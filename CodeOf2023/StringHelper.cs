namespace AoC2023;

public static class StringHelper
{
    public static IList<string> SplitAndTrim(this string input, params char[] separator)
    {
        return input.Split(separator, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }
}