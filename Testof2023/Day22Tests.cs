using AoC2023;
using SupportCode;

namespace TestOf2023;

public class Day22Tests : DayTests
{
    [Test]
    public void CanReadLinesAndMatrix() =>
        Assert.Multiple(() =>
        {
            Assert.That(RealInstance.TextGroups, Has.Count.GreaterThan(2));
            Assert.That(GetTestInstance().Map.GetLength(0), Is.GreaterThan(10));
        });

    private Day22 RealInstance => new(GetRealLines());

    private Day22 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));

}
