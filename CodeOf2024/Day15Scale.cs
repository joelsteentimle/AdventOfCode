using System.Diagnostics;
using System.Text;

namespace AoC2024;

public class Day15Scale
{
    public enum FieldEntry
    {
        floor =0,
        Lbox,
        Rbox,
        Wall,
    }

    public (int y, int x) RobotPosition = (-1, -1);

    public (int dy, int dx) ToDirection(char i) => i switch
    {
        '^' => (-1, 0),
        'v' => (1, 0),
        '<' => (0, -1),
        '>' => (0, 1),
    };

    public FieldEntry[,] Field;
    private int MaxY;
    private readonly int MaxX;
    private readonly IEnumerable<string> InstructionList;

    public Day15Scale(List<string> allData)
    {
        var inputField = allData.TakeWhile(l => !string.IsNullOrWhiteSpace(l)).ToArray();
        InstructionList = allData.SkipWhile(l => !string.IsNullOrWhiteSpace(l)).Skip(1);

        MaxY = inputField.Length;
        MaxX = inputField[0].Length * 2;

        Field = new FieldEntry[MaxY, MaxX];

        for (var y = 0; y < MaxY; y++)
        {
            for (var x = 0; x < MaxX / 2; x++)
            {
                if (inputField[y][x] == '@')
                {
                    RobotPosition = (y, x *2);
                }
                else
                {

                    switch (inputField[y][x])
                    {
                        case '.':
                            Field[y, x * 2] = FieldEntry.floor;
                            break;
                        case '#':
                            Field[y, x * 2] = FieldEntry.Wall;
                            Field[y, (x * 2) + 1] = FieldEntry.Wall;
                            break;
                        case 'O':
                            Field[y, x * 2] = FieldEntry.Lbox;
                            Field[y, (x * 2) + 1] = FieldEntry.Rbox;
                            break;
                    }

                }
            }
        }
    }


    public void RobotMovePart2((int dy, int dx) direction)
    {
        var (nextY, nextX) =
            (RobotPosition.y + direction.dy
            , RobotPosition.x + direction.dx);

        if (Field[nextY, nextX] == FieldEntry.Wall)
            return;

        // if (Field[nextY, nextX] == FieldEntry.floor)
        //     RobotPosition = (nextY, nextX);

        var boxesToMove = IteratingBoxesToMoveInY(RobotPosition, direction);

        if (boxesToMove is null)
            return;

        foreach (var mBox in boxesToMove)
            Field[mBox.y, mBox.x] = FieldEntry.floor;

        foreach (var mBox in boxesToMove)
            Field[mBox.y + direction.dy, mBox.x + direction.dx] = mBox.boxType;

        RobotPosition = (nextY, nextX);
    }
    private List<(int y, int x, FieldEntry boxType)>? IteratingBoxesToMoveInY((int y, int x) startBox, (int y, int x) direction)
    {
        var (dy, dx) = direction;
        var findingBoxes = new Queue<(int y, int x)>([(startBox.y, startBox.x)]);

        List<(int y, int x, FieldEntry boxType)> resultBoxes =
            [(startBox.y, startBox.x, Field[startBox.y, startBox.x])];

        while (findingBoxes.TryDequeue(out var box))
        {
            var (nbY, nbX) = (box.y + dy, box.x +dx);

            if (Field[nbY, nbX] == FieldEntry.Wall)
                return null;

            if (Field[nbY, nbX] == FieldEntry.Lbox)
            {
                AddBox((nbY, nbX, FieldEntry.Lbox));
                AddBox((nbY, nbX+1, FieldEntry.Rbox));
            }else if (Field[nbY, nbX] == FieldEntry.Rbox)
            {
                AddBox((nbY, nbX-1, FieldEntry.Lbox));
                AddBox((nbY, nbX, FieldEntry.Rbox));
            }
        }
        return resultBoxes.ToList();

        void AddBox((int y, int x, FieldEntry boxType) newBox)
        {
            if (!resultBoxes.Contains(newBox))
            {
                findingBoxes.Enqueue((newBox.y, newBox.x));
                resultBoxes.Add(newBox);
            }
        }
    }

    public void PrintField()
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
                        FieldEntry.Lbox => "[",
                        FieldEntry.Rbox => "]",
                    });
            }
            Console.WriteLine(row);
            Debug.WriteLine(row);
        }
    }

    public long Part2()
    {
        var sum = 0L;

        foreach (var row in InstructionList)
        {
            foreach (var instruction in row)
            {
                // PrintField();
                RobotMovePart2(ToDirection(instruction));
            }
        }

        PrintField();

        for (var y = 0; y < MaxY; y++)
        for (var x = 0; x < MaxX; x++)
            if (Field[y, x] == FieldEntry.Lbox)
                sum += y * 100 + x;

        return sum;
    }
}
