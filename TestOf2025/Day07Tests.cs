using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day07Tests
{
    [Test]
    public void Test1()
    {
        var d3 = new Day07("Day07".ReadTestLines());
        Assert.That(d3.Part1(), Is.EqualTo(21));
    }

    [Test]
    public void Real1()
    {
        var d3 = new Day07("Day07".ReadRealLines());
        Assert.That(d3.Part1(), Is.EqualTo(1560));
    }

    [Test]
    public void Test2()
    {
        var d3 = new Day07("Day07".ReadTestLines());
        Assert.That(d3.Part2(), Is.EqualTo(40));
    }

    [Test]
    public void Real2()
    {
        var d3 = new Day07("Day07".ReadRealLines());
        Assert.That(d3.Part2(), Is.EqualTo(25592971184998));
    }
}
