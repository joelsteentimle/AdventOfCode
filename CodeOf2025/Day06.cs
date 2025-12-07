using System.Diagnostics.CodeAnalysis;

namespace AoC2025;

public class Day06
{
    private readonly string[] signs;
    private readonly long[,] numbers;

    public Day06(List<string> input)
    {

        signs = input[^1].Split(' ',
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            ;
        numbers = new long[input.Count-1, signs.Length];

        for (var y = 0; y < input.Count-1; y++)
        {
            var rowNumbers =
                input[y].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            for (var x = 0; x < rowNumbers.Length; x++)
            {
                numbers[y, x] = long.Parse(rowNumbers[x]);
            }

        }
    }

    public long Part1()
    {
        var allSum = 0L;

        for (var x = 0; x < signs.Length; x++)
        {
            var sign = signs[x];
            var colValue = numbers[0, x];

            for (var y = 1; y < numbers.GetLength(0); y++)
            {
                if(sign == "+")
                    colValue += numbers[y, x];
                else
                    colValue *= numbers[y, x];

            }

            allSum += colValue;
        }

        return allSum;
    }

    public long Part2()
    {
        return 0;
    }
}
