using AoC2023;
using SupportCode;

namespace TestOf2023;

public class Day15Tests : DayTests
{
    [Test]
    public void CanGetRealMap() => Assert.That(RealInstance.Groups, Has.Count.AtLeast(50));

    [Test]
    public void CanGetTestMap() => Assert.That(GetTestInstance().Groups[2], Is.EqualTo("qp=3"));

    [Test]
    public void CanHash() => Assert.That(Day15.Hash("HASH"), Is.EqualTo(52));

    [Test]
    public void VerifyHash() => Assert.That(Day15.Hash("rn"), Is.EqualTo(0));

    [Test]
    public void CanSumHash() => Assert.That(GetTestInstance().HashSum, Is.EqualTo(1320));

    [Test]
    public void Part1() => Assert.That(RealInstance.HashSum, Is.EqualTo(504449));

    [Test]
    public void Part2Test() => Assert.That(GetTestInstance().LensSum(), Is.EqualTo(145));

    [Test]
    public void Part2() => Assert.That(RealInstance.LensSum(), Is.EqualTo(262044));

    private Day15 RealInstance => new(GetRealLines());

    private Day15 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));
}
