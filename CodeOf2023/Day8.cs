using Microsoft.VisualBasic;

namespace AoC2023;

public class Day8
{
    public Day8(string[] input)
    {
        MoveInstructions = input.First().ToCharArray();
        foreach (var line in input[2 ..])
        {
            var node = new DesertNode(line);
            Nodes[node.Name] = node;
        }
    }

    public Dictionary<string, DesertNode> Nodes { get; } = [];

    public char[] MoveInstructions { get; set; }
    public struct DesertNode
    {
        public string Right { get; }
        public string Name { get; }
        public string Left { get; }

        public DesertNode(string line)
        {
            Name = line[..3];
            Left = line[7..10];
            Right = line[12..15];
        }
    }
}

