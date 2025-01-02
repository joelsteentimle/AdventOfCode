using AdventLibrary;

namespace AoC2024;

public class Day22(List<string> allData)
{
    private readonly List<long> prices = allData.Select(long.Parse).ToList();
    private readonly Dictionary<(long, long, long, long), long> bananas =[];

    public long Part1()
    {
        var dayPrices = new List<long>();

        foreach (var price in prices)
        {
            var changingPrice = price;

            for(var i = 1; i <= 2000; i++)
            {
                var newPrice = NextPrice(changingPrice);

                changingPrice = newPrice;
            }

            dayPrices.Add(changingPrice);
        }

        return dayPrices.Sum();
    }

    public static long NextPrice(long price)
    {
        var mulPrice = price * 64;
        var nextPrice = (mulPrice ^ price) % 16777216;

        var divPrice = nextPrice / 32;
        nextPrice = (divPrice ^ nextPrice) % 16777216;

        mulPrice = nextPrice * 2048;
        nextPrice = (mulPrice ^ nextPrice) % 16777216;

        return nextPrice;
    }

    public long Part2()
    {
        foreach (var price in prices)
        {
            var changingPrice = price;
            var previousOneDidgitPrice = price % 10;

            var diffQueue = new Queue<long>(4);
            Dictionary<(long, long, long, long), long> localBananas = [];

            for (var i = 0; i < 2000; i++)
            {
                var newPrice = NextPrice(changingPrice);

                var oneDidgitPrice = newPrice % 10;
                diffQueue.Enqueue(oneDidgitPrice - previousOneDidgitPrice);

                if (i >= 3)
                {
                    if (!localBananas.ContainsKey((diffQueue.ElementAt(0),
                            diffQueue.ElementAt(1),
                            diffQueue.ElementAt(2),
                            diffQueue.ElementAt(3))))
                    {
                        localBananas[(diffQueue.ElementAt(0),
                            diffQueue.ElementAt(1),
                            diffQueue.ElementAt(2),
                            diffQueue.ElementAt(3))] = oneDidgitPrice;
                    }

                    diffQueue.Dequeue();
                }

                changingPrice = newPrice;
                previousOneDidgitPrice = oneDidgitPrice;
            }

            foreach (var banana in localBananas)
            {
                if (bananas.TryGetValue(banana.Key, out var nowCost))
                {
                    bananas[banana.Key] = nowCost + banana.Value;
                }
                else
                {
                    bananas[banana.Key] = banana.Value;
                }
            }
        }

        return bananas.Values.Max();
    }
}
