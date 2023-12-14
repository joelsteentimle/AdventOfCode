namespace AoC2023;

public class Day09(IList<string> lines)
{
    public (long left, long right) SumNext() => originalSeries.Select(GetNextValue)
        .Aggregate((0L, 0L), (v1, v2) => (v1.Item1 + v2.left, v1.Item2 + v2.right));

    public (long left, long right) GetNextValue(List<int> values)
    {
        Stack<List<int>> valueReduce = [];

        var previous = values;
        List<int> current;

        do
        {
            current = [];
            valueReduce.Push(previous);
            for (var i = 0; i + 1 < previous.Count; i++)
                current.Add(previous[i + 1] - previous[i]);

            previous = current;
        } while (current.Any(c => c != 0));

        long rightNumber = 0;
        long leftNumber = 0;
        while (valueReduce.Count != 0)
        {
            var list = valueReduce.Pop();
            rightNumber += list.Last();
            leftNumber = list.First() - leftNumber;
        }

        return (leftNumber, rightNumber);
    }

    private readonly List<List<int>> originalSeries =
        lines.Select(l => l.SplitAndTrim(' ').Select(n => Convert.ToInt32(n, NumberFormatInfo.InvariantInfo)).ToList()).ToList();
}
