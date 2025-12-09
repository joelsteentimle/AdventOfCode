using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day09Tests
{
    [Test]
    public void Test1()
    {
        var d09 = new Day09("Day09".ReadTestLines());
        Assert.That(d09.Part1(), Is.EqualTo(50));
    }

    [Test]
    public void Real1()
    {
        var d09 = new Day09("Day09".ReadRealLines());
        Assert.That(d09.Part1(), Is.EqualTo(2147482728));
    }


    [Test]
    public void Test2()
    {
        var d09 = new Day09("Day09".ReadTestLines());
        Assert.That(d09.Part2(), Is.EqualTo(2858));
    }

    [Test]
    public void Real2()
    {
        var d09 = new Day09("Day09".ReadRealLines());
        Assert.That(d09.Part2(), Is.EqualTo(6469636832766));
    }
}
