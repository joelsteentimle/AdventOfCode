using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day09Tests
{
    [Test]
    public void Test1()
    {
        var d09 = new Day09("Day09".ReadTestLines());
        Assert.That(d09.Get1(), Is.EqualTo(14));
    }

    [Test]
    public void Real1()
    {
        var d09 = new Day09("Day09".ReadRealLines());
        Assert.That(d09.Get1(), Is.EqualTo(269));
    }


    [Test]
    public void Test2()
    {
        var d09 = new Day09("Day09".ReadTestLines());
        Assert.That(d09.Get2(), Is.EqualTo(34));
    }

    [Test]
    public void Real2()
    {
        var d09 = new Day09("Day09".ReadRealLines());
        Assert.That(d09.Get2(), Is.EqualTo(4998764814652));
    }
}
