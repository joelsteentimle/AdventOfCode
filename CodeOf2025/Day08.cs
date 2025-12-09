namespace AoC2025;

public class Day08
{
    private record P3D(int x, int y, int z)
    {
        public float Distance(P3D other) =>
            MathF.Sqrt(MathF.Pow(x - other.x, 2) + MathF.Pow(y - other.y, 2) + MathF.Pow(z - other.z, 2));
    }

    private List<P3D> boxes = [];


    public Day08(List<string> input)
    {
        boxes = input.Select(l => new
            P3D(int.Parse(l.Split(',')[0]),
            int.Parse(l.Split(',')[1]),
            int.Parse(l.Split(',')[2]))).ToList();

    }

    private record NodeDistance(float distance, int n1i, int n2i);

    public long NodeThreeProduct(int numberOfPairs)
    {
        Dictionary<int, int> connectedToCluster = [];
        Dictionary<int, HashSet<int>>  clusterContains  = [];

        var orderedDistances = FillDistances();
         var index = 0;

        for (var pairs = 0; pairs < numberOfPairs; index++)
        {
            var (_, n1i, n2i) = orderedDistances[index];
            int? c1 = null;
            int? c2 = null;

            if(connectedToCluster.TryGetValue(n1i, out var n1C))
               c1 = n1C;
            if (connectedToCluster.TryGetValue(n2i, out var n2C))
                c2 = n2C;

            if (c1.HasValue && c2.HasValue && c1.Value == c2.Value)
                continue;

            if (c1.HasValue)
            {
                if (c2.HasValue)
                {
                    foreach (var i in clusterContains[c2.Value])
                    {
                        connectedToCluster[i] = c1.Value;
                        clusterContains[c1.Value].Add(i);
                    }
                }
                connectedToCluster[n2i] = c1.Value;
                clusterContains[c1.Value].Add(n2i);
            }else
            {
                connectedToCluster[n1i] = c2.Value;
                clusterContains[c2.Value].Add(n1i);
            }
            pairs++;

            connectedToCluster[n1i] = n2i;

        }

        return 0;
    }

    private List<NodeDistance> FillDistances()
    {
        var unsorted = new List<NodeDistance>();
        for (var i = 0; i < boxes.Count; i++)
        for (var j = i + 1; j < boxes.Count; j++)
            unsorted.Add(new(boxes[i].Distance(boxes[j]), i, j));

        return unsorted.OrderBy(nd => nd.distance).ToList();
    }
}
