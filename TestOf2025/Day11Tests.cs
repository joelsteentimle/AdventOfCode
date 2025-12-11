using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day11Tests
{
    [Test]
    public void Test1()
    {
        var d11 = new Day11("Day11".ReadTestLines());
        Assert.That(d11.Part1(), Is.EqualTo(5));
    }

    [Test]
    public void Real1()
    {
        var d11 = new Day11("Day11".ReadRealLines());
        Assert.That(d11.Part1(), Is.EqualTo(764));
    }


    [Test]
    public void Test2()
    {
        var d11 = new Day11("Day11".ReadTestLines("2"));
        Assert.That(d11.Part2(), Is.EqualTo(2));
    }

    [Test]
    public void Real2()
    {
        var d11 = new Day11("Day11".ReadRealLines());
        Assert.That(d11.Part2(), Is.EqualTo(257246536026785));
    }
}
