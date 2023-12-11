namespace AoC2023;

public class Day09
{
    public Day09(IList<string> lines)
    {
        OriginalSeries = lines.Select(l => l.SplitAndTrim(' ').Select(n => Convert.ToInt32(n)).ToList()).ToList();
    }

    public ( long left, long right) SumNexts() => OriginalSeries.Select(GetNextValue)
        .Aggregate((0L, 0L), (v1, v2) => (v1.Item1 + v2.left, v1.Item2 + v2.right));

    public (long left, long right ) GetNextValue(List<int> values)
    {
        // if (values.Count == 0 || values.All(v => v == 0))
        //     return (0,0);

        Stack<List<int>> valueReduce = [];

        var previus = values;
        var current = new List<int>();

        do
        {
            current = [];
            valueReduce.Push(previus);
            for (var i = 0; i + 1 < previus.Count; i++)
            {
                current.Add(previus[i + 1] - previus[i]);
            }

            previus = current;
        } while (current.Any(c => c != 0));

        long rightNumber = 0;
        long leftNumber = 0;
        while (valueReduce.Any())
        {
            var list = valueReduce.Pop();
            rightNumber += list.Last();
            leftNumber = list.First() - leftNumber;
        }

        return (leftNumber, rightNumber);
    }

    public List<List<int>> OriginalSeries { get; set; }
}