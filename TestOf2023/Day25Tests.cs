﻿using AoC2023;
using SupportCode;

namespace TestOf2023;

public class Day25Tests : DayTests
{
    [Test]
    public void CanReadLinesAndMatrix() =>
        Assert.Multiple(() =>
        {
            Assert.That(RealInstance.TextGroups, Has.Count.GreaterThan(2));
            Assert.That(GetTestInstance().Map.GetLength(0), Is.GreaterThan(10));
        });

    private Day25 RealInstance => new(GetRealLines());

    private Day25 GetTestInstance(string suffix = "") => new(GetTestLines(suffix));

}
