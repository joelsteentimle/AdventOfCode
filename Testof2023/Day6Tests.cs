using AoC2023;

namespace TestOf2023;

public class Day6Tests
{
    [Test]
    public void CanGetTimesAndDistance()
    {
        var d6 = new Day6("Day6".ReadTestLines());
        Assert.That(d6.Races, Is.EquivalentTo(new List<(int, int)> { (7, 9), (15, 40), (30, 200) }));
    }

    [Test]
    public void DoFirstRace()
    {
        Assert.That(Day6.CalculateRace((7, 9)), Is.EqualTo(4));
    }

    [Test]
    public void Part1Test()
    {
        var d6 = new Day6("Day6".ReadTestLines());
        Assert.That(d6.TotalRaceWinnings(), Is.EqualTo(288));
    }

    [Test]
    public void Part1()
    {
        var d6 = new Day6("Day6".ReadRealLines());
        Assert.That(d6.TotalRaceWinnings(), Is.EqualTo(1108800));
    }

    [Test]
    public void Part2CanReadRace()
    {
        var d6 = new Day6("Day6".ReadTestLines(), true);
        Assert.That(d6.Races, Is.EquivalentTo(new List<(int, int)> { (71530, 940200) }));
    }

    [Test]
    [Order(1)]
    public void Part2Test()
    {
        var d6 = new Day6("Day6".ReadTestLines(), true);
        Assert.That(d6.TotalRaceWinnings(), Is.EqualTo(71503));
    }

    [Test]
    [Order(2)]
    public void Part2()
    {
        var d6 = new Day6("Day6".ReadRealLines(), true);
        Assert.That(d6.TotalRaceWinnings(), Is.EqualTo(36919753));
    }
}