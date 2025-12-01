namespace AoC2025;

public class Day19
{
    private readonly List<string> availablePatterns;
    private readonly List<string> desiredPatterns;

    public Day19(List<string> allData)
    {
        var patternLine = allData[0];
        availablePatterns = patternLine
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).ToList();

        desiredPatterns = allData[2..];
    }

    public long Part1() => desiredPatterns.Count(CanDesirePattern);

    public long Part2() => desiredPatterns.Sum(PossibleArragements);

    private long PossibleArragements(string currentPattern)
    {
        var possibleForPosition = new long[currentPattern.Length+1];
        possibleForPosition[0] = 1;

        for (var i = 0; i < possibleForPosition.Length; i++)
        {
            if (possibleForPosition[i] > 0)
            {
                foreach (var aPattern in availablePatterns)
                {
                    if (aPattern.Length + i <= currentPattern.Length)
                    {
                        if (currentPattern[i..].StartsWith(aPattern))
                        {
                            possibleForPosition[i + aPattern.Length] += possibleForPosition[i];
                        }
                    }
                }
            }
        }

        return possibleForPosition[currentPattern.Length];
    }

    private bool CanDesirePattern(string currentPatternTest)
    {
        SortedSet<int> reachablePositions = [0];

        while (reachablePositions.Count > 0)
        {
            var minLength = reachablePositions.Min;

            foreach (var aPattern in availablePatterns)
            {
                if (aPattern.Length <= currentPatternTest.Length - minLength)
                {
                    if (aPattern == currentPatternTest[minLength..])
                    {
                        return true;
                    }

                    if (currentPatternTest[minLength..].StartsWith(aPattern))
                    {
                        reachablePositions.Add(minLength + aPattern.Length);
                    }
                }
            }

            reachablePositions.Remove(minLength);
        }

        return false;
    }
}
