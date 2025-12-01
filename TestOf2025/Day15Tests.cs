using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day15Tests
{
    [Test]
    public void Test1()
    {
        var d15 = new Day15("Day15".ReadTestLines());
        Assert.That(d15.Part1(), Is.EqualTo(10092));
    }

    [Test]
    public void Test1_Small()
    {
        var d15 = new Day15("Day15".ReadTestLines("Small"));
        Assert.That(d15.Part1(), Is.EqualTo(2028));
    }


    [Test]
    public void Real1()
    {
        var d15 = new Day15("Day15".ReadRealLines());
        Assert.That(d15.Part1(), Is.EqualTo(1475249));
    }

    [Test]
    public void Test2()
    {
        var d15 = new Day15("Day15".ReadTestLines(), widthMultiplier: 2);
        Assert.That(d15.Part2(), Is.EqualTo(9021));
    }

    [Test]
    public void Test2With2()
    {
        var d15 = new Day15Scale("Day15".ReadTestLines());
        Assert.That(d15.Part2(), Is.EqualTo(9021));
    }

    [Test]
    public void Test2_mini()
    {
        var d15 = new Day15("Day15".ReadTestLines("2"), widthMultiplier: 2);
        Assert.That(d15.Part2(), Is.EqualTo(1506));
    }

    [Test]
    public void Test2_play()
    {
        var d15 = new Day15("Day15".ReadTestLines("play"), widthMultiplier: 2);
        Assert.That(d15.Part2(), Is.EqualTo(1506));
    }

    [Test]
    public void Real2()
    {
        var d15 = new Day15Scale("Day15".ReadRealLines());
        Assert.That(d15.Part2(), Is.EqualTo(1509724));
    }

    [Test]
    public void FindDiff()
    {
        var d15Corr = new Day15Scale("Day15".ReadTestLines());
        var d15Org = new Day15("Day15".ReadTestLines(), widthMultiplier: 2);

        var instructionList= string.Join("", d15Org.InstructionList);

        if (!BothTheSame(d15Org, d15Corr))
        {
            Assert.Fail($"Diff at start");
        }

        for (int i = 0; i < instructionList.Length; i++)
        {
            var instruction = instructionList[i];
        // }
        //
        // foreach (var instruction in instructionList)
        // {
            d15Org.RobotMovePart2(d15Org.ToDirection(instruction));
            d15Corr.RobotMovePart2(d15Corr.ToDirection(instruction));

            if (!BothTheSame(d15Org, d15Corr))
            {
                Console.WriteLine($"Instruction: {instruction}");
                Console.WriteLine("");
                Console.WriteLine($"Correct:");
                d15Corr.PrintField();
                Console.WriteLine($"Old:");
                d15Org.PrintField();

                Assert.Fail($"Diff at instruciton {instruction} at position: {i} is not the same");
            }
        }
    }

    private bool BothTheSame(Day15 d15Org, Day15Scale d15Corr)
    {
        if(d15Org.RobotPosition != d15Corr.RobotPosition)
            return false;

        for (int y = 0; y < d15Org.MaxY; y++)
        {
            for (int x = 0; x < d15Org.MaxX; x++)
            {
                if(d15Corr.Field[y,x]== Day15Scale.FieldEntry.Lbox
                   && d15Org.Field[y,x]!= Day15.FieldEntry.box)
                    return false;

                if(d15Corr.Field[y,x] == Day15Scale.FieldEntry.Rbox
                   && d15Org.Field[y,x]!= Day15.FieldEntry.floor)
                    return false;
            }
        }

        return true;
    }
}
