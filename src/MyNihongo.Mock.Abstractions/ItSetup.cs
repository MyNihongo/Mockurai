namespace MyNihongo.Mock;

public readonly struct ItSetup<T> : IComparable<ItSetup<T>>
{
	private readonly Func<T, bool>? _predicateBool;
	private readonly Func<T, ComparisonResult>? _predicateResult;
	private readonly Func<string>? _toString;
	public readonly SetupType Type;

	public ItSetup(Func<T, bool> predicate, SetupType type, Func<string> toString)
	{
		_predicateBool = predicate;
		_toString = toString;
		Type = type;
	}

	public ItSetup(Func<T, ComparisonResult> predicate, SetupType type, Func<string> toString)
	{
		_predicateResult = predicate;
		_toString = toString;
		Type = type;
	}

	public int Sort => (int)Type;

	public int CompareTo(ItSetup<T> other)
	{
		return Sort.CompareTo(Sort);
	}

	public bool Check(in T value)
	{
		bool? result = _predicateBool?.Invoke(value) ?? _predicateResult?.Invoke(value);
		return result ?? Type == SetupType.Any;
	}

	public bool Check(in T value, out ComparisonResult? result)
	{
		if (_predicateBool is not null)
		{
			result = null;
			return _predicateBool(value);
		}

		return result = _predicateResult?.Invoke(value);
	}

	public override string ToString()
	{
		return _toString?.Invoke() ?? "any";
	}
}
