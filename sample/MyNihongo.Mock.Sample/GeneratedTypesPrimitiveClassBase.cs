namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class PrimitiveDependencyBaseMock : IMock<PrimitiveDependencyBase>
{
	private readonly InvocationIndex.Counter _invocationIndex;
	private readonly Func<IEnumerable<IInvocationProvider?>> _invocationProviders;
	private Proxy? _proxy;

	public PrimitiveDependencyBaseMock(in InvocationIndex.Counter invocationIndex)
	{
		_invocationIndex = invocationIndex;
		_invocationProviders = GetInvocations;
	}

	public PrimitiveDependencyBase Object => _proxy ??= new Proxy(this);

	// Invoke
	private Setup? _invoke;
	private Invocation? _invokeInvocation;

	public Setup SetupInvoke()
	{
		return _invoke ??= new Setup();
	}

	public void VerifyInvoke(in Times times)
	{
		_invokeInvocation ??= new Invocation("PrimitiveDependencyBase.Invoke()");
		_invokeInvocation.Verify(times, _invocationProviders);
	}

	public long VerifyInvoke(in long index)
	{
		_invokeInvocation ??= new Invocation("PrimitiveDependencyBase.Invoke()");
		return _invokeInvocation.Verify(index, _invocationProviders);
	}

	// InvokeWithParameter
	private SetupWithParameter<float>? _invokeWithParameter;
	private Invocation<float>? _invokeWithParameterInvocation;

	public SetupWithParameter<float> SetupInvokeWithParameter(in It<float> parameter)
	{
		_invokeWithParameter ??= new SetupWithParameter<float>();
		_invokeWithParameter.SetupParameter(parameter);
		return _invokeWithParameter;
	}

	public void VerifyInvokeWithParameter(in It<float> parameter, in Times times)
	{
		_invokeWithParameterInvocation ??= new Invocation<float>("PrimitiveDependencyBase.InvokeWithParameter({0})");
		_invokeWithParameterInvocation.Verify(parameter, times, _invocationProviders);
	}

	public long VerifyInvokeWithParameter(in It<float> parameter, in long index)
	{
		_invokeWithParameterInvocation ??= new Invocation<float>("PrimitiveDependencyBase.InvokeWithParameter({0})");
		return _invokeWithParameterInvocation.Verify(parameter, index, _invocationProviders);
	}

	public void VerifyNoOtherCalls()
	{
		_invokeInvocation?.VerifyNoOtherCalls();
		_invokeWithParameterInvocation?.VerifyNoOtherCalls();
	}

	private IEnumerable<IInvocationProvider?> GetInvocations()
	{
		yield return _invokeInvocation;
		yield return _invokeWithParameterInvocation;
	}

	private sealed class Proxy : PrimitiveDependencyBase
	{
		private readonly PrimitiveDependencyBaseMock _mock;

		public Proxy(PrimitiveDependencyBaseMock mock)
		{
			_mock = mock;
		}

		public override void Invoke()
		{
			_mock._invokeInvocation ??= new Invocation("PrimitiveDependencyBase.Invoke()");
			_mock._invokeInvocation.Register(_mock._invocationIndex);
			_mock._invoke?.Invoke();
		}

		public override void InvokeWithParameter(float parameter)
		{
			_mock._invokeWithParameterInvocation ??= new Invocation<float>("PrimitiveDependencyBase.InvokeWithParameter({0})");
			_mock._invokeWithParameterInvocation.Register(_mock._invocationIndex, parameter);
			_mock._invokeWithParameter?.Invoke(parameter);
		}
	}
}

public static partial class MockExtensions
{
	extension(IMock<PrimitiveDependencyBase> @this)
	{
		// Invoke
		public ISetup<Action> SetupInvoke() =>
			((PrimitiveDependencyBaseMock)@this).SetupInvoke();

		public void VerifyInvoke(in Times times) =>
			((PrimitiveDependencyBaseMock)@this).VerifyInvoke(times);

		public void VerifyInvoke(in Func<Times> times) =>
			((PrimitiveDependencyBaseMock)@this).VerifyInvoke(times());

		// InvokeWithParameter
		public ISetup<Action<float>> SetupInvokeWithParameter(in It<float> parameter = default) =>
			((PrimitiveDependencyBaseMock)@this).SetupInvokeWithParameter(parameter);

		public void VerifyInvokeWithParameter(in It<float> parameter, in Times times) =>
			((PrimitiveDependencyBaseMock)@this).VerifyInvokeWithParameter(parameter, times);

		public void VerifyInvokeWithParameter(in It<float> parameter, in Func<Times> times) =>
			((PrimitiveDependencyBaseMock)@this).VerifyInvokeWithParameter(parameter, times());
	}
}

public static partial class MockSequenceExtensions
{
	extension(IMockSequence<PrimitiveDependencyBase> @this)
	{
		// Invoke
		public void Invoke()
		{
			var nextIndex = ((PrimitiveDependencyBaseMock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// InvokeWithParameter
		public void InvokeWithParameter(in It<float> parameter)
		{
			var nextIndex = ((PrimitiveDependencyBaseMock)@this.Mock).VerifyInvokeWithParameter(parameter, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}
	}
}