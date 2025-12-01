using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day05Tests
{
    [Test]
    public void Test1()
    {
        var d5 = new Day05("Day05".ReadTestLines());
        Assert.That(d5.MidPageSumOfAllowed(), Is.EqualTo(143));
    }

    [Test]
    public void Real1()
    {
        var d5 = new Day05("Day05".ReadRealLines());
        Assert.That(d5.MidPageSumOfAllowed(), Is.EqualTo(4689));
    }

    [Test]
    public void Test2()
    {
        var d5 = new Day05("Day05".ReadTestLines());
        Assert.That(d5.MidPagesAfterFixingOutOfOrder(), Is.EqualTo(123));
    }

    [Test]
    public void Real2()
    {
        var d5 = new Day05("Day05".ReadRealLines());
        Assert.That(d5.MidPagesAfterFixingOutOfOrder(), Is.EqualTo(6336));
    }
}
