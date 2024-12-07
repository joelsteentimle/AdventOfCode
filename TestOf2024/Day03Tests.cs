using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day03Tests
{
    [Test]
    public void Test1()
    {
        var d3 = new Day03("Day03".ReadTestLines());
        Assert.That(d3.GetProductSum(), Is.EqualTo(161));
    }
    [Test]
    public void Real1()
    {
        var d3 = new Day03("Day03".ReadRealLines());
        Assert.That(d3.GetProductSum(), Is.EqualTo(173785482));
    }

}
