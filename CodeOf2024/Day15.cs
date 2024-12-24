using System.Diagnostics;
using System.Text;

namespace AoC2024;

public class Day15
{
    private enum FieldEntry
    {
        floor =0,
        box,
        Wall,
    }

    private (int y, int x) RobotPosition = (-1, -1);

    private (int dy, int dx) ToDirection(char i) => i switch
    {
        '^' => (-1, 0),
        'v' => (1, 0),
        '<' => (0, -1),
        '>' => (0, 1),
    };

    private FieldEntry[,] Field;
    private int MaxY;
    private readonly int MaxX;
    private readonly IEnumerable<string> InstructionList;
    private int widthMultiplier = 1;

    public Day15(List<string> allData, int widthMultiplier = 1)
    {
        var inputField = allData.TakeWhile(l => !string.IsNullOrWhiteSpace(l)).ToArray();
        InstructionList = allData.SkipWhile(l => !string.IsNullOrWhiteSpace(l)).Skip(1);
        this.widthMultiplier = widthMultiplier;

        MaxY = inputField.Length;
        MaxX = inputField[0].Length * this.widthMultiplier;

        Field = new FieldEntry[MaxY, MaxX];

        for (var y = 0; y < MaxY; y++)
        {
            for (var x = 0; x < MaxX / this.widthMultiplier; x++)
            {
                if (inputField[y][x] == '@')
                {
                    RobotPosition = (y, x * this.widthMultiplier);
                }
                else
                {

                    if (widthMultiplier == 1)
                    {
                        switch (inputField[y][x])
                        {
                            case '.':
                                Field[y, x * this.widthMultiplier] = FieldEntry.floor;
                                break;
                            case '#':
                                Field[y, x * this.widthMultiplier] = FieldEntry.Wall;
                                break;
                            case 'O':
                                Field[y, x * this.widthMultiplier] = FieldEntry.box;
                                break;
                        }
                    }
                    else
                    {
                        switch (inputField[y][x])
                        {
                            case '.':
                                Field[y, x * this.widthMultiplier] = FieldEntry.floor;
                                break;
                            case '#':
                                Field[y, x * this.widthMultiplier] = FieldEntry.Wall;
                                Field[y, x * this.widthMultiplier+1] = FieldEntry.Wall;
                                break;
                            case 'O':
                                Field[y, x * this.widthMultiplier] = FieldEntry.box;
                                break;
                        }

                    }
                }
            }
        }
    }

    public void RobotMovePart1((int dy, int dx) direction)
    {

        switch (Field[RobotPosition.y + direction.dy, RobotPosition.x + direction.dx])
        {
            case FieldEntry.Wall:
                break;
            case FieldEntry.floor:
                RobotPosition = (RobotPosition.y + direction.dy, RobotPosition.x + direction.dx);
                break;
            case FieldEntry.box:
                var newPossibleStonePosition =
                    NextFloorAfterBoxes((RobotPosition.y + direction.dy, RobotPosition.x + direction.dx),
                        direction);
                if (newPossibleStonePosition is { } floor)
                {
                    Field[floor.y, floor.x] = FieldEntry.box;
                    Field[RobotPosition.y + direction.dy, RobotPosition.x + direction.dx] = FieldEntry.floor;
                    RobotPosition = (RobotPosition.y + direction.dy, RobotPosition.x + direction.dx);
                }

                break;
        }
    }

    public void RobotMovePart2((int dy, int dx) direction)
    {
        // Moves in X
        if (direction.dy == 0)
        {
            var willHitWall = Field[RobotPosition.y, RobotPosition.x + direction.dx] == FieldEntry.Wall;

            (int checkBoxY, int checBoxX) = direction.dx == -1
                ? (RobotPosition.y, RobotPosition.x - 2)
                : (RobotPosition.y, RobotPosition.x + 1);

            if (willHitWall)
                return;

            if (Field[checkBoxY, checBoxX] == FieldEntry.box)
            {
                if (NextFloorAfterBoxes2((checkBoxY, checBoxX), direction)
                    is { } boxesToMove)
                {
                    foreach (var box in boxesToMove)
                    {
                        Field[box.y, box.x] = FieldEntry.floor;
                        Field[box.y, box.x + direction.dx] = FieldEntry.box;
                    }

                    RobotPosition = (RobotPosition.y, RobotPosition.x + direction.dx);
                }
            }
            else
            {
                RobotPosition = (RobotPosition.y, RobotPosition.x + direction.dx);
            }
            // moves in y
        } else {
            var blocking1 = Field[RobotPosition.y +direction.dy , RobotPosition.x];
            var blocking2 = Field[RobotPosition.y +direction.dy , RobotPosition.x-1];

            if(blocking1 is FieldEntry.Wall)
                return;

            if (blocking1 is FieldEntry.floor
                && blocking2 is FieldEntry.floor or FieldEntry.Wall)
            {
                RobotPosition = (RobotPosition.y +direction.dy, RobotPosition.x );
                return;
            }

            if ((blocking2 is not FieldEntry.box
                 && blocking1 is not FieldEntry.box)
                || (blocking2 is FieldEntry.box
                    && blocking1 is FieldEntry.box))
                throw new InvalidOperationException("Should not happen");

            (var boxY, var boxX) = Field[RobotPosition.y + direction.dy, RobotPosition.x] == FieldEntry.box
                ? (RobotPosition.y + direction.dy, RobotPosition.x)
                : (RobotPosition.y + direction.dy, RobotPosition.x - 1);

                if (TryToMoveBoxInY((boxY,boxX), direction))
                {
                    RobotPosition = (RobotPosition.y + direction.dy, RobotPosition.x);
                }
        }
    }

