using AoC2023;

namespace TestOf2023;

class Day14Tests : DayTests
{
    [Test]
    public void Smoke()
    {
        Assert.That(RealInstance, Is.Not.Null);
        Assert.That(TestInstance, Is.Not.Null);
    }
    private Day14 RealInstance => new(RealLines);
    private Day14 TestInstance => new(TestLines);
}