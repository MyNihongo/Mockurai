namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class StructDependencyServiceMock : IMock<IStructDependencyService>
{
	private Proxy? _proxy;
	private Setup? _invoke;
	private SetupWithParameter? _invokeWithParameter;
	private SetupWithMultipleParameters? _invokeWithMultipleParameters;
	private Setup<StructReturn>? _return;
	private Setup<StructReturn?>? _returnNullable;
	private SetupWithParameter<StructReturn>? _returnWithOneParameter;
	private SetupWithParameter<StructReturn?>? _returnWithOneParameterNullable;
	private SetupWithMultipleParameters<StructReturn>? _returnWithMultipleParameters;
	private SetupWithMultipleParameters<StructReturn?>? _returnWithMultipleParametersNullable;

	public IStructDependencyService Object => _proxy ??= new Proxy(this);

	public Setup SetupInvoke() =>
		_invoke ??= new Setup();

	public SetupWithParameter SetupInvokeWithParameter(in It<StructParameter1> parameter)
	{
		_invokeWithParameter ??= new SetupWithParameter();

		var hashCode = parameter.GetHashCode();
		_invokeWithParameter.SetupParameters(hashCode);
		return _invokeWithParameter;
	}

	public SetupWithMultipleParameters SetupInvokeWithMultipleParameters(in It<StructParameter1> parameter1, in It<StructParameter1> parameter2)
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

	public Setup<StructReturn> SetupReturn() =>
		_return ??= new Setup<StructReturn>();

	public Setup<StructReturn?> SetupReturnNullable() =>
		_returnNullable ??= new Setup<StructReturn?>();

	public SetupWithParameter<StructReturn> SetupReturnWithOneParameter(in It<StructParameter1> parameter)
	{
		_returnWithOneParameter ??= new SetupWithParameter<StructReturn>();

		var hashCode = parameter.GetHashCode();
		_returnWithOneParameter.SetupParameters(hashCode);
		return _returnWithOneParameter;
	}

	public SetupWithParameter<StructReturn?> SetupReturnWithOneParameterNullable(in It<StructParameter1> parameter)
	{
		_returnWithOneParameterNullable ??= new SetupWithParameter<StructReturn?>();

		var hashCode = parameter.GetHashCode();
		_returnWithOneParameterNullable.SetupParameters(hashCode);
		return _returnWithOneParameterNullable;
	}

	public SetupWithMultipleParameters<StructReturn> SetupReturnWithMultipleParameters(in It<StructParameter1> parameter1, in It<StructParameter1> parameter2)
	{
		_returnWithMultipleParameters ??= new SetupWithMultipleParameters<StructReturn>();

		var hashCodes = new[]
		{
			parameter1.GetHashCode(),
			parameter2.GetHashCode(),
		};

		_returnWithMultipleParameters.SetupParameters(hashCodes);
		return _returnWithMultipleParameters;
	}

	public SetupWithMultipleParameters<StructReturn?> SetupReturnWithMultipleParametersNullable(in It<StructParameter1> parameter1, in It<StructParameter1> parameter2)
	{
		_returnWithMultipleParametersNullable ??= new SetupWithMultipleParameters<StructReturn?>();

		var hashCodes = new[]
		{
			parameter1.GetHashCode(),
			parameter2.GetHashCode(),
		};

		_returnWithMultipleParametersNullable.SetupParameters(hashCodes);
		return _returnWithMultipleParametersNullable;
	}

	private sealed class Proxy : IStructDependencyService
	{
		private readonly StructDependencyServiceMock _mock;

		public Proxy(in StructDependencyServiceMock mock)
		{
			_mock = mock;
		}

		public void Invoke()
		{
			_mock._invoke?.Invoke();
		}

		public void InvokeWithParameter(in StructParameter1 parameter)
		{
			var hashCode = parameter.GetHashCode();
			_mock._invokeWithParameter?.Invoke(hashCode);
		}

		public void InvokeWithMultipleParameters(in StructParameter1 parameter1, in StructParameter1 parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			_mock._invokeWithMultipleParameters?.Invoke(hashCodes);
		}

		public StructReturn Return()
		{
			return _mock._return?.Invoke() ?? throw new NullReferenceException("IStructDependencyService#Return() method has not been set up");
		}

		public StructReturn? ReturnNullable()
		{
			return _mock._returnNullable?.Invoke();
		}

		public StructReturn ReturnWithParameter(in StructParameter1 parameter)
		{
			var hashcode = parameter.GetHashCode();
			return _mock._returnWithOneParameter?.TryInvoke(hashcode, out var returnValue) == true ? returnValue : throw new NullReferenceException("IStructDependencyService#ReturnWithParameter() method has not been set up");
		}

		public StructReturn? ReturnWithParameterNullable(in StructParameter1 parameter)
		{
			var hashCode = parameter.GetHashCode();
			return _mock._returnWithOneParameterNullable?.TryInvoke(hashCode, out var returnValue) == true ? returnValue : null;
		}

		public StructReturn ReturnWithMultipleParameters(StructParameter1 parameter1, StructParameter1 parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			return _mock._returnWithMultipleParameters?.TryInvoke(hashCodes, out var returnValue) == true ? returnValue : throw new NullReferenceException("IStructDependencyService#ReturnWithMultipleParameters() method has not been set up");
		}

		public StructReturn? ReturnWithMultipleParametersNullable(StructParameter1 parameter1, StructParameter1 parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			return _mock._returnWithMultipleParametersNullable?.TryInvoke(hashCodes, out var returnValue) == true ? returnValue : null;
		}
	}
}

