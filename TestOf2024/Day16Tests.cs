using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day16Tests
{

    [Test]
    public void Test1()
    {
        var d16 = new Day16("Day16".ReadTestLines());
        Assert.That(d16.Part1(), Is.EqualTo(7036));
    }

    [Test]
    public void Test1_1()
    {
        var d16 = new Day16("Day16".ReadTestLines("1"));
        Assert.That(d16.Part1(), Is.EqualTo(11048));
    }

    [Test]
    public void Real1()
    {
        var d16 = new Day16("Day16".ReadRealLines());
        Assert.That(d16.Part1(), Is.EqualTo(98416));
    }

    [Test]
    public void Test2()
    {
        var d16 = new Day16("Day16".ReadTestLines());
        Assert.That(d16.Part2(), Is.EqualTo(1606));
    }

    [Test]
    public void Real2()
    {
        var d16 = new Day16("Day16".ReadRealLines());
        Assert.That(d16.Part2(), Is.EqualTo(830566));
    }
}
