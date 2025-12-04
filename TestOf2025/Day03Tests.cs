using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day03Tests
{
    [Test]
    public void Test1()
    {
        var d3 = new Day03("Day03".ReadTestLines());
        Assert.That(d3.Part1(), Is.EqualTo(357));
    }

    [Test]
    public void Real1()
    {
        var d3 = new Day03("Day03".ReadRealLines());
        Assert.That(d3.Part1(), Is.EqualTo(17155));
    }

    [Test]
    public void Test2()
    {
        var d3 = new Day03("Day03".ReadTestLines());
        Assert.That(d3.Part2(), Is.EqualTo(3121910778619));
    }

    [Test]
    public void Real2()
    {
        var d3 = new Day03("Day03".ReadRealLines());
        Assert.That(d3.Part2(), Is.EqualTo(169685670469164));
    }
}
