using System.Runtime.InteropServices;
using System.Text.Json;

namespace MyNihongo.Mock;

public sealed class Invocation<TParameter>
{
	private readonly string _name;
	private readonly string? _prefix;
	private readonly InvocationContainer<Item> _invocations = [];

	public Invocation(in string name, in string? prefix = null)
	{
		_name = name;
		_prefix = prefix;
	}

	public void Register(in InvocationIndex.Counter index, in TParameter parameter)
	{
		var invokedIndex = index.Increment();
		_invocations.Add(invokedIndex, new Item(parameter, _prefix));
	}

	public void Verify(in It<TParameter> parameter, in Times times)
	{
		var span = _invocations.GetItemsSpan();

		var verifyOutput = new List<(long, Item, ComparisonResult?)>();
		CollectionsMarshal.SetCount(verifyOutput, span.Length);

		var count = 0;
		for (var i = 0; i < span.Length; i++)
		{
			var verifyParameter = span[i].Invocation.GetParameter(parameter.ValueSetup?.Type);
			if (parameter.ValueSetup.HasValue && !parameter.ValueSetup.Value.Check(verifyParameter, out var result))
			{
				verifyOutput[i] = (span[i].Index, span[i].Invocation, result);
				continue;
			}

			verifyOutput[i] = (span[i].Index, span[i].Invocation, null);
			span[i].Invocation.IsVerified = true;
			count++;
		}

		if (times.Predicate(count))
			return;

		var invocations = verifyOutput.GetStrings();
		var verifyName = string.Format(_name, parameter.ToString());
		throw new MockVerifyCountException(verifyName, times, count, invocations);
	}

	public long Verify(in It<TParameter> parameter, in long index)
	{
		var span = _invocations.GetItemsSpanFrom(index);

		var verifyOutput = new List<(long, Item, ComparisonResult?)>();
		CollectionsMarshal.SetCount(verifyOutput, span.Length);

		for (var i = 0; i < span.Length; i++)
		{
			var verifyParameter = span[i].Invocation.GetParameter(parameter.ValueSetup?.Type);

			if (parameter.ValueSetup.HasValue && !parameter.ValueSetup.Value.Check(verifyParameter, out var result))
			{
				verifyOutput[i] = (span[i].Index, span[i].Invocation, result);
				continue;
			}

			verifyOutput[i] = (span[i].Index, span[i].Invocation, null);
			span[i].Invocation.IsVerified = true;
			return span[i].Index + 1;
		}

		span = _invocations.GetItemsSpanBefore(index);
		for (var i = 0; i < span.Length; i++)
			verifyOutput.Insert(i, (span[i].Index, span[i].Invocation, null));

		var invocations = verifyOutput.GetStrings();
		var verifyName = string.Format(_name, parameter.ToString());
		throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
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
		private readonly string? _prefix;
		private readonly string? _jsonSnapshot;

		public Item(in TParameter parameter, in string? prefix)
		{
			_parameter = parameter;
			_prefix = prefix;

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
			var stringValue = string.IsNullOrEmpty(_jsonSnapshot)
				? _parameter?.ToString() ?? string.Empty
				: _jsonSnapshot;

			return !string.IsNullOrEmpty(_prefix)
				? $"{_prefix} {stringValue}"
				: stringValue;
		}
	}
}
