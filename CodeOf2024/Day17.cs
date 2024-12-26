using System.Data;

namespace AoC2024;

public class Day17
{
    private readonly List<string> availablePatterns;
    private readonly List<string> desiredPatterns;

    private long A;
    private long B;
    private long C;

    private List<long> Instructions;
    private int Ip;
    private List<long> Output=[];

    public Day17(List<string> allData)
    {
       A =long.Parse(allData[0].Split(':', StringSplitOptions.RemoveEmptyEntries)[1]);
       B =long.Parse(allData[1].Split(':', StringSplitOptions.RemoveEmptyEntries)[1]);
       C =long.Parse(allData[2].Split(':', StringSplitOptions.RemoveEmptyEntries)[1]);

        Instructions = allData[4].Split(':', StringSplitOptions.RemoveEmptyEntries)
            [1].Split(",", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse).ToList();
    }

    public string Part1()
    {
        while(Ip+1 < Instructions.Count)
        {
          if(  Eval(Instructions[Ip]))
            Ip += 2;
        }
        return string.Join(",", Output);
    }

    private long Combo() =>
        Instructions[Ip+1] switch
        {
            >= 0 and < 4 => Instructions[Ip+1],
            4 => A,
            5 => B,
            6 => C
        };

    private long Literal() => Instructions[Ip + 1];

    private bool Eval(long instruction)
    {
        switch (instruction)
        {
            case 0:
                A = A / SomPower();
                break;
            case 1:
                B = B ^ Instructions[Ip + 1];
                break;
            case 2:
                B = Combo() % 8;
                break;
            case 3:
                if (A != 0)
                {
                    Ip = Convert.ToInt32(Literal());
                    return false;
                }
                return true;
            case 4:
                B = B ^ C;
                break;
            case 5:
                Output.AddRange(Combo() % 8);
                break;
            case 6:
                B = A / SomPower();
                break;
            case 7:
                C = A / SomPower();
                break;
        }

        return true;
    }

    private long SomPower()
    {
        var value = Math.Pow(2, Combo());
        Console.WriteLine(value);
        return Convert.ToInt64(value);
    }

    public long Part2() => 2;
}
