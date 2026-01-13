namespace MyNihongo.Mock;

public readonly ref struct ItOut<T>()
{
	public readonly ItSetup<T> ValueSetup = new();

	public static ItOut<T> Any()
	{
		return new ItOut<T>();
	}
}
