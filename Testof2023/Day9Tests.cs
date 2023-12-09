using AoC2023;

namespace TestOf2023;

public class Day9Tests
{
    [Test]
    public void CanRead()
    {
        var d9 = new Day9("Day9".ReadTestLines());
    }

    [Test]
    public void CanCountLine()
    {
        var d9 = new Day9("Day9".ReadTestLines());
        Assert.Multiple(() =>
        {
            Assert.That(d9.GetNextValue([0, 3, 6, 9, 12, 15]).right, Is.EqualTo(18));
            Assert.That(d9.GetNextValue([1, 3, 6, 10, 15, 21]).right, Is.EqualTo(28));
            Assert.That(d9.GetNextValue([10, 13, 16, 21, 30, 45]).right, Is.EqualTo(68));

        });
    }

    [Test]
    public void CanCountLeft()
    {
        var d9 = new Day9("Day9".ReadTestLines());
        Assert.That(d9.GetNextValue([10, 13, 16, 21, 30, 45]).left, Is.EqualTo(5));
    }


    [Test]
    public void Part1Test()
    {
        var d9 = new Day9("Day9".ReadTestLines());

        Assert.That(d9.SumNexts().right, Is.EqualTo(114));
    }

    [Test]
    public void Part1()
    {
        var d9 = new Day9("Day9".ReadRealLines());

        Assert.That(d9.SumNexts().right, Is.EqualTo(114));
    }

    [Test]
    public void Part2Test()
    {
        var d9 = new Day9("Day9".ReadTestLines());
        Assert.That(d9.SumNexts().left, Is.EqualTo(2));
    }

    [Test]
    public void Part2()
    {
        var d9 = new Day9("Day9".ReadRealLines());
        Assert.That(d9.SumNexts().left, Is.EqualTo(114));
    }
}