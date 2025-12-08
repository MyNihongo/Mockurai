namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class PrimitiveDependencyMock : IMock<PrimitiveDependency>
{
	private readonly InvocationIndex.Counter _invocationIndex;
	private readonly Func<IEnumerable<IInvocationProvider?>> _invocationProviders;
	private Proxy? _proxy;

	public PrimitiveDependencyMock(in InvocationIndex.Counter invocationIndex)
	{
		_invocationIndex = invocationIndex;
		_invocationProviders = GetInvocations;
	}

	public PrimitiveDependency Object => _proxy ??= new Proxy(this);

	// Return
	private Setup<int>? _return;
	private Invocation? _returnInvocation;

	public Setup<int> SetupReturn()
	{
		return _return ??= new Setup<int>();
	}

	public void VerifyReturn(in Times times)
	{
		_returnInvocation ??= new Invocation("PrimitiveDependency.Return()");
		_returnInvocation.Verify(times, _invocationProviders);
	}

	public long VerifyReturn(in long index)
	{
		_returnInvocation ??= new Invocation("PrimitiveDependency.Return()");
		return _returnInvocation.Verify(index, _invocationProviders);
	}

	public void VerifyNoOtherCalls()
	{
		_returnInvocation?.VerifyNoOtherCalls();
	}

	private IEnumerable<IInvocationProvider?> GetInvocations()
	{
		yield return _returnInvocation;
	}

	private sealed class Proxy : PrimitiveDependency
	{
		private readonly PrimitiveDependencyMock _mock;

		public Proxy(PrimitiveDependencyMock mock)
		{
			_mock = mock;
		}

		public override int Return()
		{
			_mock._returnInvocation ??= new Invocation("PrimitiveDependency.Return()");
			_mock._returnInvocation.Register(_mock._invocationIndex);
			return _mock._return?.Execute(out var returnValue) == true ? returnValue : 0;
		}
	}
}