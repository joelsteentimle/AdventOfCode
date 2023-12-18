using System.Diagnostics;
using AoC2023.Graph;

namespace AoC2023;

public class Day18(List<string> lines)
{
    // public IList<string> TextGroups { get; } = lines.First().SplitAndTrim(',');
    public List<Instruction> Insturtctions { get; } = CalculateInstructions(lines);
    public LagoonPoint?[,] LavaMap { get; private set; } = new LagoonPoint[0, 0];
    public Position StartPoint { get; private set; } = new(0, 0);
    public bool RightIsIn { get; private set; }

    private static List<Instruction> CalculateInstructions(List<string> list) =>
        list.Select(l => new Instruction(l)).ToList();

    public void GenerateFieldAndStart()
    {
        var position = new Position(0, 0);
        var xMin = 0;
        var xMax = 0;
        var yMin = 0;
        var yMax = 0;

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

        LavaMap = new LagoonPoint[xMax - xMin + 1, yMax - yMin + 1];
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

    public Direction LookInside(Direction lastMove) =>
        (Direction) ((int)(lastMove + (RightIsIn ? 1 : 3)) %4 );

    public int TrenchVolume() => SumUp((lp) => lp?.Trench is true);
    public int PoolVolume() => SumUp((lp) => lp?.Pool is true);

    private int SumUp(Func<LagoonPoint?, bool> countPoint)
    {
        var digged = 0;

        for (var x = 0; x < LavaMap.GetLength(0); x++)
        for (var y = 0; y < LavaMap.GetLength(1); y++)
            if (countPoint(LavaMap[x, y]) )
                digged++;

        return digged;
    }

    public record LagoonPoint(bool Trench, bool Pool);

    public class Instruction
    {
        public Direction Direction { get; }
        public int Distance { get; }
        // public (int R, int G, int B) colour { get; }

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
    }
}
