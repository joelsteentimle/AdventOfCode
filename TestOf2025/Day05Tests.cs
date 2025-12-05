using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day05Tests
{
    [Test]
    public void Test1()
    {
        var d5 = new Day05("Day05".ReadTestLines());
        Assert.That(d5.Part1(), Is.EqualTo(3));
    }

    [Test]
    public void Real1()
    {
        var d5 = new Day05("Day05".ReadRealLines());
        Assert.That(d5.Part1(), Is.EqualTo(520));
    }

    [Test]
    public void Test2()
    {
        var d5 = new Day05("Day05".ReadTestLines());
        Assert.That(d5.Part2(), Is.EqualTo(14));
    }

    [Test]
    public void Real2()
    {
        var d5 = new Day05("Day05".ReadRealLines());
        Assert.That(d5.Part2(), Is.EqualTo(6336));
    }
}
