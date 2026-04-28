namespace MyNihongo.Mockurai;

/// <summary>
/// Tracks the next sequence index expected by a chained sequential verification, allowing successive calls to advance the cursor.
/// </summary>
public sealed class VerifyIndex
{
	private long _value = 1L;

	/// <summary>
	/// Atomically updates the stored sequence index.
	/// </summary>
	/// <param name="value">The new sequence index.</param>
	public void Set(in long value)
	{
		Interlocked.Exchange(ref _value, value);
	}

	/// <summary>
	/// Implicitly converts the instance to its current sequence index value.
	/// </summary>
	/// <param name="this">The <see cref="VerifyIndex"/> to read.</param>
	public static implicit operator long(VerifyIndex @this)
	{
		return @this._value;
	}
}
