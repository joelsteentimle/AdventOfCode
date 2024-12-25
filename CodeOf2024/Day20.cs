using System.Diagnostics;
using AdventLibrary;

namespace AoC2024;

public class Day20
{
    private enum FieldEntry
    {
        Floor,
        Wall,
    }

    private FieldEntry[,] Field;
    private int?[,] TimeFromStart;

    private Position ProgramPosition;
    // private int[,] TimesVisitedField;
    private int MaxY;
    private readonly int MaxX;
    // private readonly Raindeer StartRaindeer;
    // private readonly List<Raindeer> Raindeers=[];
    private readonly Position Start;
    private readonly Position End;
    // private readonly IComparer<Raindeer> PointComparer = new RaindeerComparer();

    public Day20(List<string> allData)
    {
        MaxY = allData[0].Length;
        MaxX = allData.Count;

        Field = new FieldEntry[MaxY, MaxX];
        TimeFromStart = new int?[MaxY, MaxX];

        for (var y = 0; y < MaxY; y++)
        {
            for (var x = 0; x < MaxX; x++)
            {
                if (allData[y][x] == 'S')
                {
                    Field[y, x] = FieldEntry.Floor;
                    Start = new(y, x);
                    ProgramPosition = new(y, x);

                }else if (allData[y][x] == 'E')
                {
                    Field[y, x] = FieldEntry.Floor;
                    End = new(y, x);
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

    private void NumberTheTrack()
    {
        TimeFromStart[Start.Y, Start.X] = 0;

        for (var timeRun = 1; ProgramPosition != End; timeRun++)
        {
            ProgramPosition = Direction.allDirections
                .Select(ProgramPosition.Move)
                .Single(nextPos => Field[nextPos.Y, nextPos.X] == FieldEntry.Floor &&
                                   TimeFromStart[nextPos.Y, nextPos.X] is null);
            TimeFromStart[ProgramPosition.Y, ProgramPosition.X] = timeRun;
        }
    }

    public List<int> Part1(int withMaxLength = 2)
    {
        NumberTheTrack();
        return FindAllShortCuts(withMaxLength);
    }

    private List<int> FindAllShortCuts(int withMaxLength)
    {
        List<int> shortCuts = [];
        for (int Y = 0; Y < MaxY; Y++)
        for (int X = 0; X < MaxX; X++)
        {
            if (TimeFromStart[Y, X] is { } time)
            {
                var cheatPositions = FindPositionsStepsAway(new Position(Y, X), withMaxLength);

                var timeSavingCheats = cheatPositions
                    .Where(cp => IsInbound(cp.p))
                    .Select(cp => TimeFromStart[cp.p.Y, cp.p.X] is { } aheadOfCurve
                        ? aheadOfCurve - time - cp.time
                        : -100)
                    .Where(saved => saved > 0);

                shortCuts.AddRange(timeSavingCheats);

            }
        }

        return shortCuts;
    }

    private static List<(Position p, int time)> FindPositionsStepsAway(Position position, int steps)
    {
        // HashSet< (Position, int steps)> positions = [(position, 0)];
        HashSet< Position> positions = [(position)];
        HashSet<(Position p, int time)> positionsWithTmie = [(position, 0)];

        for (var i = 1; i <= steps; i++)
        {
            var i1 = i;
            var newPositions = positions.SelectMany(cp => Direction.allDirections.Select(cp.Move));
            var validPositions = newPositions.Where(p => !positions.Contains(p)).ToList();

            positions.UnionWith(validPositions);
            positionsWithTmie.UnionWith(validPositions.Select(vp => (vp, i1)));

            // positionsWithTmie.SelectMany(cp => Direction.allDirections.Select(d => (cp.p.Move(d), i1)));
            // var validPosition =


            // var currentPositions = positions.ToList();
            // positions.UnionWith(currentPositions.SelectMany(cp => Direction.allDirections.Select(cp.Move)));
        }
        return positionsWithTmie.ToList();
    }



    public long Part2()
    {
        var shortTrack100 = 0L;

        return shortTrack100;
    }

    private bool IsInbound(Position position) => !IsOutOfBound(position);

    private bool IsOutOfBound(Position position)
    {
        var (y, x) = position;
        if (y < 0 || y >= MaxY)
            return true;
        if (x < 0 || x >= MaxX)
            return true;
        return false;
    }
}
