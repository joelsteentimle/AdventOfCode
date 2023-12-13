namespace AoC2023;

public class Day05
{
    private readonly Dictionary<string, Element> elements = [];
    private (string type, List<ElementRange> ids) requested;

    public Day05(IEnumerable<string> input, bool isSeedsRange = false)
    {
        var dataLines = input.ToList();
        ReadInitialLine(dataLines[0], isSeedsRange);
        var data = dataLines[1..];

        var sectionsOfMaps =
            data.Aggregate(new List<List<string>> { new() },
                (list, value) =>
                {
                    if (value.Trim() == string.Empty)
                    {
                        if (list.Last().Count != 0) list.Add([]);
                    }
                    else
                        list.Last().Add(value);

                    return list;
                });

        foreach (var mapSection in sectionsOfMaps) CreateElementMap(mapSection);
    }

    public Day05()
    { }

    private Dictionary<string, List<string>> ExistingMaps { get; } = [];

    public List<ElementRange> GetTargetIdsFromRequested(string target)
    {
        var currentRequest = requested;

        while (currentRequest.type != target)
        {
            var currentElement = elements[currentRequest.type];
            var currentIds = currentRequest.ids;

            var nextElementName = ExistingMaps[currentRequest.type].First();

            currentRequest = (nextElementName,
                    currentIds.SelectMany(id => currentElement.ElementMap[nextElementName].GetTargetRanges(id)).ToList()
                );
        }

        return currentRequest.ids;
    }

    private void CreateElementMap(List<string> mapSection)
    {
        var sourceAndTarget = mapSection.First()
            .SplitAndTrim( '-', ' ');

        var source = sourceAndTarget[0];
        var target = sourceAndTarget[2];

        ExistingMaps.TryAdd(source, []);
        ExistingMaps[source].Add(target);

        elements.TryAdd(source, new Element());

        var element = elements[source];
        element.AddMap(target, mapSection[1..]);
    }

    public (string type, List<ElementRange> ids) ReadInitialLine(string headLine, bool isSeedsRange = false)
    {
        var typeAndData = headLine.Split(':');
        var elementName = typeAndData.First().TrimEnd('s');

        var elementIds = typeAndData[1].Split(' ',
            StringSplitOptions.RemoveEmptyEntries |
            StringSplitOptions.TrimEntries);

        var seedInput = elementIds.Select(id => id.ToInt64()).ToList();

        if (isSeedsRange)
        {
            var seedNumber = new List<ElementRange>();

            for (var i = 0; i + 1 < seedInput.Count; i += 2)
                seedNumber.Add(new ElementRange(seedInput[i], seedInput[i] + seedInput[i + 1] - 1));

            requested = (elementName, seedNumber);
        }
        else
            requested = (elementName, seedInput.Select(id => new ElementRange(id, id)).ToList());

        return requested;
    }

    public readonly struct ElementRange : IEquatable<ElementRange>
    {
        public override int GetHashCode() => HashCode.Combine(Start, End);

        public long Start { get; }
        private long End { get; }

        public ElementRange(long start, long end)
        {
            if (start > end)
                Start = End = 0;
            else
            {
                Start = start;
                End = end;
            }
        }

        private bool Equal(ElementRange other) => Start == other.Start && End == other.End;

        private static ElementRange Empty { get; } = new(0, 0);

        public bool IsEmpty => Equals(Empty);

        public (ElementRange overlap, ElementRange left) OverLap(ElementRange other)
        {
            if (Equal(other)) return (this, Empty);

            var newEnd = Math.Min(End, other.End);
            var newStart = Math.Max(Start, other.Start);

            if (newStart > newEnd) return (Empty, this);

            var overlap = new ElementRange(newStart, newEnd);
            var left = newStart > Start
                ? new ElementRange(Start, newStart - 1)
                : new ElementRange(newEnd + 1, End);

            return (overlap, left);
        }

        public bool Equals(ElementRange other) => Start == other.Start && End == other.End;

        public ElementRange Adjust(long adjustment) => new(Start + adjustment, End + adjustment);

        public override bool Equals(object? obj) => obj is ElementRange range && Equals(range);

        public static bool operator ==(ElementRange left, ElementRange right) => left.Equals(right);

        public static bool operator !=(ElementRange left, ElementRange right) => !(left == right);
    }

    public class Element
    {
        public Dictionary<string, Map> ElementMap { get; } = [];

        public void AddMap(string target, List<string> mapData) => ElementMap[target] = new Map(mapData);


        public class Map
        {
            private readonly List<(ElementRange range, long adjustment)> rangeMaps = [];

            public Map(IEnumerable<string> input)
            {
                foreach (var row in input)
                {
                    var data =
                        row.Split(' ',
                            StringSplitOptions.RemoveEmptyEntries |
                            StringSplitOptions.TrimEntries);

                    var source = data[1].ToInt64();
                    var target = data[0].ToInt64();
                    var range = data[2].ToInt64();

                    rangeMaps.Add((new ElementRange(source, source + range - 1), target - source));
                }
            }

            public List<ElementRange> GetTargetRanges(ElementRange sourceIdRange)
            {
                var resultRanges = new List<ElementRange>();
                var left = sourceIdRange;

                foreach (var (range, adjustment) in rangeMaps)
                {
                    (var matched, left) = left.OverLap(range);

                    if (!matched.IsEmpty) resultRanges.Add(matched.Adjust(adjustment));

                    if (left.IsEmpty) break;
                }

                if (!left.IsEmpty) resultRanges.Add(left);

                return resultRanges;
            }
        }
    }
}
