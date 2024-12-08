namespace AoC2024;

public class Day04
{
    private readonly char[,] Grid;
    private readonly int MaxX;
    private readonly int MaxY;

    public Day04(List<string> input)
    {
        MaxY = input.Count;
        MaxX = input[0].Length;

        Grid = new char[MaxY, MaxX];

        for (var i = 0; i < input.Count; i++)
        {
            var charRow = input[i].ToCharArray();
            for (var j = 0; j < charRow.Length; j++) Grid[i, j] = charRow[j];
        }
    }

    private List<(int y, int x)> AllDirections =>
    [
        (-1, 0),
        (-1, -1),
        (0, -1),
        (1, -1),
        (1, 0),
        (1, 1),
        (0, 1),
        (-1, 1)
    ];

    public int CountAll(List<char> letters)
    {
        var sum = 0;
        for (var y = 0; y < Grid.GetLength(0); y++)
        for (var x = 0; x < Grid.GetLength(1); x++)
            sum += CountWords((y, x), letters);

        return sum;
    }

    private int CountWords((int y, int x) startPosition, List<char> letters)
    {
        var localSum = 0;
        var (y, x) = startPosition;
        if (Grid[y, x] == letters[0])
            foreach (var direction in AllDirections)
                if (WordMatch(startPosition, direction, letters[1..]))
                    localSum++;

        return localSum;
    }

    private bool WordMatch((int y, int x) position, (int dy, int dx) direction, List<char> letters)
    {
        if (letters.Count == 0)
            return true;

        var (y, x) = position;
        var (dy, dx) = direction;
        (int y, int x) nextPosition = (y + dy, x + dx);

        if (IsOutOfBounds(nextPosition) ||
            Grid[nextPosition.y, nextPosition.x] != letters[0])
            return false;

        return WordMatch(nextPosition, direction, letters[1..]);
    }

    private bool IsOutOfBounds((int y, int x) nextPosition)
        => nextPosition.y < 0
           || nextPosition.y >= MaxY
           || nextPosition.x < 0
           || nextPosition.x >= MaxX;

    public int XCountAll()
    {
        var sum = 0;
        for (var y = 0; y < Grid.GetLength(0); y++)
        for (var x = 0; x < Grid.GetLength(1); x++)
            if (IsXmas((y, x)))
                sum++;

        return sum;
    }

    private bool IsXmas((int y, int x) position)
    {
        var (y, x) = position;

        if (Grid[y, x] != 'A' ||
            y - 1 < 0 || y + 1 >= MaxY ||
            x - 1 < 0 || x + 1 >= MaxX)
            return false;

        var oneLeg = new HashSet<char> { Grid[y - 1, x - 1], Grid[y + 1, x + 1] };
        var otherLeg = new HashSet<char> { Grid[y + 1, x - 1], Grid[y - 1, x + 1] };

        if (oneLeg.Contains('M') && oneLeg.Contains('S') &&
            otherLeg.Contains('M') && otherLeg.Contains('S'))
            return true;

        return false;
    }
}
