namespace AoC2023;

public class Day24(List<string> lines)
{
    public IList<string> TextGroups { get; } = lines.First().SplitAndTrim(',');
    public char[,] Map { get;  } = CalculateMap(lines);

    private static char[,] CalculateMap(List<string> list)
    {
        var initialMap = new char[list[0].Length, list.Count];

        for (var y = 0; y < list.Count; y++)
        for (var x = 0; x < list[y].Length; x++)
            initialMap[x, y] = list[y][x];

        return initialMap;
    }

}
