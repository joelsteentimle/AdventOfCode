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
    public static Direction Up = new(-1,0 );
    public static Direction Down = new(1,0);
    public static Direction Left = new(0,-1);
    public static Direction Right = new(0,1);

    public static List<Direction> allDirections = [Up,Down,Left,Right];
}

public class Class1
{

}
