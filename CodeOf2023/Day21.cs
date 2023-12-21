using AoC2023.Graph;

namespace AoC2023;

public class Day21
{
    // Answer: Can Pass
    private bool[,] BoulderMap { get; } //= CalculateMap(lines);
    private int?[,] CostMap { get; }// = InitializeCosts(lines);

    private int maxSteps = 6;
    private int endeblePlots;
    private int reminder;

    public Position Start { get; }

    public Day21(List<string> lines)
    {
        CostMap = InitializeCosts(lines);
        (BoulderMap, Start) = CalculateMap(lines);
    }

    private static int?[,] InitializeCosts(List<string> list) =>
        new int?[list[0].Length, list.Count];

    private static (bool[,] map,Position start) CalculateMap(List<string> list)
    {
        var initialMap = new bool[list[0].Length, list.Count];
        var start = new Position();

        for (var y = 0; y < list.Count; y++)
            for (var x = 0; x < list[y].Length; x++)
            switch (list[y][x])
            {
                case '#' :
                    initialMap[x, y] = false;
                    break;
                case 'S':
                    start = new Position(x, y);
                    initialMap[x, y] = true;
                    break;
                default:
                    initialMap[x, y] = true;
                    break;
            }

        return (initialMap , start);
    }

    public int CountEndPlots(int steps )
    {
        maxSteps = steps;
        reminder =  maxSteps % 2;

        Dijkstra();
        return endeblePlots;
    }

    private void Dijkstra()
    {
        var known = new SortedSet<PositionCost>(new PositionCost.Comparer());

        var  current = new PositionCost(new Position(Start.X, Start.Y),0);
        CostMap[current.Position.X, current.Position.Y] = 0;
        // Can end at start
        endeblePlots = 0;
        known.Add(current);

        while (current != null && current.Cost <= maxSteps)
        {
            reminder = 0;
            if ( current.Cost % 2 == reminder)
                endeblePlots++;

            foreach (var dir in Enum.GetValues<Direction>())
            {
                var nextPosition = current.Position.Move(dir);
                if (CanMoveTo(nextPosition))
                {
                    var nextCost = current.Cost + 1;



                    var previousTargetPath = CostMap[nextPosition.X, nextPosition.Y];
                    if (previousTargetPath is null)
                    {
                        var next = new PositionCost(nextPosition, nextCost);
                        known.Add(next);

                        CostMap[next.Position.X, next.Position.Y] = nextCost;
                    }
                }
            }

            known.Remove(current);
            current = known.Min;
        }
    }

    private bool CanMoveTo(Position position) =>
        position.X < BoulderMap.GetLength(0)
        && position.X >= 0
        && position.Y < BoulderMap.GetLength(1)
        && position.Y >= 0
        && BoulderMap[position.X,position.Y];

     public class PositionCost(Position position, int cost)
     {
         public Position Position { get; } = position;
         public int Cost { get; } = cost;
         public  class Comparer : Comparer<PositionCost>
         {
             public override int Compare(PositionCost? x, PositionCost? y)
             {
                 if (ReferenceEquals(x,y)) return 0;
                 if (ReferenceEquals(x,null)) return -1;
                 if (ReferenceEquals(null,y)) return 1;
                 var costDif = x.Cost.CompareTo(y.Cost);
                 if (costDif != 0)
                     return costDif;
                 if(x.Position.X == y.Position.X
                    && x.Position.Y == y.Position.Y)
                     return 0;
                 else
                     return -1;

             }
         }
    }
}
