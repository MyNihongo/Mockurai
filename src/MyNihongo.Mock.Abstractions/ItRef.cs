namespace MyNihongo.Mock;

public readonly ref struct ItRef<T>
{
	public static ItRef<T> Any()
	{
		return new ItRef<T>();
	}

	public static implicit operator It<T>(ItRef<T> _)
	{
		return new It<T>(static () => "ref any");
	}

	public override string ToString()
	{
		return "ref any";
	}
}
