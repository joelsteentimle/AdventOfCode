using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day07Tests
{
    [Test]
    public void Test1()
    {
        var d3 = new Day07("Day07".ReadTestLines());
        Assert.That(d3.SumThatCan(), Is.EqualTo(3749));
    }

    [Test]
    public void Real1()
    {
        var d3 = new Day07("Day07".ReadRealLines());
        Assert.That(d3.SumThatCan(), Is.EqualTo(4998764814652));
    }

    [Test]
    public void Test2()
    {
        var d3 = new Day07("Day07".ReadTestLines());
        Assert.That(d3.SumThatCan(true), Is.EqualTo(11387));
    }

    [Test]
    public void Real2()
    {
        var d3 = new Day07("Day07".ReadRealLines());
        Assert.That(d3.SumThatCan(true), Is.EqualTo(37598910447546));
    }
}
