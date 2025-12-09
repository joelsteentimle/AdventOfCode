

public class Day09
{
    private record Point(long X, long Y);
    private List<Point> points = [];

    public Day09(List<string> allData)
    {
        foreach (var row in allData)
        {
            var cords = row.Split(',');
            points.Add(new Point(int.Parse(cords[0]), int.Parse(cords[1])));
        }
    }

    public long Part1()
    {
        var max = 0L;

        for (var f = 0; f < points.Count; f++)
        for (var s = f + 1; s < points.Count; s++)
            max = Math.Max(max,
                (Math.Abs(points[f].X - points[s].X) + 1)
                * (Math.Abs(points[f].Y - points[s].Y) + 1)
            );

        return max;
    }


public long Part2()
    {
        return 10;
    }
}
