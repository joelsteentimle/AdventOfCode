namespace AoC2024;

public class Day13
{
    private char[,] Field;
    private int MaxY;
    private readonly int MaxX;


    public Day13(List<string> allData)
    {
        MaxY = allData.Count;
        MaxX = allData[0].Length;
        Field = new char[MaxY, MaxX];

        for (int y = 0; y < MaxY; y++)
        {
            for (int x = 0; x < MaxX; x++)
            {
                Field[y, x] = allData[y][x];
            }
        }
    }

    public long Part1()
    {
        var sum = 0L;

        return sum;
    }


    public long Part2()
    {
        var sum = 0L;


        return sum;
    }
}
