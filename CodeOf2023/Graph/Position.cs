namespace AoC2023.Graph;

public readonly record struct Position(long X, long Y)
{
    public Position Move(Direction dir) => Move(dir, 1);

    public Position Move(Direction dir, long distance) =>
        dir switch
        {
            Direction.North => this with { Y = Y - distance },
            Direction.East => this with { X = X + distance },
            Direction.South => this with { Y = Y + distance },
            Direction.West => this with { X = X - distance },
            _ => throw new ArgumentException($"Not a direction! {dir}")
        };
}
