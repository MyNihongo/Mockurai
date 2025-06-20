namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class ClassDependencyServiceMock : IMock<IClassDependencyService>
{
	private Proxy? _proxy;
	private Setup? _invoke;
	private SetupWithParameter? _invokeWithParameter;
	private SetupWithMultipleParameters? _invokeWithMultipleParameters;
	private Setup<ClassReturn>? _return;
	private Setup<ClassReturn?>? _returnNullable;
	private SetupWithParameter<ClassReturn>? _returnWithOneParameter;
	private SetupWithParameter<ClassReturn?>? _returnWithOneParameterNullable;
	private SetupWithMultipleParameters<ClassReturn>? _returnWithMultipleParameters;
	private SetupWithMultipleParameters<ClassReturn?>? _returnWithMultipleParametersNullable;

	public IClassDependencyService Object => _proxy ??= new Proxy(this);

	public Setup SetupInvoke() =>
		_invoke ??= new Setup();

	public SetupWithParameter SetupInvokeWithParameter(in It<ClassParameter1> parameter)
	{
		_invokeWithParameter ??= new SetupWithParameter();

		var hashCode = parameter.GetHashCode();
		_invokeWithParameter.SetupParameters(hashCode);
		return _invokeWithParameter;
	}

	public SetupWithMultipleParameters SetupInvokeWithMultipleParameters(in It<ClassParameter1> parameter1, in It<ClassParameter1> parameter2)
	{
		_invokeWithMultipleParameters ??= new SetupWithMultipleParameters();

		var hashCodes = new[]
		{
			parameter1.GetHashCode(),
			parameter2.GetHashCode(),
		};

		_invokeWithMultipleParameters.SetupParameters(hashCodes);
		return _invokeWithMultipleParameters;
	}

	public Setup<ClassReturn> SetupReturn() =>
		_return ??= new Setup<ClassReturn>();

	public Setup<ClassReturn?> SetupReturnNullable() =>
		_returnNullable ??= new Setup<ClassReturn?>();

	public SetupWithParameter<ClassReturn> SetupReturnWithOneParameter(in It<ClassParameter1> parameter)
	{
		_returnWithOneParameter ??= new SetupWithParameter<ClassReturn>();

		var hashCode = parameter.GetHashCode();
		_returnWithOneParameter.SetupParameters(hashCode);
		return _returnWithOneParameter;
	}

	public SetupWithParameter<ClassReturn?> SetupReturnWithOneParameterNullable(in It<ClassParameter1> parameter)
	{
		_returnWithOneParameterNullable ??= new SetupWithParameter<ClassReturn?>();

		var hashCode = parameter.GetHashCode();
		_returnWithOneParameterNullable.SetupParameters(hashCode);
		return _returnWithOneParameterNullable;
	}

	public SetupWithMultipleParameters<ClassReturn> SetupReturnWithMultipleParameters(in It<ClassParameter1> parameter1, in It<ClassParameter1> parameter2)
	{
		_returnWithMultipleParameters ??= new SetupWithMultipleParameters<ClassReturn>();

		var hashCodes = new[]
		{
			parameter1.GetHashCode(),
			parameter2.GetHashCode(),
		};

		_returnWithMultipleParameters.SetupParameters(hashCodes);
		return _returnWithMultipleParameters;
	}

	public SetupWithMultipleParameters<ClassReturn?> SetupReturnWithMultipleParametersNullable(in It<ClassParameter1> parameter1, in It<ClassParameter1> parameter2)
	{
		_returnWithMultipleParametersNullable ??= new SetupWithMultipleParameters<ClassReturn?>();

		var hashCodes = new[]
		{
			parameter1.GetHashCode(),
			parameter2.GetHashCode(),
		};

		_returnWithMultipleParametersNullable.SetupParameters(hashCodes);
		return _returnWithMultipleParametersNullable;
	}

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

		public void InvokeWithParameter(in ClassParameter1 parameter)
		{
			var hashCode = parameter.GetHashCode();
			_mock._invokeWithParameter?.Invoke(hashCode);
		}

		public void InvokeWithMultipleParameters(in ClassParameter1 parameter1, in ClassParameter1 parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			_mock._invokeWithMultipleParameters?.Invoke(hashCodes);
		}

		public ClassReturn Return()
		{
			return _mock._return?.Invoke() ?? throw new NullReferenceException("IClassDependencyService#Return() method has not been set up");
		}

		public ClassReturn? ReturnNullable()
		{
			return _mock._returnNullable?.Invoke();
		}

		public ClassReturn ReturnWithParameter(in ClassParameter1 parameter)
		{
			var hashcode = parameter.GetHashCode();
			return _mock._returnWithOneParameter?.TryInvoke(hashcode, out var returnValue) == true ? returnValue : throw new NullReferenceException("IClassDependencyService#ReturnWithParameter() method has not been set up");
		}

		public ClassReturn? ReturnWithParameterNullable(in ClassParameter1 parameter)
		{
			var hashcode = parameter.GetHashCode();
			return _mock._returnWithOneParameterNullable?.TryInvoke(hashcode, out var returnValue) == true ? returnValue : null;
		}

		public ClassReturn ReturnWithMultipleParameters(ClassParameter1 parameter1, ClassParameter1 parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			return _mock._returnWithMultipleParameters?.TryInvoke(hashCodes, out var returnValue) == true ? returnValue : throw new NullReferenceException("IClassDependencyService#ReturnWithMultipleParameters() method has not been set up");
		}

		public ClassReturn? ReturnWithMultipleParametersNullable(ClassParameter1 parameter1, ClassParameter1 parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			return _mock._returnWithMultipleParametersNullable?.TryInvoke(hashCodes, out var returnValue) == true ? returnValue : null;
		}
	}
}

