using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day04Tests
{
    [Test]
    public void Test1()
    {
        var d3 = new Day04("Day04".ReadTestLines());
        Assert.That(d3.CountAll("XMAS".ToCharArray().ToList()), Is.EqualTo(18));
    }

    [Test]
    public void Real1()
    {
        var d3 = new Day04("Day04".ReadRealLines());
        Assert.That(d3.CountAll("XMAS".ToCharArray().ToList()), Is.EqualTo(2662));
    }
}
