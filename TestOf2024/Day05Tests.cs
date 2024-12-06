using AoC2024;
using SupportCode;

namespace TestOf2024;

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
}
