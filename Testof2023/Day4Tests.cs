using AoC2023;

namespace TestOf2023;

public class Day4Tests
{
    [Test]
    public void CanParseMyAndWinners()
    {
        Day4.Card testCard = new("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53");
        Assert.Multiple(() =>
        {
            Assert.That(testCard.Number, Is.EqualTo(1));
            Assert.That(testCard.Winner, Is.EquivalentTo(new[] { 41, 48, 83, 86, 17 }));
            Assert.That(testCard.Mine, Is.EquivalentTo(new[] { 83, 86, 6, 31, 17, 9, 48, 53 }));
        });
    }

    [Test]
    public void CanGetWorth()
    {
        Day4.Card testCard1 = new("Card 1: 41 48 83 86 17 | 83 86  6 31 17  9 48 53");
        Day4.Card testCard5 = new("Card 5: 87 83 26 28 32 | 88 30 70 12 93 22 82 36");
        Assert.Multiple(() =>
        {
            Assert.That(testCard1.Points, Is.EqualTo(8));
            Assert.That(testCard5.Points, Is.EqualTo(0));
        });
    }

    [Test]
    public void Part1Test()
    {
        Day4 test = new("Day4".ReadTestLines());
        Assert.That(test.TotalSum(), Is.EqualTo(13));
    }

    [Test]
    public void Part1()
    {
        Day4 test = new("Day4".ReadRealLines());
        Assert.That(test.TotalSum(), Is.EqualTo(22897));
    }

    [Test]
    public void Part2Test()
    {
        Day4 test = new("Day4".ReadTestLines());
        Assert.That(test.TotalScratchCards(), Is.EqualTo(30));
    }

    [Test]
    public void Part2()
    {
        Day4 test = new("Day4".ReadRealLines());
        Assert.That(test.TotalScratchCards(), Is.EqualTo(5095824));
    }
}