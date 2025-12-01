using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day09Tests
{
    [Test]
    public void Test1()
    {
        var d09 = new Day09("Day09".ReadTestLines());
        Assert.That(d09.UncompressAndSum(), Is.EqualTo(1928));
    }

    [Test]
    public void Real1()
    {
        var d09 = new Day09("Day09".ReadRealLines());
        Assert.That(d09.UncompressAndSum(), Is.EqualTo(6435922584968));
    }


    [Test]
    public void Test2()
    {
        var d09 = new Day09("Day09".ReadTestLines());
        Assert.That(d09.OtherUncompress(), Is.EqualTo(2858));
    }

    [Test]
    public void Real2()
    {
        var d09 = new Day09("Day09".ReadRealLines());
        Assert.That(d09.OtherUncompress(), Is.EqualTo(6469636832766));
    }
}
