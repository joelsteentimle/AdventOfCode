namespace AoC2023;

public class Day06
{
    public List<(long time, long distance)> Races { get; }

    public Day06(IList<string> input, bool isReal = false)
    {
        if (isReal)
            input = input.Select(s => s.Replace(" ", "")).ToList();

        var times = input.First()["Time:".Length..]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        var distances = input[1]["Distance:".Length..]
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        Races = times.Zip(distances, (t, d) => (t.ToInt64(), d.ToInt64())).ToList();
    }

    public static int CalculateRace((long time, long distance) race)
    {
        var winnings = 0;
        for (long speed = 0; speed < race.time; speed++)
            if (CalculateDistance(race.time, speed) > race.distance)
                winnings++;

        return winnings;
    }

    private static long CalculateDistance(long totalTime, long speed) => speed * (totalTime - speed);

    public int TotalRaceWinnings() => Races.Aggregate(1, (current, race) => current * CalculateRace(race));
}
