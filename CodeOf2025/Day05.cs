namespace AoC2025;

public class Day05
{
    private record Range(long First, long Second);

    private readonly List<Range> FreshRanges = new();
    private readonly List<long> Ingridience = [];

    public Day05(List<string> input)
    {
        var freshRanges = input.TakeWhile(s => !string.IsNullOrWhiteSpace(s)).ToList();
        Ingridience = input.Skip(freshRanges.Count + 1)
            .Select(long.Parse)
            .ToList();

        foreach (var range in freshRanges)
        {
            var fs  = range.Split('-', StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();
            FreshRanges.Add(new(fs[0], fs[1]));
        }
    }

    public long Part1()
        => Ingridience.Where(i => FreshRanges.Any(r => i >= r.First && i <= r.Second)).Count();

    public long Part2()
    {
        var jointRanges = FreshRanges.ToList();

        while (JoinAny(jointRanges)) ;

        return jointRanges.Sum(jr => jr.Second - jr.First + 1);
    }

    private bool JoinAny(List<Range> jointRanges)
    {
        for (var i = 0; i< jointRanges.Count; i++)
        for (var j = i+1; j < jointRanges.Count; j++)
        {
            var ir = jointRanges[i];
            var jr = jointRanges[j];
            if (Overlaps(ir, jr))
            {
                jointRanges.RemoveAt(j);
                jointRanges.RemoveAt(i);
                jointRanges.Add(JoinRanges(ir,jr ));
                return true;
            }
        }

        return false;
    }

    private static bool Overlaps(Range f, Range s) =>
        (f.Second >= s.First && f.Second <= s.Second) ||
        (f.First >= s.First && f.First <= s.Second) ||
        (s.Second >= f.First && s.Second <= f.Second) ||
        (s.First >= f.First && s.First <= f.Second);

    private static Range JoinRanges(Range  f, Range s) => new (Math.Min(f.First, s.First), Math.Max(f.Second,s.Second));
}
