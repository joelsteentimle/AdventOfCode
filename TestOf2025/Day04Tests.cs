using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day04Tests
{
    [Test]
    public void Test1()
    {
        var d4 = new Day04("Day04".ReadTestLines());
        Assert.That(d4.Part1(), Is.EqualTo(13));
    }

    [Test]
    public void Real1()
    {
        var d4 = new Day04("Day04".ReadRealLines());
        Assert.That(d4.Part1(), Is.EqualTo(1569));
    }

    [Test]
    public void Test2()
    {
        var d4 = new Day04("Day04".ReadTestLines());
        Assert.That(d4.Part2(), Is.EqualTo(43));
    }

    // was 23 - 40ms
    [Test]
    public void Real2()
    {
        var d4 = new Day04("Day04".ReadRealLines());
        Assert.That(d4.Part2(), Is.EqualTo(9280));
    }
}
