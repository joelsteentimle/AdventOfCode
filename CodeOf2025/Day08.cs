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

        List<NodeDistance> orderedDistances = FillDistances();

        for (var i = 0; i < numberOfPairs; i++)
        {
            var (distance, n1i, n2i) = orderedDistances[i];

            // custerNumber

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
