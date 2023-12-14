namespace TestOf2023;

using AoC2023;

public class Day13Tests : DayTests
{
    [Test]
    public void GetCorrectNumberOfFields() => Assert.That(TestInstance.Fields, Has.Count.EqualTo(2));

    [Test]
    public void CanFindVFoldInField()
    {
        var f1 = TestInstance.Fields[0];
        Assert.Multiple(() =>
        {
            Assert.That(f1.VFold, Is.EqualTo(5));
            Assert.That(f1.HFold, Is.Null);
        });
    }

    [Test]
    public void CanFindHFoldInField()
    {
        var f2 = TestInstance.Fields[1];
        Assert.Multiple(() =>
        {
            Assert.That(f2.HFold, Is.EqualTo(4));
            Assert.That(f2.VFold, Is.Null);
        });
    }

    [Test]
    public void Part1Test() => Assert.That(TestInstance.Sum, Is.EqualTo(405));

    [Test]
    public void Part1() => Assert.That(RealInstance.Sum, Is.EqualTo(33975));

    [Test]
    public void Part2Test() => Assert.That(TestInstance.SmudgeSum, Is.EqualTo(400));


    [Test]
    public void Part2() => Assert.That(RealInstance.SmudgeSum, Is.EqualTo(29083));

    private Day13 RealInstance => new(GetRealLines());
    private Day13 TestInstance => new(GetTestLines());
}
