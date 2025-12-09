using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day08Tests
{
    [Test]
    public void Test1()
    {
        var d08 = new Day08("Day08".ReadTestLines());
        Assert.That(d08.Part1(10), Is.EqualTo(40));
    }

    [Test]
    public void Real1()
    {
        var d08 = new Day08("Day08".ReadRealLines());
        Assert.That(d08.Part1(1000), Is.EqualTo(115885));
    }


    [Test]
    public void Test2()
    {
        var d08 = new Day08("Day08".ReadTestLines());
        Assert.That(d08.Part2(), Is.EqualTo(25272));
    }

    [Test]
    public void Real2()
    {
        var d08 = new Day08("Day08".ReadRealLines());
        Assert.That(d08.Part2(), Is.EqualTo(274150525));
    }
}
