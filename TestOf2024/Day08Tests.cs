using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day08Tests
{
    [Test]
    public void Test1()
    {
        var d08 = new Day08("Day08".ReadTestLines());
        Assert.That(d08.CountAntiNodes(), Is.EqualTo(14));
    }

    [Test]
    public void Test1_1()
    {
        var d08 = new Day08("Day08".ReadTestLines("1"));
        Assert.That(d08.CountAntiNodes(), Is.EqualTo(4));
    }

    [Test]
    public void Real1()
    {
        var d08 = new Day08("Day08".ReadRealLines());
        Assert.That(d08.CountAntiNodes(), Is.EqualTo(269));
    }


    [Test]
    public void Test2()
    {
        var d08 = new Day08("Day08".ReadTestLines());
        Assert.That(d08.CountAntiNodes(true), Is.EqualTo(34));
    }

    [Test]
    public void Real2()
    {
        var d08 = new Day08("Day08".ReadRealLines());
        Assert.That(d08.CountAntiNodes(true), Is.EqualTo(949));
    }
}