[Obsolete("Will be generated")]
public static class ClassDependencyServiceMockEx
{
	public static ISetup SetupInvoke(this IMock<IClassDependencyService> @this) =>
		((ClassDependencyServiceMock)@this).SetupInvoke();

	public static ISetup SetupInvokeWithParameter(this IMock<IClassDependencyService> @this, in It<ClassParameter1> parameter = default) =>
		((ClassDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);

	public static ISetup SetupInvokeWithMultipleParameters(this IMock<IClassDependencyService> @this, in It<ClassParameter1> parameter1, in It<ClassParameter1> parameter2) =>
		((ClassDependencyServiceMock)@this).SetupInvokeWithMultipleParameters(parameter1, parameter2);

	public static ISetup<ClassReturn> SetupReturn(this IMock<IClassDependencyService> @this) =>
		((ClassDependencyServiceMock)@this).SetupReturn();

	public static ISetup<ClassReturn?> SetupReturnNullable(this IMock<IClassDependencyService> @this) =>
		((ClassDependencyServiceMock)@this).SetupReturnNullable();

	public static ISetup<ClassReturn> SetupReturnWithOneParameter(this IMock<IClassDependencyService> @this, in It<ClassParameter1> parameter) =>
		((ClassDependencyServiceMock)@this).SetupReturnWithOneParameter(parameter);

	public static ISetup<ClassReturn?> SetupReturnWithOneParameterNullable(this IMock<IClassDependencyService> @this, in It<ClassParameter1> parameter) =>
		((ClassDependencyServiceMock)@this).SetupReturnWithOneParameterNullable(parameter);

	public static ISetup<ClassReturn> SetupReturnWithMultipleParameters(this IMock<IClassDependencyService> @this, in It<ClassParameter1> parameter1, in It<ClassParameter1> parameter2) =>
		((ClassDependencyServiceMock)@this).SetupReturnWithMultipleParameters(parameter1, parameter2);

	public static ISetup<ClassReturn?> SetupReturnWithMultipleParametersNullable(this IMock<IClassDependencyService> @this, in It<ClassParameter1> parameter1, in It<ClassParameter1> parameter2) =>
		((ClassDependencyServiceMock)@this).SetupReturnWithMultipleParametersNullable(parameter1, parameter2);
}
