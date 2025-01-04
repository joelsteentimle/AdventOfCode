namespace AdventLibrary;

public static class DictionaryExtension
{
    public static void AddOrCreate<TKey, TValue>(this Dictionary<TKey, HashSet<TValue>> dictionary, TKey key, TValue value)
        where TKey : notnull
    {
        if (dictionary.TryGetValue(key, out var collection))
        {
            collection.Add(value);
        }
        else
        {
            dictionary.Add(key, [value]);
        }
    }
}
