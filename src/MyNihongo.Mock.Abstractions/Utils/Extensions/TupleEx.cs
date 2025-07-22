namespace MyNihongo.Mock;

public static class TupleEx
{
	public static string GetString<T>(this (long, T) @this)
	{
		return $"{@this.Item1}: {@this.Item2}";
	}
}
