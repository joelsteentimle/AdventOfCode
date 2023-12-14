namespace AoC2023;

public class Day14(IList<string> realLines)
{
    public Stonish[,] Map { get; private set; } = CalculateMap(realLines);

    private static Stonish[,] CalculateMap(IList<string> list)
    {
        var initialMap = new Stonish[list[0].Length, list.Count];

        for (var y = 0; y < list.Count; y++)
        {
            var row = list[y];
            for (var x = 0; x < row.Length; x++)
            {
                initialMap[x, y] = row[x] switch
                {
                    'O' => Stonish.Rolling,
                    '#' => Stonish.Stuck,
                    '.' => Stonish.Empty,
                    _=> throw new ArgumentException("Not know ")
                };
            }
        }

        return initialMap;
    }

    public long CalculateLoad()
    {
        long totalLoad = 0;
        int mapHeight = Map.GetLength(1);
        for (int x = 0; x < Map.GetLength(0); x++)
        {
            for (int y = 0; y < Map.GetLength(1); y++)
            {
                if (Map[x, y] == Stonish.Rolling)
                    totalLoad += mapHeight - y;
            }
        }

        return totalLoad;
    }

    public enum Stonish  {
        Rolling =1,
        Stuck =2,
        Empty =3
    }

    public void RollNorth()
    {
        var height = Map.GetLength(0);
        var width = Map.GetLength(1);
        var rolledMap = new Stonish[height, width];

        for (int x = 0; x < width; x++)
        {
            for (var y = 0; y < height; y++)
            {
                if(Map[x,y] == Stonish.Rolling)
                {
                    rolledMap[x, y] = Stonish.Empty;
                    var (rolledX, rolledY) = GetRolledUp(rolledMap, (x, y), (0,-1));
                    rolledMap[rolledX, rolledY] = Stonish.Rolling;
                }
                else
                {
                    rolledMap[x, y] = Map[x, y];
                }
            }
        }

        Map = rolledMap;
    }

    private (int rolledX, int rolledY) GetRolledUp(Stonish[,] rolledMap, (int x, int y) position, (int x, int y) direction)
    {
        var nextPosition = Move(position, direction);

        while (InsideBounds(nextPosition) && rolledMap[nextPosition.x, nextPosition.y] == Stonish.Empty)
        {
            position = nextPosition;
            nextPosition = Move(position, direction);
        }

        return position;
    }

    private bool InsideBounds( (int x, int y) pos) =>
        0 <= pos.x && pos.x < Map.GetLength(0)
                   && 0 <= pos.y && pos.y < Map.GetLength(1);

    private  static (int x, int y) Move((int x, int y) pos, (int dx, int dy) dir)
        => (pos.x + dir.dx, pos.y + dir.dy);

    // public (int rolledX, int rolledY) GetRolledUp(Stonish[,] map, int x, int y)
    // {
    //     for (int rollingTo = y - 1; rollingTo >= 0; rollingTo--)
    //     {
    //
    //     }
    // }
}
