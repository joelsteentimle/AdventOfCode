using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day10Tests
{
    [Test]
    public void Test1()
    {
        var d10 = new Day10("Day10".ReadTestLines());
        Assert.That(d10.Part1(), Is.EqualTo(7));
    }

    [Test]
    public void Test1_1()
    {
        var d10 = new Day10("Day10".ReadTestLines("1"));
        Assert.That(d10.Part1(), Is.EqualTo(1));
    }

    [Test]
    public void Real1()
    {
        var d10 = new Day10("Day10".ReadRealLines());
        Assert.That(d10.Part1(), Is.EqualTo(796));
    }


    [Test]
    public void Test2()
    {
        var d10 = new Day10("Day10".ReadTestLines());
        Assert.That(d10.Part2(), Is.EqualTo(81));
    }

    [Test]
    public void Real2()
    {
        var d10 = new Day10("Day10".ReadRealLines());
        Assert.That(d10.Part2(), Is.EqualTo(1942));
    }
}
