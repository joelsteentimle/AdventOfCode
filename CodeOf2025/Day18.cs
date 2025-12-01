using System.Diagnostics;
using System.Reflection;
using System.Text;
using AdventLibrary;

namespace AoC2025;

public class Day18
{
    private enum FieldEntry
    {
        Floor=0,
        Stone,
    }

    private FieldEntry[,] Field;
    private bool[,] VisitedField;
    private List<Position> CorruptedPositions;

    private Position Start = new Position(0, 0);
    private Position End;

    private int MaxY;
    private readonly int MaxX;

    public Day18(List<string> allData, int ySize, int xSize)
    {
        MaxY = ySize+1;
        MaxX = xSize+1;

        Field = new FieldEntry[MaxY, MaxX];
        VisitedField = new bool[MaxY, MaxX];

        CorruptedPositions = [];
        End= new Position(ySize, xSize);

        foreach (var row in allData)
        {
            var splitted = row.Split(',', StringSplitOptions.RemoveEmptyEntries);
            CorruptedPositions.Add(new(int.Parse(splitted[1]), int.Parse(splitted[0])));
        }
    }

    public long Part1(int afterFalls)
    {
        for (int i = 0; i < afterFalls; i++)
        {
            var nowFalling = CorruptedPositions[i];
            Field[nowFalling.Y, nowFalling.X] = FieldEntry.Stone;
        }
        return FindShortestPath();
    }


    public Position Part2()
    {
        var leftBound = 0;
        var rightBound = CorruptedPositions.Count;

        while (leftBound+1 < rightBound)
        {
            var middle = (leftBound + rightBound) / 2;

            if (CanWinWithFallingBlocks(middle))
            {
                leftBound = middle;
            }
            else
            {
                rightBound = middle;
            }
        }

        return CorruptedPositions[leftBound];
    }

    private bool CanWinWithFallingBlocks(int middle)
    {
        Field = new FieldEntry[MaxY, MaxX];
        VisitedField = new bool[MaxY, MaxX];

        for (int i = 0; i < middle; i++)
        {
            var nowFalling = CorruptedPositions[i];
            Field[nowFalling.Y, nowFalling.X] = FieldEntry.Stone;
        }
        return FindShortestPath() > 0;
    }

    private long FindShortestPath()
    {
        List<Position> hereAt = [Start];
        var moves = 0;

        while (true)
        {
            List<Position> newPositions = [];

            foreach (var here in hereAt)
            {
                if (here == End)
                    return moves;

                var newHeres = Direction
                    .allDirections
                    .Select(here.Move)
                    .Where(p => IsInbound(p)
                                && !VisitedField[p.Y, p.X]
                                && Field[p.Y, p.X] != FieldEntry.Stone)
                    .ToList();

                foreach (var newHere in newHeres)
                    VisitedField[newHere.Y, newHere.X] = true;

                newPositions.AddRange(newHeres);
            }

            if (newPositions.Count == 0)
                return -1;

            hereAt = newPositions;
            moves++;
        }
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
