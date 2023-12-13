namespace AoC2023;

public static class DictionaryExtension
{
    public static void MaxToDictionary(this Dictionary<string, int> dictionary, string key, int anotherValue) =>
        dictionary.SetToSelectedValue(key, anotherValue, Math.Max);

    public static void SetToSelectedValue<TK, TV>(this Dictionary<TK, TV> dictionary, TK key, TV anotherValue,
        Func<TV, TV, TV> selector) where TK : notnull
    {
        if (dictionary.TryGetValue(key, out var current))
            dictionary[key] = selector(current, anotherValue);
        else
            dictionary[key] = anotherValue;
    }
}
