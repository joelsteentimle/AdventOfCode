namespace AoC2024;

public class Day04
{
    int MaxY;
    int MaxX;
    char[,] Grid;

    public Day04(List<string> input)
    {
        MaxY = input.Count;
        MaxX = input[0].Length;

        Grid = new char[MaxY, MaxX];

        for (int i = 0; i < input.Count; i++)
        {
            var charRow = input[i].ToCharArray();
            for (int j = 0; j < charRow.Length; j++)
            {
                Grid[i, j] = charRow[j];
            }
        }
    }

    public int CountAll(List<char> letters)
    {
        var sum = 0;
        for (int y = 0; y < Grid.GetLength(0); y++)
        {
            for (int x = 0; x < Grid.GetLength(1); x++)
            {
                sum += CountWords((y, x), letters);
            }
        }

        return sum;
    }

    private int CountWords((int y, int x) startPosition, List<char> letters)
    {
        var localSum = 0;
         var (y, x) = startPosition;
         if (Grid[y, x] == letters[0])
         {
             foreach (var direction in AllDirections)
             {
                 if (WordMatch(startPosition, direction, letters[1..]))
                     localSum++;
             }
         }
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
    => nextPosition.y <0
       || nextPosition.y >= MaxY
       || nextPosition.x < 0
       || nextPosition.x >= MaxX;

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
}
