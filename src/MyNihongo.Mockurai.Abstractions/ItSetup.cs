namespace MyNihongo.Mockurai;

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
		if (_predicateBool is not null)
			return _predicateBool.Invoke(value);
		
		if (_predicateResult is not null)
			return _predicateResult.Invoke(value);
		
		return Type == SetupType.Any;
	}

	public bool Check(in T value, out ComparisonResult? result)
	{
		if (_predicateBool is not null)
		{
			result = null;
			return _predicateBool(value);
		}

		if (_predicateResult is not null)
			return result = _predicateResult.Invoke(value);

		result = null;
		return Type == SetupType.Any;
	}

	public override string ToString()
	{
		return _toString?.Invoke() ?? "any";
	}
}
