namespace NuvoChessSharp.Helpers;

public static class CollectionHelper
{
    public static List<KeyValuePair<TKey, TValue>> ToKeyValuePairs<TKey, TValue>(IEnumerable<TKey> keys, IEnumerable<TValue> values)
    {
        var keysArray = keys.ToArray();
        var valuesArray = values.ToArray();
        var length = keysArray.Length <= valuesArray.Length ? keysArray.Length : valuesArray.Length;
        var keyValuePairs = new List<KeyValuePair<TKey, TValue>>();
        for (var i = 0; i < length; i++)
            keyValuePairs.Add(new KeyValuePair<TKey, TValue>(keysArray[i], valuesArray[i]));
        return keyValuePairs;
    }
}