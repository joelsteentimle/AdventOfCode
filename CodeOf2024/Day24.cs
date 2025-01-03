namespace AoC2024;

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
        public readonly string exitWire;

        public Node(string gate1, string gate2, LogicFunction op, string exitWire)
        {
            input1 = (gate1, null);
            input2 = (gate2, null);
            LogOp = op;
            this.exitWire = exitWire;
        }

        public bool? Value() {
            if (input1.value is null || input2.value is null)
                return null;

            return LogOp switch
            {
                LogicFunction.AND => input1.value.Value && input2.value.Value,
                LogicFunction.OR => input1.value.Value || input2.value.Value,
                LogicFunction.XOR => input1.value.Value ^ input2.value.Value,
            };
        }
    }

    private Dictionary<string, List<Node>> getsOnWire = [];
    private List<Node> ZWires = [];


    public Day24(List<string> allData)
    {
        var inputs = allData

    }

    public long Part1()
    {
        return 0L;
    }

    public long Part2()
    {

        return 0;
        ;
    }
}
