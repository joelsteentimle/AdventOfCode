using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day22Tests
{

    [Test]
    public void StepTest()
    {
        Assert.That(Day22.NextPrice(123), Is.EqualTo(15887950));

    }

    [Test]
    public void Test1()
    {
        var d22 = new Day22("Day22".ReadTestLines());
        Assert.That(d22.Part1(), Is.EqualTo(37327623));
    }

    [Test]
    public void Real1()
    {
        var d22 = new Day22("Day22".ReadRealLines());
        Assert.That(d22.Part1(), Is.EqualTo(14869099597));
    }

    [Test]
    public void Test2()
    {
        var d22 = new Day22("Day22".ReadTestLines("P2"));
        Assert.That(d22.Part2(), Is.EqualTo(23));
    }

    [Test]
    public void Test2Steps()
    {
        var d22 = new Day22("Day22".ReadTestLines("Steps"));
        Assert.That(d22.Part2(), Is.EqualTo(9));
    }

    [Test]
    public void Real2()
    {
        var d22 = new Day22("Day22".ReadRealLines());
        Assert.That(d22.Part2(), Is.EqualTo(1717));
    }
}
