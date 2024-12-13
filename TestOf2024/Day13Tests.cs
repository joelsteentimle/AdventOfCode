using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day13Tests
{
    [Test]
    public void Test1()
    {
        var d13 = new Day13("Day13".ReadTestLines());
        Assert.That(d13.Part1(), Is.EqualTo(480));
    }

    [Test]
    public void Real1()
    {
        var d13 = new Day13("Day13".ReadRealLines());
        Assert.That(d13.Part1(), Is.EqualTo(34393));
    }

    [Test]
    public void Test2()
    {
        var d13 = new Day13("Day13".ReadTestLines(), 10000000000000L);
        Assert.That(d13.Part2(), Is.EqualTo(1306));
    }

    [Test]
    public void Real2()
    {
        var d13 = new Day13("Day13".ReadRealLines(), 10000000000000L);
        Assert.That(d13.Part2(), Is.EqualTo(830566));
    }
}
