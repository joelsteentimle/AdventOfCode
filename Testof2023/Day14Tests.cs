namespace TestOf2023;

using AoC2023;

public class Day14Tests : DayTests
{
    [Test]
    public void Smoke() =>
        Assert.Multiple(() =>
        {
            Assert.That(RealInstance, Is.Not.Null);
            Assert.That(TestInstance, Is.Not.Null);
        });

    [Test]
    public void CanCalculatePrerolled()
    {
        var d14 = new Day14("Day14".ReadTestLines("Tilt"));
        Assert.That(d14.CalculateLoad(), Is.EqualTo(136));
    }

    [Test]
    public void CanRollStone()
    {
        var d14 = TestInstance;
        d14.RollNorth();
        Assert.That(d14.CalculateLoad(), Is.EqualTo(136));
    }

    [Test]
    public void Part1()
    {
        var d14 = RealInstance;
        d14.RollNorth();
        Assert.That(d14.CalculateLoad(), Is.EqualTo(109661));
    }

    [Test]
    public void CanCycle()
    {
        var d14 = TestInstance;

    }


    private Day14 RealInstance => new(RealLines);
    private Day14 TestInstance => new(TestLines);
}
