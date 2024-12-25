using System.Diagnostics;

namespace AdventLibrary;

[DebuggerDisplay("y: {Y}, x: {X}")]
public record Position(int Y, int X)
{
    public Position Move(Direction direction)
        => new (Y + direction.dy, X + direction.dx);
}

public record Direction(int dy, int dx)
{
    public static Direction North = new(-1,0 );
    public static Direction South = new(1,0);
    public static Direction West = new(0,-1);
    public static Direction East = new(0,1);

    public static List<Direction> allDirections = [North,South,West,East];
}
