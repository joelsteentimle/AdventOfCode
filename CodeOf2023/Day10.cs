namespace AoC2023;

public class Day10
{
    private readonly int?[,] _distances;
    private readonly char[,] _loopChars;
    private readonly char[][] _positionsChars;
    public readonly Node[,] Map;

    public Day10(IList<string> lines)
    {
        _positionsChars = lines.Select(l => l.ToCharArray()).ToArray();

        Map = new Node[_positionsChars.Length, _positionsChars[0].Length];
        _distances = new int?[_positionsChars.Length, _positionsChars[0].Length];
        _loopChars = new char[_positionsChars.Length, _positionsChars[0].Length];


        for (var y = 0; y < _positionsChars.Length; y++)
        {
            for (var x = 0; x < _positionsChars[y].Length; x++)
            {
                Map[y, x] = new Node(_positionsChars[y][x]);
            }
        }

        PolishStart();
    }

    public (int y, int x) Start { get; private set; }

    private void PolishStart()
    {
        for (var y = 0; y < Map.GetLength(0); y++)
        {
            for (var x = 0; x < Map.GetLength(1); x++)
            {
                if (Map[y, x].IsStart)
                {
                    UpdateStartNode(y, x);
                    return;
                }
            }
        }
    }

    private bool IsInRange(int y, int x) =>
        !(y < 0 || x < 0 || y >= Map.GetLength(0) || x >= Map.GetLength(1));

    private Node GetMapNode((int, int ) pos)
    {
        var (y, x) = pos;
        if (!IsInRange(y, x))
            return Node.Oob;

        return Map[y, x];
    }

    private void UpdateStartNode(int y, int x)
    {
        var yMinus = GetMapNode((y - 1, x)).Connected.Select(n => n.yMove).Contains(1);
        var yPlus = GetMapNode((y + 1, x)).Connected.Select(n => n.yMove).Contains(-1);
        var xMinus = GetMapNode((y, x - 1)).Connected.Select(n => n.xMove).Contains(1);
        var xPlus = GetMapNode((y, x + 1)).Connected.Select(n => n.xMove).Contains(-1);

        List<(int y, int x)> connected = [];

        if (yMinus)
            connected.Add((-1, 0));
        if (yPlus)
            connected.Add((1, 0));
        if (xMinus)
            connected.Add((0, -1));
        if (xPlus)
            connected.Add((0, 1));

        Map[y, x].Connected = connected.ToArray();
        _distances[y, x] = 0;
        Start = (y, x);
    }

    public int GetMaxDistance((int y, int x) startPosition)
    {
        var positions = new Queue<((int y, int x), int distance)>();
        positions.Enqueue((startPosition, 0));
        var highest = 0;

        while (positions.Count != 0)
        {
            var (pos, distance) = positions.Dequeue();
            highest = Math.Max(highest, distance);
            var node = GetMapNode(pos);

            var nexts = node.Connected
                .Select(delta => Move(pos, delta));

            _loopChars[pos.y, pos.x] = _positionsChars[pos.y][pos.x];

            foreach (var canMoveTo in
                     nexts.Where(canMoveTo =>
                         IsInRange(canMoveTo.y, canMoveTo.x)
                         && _distances[canMoveTo.y, canMoveTo.x] is null))
            {
                _distances[canMoveTo.y, canMoveTo.x] = distance + 1;
                positions.Enqueue((canMoveTo, distance + 1));
            }
        }

        return highest;
    }

    private static (int y, int x) Move((int y, int x) start, (int y, int x) delta)
        => (start.y + delta.y, start.x + delta.x);

    public int FloodFill()
    {
        var tilesInside = 0;

        // consider the position just x ++ , y++
        for (var y = 1; y < _loopChars.GetLength(0) - 1; y++)
        {
            var isInside = false;

            for (var x = 0; x < _loopChars.GetLength(1) - 1; x++)
            {
                var c = _loopChars[y, x];
                if (c is '|' or 'F' or '7')
                {
                    isInside = !isInside;
                }

                if (isInside && c == 0)
                    tilesInside++;
            }
        }

        return tilesInside;
    }

    public class Node
    {
        public static readonly Node Oob = new();
        public readonly bool IsStart;

        public (int yMove, int xMove)[] Connected;

        private Node()
        {
            Connected = [];
        }

        public Node(char mapTile)
        {
            IsStart = mapTile == 'S';
            Connected = mapTile switch
            {
                '|' => [(1, 0), (-1, 0)],
                '-' => [(0, 1), (0, -1)],
                'L' => [(-1, 0), (0, 1)],
                'J' => [(0, -1), (-1, 0)],
                '7' => [(0, -1), (1, 0)],
                'F' => [(1, 0), (0, 1)],
                _ => []
            };
        }
    }
}