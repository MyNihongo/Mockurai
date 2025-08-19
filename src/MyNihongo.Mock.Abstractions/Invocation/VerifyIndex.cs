namespace MyNihongo.Mock;

public sealed class VerifyIndex
{
	private long _value = 1L;

	public void Set(in long value)
	{
		Interlocked.Exchange(ref _value, value);
	}

	public static implicit operator long(VerifyIndex @this)
	{
		return @this._value;
	}
}
