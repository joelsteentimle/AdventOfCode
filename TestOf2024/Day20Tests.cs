using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day20Tests
{

    [Test]
    public void Test1()
    {
        var d20 = new Day20("Day20".ReadTestLines());
        var shortCuts = d20.Part1();
        Assert.That(shortCuts, Has.Count.EqualTo(44));
        Assert.That(shortCuts.Count(sc => sc == 12), Is.EqualTo(3));

    }

    [Test]
    public void Real1()
    {
        var d20 = new Day20("Day20".ReadRealLines());
        Assert.That(d20.Part1().Count(sc => sc >= 100), Is.EqualTo(1485));
    }

    [Test]
    public void Test2()
    {
        var d20 = new Day20("Day20".ReadTestLines());
        Assert.That(d20.Part2(), Is.EqualTo(45));
    }

    [Test]
    public void Real2()
    {
        var d20 = new Day20("Day20".ReadRealLines());
        Assert.That(d20.Part2(), Is.EqualTo(471));
    }
}
