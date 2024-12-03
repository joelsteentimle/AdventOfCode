using AoC2023;
using AoC2023.Graph;
using SupportCode;

namespace TestOf2023;

public class Day18Tests : DayTests
{
    [Test]
    public void CanAddDirection() =>
        Assert.Multiple(() =>
        {
            Assert.That((Direction)((int)(Direction.East + 1) % 4), Is.EqualTo(Direction.South));
            Assert.That((Direction)((int)(Direction.North + 3) % 4), Is.EqualTo(Direction.West));
            Assert.That((Direction)((int)(Direction.North + 1) % 4), Is.EqualTo(Direction.East));
            Assert.That((Direction)((int)(Direction.East + 3) % 4), Is.EqualTo(Direction.North));
        });

    [Test]
    public void CanFigureOutInside()
    {
        var d18 = GetTestInstance(false);
        d18.GenerateFieldAndStart();
        Assert.That(d18.RightIsIn);
    }

    [Test]
    public void CanCountTrench()
    {
        var d18 = GetTestInstance(false);
        d18.GenerateFieldAndStart();
        d18.WalkThroughTheTrench();
        Assert.That(d18.TrenchVolume(), Is.EqualTo(38));
    }

    [Test]
    public void Part1Test()
    {
        var d18 = GetTestInstance(false);
        d18.GenerateFieldAndStart();
        d18.WalkThroughTheTrench();
        d18.DigInside();
        Assert.Multiple(() =>
        {
            Assert.That(d18.Part1PoolVolume(), Is.EqualTo(62));
            Assert.That(d18.Part2PoolVolume(), Is.EqualTo(62));
        });
    }

    [Test]
    public void SimpleTestSecondMethod()
    {
        List<string> textInstructions = [
        "U 2",
            "R 2",
            "D 2",
            "L 2"];
        var d18 = new Day18(textInstructions, false);
        d18.GenerateFieldAndStart();
        d18.WalkThroughTheTrench();
        d18.DigInside();
        Assert.Multiple(() =>
        {
            Assert.That(d18.Part1PoolVolume(), Is.EqualTo(9), "Can count with method 1");
            Assert.That(d18.Part2PoolVolume(), Is.EqualTo(9), "Can count with method 2");
        });
    }

    [Test]
    public void Part1()
    {
        var d18 = GetRealInstance(false);
        d18.GenerateFieldAndStart();

        d18.WalkThroughTheTrench();
        d18.DigInside();
        Assert.Multiple(() =>
        {
            Assert.That(d18.Part1PoolVolume(), Is.EqualTo(106459));
            Assert.That(d18.Part2PoolVolume(), Is.EqualTo(106459));
        });
    }

    [Test]
    public void CanParsePart2Instruction()
    {
        var inst1 = Day18.Instruction.Part2("R 6 (#70c710)");
        Assert.Multiple(() =>
        {
            Assert.That(inst1.Direction, Is.EqualTo(Direction.East));
            Assert.That(inst1.Distance, Is.EqualTo(461937));
        });
    }

    [Test]
    public void Part2Test()
    {
        var d18 = GetTestInstance(true);
        d18.FindCornerAndStart();
        Assert.That(d18.Part2PoolVolume(), Is.EqualTo(952408144115));
    }

    [Test]
    public void Part2()
    {
        var d18 = GetRealInstance(true);
        d18.FindCornerAndStart();
        Assert.That(d18.Part2PoolVolume(), Is.EqualTo(63806916814808));
    }

    [Test]
    public void CanConvertFromHex() =>
        Assert.That(Convert.ToInt32("70c71", 16), Is.EqualTo(461937));

    private Day18 GetRealInstance(bool isPart2) => new(GetRealLines(), isPart2);

    private Day18 GetTestInstance(bool isPart2, string suffix = "") => new(GetTestLines(suffix), isPart2);
}
