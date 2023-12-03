using AoC2023;

namespace TestOf2023;

internal class Day2Tests
{
    private readonly Day2 _day2 = new();

    [Test]
    public void CanSplitToParts()
    {
        var gameLine = "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red";
        var idAndString = gameLine["Game ".Length ..].Split(':');
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
        AddFromFile(@"DataFiles\Day2\Example.txt");
        Assert.That(_day2.Games[2]["green"], Is.EqualTo(3));
    }

    private void AddFromFile(string filePath)
    {
        var fContent = filePath.ReadFileAsLines();
        _day2.AddAllGames(fContent);
    }

    [Test]
    public void CanFilterByLimits()
    {
        AddFromFile(@"DataFiles\Day2\Example.txt");
        var gamesMatchingLimit
            = _day2.GamesMatchingLimits(
                ("red", 12),
                ("green", 13),
                ("blue", 14)).ToArray();

        Assert.That(gamesMatchingLimit, Is.EquivalentTo(new[] { 1, 2, 5 }));
        Assert.That(gamesMatchingLimit.Sum(), Is.EqualTo(8));
    }

    [Test]
    public void Solution()
    {
        AddFromFile(@"DataFiles\Day2\RealValue.txt");
        var gamesMatchingLimit
            = _day2.GamesMatchingLimits(
                ("red", 12),
                ("green", 13),
                ("blue", 14));

        Assert.That(gamesMatchingLimit.Sum(), Is.EqualTo(2600));
        Assert.That(_day2.PowerSum, Is.EqualTo(86036));
    }

    [Test]
    public void Power()
    {
        AddFromFile(@"DataFiles\Day2\Example.txt");

        Assert.That(_day2.GamePower(1), Is.EqualTo(48));
        Assert.That(_day2.GamePower(2), Is.EqualTo(12));
        Assert.That(_day2.GamePower(3), Is.EqualTo(1560));
        Assert.That(_day2.PowerSum, Is.EqualTo(2286));
    }
}