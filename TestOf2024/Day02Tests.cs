using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day02Tests
{
    [Test]
    public void Test1()
    {
        var d2 = new Day02("Day02".ReadTestLines(),0);
        Assert.That(d2.Safe.Count, Is.EqualTo(2));
    }

    [Test]
    public void Real1()
    {
        var d2 = new Day02("Day02".ReadRealLines(),0);
        Assert.That(d2.Safe.Count, Is.EqualTo(306));
    }

    [Test]
    public void Test2()
    {
        var d2 = new Day02("Day02".ReadTestLines(),1);
        Assert.That(d2.Safe.Count, Is.EqualTo(4));
    }

    [Test]
    public void Real2()
    {
        var d2 = new Day02("Day02".ReadRealLines(),1);
        Assert.That(d2.Safe.Count, Is.EqualTo(367));

    }
}
