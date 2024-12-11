using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day11Tests
{
    [Test]
    public void Test1()
    {
        var d11 = new Day11("Day11".ReadTestLines());
        Assert.That(d11.Part1(25), Is.EqualTo(55312));
    }

    [Test]
    public void Real1()
    {
        var d11 = new Day11("Day11".ReadRealLines());
        Assert.That(d11.Part1(25), Is.EqualTo(9918828));
    }

    //
    // [Test]
    // public void Test2()
    // {
    //     var d11 = new Day11("Day11".ReadTestLines());
    //     Assert.That(d11.Part2(), Is.EqualTo(81));
    // }

    [Test]
    public void Real2()
    {
        var d11 = new Day11("Day11".ReadRealLines());
        Assert.That(d11.Part1(75), Is.EqualTo(1942));
    }
}
