using System.Text.Json;

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

public sealed class Invocation<TParameter>
{
	private readonly string _name;
	private readonly InvocationContainer<Item> _invocations = [];

	public Invocation(in string name)
	{
		_name = name;
	}

	public void Register(ref long index, in TParameter parameter)
	{
		var invokedIndex = Interlocked.Increment(ref index);
		_invocations.Add(invokedIndex, new Item(parameter));
	}

	public void Verify(in It<TParameter> parameter, in Times times)
	{
		var count = 0;
		foreach (var invocation in _invocations)
		{
			var verifyParameter = invocation.Invocation.GetParameter();
			if (parameter.ValueSetup.HasValue && !parameter.ValueSetup.Value.Predicate(verifyParameter))
				continue;

			invocation.Invocation.IsVerified = true;
			count++;
		}

		if (times.Count == count)
			return;

		var invocations = _invocations.GetItemStrings();
		throw new MockVerifyCountException(_name, times.Count, count, invocations);
	}

	public long Verify(in It<TParameter> parameter, in long index)
	{
		foreach (var item in _invocations.GetItemsFrom(index))
		{
			var verifyParameter = item.Invocation.GetParameter();

			if (!parameter.ValueSetup.HasValue || parameter.ValueSetup.Value.Predicate(verifyParameter))
			{
				item.Invocation.IsVerified = true;
				return item.Index + 1;
			}
		}

		throw new MockVerifySequenceOutOfRangeException(_name, index);
	}

	public void VerifyNoOtherCalls()
	{
		var unverifiedItems = _invocations
			.Where(static x => !x.Invocation.IsVerified)
			.Select(static x => x.GetString())
			.ToArray();

		if (unverifiedItems.Length > 0)
			throw new MockUnverifiedException(_name, unverifiedItems);
	}

	private sealed class Item
	{
		public bool IsVerified;
		private readonly TParameter _parameter;
		private readonly string? _jsonSnapshot;

		public Item(TParameter parameter)
		{
			_parameter = parameter;

			try
			{
				_jsonSnapshot = JsonSerializer.Serialize(parameter);
			}
			catch
			{
				// Swallow
			}
		}

		public TParameter GetParameter()
		{
			return _parameter;

			// TODO: only for equivalent
			return !string.IsNullOrEmpty(_jsonSnapshot)
				? JsonSerializer.Deserialize<TParameter>(_jsonSnapshot)!
				: _parameter;
		}

		public override string ToString()
		{
			return string.IsNullOrEmpty(_jsonSnapshot)
				? _parameter?.ToString() ?? string.Empty
				: _jsonSnapshot;
		}
	}
}
