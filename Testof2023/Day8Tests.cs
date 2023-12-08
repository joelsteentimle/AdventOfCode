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
        Assert.That(d8.Nodes["BBB"].Left, Is.EqualTo("DDD"));
        Assert.That(d8.Nodes["BBB"].Right, Is.EqualTo("EEE"));
    });
    }

    [Test]
    public void CanCreateNode()
    {
        var node = new Day8.DesertNode("AAA = (BBB, CCC)");
            Assert.Multiple(() =>
            {
                Assert.That(node.Name,Is.EqualTo("AAA"));
                Assert.That(node.Left,Is.EqualTo("BBB"));
                Assert.That(node.Right,Is.EqualTo("CCC"));
            });
    }
}