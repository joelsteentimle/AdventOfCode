using System.Diagnostics.CodeAnalysis;

namespace AoC2023;

public class Day03
{
    private readonly char[][] _inputField;
    private readonly Dictionary<(int row, int column), List<int>> _mightBeGears = [];
    public readonly List<int> Numbers = [];
    public readonly List<int> Parts = [];

    public Day03(IEnumerable<string> lines)
    {
        _inputField = lines.Select(l => l.ToArray()).ToArray();
        CalculateNumbersAndParts();
    }

    public IEnumerable<List<int>> Gears => _mightBeGears.Values.Where(v => v.Count == 2);

    private void CalculateNumbersAndParts()
    {
        for (var rowNumber = 0; rowNumber < _inputField.Length; rowNumber++)
        {
            var row = _inputField[rowNumber];
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

                bool EndOfNumber([NotNullWhen(true)] int? startOfNumber)
                {
                    return startOfNumber.HasValue
                           && (i == row.Length - 1
                               || !char.IsNumber(row[i + 1]));
                }

                bool StartOfNumber()
                {
                    return numberStart is null && char.IsNumber(row[i]);
                }
            }
        }
    }

    private void AddNumber(int rowNumber, int numberStart, int numberEnd)
    {
        var number = Convert.ToInt32(new string(_inputField[rowNumber][numberStart..(numberEnd + 1)]));

        Numbers.Add(number);

        if (CloseToPart())
            Parts.Add(number);
        return;

        bool CloseToPart()
        {
            var rowStart = Math.Max(0, rowNumber - 1);
            var rowEnd = Math.Min(_inputField.Length, rowNumber + 2);

            var columnStart = Math.Max(0, numberStart - 1);
            var columnEnd = Math.Min(_inputField.Length, numberEnd + 2);
            var isPart = false;

            for (var gearRow = rowStart; gearRow < rowEnd; gearRow++)
            {
                var row = _inputField[gearRow];
                for (var gearColumn = columnStart; gearColumn < columnEnd; gearColumn++)
                {
                    var c = row[gearColumn];
                    if (c != '.' && !char.IsNumber(c))
                    {
                        isPart = true;
                        if (c == '*') AddPossibleGear();
                    }

                    continue;

                    void AddPossibleGear()
                    {
                        if (_mightBeGears.TryGetValue((gearRow, gearColumn), out var numbers))
                            numbers.Add(number);
                        else
                            _mightBeGears[(gearRow, gearColumn)] = [number];
                    }
                }
            }

            return isPart;
        }
    }
}