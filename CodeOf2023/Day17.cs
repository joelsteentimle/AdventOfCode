using System.Data;
using AoC2023.Graph;

namespace AoC2023;

public class Day17(List<string> lines)
{
    public int[,] Map { get; } = CalculateMap(lines);
    public Path?[,] PathMap { get; } = InitializePaths(lines);

    private static Path?[,] InitializePaths(List<string> list)
    {
        var initialPath = new Path[list[0].Length, list.Count];
        return initialPath;
    }
    private static int[,] CalculateMap(List<string> list)
    {
        var initialMap = new int[list[0].Length, list.Count];

        for (var y = 0; y < list.Count; y++)
            for (var x = 0; x < list[y].Length; x++)
                initialMap[x, y] = $"{list[y][x]}".ToInt32();

        return initialMap;
    }

    public int Dijkstra(Position from, Position to)
    {
        var known = new SortedSet<Path>(new Path.Comparer());

        var current = new Path(new Position(from.X, from.Y), 0, Direction.East, 0);
        known.Add(current);
        var processed = 0;
        while (current != null
               && (current.Position.X != to.X || current.Position.Y != to.Y)
               && current.Cost < 500)
        {
            processed++;
            foreach (var dir in AllButBack(current.LastDirection))
            {
                if (current != null && (dir != current.LastDirection || current.TimesInSame < 3))
                {
                    var nextPosition = current.Position.Move(dir);
                    if (InsideBound(nextPosition))
                    {
                        var nextCost = current.Cost + Map[nextPosition.X, nextPosition.Y];

                        var previousTargetPath = PathMap[nextPosition.X, nextPosition.Y];
                        if (previousTargetPath is null || (previousTargetPath.Cost + 18)  > nextCost )
                        {
                            var nextPath = new Path(
                                nextPosition,
                                nextCost,
                                dir,
                                dir == current.LastDirection ? current.TimesInSame + 1 : 1
                            );
                            known.Add(nextPath);
                            if (previousTargetPath is null || previousTargetPath.Cost > nextCost)
                                PathMap[nextPath.Position.X, nextPath.Position.Y] = nextPath;
                        }
                    }
                }
            }

            if (current is not null)
                known.Remove(current);

            current = known.Min;
        }

        if (current is null || current.Position.X != to.X || current.Position.Y != to.Y)
            throw new EvaluateException("No path found");

        return current.Cost;
    }

    private Direction[] AllButBack(Direction dir) =>
        dir switch
        {
            Direction.East => [Direction.North, Direction.West, Direction.South],
            Direction.South => [Direction.North, Direction.West, Direction.East],
            Direction.West => [Direction.North, Direction.South, Direction.East],
            Direction.North => [Direction.West, Direction.South, Direction.East],
            _ => throw new ArgumentOutOfRangeException(nameof(dir), dir, null)
        };


    private bool InsideBound(Position position) =>
        position.X < Map.GetLength(0)
        && position.X >= 0
        && position.Y < Map.GetLength(1)
        && position.Y >= 0;

     public class Path(Position position, int Cost, Direction lastDirection, int timesInSame)
     {
         public Position Position { get; } = position;
         public int Cost { get; } = Cost;
         public Direction LastDirection { get; } = lastDirection;
         public int TimesInSame { get; } = timesInSame;

         public  class Comparer : Comparer<Path>
         {
             public override int Compare(Path? x, Path? y)
             {
                 if (ReferenceEquals(x,y)) return 0;
                 if (ReferenceEquals(x,null)) return -1;
                 if (ReferenceEquals(null,y)) return 1;
                 var costDif = x.Cost.CompareTo(y.Cost);
                 if (costDif != 0)
                     return costDif;
                 if(x.Position.X == y.Position.X
                     && x.Position.Y == y.Position.Y
                     && x.LastDirection == y.LastDirection
                     && x.TimesInSame == y.TimesInSame)
                     return 0;
                 else
                     return -1;
             }
         }
    }
}