[Obsolete("Will be generated")]
public static class StructDependencyServiceMockEx
{
	public static ISetup SetupInvoke(this IMock<IStructDependencyService> @this) =>
		((StructDependencyServiceMock)@this).SetupInvoke();

	public static ISetup SetupInvokeWithParameter(this IMock<IStructDependencyService> @this, in It<StructParameter1> parameter) =>
		((StructDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);

	public static ISetup SetupInvokeWithMultipleParameters(this IMock<IStructDependencyService> @this, in It<StructParameter1> parameter1, in It<StructParameter1> parameter2) =>
		((StructDependencyServiceMock)@this).SetupInvokeWithMultipleParameters(parameter1, parameter2);

	public static ISetup<StructReturn> SetupReturn(this IMock<IStructDependencyService> @this) =>
		((StructDependencyServiceMock)@this).SetupReturn();

	public static ISetup<StructReturn?> SetupReturnNullable(this IMock<IStructDependencyService> @this) =>
		((StructDependencyServiceMock)@this).SetupReturnNullable();

	public static ISetup<StructReturn> SetupReturnWithOneParameter(this IMock<IStructDependencyService> @this, in It<StructParameter1> parameter) =>
		((StructDependencyServiceMock)@this).SetupReturnWithOneParameter(parameter);

	public static ISetup<StructReturn?> SetupReturnWithOneParameterNullable(this IMock<IStructDependencyService> @this, in It<StructParameter1> parameter) =>
		((StructDependencyServiceMock)@this).SetupReturnWithOneParameterNullable(parameter);

	public static ISetup<StructReturn> SetupReturnWithMultipleParameters(this IMock<IStructDependencyService> @this, in It<StructParameter1> parameter1, in It<StructParameter1> parameter2) =>
		((StructDependencyServiceMock)@this).SetupReturnWithMultipleParameters(parameter1, parameter2);

	public static ISetup<StructReturn?> SetupReturnWithMultipleParametersNullable(this IMock<IStructDependencyService> @this, in It<StructParameter1> parameter1, in It<StructParameter1> parameter2) =>
		((StructDependencyServiceMock)@this).SetupReturnWithMultipleParametersNullable(parameter1, parameter2);
}
