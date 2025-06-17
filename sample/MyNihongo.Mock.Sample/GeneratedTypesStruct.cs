namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class StructDependencyServiceMock : Mock<IStructDependencyService>
{
	private Setup<StructReturn>? _return;
	private Setup<StructReturn?>? _returnNullable;
	private SetupWithParameter<StructReturn>? _returnWithOneParameter;
	private SetupWithParameter<StructReturn?>? _returnWithOneParameterNullable;
	private SetupWithMultipleParameters<StructReturn>? _returnWithMultipleParameters;
	private SetupWithMultipleParameters<StructReturn?>? _returnWithMultipleParametersNullable;

	public Setup<StructReturn> SetupReturn() =>
		_return ??= new Setup<StructReturn>();

	public Setup<StructReturn?> SetupReturnNullable() =>
		_returnNullable ??= new Setup<StructReturn?>();

	public SetupWithParameter<StructReturn> SetupReturnWithOneParameter(in StructParameter1 parameter)
	{
		_returnWithOneParameter ??= new SetupWithParameter<StructReturn>();

		var hashCode = parameter.GetHashCode();
		_returnWithOneParameter.SetupParameters(hashCode);
		return _returnWithOneParameter;
	}

	public SetupWithParameter<StructReturn?> SetupReturnWithOneParameterNullable(in StructParameter1 parameter)
	{
		_returnWithOneParameterNullable ??= new SetupWithParameter<StructReturn?>();

		var hashCode = parameter.GetHashCode();
		_returnWithOneParameterNullable.SetupParameters(hashCode);
		return _returnWithOneParameterNullable;
	}

	public SetupWithMultipleParameters<StructReturn> SetupReturnWithMultipleParameters(in StructParameter1 parameter1, in StructParameter1 parameter2)
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

	public SetupWithMultipleParameters<StructReturn?> SetupReturnWithMultipleParametersNullable(in StructParameter1 parameter1, in StructParameter1 parameter2)
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

	protected override IStructDependencyService CreateObject()
	{
		return new Proxy(this);
	}

	private sealed class Proxy : IStructDependencyService
	{
		private readonly StructDependencyServiceMock _mock;

		public Proxy(in StructDependencyServiceMock mock)
		{
			_mock = mock;
		}

		public StructReturn Return()
		{
			return _mock._return?.Value ?? throw new NullReferenceException("IStructDependencyService#Return() method has not been set up");
		}

		public StructReturn? ReturnNullable()
		{
			return _mock._returnNullable?.Value;
		}

		public StructReturn ReturnWithParameter(in StructParameter1 parameter)
		{
			var hashcode = parameter.GetHashCode();
			return _mock._returnWithOneParameter?.TryGetValue(hashcode, out var returnValue) == true ? returnValue : throw new NullReferenceException("IStructDependencyService#ReturnWithParameter() method has not been set up");
		}

		public StructReturn? ReturnWithParameterNullable(in StructParameter1 parameter)
		{
			var hashCode = parameter.GetHashCode();
			return _mock._returnWithOneParameterNullable?.TryGetValue(hashCode, out var returnValue) == true ? returnValue : null;
		}

		public StructReturn ReturnWithMultipleParameters(StructParameter1 parameter1, StructParameter1 parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			return _mock._returnWithMultipleParameters?.TryGetValue(hashCodes, out var returnValue) == true ? returnValue : throw new NullReferenceException("IStructDependencyService#ReturnWithMultipleParameters() method has not been set up");
		}

		public StructReturn? ReturnWithMultipleParametersNullable(StructParameter1 parameter1, StructParameter1 parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			return _mock._returnWithMultipleParametersNullable?.TryGetValue(hashCodes, out var returnValue) == true ? returnValue : null;
		}
	}
}

[Obsolete("Will be generated")]
public static class StructDependencyServiceMockEx
{
	public static ISetupReturn<StructReturn> SetupReturn(this IMock<IStructDependencyService> @this) =>
		((StructDependencyServiceMock)@this).SetupReturn();

	public static ISetupReturn<StructReturn?> SetupReturnNullable(this IMock<IStructDependencyService> @this) =>
		((StructDependencyServiceMock)@this).SetupReturnNullable();

	public static ISetupReturn<StructReturn> SetupReturnWithOneParameter(this IMock<IStructDependencyService> @this, in StructParameter1 parameter) =>
		((StructDependencyServiceMock)@this).SetupReturnWithOneParameter(parameter);

	public static ISetupReturn<StructReturn?> SetupReturnWithOneParameterNullable(this IMock<IStructDependencyService> @this, in StructParameter1 parameter) =>
		((StructDependencyServiceMock)@this).SetupReturnWithOneParameterNullable(parameter);

	public static ISetupReturn<StructReturn> SetupReturnWithMultipleParameters(this IMock<IStructDependencyService> @this, in StructParameter1 parameter1, in StructParameter1 parameter2) =>
		((StructDependencyServiceMock)@this).SetupReturnWithMultipleParameters(parameter1, parameter2);

	public static ISetupReturn<StructReturn?> SetupReturnWithMultipleParametersNullable(this IMock<IStructDependencyService> @this, in StructParameter1 parameter1, in StructParameter1 parameter2) =>
		((StructDependencyServiceMock)@this).SetupReturnWithMultipleParametersNullable(parameter1, parameter2);
}
