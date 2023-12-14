using AoC2023;

namespace TestOf2023;

public class Day15Tests : DayTests
{
    [Test]
    public void CanGetRealMap() => Assert.That(RealInstance.Map, Has.Length.AtLeast(50));

    [Test]
    public void CanGetTestMap() => Assert.That(GetTestInstance().Map, Has.Length.AtLeast(5));

    private Day15 RealInstance => new(GetRealLines());
    private Day15 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));
}
