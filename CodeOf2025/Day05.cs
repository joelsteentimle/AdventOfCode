namespace AoC2025;

public class Day05
{
    private readonly List<List<int>> Prints = new();

    private readonly Dictionary<int, List<int>> Rules = new();

    public Day05(List<string> input)
    {
        var rulesText = input.TakeWhile(s => !string.IsNullOrWhiteSpace(s)).ToList();
        var printTexts = input.Skip(rulesText.Count + 1).ToList();

        foreach (var rule in rulesText)
        {
            var sepRules = rule.Split('|', StringSplitOptions.RemoveEmptyEntries).ToList();

            List<int> currentRule;

            if (!Rules.TryGetValue(int.Parse(sepRules[1]), out currentRule))
            {
                currentRule = new List<int>();
                Rules[int.Parse(sepRules[1])] = currentRule;
            }

            currentRule.Add(int.Parse(sepRules[0]));
        }

        foreach (var printText in printTexts)
        {
            var pages = printText.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            Prints.Add(pages);
        }
    }

    public int MidPagesAfterFixingOutOfOrder()
    {
        var incorrect = Prints.Where(p => !IsPrintCorrect(p)).ToList();

        var sum = 0;
        foreach (var print in incorrect)
        {
            while (!OrderToCorrect(print)) ;

            sum += print[print.Count / 2];
        }

        return sum;
    }

    public bool OrderToCorrect(List<int> pages)
    {
        var swapToPage = new Dictionary<int, int>();

        for (var i = 0; i < pages.Count; i++)
        {
            var pageNumber = pages[i];
            if (swapToPage.TryGetValue(pageNumber, out var indexToSwapTo))
            {
                pages[i] = pages[indexToSwapTo];
                pages[indexToSwapTo] = pageNumber;
                return false;
            }

            if (Rules.TryGetValue(pages[i], out var newPages))
                foreach (var notAllowedpage in newPages)
                    swapToPage[notAllowedpage] = i;
        }

        return true;
    }

    public bool IsPrintCorrect(List<int> pages)
    {
        var notAllowed = new HashSet<int>();
        foreach (var page in pages)
        {
            if (notAllowed.Contains(page))
                return false;

            if (Rules.TryGetValue(page, out var newPages))
                foreach (var notAllowedpage in newPages)
                    notAllowed.Add(notAllowedpage);
        }

        return true;
    }

    public int MidPageSumOfAllowed()
    {
        var correctPrints = Prints.Where(IsPrintCorrect).ToList();

        var sum = 0;
        foreach (var correctPrint in correctPrints) sum += correctPrint[correctPrint.Count / 2];
        return sum;
    }
}
