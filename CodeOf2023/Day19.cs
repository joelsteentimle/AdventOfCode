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

    public long GetRangeSums()
    {
        Queue<RangePartsTarget> processingParts = new();
        processingParts.Enqueue(new RangePartsTarget(new RangeParts(), "in"));
        List<RangeParts> acceptedParts = [];

        while (processingParts.Count > 0)
        {
            var current = processingParts.Dequeue();
            var currentSort = Sortes[current.Target];
           var processed =
               currentSort.PartRangeToTargets(current.Parts);
           foreach (var p in processed)
           {
               if(p.Target == "A")
                   acceptedParts.Add(p.Parts);
               else if (p.Target != "R")
                   processingParts.Enqueue(p);
           }
        }

        long combinations =0;
        if (acceptedParts.Any(p => p.IsEmpty))
            throw new ArgumentException("Panic!!!");

        foreach (var part in acceptedParts)
        {
            combinations +=
                (part.X.max - part.X.min+1) *
                (part.M.max - part.M.min+1) *
                (part.A.max - part.A.min+1) *
                (part.S.max - part.S.min+1);
        }

        return combinations;

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

        public List<RangePartsTarget> PartRangeToTargets(RangeParts part)
        {
            List<RangePartsTarget> result = [];

            var partsLeft = part;
            foreach (var rule in Rules)
            {
                var (matches, left) = rule.Split(partsLeft);

                if (!matches.IsEmpty)
                    result.Add(new RangePartsTarget(matches, rule.Target));
                partsLeft = left;
            }

            if (!partsLeft.IsEmpty)
                result.Add(new RangePartsTarget(partsLeft, DefaultTarget));

            return result;
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

            public (RangeParts splitted, RangeParts left) Split(RangeParts startParts)
            {
                var split
                    = PropertyName switch
                    {
                        "x" when TestGreater => MatchGreater(startParts.X, TestValue),
                        "x" when !TestGreater => MatchLess(startParts.X, TestValue),
                        "m" when TestGreater => MatchGreater(startParts.M, TestValue),
                        "m" when !TestGreater => MatchLess(startParts.M, TestValue),
                        "a" when TestGreater => MatchGreater(startParts.A, TestValue),
                        "a" when !TestGreater => MatchLess(startParts.A, TestValue),
                        "s" when TestGreater => MatchGreater(startParts.S, TestValue),
                        "s" when !TestGreater => MatchLess(startParts.S, TestValue),
                        _ => throw new ArgumentException("Could not understand test"),
                    };

                return PropertyName switch
                {
                    "x" => (startParts with { X = split.matched }, startParts with { X = split.left }),
                    "m" => (startParts with { M = split.matched }, startParts with { M = split.left }),
                    "a" => (startParts with { A = split.matched }, startParts with { A = split.left }),
                    "s" => (startParts with { S = split.matched }, startParts with { S = split.left }),
                    _ => throw new ArgumentException("Could not understand test"),
                };

                (Range matched, Range left) MatchLess(Range incParts, int testValue)
                {
                    var matched = incParts with { max = Math.Min(incParts.max, testValue - 1) };
                    var left = incParts with { min = Math.Max(incParts.min, testValue) };
                    return (matched, left);
                }

                (Range matched, Range left) MatchGreater(Range incParts, int testValue)
                {
                    var matched = incParts with { min = Math.Max(incParts.min, testValue + 1) };
                    var left = incParts with { max = Math.Min(incParts.max, testValue) };
                    return (matched, left);
                }
            }
        }

        public string Name { get; }
    }

    public record Part(long X, long M, long A, long S);

    public record Range(long min, long max);

    public record RangePartsTarget(RangeParts Parts, string Target);
    public record RangeParts(Range X, Range M, Range A, Range S)
    {
        public bool IsEmpty =>
            X.min > X.max ||
            M.min > M.max ||
            A.min > A.max ||
            S.min > S.max;

        public RangeParts() : this(
            new Range(1, 4000),
            new Range(1, 4000),
            new Range(1, 4000),
            new Range(1, 4000))
        { }
    }

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
