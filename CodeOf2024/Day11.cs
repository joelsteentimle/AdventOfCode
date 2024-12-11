namespace AoC2024;

public class Day11
{
    private readonly Dictionary<(long number, long nrBlinkings), long> MemoryOfNumbers = [];
    private readonly List<long> numbers = [];
    private readonly Dictionary<long, (long, long?)> stepping = [];

    public Day11(List<string> allData) =>
        numbers = allData[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();

    public long Part2(int nrBlinkings) => NumberForListWithSteps(numbers, nrBlinkings);

    private long NumberForListWithSteps(List<long> numbers, int nrBlinkingsLeft)
    {
        if (nrBlinkingsLeft == 0)
            return numbers.Count;

        numbers.Sort();

        var sum = 0L;
        foreach (var number in numbers)
            if (MemoryOfNumbers.TryGetValue((number, nrBlinkingsLeft), out var remembered))
                sum += remembered;
            else
            {
                var calculated = SumForNumberWithBlinkings(number, nrBlinkingsLeft);
                MemoryOfNumbers[(number, nrBlinkingsLeft)] = calculated;
                sum += calculated;
            }

        return sum;
    }

    private long SumForNumberWithBlinkings(long number, int nrBlinkings)
    {
        var newNumbers = new List<long>();
        var (newNumber, maybeeNewNumber) = NewNumberStep(number);
        newNumbers.Add(newNumber);
        if (maybeeNewNumber.HasValue)
            newNumbers.Add(maybeeNewNumber.Value);

        return NumberForListWithSteps(newNumbers, nrBlinkings - 1);
    }

    private (long, long?) NewNumberStep(long number)
    {
        if (stepping.TryGetValue(number, out var result))
            return result;

        return NumberStep(number);
    }

    private static (long, long?) NumberStep(long number)
    {
        var numberString = number.ToString();
        var numberLength = numberString.Length;

        if (number == 0)
            return (1, null);
        if (numberLength % 2 == 0)
            return
                (long.Parse(numberString.Substring(0, numberLength / 2)),
                    long.Parse(numberString.Substring(numberLength / 2)));

        return (number * 2024, null);
    }
}
