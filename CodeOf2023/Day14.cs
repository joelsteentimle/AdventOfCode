namespace AoC2023;

public class Day14(IList<string> realLines)
{
    private MapElement[,] Map { get; set; } = CalculateMap(realLines);

    private static MapElement[,] CalculateMap(IList<string> list)
    {
        var initialMap = new MapElement[list[0].Length, list.Count];

        for (var y = 0; y < list.Count; y++)
        for (var x = 0; x < list[y].Length; x++)
            initialMap[x, y] = list[y][x] switch
            {
                'O' => MapElement.Rolling,
                '#' => MapElement.Stuck,
                '.' => MapElement.Empty,
                _ => throw new ArgumentException("Not know ")
            };

        return initialMap;
    }

    public long CalculateLoad()
    {
        long totalLoad = 0;
        var mapHeight = Map.GetLength(1);
        for (var x = 0; x < Map.GetLength(0); x++)
        for (var y = 0; y < Map.GetLength(1); y++)
            if (Map[x, y] == MapElement.Rolling)
                totalLoad += mapHeight - y;

        return totalLoad;
    }

    private enum MapElement
    {
        Rolling = 1,
        Stuck = 2,
        Empty = 3
    }

    public void CycleRoll(int cycles)
    {
        List<MapElement[,]> preCycleMap = [];

        for (var i = 0; i < cycles; i++)
        {
            var indexOf = preCycleMap.FindIndex(pr => MapEqual(pr, Map));
            if (indexOf != -1)
            {
                var cyclesToRepeat = preCycleMap.Count - indexOf;
                CycleRoll((cycles - i) % cyclesToRepeat);
                break;
            }

            preCycleMap.Add(Map);
            RollNorth();
            RollWest();
            RollSouth();
            RollEast();
        }
    }

    private bool MapEqual(MapElement[,] preCycleMap, MapElement[,] map)
    {
        for (var x = 0; x < Map.GetLength(0); x++)
        for (var y = 0; y < Map.GetLength(1); y++)
            if (preCycleMap[x, y] != map[x, y])
                return false;

        return true;
    }

    public void RollNorth() => Roll((0, -1));
    private void RollEast() => Roll((1, 0));
    private void RollSouth() => Roll((0, 1));
    private void RollWest() => Roll((-1, 0));

    private void Roll((int dx, int dy) direction)
    {
        var (dx, dy) = direction;
        var width = Map.GetLength(0);
        var height = Map.GetLength(1);
        var rolledMap = new MapElement[height, width];

        var xStart = dx > 0 ? width - 1 : 0;
        var yStart = dy > 0 ? height - 1 : 0;
        var xIter = dx > 0 ? -dx : 1;
        var yIter = dy > 0 ? -dy : 1;

        for (var x = xStart; x < width && x >= 0; x += xIter)
        for (var y = yStart; y < height && y >= 0; y += yIter)
            if (Map[x, y] == MapElement.Rolling)
            {
                rolledMap[x, y] = MapElement.Empty;
                var (rolledX, rolledY) = RollBoulder(rolledMap, (x, y), direction);
                rolledMap[rolledX, rolledY] = MapElement.Rolling;
            }
            else
                rolledMap[x, y] = Map[x, y];

        Map = rolledMap;
    }

    private (int rolledX, int rolledY) RollBoulder(MapElement[,] map, (int x, int y) boulderPosition,
        (int x, int y) direction)
    {
        var nextPosition = Move(boulderPosition, direction);

        while (InsideBounds(nextPosition) && map[nextPosition.x, nextPosition.y] == MapElement.Empty)
        {
            boulderPosition = nextPosition;
            nextPosition = Move(boulderPosition, direction);
        }

        return boulderPosition;
    }

    private bool InsideBounds((int x, int y) pos) =>
        0 <= pos.x && pos.x < Map.GetLength(0)
                   && 0 <= pos.y && pos.y < Map.GetLength(1);

    private static (int x, int y) Move((int x, int y) pos, (int dx, int dy) dir)
        => (pos.x + dir.dx, pos.y + dir.dy);
}
