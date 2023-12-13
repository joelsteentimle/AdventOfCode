namespace AoC2023;

public class Day07(IEnumerable<string> readTestLines, bool isPart2 = false)
{
    private List<Hand> Hands { get; } = readTestLines.Select(l => l.SplitAndTrim(' '))
        .Select(p => new Hand(p.First(), Convert.ToInt32(p[1], NumberFormatInfo.InvariantInfo), isPart2))
        .ToList();

    public int TotalWinnings()
    {
        var orderedHands = Hands.Order().ToList();
        return orderedHands.Select((h, i) => (i + 1) * h.Bet).Sum();
    }

    public readonly record struct Hand : IComparable<Hand>
    {
        private readonly bool isPart2;

        public Hand(string cards, int bet, bool part2 = false)
        {
            Cards = cards.ToCharArray();
            Bet = bet;
            isPart2 = part2;
            HandValue = CalculateHandValue();
        }

        public int HandValue { get; }

        public char[] Cards { get; }
        public int Bet { get; }

        public int CompareTo(Hand other)
        {
            var handValueComparison = HandValue.CompareTo(other.HandValue);
            if (handValueComparison != 0) return handValueComparison;

            for (var card = 0; card < Cards.Length; card++)
            {
                var comp = CardValue(Cards[card]).CompareTo(CardValue(other.Cards[card]));
                if (comp != 0) return comp;
            }

            return 0;
        }

        private int CalculateHandValue()
        {
            var nonJokerCards = Cards;
            var nrOfJokers = 0;

            if (isPart2)
            {
                nonJokerCards = Cards.Where(c => c != 'J').ToArray();
                nrOfJokers = Cards.Count(c => c == 'J');
            }

            var groups =
                nonJokerCards
                    .GroupBy(c => c, (b, c) => c.Count())
                    .OrderDescending()
                    .ToList();

            var highest = groups.FirstOrDefault() + nrOfJokers;
            var second = groups.Count > 1 ? groups[1] : 0;

            return (highest, second) switch
            {
                (5, _) => 7,
                (4, _) => 6,
                (3, 2) => 5,
                (3, _) => 4,
                (2, 2) => 3,
                (2, _) => 2,
                _ => 1
            };
        }

        private int CardValue(char c) => c switch
        {
            'A' => 13,
            'K' => 12,
            'Q' => 11,
            'J' => isPart2 ? 0 : 10,
            'T' => 9,
            '9' => 8,
            '8' => 7,
            '7' => 6,
            '6' => 5,
            '5' => 4,
            '4' => 3,
            '3' => 2,
            _ => 1
        };

        public static bool operator <(Hand left, Hand right) => left.CompareTo(right) < 0;

        public static bool operator <=(Hand left, Hand right) => left.CompareTo(right) <= 0;

        public static bool operator >(Hand left, Hand right) => left.CompareTo(right) > 0;

        public static bool operator >=(Hand left, Hand right) => left.CompareTo(right) >= 0;
    }
}
