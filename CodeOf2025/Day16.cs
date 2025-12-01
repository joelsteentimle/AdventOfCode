using System.Diagnostics;
using System.Text;

namespace AoC2025;

public class Day16
{
    private enum FieldEntry
    {
        Floor,
        Wall,
    }

    private record Raindeer((int y, int x) Pos, (int dy, int dx) Dir, int Points, List<Raindeer> Parents)
    {
        public Raindeer Move() =>
            new Raindeer((Pos.y + Dir.dy, Pos.x +Dir. dx), (Dir.dy, Dir.dx), Points + 1, [this]);
        public Raindeer TurnLeft() =>
            new Raindeer((Pos.y, Pos.x), (-Dir.dx, Dir.dy), Points + 1000, [this]);
        public Raindeer TurnRight() =>
            new Raindeer((Pos.y, Pos.x), (Dir.dx, -Dir.dy), Points + 1000, [this]);
    }

    private class RaindeerComparer : IComparer<Raindeer>
    {
        public int Compare(Raindeer? x, Raindeer? y)
        {
            if (ReferenceEquals(x, y)) return 0;
            if (y is null) return 1;
            if (x is null) return -1;
            return x.Points.CompareTo(y.Points);
        }
    }

    private FieldEntry[,] Field;
    private Raindeer[,,,] VisitedField;
    private int[,] TimesVisitedField;
    private int MaxY;
    private readonly int MaxX;
    private readonly Raindeer StartRaindeer;
    private readonly List<Raindeer> Raindeers=[];
    private readonly (int y, int x) End;
    private readonly IComparer<Raindeer> PointComparer = new RaindeerComparer();

    public Day16(List<string> allData)
    {
        MaxY = allData[0].Length;
        MaxX = allData.Count;

        Field = new FieldEntry[MaxY, MaxX];
        VisitedField = new Raindeer[MaxY, MaxX,3,3];
        TimesVisitedField = new int[MaxY, MaxX];

        for (var y = 0; y < MaxY; y++)
        {
            for (var x = 0; x < MaxX; x++)
            {
                if (allData[y][x] == 'S')
                {
                    Field[y, x] = FieldEntry.Floor;
                    StartRaindeer = new Raindeer((y, x), (0, 1), 0, []);
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
        if (VisitedField[
                raindeer.Pos.y,
                raindeer.Pos.x,
                raindeer.Dir.dy + 1,
                raindeer.Dir.dx + 1] is {} allReadyHere)
        {
            if (allReadyHere.Points == raindeer.Points)
            {
                allReadyHere.Parents.AddRange(raindeer.Parents);
            }

            return;
        }

        VisitedField[
            raindeer.Pos.y,
            raindeer.Pos.x,
            raindeer.Dir.dy+1,
            raindeer.Dir.dx+1] = raindeer;

        TimesVisitedField[
            raindeer.Pos.y,
            raindeer.Pos.x]++;

        var forwardRaindeer = raindeer.Move();

        if (!IsOutOfBound(forwardRaindeer.Pos)
            && Field[forwardRaindeer.Pos.y, forwardRaindeer.Pos.x] == FieldEntry.Floor)
        {
            Raindeers.Add(forwardRaindeer);
        }

        var rightRaindeer = raindeer.TurnRight();
        Raindeers.Add(rightRaindeer);

        var leftReindeer = raindeer.TurnLeft();
        Raindeers.Add(leftReindeer);

        Raindeers.Sort(PointComparer);
    }

    public long Part1() => GetWinningRaindeers().First().Points;

    private List<Raindeer> GetWinningRaindeers()
    {
        Raindeers.Add(StartRaindeer);
        List<Raindeer> winningRaindeers = [];

        long foundGoalCost = long.MaxValue;
        var cheepestRainder = Raindeers.First();

        do
        {
            cheepestRainder = Raindeers.First();

            if (cheepestRainder.Pos == End)
            {
                foundGoalCost = cheepestRainder.Points;
                winningRaindeers.Add(cheepestRainder);
            }

            Raindeers.RemoveAt(0);
            MoveRaindeer(cheepestRainder);

        } while (Raindeers.First().Points <= foundGoalCost);

        return winningRaindeers;
    }

    public long Part2()
    {
        var winners = GetWinningRaindeers();
        var posi = GetAllParentPositions(winners);
        HashSet<(int y, int x)> passingPositions = [];


        return posi.Count;
    }

    private HashSet<(int y, int x)> GetAllParentPositions(List<Raindeer> raindeers)
    {
        HashSet<(int y, int x)> allParents = [];

        foreach (var raindeer in raindeers)
        {
            allParents.Add(raindeer.Pos);
            allParents.UnionWith(GetAllParentPositions(raindeer.Parents));
        }
        return allParents;
    }

    // private void PrintVisited()
    // {
    //     for (var y = 0; y < MaxY; y++)
    //     {
    //         var row = new StringBuilder();
    //         for (var x = 0; x < MaxX; x++)
    //         {
    //             if(Field[y, x] == FieldEntry.Wall)
    //                 row.Append('#');
    //             else
    //             {
    //                 if (TimesVisitedField[y, x] > 0)
    //                 {
    //                     row.Append(TimesVisitedField[y, x] %10);
    //                 }
    //                 else if( VisitedField[y,x, 1,2]
    //                     ||VisitedField[y,x, 1,0]
    //                     ||VisitedField[y,x, 2,1]
    //                     ||VisitedField[y,x, 0,1]
    //                     )
    //                     row.Append('X');
    //                 else
    //                 {
    //                     row.Append('.');
    //                 }
    //             }
    //         }
    //         Console.WriteLine(row.ToString());
    //     }
    // }



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
