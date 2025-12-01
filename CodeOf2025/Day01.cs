namespace AoC2025;

public class Day01
{
    private int timesAtZero = 0;
    private int timesPassingZero = 0;

    public Day01(List<string> data)
    {
        var position = 50;
        foreach (var line in data)
        {
            var wasZero = position == 0;
            switch (line[0])
            {
                case 'R':
                    position += int.Parse(line[1..]);
                    break;
                case 'L':
                    position -= int.Parse(line[1..]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("Not supposed to happen.");
            }

            if (position >= 100)
                timesPassingZero += position /100;
            else if(position <= 0 )
            {
                var timesPassed = position / 100;
                timesPassingZero += Math.Abs(timesPassed);

                timesPassingZero += wasZero ? 0 : 1;
                position += 100 * (Math.Abs( timesPassed)+1);
            }

            position %= 100;

            if (position == 0)
                timesAtZero++;
        }
    }

    public int Part1() => timesAtZero;

    public int Part2() => timesPassingZero;
}
