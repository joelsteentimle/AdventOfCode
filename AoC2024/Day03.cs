using System.Text.RegularExpressions;

namespace AoC2024;

public class Day03
{
    private readonly string theLine;

    public Day03(List<string> input)
    {
         theLine = input.First();
    }

    public int GetProductSum()
    {

        var sum = 0;

    string pattern = @"mul\(\d{1,3},\d{1,3}\)";

        Regex regex = new Regex(pattern);
        MatchCollection matches = regex.Matches(theLine);

        foreach (Match match in matches)
        {
var numbers =
            match.Value[4..^1].Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList()
                ;

sum += numbers[0] * numbers[1];
        }

        return sum;
    }

}
