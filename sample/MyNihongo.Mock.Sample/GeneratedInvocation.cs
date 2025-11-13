using System.Text;
using System.Text.Json;

namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class InvocationIntInt : IInvocationVerify
{
	private readonly string _name;
	private readonly string? _prefix1, _prefix2;
	private readonly InvocationContainer<Item> _invocations = [];

	public InvocationIntInt(in string name, in string? prefix1 = null, in string? prefix2 = null)
	{
		_name = name;
		_prefix1 = prefix1;
		_prefix2 = prefix2;
	}

	public void Register(in InvocationIndex.Counter index, in int parameter1, in int parameter2)
	{
		var invokedIndex = index.Increment();
		_invocations.Add(new Item(invokedIndex, parameter1, parameter2, invocation: this));
	}

	public void Verify(in It<int> parameter1, in It<int> parameter2, in Times times, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var span = _invocations.GetItemsSpan();

		var verifyOutput = new List<(Item, (string, ComparisonResult?)[]?)>();
		CollectionsMarshal.SetCount(verifyOutput, span.Length);

		var count = 0;
		for (var i = 0; i < span.Length; i++)
		{
			var verifyParameter1 = span[i].GetParameter1(parameter1.ValueSetup?.Type);
			var verifyParameter2 = span[i].GetParameter2(parameter2.ValueSetup?.Type);
			(string, ComparisonResult?)[]? verifyResults = null;

			if (parameter1.ValueSetup.HasValue && !parameter1.ValueSetup.Value.Check(verifyParameter1, out var result))
			{
				verifyResults = [("parameter1", result)];
			}

			if (parameter2.ValueSetup.HasValue && !parameter2.ValueSetup.Value.Check(verifyParameter2, out result))
			{
				verifyResults = verifyResults is not null
					? [..verifyResults, ("parameter2", result)]
					: [("parameter2", result)];
			}

			if (verifyResults is not null)
			{
				verifyOutput[i] = (span[i], verifyResults);
				continue;
			}

			verifyOutput[i] = (span[i], null);
			span[i].IsVerified = true;
			count++;
		}

		if (times.Predicate(count))
			return;

		var invocations = verifyOutput.GetStrings(invocationProviders);
		var verifyName = string.Format(_name, parameter1.ToString(_prefix1), parameter2.ToString(_prefix2));
		throw new MockVerifyCountException(verifyName, times, count, invocations);
	}

	public long Verify(in It<int> parameter1, in It<int> parameter2, in long index, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var span = _invocations.GetItemsSpanFrom(index);

		var verifyOutput = new List<(Item, (string, ComparisonResult?)[]?)>();
		CollectionsMarshal.SetCount(verifyOutput, span.Length);

		for (var i = 0; i < span.Length; i++)
		{
			var verifyParameter1 = span[i].GetParameter1(parameter1.ValueSetup?.Type);
			var verifyParameter2 = span[i].GetParameter2(parameter2.ValueSetup?.Type);
			(string, ComparisonResult?)[]? verifyResults = null;

			if (parameter1.ValueSetup.HasValue && !parameter1.ValueSetup.Value.Check(verifyParameter1, out var result))
			{
				verifyResults = [("parameter1", result)];
			}

			if (parameter2.ValueSetup.HasValue && !parameter2.ValueSetup.Value.Check(verifyParameter2, out result))
			{
				verifyResults = verifyResults is not null
					? [..verifyResults, ("parameter2", result)]
					: [("parameter2", result)];
			}

			if (verifyResults is not null)
			{
				verifyOutput[i] = (span[i], verifyResults);
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
		var verifyName = string.Format(_name, parameter1.ToString(_prefix1), parameter2.ToString(_prefix2));
		throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
	}

	public void VerifyNoOtherCalls(Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
		if (unverifiedItems is null)
			return;

		var typeName1 = !string.IsNullOrEmpty(_prefix1) ? $"{_prefix1} Int32" : "Int32";
		var typeName2 = !string.IsNullOrEmpty(_prefix2) ? $"{_prefix2} Int32" : "Int32";
		var verifyName = string.Format(_name, typeName1, typeName2);
		throw new MockUnverifiedException(verifyName, unverifiedItems);
	}

	public IEnumerable<IInvocation> GetInvocations()
	{
		return _invocations;
	}

	private sealed class Item : IInvocation
	{
		private readonly int _parameter1, _parameter2;
		private readonly string? _jsonSnapshot1, _jsonSnapshot2;
		private readonly InvocationIntInt _invocation;

		public Item(in long index, in int parameter1, in int parameter2, in InvocationIntInt invocation)
		{
			_parameter1 = parameter1;
			_parameter2 = parameter2;
			_invocation = invocation;
			Index = index;

			try
			{
				_jsonSnapshot1 = JsonSerializer.Serialize(parameter1);
			}
			catch
			{
				// Swallow
			}

			try
			{
				_jsonSnapshot2 = JsonSerializer.Serialize(parameter2);
			}
			catch
			{
				// Swallow
			}
		}

		public long Index { get; }

		public bool IsVerified { get; set; }

		public int GetParameter1(SetupType? setupType)
		{
			return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshot1)
				? JsonSerializer.Deserialize<int>(_jsonSnapshot1)
				: _parameter1;
		}

		public int GetParameter2(SetupType? setupType)
		{
			return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshot2)
				? JsonSerializer.Deserialize<int>(_jsonSnapshot2)
				: _parameter2;
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			if (!string.IsNullOrEmpty(_invocation._prefix1))
				stringBuilder.Append($"{_invocation._prefix1} ");
			if (!string.IsNullOrEmpty(_jsonSnapshot1))
				stringBuilder.Append(_jsonSnapshot1);
			else
				stringBuilder.Append(_parameter1);

			var parameter1 = stringBuilder.ToString();
			stringBuilder.Clear();

			if (!string.IsNullOrEmpty(_invocation._prefix2))
				stringBuilder.Append($"{_invocation._prefix2} ");
			if (!string.IsNullOrEmpty(_jsonSnapshot2))
				stringBuilder.Append(_jsonSnapshot2);
			else
				stringBuilder.Append(_parameter2);

			var parameter2 = stringBuilder.ToString();
			var value = string.Format(_invocation._name, parameter1, parameter2);
			return $"{Index}: {value}";
		}
	}
}

