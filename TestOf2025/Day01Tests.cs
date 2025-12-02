using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day01Tests
{
    [Test]
    public void Test1()
    {
        var d1 = new Day01("Day01".ReadTestLines());
        Assert.That(d1.Part1(), Is.EqualTo(3));
    }

    [Test]
    public void Real1()
    {
        var d1 = new Day01("Day01".ReadRealLines());
        Assert.That(d1.Part1(), Is.EqualTo(1092));
    }


    [Test]
    public void Test2()
    {
        var d1 = new Day01("Day01".ReadTestLines());
        Assert.That(d1.Part2(), Is.EqualTo(6));
    }

    [Test]
    public void Test2Thousand()
    {
        var d1 =  new Day01("Day01".ReadTestLines("R1000"));
        Assert.That(d1.Part2(), Is.EqualTo(11));
    }

    [Test]
    public void Real2()
    {
        var d1 = new Day01("Day01".ReadRealLines());
        Assert.That(d1.Part2(), Is.EqualTo(6616));
    }
}
