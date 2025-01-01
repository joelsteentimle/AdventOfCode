using AdventLibrary;

namespace AoC2024;

public class Day22
{
    private List<long> Prices;

    public Day22(List<string> allData)
    {
        Prices = allData.Select(long.Parse).ToList();
    }

    public long Part1()
    {
        var dayPrices = new List<long>();
        foreach (var price in Prices)
        {
            var changingPrice = price;
            for(int i = 1; i <= 2000; i++)
                changingPrice = NextPrice(changingPrice);

            dayPrices.Add(changingPrice);
        }

        return dayPrices.Sum();
    }

    public static long NextPrice(long price)
    {
        var mulPrice = price*64;
        var nextPrice = (mulPrice ^ price) %16777216;

        var divPrice = nextPrice / 32;
        nextPrice = (divPrice ^ nextPrice) % 16777216;

        mulPrice = nextPrice *2048;
        nextPrice = (mulPrice ^ nextPrice)%16777216;

        return nextPrice;
    }

    public long Part2()
    {
        var shortTrack100 = 0L;

        return shortTrack100;
    }


}
