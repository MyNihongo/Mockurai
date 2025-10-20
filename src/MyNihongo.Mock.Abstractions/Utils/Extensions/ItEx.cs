namespace MyNihongo.Mock;

public static class ItEx
{
	public static string ToString<TParameter>(this It<TParameter> @this, in string? prefix)
	{
		return !string.IsNullOrEmpty(prefix)
			? $"{prefix} {@this.ToString()}"
			: @this.ToString();
	}
}
