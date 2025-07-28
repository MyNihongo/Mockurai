namespace MyNihongo.Mock;

public sealed class EquivalencyComparer<T>
{
	public static readonly EquivalencyComparer<T> Default = new();

	public bool Equivalent(T? x, T? y)
	{
		return false;
	}
}
