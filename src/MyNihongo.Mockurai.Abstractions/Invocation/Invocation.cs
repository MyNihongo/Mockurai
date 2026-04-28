namespace MyNihongo.Mockurai;

/// <summary>
/// Tracks parameterless invocations against a mocked member and exposes verification helpers.
/// </summary>
public sealed class Invocation : IInvocationVerify
{
	private readonly string _name;
	private readonly InvocationContainer<Item> _invocations = [];

	/// <summary>
	/// Initializes a new instance of the <see cref="Invocation"/> class.
	/// </summary>
	/// <param name="name">The display name of the mocked member used in failure messages.</param>
	public Invocation(in string name)
	{
		_name = name;
	}

	/// <summary>
	/// Records a new invocation, assigning it the next index from the supplied counter.
	/// </summary>
	/// <param name="index">The shared invocation counter used to order calls.</param>
	public void Register(in InvocationIndex.Counter index)
	{
		var invokedIndex = index.Increment();
		_invocations.Add(new Item(invokedIndex, invocation: this));
	}

	/// <summary>
	/// Verifies that the recorded invocations satisfy the supplied <see cref="Times"/> constraint
	/// and marks all matching invocations as verified.
	/// </summary>
	/// <param name="times">The invocation count constraint to enforce.</param>
	/// <param name="invocationProviders">An optional accessor that supplies additional invocation providers used to enrich the failure message.</param>
	/// <exception cref="MockVerifyCountException">Thrown when the recorded count does not satisfy <paramref name="times"/>.</exception>
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

	/// <summary>
	/// Verifies that an invocation occurred at or after the specified sequence index, marks it as verified,
	/// and returns the next index to use for sequential verification.
	/// </summary>
	/// <param name="index">The minimum sequence index expected for the next invocation.</param>
	/// <param name="invocationProviders">An optional accessor that supplies additional invocation providers used to enrich the failure message.</param>
	/// <returns>The index immediately after the matched invocation, suitable for chaining further sequence verifications.</returns>
	/// <exception cref="MockVerifySequenceOutOfRangeException">Thrown when no invocation exists at or after <paramref name="index"/>.</exception>
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

	/// <inheritdoc/>
	public void VerifyNoOtherCalls(Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
		if (unverifiedItems is not null)
			throw new MockUnverifiedException(_name, unverifiedItems);
	}

	/// <inheritdoc/>
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
