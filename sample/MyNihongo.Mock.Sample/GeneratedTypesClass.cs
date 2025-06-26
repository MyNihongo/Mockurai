namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class ClassDependencyServiceMock : IMock<IClassDependencyService>
{
	private Proxy? _proxy;
	private Setup? _invoke;
	private SetupWithParameter<ClassParameter1>? _invokeWithParameter;
	// private SetupWithSeveralParameters? _invokeWithSeveralParameters;
	private Setup<ClassReturn>? _return;
	private Setup<ClassReturn?>? _returnNullable;
	// private SetupWithParameter<ClassReturn>? _returnWithOneParameter;
	// private SetupWithParameter<ClassReturn?>? _returnWithOneParameterNullable;
	// private SetupWithSeveralParameters<ClassReturn>? _returnWithSeveralParameters;
	// private SetupWithSeveralParameters<ClassReturn?>? _returnWithSeveralParametersNullable;

	public IClassDependencyService Object => _proxy ??= new Proxy(this);

	public Setup SetupInvoke() =>
		_invoke ??= new Setup();

	public SetupWithParameter<ClassParameter1> SetupInvokeWithOneParameter(in It<ClassParameter1> parameter = default)
	{
		_invokeWithParameter ??= new SetupWithParameter<ClassParameter1>();
		_invokeWithParameter.SetupParameter(parameter);
		return _invokeWithParameter;
	}

	// public SetupWithSeveralParameters SetupInvokeWithSeveralParameters(in It<ClassParameter1> parameter1, in It<ClassParameter1> parameter2)
	// {
	// 	_invokeWithSeveralParameters ??= new SetupWithSeveralParameters();
	//
	// 	var hashCodes = new[]
	// 	{
	// 		parameter1.GetHashCode(),
	// 		parameter2.GetHashCode(),
	// 	};
	//
	// 	_invokeWithSeveralParameters.SetupParameters(hashCodes);
	// 	return _invokeWithSeveralParameters;
	// }

	public Setup<ClassReturn> SetupReturn() =>
		_return ??= new Setup<ClassReturn>();

	public Setup<ClassReturn?> SetupReturnNullable() =>
		_returnNullable ??= new Setup<ClassReturn?>();

	// public SetupWithParameter<ClassReturn> SetupReturnWithOneParameter(in It<ClassParameter1> parameter = default)
	// {
	// 	_returnWithOneParameter ??= new SetupWithParameter<ClassReturn>();
	//
	// 	var hashCode = parameter.GetHashCode();
	// 	_returnWithOneParameter.SetupParameters(hashCode);
	// 	return _returnWithOneParameter;
	// }

	// public SetupWithParameter<ClassReturn?> SetupReturnWithOneParameterNullable(in It<ClassParameter1> parameter)
	// {
	// 	_returnWithOneParameterNullable ??= new SetupWithParameter<ClassReturn?>();
	//
	// 	var hashCode = parameter.GetHashCode();
	// 	_returnWithOneParameterNullable.SetupParameters(hashCode);
	// 	return _returnWithOneParameterNullable;
	// }

	// public SetupWithSeveralParameters<ClassReturn> SetupReturnWithSeveralParameters(in It<ClassParameter1> parameter1, in It<ClassParameter1> parameter2)
	// {
	// 	_returnWithSeveralParameters ??= new SetupWithSeveralParameters<ClassReturn>();
	//
	// 	var hashCodes = new[]
	// 	{
	// 		parameter1.GetHashCode(),
	// 		parameter2.GetHashCode(),
	// 	};
	//
	// 	_returnWithSeveralParameters.SetupParameters(hashCodes);
	// 	return _returnWithSeveralParameters;
	// }

	// public SetupWithSeveralParameters<ClassReturn?> SetupReturnWithSeveralParametersNullable(in It<ClassParameter1> parameter1, in It<ClassParameter1> parameter2)
	// {
	// 	_returnWithSeveralParametersNullable ??= new SetupWithSeveralParameters<ClassReturn?>();
	//
	// 	var hashCodes = new[]
	// 	{
	// 		parameter1.GetHashCode(),
	// 		parameter2.GetHashCode(),
	// 	};
	//
	// 	_returnWithSeveralParametersNullable.SetupParameters(hashCodes);
	// 	return _returnWithSeveralParametersNullable;
	// }

	private sealed class Proxy : IClassDependencyService
	{
		private readonly ClassDependencyServiceMock _mock;

		public Proxy(in ClassDependencyServiceMock mock)
		{
			_mock = mock;
		}

		public void Invoke()
		{
			_mock._invoke?.Invoke();
		}

		public void InvokeWithOneParameter(in ClassParameter1 parameter)
		{
			var hashCode = parameter.GetHashCode();
			_mock._invokeWithParameter?.Invoke(parameter);
		}

		public void InvokeWithSeveralParameters(in ClassParameter1 parameter1, in ClassParameter1 parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			// _mock._invokeWithSeveralParameters?.Invoke(hashCodes);
		}

		public ClassReturn Return()
		{
			return _mock._return?.Invoke() ?? throw new NullReferenceException("IClassDependencyService#Return() method has not been set up");
		}

		public ClassReturn? ReturnNullable()
		{
			return _mock._returnNullable?.Invoke();
		}

		public ClassReturn ReturnWithOneParameter(in ClassParameter1 parameter)
		{
			var hashcode = parameter.GetHashCode();
			throw new NotImplementedException();
			// return _mock._returnWithOneParameter?.TryInvoke(hashcode, out var returnValue) == true ? returnValue : throw new NullReferenceException("IClassDependencyService#ReturnWithParameter() method has not been set up");
		}

		public ClassReturn? ReturnWithOneParameterNullable(in ClassParameter1 parameter)
		{
			var hashcode = parameter.GetHashCode();
			throw new NotImplementedException();
			// return _mock._returnWithOneParameterNullable?.TryInvoke(hashcode, out var returnValue) == true ? returnValue : null;
		}

		public ClassReturn ReturnWithSeveralParameters(ClassParameter1 parameter1, ClassParameter1 parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			throw new NotImplementedException();
			// return _mock._returnWithSeveralParameters?.TryInvoke(hashCodes, out var returnValue) == true ? returnValue : throw new NullReferenceException("IClassDependencyService#ReturnWithSeveralParameters() method has not been set up");
		}

		public ClassReturn? ReturnWithSeveralParametersNullable(ClassParameter1 parameter1, ClassParameter1 parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			throw new NotImplementedException();
			// return _mock._returnWithSeveralParametersNullable?.TryInvoke(hashCodes, out var returnValue) == true ? returnValue : null;
		}
	}
}

