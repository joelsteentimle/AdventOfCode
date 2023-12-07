namespace TestOf2023;

public static class FileHelper
{
    public static IEnumerable<string> ReadFileAsLines(this string filePath) =>
        File.ReadLines(filePath);

    public static IList<string> ReadTestLines(this string day) =>
        @$"DataFiles\{day}\Test.txt".ReadFileAsLines().ToList();

    public static IList<string> ReadRealLines(this string day) =>
        @$"DataFiles\{day}\Real.txt".ReadFileAsLines().ToList();
}
