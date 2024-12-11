using System.Numerics;

namespace AoC2024;

public class Day11
{
    private List<long> numbers = [];

    public Day11(List<string> allData)
    {
        numbers = allData[0]
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToList();
    }

    public int Part1(int nrBlinkings)
    {
        for (int i = 0; i <nrBlinkings; i++)
            Blink();

        return numbers.Count;
    }

    private void Blink()
    {
        var newNumbers = new List<long>();

        foreach (var number in numbers)
        {
            var numberString = number.ToString();
            var numberLength = numberString.Length;

            if (number == 0)
                newNumbers.Add(1);
            else if (numberLength % 2 == 0)
            {
                newNumbers.Add(long.Parse(numberString.Substring(0, numberLength / 2)));
                newNumbers.Add(long.Parse(numberString.Substring(numberLength / 2)));
            }
            else
            {
                newNumbers.Add(number * 2024);
            }
        }
        numbers = newNumbers;
    }

    public int Part2()
    {
        var sum = 0;

        return sum;
    }

}
