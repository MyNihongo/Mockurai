namespace MyNihongo.Mock.Sample;

public readonly ref struct Times
{
	public readonly int Count;

	private Times(in int count)
	{
		Count = count;
	}
}

public sealed class Invocation
{
	private readonly string _name;
	private readonly InvocationContainer<bool> _invocations = [];
	private bool _isVerified;

	public Invocation(in string name)
	{
		_name = name;
	}

	public void Register(ref long index)
	{
		var invokedIndex = Interlocked.Increment(ref index);
		_invocations.Add(invokedIndex, false);
	}

	public void Verify(in Times times)
	{
		if (_invocations.Count != times.Count)
			throw new MockVerifyException(_name, times.Count, _invocations.Count);

		_isVerified = true;
	}

	public long Verify(in long index)
	{
		throw new NotImplementedException();
	}

	public void VerifyNoOtherCalls()
	{
		if (_isVerified)
			return;

		foreach (var invocation in _invocations)
		{
			if (!invocation.Item2)
				throw new MockVerifyException(_name, invocation.Item1);
		}
	}
}
