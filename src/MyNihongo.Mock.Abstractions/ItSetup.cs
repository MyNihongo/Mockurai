namespace MyNihongo.Mock;

public readonly struct ItSetup<T> : IComparable<ItSetup<T>>
{
	private readonly Func<T, bool>? _predicateBool;
	private readonly Func<T, ComparisonResult>? _predicateResult;
	public readonly SetupType Type;

	public ItSetup(in Func<T, bool> predicate, in SetupType type)
	{
		_predicateBool = predicate;
		Type = type;
	}

	public ItSetup(in Func<T, ComparisonResult> predicate, in SetupType type)
	{
		_predicateResult = predicate;
		Type = type;
	}

	public int Sort => (int)Type;

	public int CompareTo(ItSetup<T> other)
	{
		return Sort.CompareTo(Sort);
	}

	public bool Check(in T value)
	{
		return _predicateBool?.Invoke(value) ?? _predicateResult?.Invoke(value) ?? false;
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
		return Sort.ToString();
	}
}
