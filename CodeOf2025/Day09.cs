namespace AoC2025;

public class Day09
{
    private List<int?> uncompressedFile = [];
    private List<int> movedFile =  [];
    private List<(int? number, int count)> numAndCount = [];
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

    public long OtherUncompress()
    {

        var isFile = true;
        var number = 0;

        foreach (var repeatChar in input)
        {
            var repeats =  int.Parse(repeatChar.ToString());

            if (isFile)
            {
                numAndCount.Add((number, repeats));
                number++;
            }
            else
                numAndCount.Add((null, repeats));

            isFile = !isFile;
        }

        var i = numAndCount.Count-1;
        while(i > 0 )
        {
            var insertIndex = 0;
            var inserted = false;
            if (numAndCount[i] is (int repeatingNumber, int count))
            {
                while (!inserted && insertIndex < i)
                {
                    if (numAndCount[insertIndex] is
                            (null, int insertCount)
                        && insertCount >= count)
                    {
                        inserted = true;
                        numAndCount[insertIndex] = (null,
                                insertCount-count);
                        numAndCount[i]  = (null, count);

                        numAndCount.Insert(insertIndex, (repeatingNumber, count));
                    }
                    insertIndex++;
                }
            }

            if (!inserted)
                i--;

        }

        var sum = 0L;
        var index = 0;

        foreach (var numAndCount in numAndCount)
        {
            var (fixedNum, fixedCount) = numAndCount;
            if (fixedNum.HasValue)
            {
                for (int j = 0; j < fixedCount; j++)
                {
                    sum += fixedNum.Value * (index +j) ;
                }
            }

            index += fixedCount;

        }

        return sum;
    }
}
