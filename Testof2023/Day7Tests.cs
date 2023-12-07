using AoC2023;

namespace TestOf2023;

public class Day7Tests
{
    private readonly List<Day7.Hand> _testHands =
    [
        new Day7.Hand("KK677", 28),
        new Day7.Hand("32T3K", 765),
        new Day7.Hand("T55J5", 684),
        new Day7.Hand("KTJJT", 220),
        new Day7.Hand("QQQJA", 483)
    ];

    [Test]
    public void CanGetHandValue()
    {
        Assert.Multiple(() =>
        {
            Assert.That(new Day7.Hand("KK677", 28).HandValue, Is.EqualTo(3));
            Assert.That(new Day7.Hand("32T3K", 765).HandValue, Is.EqualTo(2));
            Assert.That(new Day7.Hand("T55J5", 684).HandValue, Is.EqualTo(4));
            Assert.That(new Day7.Hand("KTJJT", 220).HandValue, Is.EqualTo(3));
            Assert.That(new Day7.Hand("QQQJA", 483).HandValue, Is.EqualTo(4));
        });
    }

    [Test]
    public void SameHandIsSame()
    {
        Assert.That(new Day7.Hand("QQQJA", 483).CompareTo(new Day7.Hand("QQQJA", 555))
            , Is.EqualTo(0));
    }

    [Test]
    public void CanOrderHands()
    {
        var orderedHands = _testHands.Order().ToList();
        Assert.Multiple(() =>
        {
            Assert.That(orderedHands, Has.Count.EqualTo(5));
            Assert.That(orderedHands[0].Cards, Is.EqualTo(new[] { '3', '2', 'T', '3', 'K' }));
            Assert.That(orderedHands[1].Cards, Is.EqualTo(new[] { 'K', 'T', 'J', 'J', 'T' }));
            Assert.That(orderedHands[2].Cards, Is.EqualTo(new[] { 'K', 'K', '6', '7', '7' }));
            Assert.That(orderedHands[3].Cards, Is.EqualTo(new[] { 'T', '5', '5', 'J', '5' }));
            Assert.That(orderedHands[4].Cards, Is.EqualTo(new[] { 'Q', 'Q', 'Q', 'J', 'A' }));
        });
    }

    [Test]
    public void TestPart1()
    {
        var d7 = new Day7("Day7".ReadTestLines());
        Assert.That(d7.TotalWinnings(), Is.EqualTo(6440));
    }

    [Test]
    public void Part1()
    {
        var d7 = new Day7("Day7".ReadRealLines());
        Assert.That(d7.TotalWinnings(), Is.EqualTo(248453531));
    }

    [Test]
    public void TestPart2()
    {
        var d7 = new Day7("Day7".ReadTestLines(), true);
        Assert.That(d7.TotalWinnings(), Is.EqualTo(5905));
    }

    [Test]
    public void Part2()
    {
        var d7 = new Day7("Day7".ReadRealLines(), true);
        Assert.That(d7.TotalWinnings(), Is.EqualTo(248781813));
    }
}