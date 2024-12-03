namespace AoC2024;

public class Day01
{
    private List<int> first;
    private List<int> second;
    private Dictionary<int, int> occurences = new Dictionary<int, int>();

    public Day01(List<string> data)
    {
        first = new List<int>(data.Count);
        second = new List<int>(data.Count);

        foreach (var line in data)
        {
            var words = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            var firstNumber = int.Parse(words[0]);
            var secondNumber = int.Parse(words[1]);


            first.Add(firstNumber);
            second.Add(secondNumber);

            if (occurences.TryGetValue(secondNumber, out var previousOccurences))
            {
                occurences[secondNumber] = previousOccurences + 1;
            }
            else
            {
                occurences[secondNumber] = 1;
            }

        }

        first.Sort();
        second.Sort();


    }

    public int Part1()
    {
        var sum = 0;
        for (int i = 0; i < first.Count; i++)
        {
            sum += Math.Abs(first[i] - second[i]);
        }
        return sum;
    }

    public int Part2()
    {
        var sum = 0;
        foreach (var number in first)
        {
            if (occurences.TryGetValue(number, out var secondOccurences))
            {
                sum += number * secondOccurences;
            }
        }
        return sum;
    }
}
