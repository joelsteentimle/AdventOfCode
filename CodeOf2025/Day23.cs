using AdventLibrary;

namespace AoC2025;

public class Day23
{
    private readonly Dictionary<string, HashSet<string>> connectionFromComputer = [];
    private readonly HashSet<string> allComputers = [];
    private readonly HashSet<(string, string, string)> CalculatingSets = [];
    private HashSet<string> LargestLan = [];

    private static (string frst, string snd, string trd) GetLink(params string[] computers)
    {
        var sorted = computers.Order().ToArray();
        return (sorted[0], sorted[1], sorted[2]);
    }

    public Day23(List<string> allData)
    {
        foreach (var connectionString in allData)
        {
            var computers = connectionString.Split("-", StringSplitOptions.RemoveEmptyEntries);
            var comp1 = computers[0];
            var comp2 = computers[1];
            allComputers.Add(comp1);
            allComputers.Add(comp2);
            connectionFromComputer.AddOrCreate(comp1, comp2);
            connectionFromComputer.AddOrCreate(comp2, comp1);
        }
    }

    public long Part1()
    {
        foreach (var link in connectionFromComputer.Where(l => l.Key.StartsWith('t')))
        {
            var connectedTo = link.Value.ToArray();
            for (var i = 0; i < connectedTo.Length; i++)
            {
                var fst = connectedTo[i];
                for (var j = i + 1; j < connectedTo.Length; j++)
                {
                    var snd = connectedTo[j];
                    if (connectionFromComputer[fst]
                        .Contains(snd))
                        CalculatingSets.Add(GetLink(link.Key, fst, snd));
                }
            }

        }

        return CalculatingSets.Count;
    }

    public string Part2()
    {
        var comupterList = allComputers.ToList();
        foreach (var computer in comupterList)
        {
            var setStart = new HashSet<string> { computer };
             PopulateLongest(setStart, connectionFromComputer[computer] );
        }

        return string.Join(',', LargestLan.Order());
    }

    private void PopulateLongest(HashSet<string> nowSet, HashSet<string> candidates)
    {
        if (candidates.Count == 0)
        {
            if (nowSet.Count > LargestLan.Count)
                LargestLan = nowSet;
            return;
        }

        if (nowSet.Count + candidates.Count <= LargestLan.Count)
            return;

        var candidateList = candidates.ToList();

        foreach (var candidate in candidateList )
        {
            var extCand = new HashSet<string>(nowSet);
            extCand.Add(candidate);

            var limitedCandidates = new HashSet<string>(candidates);
            limitedCandidates.IntersectWith(connectionFromComputer[candidate]);
            PopulateLongest( extCand , limitedCandidates );
        }
    }
}
