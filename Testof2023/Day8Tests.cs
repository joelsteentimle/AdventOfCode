using AoC2023;

namespace TestOf2023;

public class Day8Tests
{
    [Test]
    public void CanReadInput()
    {
        var d8 = new Day8("Day8".ReadTestLines().ToArray());
        Assert.Multiple(() =>
        {
            Assert.That(d8.Nodes["BBB"].Left.Value.Name, Is.EqualTo("DDD"));
            Assert.That(d8.Nodes["BBB"].Right.Value.Name, Is.EqualTo("EEE"));
        });
    }

    [Test]
    public void CanCreateNode()
    {
        var node = new Day8.DesertNode("AAA = (BBB, CCC)",[]);
        Assert.Multiple(() =>
        {
            Assert.That(node.Name, Is.EqualTo("AAA"));
        });
    }
    
    [Test]
    public void Part1Test()
    {
        var d8 = new Day8("Day8".ReadTestLines().ToArray());
        Assert.That(d8.StepsToNode("ZZZ"), Is.EqualTo(2));
    }
     
    [Test]
    public void Part1()
    {
        var d8 = new Day8("Day8".ReadRealLines().ToArray());
        Assert.That(d8.StepsToNode("ZZZ"), Is.EqualTo(19667));
    }

    [Test]
    public void MultipeMovements()
    {
        var d8 = new Day8("Day8".ReadTestLines("2").ToArray());
        Func<string, bool> startNodeCondition = node => node[2]=='A';
        Func<string, bool> endNodeCondition = node => node[2] == 'Z';
        Assert.That(d8.MultiStepsTo(startNodeCondition, endNodeCondition), Is.EqualTo(6));

    }    
    
    [Test]
    public void Part2()
    {
        var d8 = new Day8("Day8".ReadRealLines().ToArray());
        Func<string, bool> startNodeCondition = node => node[2]=='A';
        Func<string, bool> endNodeCondition = node => node[2] == 'Z';
        Assert.That(d8.MultiStepsTo(startNodeCondition, endNodeCondition), Is.EqualTo(6));
    
    }
}