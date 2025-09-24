namespace MyNihongo.Mock;

public readonly ref struct ItOut<T>
{
	public static implicit operator It<T>(ItOut<T> itRef)
	{
		return default;
	}
}
