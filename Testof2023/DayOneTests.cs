namespace TestOf2023;

public class DayOneTests
{
    private Day1 D1 = new();

    [Test]
    public void CanExtractNumbers() =>
        Assert.That(D1.LineNumbers("ue5shus53 ssa  89"), Is.EqualTo("55389".ToCharArray()));

    [Test]
    public void CanFindLineValue() =>
        Assert.That(D1.LineValue("ue5shus53 ssa  89"), Is.EqualTo(59));

    [Test]
    public void CanAddRows()
    {
        var fContent = File.ReadLines($"DataFiles\\Day1\\test.txt");
        var fileTotal = fContent.Select(D1.LineValue).Sum();
        Assert.That(fileTotal, Is.EqualTo(142));
    }

    [Test]
    public void Solution()
    {
        var fContent = File.ReadLines($"DataFiles\\Day1\\RealValue.txt");
        var fileTotal = fContent.Select(D1.LineValue).Sum();
        Assert.That(fileTotal, Is.EqualTo(53502));
    }

    [Test]
    public void CanHandleWrittenNumbers()
    {
        var fContent = File.ReadLines($"DataFiles\\Day1\\Test2.txt");
        var fileTotal = fContent.Select(D1.LineValue).Sum();
        Assert.That(fileTotal, Is.EqualTo(281));
    }

    [TestCase(0, '1')]
    [TestCase(1, null)]
    [TestCase(3, '1')]
    [TestCase(4, null)]
    public void CanHandleText(int pos,char? value) =>
        Assert.That(D1.PosAsNum("1z5one3", pos),
            Is.EqualTo(value));

    [Test]
    public void Solution2()
    {
        var fContent = File.ReadLines($"DataFiles\\Day1\\RealValue2.txt");
        var fileTotal = fContent.Select(D1.LineValue).Sum();
        Assert.That(fileTotal, Is.EqualTo(53502));
    }
}