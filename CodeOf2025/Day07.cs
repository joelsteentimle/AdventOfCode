namespace AoC2025;

public class Day07
{
    private readonly char[,] Field;
    private readonly long[,] WaysHere;

    public Day07(List<string> input)
    {
        Field = new char[input.Count, input[0].Length];
        WaysHere = new long[input.Count, input[0].Length];
        for (var y = 0; y < input.Count; y++)
        for (var x = 0; x < input[y].Length; x++)
        {
            WaysHere[y, x] = 0;
            Field[y, x] = input[y][x];
            if (Field[y, x] == 'S')
            {
                Field[y, x] = '|';
                WaysHere[y, x]++;
            }
        }

        for (var y = 0; y < Field.GetLength(0) - 1; y++)
        for (var x = 0; x < Field.GetLength(1); x++)
            if (Field[y, x] == '|')
            {
                if (Field[y + 1, x] == '^')
                {
                    Field[y + 1, x - 1] = '|';
                    WaysHere[y + 1, x - 1] += WaysHere[y, x];

                    Field[y + 1, x + 1] = '|';
                    WaysHere[y + 1, x + 1] += WaysHere[y, x];
                    splits++;
                }
                else
                {
                    Field[y + 1, x] = '|';
                    WaysHere[y + 1, x] += WaysHere[y, x];
                }
            }
    }


    private readonly int splits;

    public long Part1() => splits;


    public long Part2()
    {
        long totalWays = 0;
        for(var x=0; x< Field.GetLength(1); x++)
        {
           totalWays+=WaysHere[Field.GetLength(0)-1,x];
        }

        return totalWays;
    }
}
