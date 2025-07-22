namespace MyNihongo.Mock;

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

	public long Verify(in long index)
	{
		var item = _invocations.TryGetItemAt(index);
		if (!item.HasValue)
			throw new MockVerifySequenceOutOfRangeException(_name, index);

		item.Value.Invocation.IsVerified = true;
		return item.Value.Index + 1;
	}

	public void VerifyNoOtherCalls()
	{
		if (_isVerified)
			return;

		var unverifiedItems = _invocations
			.Where(static x => !x.Invocation.IsVerified)
			.Select(static x => x.Index)
			.ToArray();

		if (unverifiedItems.Length > 0)
			throw new MockUnverifiedException(_name, unverifiedItems);
	}

	private sealed class Item
	{
		public bool IsVerified;
	}
}
