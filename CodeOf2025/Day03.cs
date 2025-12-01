using System.Text.RegularExpressions;

namespace AoC2025;

public class Day03(List<string> input)
{
    private readonly string theLine = input.First();

    public int GetProductSum()
    {
        var sum = 0;
        var pattern = @"mul\(\d{1,3},\d{1,3}\)";
        var regex = new Regex(pattern);
        var matches = regex.Matches(theLine);

        foreach (Match match in matches)
        {
            var numbers = NumbersForMatch(match);

            sum += numbers[0] * numbers[1];
        }

        return sum;
    }

    public int GetProducSumWhenDo()
    {
        var sum = 0;
        var pattern = @"(mul\(\d{1,3},\d{1,3}\))|don't\(\)|do\(\)";
        var regex = new Regex(pattern);
        var matches = regex.Matches(theLine);
        var shuoldDo = true;

        foreach (Match match in matches)
            if (match.Value == "don't()")
                shuoldDo = false;
            else if (match.Value == "do()")
                shuoldDo = true;
            else if (shuoldDo)
            {
                var numbers = NumbersForMatch(match);
                sum += numbers[0] * numbers[1];
            }

        return sum;
    }

    private static List<int> NumbersForMatch(Match match) =>
        match.Value[4..^1].Split(',', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToList();
}
