namespace AoC2024;

public class Day05
{
    public Day05(List<string> input)
    {
        var rulesText = input.TakeWhile(s => !string.IsNullOrWhiteSpace(s)).ToList();
        var printTexts = input.Skip(rulesText.Count +1).ToList();

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


        return 0;

    }

    public int MidPageSumOfAllowed()
    {
        var correctPrints = Prints.Where(IsPrintCorrect).ToList();

        var sum = 0;
        foreach (var correctPrint in correctPrints)
        {
            sum += correctPrint[correctPrint.Count /2];
        }
        return sum;
    }

    public bool IsPrintCorrect(List<int> pages)
    {
        var notAllowed = new HashSet<int>();
        foreach (var page in pages)
        {
            if(notAllowed.Contains(page))
                return false;

            if(Rules.TryGetValue(page, out var newPages))
                foreach(var notAllowedpage in newPages)
                    notAllowed.Add(notAllowedpage);
        }

        return true;
    }

    Dictionary<int, List<int>> Rules = new();
    List<List<int>> Prints = new();
}
