using System.Diagnostics.CodeAnalysis;

namespace AoC2024;

public class Day06
{
    private enum Place : int
    {
        Open,
        Boulder,
        Visited
    };

    private (int y, int x) GuardPosition = (0, 0);
    private (int dy, int dx) GuardDirection = (0, 0);
    private Place[,] Field;
    private HashSet<(int y, int x)>[,] DirectionsAtPlace;
    private bool[,] LoopingBoulder;

    private (int y, int x) LoopGuard;
    private (int dy, int dx) LoopGuardDirection;
    private HashSet<(int y, int x)>[,] LoopDirectionsAtPlace;

    public Day06(List<string> input)
    {
        Field = new Place[input.Count, input[0].Length];
        DirectionsAtPlace = new HashSet<(int y, int x)>[input.Count, input[0].Length];
        LoopDirectionsAtPlace = new HashSet<(int y, int x)>[input.Count, input[0].Length];
        LoopingBoulder = new bool[input.Count, input[0].Length];

        for (var y = 0; y < Field.GetLength(0); y++)
        for (var x = 0; x < Field.GetLength(1); x++)
        {
            DirectionsAtPlace[y, x] = [];
            LoopDirectionsAtPlace[y, x] = [];
        }

        for (var y = 0; y < input.Count; y++)
        {
            var row = input[y].ToCharArray();
            for (var x = 0; x < row.Length; x++)
            {
                switch (row[x])
                {
                    case '.':
                        Field[y, x] = Place.Open;
                        break;
                    case '#':
                        Field[y, x] = Place.Boulder;
                        break;
                    case 'X':
                        Field[y, x] = Place.Visited;
                        break;
                    case '^':
                        GuardPosition = (y, x);
                        GuardDirection = (-1, 0);
                        Field[y, x] = Place.Visited;
                        DirectionsAtPlace[y, x].Add(GuardDirection);
                        break;
                    case '>':
                        GuardPosition = (y, x);
                        GuardDirection = (0, 1);
                        Field[y, x] = Place.Visited;
                        DirectionsAtPlace[y, x].Add(GuardDirection);
                        break;
                    case 'v':
                        GuardPosition = (y, x);
                        GuardDirection = (1, 0);
                        Field[y, x] = Place.Visited;
                        DirectionsAtPlace[y, x].Add(GuardDirection);
                        break;
                    case '<':
                        GuardPosition = (y, x);
                        GuardDirection = (0, -1);
                        Field[y, x] = Place.Visited;
                        DirectionsAtPlace[y, x].Add(GuardDirection);
                        break;
                }

            }
        }
    }

    public int NumberOfLoopBoulders()
    {
        while (TryMoveGuard(GuardPosition, GuardDirection, out GuardPosition, out GuardDirection))
        {
            var (pbY,pbX) = Peek(GuardPosition, GuardDirection);
            if (!IsOutOfBound((pbY,pbX)))
            {
                for (var y = 0; y < Field.GetLength(0); y++)
                for (var x = 0; x < Field.GetLength(1); x++)
                {
                    LoopDirectionsAtPlace[y, x] = [];
                }

                if( WillGenerateALoop(GuardPosition, RotateRight( GuardDirection)) )
                    LoopingBoulder[pbY, pbX] = true;

                /*
                 * True loop boulders:
                 * 6,3
                 * 7,6
                 * 8,3
                 * 8,1
                 * 7,7
                 * 9,7
                 *
                 */
            }
        }

        var boulders = 0;
        foreach (var isBolder in LoopingBoulder)
        {
            if(isBolder)
                boulders++;
        }
        return boulders;
    }

    private bool WillGenerateALoop((int y, int x) guardPosition, (int dY, int dX) guardDirection)
    {
        do
        {
            if (DirectionsAtPlace[guardPosition.y,guardPosition.x].Contains(guardDirection)
                || LoopDirectionsAtPlace[guardPosition.y,guardPosition.x].Contains(guardDirection))
                return true;
        } while (TryMoveGuard(guardPosition, guardDirection, out guardPosition, out guardDirection, markVisit: false));
        return false;
    }

    public int GuardVisitedSquares()
    {
        while (TryMoveGuard(GuardPosition, GuardDirection, out GuardPosition, out GuardDirection)) ;

        var sum = 0;
        foreach (var positions in Field)
        {
            if (positions == Place.Visited)
                sum++;
        }

        return sum;
    }

    private bool TryMoveGuard(
        (int y, int x) guardPosition,
        (int dy, int dx) guardDirection,
        out (int y, int x) newGuardPosition,
        out (int dy, int dx) newGuardDirection,
        bool markVisit = true)
    {
        newGuardPosition = guardPosition;
        newGuardDirection = guardDirection;

        var nextSquare = Peek(guardPosition, guardDirection);
        if (IsOutOfBound(nextSquare))
        {
            return false;
        }

        if (IsMovable(nextSquare))
        {
            newGuardPosition = nextSquare;
            if (markVisit)
            {
                Field[newGuardPosition.y, newGuardPosition.x] = Place.Visited;
                DirectionsAtPlace[newGuardPosition.y,newGuardPosition.x].Add(guardDirection);
            }
            else
            {
                LoopDirectionsAtPlace[guardPosition.y,guardPosition.x].Add(guardDirection);
            }
            return true;
        }
        else
        {
            newGuardDirection = RotateRight(guardDirection);
            if(markVisit)
            {
                DirectionsAtPlace[guardPosition.y, guardPosition.x].Add(newGuardDirection);
            }
            else
            {
                LoopDirectionsAtPlace[guardPosition.y, guardPosition.x].Add(guardDirection);
            }
            return true;
        }
    }

    private (int newY, int newX) RotateRight((int dy, int dx) direction)
    {
        var newY = direction.dx switch
        {
            1 => 1,
            -1 => -1,
            _ => 0
        };
        var newX = direction.dy switch
        {
            1 => -1,
            -1 => 1,
            _ => 0
        };
        return (newY, newX);
    }

    private bool IsOutOfBound((int, int ) nextSquare)
    {
        var (y, x) = nextSquare;
        if (y < 0 || y >= Field.GetLength(0))
            return true;
        if (x < 0 || x >= Field.GetLength(1))
            return true;
        return false;
    }

    private bool IsMovable((int y, int x) nextSquare)
    {
        var (y, x) = nextSquare;
        if (Field[y, x] != Place.Visited &&
            Field[y, x] != Place.Open)
            return false;

        return true;
    }

    private (int y, int x) Peek((int y, int x)guardPosition, (int dy, int dx) guardDirection ) =>
        (guardPosition.y + guardDirection.dy, guardPosition.x + guardDirection.dx);
}
