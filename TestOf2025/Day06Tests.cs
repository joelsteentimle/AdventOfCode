using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day06Tests
{
    [Test]
    public void Test1()
    {
        var d6 = new Day06("Day06".ReadTestLines());
        Assert.That(d6.Part1(), Is.EqualTo(4277556));
    }

    [Test]
    public void Real1()
    {
        var d6 = new Day06("Day06".ReadRealLines());
        Assert.That(d6.Part1(), Is.EqualTo(5873191732773));
    }

    [Test]
    public void Test2()
    {
        var d6 = new Day06("Day06".ReadTestLines());
        Assert.That(d6.Part2(), Is.EqualTo(3263827));
    }

    [Test]
    public void Real2()
    {
        var d6 = new Day06("Day06".ReadRealLines());
        Assert.That(d6.Part2(), Is.EqualTo(11386445308378));
    }
}
