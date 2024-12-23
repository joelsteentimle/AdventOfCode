using System.Threading.Tasks.Dataflow;

namespace AoC2024;

public class Day15
{
    private enum FindEntry
    {
        floor =0,
        box,
        wall,
    }

    private (int y, int x) RobotPosition = (-1, -1);

    private (int dy, int dx) ToDirection(char i) => i switch
    {
        '^' => (-1, 0),
        'v' => (1, 0),
        '<' => (0, -1),
        '>' => (0, 1),
    };

    private FindEntry[,] Field;
    private int MaxY;
    private readonly int MaxX;
    private readonly IEnumerable<string> InstructionList;
    private int widthMultiplier = 1;

    public Day15(List<string> allData, int width = 1)
    {
        var inputField = allData.TakeWhile(l => !string.IsNullOrWhiteSpace(l)).ToArray();
        InstructionList = allData.SkipWhile(l => !string.IsNullOrWhiteSpace(l)).Skip(1);
        widthMultiplier = width;

        MaxY = inputField.Length;
        MaxX = inputField[0].Length * widthMultiplier;

        Field = new FindEntry[MaxY, MaxX];

        for (var y = 0; y < MaxY; y++)
        {
            for (var x = 0; x < MaxX / widthMultiplier; x++)
            {
                if (inputField[y][x] == '@')
                {
                    RobotPosition = (y, x * widthMultiplier);
                }
                else
                {

                    Field[y, x * widthMultiplier] = inputField[y][x] switch
                    {
                        '#' => FindEntry.wall,
                        '.' => FindEntry.floor,
                        'O' => FindEntry.box
                    };
                }
            }
        }
    }

    public void RobotMovePart1((int dy, int dx) direction)
    {

        switch (Field[RobotPosition.y + direction.dy, RobotPosition.x + direction.dx])
        {
            case FindEntry.wall:
                break;
            case FindEntry.floor:
                RobotPosition = (RobotPosition.y + direction.dy, RobotPosition.x + direction.dx);
                break;
            case FindEntry.box:
                var newPossibleStonePosition =
                    NextFloorAfterStones((RobotPosition.y + direction.dy, RobotPosition.x + direction.dx),
                        direction);
                if (newPossibleStonePosition is { } floor)
                {
                    Field[floor.y, floor.x] = FindEntry.box;
                    Field[RobotPosition.y + direction.dy, RobotPosition.x + direction.dx] = FindEntry.floor;
                    RobotPosition = (RobotPosition.y + direction.dy, RobotPosition.x + direction.dx);
                }

                break;
        }
    }

    public void RobotMovePart2((int dy, int dx) direction)
    {
        if (direction.dy == 0)
        {
            var thingsToMoveTo = Field[RobotPosition.y, RobotPosition.x + widthMultiplier * direction.dx];
            if (thingsToMoveTo is FindEntry.floor)
            {
                RobotPosition = (RobotPosition.y, RobotPosition.x + direction.dx);
            }
            else if (thingsToMoveTo is FindEntry.box)
            {
                if (TryToMoveBoxInX((RobotPosition.y, RobotPosition.x + widthMultiplier * direction.dx), direction))
                {
                    RobotPosition = (RobotPosition.y, RobotPosition.x + direction.dx);
                }
            }
            else if (thingsToMoveTo is FindEntry.wall
                     && Field[RobotPosition.y, RobotPosition.x + direction.dx] == FindEntry.floor)
            {
                RobotPosition = (RobotPosition.y, RobotPosition.x + direction.dx);
            }
        }
        else { }

    }

    private bool TryToMoveBoxInX((int y, int) box, (int dy, int dx) direction)
    {

        if(CanMoveAllBoxes(box,direction))

        var thingsToMoveTo = Field[RobotPosition.y, RobotPosition.x + widthMultiplier * direction.dx];
        // if(thingsToMoveTo is FindEntry.floor)

            //TODO: Fix this
            return false;
    }

    private bool CanMoveAllBoxes((int y, int x) box, (int y, int x) direction)
    {
        (int nbY, int nbX) newBoxPosition = (box.y + direction.y, box.x + direction.x);

        List<(int y ,int x)> stonesToMove = [];

        // var boxToLeft = (nby, nb
        // if()

        return false;
    }



    private (int y, int x)? NextFloorAfterStones((int y, int x) posiiton, (int dy, int dx) directionDy)
    {
        while (!IsOutOfBound(posiiton))
        {
            if (Field[posiiton.y, posiiton.x] == FindEntry.floor)
                return posiiton;
            if (Field[posiiton.y, posiiton.x] == FindEntry.wall)
                return null;

            // stone
            posiiton = (posiiton.y + directionDy.dy, posiiton.x + directionDy.dx);

        }

        return null;
    }


    public long Part1(int seconds)
    {
        foreach (var row in InstructionList)
        {
            foreach (var instruction in row)
            {
                RobotMovePart1(ToDirection(instruction));
            }
        }

        var sum = 0L;

        for (var y = 0; y < MaxY; y++)
        for (var x = 0; x < MaxX; x++)
            if (Field[y, x] == FindEntry.box)
                sum += y * 100 + x;


        return sum;
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


    public long Part2()
    {
        var sum = 0L;


        return -100;
    }

}
