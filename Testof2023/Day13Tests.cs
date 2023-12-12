using AoC2023;

namespace TestOf2023;

public class Day13Tests
{
    [Test]
    public void CanGetResult()
    {
        var d13 = GetTest();
        Assert.That(d13.Result, Is.EqualTo(5555));
    }

    [Test]
    public void GetCorrectNumberOfFields()
    {
        Assert.That(GetTest().Fields.Count, Is.EqualTo(2));
    }

    [Test]
    public void CanFindVFoldInField()
    {
        var F1 = GetTest().Fields[0];
        Assert.That(F1.VFold, Is.EqualTo(5));
        Assert.That(F1.HFold, Is.Null);
    }
    [Test]
    public void CanFindHFoldInField()
    {
        var F2 = GetTest().Fields[1];
        Assert.That(F2.HFold, Is.EqualTo(4));
        Assert.That(F2.VFold, Is.Null);
    }

    [Test]
    public void Part1Test()
    {
        Assert.That(GetTest().Sum, Is.EqualTo(405));
    }

    [Test]
    public void Part1()
    {
        Assert.That(GetReal().Sum, Is.EqualTo(33975));
    }
    
    [Test]
    public void Part2Test()
    {
        Assert.That(GetTest().SmudgeSum, Is.EqualTo(400));
    }


    [Test]
    public void Part2()
    {
        Assert.That(GetReal().SmudgeSum, Is.EqualTo(29083));
    }
    

    private static Day13 GetTest()
    {
        var d13 = new Day13("Day13".ReadTestLines());
        return d13;
    }
    
    private static Day13 GetReal()
    {
        var d13 = new Day13("Day13".ReadRealLines());
        return d13;
    }
}