namespace MyNihongo.Mockurai;

/// <summary>
/// Extension methods for <see cref="IDictionary{TKey, TValue}"/>.
/// </summary>
public static class DictionaryEx
{
	/// <summary>
	/// Returns the value associated with <paramref name="key"/>, or the default value of <typeparamref name="TValue"/> when the key is not present.
	/// </summary>
	/// <typeparam name="TKey">The dictionary key type.</typeparam>
	/// <typeparam name="TValue">The dictionary value type.</typeparam>
	/// <param name="dictionary">The dictionary to look up.</param>
	/// <param name="key">The key to retrieve.</param>
	/// <returns>The associated value, or <see langword="default"/> when the key is missing.</returns>
	public static TValue? ValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key)
	{
		return dictionary.TryGetValue(key, out var value)
			? value
			: default;
	}
}
