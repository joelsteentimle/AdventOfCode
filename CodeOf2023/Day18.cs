using AoC2023.Graph;

namespace AoC2023;

public class Day18(List<string> lines, bool IsPart2)
{
    private long xMin;
    private long xMax;
    private long yMin;
    private long yMax;
    public List<Instruction> Insturtctions { get; } = CalculateInstructions(lines, IsPart2);
    public LagoonPoint?[,] LavaMap { get; private set; } = new LagoonPoint[0, 0];
    public Position StartPoint { get; private set; } = new(0, 0);
    public bool RightIsIn { get; private set; }

    private static List<Instruction> CalculateInstructions(List<string> list, bool isPart2) =>
        list.Select(l => isPart2 ? Instruction.Part2(l) : new Instruction(l)).ToList();

    public void GenerateFieldAndStart()
    {
        FindCornerAndStart();
        LavaMap = new LagoonPoint[xMax - xMin + 1, yMax - yMin + 1];
    }

    public void FindCornerAndStart()
    {
        var position = new Position(0, 0);

        foreach (var instruction in Insturtctions)
        {
            position = position.Move(instruction.Direction, instruction.Distance);
            yMin = Math.Min(position.Y, yMin);
            yMax = Math.Max(position.Y, yMax);
            xMin = Math.Min(position.X, xMin);
            xMax = Math.Max(position.X, xMax);

            if (position.Y == yMin && position.X == xMin)
            {
                if (instruction.Direction == Direction.North)
                    RightIsIn = true;
                else if (instruction.Direction == Direction.West)
                    RightIsIn = false;
                else
                    throw new ArgumentException("You don't understand left and right");
            }

        }
        StartPoint = new Position(-xMin, -yMin);
    }


    public void WalkThroughTheTrench()
    {
        var currentPos = StartPoint;
        foreach (var instruction in Insturtctions)
            for (var i = 0; i < instruction.Distance; i++)
            {
                LavaMap[currentPos.X, currentPos.Y] = new LagoonPoint(true, true);
                currentPos = currentPos.Move(instruction.Direction);
            }
    }

    public void DigInside()
    {
        var currentTrench = StartPoint;
        foreach (var instruction in Insturtctions)
        {
            var insideDirection = LookInside(instruction.Direction);

            var canBeInside = currentTrench.Move(insideDirection);
            while (LavaMap[canBeInside.X, canBeInside.Y]?.Trench is not true)
            {
                LavaMap[canBeInside.X, canBeInside.Y] = new LagoonPoint(false, true);
                canBeInside = canBeInside.Move(insideDirection);
            }

            for (var i = 0; i < instruction.Distance; i++)
            {
                currentTrench = currentTrench.Move(instruction.Direction);
                canBeInside = currentTrench.Move(insideDirection);
                while (LavaMap[canBeInside.X, canBeInside.Y]?.Trench is not true)
                {
                    LavaMap[canBeInside.X, canBeInside.Y] = new LagoonPoint(false, true);
                    canBeInside = canBeInside.Move(insideDirection);
                }
            }
        }
    }

    public long Part2PoolVolume()
    {
        var current = StartPoint;
        var totalDoubleCountedVolume = 0L;
        var distance = 0L;

        for (var i = 0; i < Insturtctions.Count; i++)
        {
            var instruction = Insturtctions[i];

            if (instruction.Direction is Direction.North or Direction.South)
            {
                var nextDir = Insturtctions[(i + 1) % Insturtctions.Count].Direction;
                var prevDir = Insturtctions[(i + Insturtctions.Count - 1) % Insturtctions.Count].Direction;

                var nrOuter = 0;
                if (LookInside(instruction.Direction) == nextDir)
                    nrOuter++;
                if (LookInside(prevDir) == instruction.Direction)
                    nrOuter++;

                // Right of is inside
                if ((instruction.Direction is Direction.North && RightIsIn)
                    || (instruction.Direction is Direction.South && !RightIsIn))
                {
                    totalDoubleCountedVolume += (xMax - current.X + 1) * (instruction.Distance - 1 + nrOuter);
                    totalDoubleCountedVolume -= current.X * (instruction.Distance - 1 + nrOuter);
                }
                else
                {
                    totalDoubleCountedVolume += (current.X + 1) * (instruction.Distance - 1 + nrOuter);
                    totalDoubleCountedVolume -= (xMax - current.X) * (instruction.Distance - 1 + nrOuter);
                }
            }

            current = current.Move(instruction.Direction, instruction.Distance);
        }

        return (totalDoubleCountedVolume / 2) + distance;
    }

    public Direction LookInside(Direction lastMove) =>
        (Direction)((int)(lastMove + (RightIsIn ? 1 : 3)) % 4);

    public int TrenchVolume() => SumUp((lp) => lp?.Trench is true);
    public int Part1PoolVolume() => SumUp((lp) => lp?.Pool is true);

    private int SumUp(Func<LagoonPoint?, bool> countPoint)
    {
        var digged = 0;

        for (var x = 0; x < LavaMap.GetLength(0); x++)
            for (var y = 0; y < LavaMap.GetLength(1); y++)
                if (countPoint(LavaMap[x, y]))
                    digged++;

        return digged;
    }

    public record LagoonPoint(bool Trench, bool Pool);

    public class Instruction
    {
        public Direction Direction { get; }
        public long Distance { get; }

        public Instruction(Direction dir, int dist)
        {
            Direction = dir;
            Distance = dist;
        }

        public Instruction(string line)
        {
            var splitLine = line.SplitAndTrim(' ');
            Direction = splitLine[0] switch
            {
                "R" => Direction.East,
                "D" => Direction.South,
                "L" => Direction.West,
                "U" => Direction.North,
                _ => throw new ArgumentException("Not valid!")
            };
            Distance = splitLine[1].ToInt32();
        }

        public static Instruction Part2(string line)
        {
            var splitLine = line.SplitAndTrim(' ', '#', '(', ')');
            var hexCoded = splitLine.Last();
            var dist = Convert.ToInt32(hexCoded[..5], 16);
            var dir = hexCoded[^1] switch
            {
                '0' => Direction.East,
                '1' => Direction.South,
                '2' => Direction.West,
                '3' => Direction.North,
                _ => throw new ArgumentException("Not valid!")
            };
            return new Instruction(dir, dist);
        }
    }
}
