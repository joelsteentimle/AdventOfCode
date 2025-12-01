namespace AoC2025;

public class Day06
{
    private readonly Place[,] Field;
    private readonly HashSet<(int y, int x)> LoopBolderPositions = [];
    private readonly HashSet<(int y, int x)>[,] LoopDirectionsAtPlace;
    private bool DidLoop;
    private (int dy, int dx) GuardDirection = (0, 0);

    private (int y, int x) GuardPosition = (0, 0);
    private (int y, int x)? testingLoopBoulder;

    public Day06(List<string> input)
    {
        Field = new Place[input.Count, input[0].Length];
        LoopDirectionsAtPlace = new HashSet<(int y, int x)>[input.Count, input[0].Length];

        for (var y = 0; y < Field.GetLength(0); y++)
        for (var x = 0; x < Field.GetLength(1); x++)
            LoopDirectionsAtPlace[y, x] = [];

        for (var y = 0; y < input.Count; y++)
        {
            var row = input[y].ToCharArray();
            for (var x = 0; x < row.Length; x++)
                switch (row[x])
                {
                    case '.':
                        Field[y, x] = Place.Open;
                        break;
                    case '#':
                        Field[y, x] = Place.Boulder;
                        break;
                    case '^':
                        GuardPosition = (y, x);
                        GuardDirection = (-1, 0);
                        Field[y, x] = Place.Visited;
                        break;
                    // case '>':
                    //     GuardPosition = (y, x);
                    //     GuardDirection = (0, 1);
                    //     Field[y, x] = Place.Visited;
                    //     break;
                    // case 'v':
                    //     GuardPosition = (y, x);
                    //     GuardDirection = (1, 0);
                    //     Field[y, x] = Place.Visited;
                    //     break;
                    // case '<':
                    //     GuardPosition = (y, x);
                    //     GuardDirection = (0, -1);
                    //     Field[y, x] = Place.Visited;
                    //     break;
                }
        }

        GuardStartPosition = GuardPosition;
        GuardStartDirection = GuardDirection;
    }

    public (int dy, int dx) GuardStartDirection { get; set; }

    public (int y, int x) GuardStartPosition { get; set; }

    public int NumberOfLoopBoulders()
    {
        var numberOfLoopBoulders = 0;
        while (TryMoveGuard(
                   GuardPosition,
                   GuardDirection,
                   out GuardPosition,
                   out GuardDirection,
                   true)) ;


        foreach (var loopBoulder in LoopBolderPositions)
        {
            Field[loopBoulder.y, loopBoulder.x] = Place.Boulder;
            DidLoop = false;
            GuardPosition = GuardStartPosition;
            GuardDirection = GuardStartDirection;

            foreach (var loopDirection in LoopDirectionsAtPlace)
                loopDirection.Clear();

            while (TryMoveGuard(
                       GuardPosition,
                       GuardDirection,
                       out GuardPosition,
                       out GuardDirection)) ;

            if (DidLoop)
                numberOfLoopBoulders++;

            Field[loopBoulder.y, loopBoulder.x] = Place.Visited;
        }

        return numberOfLoopBoulders;
    }

    public int GuardVisitedSquares()
    {
        while (TryMoveGuard(GuardPosition, GuardDirection, out GuardPosition, out GuardDirection)) ;

        var sum = 0;
        foreach (var positions in Field)
            if (positions == Place.Visited)
                sum++;

        return sum;
    }

    private bool TryMoveGuard((int y, int x) guardPosition,
        (int dy, int dx) guardDirection,
        out (int y, int x) newGuardPosition,
        out (int dy, int dx) newGuardDirection,
        bool populateLoopBoulder = false)
    {
        newGuardPosition = guardPosition;
        newGuardDirection = guardDirection;

        var nextSquare = Peek(guardPosition, guardDirection);
        if (IsOutOfBound(nextSquare)) return false;

        if (IsMovable(nextSquare))
        {
            if (populateLoopBoulder)
                LoopBolderPositions.Add(nextSquare);

            if (LoopDirectionsAtPlace[nextSquare.y, nextSquare.x].Contains(guardDirection))
            {
                DidLoop = true;
                return false;
            }

            newGuardPosition = nextSquare;
            Field[newGuardPosition.y, newGuardPosition.x] = Place.Visited;
            LoopDirectionsAtPlace[newGuardPosition.y, newGuardPosition.x].Add(guardDirection);

            return true;
        }

        newGuardDirection = RotateRight(guardDirection);
        if (LoopDirectionsAtPlace[guardPosition.y, guardPosition.x].Contains(newGuardDirection))
        {
            DidLoop = true;
            return false;
        }


        LoopDirectionsAtPlace[guardPosition.y, guardPosition.x].Add(newGuardDirection);

        return true;
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

    private (int y, int x) Peek((int y, int x) guardPosition, (int dy, int dx) guardDirection) =>
        (guardPosition.y + guardDirection.dy, guardPosition.x + guardDirection.dx);

    private enum Place
    {
        Open,
        Boulder,
        Visited
    }
}
