using System.Text;
using System.Text.Json;

namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class InvocationIntInt
{
	private readonly string _name;
	private readonly InvocationContainer<Item> _invocations = [];

	public InvocationIntInt(in string name)
	{
		_name = name;
	}

	public void Register(in InvocationIndex.Counter index, in int parameter1, in int parameter2)
	{
		var invokedIndex = index.Increment();
		_invocations.Add(invokedIndex, new Item(parameter1, parameter2));
	}

	public void Verify(in It<int> parameter1, in It<int> parameter2, in Times times)
	{
		var count = 0;
		foreach (var invocation in _invocations)
		{
			var verifyParameter1 = invocation.Invocation.GetParameter1(parameter1.ValueSetup?.Type);
			var verifyParameter2 = invocation.Invocation.GetParameter2(parameter2.ValueSetup?.Type);

			if (parameter1.ValueSetup.HasValue && !parameter1.ValueSetup.Value.Check(verifyParameter1))
				continue;
			if (parameter2.ValueSetup.HasValue && !parameter2.ValueSetup.Value.Check(verifyParameter2))
				continue;

			invocation.Invocation.IsVerified = true;
			count++;
		}

		if (times.Predicate(count))
			return;

		var verifyName = string.Format(_name, parameter1.ToString(), parameter2.ToString());
		var invocations = _invocations.GetItemStrings();
		throw new MockVerifyCountException(verifyName, times, count, invocations);
	}

	public long Verify(in It<int> parameter1, in It<int> parameter2, in long index)
	{
		foreach (var item in _invocations.GetItemsFrom(index))
		{
			var verifyParameter1 = item.Invocation.GetParameter1(parameter1.ValueSetup?.Type);
			var verifyParameter2 = item.Invocation.GetParameter2(parameter2.ValueSetup?.Type);

			if (parameter1.ValueSetup.HasValue && !parameter1.ValueSetup.Value.Check(verifyParameter1))
				continue;
			if (parameter2.ValueSetup.HasValue && !parameter2.ValueSetup.Value.Check(verifyParameter2))
				continue;

			item.Invocation.IsVerified = true;
			return item.Index + 1;
		}

		var verifyName = string.Format(_name, parameter1.ToString(), parameter2.ToString());
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
			var verifyName = string.Format(_name, "Int32", "Int32");
			throw new MockUnverifiedException(verifyName, unverifiedItems);
		}
	}

	private sealed class Item
	{
		public bool IsVerified;
		private readonly int _parameter1, _parameter2;
		private readonly string? _jsonSnapshot1, _jsonSnapshot2;

		public Item(in int parameter1, in int parameter2)
		{
			_parameter1 = parameter1;
			_parameter2 = parameter2;

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

			if (!string.IsNullOrEmpty(_jsonSnapshot1))
				stringBuilder.Append(_jsonSnapshot1);
			else
				stringBuilder.Append(_parameter1);

			stringBuilder.Append(", ");

			if (!string.IsNullOrEmpty(_jsonSnapshot2))
				stringBuilder.Append(_jsonSnapshot2);
			else
				stringBuilder.Append(_parameter2);

			return stringBuilder.ToString();
		}
	}
}
