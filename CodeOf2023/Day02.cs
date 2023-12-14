namespace AoC2023;

public class Day02
{
    private readonly Dictionary<int, Dictionary<string, int>> gamesMaxValuePerColour = [];

    public IReadOnlyDictionary<int, Dictionary<string, int>> Games
        => gamesMaxValuePerColour;

    public int PowerSum =>
        Games.Keys.Select(GamePower).Sum();

    public void AddAllGames(IEnumerable<string> games)
    {
        foreach (var game in games)
            AddGameString(game);
    }


    private void AddGameString(string gameLine)
    {
        var idAndString = gameLine["Game ".Length..].Split(':');
        var gameId = Convert.ToInt32(idAndString.First(), NumberFormatInfo.InvariantInfo);
        gamesMaxValuePerColour[gameId] = ParseGameLine(idAndString.Last());
    }

    private static Dictionary<string, int> ParseGameLine(string gamePart)
    {
        var result = new Dictionary<string, int>();
        var colorParts = gamePart.Split(',', ';');

        foreach (var colorString in colorParts)
        {
            var countAndColour = colorString.Trim().Split(' ');
            result.MaxToDictionary(countAndColour.Last(), Convert.ToInt32(countAndColour.First(), NumberFormatInfo.InvariantInfo));
        }

        return result;
    }

    public IEnumerable<int> GamesMatchingLimits(params (string colour, int limit)[] limits) =>
        Games.Keys.Where(game =>
            limits.All(l => Games[game][l.colour] <= l.limit));

    public int GamePower(int game) =>
        Games[game].Values.Aggregate(1, (i, j) => i * j);
}
