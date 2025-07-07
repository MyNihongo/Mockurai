namespace MyNihongo.Mock.Sample;

public readonly ref struct Times
{
	public readonly int Count;

	private Times(int count)
	{
		Count = count;
	}

	public static Times Exactly(in int count)
	{
		return new Times(count);
	}

	public static Times Once()
	{
		return new Times(1);
	}

	public static Times Never()
	{
		return new Times();
	}
}

public sealed class Invocation
{
	private readonly string _name;
	private readonly InvocationContainer<Item> _invocations = [];
	private bool _isVerified;

	public Invocation(in string name)
	{
		_name = name;
	}

	public void Register(ref long index)
	{
		var invokedIndex = Interlocked.Increment(ref index);
		_invocations.Add(invokedIndex, new Item());
	}

	public void Verify(in Times times)
	{
		if (_invocations.Count != times.Count)
			throw new MockVerifyCountException(_name, times.Count, _invocations.Count);

		_isVerified = true;
	}

	public void Verify(in long index)
	{
		var item = _invocations.TryGetItemAt(index);
		if (!item.HasValue)
			throw new MockVerifySequenceOutOfRangeException(_name, index);

		item.Value.Item2.IsVerified = true;
	}

	public void VerifyNoOtherCalls()
	{
		if (_isVerified)
			return;

		var unverifiedItems = new List<long>();
		foreach (var invocation in _invocations)
		{
			if (!invocation.Item2.IsVerified)
				unverifiedItems.Add(invocation.Item1);
		}

		if (unverifiedItems.Count > 0)
			throw new MockUnverifiedException(_name, unverifiedItems);
	}

	private sealed class Item
	{
		public bool IsVerified;
	}
}
