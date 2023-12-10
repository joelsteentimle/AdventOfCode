using AoC2023;

namespace TestOf2023;

public class Day10Test
{
    [Test]
    public void CanFindStart()
    {
        var d10 = new Day10("Day10".ReadTestLines());
        Assert.That(d10.Current,Is.EqualTo((1,1))); 
    }

    [Test]
    public void CanFindStartConnections()
    {
        var d10 = new Day10("Day10".ReadTestLines());
        var (y,x) = d10.Current;
        Assert.That(d10.Map[y,x].Connected
            ,Is.EquivalentTo(new []{(1,0),(0,1)}));
    }

    [Test]
    public void Part1Test()
    {
        var d10 = new Day10("Day10".ReadTestLines());
        Assert.That(d10.GetMaxDistance(d10.Current),Is.EqualTo(4)); 
    }
    
    [Test]
    public void Part1()
    {
        var d10 = new Day10("Day10".ReadRealLines());
        Assert.That(d10.GetMaxDistance(d10.Current),Is.EqualTo(4)); 
    }
}