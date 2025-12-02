using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day02Tests
{
    [Test]
    public void Test1()
    {
        var d2 = new Day02("Day02".ReadTestLines());
        Assert.That(d2.Part1, Is.EqualTo(1227775554));
    }

    [Test]
    public void Real1()
    {
        var d2 = new Day02("Day02".ReadRealLines());
        Assert.That(d2.Part1, Is.EqualTo(29818212493));
    }

    [Test]
    public void Test2()
    {
        var d2 = new Day02("Day02".ReadTestLines());
        Assert.That(d2.Part2, Is.EqualTo(4174379265));
    }

    [Test]
    public void Real2()
    {
        var d2 = new Day02("Day02".ReadRealLines());
        Assert.That(d2.Part2, Is.EqualTo(37432260594));
    }
}
