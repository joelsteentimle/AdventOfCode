namespace SupportCode;

public static class FileHelper
{
    public static IEnumerable<string> ReadFileAsLines(this string filePath) =>
        File.ReadLines(filePath);

    public static List<string> ReadTestLines(this string day, string suffix = "") =>
        Path.Combine( "DataFiles",$"{day}",$"Test{suffix}.txt").ReadFileAsLines().ToList();
       // @$"DataFiles\{day}\Test{suffix}.txt".ReadFileAsLines().ToList();

    public static List<string> ReadRealLines(this string day, string suffix = "") =>
        Path.Combine( "DataFiles",$"{day}",$"Real{suffix}.txt").ReadFileAsLines().ToList();
}
