using System.Text.RegularExpressions;

namespace AoC2025;

public class Day03(List<string> input)
{
    private int[][] BatterPacks = input.Select(line => line.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();

    public long Part1() => Batteries(2);
    public long Part2() => Batteries(12);

    private long Batteries(int n)
    {
        var sum = 0L;

        foreach (var batteryPack in BatterPacks)
        {
            var lastIndex = -1;
            var packSum = 0L;

            for (var p = n - 1; p >= 0; p--)
            {
                var maxIndex = lastIndex + 1;
                var maxBatery = batteryPack[maxIndex];

                for (var i = lastIndex +1; i < batteryPack.Length - p; i++)
                {
                    if (batteryPack[i] > maxBatery)
                    {
                        maxIndex = i;
                        maxBatery = batteryPack[i];
                    }
                }
                lastIndex = maxIndex;
                packSum += batteryPack[maxIndex] * (long)Math.Pow(10, p);
            }

            sum += packSum;
        }

        return sum;
    }
}
