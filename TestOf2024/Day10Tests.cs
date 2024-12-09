using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day10Tests
{
    [Test]
    public void Test1()
    {
        var d10 = new Day10("Day10".ReadTestLines());
        Assert.That(d10.Part1(), Is.EqualTo(1928));
    }

    [Test]
    public void Real1()
    {
        var d10 = new Day10("Day10".ReadRealLines());
        Assert.That(d10.Part1(), Is.EqualTo(6435922584968));
    }


    [Test]
    public void Test2()
    {
        var d10 = new Day10("Day10".ReadTestLines());
        Assert.That(d10.Part2(), Is.EqualTo(2858));
    }

    [Test]
    public void Real2()
    {
        var d10 = new Day10("Day10".ReadRealLines());
        Assert.That(d10.Part2(), Is.EqualTo(6469636832766));
    }
}
