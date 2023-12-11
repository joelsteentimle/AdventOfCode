using AoC2023;

namespace TestOf2023;

internal class Day03Tests
{
    [Test]
    public void CanFindNumbers()
    {
        var d3 = new Day03("Day03".ReadTestLines().ToArray());
        var allNumbers = d3.Numbers;
        Assert.That(allNumbers, Is.EquivalentTo(new[] { 467, 114, 35, 633, 617, 58, 592, 755, 664, 598 }));
    }

    [Test]
    public void CanFindParts()
    {
        var d3 = new Day03("Day03".ReadTestLines().ToArray());
        var allNumbers = d3.Parts;
        Assert.That(allNumbers, Is.EquivalentTo(new[] { 467, 35, 633, 617, 592, 755, 664, 598 }));
    }

    [Test]
    public void CanSumParts()
    {
        var d3 = new Day03("Day03".ReadTestLines().ToArray());
        Assert.That(d3.Parts.Sum(), Is.EqualTo(4361));
    }

    [Test]
    public void SumPart1()
    {
        var d3 = new Day03("Day03".ReadRealLines().ToArray());
        Assert.That(d3.Parts.Sum(), Is.EqualTo(522726));
    }

    [Test]
    public void CanSumGears()
    {
        var d3 = new Day03("Day03".ReadTestLines().ToArray());
        Assert.That(d3.Gears.Select(g => g.Aggregate(1, (a, b) => a * b)).Sum(), Is.EqualTo(467835));
    }

    [Test]
    public void Part2()
    {
        var d3 = new Day03("Day03".ReadRealLines().ToArray());
        Assert.That(d3.Gears.Select(g => g.Aggregate(1, (a, b) => a * b)).Sum(), Is.EqualTo(81721933));
    }
}