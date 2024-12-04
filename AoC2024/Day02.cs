namespace AoC2024;

public class Day02
{
    public Day02(List<string> lines, int tolerance)
    {
        foreach (var line in lines)
        {
            BreakToOuter:
            var stairs = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            int? previous =null;
            var sign = Math.Sign(stairs[1] - stairs[0]);
            var dangerous = 0;

            for (var index = 0; index < stairs.Count; index++)
            {
                var stair = stairs[index];

                if (index > 0 && previous != null)
                {
                    var abs = Math.Abs(stair - previous.Value);
                    var localSign = Math.Sign(stair - previous.Value);

                    if (abs > 3 || abs < 1 || localSign != sign)
                    {
                        dangerous++;

                        if (index == 1)
                        {
                            sign = Math.Sign(stairs[2] - stairs[1]);
                            previous = null;
                        }
                    }
                    else
                    {
                        previous = stair;
                    }
                }
                else
                {
                    previous = stair;
                }
            }

            if(dangerous>tolerance)
            {
                Unsafe.Add(stairs);
            }else
            {
                Safe.Add(stairs);
            }
        }
    }

    public List<List<int>> Safe { get;  } = new();
    public List<List<int>> Unsafe { get; } = new();
}
