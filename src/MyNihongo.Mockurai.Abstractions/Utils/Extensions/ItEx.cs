namespace MyNihongo.Mockurai;

/// <summary>
/// Extension methods for rendering <see cref="It{TParameter}"/> and <see cref="ItSetup{TParameter}"/> matchers as strings,
/// optionally with a leading parameter prefix.
/// </summary>
public static class ItEx
{
	/// <summary>
	/// Returns the string representation of the matcher's value setup, optionally prefixed with <paramref name="prefix"/>.
	/// </summary>
	/// <typeparam name="TParameter">The argument type matched.</typeparam>
	/// <param name="this">The matcher to render.</param>
	/// <param name="prefix">An optional prefix prepended to the matcher representation.</param>
	public static string ToString<TParameter>(this It<TParameter> @this, in string? prefix)
	{
		return @this.ValueSetup.ToString(prefix);
	}

	/// <summary>
	/// Returns the string representation of the matcher setup, optionally prefixed with <paramref name="prefix"/>.
	/// </summary>
	/// <typeparam name="TParameter">The argument type matched.</typeparam>
	/// <param name="this">The matcher setup to render.</param>
	/// <param name="prefix">An optional prefix prepended to the matcher representation.</param>
	public static string ToString<TParameter>(this ItSetup<TParameter> @this, in string? prefix)
	{
		return !string.IsNullOrEmpty(prefix)
			? $"{prefix} {@this.ToString()}"
			: @this.ToString();
	}
}
