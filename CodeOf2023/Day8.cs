using Microsoft.VisualBasic;

namespace AoC2023;

public class Day8
{
    public Day8(string[] input)
    {
        MoveInstructions = input.First().ToCharArray();
        foreach (var line in input[2 ..])
        {
            var node = new DesertNode(line, Nodes);
            Nodes[node.Name] = node;
        }
    }

    public int StepsToNode(string endNodeName)
    {
        List<DesertNode> startNodes = [ Nodes["AAA"]];
        
        return Steps((n)=>n==endNodeName , startNodes);
    }
    
    public int MultiStepsTo(Func<string,bool> startNodeCondition,Func<string,bool> endNodeCondition)
    {
        List<DesertNode> startNodes = Nodes.Values.Where(n => startNodeCondition(n.Name)).ToList();

        return Steps(endNodeCondition, startNodes);
        
        /* For each "ghost":
        * save start.
         * run and note all goal-points
         * stop when back at a previously visited node.
         * store - then walk from start, adding the smallest largest step until next goal
         * move along for all ghosts.
         * stop when all ghosts are on a goal node.
        */
    }

    private int Steps(Func<string,bool> endCondition, IList<DesertNode> currentNode)
    {
        int ip = 0;
        int steps = 0;

        while (currentNode.Any(n=>!endCondition(n.Name)))
        {
            var currentInstruction = MoveInstructions[ip];
            if (currentInstruction == 'L')
            {
                for (int i = 0; i < currentNode.Count; i++)
                {
                    currentNode[i] = currentNode[i].Left.Value;
                }
            }
            else if (currentInstruction == 'R')
            {
                for (int i = 0; i < currentNode.Count; i++)
                {
                    currentNode[i] = currentNode[i].Right.Value;
                }
            }
            ip = ++ip >= MoveInstructions.Length ? 0 : ip;
            steps++;
        }

        return steps;
    }
    
    public Dictionary<string, DesertNode> Nodes { get; } = [];
    public char[] MoveInstructions { get; set; }

    public struct DesertNode
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