using Microsoft.Win32.SafeHandles;

namespace AoC2024;

public class Day12
{
    private char[,] Field;
    private bool[,] HaveBeenFenced;
    private int MaxY;
    private readonly int MaxX;
    private bool[,] FencePositionsDy;
    private bool[,] FencePositionsDx;


    public Day12(List<string> allData)
    {
        MaxY = allData.Count;
        MaxX = allData[0].Length;
        Field = new char[MaxY, MaxX];
        HaveBeenFenced = new bool[MaxY, MaxX];

        for (int y = 0; y < MaxY; y++)
        {
            for (int x = 0; x < MaxX; x++)
            {
                Field[y, x] = allData[y][x];
            }


        }
    }

    public long Part1()
    {
        var sum = 0L;

        for (int y = 0; y < MaxY; y++)
        {
            for (int x = 0; x < MaxX; x++)
            {
                sum += CalculateFieldCost((y, x) );
            }

        }

        return sum;
    }

    private long CalculateFieldCost((int y, int x) startPosition)
    {
        if (HaveBeenFenced[startPosition.y, startPosition.x])
            return 0;

        FencePositionsDy = new bool[MaxY + 1, MaxX];
        FencePositionsDx = new bool[MaxY, MaxX + 1];

        var (area, fence) = FloodFillApproach(startPosition);

        return area * fence;
    }

    private long CalculateFieldWithSideCost((int y, int x) startPosition)
    {
        if (HaveBeenFenced[startPosition.y, startPosition.x])
            return 0;

        FencePositionsDy = new bool[MaxY + 1, MaxX];
        FencePositionsDx = new bool[MaxY, MaxX + 1];

        var (area, fence) = FloodFillApproach(startPosition);

        var dxEdges = 0;
        var dyEdges = 0;
        var continiousFence = false;

        for (int y = 0; y < MaxY + 1; y++)
        {
            continiousFence = false;
            for (int x = 0; x < MaxX; x++)
            {
                if (FencePositionsDy[y, x])
                {
                    if (!continiousFence)
                        dyEdges++;

                    else
                    {
                        if (!IsOutOfBound((y - 1, x - 1)) &&
                            !IsOutOfBound((y, x))
                            && (Field[y - 1, x - 1] != Field[y-1, x ]
                                && Field[y , x-1] != Field[y, x]))
                        {
                            dyEdges++;
                        }

                    }


                    continiousFence = true;
                }
                else
                {
                    continiousFence = false;
                }
            }
        }

        for (int x = 0; x < MaxX + 1; x++)
        {
            continiousFence = false;
            for (int y = 0; y < MaxY; y++)
            {
                if (FencePositionsDx[y, x])
                {
                    if (!continiousFence)
                        dxEdges++;
                    else
                    {
                        if (!IsOutOfBound((y - 1, x - 1)) &&
                            !IsOutOfBound((y, x)) &&
                            (Field[y - 1, x - 1] != Field[y, x - 1]
                             && Field[y - 1, x] != Field[y, x]))
                        {
                            dxEdges++;
                        }

                    }

                    continiousFence = true;
                }
                else
                {
                    continiousFence = false;
                }
            }
        }



        return area * (dxEdges + dyEdges);
    }

    private (int area, int fence) FloodFillApproach((int y, int x) fromPosition)
    {
        if(HaveBeenFenced[fromPosition.y, fromPosition.x])
            return (0, 0);

        var area = 1;
        var fence = 0;
        HaveBeenFenced[fromPosition.y, fromPosition.x] = true;

        var nextSteps = NextPositions(fromPosition);

        foreach (var nextStep in nextSteps)
        {
            if (IsOutOfBound((nextStep.y, nextStep.x))
                || Field[nextStep.y, nextStep.x] != Field[fromPosition.y, fromPosition.x]
                || IsOutOfBound(nextStep))
            {
                fence++;
                if (fromPosition.y != nextStep.y)
                {
                    var fencY = fromPosition.y > nextStep.y ? fromPosition.y : fromPosition.y + 1;
                    FencePositionsDy[fencY, fromPosition.x] = true;
                }
                else
                {
                    var fencX = fromPosition.x > nextStep.x ? fromPosition.x : fromPosition.x + 1;
                    FencePositionsDx[fromPosition.y, fencX] = true;
                }
            }

            else if(!HaveBeenFenced[nextStep.y, nextStep.x] )
            {
                var (newArea, newFenc) = FloodFillApproach(nextStep);
                area += newArea;
                fence += newFenc;
            }
        }

        return (area, fence);
    }


    public long Part2()
    {
        var sum = 0L;

        for (int y = 0; y < MaxY; y++)
        {
            for (int x = 0; x < MaxX; x++)
            {
                sum += CalculateFieldWithSideCost((y, x) );
            }
        }

        return sum;
    }
    private bool IsOutOfBound((int, int ) position)
    {
        var (y, x) = position;
        if (y < 0 || y >= MaxY)
            return true;
        if (x < 0 || x >= MaxX)
            return true;
        return false;
    }

    private List<(int y, int x) > NextPositions((int y, int x) position) =>
        AllDirections.Select(d => (position.y + d.y, position.x + d.x)).ToList();

    private List<(int y, int x)> AllDirections =>
    [
        (-1, 0),
        (0, -1),
        (1, 0),
        (0, 1),
    ];
}
