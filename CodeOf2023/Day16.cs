using AoC2023.Graph;

namespace AoC2023;

public class Day16(List<string> lines)
{
    public IList<string> TextGroups { get; } = lines.First().SplitAndTrim(',');
    public char[,] Map { get; } = CalculateMap(lines);
    private List<Direction>[,] PassedBeam { get; set; } = new List<Direction>[0, 0];

    private void InitializePassedBeams()
    {
        PassedBeam = new List<Direction>[Map.GetLength(0), Map.GetLength(1)];

        for (var y = 0; y < Map.GetLength(0); y++)
            for (var x = 0; x < Map.GetLength(1); x++)
                PassedBeam[x, y] = [];
    }

    public int MaxEnergized()
    {
        var maxEnergy = 0;

        for (var x = 0; x < Map.GetLength(0); x++)
        {
            Shine(new Position(x, 0), Direction.South);
            UpdateMaxEnergy();
            Shine(new Position(x, Map.GetLength(1) - 1), Direction.North);
            UpdateMaxEnergy();
        }

        for (var y = 0; y < Map.GetLength(1); y++)
        {
            Shine(new Position(0, y), Direction.East);
            UpdateMaxEnergy();
            Shine(new Position(Map.GetLength(1) - 1, y), Direction.West);
            UpdateMaxEnergy();
        }

        return maxEnergy;

        void UpdateMaxEnergy() => maxEnergy = Math.Max(maxEnergy, Energized());
    }

    public int Energized()
    {
        var energies = 0;
        for (var y = 0; y < PassedBeam.GetLength(0); y++)
            for (var x = 0; x < PassedBeam.GetLength(1); x++)
                if (PassedBeam[x, y].Count > 0)
                    energies++;
        return energies;
    }

    private static char[,] CalculateMap(List<string> lines)
    {
        var initialMap = new char[lines[0].Length, lines.Count];

        for (var y = 0; y < lines.Count; y++)
            for (var x = 0; x < lines[y].Length; x++)
                initialMap[x, y] = lines[y][x];

        return initialMap;
    }

    public IEnumerable<Beam> MoveBeam(Beam current)
    {
        var x = current.Pos.X;
        var y = current.Pos.Y;
        var dir = current.Dir;

        if (OutOfBounds())
            return [];

        if (PassedBeam[x, y].Contains(dir))
            return [];
        else
            PassedBeam[x, y].Add(dir);

        return NewBeams();

        bool OutOfBounds() =>
            x < 0
            || x >= Map.GetLength(0)
            || y < 0
            || y >= Map.GetLength(1);

        List<Beam> NewBeams() => (dir, Map[x, y]) switch
        {

            (Direction.East, '\\') or (Direction.West, '/') =>
                [new Beam(Direction.South, current.Pos.Move(Direction.South))],
            (Direction.East, '/') or (Direction.West, '\\') =>
                [new Beam(Direction.North, current.Pos.Move(Direction.North))],
            (Direction.South, '\\') or (Direction.North, '/') =>
                [new Beam(Direction.East, current.Pos.Move(Direction.East))],
            (Direction.South, '/') or (Direction.North, '\\') =>
                [new Beam(Direction.West, current.Pos.Move(Direction.West))],

            (Direction.North or Direction.South, '-') =>
            [
                new Beam(Direction.East, current.Pos.Move(Direction.East)),
                new Beam(Direction.West, current.Pos.Move(Direction.West))
            ],
            (Direction.West or Direction.East, '|') =>
            [
                new Beam(Direction.South, current.Pos.Move(Direction.South)),
                new Beam(Direction.North, current.Pos.Move(Direction.North))
            ],

            (Direction.North or Direction.South, '|') => [current with { Pos = current.Pos.Move(dir) }],
            (Direction.East or Direction.West, '-') => [current with { Pos = current.Pos.Move(dir) }],
            (_, '.') => [current with { Pos = current.Pos.Move(dir) }],
            _ => []
        };
    }

    // public static Position Move( Direction dir) =>
    //     dir switch
    //     {
    //         Direction.North => pos with { Y = pos.Y - 1 },
    //         Direction.East => pos with { X = pos.X + 1 },
    //         Direction.South => pos with { Y = pos.Y + 1 },
    //         Direction.West => pos with { X = pos.X - 1 },
    //         _ => throw new ArgumentException($"Not a direction! {dir}")
    //     };

    public record struct Beam(Direction Dir, Position Pos);

    public int Shine(Position start, Direction dir)
    {
        InitializePassedBeams();
        var currentBeams = new Queue<Beam>();
        currentBeams.Enqueue(new Beam(dir, start));

        while (currentBeams.Count > 0)
            foreach (var beam in MoveBeam(currentBeams.Dequeue()))
                currentBeams.Enqueue(beam);

        return Energized();
    }
}
