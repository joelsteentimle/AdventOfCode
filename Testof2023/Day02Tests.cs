
using AoC2023;

namespace TestOf2023;
public class Day02Tests
{
    private readonly Day02 day02 = new();

    [Test]
    public void CanSplitToParts()
    {
        var gameLine = "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red";
        var idAndString = gameLine["Game ".Length..].Split(':');
        var gameBalls = idAndString[1].Split(',', ';');
        Assert.Multiple(() =>
        {
            Assert.That(idAndString.First(), Is.EqualTo("3"));
            Assert.That(gameBalls.Any(gb => gb == " 6 blue"));
            Assert.That(gameBalls.Any(gb => gb == " 13 green"));
        });
    }

    [Test]
    public void DictionaryMax()
    {
        var d = new Dictionary<string, int>();
        d.MaxToDictionary("blue", 5);
        Assert.That(d["blue"], Is.EqualTo(5));

        d["blue"] = 3;
        d.MaxToDictionary("blue", 23);
        Assert.That(d["blue"], Is.EqualTo(23));
    }

    [Test]
    public void FirstOfData()
    {
        day02.AddAllGames("Day02".ReadTestLines());
        Assert.That(day02.Games[2]["green"], Is.EqualTo(3));
    }

    [Test]
    public void CanFilterByLimits()
    {
        day02.AddAllGames("Day02".ReadTestLines());
        var gamesMatchingLimit
            = day02.GamesMatchingLimits(
                ("red", 12),
                ("green", 13),
                ("blue", 14)).ToArray();

        Assert.That(gamesMatchingLimit, Is.EquivalentTo(new[] { 1, 2, 5 }));
        Assert.That(gamesMatchingLimit.Sum(), Is.EqualTo(8));
    }

    [Test]
    public void Solution()
    {
        day02.AddAllGames("Day02".ReadRealLines());
        var gamesMatchingLimit
            = day02.GamesMatchingLimits(
                ("red", 12),
                ("green", 13),
                ("blue", 14));
        Assert.Multiple(() =>
        {
            Assert.That(gamesMatchingLimit.Sum(), Is.EqualTo(2600));
            Assert.That(day02.PowerSum, Is.EqualTo(86036));
        });
    }

    [Test]
    public void Power()
    {
        day02.AddAllGames("Day02".ReadTestLines());
        Assert.Multiple(() =>
        {
            Assert.That(day02.GamePower(1), Is.EqualTo(48));
            Assert.That(day02.GamePower(2), Is.EqualTo(12));
            Assert.That(day02.GamePower(3), Is.EqualTo(1560));
            Assert.That(day02.PowerSum, Is.EqualTo(2286));
        });
    }
}
