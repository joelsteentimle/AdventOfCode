namespace AoC2025;

public class Day02(List<string> lines)
{
    private readonly string[] pairs = lines[0].Split(',');

    public long Part1 => Iterate(D1Test);
    public long Part2 => Iterate(D2Test);

    private long Iterate(Func<long, bool> shouldCount)
    {
        var PairSum = 0L;
        foreach (var pair in pairs)
        {
            var f = long.Parse(pair.Split('-')[0]);
            var l = long.Parse(pair.Split('-')[1]);

            for (var i = f; i <= l; i++)
                if (shouldCount(i))
                    PairSum += i;
        }
        return PairSum;
    }

    private static bool D1Test(long i)
    {
        var s = i.ToString();
        if (s.Length % 2 != 0)
        {
            return false;
        }
        var h = s.Length / 2;

        return s[..h] == s[h..];
    }

    private static bool D2Test(long i)
    {
        var s = i.ToString();

        for (var l = 1; l <= s.Length / 2; l++)
        {
            if(s.Length % l != 0)
                continue;

            var h = s.Chunk(l).ToArray();

            if (h.Length >1
                && h.All(chunk =>
                    chunk.Select((c,i) => c == h[0][i]).All(b => b)))
                return true;
        }

        return false;
    }
}
