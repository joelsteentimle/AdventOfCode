using System.Collections;

namespace AoC2025;

public class Day10(List<string> allData)
{

    private record Config(int targetLights, int numberOfLights, int[] bitFlips, int[][]positionFlips, int[] targetJolt);

    private Config[] configs = allData.Select(ReadConfigRow).ToArray();

    private static Config ReadConfigRow(string text)
    {
        var lights = text[1..].TakeWhile(c => c != ']').ToArray();
        var targetLight = 0;
        List<int> bitFlipps = [];
        List<int[]> posFlips = [];

        for (var i = 0; i < lights.Length; i++)
            if (lights[i] == '#')
                targetLight |= ((int)Math.Pow(2, i));

        var switchStrings =
            new string(text.SkipWhile(c => c != ' ')
            .TakeWhile(c => c != '{')
            .ToArray())
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);



        for(var i =0; i< switchStrings.Length; i++)
        {
            var bitFlip = 0;
            var switchPositions = switchStrings[i][1..^1]
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse).ToArray();

            foreach (var switchPosition in switchPositions)
            {
                bitFlip |= ((int)Math.Pow(2, switchPosition));
            }

            posFlips.Add(switchPositions);
            bitFlipps.Add(bitFlip);
        }

        var costs =
            new string(
                    text.SkipWhile(c => c != '{')
                        .TakeWhile(c => c != '}')
                        .ToArray()
                )[1..]
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse).ToArray();

        return new(targetLight, lights.Length, bitFlipps.ToArray(), posFlips.ToArray(), costs);
    }


    public int Part1()
    {
        var totalCost = 0;
        foreach (var config in configs)
        {
            totalCost += CostFlipToTarget(config);
        }

        return totalCost;
    }

    private int CostFlipToTarget(Config config)
    {
        var costDict =  new Dictionary<int, HashSet<int>>();
        var visitedPatterns = new HashSet<int>();
        costDict[0] = [0 ];

        for(var cost = 0; cost < 10000; cost++)
        {
            if (costDict.TryGetValue(cost, out var set))
            {
                if (set.Contains(config.targetLights))
                    return cost;

                foreach (var pattern in set)
                {
                    if(!costDict.TryGetValue(cost+1, out var nextCosts))
                    {
                        nextCosts = [];
                        costDict[cost+1] = nextCosts;
                    }

                    if (!visitedPatterns.Contains(pattern)  )
                    {
                        visitedPatterns.Add(pattern);

                        foreach (var newPattern in GetMutations(pattern, config))
                        {
                            nextCosts.Add(newPattern);
                        }
                    }
                }
            }
        }

        return -1;
    }


    private IEnumerable<int> GetMutations(int pattern, Config config)
    {
        foreach (var flip in config.bitFlips)
            yield return pattern ^ flip;
    }

    public int Part2()
    {
        var totalCost = 0;
        foreach (var config in configs)
        {
            totalCost += CostAddToTarget(config);
        }

        return totalCost;
    }

    private class IntArrComp : IEqualityComparer<int[]>
    {
        public bool Equals(int[]? x, int[]? y)
        {
            if (x is null && y is null)
                return true;
            if (x is null || y is null)
                return false;
            if (x.Length != y.Length)
                return false;
            return Enumerable.SequenceEqual(x, y);
        }

        public int GetHashCode(int[] obj)
        {
            if (obj is null)
                return 0;
            return obj.Aggregate(17, (a, b) => a ^ b);
        }
    }


    private int CostAddToTarget(Config config)
    {
        var arrComp = new IntArrComp();
        var costDict =  new Dictionary<int, HashSet<int[]>>();
        var visitedPatterns = new HashSet<int[]>(comparer: arrComp);

        var initialPatterns = new HashSet<int[]>(comparer: arrComp);
        initialPatterns.Add(new int[config.numberOfLights]);
        costDict[0] = initialPatterns;

        for(var cost = 0; cost < 10000; cost++)
        {
            if (costDict.TryGetValue(cost, out var set))
            {
                if (set.Contains(config.targetJolt))
                    return cost;

                HashSet<int[]> nextCosts = new(comparer: arrComp);
                costDict[cost + 1] = nextCosts;

                foreach (var pattern in set)
                {
                    if (!visitedPatterns.Contains(pattern))
                    {
                        visitedPatterns.Add(pattern);

                        foreach (var newPattern in GetMutationsCosts(pattern, config))
                        {

                            var noOverCost = true;
                            for (var i = 0; i < config.numberOfLights && noOverCost; i++)
                                if(newPattern[i] > config.targetJolt[i])
                                    noOverCost = false;
                            if(noOverCost)
                                nextCosts.Add(newPattern);
                        }
                    }
                }
            }
        }

        return -1;
    }


    private IEnumerable<int[]> GetMutationsCosts(int[] pattern, Config config)
    {
        foreach (var positions in config.positionFlips)
        {
            var newPattern = new int[pattern.Length];

            for (int i = 0; i< newPattern.Length ; i++)
            {
                newPattern[i] = pattern[i] + (positions.Contains(i) ? 1 : 0);
            }
            yield return newPattern;
        }
    }
}
