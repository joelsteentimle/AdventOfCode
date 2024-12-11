using AoC2023;
using SupportCode;

namespace TestOf2023;

public class Day21Tests : DayTests
{
    [Test]
    public void Part1Test()
        => Assert.That(GetTestInstance().CountEndPlots(16), Is.EqualTo(1642));

    [Test]
    public void Part1()
        => Assert.That(RealInstance.CountEndPlots(64), Is.EqualTo(3658));

    private Day21 RealInstance => new(GetRealLines());

    private Day21 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));

}
