using AdventLibrary;
using AoC2025;
using SupportCode;

namespace TestOf2025;

public class Day19Tests
{
    [Test]
    public void Test1()
    {
        var d19 = new Day19("Day19".ReadTestLines());
        Assert.That(d19.Part1(), Is.EqualTo(6));
    }

    [Test]
    public void Real1()
    {
        var d19 = new Day19("Day19".ReadRealLines());
        Assert.That(d19.Part1(), Is.EqualTo(369));
    }

    [Test]
    public void Test2()
    {
        var d19 = new Day19("Day19".ReadTestLines());
        Assert.That(d19.Part2(), Is.EqualTo(16));
    }

    [Test]
    public void Real2()
    {
        var d19 = new Day19("Day19".ReadRealLines());
        Assert.That(d19.Part2(), Is.EqualTo(761826581538190));
    }
}
