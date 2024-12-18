using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day15Tests
{


    [Test]
    public void Test1()
    {
        var d15 = new Day15("Day15".ReadTestLines(), (7,11));
        Assert.That(d15.Part1(100), Is.EqualTo(12));
    }

    [Test]
    public void Real1()
    {
        var d15 = new Day15("Day15".ReadRealLines());

        // This is too low: 218618400
        Assert.That(d15.Part1(100), Is.EqualTo(226548000));
    }

    [Test]
    public void Test2()
    {
        var d15 = new Day15("Day15".ReadTestLines());
        Assert.That(d15.Part2(), Is.EqualTo(1506));
    }

    [Test]
    public void Real2()
    {
        var d15 = new Day15("Day15".ReadRealLines());
        Assert.That(d15.Part2(), Is.EqualTo(830566));
    }
}
