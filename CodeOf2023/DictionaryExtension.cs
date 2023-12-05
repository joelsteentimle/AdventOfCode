namespace AoC2023;

public static class DictionaryExtension
{
    public static void MaxToDictionary(this Dictionary<string, int> dictionary, string blue, int readValue)
    {
        dictionary.TryGetValue(blue, out var count);
        dictionary[blue] = Math.Max(count, readValue);
    }
}