
using AoC2023;
using SupportCode;

namespace TestOf2023;
public class Day14Tests : DayTests
{
    [Test]
    public void Smoke() =>
        Assert.Multiple(() =>
        {
            Assert.That(RealInstance, Is.Not.Null);
            Assert.That(GetTestInstance(), Is.Not.Null);
        });

    [Test]
    public void CanCalculatePrerolled()
    {
        var d14 = new Day14("Day14".ReadTestLines("Tilt"));
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
        var d14 = GetTestInstance();
        d14.CycleRoll(1);
        Assert.That(d14.CalculateLoad(), Is.EqualTo(GetTestInstance("C1").CalculateLoad()));
        d14.CycleRoll(1);
        Assert.That(d14.CalculateLoad(), Is.EqualTo(GetTestInstance("C2").CalculateLoad()));
        d14.CycleRoll(1);
        Assert.That(d14.CalculateLoad(), Is.EqualTo(GetTestInstance("C3").CalculateLoad()));
    }

    [Test]
    public void CanCycleManyTimes()
    {
        var d14 = GetTestInstance();
        d14.CycleRoll(1000000000);
        Assert.That(d14.CalculateLoad(), Is.EqualTo(64));
    }

    [Test]
    public void CanStopOnShortMap()
    {
        var d14 = GetTestInstance("Short");
        d14.CycleRoll(201000);
        Assert.That(d14.CalculateLoad(), Is.EqualTo(1));
    }

    [Test]
    public void Part2()
    {
        var d14 = RealInstance;
        d14.CycleRoll(1000000000);
        Assert.That(d14.CalculateLoad(), Is.EqualTo(90176));
    }

    private Day14 RealInstance => new(GetRealLines());
    private Day14 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));
}
