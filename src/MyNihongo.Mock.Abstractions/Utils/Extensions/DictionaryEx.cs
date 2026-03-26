namespace MyNihongo.Mock;

public static class DictionaryEx
{
	public static TValue? ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
	{
		return dictionary.TryGetValue(key, out var value)
			? value
			: default;
	}
}
