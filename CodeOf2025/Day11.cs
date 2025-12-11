namespace AoC2025;

public class Day11
{
    // private readonly Dictionary<(long number, long nrBlinkings), long> MemoryOfNumbers = [];
    // private readonly List<long> numbers = [];
    // private readonly Dictionary<long, (long, long?)> stepping = [];

    private int[] devices;
    private Dictionary<string, int> deviceNamesNumbers =[];
    private int[][] devicesConnectedTo;
    private int youIndex;
    private int outIndex;

    public Day11(List<string> allData)
    {
        var deviceNames = allData.Select(r => r.Split(':')[0])
            .ToArray();

        devices = new int[deviceNames.Length];
        devicesConnectedTo = new int[deviceNames.Length][];

        for (var i = 0; i < deviceNames.Length; i++)
        {
            deviceNamesNumbers[deviceNames[i]] = i;
            if(deviceNames[i] == "you") youIndex = i;
            if(deviceNames[i] == "output") outIndex = i;
        }

        devicesConnectedTo = allData
            .Select(r => r.Split(':')[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(dn => deviceNamesNumbers[dn])
                .ToArray()
            )
            .ToArray();
    }

    public long Part1()
    {
        int[] startPath = [youIndex];
        Dictionary<int, int[]> pathsByLength = [];
        pathsByLength[0] = startPath;

        return 0;
    }

    public long Part2()
    {
        return 0;
    }

}
