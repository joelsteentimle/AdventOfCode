using AoC2023;
using AoC2023.Graph;

namespace TestOf2023;

public class Day17Tests : DayTests
{
    [Test]
    public void CanReadMap() =>
        Assert.Multiple(() =>
        {
            Assert.That(GetTestInstance().Map[0,0], Is.EqualTo(2));
            Assert.That(RealInstance.Map[1,1], Is.EqualTo(2));
        });

    [Test]
    public void Part1Test()
    {
        var testInstance = GetTestInstance();
        Assert.That(testInstance.Dijkstra(new Position(0, 0),
                new Position( testInstance.Map.GetLength(0)
                ,testInstance.Map.GetLength(1))),
                Is.EqualTo(102));
    }


    private Day17 RealInstance => new(GetRealLines());

    private Day17 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));
}
