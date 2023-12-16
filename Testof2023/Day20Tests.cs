using AoC2023;

namespace TestOf2023;

public class Day20Tests : DayTests
{
    [Test]
    public void CanReadLinesAndMatrix() =>
        Assert.Multiple(() =>
        {
            Assert.That(RealInstance.TextGroups, Has.Count.GreaterThan(2));
            Assert.That(GetTestInstance().Map.GetLength(0), Is.GreaterThan(10));
        });

    private Day20 RealInstance => new(GetRealLines());

    private Day20 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));

}
