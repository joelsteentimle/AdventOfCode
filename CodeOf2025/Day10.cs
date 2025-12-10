namespace AoC2025;

public class Day10(List<string> allData)
{
    private int[,] terrain;

    private record Config(bool[] lights, int[] switches, int[] cost);

    private Config[] configs = allData.Select(ReadConfigRow).ToArray();

    private static Config ReadConfigRow(string text)
    {
        var lights = text[1..].TakeWhile(c  => c!= ']')
            .Select(c => c == '#').ToArray();

        var switches = text.SkipWhile(c => c != ' ')
            .TakeWhile(c => c != '{')
            .ToString()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            [1..^1]
            .ToString()
            .Split(',',StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse).ToArray();

        var costs = text.SkipWhile(c => c != '{')
            .ToString()[1..^1]
            .Split(',',StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries )
            .Select(int.Parse).ToArray();
        return new(lights, switches, costs);
    }


    public int Part1()
    {
        return 10;
    }

    public int Part2()
    {
        return 10;
    }
}
