
using AoC2023;
using SupportCode;

namespace TestOf2023;
public class Day10Tests
{
    [Test]
    public void CanFindStart()
    {
        var d10 = new Day10("Day10".ReadTestLines());
        Assert.That(d10.Start, Is.EqualTo((1, 1)));
    }

    [Test]
    public void CanFindStartConnections()
    {
        var d10 = new Day10("Day10".ReadTestLines());
        var (y, x) = d10.Start;
        Assert.That(d10.Map[y, x].Connected
            , Is.EquivalentTo(new[] { (1, 0), (0, 1) }));
    }

    [Test]
    public void Part1Test()
    {
        var d10 = new Day10("Day10".ReadTestLines());
        Assert.That(d10.GetMaxDistance(d10.Start), Is.EqualTo(4));
    }

    [Test]
    public void Part1Test4()
    {
        var d10 = new Day10("Day10".ReadTestLines("4"));
        Assert.That(d10.GetMaxDistance(d10.Start), Is.EqualTo(4));
    }

    [Test]
    public void Part1()
    {
        var d10 = new Day10("Day10".ReadRealLines());
        Assert.That(d10.GetMaxDistance(d10.Start), Is.EqualTo(6823));
    }

    [Test]
    public void Part2Test()
    {
        var d10 = new Day10("Day10".ReadTestLines());
        d10.GetMaxDistance(d10.Start);
        Assert.That(d10.FloodFill(), Is.EqualTo(1));
    }

    [Test]
    public void Part2Test1()
    {
        var d10 = new Day10("Day10".ReadTestLines("1"));
        d10.GetMaxDistance(d10.Start);
        Assert.That(d10.FloodFill(), Is.EqualTo(4));
    }

    [Test]
    public void Part2Test2()
    {
        var d10 = new Day10("Day10".ReadTestLines("2"));
        d10.GetMaxDistance(d10.Start);
        Assert.That(d10.FloodFill(), Is.EqualTo(8));
    }

    [Test]
    public void Part2Test3()
    {
        var d10 = new Day10("Day10".ReadTestLines("3"));
        d10.GetMaxDistance(d10.Start);
        Assert.That(d10.FloodFill(), Is.EqualTo(10));
    }

    [Test]
    public void Part2()
    {
        var d10 = new Day10("Day10".ReadRealLines());
        d10.GetMaxDistance(d10.Start);
        Assert.That(d10.FloodFill(), Is.EqualTo(415));
    }
}