[Obsolete("Will be generated")]
public sealed class InvocationT1Int<T1> : IInvocationVerify
{
	private readonly string _name;
	private readonly string? _prefix1, _prefix2;
	private readonly InvocationContainer<Item> _invocations = [];

	public InvocationT1Int(in string name, in string? prefix1 = null, in string? prefix2 = null)
	{
		_name = name;
		_prefix1 = prefix1;
		_prefix2 = prefix2;
	}

	public void Register(in InvocationIndex.Counter index, in T1 parameter1, in int parameter2)
	{
		var invokedIndex = index.Increment();
		_invocations.Add(new Item(invokedIndex, parameter1, parameter2, invocation: this));
	}

	public void Verify(in It<T1> parameter1, in It<int> parameter2, in Times times, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var span = _invocations.GetItemsSpan();

		var verifyOutput = new List<(Item, (string, ComparisonResult?)[]?)>();
		CollectionsMarshal.SetCount(verifyOutput, span.Length);

		var count = 0;
		for (var i = 0; i < span.Length; i++)
		{
			var verifyParameter1 = span[i].GetParameter1(parameter1.ValueSetup?.Type);
			var verifyParameter2 = span[i].GetParameter2(parameter2.ValueSetup?.Type);
			(string, ComparisonResult?)[]? verifyResults = null;

			if (parameter1.ValueSetup.HasValue && !parameter1.ValueSetup.Value.Check(verifyParameter1, out var result))
			{
				verifyResults = [("parameter1", result)];
			}

			if (parameter2.ValueSetup.HasValue && !parameter2.ValueSetup.Value.Check(verifyParameter2, out result))
			{
				verifyResults = verifyResults is not null
					? [..verifyResults, ("parameter2", result)]
					: [("parameter2", result)];
			}

			if (verifyResults is not null)
			{
				verifyOutput[i] = (span[i], verifyResults);
				continue;
			}

			verifyOutput[i] = (span[i], null);
			span[i].IsVerified = true;
			count++;
		}

		if (times.Predicate(count))
			return;

		var invocations = verifyOutput.GetStrings(invocationProviders);
		var verifyName = string.Format(_name, parameter1.ToString(_prefix1), parameter2.ToString(_prefix2));
		throw new MockVerifyCountException(verifyName, times, count, invocations);
	}

