using Open.Numeric.Primes.Extensions;

namespace AoC2023;

public class Day10
{
    public Day10(IList<string> lines)
    {
        var positions=lines.Select(l => l.ToCharArray()).ToArray();
        
        Map = new Node[positions.Length, positions[0].Length];
        Distances = new int[positions.Length, positions[0].Length];
        
        for (int y = 0; y < positions.Length; y++)
        {
            for (int x = 0; x < positions[y].Length; x++)
            {
                Map[x,y] = new Node(positions[y][x]);
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
                       
                }
            }
        }
        
        throw new NotImplementedException();
    }

    public (int y, int x) Current { get; set; }

    // private void ReadInput(char[][] positions)
    // {
    //     Map = new Node[positions.Length, positions[0].Length];
    //     Distances = new int[positions.Length, positions[0].Length];
    //     
    //     for (int y = 0; y < positions.Length; y++)
    //     {
    //         for (int x = 0; x < positions[y].Length; x++)
    //         {
    //             Map[x,y] = new Node(positions[y][x]);
    //         }
    //     }
    //     
    // }

    private Node[,] Map;
    private int[,] Distances;
    
    public class Node
    {
        (int yMove,int xMove)[] Connected;
        public bool IsStart = false; 
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

