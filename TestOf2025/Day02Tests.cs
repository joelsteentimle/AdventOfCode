using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day02Tests
{
    [Test]
    public void Test1()
    {
        var d2 = new Day02("Day02".ReadTestLines());
        Assert.That(d2.Safe, Has.Count.EqualTo(2));
    }

    [Test]
    public void Real1()
    {
        var d2 = new Day02("Day02".ReadRealLines());
        Assert.That(d2.Safe, Has.Count.EqualTo(306));
    }

    [Test]
    public void Test2()
    {
        var d2 = new Day02("Day02".ReadTestLines(), true);
        Assert.That(d2.Safe, Has.Count.EqualTo(4));
    }

    [Test]
    public void Real2()
    {
        var d2 = new Day02("Day02".ReadRealLines(), true);
        Assert.That(d2.Safe, Has.Count.EqualTo(366));
    }
}
