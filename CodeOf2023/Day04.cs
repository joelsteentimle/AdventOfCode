namespace AoC2023;

public class Day04
{
    public Day04(IEnumerable<string> readTestLines)
    {
        foreach (var line in readTestLines)
        {
            var card = new Card(line);
            Cards[card.Number] = card;
        }
    }

    private Dictionary<int, Card> Cards { get; } = [];
    private Dictionary<int, int> WonCardCopies { get; } = [];

    public int TotalSum() => Cards.Values.Select(c => c.Points()).Sum();

    public int TotalScratchCards()
    {
        for (var currentCard = 1; currentCard <= Cards.Count; currentCard++)
        {
            WonCardCopies.TryGetValue(currentCard, out var wonCardCopy);

            var copiesOfCurrent = wonCardCopy + 1;
            var currentsMatches = Cards[currentCard].MatchingNumbers();

            for (var i = currentCard + 1;
                 i <= Math.Min(currentCard + currentsMatches, Cards.Count);
                 i++)
            {
                WonCardCopies.TryGetValue(i, out var value);
                value += copiesOfCurrent;
                WonCardCopies[i] = value;
            }
        }

        return WonCardCopies.Values.Sum() + Cards.Count;
    }


    public class Card
    {
        public Card(string card)
        {
            var cardAndNumbers = card.Split(':');
            Number = Convert.ToInt32(cardAndNumbers.First()["Card".Length..], NumberFormatInfo.InvariantInfo);

            var winnerAndMine = cardAndNumbers[1].Split('|');
            Winner = winnerAndMine.First().Split(' ',
                    StringSplitOptions.RemoveEmptyEntries |
                    StringSplitOptions.TrimEntries)
                .Select(w => Convert.ToInt32(w, NumberFormatInfo.InvariantInfo));
            Mine = winnerAndMine[1].Split(' ',
                    StringSplitOptions.RemoveEmptyEntries |
                    StringSplitOptions.TrimEntries)
                .Select(m => Convert.ToInt32(m, NumberFormatInfo.InvariantInfo));
        }

        public IEnumerable<int> Mine { get; }

        public IEnumerable<int> Winner { get; }

        public int Number { get; }

        public int Points()
        {
            var myWinners = MatchingNumbers();

            if (myWinners == 0) return 0;

            var myScore = 1;
            for (var i = 0; i < myWinners - 1; i++) myScore *= 2;

            return myScore;
        }

        public int MatchingNumbers()
        {
            var myWinners = Mine.Intersect(Winner).Count();
            return myWinners;
        }
    }
}
