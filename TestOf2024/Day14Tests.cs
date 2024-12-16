using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day14Tests
{


    [Test]
    public void Test1()
    {
        var d14 = new Day14("Day14".ReadTestLines(), (7,11));
        Assert.That(d14.Part1(100), Is.EqualTo(12));
    }

    [Test]
    public void Test1_1()
    {
        var d14 = new Day14("Day14".ReadTestLines("1"), (7,11));
        d14.WaitSeconds(1);
        Assert.That(d14.Robots[0].Position, Is.EqualTo((1,4)));
        d14.WaitSeconds(1);
        Assert.That(d14.Robots[0].Position, Is.EqualTo((5,6)));
        d14.WaitSeconds(1);
        Assert.That(d14.Robots[0].Position, Is.EqualTo((2,8)));
        d14.WaitSeconds(1);
        Assert.That(d14.Robots[0].Position, Is.EqualTo((6,10)));
        d14.WaitSeconds(1);
        Assert.That(d14.Robots[0].Position, Is.EqualTo((3,1)));
    }

    [Test]
    public void Test1_1_multiStep()
    {
        var d14 = new Day14("Day14".ReadTestLines("1"), (7,11));
        d14.WaitSeconds(5);
        Assert.That(d14.Robots[0].Position, Is.EqualTo((3,1)));
    }

    [Test]
    public void Real1()
    {
        var d14 = new Day14("Day14".ReadRealLines());

        // This is too low: 218618400
        Assert.That(d14.Part1(100), Is.EqualTo(226548000));
    }

    [Test]
    public void Test2()
    {
        var d14 = new Day14("Day14".ReadTestLines());
        Assert.That(d14.Part2(), Is.EqualTo(1406));
    }

    [Test]
    public void Real2()
    {
        var d14 = new Day14("Day14".ReadRealLines());
        Assert.That(d14.Part2(), Is.EqualTo(830566));
    }
}
