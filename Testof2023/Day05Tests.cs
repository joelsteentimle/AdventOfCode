
using AoC2023;

namespace TestOf2023;
public class Day05Tests
{
    [Test]
    public void CanReadFirstLine()
    {
        var uut = new Day05();
        var (type, ids) = uut.ReadInitialLine("seeds: 79 14 55 13");
        Assert.Multiple(() =>
        {
            Assert.That(type, Is.EqualTo("seed"));
            Assert.That(ids.Select(e => e.Start), Is.EquivalentTo(new[] { 79, 14, 55, 13 }));
        });
    }

    [Test]
    public void CanSplitToSections()
    {
        var input = new[]
        {
            "", "seed-to-soil map:", "50 98 2", "52 50 48", "", "soil-to-fertilizer map:", "0 15 37", "37 52 2",
            "39 0 15"
        };
        var parsed = input.Aggregate(new List<List<string>> { new() },
            (list, value) =>
            {
                if (value.Trim() == string.Empty)
                {
                    if (list.Last().Count != 0)
                        list.Add([]);
                }
                else
                    list.Last().Add(value);

                return list;
            });
        Assert.Multiple(() =>
        {
            Assert.That(parsed, Has.Count.EqualTo(2));
            Assert.That(parsed[1][0], Is.EqualTo("soil-to-fertilizer map:"));
            Assert.That(parsed[1][2], Is.EqualTo("37 52 2"));
        });
    }

    [Test]
    public void ValidateMap()
    {
        var map = new Day05.Element.Map(["50 98 2", "52 50 48"]);
        Assert.Multiple(() =>
        {
            Assert.That(map.GetTargetRanges(new Day05.ElementRange(10, 10)).First().Start, Is.EqualTo(10));
            Assert.That(map.GetTargetRanges(new Day05.ElementRange(98, 98)).First().Start, Is.EqualTo(50));
            Assert.That(map.GetTargetRanges(new Day05.ElementRange(99, 99)).First().Start, Is.EqualTo(51));
            Assert.That(map.GetTargetRanges(new Day05.ElementRange(52, 52)).First().Start, Is.EqualTo(54));
        });
    }

    [Test]
    public void TestData()
    {
        var uut = new Day05("Day05".ReadTestLines());
        var lowest = uut.GetTargetIdsFromRequested("location")
            .Select(elemRange => elemRange.Start).Min();
        Assert.That(lowest, Is.EqualTo(35));
    }

    [Test]
    public void Part1()
    {
        var uut = new Day05("Day05".ReadRealLines());
        var lowest = uut.GetTargetIdsFromRequested("location").Select(elemRange => elemRange.Start).Min();
        Assert.That(lowest, Is.EqualTo(157211394));
    }

    [Test]
    public void TestStep()
    {
        var uut = new Day05("Day05".ReadTestLines());
        var lowest = uut.GetTargetIdsFromRequested("soil").Select(e => e.Start);
        Assert.That(lowest, Is.EquivalentTo(new[] { 13, 14, 57, 81 }));
    }

    [Test]
    public void TestPart2()
    {
        var uut = new Day05("Day05".ReadTestLines(), true);
        var lowest = uut.GetTargetIdsFromRequested("location").Select(elemRange => elemRange.Start).Min();
        Assert.That(lowest, Is.EqualTo(46));
    }

    [Test]
    public void Part2()
    {
        var uut = new Day05("Day05".ReadRealLines(), true);
        var lowest = uut.GetTargetIdsFromRequested("location").Select(elemRange => elemRange.Start).Min();
        Assert.That(lowest, Is.EqualTo(50855035));
    }
}
