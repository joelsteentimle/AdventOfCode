namespace AoC2024;

public class Day06
{
    public enum Place : int
    {
        Open,
        Boulder,
        Visited
    };

    private (int y, int x) GuardPosition = (0, 0);
    private (int dy, int dx) GuardHeading = (0, 0);
    private Place[,] Field;

    public Day06(List<string> input)
    {
        Field = new Place[input.Count, input[0].Length];

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
                        GuardHeading = (-1, 0);
                        Field[y, x] = Place.Visited;
                        break;
                    case '>':
                        GuardPosition = (y, x);
                        GuardHeading = (0, 1);
                        Field[y, x] = Place.Visited;
                        break;
                    case 'v':
                        GuardPosition = (y, x);
                        GuardHeading = (1, 0);
                        Field[y, x] = Place.Visited;
                        break;
                    case '<':
                        GuardPosition = (y, x);
                        GuardHeading = (0, -1);
                        Field[y, x] = Place.Visited;
                        break;
                }

            }
        }
    }

    public int GuardVisitedSquares()
    {
        while (TryMoveGuard()) ;

        var sum = 0;
        foreach (var positions in Field)
        {
            if (positions == Place.Visited)
                sum++;
        }

        return sum;
    }


    private bool TryMoveGuard()
    {
        var nextSquare = Peek();
        if (IsOutOfBound(nextSquare))
        {
            return false;
        }

        if (IsMovable(nextSquare))

        {
            GuardPosition = nextSquare;
            Field[nextSquare.y, nextSquare.x] = Place.Visited;
            return true;
        }
        else
        {
            var newY = GuardHeading.dx switch
            {
                1 => 1,
                -1 => -1,
                _ => 0
            };
            var newX = GuardHeading.dy switch
            {
                1 => -1,
                -1 => 1,
                _ => 0
            };
            GuardHeading = (newY, newX);
            return true;
        }

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

    private (int y, int x) Peek() =>
        (GuardPosition.y + GuardHeading.dy, GuardPosition.x + GuardHeading.dx);

}
