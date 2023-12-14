namespace AoC2023;

public class Day11
{
    private readonly int insertSpace;

    public Day11(IList<string> input, int expandTo = 2)
    {
        insertSpace = expandTo - 1;
        var charArr = input.Select(l => l.ToCharArray().ToList()).ToList();
        expandOnX = new bool[charArr[0].Count];
        expandOnY = new bool[charArr.Count];

        ExpandSpace(charArr);
    }

    private List<Star> Stars { get; } = [];
    private readonly bool[] expandOnX;
    private readonly bool[] expandOnY;

    private void ExpandSpace(List<List<char>> charArr)
    {
        InsertInY();
        InsertInX();

        for (var x = 0; x < charArr[0].Count; x++)
            for (var y = 0; y < charArr.Count; y++)
                if (charArr[y][x] == '#')
                    Stars.Add(new Star(x, y));

        void InsertInY()
        {
            for (var y = charArr.Count - 1; y >= 0; y--)
                if (charArr[y].All(c => c == '.'))
                    expandOnY[y] = true;
        }

        void InsertInX()
        {
            for (var x = charArr[0].Count - 1; x >= 0; x--)
                if (charArr.All(ca => ca[x] == '.'))
                    expandOnX[x] = true;
        }
    }

    public long CalculateStarsDistance()
    {
        var totalDistance = 0L;
        for (var firstStar = 0; firstStar < Stars.Count - 1; firstStar++)
            for (var secondStar = firstStar + 1; secondStar < Stars.Count; secondStar++)
                totalDistance += StarsDistance(Stars[firstStar], Stars[secondStar]);

        return totalDistance;
    }

    private int StarsDistance(Star starOne, Star startTwo)
    {
        var minX = Math.Min(starOne.X, startTwo.X);
        var maxX = Math.Max(starOne.X, startTwo.X);
        var minY = Math.Min(starOne.Y, startTwo.Y);
        var maxY = Math.Max(starOne.Y, startTwo.Y);

        var expandedX = expandOnX[minX..maxX].Count(e => e);
        var expandedY = expandOnY[minY..maxY].Count(e => e);

        var xDistance = maxX - minX;
        var yDistance = maxY - minY;
        return xDistance + yDistance + (insertSpace * (expandedX + expandedY));
    }

    public readonly record struct Star(int X, int Y);
}
