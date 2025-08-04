using System.Text.Json;

namespace MyNihongo.Mock;

public readonly ref struct It<T>
{
	public readonly Setup? ValueSetup;
	private readonly Func<string>? _toString;

	private It(Func<T, bool> predicate, SetupType type, Func<string> toString)
	{
		_toString = toString;
		ValueSetup = new Setup(predicate, type);
	}

	public static It<T> Value(T value)
	{
		return new It<T>(x => EqualityComparer<T>.Default.Equals(value, x), SetupType.Value, () => JsonSerializer.Serialize(value));
	}

	public static It<T> Equivalent(T value)
	{
		return new It<T>(x => EquivalencyComparer<T>.Default.Equivalent(value, x), SetupType.Equivalent, () => JsonSerializer.Serialize(value));
	}

	public static It<T> Where(in Func<T, bool> predicate)
	{
		return new It<T>(predicate, SetupType.Where, static () => "where(predicate)");
	}

	public static It<T> Any()
	{
		return new It<T>();
	}

	public static implicit operator It<T>(in T value)
	{
		return Value(value);
	}

	public override string ToString()
	{
		return _toString?.Invoke() ?? "any";
	}

	public readonly struct Setup : IComparable<Setup>
	{
		public readonly Func<T, bool> Predicate;
		public readonly SetupType Type;

		public Setup(in Func<T, bool> predicate, in SetupType type)
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
