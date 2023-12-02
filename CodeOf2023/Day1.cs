namespace AoC2023;

public class Day1
{
    public int SumTheFile(IEnumerable<string> lines) =>
        lines.Select(LineValue).Sum();

    public static char[] LineNumbers(string line) =>
        line.Where(char.IsNumber).ToArray();

    public int LineValue(string line) =>
        Convert.ToInt16(new string(new[] { FirstNumber(line), LastNumber(line) }));

    private char LastNumber(string line) =>
        FirstInRange(line, Enumerable.Range(0, line.Length).Reverse());

    private char FirstNumber(string line) =>
        FirstInRange(line, Enumerable.Range(0, line.Length));

    private char FirstInRange(string line, IEnumerable<int> range) =>
        range.Select(i => PosAsNum(line, i))
            .FirstOrDefault(c => c is not null) ?? '0';

    public static char? PosAsNum(string line, int position) =>
        line[position..] switch
        {
            ['1', ..] or ['o', 'n', 'e', ..] => '1',
            ['2', ..] or ['t', 'w', 'o', ..] => '2',
            ['3', ..] or ['t', 'h', 'r', 'e', 'e', ..] => '3',
            ['4', ..] or ['f', 'o', 'u', 'r', ..] => '4',
            ['5', ..] or ['f', 'i', 'v', 'e', ..] => '5',
            ['6', ..] or ['s', 'i', 'x', ..] => '6',
            ['7', ..] or ['s', 'e', 'v', 'e', 'n', ..] => '7',
            ['8', ..] or ['e', 'i', 'g', 'h', 't', ..] => '8',
            ['9', ..] or ['n', 'i', 'n', 'e', ..] => '9',
            _ => null
        };
}