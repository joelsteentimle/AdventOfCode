using AoC2023;
using AoC2023.Graph;

namespace TestOf2023;

public class Day18Tests : DayTests
{
    [Test]
    public void CanAddDirection() =>
        Assert.Multiple(() =>
        {
            Assert.That((Direction)((int)(Direction.East + 1) % 4) , Is.EqualTo(Direction.South));
            Assert.That((Direction)((int)(Direction.North +3)% 4), Is.EqualTo(Direction.West));
            Assert.That((Direction)((int)(Direction.North + 1)% 4), Is.EqualTo(Direction.East));
            Assert.That((Direction)((int)(Direction.East +3)% 4), Is.EqualTo(Direction.North));
        });

    [Test]
    public void CanFigureOutInside()
    {
        var d18 = GetTestInstance();
        d18.GenerateFieldAndStart();
        Assert.That(d18.RightIsIn);
    }

    [Test]
    public void CanCountTrench()
    {
        var d18 = GetTestInstance();
        d18.GenerateFieldAndStart();
        d18.WalkThroughTheTrench();
        Assert.That(d18.TrenchVolume(), Is.EqualTo(38));
    }

    [Test]
    public void Part1Test()
    {
        var d18 = GetTestInstance();
        d18.GenerateFieldAndStart();
        d18.WalkThroughTheTrench();
        d18.DigInside();
        Assert.That(d18.PoolVolume(), Is.EqualTo(62));
    }

    [Test]
    public void Part1()
    {
        var d18 = RealInstance;
        d18.GenerateFieldAndStart();
        d18.WalkThroughTheTrench();
        d18.DigInside();
        Assert.That(d18.PoolVolume(), Is.EqualTo(106453));
    }



    private Day18 RealInstance => new(GetRealLines());

    private Day18 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));

}
