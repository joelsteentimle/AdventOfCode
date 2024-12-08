namespace AoC2024;

public class Day08
{
    private Dictionary<char, List<(int y, int x)>> antennas = [];
    private HashSet<(int y, int x)> antinodes = [];
    private int MaxY;
    private int MaxX;
    private bool ResFix;

    public Day08(List<string> input)
    {
        MaxY = input.Count;
        MaxX = input[0].Length;

        for (int y = 0; y < input.Count; y++)
        {

            var line = input[y];
            for (int x = 0; x < line.Length; x++)
            {
                var maybeeAntenna = line[x];
                if (maybeeAntenna != '.')
                {
                    if (antennas.TryGetValue(maybeeAntenna, out var locations))
                    {
                        locations.Add((y, x));
                    }
                    else
                    {
                        antennas[maybeeAntenna] = [(y, x)];
                    }
                }
            }
        }
    }

    public int CountAntiNodes(bool resoning = false)
    {
        ResFix = resoning;
        foreach (var  antennaPositions in antennas.Values.Where(a => a.Count > 1))
        {
            for (int i = 0; i < antennaPositions.Count; i++)
            {
                foreach (var otherPosition in antennaPositions[(i+1)..])
                {
                  var antiPositions =   GetAntiNodes(antennaPositions[i], otherPosition);
                  foreach (var antiPosition in antiPositions)
                  {
                      antinodes.Add(antiPosition);
                  }
                }
            }
        }

        return antinodes.Count;
    }

    private List<(int y , int x)> GetAntiNodes((int y, int x) position, (int y, int x) otherPosition)
    {
        List<(int y, int x)> ret = [];
        (int y, int x) diff = (position.y - otherPosition.y, position.x - otherPosition.x);

        if (!ResFix)
        {
            // A(1,1) - B(0,0) = B -> A
            var oneAntinode = (otherPosition.y - diff.y, otherPosition.x - diff.x);
            var othenAntiNode = (position.y + diff.y, position.x + diff.x);
            if (!IsOutOfBound(oneAntinode))
                ret.Add(oneAntinode);
            if (!IsOutOfBound(othenAntiNode))
                ret.Add(othenAntiNode);
        }
        else
        {
            var repeats = 1;
             (int y,int x) testPos = (position.y, position.x);
            while (!IsOutOfBound(testPos))
            {
                ret.Add(testPos);
                testPos = (testPos.y - diff.y, testPos.x - diff.x);
            }

            testPos = (position.y, position.x);
            while (!IsOutOfBound(testPos))
            {
                ret.Add(testPos);
                testPos = (testPos.y + diff.y, testPos.x + diff.x);
            }
        }


        return ret;
    }

    private bool IsOutOfBound((int, int ) position)
    {
        var (y, x) = position;
        if (y < 0 || y >= MaxY)
            return true;
        if (x < 0 || x >= MaxX)
            return true;
        return false;
    }
}
