using System.Runtime.InteropServices;
using System.Text.Json;

namespace MyNihongo.Mock;

public sealed class Invocation<TParameter> : IInvocationProvider
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
		_invocations.Add(invokedIndex, new Item(parameter, invocation: this));
	}

	public void Verify(in It<TParameter> parameter, in Times times, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
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

		var invocations = verifyOutput.GetStrings(invocationProviders);
		var verifyName = string.Format(_name, parameter.ToString());
		throw new MockVerifyCountException(verifyName, times, count, invocations);
	}

	public long Verify(in It<TParameter> parameter, in long index, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
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

		var invocations = verifyOutput.GetStrings(invocationProviders);
		var verifyName = string.Format(_name, parameter.ToString());
		throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
	}

	public void VerifyNoOtherCalls(Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var hasUnverifiedInvocations = _invocations
			.Any(static x => !x.Invocation.IsVerified);

		if (!hasUnverifiedInvocations)
			return;

		// TODO: resume after rework
		var unverifiedItems = _invocations
			.Where(static x => !x.Invocation.IsVerified)
			.Select(static x => new InvocationSnapshot
			{
				Index = x.Index,
				Snapshot = x.Invocation.ToString(),
			})
			.ToArray();

		if (unverifiedItems.Length > 0)
		{
			var verifyName = string.Format(_name, typeof(TParameter).Name);
			throw new MockUnverifiedException(verifyName, unverifiedItems);
		}
	}

	public IEnumerable<IInvocation> GetInvocations()
	{
		return _invocations
			.Select(static x => new InvocationSnapshot
			{
				Index = x.Index,
				Snapshot = x.Invocation.ToString(),
			});
	}

	private sealed class Item
	{
		public bool IsVerified;
		private readonly TParameter _parameter;
		private readonly string? _jsonSnapshot;
		private readonly Invocation<TParameter> _invocation;

		public Item(in TParameter parameter, in Invocation<TParameter> invocation)
		{
			_parameter = parameter;
			_invocation = invocation;

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

			stringValue = !string.IsNullOrEmpty(_invocation._prefix)
				? $"{_invocation._prefix} {stringValue}"
				: stringValue;

			return string.Format(_invocation._name, stringValue);
		}
	}
}
