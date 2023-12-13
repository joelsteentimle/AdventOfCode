namespace AoC2023;

public static class StringHelper
{
    public static IList<string> SplitAndTrim(this string input, params char[] separator) => input.Split(separator,
        StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

    public static long ToInt64(this string input) =>
        Convert.ToInt64(input, NumberFormatInfo.InvariantInfo);
    public static int ToInt32(this string input) =>
        Convert.ToInt32(input, NumberFormatInfo.InvariantInfo);
}
