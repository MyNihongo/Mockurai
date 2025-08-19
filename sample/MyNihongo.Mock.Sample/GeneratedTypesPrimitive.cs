namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class PrimitiveDependencyServiceMock : IMock<IPrimitiveDependencyService>
{
	private Proxy? _proxy;
	private Setup? _invoke;
	private Invocation? _invokeInvocation;
	private SetupWithParameter<string>? _invokeWithParameter1;
	private Invocation<string>? _invokeWithParameterInvocation1;
	private SetupWithParameter<int>? _invokeWithParameter2;
	private Invocation<int>? _invokeWithParameterInvocation2;
	private SetupIntInt? _invokeWithSeveralParameters;
	private InvocationIntInt? _invokeWithSeveralParametersInvocation;
	private Setup<int>? _return;
	private Invocation? _returnInvocation;
	private SetupWithParameter<string, string>? _returnWithParameter;
	private Invocation<string>? _returnWithParameterInvocation;
	private SetupIntInt<decimal>? _returnWithSeveralParameters;
	private InvocationIntInt? _returnWithSeveralParametersInvocation;

	public IPrimitiveDependencyService Object => _proxy ??= new Proxy(this);

	public Setup SetupInvoke()
	{
		return _invoke ??= new Setup();
	}

	public void VerifyInvoke(in Times times)
	{
		_invokeInvocation ??= new Invocation("IPrimitiveDependencyService#Invoke()");
		_invokeInvocation.Verify(times);
	}

	public long VerifyInvoke(in long index)
	{
		_invokeInvocation ??= new Invocation("IPrimitiveDependencyService#Invoke()");
		return _invokeInvocation.Verify(index);
	}

	public SetupWithParameter<string> SetupInvokeWithParameter(in It<string> parameter)
	{
		_invokeWithParameter1 ??= new SetupWithParameter<string>();
		_invokeWithParameter1.SetupParameter(parameter);
		return _invokeWithParameter1;
	}

	public void VerifyInvokeWithParameter(in It<string> parameter, in Times times)
	{
		_invokeWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService#InvokeWithParameter({0})");
		_invokeWithParameterInvocation1.Verify(parameter, times);
	}

	public long VerifyInvokeWithParameter(in It<string> parameter, in long index)
	{
		_invokeWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService#InvokeWithParameter({0})");
		return _invokeWithParameterInvocation1.Verify(parameter, index);
	}

	public SetupWithParameter<int> SetupInvokeWithParameter(in It<int> parameter)
	{
		_invokeWithParameter2 ??= new SetupWithParameter<int>();
		_invokeWithParameter2.SetupParameter(parameter);
		return _invokeWithParameter2;
	}

	public void VerifyInvokeWithParameter(in It<int> parameter, in Times times)
	{
		_invokeWithParameterInvocation2 ??= new Invocation<int>("IPrimitiveDependencyService#InvokeWithParameter({0})");
		_invokeWithParameterInvocation2.Verify(parameter, times);
	}

	public long VerifyInvokeWithParameter(in It<int> parameter, in long index)
	{
		_invokeWithParameterInvocation2 ??= new Invocation<int>("IPrimitiveDependencyService#InvokeWithParameter({0})");
		return _invokeWithParameterInvocation2.Verify(parameter, index);
	}

	public SetupIntInt SetupInvokeWithSeveralParameters(in It<int> parameter1, in It<int> parameter2)
	{
		_invokeWithSeveralParameters ??= new SetupIntInt();
		_invokeWithSeveralParameters.SetupParameters(parameter1, parameter2);
		return _invokeWithSeveralParameters;
	}

	public void VerifyInvokeWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in Times times)
	{
		_invokeWithSeveralParametersInvocation ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters({0}, {1})");
		_invokeWithSeveralParametersInvocation.Verify(parameter1, parameter2, times);
	}

	public long VerifyInvokeWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in long index)
	{
		_invokeWithSeveralParametersInvocation ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters({0}, {1})");
		return _invokeWithSeveralParametersInvocation.Verify(parameter1, parameter2, index);
	}

	public Setup<int> SetupReturn()
	{
		return _return ??= new Setup<int>();
	}

	public void VerifyReturn(in Times times)
	{
		_returnInvocation ??= new Invocation("IPrimitiveDependencyService#Return()");
		_returnInvocation.Verify(times);
	}

	public long VerifyReturn(in long index)
	{
		_returnInvocation ??= new Invocation("IPrimitiveDependencyService#Return()");
		return _returnInvocation.Verify(index);
	}

	public SetupWithParameter<string, string> SetupReturnWithParameter(in It<string> parameter)
	{
		_returnWithParameter ??= new SetupWithParameter<string, string>();
		_returnWithParameter.SetupParameter(parameter);
		return _returnWithParameter;
	}

	public void VerifyReturnWithParameter(in It<string> parameter, in Times times)
	{
		_returnWithParameterInvocation ??= new Invocation<string>("IPrimitiveDependencyService#ReturnWithParameter({0})");
		_returnWithParameterInvocation.Verify(parameter, times);
	}

	public long VerifyReturnWithParameter(in It<string> parameter, in long index)
	{
		_returnWithParameterInvocation ??= new Invocation<string>("IPrimitiveDependencyService#ReturnWithParameter({0})");
		return _returnWithParameterInvocation.Verify(parameter, index);
	}

	public SetupIntInt<decimal> SetupReturnWithSeveralParameters(in It<int> parameter1, in It<int> parameter2)
	{
		_returnWithSeveralParameters ??= new SetupIntInt<decimal>();
		_returnWithSeveralParameters.SetupParameters(parameter1, parameter2);
		return _returnWithSeveralParameters;
	}

	public void VerifyReturnWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in Times times)
	{
		_returnWithSeveralParametersInvocation ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters({0}, {1})");
		_returnWithSeveralParametersInvocation.Verify(parameter1, parameter2, times);
	}

	public long VerifyReturnWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in long index)
	{
		_returnWithSeveralParametersInvocation ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters({0}, {1})");
		return _returnWithSeveralParametersInvocation.Verify(parameter1, parameter2, index);
	}

	public void VerifyNoOtherCalls()
	{
		_invokeInvocation?.VerifyNoOtherCalls();
		_invokeWithParameterInvocation1?.VerifyNoOtherCalls();
		_invokeWithParameterInvocation2?.VerifyNoOtherCalls();
		_invokeWithSeveralParametersInvocation?.VerifyNoOtherCalls();
		_returnInvocation?.VerifyNoOtherCalls();
		_returnWithParameterInvocation?.VerifyNoOtherCalls();
		_returnWithSeveralParametersInvocation?.VerifyNoOtherCalls();
	}

	private sealed class Proxy : IPrimitiveDependencyService
	{
		private readonly PrimitiveDependencyServiceMock _mock;

		public Proxy(PrimitiveDependencyServiceMock mock)
		{
			_mock = mock;
		}

		public void Invoke()
		{
			(_mock._invokeInvocation ??= new Invocation("IPrimitiveDependencyService#Invoke()")).Register(InvocationIndex.CounterValue);
			_mock._invoke?.Invoke();
		}

		public void InvokeWithParameter(in string parameter)
		{
			(_mock._invokeWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService#InvokeWithParameter({0})")).Register(InvocationIndex.CounterValue, parameter);
			_mock._invokeWithParameter1?.Invoke(parameter);
		}

		public void InvokeWithParameter(in int parameter)
		{
			(_mock._invokeWithParameterInvocation2 ??= new Invocation<int>("IPrimitiveDependencyService#InvokeWithParameter({0})")).Register(InvocationIndex.CounterValue, parameter);
			_mock._invokeWithParameter2?.Invoke(parameter);
		}

		public void InvokeWithSeveralParameters(in int parameter1, in int parameter2)
		{
			(_mock._invokeWithSeveralParametersInvocation ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters({0}, {1})")).Register(InvocationIndex.CounterValue, parameter1, parameter2);
			_mock._invokeWithSeveralParameters?.Invoke(parameter1, parameter2);
		}

		public int Return()
		{
			(_mock._returnInvocation ??= new Invocation("IPrimitiveDependencyService#Return()")).Register(InvocationIndex.CounterValue);
			return _mock._return?.Execute(out var returnValue) == true ? returnValue : 0;
		}

		public string ReturnWithParameter(in string parameter)
		{
			(_mock._returnWithParameterInvocation ??= new Invocation<string>("IPrimitiveDependencyService#ReturnWithParameter({0})")).Register(InvocationIndex.CounterValue, parameter);
			return _mock._returnWithParameter?.Execute(parameter, out var returnValue) == true ? returnValue! : string.Empty;
		}

		public decimal ReturnWithSeveralParameters(int parameter1, int parameter2)
		{
			(_mock._returnWithSeveralParametersInvocation ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters({0}, {1})")).Register(InvocationIndex.CounterValue, parameter1, parameter2);
			return _mock._returnWithSeveralParameters?.Execute(parameter1, parameter2, out var returnValue) == true ? returnValue : 0m;
		}
	}
}

[Obsolete("Will be generated")]
public static class PrimitiveDependencyServiceMockEx
{
	public static ISetup SetupInvoke(this IMock<IPrimitiveDependencyService> @this) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvoke();

	public static void VerifyInvoke(this IMock<IPrimitiveDependencyService> @this, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvoke(times);

	public static void VerifyInvoke(this IMock<IPrimitiveDependencyService> @this, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvoke(times());

	public static ISetup SetupInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter = default) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);

	public static void VerifyInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times);

	public static void VerifyInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times());

	public static ISetup SetupInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter = default) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);

	public static void VerifyInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times);

	public static void VerifyInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times());

	public static ISetup SetupInvokeWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1 = default, in It<int> parameter2 = default) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithSeveralParameters(parameter1, parameter2);

	public static void VerifyInvokeWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times);

	public static void VerifyInvokeWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times());

	public static ISetup<int> SetupReturn(this IMock<IPrimitiveDependencyService> @this) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturn();

	public static void VerifyReturn(this IMock<IPrimitiveDependencyService> @this, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturn(times);

	public static void VerifyReturn(this IMock<IPrimitiveDependencyService> @this, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturn(times());

	public static ISetup<string> SetupReturnWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturnWithParameter(parameter);

	public static void VerifyReturnWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(parameter, times);

	public static void VerifyReturnWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(parameter, times());

	public static ISetup<decimal> SetupReturnWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturnWithSeveralParameters(parameter1, parameter2);

	public static void VerifyReturnWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times);

	public static void VerifyReturnWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times());

	public static void VerifyNoOtherCalls(this IMock<IPrimitiveDependencyService> @this) =>
		((PrimitiveDependencyServiceMock)@this).VerifyNoOtherCalls();
}

[Obsolete("Will be generated")]
public static class PrimitiveDependencyServiceMockSequenceEx
{
	public static void Invoke(this IMockSequence<IPrimitiveDependencyService> @this)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void InvokeWithParameter(this IMockSequence<IPrimitiveDependencyService> @this, in It<string> parameter)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithParameter(parameter, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void InvokeWithParameter(this IMockSequence<IPrimitiveDependencyService> @this, in It<int> parameter)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithParameter(parameter, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void InvokeWithSeveralParameters(this IMockSequence<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void Return(this IMockSequence<IPrimitiveDependencyService> @this)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturn(@this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void ReturnWithParameter(this IMockSequence<IPrimitiveDependencyService> @this, in It<string> parameter)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithParameter(parameter, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void ReturnWithSeveralParameters(this IMockSequence<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}
}
