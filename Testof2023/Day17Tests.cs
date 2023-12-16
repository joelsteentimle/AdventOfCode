using AoC2023;

namespace TestOf2023;

public class Day17Tests : DayTests
{
    [Test]
    public void CanReadLinesAndMatrix() =>
        Assert.Multiple(() =>
        {
            Assert.That(RealInstance.TextGroups, Has.Count.GreaterThan(2));
            Assert.That(GetTestInstance().Map.GetLength(0), Is.GreaterThan(10));
        });

    private Day17 RealInstance => new(GetRealLines());

    private Day17 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));

}
