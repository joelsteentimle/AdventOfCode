using AdventLibrary;
using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day18Tests
{

    [Test]
    public void Test1()
    {
        var d18 = new Day18("Day18".ReadTestLines(),6,6);
        Assert.That(d18.Part1(12), Is.EqualTo(22));
    }

    [Test]
    public void Real1()
    {
        var d18 = new Day18("Day18".ReadRealLines(),70,70);
        Assert.That(d18.Part1(1024), Is.EqualTo(270));
    }

    [Test]
    public void Test2()
    {
        var d18 = new Day18("Day18".ReadTestLines(),6,6);
        Assert.That(d18.Part2(), Is.EqualTo(new Position(1, 6)));
    }

    [Test]
    public void Real2()
    {
        var d18 = new Day18("Day18".ReadRealLines(),70,70);
        Assert.That(d18.Part2(), Is.EqualTo(new Position(40,51)));
    }
}
