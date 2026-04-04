namespace MyNihongo.Mockurai;

public sealed class Invocation : IInvocationVerify
{
	private readonly string _name;
	private readonly InvocationContainer<Item> _invocations = [];

	public Invocation(in string name)
	{
		_name = name;
	}

	public void Register(in InvocationIndex.Counter index)
	{
		var invokedIndex = index.Increment();
		_invocations.Add(new Item(invokedIndex, invocation: this));
	}

	public void Verify(in Times times, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		if (!times.Predicate(_invocations.Count))
		{
			var invocations = GetStrings(invocationProviders, _invocations);
			throw new MockVerifyCountException(_name, times, _invocations.Count, invocations);
		}

		foreach (var item in _invocations.GetItemsSpan())
			item.IsVerified = true;
	}

	public long Verify(in long index, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var item = _invocations.TryGetItemAt(index);
		if (item is null)
		{
			var invocations = GetStrings(invocationProviders, _invocations);
			throw new MockVerifySequenceOutOfRangeException(_name, index, invocations);
		}

		item.IsVerified = true;
		return item.Index + 1;
	}

	public void VerifyNoOtherCalls(Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
		if (unverifiedItems is not null)
			throw new MockUnverifiedException(_name, unverifiedItems);
	}

	public IEnumerable<IInvocation> GetInvocations()
	{
		return _invocations;
	}

	private static IEnumerable<string>? GetStrings(Func<IEnumerable<IInvocationProvider?>>? invocationProviders, in InvocationContainer<Item> invocations)
	{
		return invocationProviders is not null
			? invocationProviders.GetStrings()
			: invocations.NullIfEmpty();
	}

	private sealed class Item : IInvocation
	{
		private readonly Invocation _invocation;

		public Item(in long index, in Invocation invocation)
		{
			_invocation = invocation;
			Index = index;
		}

		public long Index { get; }

		public bool IsVerified { get; set; }

		public override string ToString()
		{
			return $"{Index}: {_invocation._name}";
		}
	}
}
