namespace AoC2023.Graph;

public record struct Position(int X, int Y)
{
    public Position Move(Direction dir) =>
        dir switch
        {
            Direction.North => this with { Y = this.Y - 1 },
            Direction.East => this with { X = this.X + 1 },
            Direction.South => this with { Y = this.Y + 1 },
            Direction.West => this with { X = this.X - 1 },
            _ => throw new ArgumentException($"Not a direction! {dir}")
        };
}
