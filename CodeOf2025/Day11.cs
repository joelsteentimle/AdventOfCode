namespace AoC2025;

public class Day11
{
    // private int[] devices;
    private Dictionary<string, int> deviceNamesNumbers =[];
    private int[][] devicesConnectedTo;
    private int youIndex;
    private int outIndex;
    private int fftIndex;
    private int dacIndex;
    private int svrIndex;


    public Day11(List<string> allData)
    {
        var deviceNames = allData.Select(r => r.Split(':')[0])
            .ToArray();

        for (var i = 0; i < deviceNames.Length; i++)
        {
            deviceNamesNumbers[deviceNames[i]] = i;
            if(deviceNames[i] == "you") youIndex = i;
            if(deviceNames[i] == "fft") fftIndex = i;
            if(deviceNames[i] == "dac") dacIndex = i;
            if(deviceNames[i] == "svr") svrIndex = i;
        }

        outIndex = deviceNames.Length;
        deviceNamesNumbers["out"] = outIndex;


       devicesConnectedTo = allData
            .Select(r => r.Split(':')[1]
                .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .Select(dn => deviceNamesNumbers[dn])
                .ToArray()
            )
            .ToArray();
    }


    private long CountValidPaths(Func<int[], bool> validPath, int startIndex)
    {
        int[] startPath = [startIndex];
        Dictionary<int, List<int[]>> pathsByLength = [];
        pathsByLength[0] = [startPath];

        var pathsToEnd = 0;

        for (var length = 0; length < 600; length++)
        {
            if (!pathsByLength.TryGetValue(length, out var currentPaths)
                || currentPaths.Count == 0)
                return pathsToEnd;

            var nextLengthPaths = new List<int[]>();

            foreach (var path in currentPaths)
            {
                if (validPath(path))
                {
                    pathsToEnd++;
                    continue;
                }

                if (devicesConnectedTo.Length <= path.Last())
                    continue;

                foreach (var nextDevice in devicesConnectedTo[path.Last()])
                {
                    if (path.Contains(nextDevice))
                        continue;

                    var nextPath = new int[path.Length + 1];

                    Array.Copy(path, nextPath, path.Length);
                    nextPath[^1] = nextDevice;

                    nextLengthPaths.Add(nextPath);

                }
            }
            pathsByLength[length + 1] = nextLengthPaths;
        }

        return pathsToEnd;
    }

    private bool ValidPathPart1(int[] path) => path.Last() == outIndex;

    public long Part1() => CountValidPaths(ValidPathPart1, youIndex);

    private bool ValidPathPart2(int[] path) =>
        path.Contains(fftIndex) && path.Contains(dacIndex) && path.Last() == outIndex;

    public long Part2()=> CountValidPaths(ValidPathPart2, svrIndex);

}
