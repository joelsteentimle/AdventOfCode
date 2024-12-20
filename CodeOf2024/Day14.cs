using System.Diagnostics;

namespace AoC2024;

public class Day14
{
    private char[,] Field;
    private int MaxY;
    private readonly int MaxX;
    public List<Robot> Robots =[];
    private List<string> inData;

    public class Robot(
        (int y, int x) Position,
        (int dy, int dx) Velocity)
    {
        public (int y, int x) Position { get; set; } = Position;
        public (int dy, int dx) Velocity { get; } = Velocity;
    }

    public Day14(List<string> allData, (int y, int x)? inputFieldSize = null)
    {
        var fieldSize = inputFieldSize ?? (103, 101);
        MaxY = fieldSize.y;
        MaxX = fieldSize.x;

        inData = allData;

        ResetBoard(inData);
    }

    private void ResetBoard(List<string> allData)
    {
        foreach (var robot in allData)
        {
            var input  = robot.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var pos = input[0][2..].Split(',', StringSplitOptions.RemoveEmptyEntries);
            var posX = int.Parse(pos[0]);
            var posY = int.Parse(pos[1]);

            var dir = input[1][2..].Split(',', StringSplitOptions.RemoveEmptyEntries);
            var dirX = int.Parse(dir[0]);
            var dirY = int.Parse(dir[1]);

            Robots.Add(new Robot((posY, posX), (dirY, dirX)));
        }
    }

    public long Part1(int seconds)
    {
        WaitSeconds(seconds);

        // var sum = 0L;

        //ymin xmin quad
        var sum = PartOneCalculate();

        return sum;
    }

    private int PartOneCalculate()
    {
        var tlQuad = Robots.Count(r => r.Position.y < MaxY / 2 && r.Position.x < MaxX / 2);
        var sum = tlQuad;

        //ymin xmax quad
        var  trQuad = Robots.Count(r => r.Position.y < MaxY / 2 && r.Position.x >  (MaxX / 2));
        sum *= trQuad;

        //ymax xmin quad
        var blQuad = Robots.Count(r => r.Position.y >  (MaxY / 2) && r.Position.x < MaxX / 2);
        sum *= blQuad;

        //ymax xmax quad
        var brQuad = Robots.Count(r => r.Position.y > (MaxY / 2) && r.Position.x >  (MaxX / 2));
        sum *= brQuad;
        return sum;
    }


    public void PrintTreeRobots()
    {
        // var field = new char[MaxY, MaxX];
        var charField = new char[MaxY][];

        var textPrint = new List<string>();

        for (int y = 0; y < MaxY; y++)
        {
            charField[y]  = Enumerable.Repeat('.', MaxX).ToArray();
            // textPrint.Add(Enumerable.Repeat('.', MaxX));
        }

        // for(var y=0; y< field.GetLength(0); y++)
        // for(var x=0;  x < field.GetLength(1) ;x++)
        // {
        //     field[y, x] = '.';
        // }

        foreach (var ro in Robots)
        {
            charField[ro.Position.y][ro.Position.x] = 'X';
            // textPrint[ro.Position.y]. [ro.Position.x] = 'R';
            // field[ro.Position.y, ro.Position.x] = 'X';
        }

        foreach (var line in charField)
        {
            Debug.WriteLine(line.ToString());
        }

    }

    public bool IsChristmasTree()
    {
        var robotCountPerLine = new int[MaxY];

        foreach (var robot in Robots)
        {
            var y = robot.Position.y;
            var x = robot.Position.x;

            var distanceFromMiddle = Math.Abs(x - MaxX / 2);
            var distanceFromTop = y;

            if(distanceFromMiddle > distanceFromTop)
                return false;

        }

        return true;

        foreach (var robot in Robots)
        {
            if (robot.Position.x != MaxX/2)
                robotCountPerLine[robot.Position.y]++;
        }

        for (int i = 1; i < MaxY; i++)
        {
            if (
                robotCountPerLine[i] > 0 &&
                robotCountPerLine[i] < robotCountPerLine[i - 1])
                return false;
        }
        return true;
    }


    public long Part2()
    {
        int timWentBy = 0;
        List<(int value, int second)> timeScores = [];

        for (int i = 0; i < MaxY * MaxX; i++)
        {
            WaitSeconds(1);
            var value = PartOneCalculate();

            timeScores.Add((value, i));
        }

        var bestTimes =
            timeScores.OrderBy(t => t.value)
                .Take(50);

        foreach (var time in bestTimes)
        {
            ResetBoard(inData);
            WaitSeconds(time.second);
            Debug.WriteLine("");
            Debug.WriteLine("");
            Debug.WriteLine("");
            Debug.WriteLine($"This is for second{time.second}");
            PrintTreeRobots();
        }


        // while (timWentBy < 400000)
        // while (timWentBy < 10000)
        // {
        //
        //     WaitSeconds(1);
        //     timWentBy++;
        //
        //     if(IsChristmasTree())
        //         return timWentBy;
        // }

        return bestTimes.First().second;
    }

    public void WaitSeconds(int seconds)
    {
        foreach (var robot in Robots)
        {
            (int newY, int newX) = (
                (robot.Position.y + robot.Velocity.dy * seconds) % MaxY,
                (robot.Position.x + robot.Velocity.dx * seconds) % MaxX
                );

            var finalpos = (
                newY < 0 ? newY + MaxY : newY,
                newX < 0 ? newX + MaxX : newX
            );

            robot.Position = finalpos;
        }
    }
}
