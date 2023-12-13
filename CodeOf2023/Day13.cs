namespace AoC2023;

public class Day13
{
    public Day13(IList<string> lines) => Fields = lines.Aggregate(new List<List<string>> { new() }, (list, line) =>
            {
                if (line.Trim() == string.Empty)
                    list.Add([]);
                else
                    list.Last().Add(line);

                return list;
            }
        )
        .Select(bl => new Field(bl))
        .ToList();

    public int Sum => Fields.Sum(f => ((f.HFold ?? 0) * 100) + (f.VFold ?? 0));
    public int SmudgeSum => Fields.Sum(f => ((f.HSmudgeFold ?? 0) * 100) + (f.VSmudgeFold ?? 0));

    public List<Field> Fields { get; }

    public class Field
    {
        public Field(List<string> lines)
        {
            _isRock = new bool[lines.First().Length, lines.Count];

            for (var y = 0; y < lines.Count; y++)
            for (var x = 0; x < lines[y].Length; x++)
                _isRock[x, y] = lines[y][x] == '#';

            for (var x = 1; x < _isRock.GetLength(0); x++)
            {
                var (current, smudge) = CheckIfVMirror(x);
                if (current) VFold = x;

                if (smudge) VSmudgeFold = x;
            }

            for (var y = 1; y < _isRock.GetLength(1); y++)
            {
                var (current, smudge) = CheckIfHMirror(y);
                if (current) HFold = y;

                if (smudge) HSmudgeFold = y;
            }
        }

        private (bool current, bool smudge) CheckIfVMirror(int i)
        {
            var right = i;
            var left = i - 1;
            var smudges = 0;

            while (left >= 0 && right < _isRock.GetLength(0))
            {
                for (var y = 0; y < _isRock.GetLength(1); y++)
                    if (_isRock[right, y] != _isRock[left, y])
                    {
                        smudges++;
                        if (smudges > 1) return (false, false);
                    }

                left--;
                right++;
            }

            return (smudges == 0, smudges == 1);
        }

        private (bool current, bool smudge) CheckIfHMirror(int i)
        {
            var upper = i;
            var lower = i - 1;
            var smudges = 0;

            while (lower >= 0 && upper < _isRock.GetLength(1))
            {
                for (var x = 0; x < _isRock.GetLength(0); x++)
                    if (_isRock[x, lower] != _isRock[x, upper])
                    {
                        smudges++;
                        if (smudges > 1) return (false, false);
                    }

                upper++;
                lower--;
            }

            return (smudges == 0, smudges == 1);
        }

        private readonly bool[,] _isRock;

        public int? VFold { get; }
        public int? HFold { get; }
        public int? VSmudgeFold { get; }
        public int? HSmudgeFold { get; }
    }
}
