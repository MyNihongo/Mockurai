using System.Runtime.InteropServices;
using System.Text.Json;

namespace MyNihongo.Mockurai;

public sealed class Invocation<TParameter> : IInvocationVerify
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
		_invocations.Add(new Item(invokedIndex, parameter, invocation: this));
	}

	public void Verify(in ItSetup<TParameter> parameter, in Times times, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var span = _invocations.GetItemsSpan();

		var verifyOutput = new List<(Item, ComparisonResult?)>();
		CollectionsMarshal.SetCount(verifyOutput, span.Length);

		var count = 0;
		for (var i = 0; i < span.Length; i++)
		{
			var verifyParameter = span[i].GetParameter(parameter.Type);
			if (!parameter.Check(verifyParameter, out var result))
			{
				verifyOutput[i] = (span[i], result);
				continue;
			}

			verifyOutput[i] = (span[i], null);
			span[i].IsVerified = true;
			count++;
		}

		if (times.Predicate(count))
			return;

		var invocations = verifyOutput.GetStrings(invocationProviders);
		var verifyName = string.Format(_name, parameter.ToString(_prefix));
		throw new MockVerifyCountException(verifyName, times, count, invocations);
	}

	public long Verify(in ItSetup<TParameter> parameter, in long index, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var span = _invocations.GetItemsSpanFrom(index);

		var verifyOutput = new List<(Item, ComparisonResult?)>();
		CollectionsMarshal.SetCount(verifyOutput, span.Length);

		for (var i = 0; i < span.Length; i++)
		{
			var verifyParameter = span[i].GetParameter(parameter.Type);

			if (!parameter.Check(verifyParameter, out var result))
			{
				verifyOutput[i] = (span[i], result);
				continue;
			}

			verifyOutput[i] = (span[i], null);
			span[i].IsVerified = true;
			return span[i].Index + 1;
		}

		if (invocationProviders is null)
		{
			span = _invocations.GetItemsSpanBefore(index);
			for (var i = 0; i < span.Length; i++)
				verifyOutput.Insert(i, (span[i], null));
		}

		var invocations = verifyOutput.GetStrings(invocationProviders);
		var verifyName = string.Format(_name, parameter.ToString(_prefix));
		throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
	}

	public void VerifyNoOtherCalls(Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
		if (unverifiedItems is null)
			return;

		var typeName = !string.IsNullOrEmpty(_prefix)
			? $"{_prefix} {typeof(TParameter).Name}"
			: typeof(TParameter).Name;
		
		var verifyName = string.Format(_name, typeName);
		throw new MockUnverifiedException(verifyName, unverifiedItems);
	}

	public IEnumerable<IInvocation> GetInvocations()
	{
		return _invocations;
	}

	private sealed class Item : IInvocation
	{
		private readonly TParameter _parameter;
		private readonly string? _jsonSnapshot;
		private readonly Invocation<TParameter> _invocation;

		public Item(in long index, in TParameter parameter, in Invocation<TParameter> invocation)
		{
			_parameter = parameter;
			_invocation = invocation;
			Index = index;

			try
			{
				_jsonSnapshot = JsonSerializer.Serialize(parameter);
			}
			catch
			{
				// Swallow
			}
		}

		public long Index { get; }

		public bool IsVerified { get; set; }

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

			stringValue = string.Format(_invocation._name, stringValue);
			return $"{Index}: {stringValue}";
		}
	}
}
