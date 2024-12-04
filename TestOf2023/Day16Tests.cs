using AoC2023;
using AoC2023.Graph;
using SupportCode;

namespace TestOf2023;

public class Day16Tests : DayTests
{
    [Test]
    public void Part1Test() =>
        Assert.That(GetTestInstance().Shine(new Position(0, 0), Direction.East), Is.EqualTo(46));

    [Test]
    public void Part1() =>
        Assert.That(RealInstance.Shine(new Position(0, 0), Direction.East), Is.EqualTo(6361));

    [Test]
    public void Part2Test()
        => Assert.That(GetTestInstance().MaxEnergized(), Is.EqualTo(51));

    [Test]
    public void Part2()
        => Assert.That(RealInstance.MaxEnergized(), Is.EqualTo(6701));


    private Day16 RealInstance => new(GetRealLines());

    private Day16 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));
}
