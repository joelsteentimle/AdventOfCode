using AoC2024;
using SupportCode;

namespace TestOf2024;

public class Day06Tests
{
    [Test]
    public void Test1()
    {
        var d6 = new Day06("Day06".ReadTestLines());
        Assert.That(d6.GuardVisitedSquares(), Is.EqualTo(143));
    }

}