﻿
using AoC2023;
using SupportCode;

namespace TestOf2023;
public class Day09Tests
{
    [Test]
    public void CanRead()
    {
        var d9 = new Day09("Day09".ReadTestLines());
    }

    [Test]
    public void CanCountLine()
    {
        var d9 = new Day09("Day09".ReadTestLines());
        Assert.Multiple(() =>
        {
            Assert.That(Day09.GetNextValue([0, 3, 6, 9, 12, 15]).right, Is.EqualTo(18));
            Assert.That(Day09.GetNextValue([1, 3, 6, 10, 15, 21]).right, Is.EqualTo(28));
            Assert.That(Day09.GetNextValue([10, 13, 16, 21, 30, 45]).right, Is.EqualTo(68));
        });
    }

    [Test]
    public void CanCountLeft()
    {
        var d9 = new Day09("Day09".ReadTestLines());
        Assert.That(Day09.GetNextValue([10, 13, 16, 21, 30, 45]).left, Is.EqualTo(5));
    }


    [Test]
    public void Part1Test()
    {
        var d9 = new Day09("Day09".ReadTestLines());

        Assert.That(d9.SumNext().right, Is.EqualTo(114));
    }

    [Test]
    public void Part1()
    {
        var d9 = new Day09("Day09".ReadRealLines());

        Assert.That(d9.SumNext().right, Is.EqualTo(2174807968));
    }

    [Test]
    public void Part2Test()
    {
        var d9 = new Day09("Day09".ReadTestLines());
        Assert.That(d9.SumNext().left, Is.EqualTo(2));
    }

    [Test]
    public void Part2()
    {
        var d9 = new Day09("Day09".ReadRealLines());
        Assert.That(d9.SumNext().left, Is.EqualTo(1208));
    }
}
