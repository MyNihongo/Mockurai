namespace MyNihongo.Mock.Sample;

public readonly ref struct It<T>
{
	public readonly Setup? ValueSetup;

	private It(Func<T, bool> predicate, int sort)
	{
		ValueSetup = new Setup(predicate, sort);
	}

	public static It<T> Value(T value)
	{
		return new It<T>(x => EqualityComparer<T>.Default.Equals(x, value), sort: 2);
	}

	public static It<T> Where(in Func<T, bool> predicate)
	{
		return new It<T>(predicate, sort: 1);
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
		public readonly Func<T, bool> Predicate;
		private readonly int _sort;

		public Setup(in Func<T, bool> predicate, in int sort)
		{
			Predicate = predicate;
			_sort = sort;
		}

		public int CompareTo(Setup other)
		{
			return _sort.CompareTo(other._sort);
		}
	}
}
