using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day17Tests
{

    [Test] public void Test1_micro()
    {
        var d17 = new Day17("Day17".ReadTestLines("micro"));
        Assert.That(d17.Part1(), Is.EqualTo("4,6,3,5,6,3,5,2,1,0"));
    }

    [Test]
    public void Test1()
    {
        var d17 = new Day17("Day17".ReadTestLines());
        Assert.That(d17.Part1(), Is.EqualTo("4,6,3,5,6,3,5,2,1,0"));
    }

    [Test]
    public void Real1()
    {
        var d17 = new Day17("Day17".ReadRealLines());
        Assert.That(d17.Part1(), Is.EqualTo("4,6,3,5,6,3,5,2,1,0"));
    }

    [Test]
    public void Test2()
    {
        var d17 = new Day17("Day17".ReadTestLines("2"));
        Assert.That(d17.Part1(), Is.EqualTo("0,3,5,4,3,0"));
    }

    [Test]
    public void Real2()
    {
        var d17 = new Day17("Day17".ReadRealLines("2"));
        Assert.That(d17.Part1(), Is.EqualTo("2,4,1,7,7,5,1,7,4,6,0,3,5,5,3,0"));
    }
}
