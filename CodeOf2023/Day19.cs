namespace AoC2023;

public class Day19(List<string> lines)
{
    private Dictionary<string, Sorter> Sortes = GenerateSorter(lines);

    private static Dictionary<string, Sorter> GenerateSorter(List<string> lines)
    {
        var sortLookup = new Dictionary<string, Sorter>();
        var sorterLines = lines.TakeWhile(l => !string.IsNullOrWhiteSpace(l));
        foreach (var so in sorterLines)
        {
            var sorter = new Sorter(so);
            sortLookup[sorter.Name] = sorter;
        }

        return sortLookup;
    }

    private List<Part> Parts = GenerateParts(lines);

    private static List<Part> GenerateParts(List<string> lines)
    {
        var parts = new List<Part>();
        var partStartsAt = lines.IndexOf("") + 1;

        for (var i = partStartsAt; i < lines.Count; i++)
            parts.Add(ParsePart(lines[i]));

        return parts;
    }

    public long GetSum()
    {
        var sum = 0L;
        foreach (var part in Parts)
        {
            var at = "in";
            while (at != "R" && at != "A")
            {
                var currentSort = Sortes[at];
                at = currentSort.Sort(part);
            }

            if (at is "A")
                sum += part.X + part.M + part.A + part.S;
        }

        return sum;
    }

    public class Sorter
    {
        public Sorter(string definition)
        {
            var nameAndRest = definition.SplitAndTrim('{', '}');
            Name = nameAndRest.First();
            var rulesStrings = nameAndRest[1].SplitAndTrim(',');
            DefaultTarget = rulesStrings.Last();


            for (var index = 0; index < rulesStrings.Count - 1; index++)
            {
                var ruleString = rulesStrings[index];
                Rules.Add(new Rule(ruleString));
            }
        }

        public string Sort(Part uut)
        {
            foreach (var rule in Rules)
                if (rule.Matches(uut))
                    return rule.Target;

            return DefaultTarget;
        }

        private List<Rule> Rules { get; } = [];

        private string DefaultTarget { get; }

        public class Rule
        {
            public Rule(string ruleString)
            {
                var condAndTarget = ruleString.SplitAndTrim(':');
                Target = condAndTarget[1];
                TestGreater = condAndTarget.First().Contains('>');
                var propAndValue = condAndTarget.First().SplitAndTrim('>', '<');
                PropertyName = propAndValue.First();
                TestValue = propAndValue[1].ToInt32();
            }

            public string Target { get; }

            private int TestValue { get; }

            private string PropertyName { get; }

            public bool Matches(Part uut) =>
                PropertyName switch
                {
                    "x" => TestGreater ? uut.X > TestValue : uut.X < TestValue,
                    "m" => TestGreater ? uut.M > TestValue : uut.M < TestValue,
                    "a" => TestGreater ? uut.A > TestValue : uut.A < TestValue,
                    "s" => TestGreater ? uut.S > TestValue : uut.S < TestValue,
                    _ => throw new ArgumentException("Not a avlid property")
                };

            private bool TestGreater { get; }
        }

        public string Name { get; }
    }

    public record Part(long X, long M, long A, long S);

    public static Part ParsePart(string partString)
    {
        var values = partString.SplitAndTrim('{', 'x', 'm', 'a', 's', ',', '=', '}');
        return new Part(
            values[0].ToInt64(),
            values[1].ToInt64(),
            values[2].ToInt64(),
            values[3].ToInt64());
    }
}
