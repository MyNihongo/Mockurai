namespace MyNihongo.Mock;

public sealed class Invocation : IInvocationProvider
{
	private readonly string _name;
	private readonly InvocationContainer<Item> _invocations = [];
	private bool _isVerified;

	public Invocation(in string name)
	{
		_name = name;
	}

	public void Register(in InvocationIndex.Counter index)
	{
		var invokedIndex = index.Increment();
		_invocations.Add(invokedIndex, new Item());
	}

	public void Verify(in Times times, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		if (!times.Predicate(_invocations.Count))
		{
			var invocations = GetStrings(invocationProviders, _invocations);
			throw new MockVerifyCountException(_name, times, _invocations.Count, invocations);
		}

		_isVerified = true;
	}

	public long Verify(in long index, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var item = _invocations.TryGetItemAt(index);
		if (!item.HasValue)
		{
			var invocations = GetStrings(invocationProviders, _invocations);
			throw new MockVerifySequenceOutOfRangeException(_name, index, invocations);
		}

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

	public IEnumerable<IInvocation> GetInvocations()
	{
		foreach (var x in _invocations)
		{
			yield return new InvocationSnapshot
			{
				Index = x.Index,
				Snapshot = _name,
			};
		}
	}

	private static IEnumerable<string>? GetStrings(Func<IEnumerable<IInvocationProvider?>>? invocationProviders, in InvocationContainer<Item> invocations)
	{
		if (invocationProviders is not null)
			return invocationProviders.GetStrings();

		return invocations.Count > 0
			? invocations.Select(static x => x.Index.ToString())
			: null;
	}

	private sealed class Item
	{
		public bool IsVerified;
	}
}
