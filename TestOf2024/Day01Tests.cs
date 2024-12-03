using AoC2024;
using SupportCode;

namespace TestOf2026;

public class Day01Tests
{
    [Test]
    public void Test1()
    {
        var d1 = new Day01("Day01".ReadTestLines());
        Assert.That(d1.Part1(), Is.EqualTo(11));
    }

    [Test]
    public void Real1()
    {
        var d1 = new Day01("Day01".ReadRealLines());
        Assert.That(d1.Part1(), Is.EqualTo(1530215));
    }


    [Test]
    public void Test2()
    {
        var d1 = new Day01("Day01".ReadTestLines());
        Assert.That(d1.Part2(), Is.EqualTo(31));
    }

    [Test]
    public void Real2()
    {
        var d1 = new Day01("Day01".ReadRealLines());
        Assert.That(d1.Part2(), Is.EqualTo(26800609));
    }
}
