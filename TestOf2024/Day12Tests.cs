using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day12Tests
{
    [Test]
    public void Test1()
    {
        var d12 = new Day12("Day12".ReadTestLines());
        Assert.That(d12.Part1(), Is.EqualTo(55312));
    }

    [Test]
    public void Real1()
    {
        var d12 = new Day12("Day12".ReadRealLines());
        Assert.That(d12.Part1(), Is.EqualTo(217443));
    }

    [Test]
    public void Test2()
    {
        var d12 = new Day12("Day12".ReadTestLines());
        Assert.That(d12.Part2(), Is.EqualTo(55312));
    }


    [Test]
    public void Real2()
    {
        var d12 = new Day12("Day12".ReadRealLines());
        Assert.That(d12.Part2(), Is.EqualTo(257246536026785));
    }
}
