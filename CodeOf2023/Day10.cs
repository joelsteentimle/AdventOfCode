using System.Collections;
using Microsoft.VisualBasic.CompilerServices;
using Open.Numeric.Primes.Extensions;

namespace AoC2023;

public class Day10
{
    public Day10(IList<string> lines)
    {
        var positions=lines.Select(l => l.ToCharArray()).ToArray();
        
        Map = new Node[positions.Length, positions[0].Length];
        Distances = new int?[positions.Length, positions[0].Length];
        
        for (int y = 0; y < positions.Length; y++)
        {
            for (int x = 0; x < positions[y].Length; x++)
            {
                Map[y,x] = new Node(positions[y][x]);
            }
        }

        Current = FindStart();
    }

    private  (int y, int x) FindStart()
    {
        for (int y = 0; y < Map.GetLength(0); y++)
        {
            for (int x = 0; x < Map.GetLength(1); x++)
            {
                if (Map[y, x].IsStart)
                {
                    UpdateStartNode(y, x);
                    return (y, x);
                }
            }
        }

        throw new FormatException("No start");
    }

    private bool IsInRange(int y, int x) => 
        !(y < 0 || x < 0 || y >= Map.GetLength(0) || y >= Map.GetLength(0));

    public Node GetMapNode((int , int ) pos)
    {
        (int y, int x) = pos;
        if (y < 0 || x < 0 || y >= Map.GetLength(0) || y >= Map.GetLength(0))
            return Node.Oob;

        return Map[y, x];
    }

    private void UpdateStartNode(int y, int x)
    {
        var yminus = GetMapNode((y - 1, x)).Connected.Select(n => n.yMove).Contains(1);
        var yplus = GetMapNode((y + 1, x)).Connected.Select(n => n.yMove).Contains(-1);
        var xminus = GetMapNode((y, x - 1)).Connected.Select(n => n.xMove).Contains(1);
        var xplus = GetMapNode((y, x + 1)).Connected.Select(n => n.xMove).Contains(-1);

        List<(int y, int x)> connected = [];

        if (yminus)
            connected.Add((-1, 0));
        if (yplus)
            connected.Add((1, 0));
        if (xminus)
            connected.Add((0, -1));
        if (xplus)
            connected.Add((0, 1));

        Map[y, x].Connected = connected.ToArray();
        Map[y, x].IsStart = true;
        Distances[y, x] = 0;
        Current = (y, x);
    }

    public int GetMaxDistance((int y, int x) startPostion)
    {
        var positions = new Queue<((int y, int x), int distance)>();
        positions.Enqueue((startPostion, 0));
        var highest = 0;

        while (positions.Any())
        {
            var (pos,distance) = positions.Dequeue();
            highest = Math.Max(highest,distance);
            var node = GetMapNode(pos);

            var nexts = node.Connected
                .Select(delta => Move(pos, delta));

            foreach (var canMoveTo in nexts)
            {
                if (IsInRange(canMoveTo.y, canMoveTo.x)
                    && Distances[canMoveTo.y, canMoveTo.x] is null)
                {
                    Distances[canMoveTo.y, canMoveTo.x] = distance + 1;
                    positions.Enqueue((canMoveTo, distance + 1));

                }
            }
        }

        return highest;

    }

    public (int y, int x) Move((int y, int x) start, (int y, int x) delta)
        => (start.y + delta.y, start.x + delta.x);

    public (int y, int x) Current { get; set; }

    public Node[,] Map;
    private int?[,] Distances;
    
    public class Node
    {
        public static readonly Node Oob = new( );

        private Node()
        {
            Connected = [];
        }
        
        public (int yMove,int xMove)[] Connected;
        public bool IsStart; 
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

