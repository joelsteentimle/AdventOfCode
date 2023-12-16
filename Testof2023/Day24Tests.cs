using AoC2023;

namespace TestOf2023;

public class Day24Tests : DayTests
{
    [Test]
    public void CanReadLinesAndMatrix() =>
        Assert.Multiple(() =>
        {
            Assert.That(RealInstance.TextGroups, Has.Count.GreaterThan(2));
            Assert.That(GetTestInstance().Map.GetLength(0), Is.GreaterThan(10));
        });

    private Day24 RealInstance => new(GetRealLines());

    private Day24 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));

}
