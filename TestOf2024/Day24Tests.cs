using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day24Tests
{

    [Test]
    public void Test1()
    {
        var d24 = new Day24("Day24".ReadTestLines());
        Assert.That(d24.Part1(), Is.EqualTo(7));
    }

    [Test]
    public void Real1()
    {
        var d24 = new Day24("Day24".ReadRealLines());
        Assert.That(d24.Part1(), Is.EqualTo(1370));
    }

    [Test]
    public void Test2()
    {
        var d24 = new Day24("Day24".ReadTestLines());
        Assert.That(d24.Part2(), Is.EqualTo(5555));
    }

    [Test]
    public void Real2()
    {
        var d24 = new Day24("Day24".ReadRealLines());
        Assert.That(d24.Part2(), Is.EqualTo(5555));
    }
}
