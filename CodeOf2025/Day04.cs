namespace AoC2025;

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

    public int Part1()
    {
        var liftable = 0;
        for (var y = 0; y < MaxY; y++)
        for (var x = 0; x < MaxX; x++)
        {
            if (Grid[y,x] == '@' && CanLift(y, x))
                liftable++;
        }

        return liftable;
    }

    public int Part2()
    {
        var liftable = 0;

        var toTest = new HashSet<(int y, int x)>();

        for (var y = 0; y < MaxY; y++)
        for (var x = 0; x < MaxX; x++)
            if (Grid[y, x] == '@') toTest.Add((y, x));

        // while (toTest.Count > 0)
        // {
        //   var testBoulder = toTest.First();
        //   toTest.Remove(testBoulder);
        //
        //   if (CanLift(testBoulder.y, testBoulder.x))
        //   {
        //       liftable++;
        //       Grid[testBoulder.y, testBoulder.x] = '.';
        //       foreach (var (dy, dx) in AllDirections)
        //           if (IsBlocked(testBoulder.y + dy,testBoulder. x + dx))
        //               toTest.Add((testBoulder.y + dy, testBoulder.x + dx));
        //
        //   }
        // }

        bool didLift;
        do
        {
            didLift = false;
            for (var y = 0; y < MaxY; y++)
            for (var x = 0; x < MaxX; x++)
            {
                if (Grid[y, x] == '@' && CanLift(y, x))
                {
                    liftable++;
                    Grid[y, x] = '.';
                    didLift = true;
                }
            }
        } while (didLift);

        return liftable;
    }

    private bool CanLift(int y, int x)
    {
        var neigbourghBlocked = 0;
        foreach (var (dy, dx) in AllDirections)
        {
            if(IsBlocked(y +dy, x+dx))
                neigbourghBlocked++;
        }
        return neigbourghBlocked < 4;
    }

    private bool IsBlocked(int y, int x) =>
        IsInOfBounds(y, x) && Grid[y, x] == '@';

    private bool IsInOfBounds(int y, int x) =>
        y < MaxY && y >= 0 && x < MaxY && x >= 0;

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
