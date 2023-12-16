using Open.Collections.NonGeneric;

namespace AoC2023;

public class Day15(List<string> lines)
{
    public IList<string> Groups { get; } = lines.First().SplitAndTrim(',');

    private static Dictionary<int, List<Lens>> ParseBoxes(string row)
    {
        Dictionary<int, List<Lens>> result = [];
        var boxStrings = row.SplitAndTrim(',').ToList();

        foreach (var boxString in boxStrings)
        {
            var isAdd = boxString.Contains('=');
            var nameAndNumber = boxString.SplitAndTrim('=', '-');
            var key = nameAndNumber[0];
            var hash = Hash(key);
            if (isAdd)
            {
                var number = nameAndNumber[1].ToInt32();
                var lenses = result.GetOrAdd(hash, new List<Lens>());
                var existingLens = lenses.Find(l => l.Key == key);
                if (existingLens is null)
                    lenses.Add(new Lens(key, number));
                else
                    existingLens.Number = number;
            }
            else
            {
                if (result.TryGetValue(hash, out var box))
                {
                    var existingLens = box.Find(l => l.Key == key);
                    if (existingLens is not null)
                        box.Remove(existingLens);
                }
            }
        }

        return result;
    }

    private Dictionary<int, List<Lens>> Boxes { get; } = ParseBoxes(lines.First());

    private sealed record Lens(string Key, int Number)
    {
        public int Number { get; set; } = Number;
    }

    public static int Hash(string message) => message.Aggregate(0, (r, c) => (r + Convert.ToInt32(c)) * 17 % 256);

    public int HashSum => Groups.Select(Hash).Sum();

    public int LensSum()
    {
        var sum = 0;

        foreach (var key in Boxes.Keys)
        {
            var boxSum = 0;
            var lenses = Boxes[key];
            for (var i = 0; i < lenses.Count; i++)
                boxSum += (i + 1) * lenses[i].Number;

            sum += boxSum * (key + 1);

        }

        return sum;
    }

}
