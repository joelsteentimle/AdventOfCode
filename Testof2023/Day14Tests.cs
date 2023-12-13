namespace TestOf2023;

using AoC2023;

public class Day14Tests : DayTests
{
    [Test]
    public void Smoke() =>
        Assert.Multiple(() =>
        {
            Assert.That(RealInstance, Is.Not.Null);
            Assert.That(TestInstance, Is.Not.Null);
        });

    private Day14 RealInstance => new(RealLines);
    private Day14 TestInstance => new(TestLines);
}