    private bool TryToMoveBoxInY((int y, int) box, (int dy, int dx) direction)
    {
        var boxesToMove = BoxesToMoveInY(box, direction);

        if (boxesToMove is null)
            return false;

        foreach (var moveBox in boxesToMove)
            Field[moveBox.y, moveBox.x] = FieldEntry.floor;

        foreach (var moveBox in boxesToMove)
            Field[moveBox.y + direction.dy, moveBox.x] = FieldEntry.box;

        //TODO: Fix this
        return true;
    }

    private List<(int y, int x)>?  BoxesToMoveInY((int y, int x) box, (int y, int x) direction)
    {
        (var nbY, var nbX) = (box.y + direction.y, box.x + direction.x);

        List<(int y, int x)> collidingPositions = [(nbY, nbX), (nbY, nbX + 1), (nbY, nbX - 1)];

        List<(int y, int x)> resultBoxes = [box];


        // All is floor
        if ( collidingPositions.All(cp => Field[cp.y, cp.x] == FieldEntry.floor))
            return resultBoxes;

        if (Field[nbY, nbX] == FieldEntry.Wall ||
            Field[nbY, nbX + 1] == FieldEntry.Wall)
            return null;

        var boxesMustBeMoved = collidingPositions
            .Where(cp => Field[cp.y, cp.x] == FieldEntry.box);

        foreach (var newBox in boxesMustBeMoved)
        {
            var moreMove = BoxesToMoveInY(newBox, direction);
            if (moreMove is null)
                return null;
            resultBoxes.AddRange(moreMove);
        }

        return resultBoxes;
    }

    private (int y, int x)? NextFloorAfterBoxes((int y, int x) position, (int dy, int dx) direction)
    {
        while (!IsOutOfBound(position))
        {
            if (Field[position.y, position.x] == FieldEntry.floor)
                return position;
            if (Field[position.y, position.x] == FieldEntry.Wall)
                return null;

            // stone
            position = (position.y + direction.dy, position.x + (direction.dx * widthMultiplier));
        }
        return null;
    }

    private List<(int y, int x)>? NextFloorAfterBoxes2((int y, int x) position, (int dy, int dx) direction)
    {
        List<(int y, int x)> boxesToMove = [];

        while (!IsOutOfBound(position))
        {
            if (Field[position.y, position.x] == FieldEntry.floor)
                return boxesToMove;
            if (Field[position.y, position.x] == FieldEntry.Wall)
                return null;

            boxesToMove.Add(position);
            // box
            position = (position.y + direction.dy, position.x + (direction.dx * widthMultiplier));
        }
        return null;
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

    private void PrintField()
    {
        for (var y = 0; y < MaxY; y++)
        {
            var row = new StringBuilder();
            for (var x = 0; x < MaxX; x++)
            {
                if (RobotPosition.x == x && RobotPosition.y == y)
                    row.Append('@');
                else
                    row.Append(Field[y, x] switch
                    {
                        FieldEntry.Wall => "#",
                        FieldEntry.floor => ".",
                        FieldEntry.box => "O",
                    });
            }
            Console.WriteLine(row.ToString());
            Debug.WriteLine(row.ToString());
        }
    }

    public long Part1()
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
            if (Field[y, x] == FieldEntry.box)
                sum += y * 100 + x;

        return sum;
    }

    public long Part2()
    {
        var sum = 0L;

        foreach (var row in InstructionList)
        {
            foreach (var instruction in row)
            {
                PrintField();
                Console.WriteLine(instruction);
                Debug.WriteLine(instruction);
                RobotMovePart2(ToDirection(instruction));
            }
        }

        PrintField();

        for (var y = 0; y < MaxY; y++)
        for (var x = 0; x < MaxX; x++)
            if (Field[y, x] == FieldEntry.box)
                sum += y * 100 + x;

        return sum;
    }
}
