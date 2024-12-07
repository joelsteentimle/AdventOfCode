using System.Diagnostics.CodeAnalysis;

namespace AoC2023;

public class Day03
{
    private readonly char[][] inputField;
    private readonly Dictionary<(int row, int column), List<int>> mightBeGears = [];
    public List<int> Numbers { get; } = [];
    public List<int> Parts { get; } = [];

    public Day03(IEnumerable<string> lines)
    {
        inputField = lines.Select(l => l.ToArray()).ToArray();
        CalculateNumbersAndParts();
    }

    public IEnumerable<List<int>> Gears => mightBeGears.Values.Where(v => v.Count == 2);

    private void CalculateNumbersAndParts()
    {
        for (var rowNumber = 0; rowNumber < inputField.Length; rowNumber++)
        {
            var row = inputField[rowNumber];
            int? numberStart = null;
            for (var i = 0; i < row.Length; i++)
            {
                if (StartOfNumber())
                    numberStart = i;

                if (EndOfNumber(numberStart))
                {
                    AddNumber(rowNumber, numberStart.Value, i);
                    numberStart = null;
                }

                continue;

                bool EndOfNumber([NotNullWhen(true)] int? startOfNumber) => startOfNumber.HasValue
                                                                            && (i == row.Length - 1
                                                                                || !char.IsNumber(row[i + 1]));

                bool StartOfNumber() => numberStart is null && char.IsNumber(row[i]);
            }
        }
    }

    private void AddNumber(int rowNumber, int numberStart, int numberEnd)
    {
        var number = Convert.ToInt32(new string(inputField[rowNumber][numberStart..(numberEnd + 1)]),
            NumberFormatInfo.InvariantInfo);

        Numbers.Add(number);

        if (CloseToPart())
            Parts.Add(number);

        return;

        bool CloseToPart()
        {
            var rowStart = Math.Max(0, rowNumber - 1);
            var rowEnd = Math.Min(inputField.Length, rowNumber + 2);

            var columnStart = Math.Max(0, numberStart - 1);
            var columnEnd = Math.Min(inputField.Length, numberEnd + 2);
            var isPart = false;

            for (var gearRow = rowStart; gearRow < rowEnd; gearRow++)
            {
                var row = inputField[gearRow];
                for (var gearColumn = columnStart; gearColumn < columnEnd; gearColumn++)
                {
                    var c = row[gearColumn];
                    if (c != '.' && !char.IsNumber(c))
                    {
                        isPart = true;
                        if (c == '*')
                            AddPossibleGear();
                    }

                    continue;

                    void AddPossibleGear()
                    {
                        if (mightBeGears.TryGetValue((gearRow, gearColumn), out var numbers))
                            numbers.Add(number);
                        else
                            mightBeGears[(gearRow, gearColumn)] = [number];
                    }
                }
            }

            return isPart;
        }
    }
}
