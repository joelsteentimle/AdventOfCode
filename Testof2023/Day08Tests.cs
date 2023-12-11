using AoC2023;

namespace TestOf2023;

public class Day08Tests
{
    [Test]
    public void CanReadInput()
    {
        var d8 = new Day08("Day08".ReadTestLines().ToArray());
        Assert.Multiple(() =>
        {
            Assert.That(d8.Nodes["BBB"].Left.Value.Name, Is.EqualTo("DDD"));
            Assert.That(d8.Nodes["BBB"].Right.Value.Name, Is.EqualTo("EEE"));
        });
    }

    [Test]
    public void CanCreateNode()
    {
        var node = new Day08.DesertNode("AAA = (BBB, CCC)", []);
        Assert.Multiple(() => { Assert.That(node.Name, Is.EqualTo("AAA")); });
    }

    [Test]
    public void Part1Test()
    {
        var d8 = new Day08("Day08".ReadTestLines().ToArray());
        Assert.That(d8.StepsToNode("ZZZ"), Is.EqualTo(2));
    }

    [Test]
    public void Part1()
    {
        var d8 = new Day08("Day08".ReadRealLines().ToArray());
        Assert.That(d8.StepsToNode("ZZZ"), Is.EqualTo(19667));
    }

    [Test]
    public void MultipeMovements()
    {
        var d8 = new Day08("Day08".ReadTestLines("2").ToArray());
        Func<string, bool> startNodeCondition = nodeName => nodeName[2] == 'A';
        Func<string, bool> endNodeCondition = nodeName => nodeName[2] == 'Z';
        Assert.That(d8.MultiStepsTo(startNodeCondition, endNodeCondition), Is.EqualTo(6));
    }

    [Test]
    public void OneMultiMove()
    {
        var d8 = new Day08("Day08".ReadRealLines().ToArray());
        Func<string, bool> startNodeCondition = nodeName => nodeName == "TSA";
        Func<string, bool> endNodeCondition = nodeName => nodeName[2] == 'Z';
        Assert.That(d8.MultiStepsTo(startNodeCondition, endNodeCondition), Is.EqualTo(16343));
    }

    // soon??
    [Test]
    public void FindLoops()
    {
        var d8 = new Day08("Day08".ReadRealLines().ToArray());
        bool StartNodeCondition(string nodeName) => nodeName == "AAA";

        Assert.That(d8.FindLoops(StartNodeCondition), Is.EquivalentTo(new[] { (19670, 16898) }));
    }

    [Test]
    public void JustToTest()
    {
        var d8 = new Day08("Day08".ReadTestLines("2").ToArray());
        Func<string, bool> startNodeCondition = nodeName => nodeName[2] == 'A';
        Func<string, bool> endNodeCondition = nodeName => nodeName[2] == 'Z';
        Assert.That(d8.JustToZ(startNodeCondition, endNodeCondition), Is.EqualTo(6));
    }

    [Test]
    public void Part2()
    {
        var d8 = new Day08("Day08".ReadRealLines().ToArray());
        bool StartNodeCondition(string nodeName) => nodeName[2] == 'A';
        bool EndNodeCondition(string nodeName) => nodeName[2] == 'Z';
        Assert.That(d8.JustToZ(StartNodeCondition, EndNodeCondition), Is.EqualTo(19185263738117));
    }
}