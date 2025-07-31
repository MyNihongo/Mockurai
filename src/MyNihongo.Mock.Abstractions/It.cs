namespace MyNihongo.Mock;

public readonly ref struct It<T>
{
	public readonly Setup? ValueSetup;

	private It(Func<T?, bool> predicate, SetupType type)
	{
		ValueSetup = new Setup(predicate, type);
	}

	public static It<T> Value(T value)
	{
		return new It<T>(x => EqualityComparer<T>.Default.Equals(value, x), SetupType.Value);
	}

	public static It<T> Equivalent(T value)
	{
		return new It<T>(x => EquivalencyComparer<T>.Default.Equivalent(value, x), SetupType.Equivalent);
	}

	public static It<T> Where(in Func<T?, bool> predicate)
	{
		return new It<T>(predicate, SetupType.Where);
	}

	public static It<T> Any()
	{
		return new It<T>();
	}

	public static implicit operator It<T>(in T value)
	{
		return Value(value);
	}

	public readonly struct Setup : IComparable<Setup>
	{
		public readonly Func<T?, bool> Predicate;
		public readonly SetupType Type;

		public Setup(in Func<T?, bool> predicate, in SetupType type)
		{
			Predicate = predicate;
			Type = type;
		}

		public int Sort => (int)Type;

		public int CompareTo(Setup other)
		{
			return Sort.CompareTo(Sort);
		}

		public override string ToString()
		{
			return Sort.ToString();
		}
	}
}
