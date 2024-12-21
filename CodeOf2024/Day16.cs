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
    private int MaxY;
    private readonly int MaxX;
    private Raindeer StartRaindeer;
    private SortedSet<Raindeer> Raindeers;
    private readonly (int y, int x) End;


    public Day16(List<string> allData)
    {
        MaxY = allData[0].Length;
        MaxX = allData.Count;
        Field = new FieldEntry[MaxX, MaxY];
        Raindeers = new SortedSet<Raindeer>(new RaindeerComparer());

        for (var y = 0; y < MaxY; y++)
        {
            for (var x = 0; x < MaxX; x++)
            {
                if (allData[y][x] == 'S')
                {
                    Field[x, y] = FieldEntry.Floor;
                    StartRaindeer = new Raindeer((x, y), (1, 0), 0);
                }else if (allData[y][x] == 'E')
                {
                    Field[x, y] = FieldEntry.Floor;
                    End = (y, x);
                }
                else
                {
                    Field[x, y] = allData[y][x] switch
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
        (int py, int px) = raindeer.pos;
        (int dy, int dx) = raindeer.dir;
        Raindeers.Remove(raindeer);

        var forwardRaindeer = StartRaindeer.Move();

        if (!IsOutOfBound(forwardRaindeer.pos))
        {
            Raindeers.Add(forwardRaindeer);
        }

        Raindeers.Add(raindeer.TurnRight());
        Raindeers.Add(raindeer.TurnLeft());
    }


    public long Part1()
    {
        while (true)
        {
            var cheepestRainder = Raindeers.Min;

            if (cheepestRainder.pos == End)
            {
                return cheepestRainder.points;
            }

            MoveRaindeer(cheepestRainder);
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
