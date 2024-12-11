using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day11Tests
{
    [Test]
    public void Test1()
    {
        var d11 = new Day11("Day11".ReadTestLines());
        Assert.That(d11.Part2(25), Is.EqualTo(55312));
    }

    [Test]
    public void Real1()
    {
        var d11 = new Day11("Day11".ReadRealLines());
        Assert.That(d11.Part2(25), Is.EqualTo(217443));
    }


    [Test]
    public void Test2_smallBlink()
    {
        var d11 = new Day11("Day11".ReadTestLines());
        Assert.That(d11.Part2(6), Is.EqualTo(22));
    }

    [Test]
    public void Test2_MidBlink()
    {
        var d11 = new Day11("Day11".ReadTestLines());
        Assert.That(d11.Part2(1), Is.EqualTo(3));
    }

    [Test]
    public void Real2()
    {
        var d11 = new Day11("Day11".ReadRealLines());
        Assert.That(d11.Part2(75), Is.EqualTo(257246536026785));
    }
}
