namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class PrimitiveDependencyServiceMock : IMock<IPrimitiveDependencyService>
{
	private Proxy? _proxy;
	private Setup? _invoke;
	private Invocation? _invokeInvocation;
	private SetupWithParameter<string>? _invokeWithParameter1;
	private SetupWithParameter<int>? _invokeWithParameter2;
	private SetupIntInt? _invokeWithMultipleParameters;
	private Setup<int>? _return;
	private SetupWithParameter<string, string>? _returnWithOneParameter;
	private SetupIntInt<decimal>? _returnWithMultipleParameters;

	public IPrimitiveDependencyService Object => _proxy ??= new Proxy(this);

	public Setup SetupInvoke()
	{
		return _invoke ??= new Setup();
	}

	public SetupWithParameter<string> SetupInvokeWithParameter(in It<string> parameter)
	{
		_invokeWithParameter1 ??= new SetupWithParameter<string>();
		_invokeWithParameter1.SetupParameter(parameter);
		return _invokeWithParameter1;
	}

	public SetupWithParameter<int> SetupInvokeWithParameter(in It<int> parameter)
	{
		_invokeWithParameter2 ??= new SetupWithParameter<int>();
		_invokeWithParameter2.SetupParameter(parameter);
		return _invokeWithParameter2;
	}

	public SetupIntInt SetupInvokeWithMultipleParameters(in It<int> parameter1, in It<int> parameter2)
	{
		_invokeWithMultipleParameters ??= new SetupIntInt();
		_invokeWithMultipleParameters.SetupParameters(parameter1, parameter2);
		return _invokeWithMultipleParameters;
	}

	public Setup<int> SetupReturn() =>
		_return ??= new Setup<int>();

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

	private sealed class Proxy : IPrimitiveDependencyService
	{
		private readonly PrimitiveDependencyServiceMock _mock;

		public Proxy(PrimitiveDependencyServiceMock mock)
		{
			_mock = mock;
		}

		public void Invoke()
		{
			_mock._invoke?.Invoke();
		}

		public void InvokeWithParameter(in string parameter)
		{
			_mock._invokeWithParameter1?.Invoke(parameter);
		}

		public void InvokeWithParameter(in int parameter)
		{
			_mock._invokeWithParameter2?.Invoke(parameter);
		}

		public void InvokeWithMultipleParameters(in int parameter1, in int parameter2)
		{
			_mock._invokeWithMultipleParameters?.Invoke(parameter1, parameter2);
		}

		public int Return()
		{
			return _mock._return?.Execute(out var returnValue) == true ? returnValue : 0;
		}

		public string ReturnWithParameter(in string parameter)
		{
			return _mock._returnWithOneParameter?.Execute(parameter, out var returnValue) == true ? returnValue! : string.Empty;
		}

		public decimal ReturnWithMultipleParameters(int parameter1, int parameter2)
		{
			return _mock._returnWithMultipleParameters?.Execute(parameter1, parameter2, out var returnValue) == true ? returnValue : 0m;
		}
	}
}

[Obsolete("Will be generated")]
public static class PrimitiveDependencyServiceMockEx
{
	public static ISetup SetupInvoke(this IMock<IPrimitiveDependencyService> @this) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvoke();

	public static ISetup SetupInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter = default) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);

	public static ISetup SetupInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter = default) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);

	public static ISetup SetupInvokeWithMultipleParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1 = default, in It<int> parameter2 = default) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithMultipleParameters(parameter1, parameter2);

	public static ISetup<int> SetupReturn(this IMock<IPrimitiveDependencyService> @this) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturn();

	public static ISetup<string> SetupReturnWithOneParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturnWithOneParameter(parameter);

	public static ISetup<decimal> SetupReturnWithMultipleParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturnWithMultipleParameters(parameter1, parameter2);
}
