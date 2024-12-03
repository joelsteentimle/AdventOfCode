using AoC2023;
using SupportCode;

namespace TestOf2023;

public class Day19Tests : DayTests
{

    [Test]
    public void CanUseRule()
    {
        var r1 = new Day19.Sorter.Rule("a<2006:qkq");
        var nullPart = new Day19.Part(0, 0, 0, 0);
        Assert.Multiple(() =>
        {
            Assert.That(r1.Matches(nullPart), Is.True);
            Assert.That(r1.Matches(nullPart with { A = 2000 }), Is.True);
            Assert.That(r1.Matches(nullPart with { A = 2006 }), Is.False);
            Assert.That(r1.Matches(nullPart with { A = 20006 }), Is.False);
        });
    }

    [Test]
    public void CanUseSorter()
    {
        var s1 = new Day19.Sorter("px{a<2006:qkq,m>2090:A,rfg}");

        var nullPart = new Day19.Part(0, 0, 0, 0);
        ;

        Assert.Multiple(() =>
        {
            Assert.That(s1.Sort(nullPart), Is.EqualTo("qkq"));
            Assert.That(s1.Sort(nullPart with { A = 2006, M = 2091 }), Is.EqualTo("A"));
            Assert.That(s1.Sort(nullPart with { A = 2006, M = 2081 }), Is.EqualTo("rfg"));
        });
    }

    [Test]
    public void CanParsePart()
    {
        var part = Day19.ParsePart("{x=787,m=2655,a=1222,s=2876}");

        Assert.Multiple(() =>
        {
            Assert.That(part, Is.EqualTo(new Day19.Part(787, 2655, 1222, 2876)));
            Assert.That(part, Is.Not.EqualTo(new Day19.Part(787, 2656, 1222, 2876)));
        });
    }

    [Test]
    public void Part1Test() =>
        Assert.That(GetTestInstance().GetSum(), Is.EqualTo(19114));

    [Test]
    public void Part1() =>
        Assert.That(RealInstance.GetSum(), Is.EqualTo(263678));

    [Test]
    public void CanProcessRange()
    {
        var r1 = new Day19.Sorter.Rule("a<2006:qkq");
        var r2 = new Day19.Sorter.Rule("s>3448:A");
        var startParts = new Day19.RangeParts();
        var (splitted, left) = r1.Split(startParts);
        var afterGreater = r2.Split(startParts);

        Assert.Multiple(() =>
        {
            Assert.That(splitted, Is.EqualTo(new Day19.RangeParts(
                new Day19.Range(0, 4000),
                new Day19.Range(0, 4000),
                new Day19.Range(0, 2005),
                new Day19.Range(0, 4000))));

            Assert.That(left, Is.EqualTo(new Day19.RangeParts(
                new Day19.Range(0, 4000),
                new Day19.Range(0, 4000),
                new Day19.Range(2006, 4000),
                new Day19.Range(0, 4000))));

            Assert.That(afterGreater.splitted, Is.EqualTo(new Day19.RangeParts(
               new Day19.Range(0, 4000),
               new Day19.Range(0, 4000),
               new Day19.Range(0, 4000),
               new Day19.Range(3449, 4000))));

            Assert.That(afterGreater.left, Is.EqualTo(new Day19.RangeParts(
                new Day19.Range(0, 4000),
                new Day19.Range(0, 4000),
                new Day19.Range(0, 4000),
                new Day19.Range(0, 3448))));
        });
    }

    [Test]
    public void Part2Test() =>
        Assert.That(GetTestInstance().GetRangeSums(), Is.EqualTo(167409079868000));

    [Test]
    public void Part2() =>
        Assert.That(RealInstance.GetRangeSums(), Is.EqualTo(125455345557345));

    [Test]
    public void Part2Minimal() =>
        Assert.That(new Day19(["in{a<3997:R,a>3997:R,x>1:R,m<4000:R,s>2:R,A", ""]).GetRangeSums(),
            Is.EqualTo(6));


    private Day19 RealInstance => new(GetRealLines());
    private Day19 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));
}
