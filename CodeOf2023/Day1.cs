public class Day1
{
    public char[] LineNumbers(string line) => line.Where(char.IsNumber).ToArray();

    public int LineValue(string line)
    {
        var ln = LineNumbers(ReplaceAll(line));
        var sValue = new string(new[] { FirstNumber(line), LastNumber(line) });
        return Convert.ToInt16(sValue);
    }

    private char LastNumber(string line)
    {
        char? found = null;
        for (int i = line.Length - 1; i > 0 && found is null; i--)
        {
            found = PosAsNum(line, i);
        }

        return found ?? '0';
    }

    private char FirstNumber(string line)
    {
        char? found = null;
        for (int i = 0; i < line.Length - 1 && found is null; i++)
        {
            found = PosAsNum(line, i);
        }

        return found ?? '0';
    }

    public char? PosAsNum(string line, int position)
    {
        var posLine = line[position..];
        return posLine switch
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

    public string ReplaceAll(string line)
    {
        return line.Replace("one", "1", StringComparison.InvariantCultureIgnoreCase)
            .Replace("two", "2", StringComparison.InvariantCultureIgnoreCase)
            .Replace("three", "3", StringComparison.InvariantCultureIgnoreCase)
            .Replace("four", "4", StringComparison.InvariantCultureIgnoreCase)
            .Replace("five", "5", StringComparison.InvariantCultureIgnoreCase)
            .Replace("six", "6", StringComparison.InvariantCultureIgnoreCase)
            .Replace("seven", "7", StringComparison.InvariantCultureIgnoreCase)
            .Replace("eight", "8", StringComparison.InvariantCultureIgnoreCase)
            .Replace("nine", "9", StringComparison.InvariantCultureIgnoreCase);
    }
}