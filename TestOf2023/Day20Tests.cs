using AoC2023;
using SupportCode;

namespace TestOf2023;

public class Day20Tests : DayTests
{
    [Test]
    public void Part1Test()
        => Assert.That(GetTestInstance().SentPulses(), Is.EqualTo(32000000));

    [Test]
    public void Part1Test2()
        => Assert.That(GetTestInstance("2").SentPulses(), Is.EqualTo(11687500));

    [Test]
    public void Part1()
        => Assert.That(RealInstance.SentPulses(), Is.EqualTo(856482136));

    // [Test]
    // public void FindSingleParts()
    // {
    //     var day20 = RealInstance;
    //
    //     var allNames= day20.PulseMods.Keys.ToList();
    //     var allTargets = day20.PulseMods.Values.SelectMany(mod => mod.Targets).ToList();
    //
    //     List<(string Name, int count)> nrTarget = [];
    //     foreach (var name in allNames)
    //     {
    //         nrTarget.Add((name, allTargets.Count(t => t == name)));
    //     }
    //
    //     Assert.That(nrTarget.Count(tuple => tuple.count == 1 ), Is.EqualTo(5));
    //
    // }


    private Day20 RealInstance => new(GetRealLines());

    private Day20 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));

}
