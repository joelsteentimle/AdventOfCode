using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace AoC2025;

public class Day06(List<string> input)
{
    public long Part1()
    {
        string[] signs;
        long[,] numbers;

        signs = input[^1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        numbers = new long[input.Count - 1, signs.Length];

        for (var y = 0; y < input.Count - 1; y++)
        {
            var rowNumbers =
                input[y].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

            for (var x = 0; x < rowNumbers.Length; x++)
            {
                numbers[y, x] = long.Parse(rowNumbers[x]);
            }
        }

        var allSum = 0L;
        for (var x = 0; x < signs.Length; x++)
        {
            var sign = signs[x];
            var colValue = numbers[0, x];

            for (var y = 1; y < numbers.GetLength(0); y++)
            {
                if (sign == "+")
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
        var signs = input[^1].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
        //
        // var flipped = new List<StringBuilder>(input.Count);
        //
        // for (var i = 0; i < flipped.Count; i++)
        //     flipped[i] = new StringBuilder();

        var signindex = 0;

        var currentValue =
            signs[0] == "*"
                ? 1L
                : 0L;

        var totalValue = 0L;

        for (var y = 0; y < input[0].Length; y++)
        {
            var numBuldir = new StringBuilder();
            for (var x = 0; x < input.Count-1; x++)
                numBuldir.Append(input[x][y]);

            var rowString = numBuldir.ToString().Trim();
            if (string.IsNullOrEmpty(rowString))
            {
                totalValue += currentValue;
                signindex++;
                if (signs[signindex] == "*")
                    currentValue = 1;
                else
                    currentValue = 0;
            }
            else
            {
                if (signs[signindex] == "+")
                    currentValue += int.Parse(rowString.Trim());
                else
                    currentValue *= int.Parse(rowString.Trim());
            }
        }

        return totalValue + currentValue;
    }
}
