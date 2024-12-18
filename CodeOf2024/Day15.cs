namespace AoC2024;

public class Day15
{
    private enum FindEntry
    {
        floor,
        stone,
    }
    private char[,] Field;
    private int MaxY;
    private readonly int MaxX;

    public Day15(List<string> allData, (int y, int x)? inputFieldSize = null)
    {
        var fieldSize = inputFieldSize ?? (103, 101);
        MaxY = fieldSize.y;
        MaxX = fieldSize.x;
    }

    public long Part1(int seconds)
    {
         var sum = 0L;


        return sum;
    }



    public long Part2()
    {
        var sum = 0L;


        return -100;
    }

}
