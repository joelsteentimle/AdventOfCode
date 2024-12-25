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

    public List<int> Part1()
    {
        NumberTheTrack();
        return FindAllShortCuts();
        // return TimeFromStart[End.Y, End.X]!.Value;
    }

    private List<int> FindAllShortCuts()
    {

        List<int> shortCuts = [];
        for (int Y = 0; Y < MaxY; Y++)
        for (int X = 0; X < MaxX; X++)
        {
            if (TimeFromStart[Y, X] is { } time)
            {
                var cheatPositions = FindPositions2StepsAway(new Position(Y, X));

                var timeSavingCheats = cheatPositions
                    .Where(IsInbound)
                    .Select(cp => TimeFromStart[cp.Y, cp.X] is {} aheadOfCurve
                    ?  aheadOfCurve - time -2
                    : -100)
                    .Where(saved => saved > 0);
                shortCuts.AddRange(timeSavingCheats);

                Debug.Assert(cheatPositions.Count == 8);
            }
        }

        return shortCuts;
    }

    private List<Position> FindPositions2StepsAway(Position position)
        =>
        [
            position.Move(Direction.Left).Move(Direction.Left),
            position.Move(Direction.Left).Move(Direction.Up),
            position.Move(Direction.Up).Move(Direction.Up),
            position.Move(Direction.Up).Move(Direction.Right),
            position.Move(Direction.Right).Move(Direction.Right),
            position.Move(Direction.Right).Move(Direction.Down),
            position.Move(Direction.Down).Move(Direction.Down),
            position.Move(Direction.Down).Move(Direction.Left)
        ];


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
