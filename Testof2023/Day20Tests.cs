using AoC2023;

namespace TestOf2023;

public class Day20Tests : DayTests
{
    [Test]
    public void Part1Test()
        => Assert.That(GetTestInstance().SentPulses(),Is.EqualTo(32000000));

    [Test]
    public void Part1Test2()
        => Assert.That(GetTestInstance("2").SentPulses(),Is.EqualTo(11687500));

    [Test]
    public void Part1()
        => Assert.That(RealInstance.SentPulses(),Is.EqualTo(856482136));


    private Day20 RealInstance => new(GetRealLines());

    private Day20 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));

}
