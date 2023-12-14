
using AoC2023;

namespace TestOf2023;
public class Day12Tests
{
    [Test]
    public void CanReadInput()
    {
        var d12 = new Day12("Day12".ReadTestLines());
        Assert.Multiple(() =>
        {
            Assert.That(d12.Fields[0].Pattern, Is.EqualTo("???.###"));
            Assert.That(d12.Fields[0].Springs, Is.EqualTo(new[] { 1, 1, 3 }));
        });
    }

    [Test]
    public void CanMatchPattern()
    {
        var d12f = new Day12.Field("?.???## 1,1,4");
        Assert.That(d12f.MatchStart(3, 1));
    }

    [Test]
    public void CanFindForOneLine() => Assert.Multiple(() =>
    {
        Assert.That(LineVersions("???.### 1,1,3"), Is.EqualTo(1));
        Assert.That(LineVersions(".??..??...?##. 1,1,3"), Is.EqualTo(4));
        Assert.That(LineVersions("?###???????? 3,2,1"), Is.EqualTo(10));
        Assert.That(LineVersions("???#?? 3"), Is.EqualTo(3));
        Assert.That(LineVersions(".?##?????#?? 3,3"), Is.EqualTo(6));
    });

    [Test]
    public void CanFindFoldedlLine() => Assert.Multiple(() =>
    {
        Assert.That(FoldedLineVersions("???.### 1,1,3", 5), Is.EqualTo(1));
        Assert.That(FoldedLineVersions(".??..??...?##. 1,1,3", 5), Is.EqualTo(16384));
        Assert.That(FoldedLineVersions("?#?#?#?#?#?#?#? 1,3,1,6", 5), Is.EqualTo(1));
        Assert.That(FoldedLineVersions("????.#...#... 4,1,1", 5), Is.EqualTo(16));
        Assert.That(FoldedLineVersions("????.######..#####. 1,6,5", 5), Is.EqualTo(2500));
        Assert.That(FoldedLineVersions("?###???????? 3,2,1 1,6,5", 5), Is.EqualTo(506250));
    });


    private static long FoldedLineVersions(string line, int times)
    {
        var f = new Day12.Field(line);
        f.Fold(times);
        return f.TotalCombinations();
    }

    private static long LineVersions(string line) => new Day12.Field(line).TotalCombinations();

    [Test]
    public void TestAllRows()
    {
        var d12 = new Day12("Day12".ReadTestLines());

        Assert.Multiple(() =>
        {
            Assert.That(d12.Fields[0].TotalCombinations(), Is.EqualTo(1));
            Assert.That(d12.Fields[1].TotalCombinations(), Is.EqualTo(4));
            Assert.That(d12.Fields[2].TotalCombinations(), Is.EqualTo(1));
            Assert.That(d12.Fields[3].TotalCombinations(), Is.EqualTo(1));
            Assert.That(d12.Fields[4].TotalCombinations(), Is.EqualTo(4));
            Assert.That(d12.Fields[5].TotalCombinations(), Is.EqualTo(10));
        });
    }

    [Test]
    public void Part1Test()
    {
        var d12 = new Day12("Day12".ReadTestLines());
        Assert.That(d12.GetSum, Is.EqualTo(21));
    }

    [Test]
    public void Part1()
    {
        var d12 = new Day12("Day12".ReadRealLines());
        Assert.That(d12.GetSum, Is.EqualTo(7195));
    }

    [Test]
    public void Part2()
    {
        var d12 = new Day12("Day12".ReadRealLines());
        d12.Fold(5);
        Assert.That(d12.GetSum, Is.EqualTo(33992866292225));
    }
}
