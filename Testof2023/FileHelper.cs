namespace TestOf2023;

public static class FileHelper
{
    public static IEnumerable<string> ReadFileAsLines(this string filePath) =>
        File.ReadLines(filePath);

    public static List<string> ReadTestLines(this string day, string suffix = "") =>
        @$"DataFiles\{day}\Test{suffix}.txt".ReadFileAsLines().ToList();

    public static List<string> ReadRealLines(this string day) =>
        @$"DataFiles\{day}\Real.txt".ReadFileAsLines().ToList();
}
