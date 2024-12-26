using AoC2024;
using SupportCode;

namespace TestOf2024;

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
        var d15 = new Day15Scale ("Day15".ReadRealLines());
        Assert.That(d15.Part2(), Is.EqualTo(1509724));
    }
}
