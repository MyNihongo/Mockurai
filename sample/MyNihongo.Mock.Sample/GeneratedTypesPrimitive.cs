namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class PrimitiveDependencyServiceMock : IMock<IPrimitiveDependencyService>
{
	private Proxy? _proxy;
	private Setup? _invoke; private Invocation? _invokeInvocation;
	private SetupWithParameter<string>? _invokeWithParameter1; private Invocation<string>? _invokeWithParameterInvocation1;
	private SetupWithParameter<int>? _invokeWithParameter2; private Invocation<int>? _invokeWithParameterInvocation2;
	private SetupIntInt? _invokeWithMultipleParameters; private InvocationIntInt? _invokeWithMultipleParametersInvocation;
	private Setup<int>? _return; private Invocation? _returnInvocation;
	private SetupWithParameter<string, string>? _returnWithOneParameter; private Invocation<string>? _returnWithOneParameterInvocation;
	private SetupIntInt<decimal>? _returnWithMultipleParameters; private InvocationIntInt? _returnWithMultipleParametersInvocation;

	public IPrimitiveDependencyService Object => _proxy ??= new Proxy(this);

	public Setup SetupInvoke()
	{
		return _invoke ??= new Setup();
	}

	public void VerifyInvoke(in Times times)
	{
		_invokeInvocation?.Verify(times);
	}

	public SetupWithParameter<string> SetupInvokeWithParameter(in It<string> parameter)
	{
		_invokeWithParameter1 ??= new SetupWithParameter<string>();
		_invokeWithParameter1.SetupParameter(parameter);
		return _invokeWithParameter1;
	}
	
	public void VerifyInvokeWithParameter(in It<string> parameter, in Times times)
	{
		_invokeWithParameterInvocation1?.Verify(parameter, times);
	}

	public SetupWithParameter<int> SetupInvokeWithParameter(in It<int> parameter)
	{
		_invokeWithParameter2 ??= new SetupWithParameter<int>();
		_invokeWithParameter2.SetupParameter(parameter);
		return _invokeWithParameter2;
	}

	public void VerifyInvokeWithParameter(in It<int> parameter, in Times times)
	{
		_invokeWithParameterInvocation2?.Verify(parameter, times);
	}

	public SetupIntInt SetupInvokeWithMultipleParameters(in It<int> parameter1, in It<int> parameter2)
	{
		_invokeWithMultipleParameters ??= new SetupIntInt();
		_invokeWithMultipleParameters.SetupParameters(parameter1, parameter2);
		return _invokeWithMultipleParameters;
	}

	public void VerifyInvokeWithMultipleParameters(in It<int> parameter1, in It<int> parameter2, in Times times)
	{
		_invokeWithMultipleParametersInvocation?.Verify(parameter1, parameter2, times);
	}

	public Setup<int> SetupReturn()
	{
		return _return ??= new Setup<int>();
	}

	public void VerifyReturn(in Times times)
	{
		_returnInvocation?.Verify(times);
	}

	public SetupWithParameter<string, string> SetupReturnWithOneParameter(in It<string> parameter)
	{
		_returnWithOneParameter ??= new SetupWithParameter<string, string>();
		_returnWithOneParameter.SetupParameter(parameter);
		return _returnWithOneParameter;
	}

	public SetupIntInt<decimal> SetupReturnWithMultipleParameters(in It<int> parameter1, in It<int> parameter2)
	{
		_returnWithMultipleParameters ??= new SetupIntInt<decimal>();
		_returnWithMultipleParameters.SetupParameters(parameter1, parameter2);
		return _returnWithMultipleParameters;
	}

	public void VerifyNoOtherCalls()
	{
		_invokeInvocation?.VerifyNoOtherCalls();
		_invokeWithParameterInvocation1?.VerifyNoOtherCalls();
		_invokeWithParameterInvocation2?.VerifyNoOtherCalls();
		_invokeWithMultipleParametersInvocation?.VerifyNoOtherCalls();
		_returnInvocation?.VerifyNoOtherCalls();
		_returnWithOneParameterInvocation?.VerifyNoOtherCalls();
		_returnWithMultipleParametersInvocation?.VerifyNoOtherCalls();
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
			(_mock._invokeWithMultipleParametersInvocation ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters({0}, {1})")).Register(InvocationIndex.CounterValue, parameter1, parameter2);
			_mock._invokeWithMultipleParameters?.Invoke(parameter1, parameter2);
		}

		public int Return()
		{
			(_mock._returnInvocation ??= new Invocation("IPrimitiveDependencyService#Return()")).Register(InvocationIndex.CounterValue);
			return _mock._return?.Execute(out var returnValue) == true ? returnValue : 0;
		}

		public string ReturnWithParameter(in string parameter)
		{
			(_mock._returnWithOneParameterInvocation ??= new Invocation<string>("IPrimitiveDependencyService#ReturnWithParameter({0})")).Register(InvocationIndex.CounterValue, parameter);
			return _mock._returnWithOneParameter?.Execute(parameter, out var returnValue) == true ? returnValue! : string.Empty;
		}

		public decimal ReturnWithSeveralParameters(int parameter1, int parameter2)
		{
			(_mock._returnWithMultipleParametersInvocation ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters({0}, {1})")).Register(InvocationIndex.CounterValue, parameter1, parameter2);
			return _mock._returnWithMultipleParameters?.Execute(parameter1, parameter2, out var returnValue) == true ? returnValue : 0m;
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
		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithMultipleParameters(parameter1, parameter2);

	public static void VerifyInvokeWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithMultipleParameters(parameter1, parameter2, times);

	public static void VerifyInvokeWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithMultipleParameters(parameter1, parameter2, times());

	public static ISetup<int> SetupReturn(this IMock<IPrimitiveDependencyService> @this) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturn();

	public static void VerifyReturn(this IMock<IPrimitiveDependencyService> @this, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturn(times);

	public static void VerifyReturn(this IMock<IPrimitiveDependencyService> @this, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturn(times());

	public static ISetup<string> SetupReturnWithOneParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturnWithOneParameter(parameter);

	public static ISetup<decimal> SetupReturnWithMultipleParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturnWithMultipleParameters(parameter1, parameter2);

	public static void VerifyNoOtherCalls(this IMock<IPrimitiveDependencyService> @this) =>
		((PrimitiveDependencyServiceMock)@this).VerifyNoOtherCalls();
}
