namespace AoC2024;

public class Day09
{

    private List<int?> uncompressedFile = [];
    private List<int> movedFile =  [];
    private List<(int?, int)> movWhatCan = [];
    private readonly string input;

    public Day09(List<string> allData)
    {
         input = allData[0];

    }

    public void FirstUncompressing()
    {
        var isFile = true;
        var number = 0;
        foreach (var repeatChar in input)
        {
            var repeats =  int.Parse(repeatChar.ToString());

            if (isFile)
            {
                uncompressedFile.AddRange(Enumerable.Repeat<int?>(
                    number, repeats).ToArray());
                number++;
            }            else
                uncompressedFile.AddRange(Enumerable.Repeat<int?>(
                    null, repeats).ToArray());
            isFile = !isFile;
        }

    }

    public long UncompressAndSum()
    {
        FirstUncompressing();
        var left = 0;
        var right = uncompressedFile.Count - 1;

        while (left <= right)
        {
            while (uncompressedFile[left] is {} lValue&&
                   left <= right)
            {
                movedFile.Add(lValue);
                left++;
            }

            while (!uncompressedFile[right].HasValue)
            {
                right--;
            }

            while (uncompressedFile[left] is null &&
                   uncompressedFile[right] is { } rValue
                   && left <= right)
            {
                movedFile.Add(rValue);
                left++;
                right--;
            }
        }

        var sum = 0L;

        for (int i = 0; i < movedFile.Count; i++)
        {
            sum += movedFile[i] * i;
        }

        return sum;

    }
    public int OtherUncompress() => 0;
}
