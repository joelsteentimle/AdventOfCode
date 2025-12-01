namespace AoC2025;

public class Day07
{
    private readonly List<List<long>> numberLines = [];

    public Day07(List<string> input)
    {
        foreach (var equation in input)
        {
            var numbers = equation
                .Replace(":", "")
                .Split(' ')
                .Select(long.Parse)
                .ToList();

            numberLines.Add(numbers);
        }
    }

    public long SumThatCan(bool withConcat = false)
    {
        var sum = 0L;
        foreach (var numbers in numberLines)
        {
            var targetSum = numbers[0];
            var parts = numbers[2..];

            HashSet<long> currentNums = [numbers[1]];

            foreach (var num in parts)
            {
                var currentNumList = currentNums.ToList();
                currentNums = [];
                foreach (var prev in currentNumList)
                {
                    var adds = prev + num;
                    if (adds <= targetSum)
                        currentNums.Add(adds);

                    var muls = prev * num;
                    if (muls <= targetSum)
                        currentNums.Add(muls);

                    if (withConcat &&
                        long.TryParse($"{prev}{num}", out var concat)
                        && concat <= targetSum)
                        currentNums.Add(concat);
                }
            }

            if (currentNums.Contains(targetSum))
                sum += targetSum;
        }

        return sum;
    }
}
