

using System.Runtime.InteropServices.ComTypes;

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

    public long Part1() => GetValidMax((a, b) => true);

    private long GetValidMax(Func<Point, Point, bool> validator)
    {
        var max = 0L;
        for (var f = 0; f < points.Count; f++)
        for (var s = f + 1; s < points.Count; s++)
        {
            var currentArea = (Math.Abs(points[f].X - points[s].X) + 1)
                              * (Math.Abs(points[f].Y - points[s].Y) + 1);
            if(currentArea > max &&
                validator(points[f], points[s]))
                max = currentArea;
        }

        return max;
    }


    public long Part2()
    {
        (var xLines, var yLines) = GetAllConnections();

        return GetValidMax(ValidatiorForTwo);

        bool ValidatiorForTwo(Point c1, Point c2)
        {
            var minX = Math.Min(c1.X, c2.X);
            var minY = Math.Min(c1.Y, c2.Y);
            var maxX = Math.Max(c1.X, c2.X);
            var maxY = Math.Max(c1.Y, c2.Y);

            foreach (var yl in yLines.Where(l => l.y > minY && l.y < maxY))
            {
                if ((yl.lx > minX && yl.lx < maxX)
                    || (yl.hx > minX && yl.lx < maxX))
                    return false;
            }

            foreach (var xl in xLines.Where(l => l.x > minX && l.x < maxX))
            {
                if ((xl.ly > minY && xl.ly < maxY)
                    || (xl.hy > minY && xl.ly < maxY))
                    return false;
            }

            return true;
        }
    }

    private record XLine(long x, long ly, long hy);
    private record YLine(long y, long lx, long hx);

    private (XLine[] xLines, YLine[] yLines) GetAllConnections()
    {
        List<XLine> xLines = [];
        List<YLine> yLines = [];

        for (var i = 0; i < points.Count; i++)
        {
            var p1 = points[i];
            var p2 = points[(i+1)%points.Count];

            if (p1.X == p2.X)
                xLines.Add(new(p1.X, Math.Min(p1.Y, p2.Y), Math.Max(p1.Y, p2.Y)));
            else
             yLines.Add(new(p1.Y, Math.Min(p1.X, p2.X), Math.Max(p1.X, p2.X) ));
        }

        return (
            xLines.OrderBy(xl => xl.x).ToArray(),
            yLines.OrderBy(yl => yl.y) .ToArray());
    }
}
