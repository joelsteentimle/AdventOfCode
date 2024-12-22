using System.Diagnostics;

namespace AoC2024;

public class Day16
{
    private enum FieldEntry
    {
        Floor,
        Wall,
    }

    private record Raindeer((int y, int x) pos, (int dy, int dx) dir, int points)
    {
        public Raindeer Move() =>
            new Raindeer((pos.y + dir.dy, pos.x +dir. dx), (dir.dy, dir.dx), points + 1);
        public Raindeer TurnLeft() =>
            new Raindeer((pos.y, pos.x), (-dir.dx, dir.dy), points + 1000);
        public Raindeer TurnRight() =>
            new Raindeer((pos.y, pos.x), (dir.dx, -dir.dy), points + 1000);
    }

    private class RaindeerComparer : IComparer<Raindeer>
    {
        public int Compare(Raindeer? x, Raindeer? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (y is null) return 1;
            if (x is null) return -1;
            return x.points.CompareTo(y.points);
        }
    }

    private FieldEntry[,] Field;
    private bool[,,,] VisitedField;
    private int MaxY;
    private readonly int MaxX;
    private Raindeer StartRaindeer;
    private List<Raindeer> Raindeers=[];
    private readonly (int y, int x) End;
    private IComparer<Raindeer> PointComparer = new RaindeerComparer();

    public Day16(List<string> allData)
    {
        MaxY = allData[0].Length;
        MaxX = allData.Count;

        Field = new FieldEntry[MaxY, MaxX];
        VisitedField = new bool[MaxY, MaxX,3,3];

        for (var y = 0; y < MaxY; y++)
        {
            for (var x = 0; x < MaxX; x++)
            {
                if (allData[y][x] == 'S')
                {
                    Field[y, x] = FieldEntry.Floor;
                    StartRaindeer = new Raindeer((y, x), (0, 1), 0);
                }else if (allData[y][x] == 'E')
                {
                    Field[y, x] = FieldEntry.Floor;
                    End = (y, x);
                }
                else
                {
                    Field[y,x] = allData[y][x] switch
                    {
                        '.' => FieldEntry.Floor,
                        '#' => FieldEntry.Wall,
                    };
                }
            }
        }
    }

    private void MoveRaindeer(Raindeer raindeer)
    {
        VisitedField[
            raindeer.pos.y,
            raindeer.pos.x,
            raindeer.dir.dy+1,
            raindeer.dir.dx+1] = true;

        var forwardRaindeer = raindeer.Move();

        if (!IsOutOfBound(forwardRaindeer.pos)
            && Field[forwardRaindeer.pos.y, forwardRaindeer.pos.x] == FieldEntry.Floor
            && !VisitedField[
                forwardRaindeer.pos.y,
                forwardRaindeer.pos.x,
                forwardRaindeer.dir.dy+1,
                forwardRaindeer.dir.dx+1])

        {
            Raindeers.Add(forwardRaindeer);
        }

        var rightRaindeer = raindeer.TurnRight();
        if (!VisitedField[
                rightRaindeer.pos.y,
                rightRaindeer.pos.x,
                rightRaindeer.dir.dy+1,
                rightRaindeer.dir.dx+1
            ])

        {
            Raindeers.Add(rightRaindeer);
        }

        var leftReindeer = raindeer.TurnLeft();
        if (!VisitedField[
                leftReindeer.pos.y,
                leftReindeer.pos.x,
                leftReindeer.dir.dy+1,
                leftReindeer.dir.dx+1])
        {
            Raindeers.Add(leftReindeer);
        }

        Raindeers.Sort(PointComparer);
    }

    public long Part1()
    {
        Raindeers.Add(StartRaindeer);
        var thousandTimer = Stopwatch.StartNew();
        long positionsEvaluated = 0;
        while (true)
        {
            var cheepestRainder = Raindeers.First();
            Raindeers.RemoveAt(0);

            positionsEvaluated++;
            if (cheepestRainder.pos == End)
            {
                return cheepestRainder.points;
            }

            MoveRaindeer(cheepestRainder);
            if (positionsEvaluated % 1000 == 0)
            {
                Console.WriteLine($"Time passed to {positionsEvaluated}: {thousandTimer.ElapsedMilliseconds}ms");
                thousandTimer.Restart();
            }

            if (positionsEvaluated > 91000)
            {
                return -100;
            }
        }
    }

    public long Part2()
    {
        var sum = 0L;


        return -100;
    }

    private bool IsOutOfBound((int, int ) position)
    {
        var (y, x) = position;
        if (y < 0 || y >= MaxY)
            return true;
        if (x < 0 || x >= MaxX)
            return true;
        return false;
    }
}
