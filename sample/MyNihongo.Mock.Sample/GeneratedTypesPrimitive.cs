// namespace MyNihongo.Mock.Sample;
//
// [Obsolete("Will be generated")]
// public sealed class PrimitiveDependencyServiceMock : IMock<IPrimitiveDependencyService>
// {
// 	private Proxy? _proxy;
// 	private Setup? _invoke;
// 	private SetupWithParameter? _invokeWithParameter1;
// 	private SetupWithParameter? _invokeWithParameter2;
// 	private SetupWithMultipleParameters? _invokeWithMultipleParameters;
// 	private Setup<int>? _return;
// 	private SetupWithParameter<string>? _returnWithOneParameter;
// 	private SetupWithMultipleParameters<decimal>? _returnWithMultipleParameters;
//
// 	public IPrimitiveDependencyService Object => _proxy ??= new Proxy(this);
//
// 	public Setup SetupInvoke() =>
// 		_invoke ??= new Setup();
//
// 	public SetupWithParameter SetupInvokeWithParameter(in It<string> parameter)
// 	{
// 		_invokeWithParameter1 ??= new SetupWithParameter();
//
// 		var hashCode = parameter.GetHashCode();
// 		_invokeWithParameter1.SetupParameters(hashCode);
// 		return _invokeWithParameter1;
// 	}
//
// 	public SetupWithParameter SetupInvokeWithParameter(in It<int> parameter)
// 	{
// 		_invokeWithParameter2 ??= new SetupWithParameter();
//
// 		var hashCode = parameter.GetHashCode();
// 		_invokeWithParameter2.SetupParameters(hashCode);
// 		return _invokeWithParameter2;
// 	}
//
// 	public SetupWithMultipleParameters SetupInvokeWithMultipleParameters(in It<int> parameter1, in It<int> parameter2)
// 	{
// 		_invokeWithMultipleParameters ??= new SetupWithMultipleParameters();
//
// 		var hashCodes = new[]
// 		{
// 			parameter1.GetHashCode(),
// 			parameter2.GetHashCode(),
// 		};
//
// 		_invokeWithMultipleParameters.SetupParameters(hashCodes);
// 		return _invokeWithMultipleParameters;
// 	}
//
// 	public Setup<int> SetupReturn() =>
// 		_return ??= new Setup<int>();
//
// 	public SetupWithParameter<string> SetupReturnWithOneParameter(in It<string> parameter)
// 	{
// 		_returnWithOneParameter ??= new SetupWithParameter<string>();
//
// 		var hashCode = parameter.GetHashCode();
// 		_returnWithOneParameter.SetupParameters(hashCode);
// 		return _returnWithOneParameter;
// 	}
//
// 	public SetupWithMultipleParameters<decimal> SetupReturnWithMultipleParameters(in It<int> parameter1, in It<int> parameter2)
// 	{
// 		_returnWithMultipleParameters ??= new SetupWithMultipleParameters<decimal>();
//
// 		var hashCodes = new[]
// 		{
// 			parameter1.GetHashCode(),
// 			parameter2.GetHashCode(),
// 		};
//
// 		_returnWithMultipleParameters.SetupParameters(hashCodes);
// 		return _returnWithMultipleParameters;
// 	}
//
// 	private sealed class Proxy : IPrimitiveDependencyService
// 	{
// 		private readonly PrimitiveDependencyServiceMock _mock;
//
// 		public Proxy(PrimitiveDependencyServiceMock mock)
// 		{
// 			_mock = mock;
// 		}
//
// 		public void Invoke()
// 		{
// 			_mock._invoke?.Invoke();
// 		}
//
// 		public void InvokeWithParameter(in string parameter)
// 		{
// 			var hashCode = parameter.GetHashCode();
// 			_mock._invokeWithParameter1?.Invoke(hashCode);
// 		}
//
// 		public void InvokeWithParameter(in int parameter)
// 		{
// 			var hashCode = parameter.GetHashCode();
// 			_mock._invokeWithParameter2?.Invoke(hashCode);
// 		}
//
// 		public void InvokeWithMultipleParameters(in int parameter1, in int parameter2)
// 		{
// 			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
// 			_mock._invokeWithMultipleParameters?.Invoke(hashCodes);
// 		}
//
// 		public int Return() =>
// 			_mock._return?.Invoke() ?? 0;
//
// 		public string ReturnWithParameter(in string parameter)
// 		{
// 			var hashcode = parameter.GetHashCode();
// 			return _mock._returnWithOneParameter?.TryInvoke(hashcode, out var returnValue) == true ? returnValue! : string.Empty;
// 		}
//
// 		public decimal ReturnWithMultipleParameters(int parameter1, int parameter2)
// 		{
// 			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
// 			return _mock._returnWithMultipleParameters?.TryInvoke(hashCodes, out var returnValue) == true ? returnValue : 0m;
// 		}
// 	}
// }
//
// [Obsolete("Will be generated")]
// public static class PrimitiveDependencyServiceMockEx
// {
// 	public static ISetup SetupInvoke(this IMock<IPrimitiveDependencyService> @this) =>
// 		((PrimitiveDependencyServiceMock)@this).SetupInvoke();
//
// 	public static ISetup SetupInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter = default) =>
// 		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);
//
// 	public static ISetup SetupInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter = default) =>
// 		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);
//
// 	public static ISetup SetupInvokeWithMultipleParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1 = default, in It<int> parameter2 = default) =>
// 		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithMultipleParameters(parameter1, parameter2);
//
// 	public static ISetup<int> SetupReturn(this IMock<IPrimitiveDependencyService> @this) =>
// 		((PrimitiveDependencyServiceMock)@this).SetupReturn();
//
// 	public static ISetup<string> SetupReturnWithOneParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter) =>
// 		((PrimitiveDependencyServiceMock)@this).SetupReturnWithOneParameter(parameter);
//
// 	public static ISetup<decimal> SetupReturnWithMultipleParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2) =>
// 		((PrimitiveDependencyServiceMock)@this).SetupReturnWithMultipleParameters(parameter1, parameter2);
// }
