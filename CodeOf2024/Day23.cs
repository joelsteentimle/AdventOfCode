using AdventLibrary;

namespace AoC2024;

public class Day23
{

    private readonly Dictionary<string, HashSet<string>> connectionFromComputer = [];
    private readonly HashSet<string> allComputers = [];

    private readonly HashSet<(string, string, string)> CalculatingSets = [];

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
        foreach (var link in connectionFromComputer)
        {
            if (link.Key.StartsWith('t'))
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

        }

        return CalculatingSets.Count;
    }

    public string Part2()
    {
        HashSet<string> largestLan = [];

        foreach (var computer in allComputers)
        {
            var setStart = new HashSet<string>();
            setStart.Add(computer);
            setStart.UnionWith(connectionFromComputer[computer]);

             var longestForThis = GetLongest(setStart);

             if(longestForThis.Count > largestLan.Count)
                 largestLan = longestForThis;
        }
        return string.Join(',', largestLan.Order());
    }

    private HashSet<string> GetLongest(HashSet<string> setStart)
    {
        

    }
}

public static class DictionaryExtension
{
    public static void AddOrCreate(this Dictionary<string, HashSet<string>> dictionary, string key, string value)
    {
        if (dictionary.TryGetValue(key, out var collection))
        {
            collection.Add(value);
        }
        else
        {
            dictionary.Add(key, [value]);
        }
    }
}

