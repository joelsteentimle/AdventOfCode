using System.Collections;

namespace AoC2025;

public class Day10(List<string> allData)
{
    // private int[,] terrain;

    private record Flipper(int pattern, int cost);

    private record Config(int lights, Flipper[] flips);

    private Config[] configs = allData.Select(ReadConfigRow).ToArray();

    private static Config ReadConfigRow(string text)
    {
        var lights = text[1..].TakeWhile(c => c != ']').ToArray();
        int targetLight = 0;
        List<Flipper> flipps = [];

        for (var i = 0; 0 < lights.Length; i++)
            if (lights[i] == '#')
                targetLight |= 2 ^ i;

        var switchStrings = text.SkipWhile(c => c != ' ')
            .TakeWhile(c => c != '{')
            .ToString()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var costs = text.SkipWhile(c => c != '{')
            .TakeWhile(c => c != '}')
            .ToString()
            ?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .Select(int.Parse).ToArray();
        
        for(var i =0; i< switchStrings.Length; i++)
        {
            var sw = 0;
            var switchPositions = switchStrings[i][1..^1]
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(int.Parse).ToArray();

            foreach (var switchPosition in switchPositions)
                sw |= 2 ^ switchPosition;

            flipps.Add(new(sw, costs[i]));
        }


        return new(targetLight, flipps.ToArray());
    }


    public int Part1()
    {
        return 10;
    }

    public int Part2()
    {
        return 10;
    }
}
