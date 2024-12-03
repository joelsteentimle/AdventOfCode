namespace AoC2024;

public class Day02
{
    public Day02(List<string> lines, int tolerance)
    {
        foreach (var line in lines)
        {
            BreakToOuter:
            var stairs = line.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            var previous = stairs[0];
            var sign = Math.Sign(stairs[1] - stairs[0]);
            var dangerous = 0;

            foreach (var stair in stairs[1..])
            {
                var abs = Math.Abs(stair - previous);
                var localSign = Math.Sign(stair - previous);

                if (abs > 3 || abs < 1 ||  localSign != sign)
                {
                    dangerous++;
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
