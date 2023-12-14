namespace AoC2023;

public class Day15(List<string> lines)
{
    public char[,] Map { get; set; } = CalculateMap(lines);

    private static char[,] CalculateMap(List<string> list)
    {
        var initialMap = new char[list[0].Length, list.Count];
        for (var y = 0; y < list.Count; y++)
        for (var x = 0; x < list[y].Length; x++)
            initialMap[x, y] = list[y][x];
        return initialMap;
    }
}
