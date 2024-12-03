
using AoC2023;
using SupportCode;

namespace TestOf2023;
public class Day11Tests
{
    [Test]
    public void Test1()
    {
        var d11 = new Day11("Day11".ReadTestLines());
        Assert.That(d11.CalculateStarsDistance(), Is.EqualTo(374));
    }

    [Test]
    public void Part1()
    {
        var d11 = new Day11("Day11".ReadRealLines());
        Assert.That(d11.CalculateStarsDistance(), Is.EqualTo(9918828));
    }

    [Test]
    public void Test2Distance10()
    {
        var d11 = new Day11("Day11".ReadTestLines(), 10);
        Assert.That(d11.CalculateStarsDistance(), Is.EqualTo(1030));
    }

    [Test]
    public void Test2Distance100()
    {
        var d11 = new Day11("Day11".ReadTestLines(), 100);
        Assert.That(d11.CalculateStarsDistance(), Is.EqualTo(8410));
    }

    [Test]
    public void Part2()
    {
        var d11 = new Day11("Day11".ReadRealLines(), 1000000);
        Assert.That(d11.CalculateStarsDistance(), Is.EqualTo(692506533832));
    }

    [Test]
    public void JustToCover()
    {
        var s1 = new Day11.Star { X = 2, Y = 3 };
        Assert.Multiple(() =>
        {
            Assert.That(s1.X, Is.EqualTo(2));
            Assert.That(s1.Y, Is.EqualTo(3));
        });
    }
}