[Obsolete("Will be generated")]
public static class ClassDependencyServiceMockEx
{
	public static ISetup SetupInvoke(this IMock<IClassDependencyService> @this) =>
		((ClassDependencyServiceMock)@this).SetupInvoke();

	public static ISetup SetupInvokeWithOneParameter(this IMock<IClassDependencyService> @this, in It<ClassParameter1> parameter = default) =>
		((ClassDependencyServiceMock)@this).SetupInvokeWithOneParameter(parameter);

	public static ISetup SetupInvokeWithSeveralParameters(this IMock<IClassDependencyService> @this, in It<ClassParameter1> parameter1, in It<ClassParameter1> parameter2) =>
		((ClassDependencyServiceMock)@this).SetupInvokeWithSeveralParameters(parameter1, parameter2);

	public static ISetup<ClassReturn> SetupReturn(this IMock<IClassDependencyService> @this) =>
		((ClassDependencyServiceMock)@this).SetupReturn();

	public static ISetup<ClassReturn?> SetupReturnNullable(this IMock<IClassDependencyService> @this) =>
		((ClassDependencyServiceMock)@this).SetupReturnNullable();

	public static ISetup<ClassReturn> SetupReturnWithOneParameter(this IMock<IClassDependencyService> @this, in It<ClassParameter1> parameter = default) =>
		((ClassDependencyServiceMock)@this).SetupReturnWithOneParameter(parameter);

	public static ISetup<ClassReturn?> SetupReturnWithOneParameterNullable(this IMock<IClassDependencyService> @this, in It<ClassParameter1> parameter) =>
		((ClassDependencyServiceMock)@this).SetupReturnWithOneParameterNullable(parameter);

	public static ISetup<ClassReturn> SetupReturnWithSeveralParameters(this IMock<IClassDependencyService> @this, in It<ClassParameter1> parameter1, in It<ClassParameter1> parameter2) =>
		((ClassDependencyServiceMock)@this).SetupReturnWithSeveralParameters(parameter1, parameter2);

	public static ISetup<ClassReturn?> SetupReturnWithSeveralParametersNullable(this IMock<IClassDependencyService> @this, in It<ClassParameter1> parameter1, in It<ClassParameter1> parameter2) =>
		((ClassDependencyServiceMock)@this).SetupReturnWithSeveralParametersNullable(parameter1, parameter2);
}
