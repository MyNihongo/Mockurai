namespace MyNihongo.Mockurai;

public static class ItEx
{
	public static string ToString<TParameter>(this It<TParameter> @this, in string? prefix)
	{
		return @this.ValueSetup.ToString(prefix);
	}

	public static string ToString<TParameter>(this ItSetup<TParameter> @this, in string? prefix)
	{
		return !string.IsNullOrEmpty(prefix)
			? $"{prefix} {@this.ToString()}"
			: @this.ToString();
	}
}
