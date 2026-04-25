namespace MyNihongo.Mockurai;

public interface IEquivalencyComparer<in T>
{
	ComparisonResult Equivalent(T? x, T? y);
}
