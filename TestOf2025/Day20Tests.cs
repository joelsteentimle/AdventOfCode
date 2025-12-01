using AoC2025;
using SupportCode;

namespace TestOf2025;

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
        var shortCuts = d20.Part1(20);

        Assert.Multiple(() =>
        {
            Assert.That(shortCuts.Count(sc => sc == 50), Is.EqualTo(32));
            Assert.That(shortCuts.Count(sc => sc == 52), Is.EqualTo(31));
            Assert.That(shortCuts.Count(sc => sc == 64), Is.EqualTo(19));
            Assert.That(shortCuts.Count(sc => sc == 74), Is.EqualTo(4));
        });
    }

    [Test]
    public void Real2()
    {
        var d20 = new Day20("Day20".ReadRealLines());
        Assert.That(d20.Part1(20).Count(ts => ts >= 100), Is.EqualTo(471));
    }
}
