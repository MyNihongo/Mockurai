namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class PrimitiveDependencyServiceMock<T> : IMock<IPrimitiveDependencyService<T>>
{
	private readonly InvocationIndex.Counter _invocationIndex;
	private readonly Func<IEnumerable<IInvocationProvider?>> _invocationProviders;
	private Proxy? _proxy;
	public IPrimitiveDependencyService<T> Object => _proxy ??= new Proxy(this);

	public PrimitiveDependencyServiceMock(in InvocationIndex.Counter invocationIndex)
	{
		_invocationIndex = invocationIndex;
		_invocationProviders = GetInvocations;
	}

	// GetOnly
	private Setup<T>? _getOnlyGet;
	private Invocation? _getOnlyGetInvocation;

	public Setup<T> SetupGetGetOnly()
	{
		return _getOnlyGet ??= new Setup<T>();
	}

	public void VerifyGetGetOnly(in Times times)
	{
		_getOnlyGetInvocation ??= new Invocation($"PrimitiveDependencyServiceMock<{typeof(T).Name}>.GetOnly.get");
		_getOnlyGetInvocation.Verify(times, _invocationProviders);
	}

	public long VerifyGetGetOnly(in long index)
	{
		_getOnlyGetInvocation ??= new Invocation($"PrimitiveDependencyServiceMock<{typeof(T).Name}>.GetOnly.get");
		return _getOnlyGetInvocation.Verify(index, _invocationProviders);
	}

	// GetSet
	private Setup<T>? _getSetGet;
	private Invocation? _getSetGetInvocation;
	private SetupWithParameter<T>? _getSetSet;
	private Invocation<T>? _getSetSetInvocation;

	public Setup<T> SetupGetGetSet()
	{
		return _getSetGet ??= new Setup<T>();
	}

	public void VerifyGetGetSet(in Times times)
	{
		_getSetGetInvocation ??= new Invocation($"PrimitiveDependencyServiceMock<{typeof(T).Name}>.GetSet.get");
		_getSetGetInvocation.Verify(times, _invocationProviders);
	}

	public long VerifyGetGetSet(in long index)
	{
		_getSetGetInvocation ??= new Invocation($"PrimitiveDependencyServiceMock<{typeof(T).Name}>.GetSet.get");
		return _getSetGetInvocation.Verify(index, _invocationProviders);
	}

	public SetupWithParameter<T> SetupSetGetSet(in It<T> value)
	{
		_getSetSet ??= new SetupWithParameter<T>();
		_getSetSet.SetupParameter(value);
		return _getSetSet;
	}

	public void VerifySetGetSet(in It<T> value, in Times times)
	{
		_getSetSetInvocation ??= new Invocation<T>($"PrimitiveDependencyServiceMock<{typeof(T).Name}>.GetSet.set = {{0}}");
		_getSetSetInvocation.Verify(value, times, _invocationProviders);
	}

	public long VerifySetGetSet(in It<T> value, in long index)
	{
		_getSetSetInvocation ??= new Invocation<T>($"PrimitiveDependencyServiceMock<{typeof(T).Name}>.GetSet.set = {{0}}");
		return _getSetSetInvocation.Verify(value, index, _invocationProviders);
	}

	public void VerifyNoOtherCalls()
	{
		_getOnlyGetInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_getSetGetInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_getSetSetInvocation?.VerifyNoOtherCalls(_invocationProviders);
	}

	private IEnumerable<IInvocationProvider?> GetInvocations()
	{
		yield return _getOnlyGetInvocation;
		yield return _getSetGetInvocation;
		yield return _getSetSetInvocation;
	}

	private sealed class Proxy : IPrimitiveDependencyService<T>
	{
		private readonly PrimitiveDependencyServiceMock<T> _mock;

		public Proxy(PrimitiveDependencyServiceMock<T> mock)
		{
			_mock = mock;
		}

		public T GetOnly
		{
			get
			{
				_mock._getOnlyGetInvocation ??= new Invocation($"PrimitiveDependencyServiceMock<{typeof(T).Name}>.GetOnly.get");
				_mock._getOnlyGetInvocation.Register(_mock._invocationIndex);
				return _mock._getOnlyGet?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
		}

		public T GetSet
		{
			get
			{
				_mock._getSetGetInvocation ??= new Invocation($"PrimitiveDependencyServiceMock<{typeof(T).Name}>.GetSet.get");
				_mock._getSetGetInvocation.Register(_mock._invocationIndex);
				return _mock._getSetGet?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			set
			{
				_mock._getSetSetInvocation ??= new Invocation<T>($"PrimitiveDependencyServiceMock<{typeof(T).Name}>.GetSet.set = {{0}}");
				_mock._getSetSetInvocation.Register(_mock._invocationIndex, value);
				_mock._getSetSet?.Invoke(value);
			}
		}

		public void InvokeWithParameter(T parameter)
		{
			throw new NotImplementedException();
		}

		public void InvokeWithSeveralParameters<TParameter>(TParameter param1, T param2)
		{
			throw new NotImplementedException();
		}

		public TReturn Return<TReturn>()
		{
			throw new NotImplementedException();
		}

		public TReturn ReturnWithParameter<TReturn>(T parameter)
		{
			throw new NotImplementedException();
		}

		public T ReturnWithSeveralParameters<TParameter1, TParameter2>(TParameter1 param1, TParameter2 param2)
		{
			throw new NotImplementedException();
		}
	}
}