	public long Verify(in It<T1> parameter1, in It<int> parameter2, in long index, Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var span = _invocations.GetItemsSpanFrom(index);

		var verifyOutput = new List<(Item, (string, ComparisonResult?)[]?)>();
		CollectionsMarshal.SetCount(verifyOutput, span.Length);

		for (var i = 0; i < span.Length; i++)
		{
			var verifyParameter1 = span[i].GetParameter1(parameter1.ValueSetup?.Type);
			var verifyParameter2 = span[i].GetParameter2(parameter2.ValueSetup?.Type);
			(string, ComparisonResult?)[]? verifyResults = null;

			if (parameter1.ValueSetup.HasValue && !parameter1.ValueSetup.Value.Check(verifyParameter1, out var result))
			{
				verifyResults = [("parameter1", result)];
			}

			if (parameter2.ValueSetup.HasValue && !parameter2.ValueSetup.Value.Check(verifyParameter2, out result))
			{
				verifyResults = verifyResults is not null
					? [..verifyResults, ("parameter2", result)]
					: [("parameter2", result)];
			}

			if (verifyResults is not null)
			{
				verifyOutput[i] = (span[i], verifyResults);
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
		var verifyName = string.Format(_name, parameter1.ToString(_prefix1), parameter2.ToString(_prefix2));
		throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
	}

	public void VerifyNoOtherCalls(Func<IEnumerable<IInvocationProvider?>>? invocationProviders = null)
	{
		var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
		if (unverifiedItems is null)
			return;

		var typeName1 = !string.IsNullOrEmpty(_prefix1) ? $"{_prefix1} {typeof(T1)}" : typeof(T1).Name;
		var typeName2 = !string.IsNullOrEmpty(_prefix2) ? $"{_prefix2} Int32" : "Int32";
		var verifyName = string.Format(_name, typeName1, typeName2);
		throw new MockUnverifiedException(verifyName, unverifiedItems);
	}

	public IEnumerable<IInvocation> GetInvocations()
	{
		return _invocations;
	}

	private sealed class Item : IInvocation
	{
		private readonly T1 _parameter1;
		private readonly int _parameter2;
		private readonly string? _jsonSnapshot1, _jsonSnapshot2;
		private readonly InvocationT1Int<T1> _invocation;

		public Item(in long index, in T1 parameter1, in int parameter2, in InvocationT1Int<T1> invocation)
		{
			_parameter1 = parameter1;
			_parameter2 = parameter2;
			_invocation = invocation;
			Index = index;

			try
			{
				_jsonSnapshot1 = JsonSerializer.Serialize(parameter1);
			}
			catch
			{
				// Swallow
			}

			try
			{
				_jsonSnapshot2 = JsonSerializer.Serialize(parameter2);
			}
			catch
			{
				// Swallow
			}
		}

		public long Index { get; }

		public bool IsVerified { get; set; }

		public T1 GetParameter1(SetupType? setupType)
		{
			return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshot1)
				? JsonSerializer.Deserialize<T1>(_jsonSnapshot1)!
				: _parameter1;
		}

		public int GetParameter2(SetupType? setupType)
		{
			return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshot2)
				? JsonSerializer.Deserialize<int>(_jsonSnapshot2)
				: _parameter2;
		}

		public override string ToString()
		{
			var stringBuilder = new StringBuilder();

			if (!string.IsNullOrEmpty(_invocation._prefix1))
				stringBuilder.Append($"{_invocation._prefix1} ");
			if (!string.IsNullOrEmpty(_jsonSnapshot1))
				stringBuilder.Append(_jsonSnapshot1);
			else
				stringBuilder.Append(_parameter1);

			var parameter1 = stringBuilder.ToString();
			stringBuilder.Clear();

			if (!string.IsNullOrEmpty(_invocation._prefix2))
				stringBuilder.Append($"{_invocation._prefix2} ");
			if (!string.IsNullOrEmpty(_jsonSnapshot2))
				stringBuilder.Append(_jsonSnapshot2);
			else
				stringBuilder.Append(_parameter2);

			var parameter2 = stringBuilder.ToString();
			var value = string.Format(_invocation._name, parameter1, parameter2);
			return $"{Index}: {value}";
		}
	}
}
