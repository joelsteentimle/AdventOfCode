namespace AoC2024;

public class Day09
{
    private readonly char[,] Field;
    private readonly int MaxX;
    private readonly int MaxY;

    public Day09(List<string> input)
    {
        MaxY = input.Count;
        MaxX = input[0].Length;
        Field = new char[MaxY, MaxX];

        for (var y = 0; y < MaxY; y++)
        for (var x = 0; x < MaxX; x++)
            Field[y, x] = input[y][x];
    }

    public int Get1() => 0;
    public int Get2() => 0;
}
