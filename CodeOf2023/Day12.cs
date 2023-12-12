using System.Text;

namespace AoC2023;

public class Day12
{
    public Day12(IList<string> lines)
    {
        Fields = lines.Select(l => new Field(l)).ToList();
    }

    public long GetSum => Fields.Select(f => f.TotalCombinations()).Sum();

    public void Fold(int times)
    {
        foreach (var field in Fields)
        {
            field.Fold(times);
        }
    }

    public IList<Field> Fields { get; set; }

    public class Field
    {
        public Field(string line)
        {
            var patternAndSprings = line.SplitAndTrim(' ');
            Pattern = patternAndSprings[0];
            Springs = patternAndSprings[1].SplitAndTrim(',').Select(n => Convert.ToInt32(n)).ToList();
            Combinations = new long?[Pattern.Length, Springs.Count];
        }

        public void Fold(int times)
        {
            var newPatterBuilder = new StringBuilder(Pattern);
            List<int> newSprings = [..Springs];
            for (int i = 1; i < times; i++)
            {
                newPatterBuilder.Append('?');
                newPatterBuilder.Append(Pattern);

                newSprings.AddRange(Springs);
            }

            Pattern = newPatterBuilder.ToString();
            Springs = newSprings;
            Combinations = new long?[Pattern.Length, Springs.Count];
        }

        public long?[,] Combinations { get; set; }

        public List<int> Springs { get; set; }


        public string Pattern { get; set; }

        public long TotalCombinations() => GetCombinations(0, 0);

        public long GetCombinations(int startInPattern, int springNumber)
        {
            if (springNumber >= Springs.Count || startInPattern >= Pattern.Length)
                return 0;

            var preCalculated = Combinations[startInPattern, springNumber];

            if (preCalculated is not null)
                return preCalculated.Value;

            var springSize = Springs[springNumber];
            
            if (startInPattern + springSize > Pattern.Length)
            {
                Combinations[startInPattern, springNumber] = 0;
                return 0;
            }
  
            var totalMatches = 0L;

            if (MatchStart(startInPattern, springNumber))
            {
                if (springNumber + 1 == Springs.Count
                    && Pattern[(startInPattern + springSize) ..].All(c => c!= '#'))
                {
                    totalMatches += 1;
                }
                else if(startInPattern + springSize < Pattern.Length &&
                        Pattern[startInPattern + springSize] is '.' or '?')
                {
                    totalMatches += GetCombinations(startInPattern + springSize + 1, springNumber + 1);
                }
            }

            if (Pattern[startInPattern] is '.' or '?')
                totalMatches += GetCombinations(startInPattern + 1, springNumber);

            Combinations[startInPattern, springNumber] = totalMatches;
            return totalMatches;
        }

        public bool MatchStart(int startInPattern, int springNumber)
        {
            var springSize = Springs[springNumber];
            return Pattern[startInPattern .. (startInPattern + springSize)].All(c => c is '#' or '?');
        }
    }
}