using System.Text.Json;

namespace MyNihongo.Mock;

public sealed class Invocation<TParameter>
{
	private readonly string _name;
	private readonly InvocationContainer<Item> _invocations = [];

	public Invocation(in string name)
	{
		_name = name;
	}

	public void Register(in InvocationIndex.Counter index, in TParameter parameter)
	{
		var invokedIndex = index.Increment();
		_invocations.Add(invokedIndex, new Item(parameter));
	}

	public void Verify(in It<TParameter> parameter, in Times times)
	{
		var count = 0;
		foreach (var invocation in _invocations)
		{
			var verifyParameter = invocation.Invocation.GetParameter(parameter.ValueSetup?.Type);
			if (parameter.ValueSetup.HasValue && !parameter.ValueSetup.Value.Check(verifyParameter, out var result))
				continue;

			invocation.Invocation.IsVerified = true;
			count++;
		}

		if (times.Predicate(count))
			return;

		var verifyName = string.Format(_name, parameter.ToString());
		var invocations = _invocations.GetItemStrings();
		throw new MockVerifyCountException(verifyName, times, count, invocations);
	}

	public long Verify(in It<TParameter> parameter, in long index)
	{
		var span = _invocations.GetItemsFromSpan(index);

		for (var i = 0; i < span.Length; i++)
		{
			var verifyParameter = span[i].Invocation.GetParameter(parameter.ValueSetup?.Type);

			if (parameter.ValueSetup.HasValue && !parameter.ValueSetup.Value.Check(verifyParameter, out var result))
				continue;

			span[i].Invocation.IsVerified = true;
			return span[i].Index + 1;
		}

		var verifyName = string.Format(_name, parameter.ToString());
		throw new MockVerifySequenceOutOfRangeException(verifyName, index);
	}

	public void VerifyNoOtherCalls()
	{
		var unverifiedItems = _invocations
			.Where(static x => !x.Invocation.IsVerified)
			.Select(static x => x.GetString())
			.ToArray();

		if (unverifiedItems.Length > 0)
		{
			var verifyName = string.Format(_name, typeof(TParameter).Name);
			throw new MockUnverifiedException(verifyName, unverifiedItems);
		}
	}

	private sealed class Item
	{
		public bool IsVerified;
		private readonly TParameter _parameter;
		private readonly string? _jsonSnapshot;

		public Item(in TParameter parameter)
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

		public TParameter GetParameter(SetupType? setupType)
		{
			return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshot)
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
