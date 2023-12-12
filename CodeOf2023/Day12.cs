namespace AoC2023;

public class Day12
{
    public Day12(IList<string> lines)
    {
        Fields = lines.Select(l => new Field(l)).ToList();
    }
    
    public int GetSum => Fields.Select(f => f.TotalCombinations()).Sum();

    public IList<Field> Fields { get; set; }

    public class Field
    {
        public Field(string line)
        {
            var patternAndSprings = line.SplitAndTrim(' ');
            Pattern = patternAndSprings[0];
            Springs = patternAndSprings[1].SplitAndTrim(',').Select(n => Convert.ToInt32(n)).ToList();
            Combinations = new int?[Pattern.Length, Springs.Count];
        }

        public int?[,] Combinations { get; set; }

        public List<int> Springs { get; set; }


        public string Pattern { get; set; }

        public int TotalCombinations() => GetCombinations(0, 0);
        
        public int GetCombinations(int startInPattern, int springNumber)
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
  
            var totalMatches = 0;

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