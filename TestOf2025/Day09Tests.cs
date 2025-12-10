using SupportCode;

namespace TestOf2025;

public class Day09Tests
{
    [Test]
    public void Test1()
    {
        var d09 = new Day09("Day09".ReadTestLines());
        Assert.That(d09.Part1(), Is.EqualTo(50));
    }

    [Test]
    public void Real1()
    {
        var d09 = new Day09("Day09".ReadRealLines());
        Assert.That(d09.Part1(), Is.EqualTo(4781546175));
    }


    [Test]
    public void Test2()
    {
        var d09 = new Day09("Day09".ReadTestLines());
        Assert.That(d09.Part2(), Is.EqualTo(24));
    }

    [Test]
    public void Real2()
    {
        var d09 = new Day09("Day09".ReadRealLines());
        Assert.That(d09.Part2(), Is.EqualTo(1573359081));
    }
}
