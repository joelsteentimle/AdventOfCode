using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day12Tests
{
    [Test]
    public void Test1()
    {
        var d12 = new Day12("Day12".ReadTestLines());
        Assert.That(d12.Part1(), Is.EqualTo(1930));
    }

    [Test]
    public void Test1_1()
    {
        var d12 = new Day12("Day12".ReadTestLines("1"));
        Assert.That(d12.Part1(), Is.EqualTo(140));
    }

    [Test]
    public void Real1()
    {
        var d12 = new Day12("Day12".ReadRealLines());
        Assert.That(d12.Part1(), Is.EqualTo(1375574));
    }

    [Test]
    public void Test2()
    {
        var d12 = new Day12("Day12".ReadTestLines());
        Assert.That(d12.Part2(), Is.EqualTo(1206));
    }

    [Test]
    public void Test2_1()
    {
        var d12 = new Day12("Day12".ReadTestLines("1"));
        Assert.That(d12.Part2(), Is.EqualTo(80));
    }

    [Test]
    public void Test2_2()
    {
        var d12 = new Day12("Day12".ReadTestLines("2"));
        Assert.That(d12.Part2(), Is.EqualTo(236));
    }

    [Test]
    public void Test2_3()
    {
        var d12 = new Day12("Day12".ReadTestLines("3"));
        Assert.That(d12.Part2(), Is.EqualTo(368));
    }

    [Test]
    public void Real2()
    {
        var d12 = new Day12("Day12".ReadRealLines());
        Assert.That(d12.Part2(), Is.EqualTo(830566));
    }
}
