namespace AoC2023;

public class Day13
{
    public Day13(IList<string> lines)
    {
       Fields =  lines.Aggregate(new List<List<string>> { new() }, (list, line) =>
            {
                if (line.Trim() == string.Empty)
                {
                    list.Add([]);
                }
                else
                {
                    list.Last().Add(line);
                }
                return list;
            }
        )
           .Select(bl => new Field(bl))
        .ToList();
    }

    public int Sum => Fields.Sum(f => (f.HFold ?? 0) * 100 + (f.VFold ?? 0));
    public int SmudgeSum => Fields.Sum(f => (f.HSmudgeFold ?? 0) * 100 + (f.VSmudgeFold ?? 0));
    

    public int Result { get; set; } = 123456;
    
    public List< Field > Fields{ get; set; }

    public class Field
    {
        public Field(List<string> lines)
        {
            IsRock = new bool[lines.First().Length, lines.Count];

            for (int y = 0; y < lines.Count; y++)
            {
                for (int x = 0; x < lines[y].Length; x++)
                {
                    IsRock[x, y] = lines[y][x] == '#';
                }
            }
            
            for (int x = 1; x < IsRock.GetLength(0); x++)
            {
                (bool current, bool smudge)  = CheckIfVMirror(x);
                if (current)
                {
                    VFold = x;
                }

                if (smudge)
                {
                    VSmudgeFold = x;
                }
            }
            
            for (int y = 1; y < IsRock.GetLength(1); y++)
            {
                (bool current, bool smudge)  = CheckIfHMirror(y);
                if (current)
                {
                    HFold = y;
                    // break;
                }

                if (smudge)
                {
                    HSmudgeFold = y;
                }
            }
        }

        private (bool current, bool smudge) CheckIfVMirror(int i)
        {
            var right = i;
            var left = i - 1;
            int smudges = 0;
            
            while (left >= 0 && right < IsRock.GetLength(0))
            {
                for (var y = 0; y < IsRock.GetLength(1); y++)
                {
                    if (IsRock[right, y] != IsRock[left, y])
                    {
                        smudges++;
                        if(smudges >1)
                            return (false,false);
                    }
                }

                left--;
                right++;
            }

            return (smudges ==0, smudges == 1);
        }
        private (bool current, bool smudge) CheckIfHMirror(int i)
        {
            var upper = i;
            var lower = i - 1;
            var smudges = 0;
            
            while (lower >= 0 && upper < IsRock.GetLength(1))
            {
                for (var x = 0; x < IsRock.GetLength(0); x++)
                {
                    if (IsRock[x, lower] != IsRock[x, upper])
                    {
                        smudges++;
                        if(smudges >1)
                            return (false,false);
                    }
                }

                upper++;
                lower--;
            }

            return (smudges ==0, smudges == 1);

        }

        private bool[,] IsRock;

        public int? VFold { get; set; }
        public int? HFold { get; set; }
        public int? VSmudgeFold { get; set; }
        public int? HSmudgeFold { get; set; }
    }
}
