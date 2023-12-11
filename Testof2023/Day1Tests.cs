using AoC2023;

namespace TestOf2023;

public class Day01Tests
{
    private readonly Day01 _d1 = new();

    [Test]
    public void CanExtractNumbers() =>
        Assert.That(Day01.LineNumbers("ue5shus53 ssa  89"), Is.EqualTo("55389".ToCharArray()));

    [Test]
    public void CanFindLineValue() =>
        Assert.That(_d1.LineValue("ue5shus53 ssa  89"), Is.EqualTo(59));

    [Test]
    public void CanAddRows()
    {
        var fContent = "Day01".ReadTestLines();
        var fileTotal = fContent.Select(_d1.LineValue).Sum();
        Assert.That(fileTotal, Is.EqualTo(142));
    }

    [Test]
    public void Solution()
    {
        var fContent = "Day01".ReadRealLines();
        var fileTotal = fContent.Select(_d1.LineValue).Sum();
        Assert.That(fileTotal, Is.EqualTo(54649));
    }

    [Test]
    public void CanHandleWrittenNumbers()
    {
        var fContent = "Day01".ReadTestLines("2");
        Assert.That(_d1.SumTheFile(fContent), Is.EqualTo(281));
    }

    [TestCase(0, '1')]
    [TestCase(1, null)]
    [TestCase(3, '1')]
    [TestCase(4, null)]
    public void CanHandleText(int pos, char? value) =>
        Assert.That(Day01.PosAsNum("1z5one3", pos),
            Is.EqualTo(value));

    [Test]
    public void Solution2()
    {
        var fContent = "Day01".ReadRealLines(); 
        Assert.That(_d1.SumTheFile(fContent), Is.EqualTo(54649));
    }

    [Test]
    public void HomegrownTest3()
    {
        var fContent = "Day01".ReadTestLines("3");            
        Assert.That(_d1.SumTheFile(fContent), Is.EqualTo(231));
    }
}