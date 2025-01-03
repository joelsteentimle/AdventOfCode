﻿
using Open.Numeric.Primes;

namespace AoC2023;
public class Day08
{
    public Day08(List<string> input)
    {
        MoveInstructions = input.First().ToCharArray();
        foreach (var line in input[2..])
        {
            var node = new DesertNode(line, Nodes);
            Nodes[node.Name] = node;
        }
    }

    public int StepsToNode(string endNodeName) => MultiStepsTo(n => n == "AAA", n => n == endNodeName);

    public long JustToZ(Func<string, bool> startNodeCondition, Func<string, bool> endNodeCondition)
    {
        var startNodes = Nodes.Values.Where(n => startNodeCondition(n.Name)).ToList();
        Dictionary<long, int> totalFactors = [];
        foreach (var startNode in startNodes)
        {
            var steps = Steps(endNodeCondition, [startNode]);

            var factors = Prime.Factors(steps).Order().ToList();

            for (var i = 0; i < factors.Count; i++)
            {
                var fac = factors[i];
                var count = factors.Count(f => f == fac);
                totalFactors.SetToSelectedValue(fac, count, Math.Max);
            }
        }

        var repFact = new List<long>();

        foreach (var key in totalFactors.Keys)
            repFact.AddRange(Enumerable.Repeat(key, totalFactors[key]));

        return repFact.Aggregate((i, j) => i * j);


        // return Steps(endNodeCondition, startNodes);
    }

    public int MultiStepsTo(Func<string, bool> startNodeCondition, Func<string, bool> endNodeCondition)
    {
        var startNodes = Nodes.Values.Where(n => startNodeCondition(n.Name)).ToList();

        return Steps(endNodeCondition, startNodes);
    }

    public List<(int visited, int loop)> FindLoops(Func<string, bool> startNodeCondition)
    {
        var startNodes = Nodes.Values.Where(n => startNodeCondition(n.Name)).ToList();
        var loopLengths = new List<(int, int)>(startNodes.Count);

        foreach (var startNode in startNodes)
        {
            var visited = new List<(DesertNode, int)>();
            var current = startNode;
            // var steps = 0;
            var ip = 0;
            while (!visited.Contains((current, ip)))
            {
                visited.Add((current, ip));
                current = SingleStep(MoveInstructions[ip], current);
                ip = ++ip >= MoveInstructions.Length ? 0 : ip;
                // steps++;
            } // while (current.Name != startNode.Name);

            ip = --ip < 0 ? MoveInstructions.Length - 1 : ip;
            var loopLength = visited.Count - visited.IndexOf((current, ip));

            loopLengths.Add((visited.Count, loopLength));
        }

        return loopLengths;
    }

    private int Steps(Func<string, bool> endCondition, List<DesertNode> currentNode)
    {
        var ip = 0;
        var steps = 0;

        while (currentNode.Any(n => !endCondition(n.Name)))
        {
            var currentInstruction = MoveInstructions[ip];

            for (var i = 0; i < currentNode.Count; i++)
                currentNode[i] = SingleStep(currentInstruction, currentNode[i]);

            ip = ++ip >= MoveInstructions.Length ? 0 : ip;
            steps++;
        }

        return steps;
    }

    private static DesertNode SingleStep(char currentInstruction, DesertNode here) => currentInstruction switch
    {
        'L' => here.Left.Value,
        _ => here.Right.Value
    };

    public Dictionary<string, DesertNode> Nodes { get; } = [];
    public char[] MoveInstructions { get; }

    public readonly struct DesertNode
    {
        public string Name { get; }
        public Lazy<DesertNode> Right { get; }
        public Lazy<DesertNode> Left { get; }

        public DesertNode(string line, Dictionary<string, DesertNode> nodes)
        {
            Name = line[..3];
            var leftName = line[7..10];
            var rightName = line[12..15];
            Right = new Lazy<DesertNode>(() => nodes[rightName]);
            Left = new Lazy<DesertNode>(() => nodes[leftName]);
        }
    }
}
