using AdventLibrary;

namespace AoC2025;

public class Day24
{
    private enum LogicFunction
    {
        AND,
        OR,
        XOR
    }

    private class Node
    {
        private (string gate, bool? value) input1;
        private (string gate, bool? value) input2;

        private LogicFunction LogOp;
        public readonly string outputWire;

        public Node(string gate1, string gate2, LogicFunction op, string outputWire)
        {
            input1 = (gate1, null);
            input2 = (gate2, null);
            LogOp = op;
            this.outputWire = outputWire;
        }

        public bool? OutputValue() {
            if (input1.value is null || input2.value is null)
                return null;

            return LogOp switch
            {
                LogicFunction.AND => input1.value.Value && input2.value.Value,
                LogicFunction.OR => input1.value.Value || input2.value.Value,
                LogicFunction.XOR => input1.value.Value ^ input2.value.Value,
            };
        }

        public void SetInput(string wire, bool value)
        {
            if(input1.gate == wire)
                input1.value = value;
            if(input2.gate == wire)
                input2.value = value;
        }
    }

    private List<(string wire, bool value)> Inputs = [];
    private Dictionary<string, HashSet<Node>> nodesDependingOnWire = [];
    private List<Node> ZWires = [];
    private List<Node> Nodes = [];

    public Day24(List<string> allData)
    {
        var inputRows = allData.TakeWhile(row => !string.IsNullOrWhiteSpace(row));
        var connectionRows = allData.SkipWhile(row => !string.IsNullOrWhiteSpace(row))
            .ToList()[1..];

        Inputs = inputRows.Select(
            row =>
                (row.Split(':', StringSplitOptions.RemoveEmptyEntries)[0],
                    row.Split(':', StringSplitOptions.RemoveEmptyEntries)[1].Trim() == "1")
        ).ToList();

        Nodes = connectionRows.Select(ParseNode).ToList();
    }

    private Node ParseNode(string nodeString)
    {
        var tokens = nodeString.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var input1 = tokens[0];
        var input2 = tokens[2];
        var nodeOp = tokens[1] switch
        {
            "XOR" => LogicFunction.XOR,
            "AND" => LogicFunction.AND,
            "OR" => LogicFunction.OR,
        };
        var outputWire = tokens[4];


        var node = new Node(input1, input2, nodeOp, outputWire);
        nodesDependingOnWire.AddOrCreate(input1, node);
        nodesDependingOnWire.AddOrCreate(input2, node);
        if(outputWire[0] == 'z')
            ZWires.Add(node);

        return node;
    }

    public long Part1()
    {
        foreach (var (wire, value) in Inputs)
        {
            ApplyInputs(wire, value);
        }

        return GetZOutputValue();
    }

    private void ApplyInputs(string wire, bool value)
    {
        if( nodesDependingOnWire.TryGetValue(wire, out var affectedNodes))
            foreach (var node in affectedNodes)
            {
                node.SetInput(wire, value);
                if (node.OutputValue() is { } outputValue)
                    ApplyInputs(node.outputWire, outputValue);
            }
    }

    private long GetZOutputValue()
    {
        long value = 0;

        foreach (var zNode in ZWires)
        {
            if (zNode.OutputValue().Value)
                value += Convert.ToInt64(Math.Pow(2, int.Parse(zNode.outputWire[1..])));
        }

        return value;
    }

    public long Part2()
    {

        return 0;
        ;
    }
}
