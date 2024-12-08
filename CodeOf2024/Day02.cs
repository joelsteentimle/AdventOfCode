namespace AoC2024;

public class Day02
{
    public Day02(List<string> lines, bool allowOne = false)
    {
        foreach (var line in lines)
        {
            var stairs = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            var isSafe = AreStrairsSafe(stairs);

            if (!isSafe && allowOne) isSafe = AreAlteredStraisSafe(stairs);

            if (isSafe)
                Safe.Add(stairs);
            else
                Unsafe.Add(stairs);
        }

        bool AreAlteredStraisSafe(List<int> stairs)
        {
            for (var i = 0; i < stairs.Count; i++)
            {
                var stairsMinusOne = stairs[..i];
                stairsMinusOne.AddRange(stairs[(i + 1)..]);
                if (AreStrairsSafe(stairsMinusOne))
                    return true;
            }

            return false;
        }

        bool AreStrairsSafe(List<int> stairs)
        {
            var previous = stairs[0];
            var sign = Math.Sign(stairs[1] - stairs[0]);

            for (var index = 1; index < stairs.Count; index++)
            {
                var stair = stairs[index];

                var abs = Math.Abs(stair - previous);
                var localSign = Math.Sign(stair - previous);

                if (abs > 3 || abs < 1 || localSign != sign) return false;
                previous = stair;
            }

            return true;
        }
    }

    public List<List<int>> Safe { get; } = new();
    public List<List<int>> Unsafe { get; } = new();
}
