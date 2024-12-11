namespace AoC2024;

public class Day11
{
    private List<long> numbers = [];
    private Dictionary<long, (long, long?)> stepping = [];

    public Day11(List<string> allData)
    {
        numbers = allData[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();
    }

    public int Part1(int nrBlinkings)
    {
        for (int i = 0; i <nrBlinkings; i++)
            Blink();

        return numbers.Count;
    }

    private Dictionary<(long number, long nrBlinkings), long> MemoryOfNumbers = [];

    public long Part2(int nrBlinkings) => NumberForListWithSteps(numbers, nrBlinkings);

    private long NumberForListWithSteps(List<long> numbers, int nrBlinkingsLeft)
    {
        if (nrBlinkingsLeft == 0)
            return numbers.Count;

        numbers.Sort();

        var sum = 0L;
        foreach (var number in numbers)
        {
            if (MemoryOfNumbers.TryGetValue((number, nrBlinkingsLeft), out var remembered))
                sum += remembered;
            else
            {
                var calculated = SumForNumberWithBlinkings(number, nrBlinkingsLeft);
                MemoryOfNumbers[(number, nrBlinkingsLeft)] = calculated;
                sum += calculated;
            }
        }

        // sum += NumberForListWithSteps(unknownNumbers, nrBlinkingsLeft -1);


        return sum;
    }

    private long SumForNumberWithBlinkings(long number, int nrBlinkings)
    {
        var newNumbers = new List<long>();
        var (newNumber, maybeeNewNumber) = NewNumberStep(number);
        newNumbers.Add(newNumber);
        if (maybeeNewNumber.HasValue)
            newNumbers.Add(maybeeNewNumber.Value);

        return NumberForListWithSteps(newNumbers, nrBlinkings-1);
    }


    private void Blink()
    {
        var newNumbers = new List<long>();

        foreach (var number in numbers)
        {
            // var (i,j) = NumberStep(number);
            var (i,j) = NewNumberStep(number);

            newNumbers.Add(i);
            if(j is {} jl)
                newNumbers.Add(jl);
        }
        numbers = newNumbers;
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
        // List<long> numberResult = [];

        if (number == 0)
            return (1, null);
        // numberResult.Add(1);
        else if (numberLength % 2 == 0)
        {
            return
                (long.Parse(numberString.Substring(0, numberLength / 2)),
                    long.Parse(numberString.Substring(numberLength / 2)));

            // numberResult.Add(long.Parse(numberString.Substring(0, numberLength / 2)));
            // numberResult.Add(long.Parse(numberString.Substring(numberLength / 2)));
        }
        else
        {
            return (number * 2024, null);
            // numberResult.Add(number * 2024);
        }

        // return numberResult;
    }

    // public int Part2()
    // {
    //     var sum = 0;
    //
    //     return sum;
    // }

}
