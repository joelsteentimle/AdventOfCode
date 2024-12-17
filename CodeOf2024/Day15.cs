namespace AoC2024;

public class Day14
{
    private char[,] Field;
    private int MaxY;
    private readonly int MaxX;
    public List<Robot> Robots =[];

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


    public bool IsChristmasTree()
    {
        var robotCountPerLine = new int[MaxY];

        foreach (var robot in Robots)
        {
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

        while (timWentBy < 4000000)
        {
            WaitSeconds(1);
            timWentBy++;

            if(IsChristmasTree())
                return timWentBy;
        }

        return -100;
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
