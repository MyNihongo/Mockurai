namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class RecordDependencyServiceMock : IMock<IRecordDependencyService>
{
	private Proxy? _proxy;
	private Setup<RecordReturn>? _return;
	private Setup<RecordReturn?>? _returnNullable;
	private SetupWithParameter<RecordReturn>? _returnWithOneParameter;
	private SetupWithParameter<RecordReturn?>? _returnWithOneParameterNullable;
	private SetupWithMultipleParameters<RecordReturn>? _returnWithMultipleParameters;
	private SetupWithMultipleParameters<RecordReturn?>? _returnWithMultipleParametersNullable;

	public IRecordDependencyService Object => _proxy ??= new Proxy(this);

	public Setup<RecordReturn> SetupReturn() =>
		_return ??= new Setup<RecordReturn>();

	public Setup<RecordReturn?> SetupReturnNullable() =>
		_returnNullable ??= new Setup<RecordReturn?>();

	public SetupWithParameter<RecordReturn> SetupReturnWithOneParameter(in RecordParameter1 parameter)
	{
		_returnWithOneParameter ??= new SetupWithParameter<RecordReturn>();

		var hashCode = parameter.GetHashCode();
		_returnWithOneParameter.SetupParameters(hashCode);
		return _returnWithOneParameter;
	}

	public SetupWithParameter<RecordReturn?> SetupReturnWithOneParameterNullable(in RecordParameter1 parameter)
	{
		_returnWithOneParameterNullable ??= new SetupWithParameter<RecordReturn?>();

		var hashCode = parameter.GetHashCode();
		_returnWithOneParameterNullable.SetupParameters(hashCode);
		return _returnWithOneParameterNullable;
	}

	public SetupWithMultipleParameters<RecordReturn> SetupReturnWithMultipleParameters(in RecordParameter1 parameter1, in RecordParameter1 parameter2)
	{
		_returnWithMultipleParameters ??= new SetupWithMultipleParameters<RecordReturn>();

		var hashCodes = new[]
		{
			parameter1.GetHashCode(),
			parameter2.GetHashCode(),
		};

		_returnWithMultipleParameters.SetupParameters(hashCodes);
		return _returnWithMultipleParameters;
	}

	public SetupWithMultipleParameters<RecordReturn?> SetupReturnWithMultipleParametersNullable(in RecordParameter1 parameter1, in RecordParameter1 parameter2)
	{
		_returnWithMultipleParametersNullable ??= new SetupWithMultipleParameters<RecordReturn?>();

		var hashCodes = new[]
		{
			parameter1.GetHashCode(),
			parameter2.GetHashCode(),
		};

		_returnWithMultipleParametersNullable.SetupParameters(hashCodes);
		return _returnWithMultipleParametersNullable;
	}

	private sealed class Proxy : IRecordDependencyService
	{
		private readonly RecordDependencyServiceMock _mock;

		public Proxy(in RecordDependencyServiceMock mock)
		{
			_mock = mock;
		}

		public RecordReturn Return()
		{
			return _mock._return?.Value ?? throw new NullReferenceException("IRecordDependencyService#Return() method has not been set up");
		}

		public RecordReturn? ReturnNullable()
		{
			return _mock._returnNullable?.Value;
		}

		public RecordReturn ReturnWithParameter(in RecordParameter1 parameter)
		{
			var hashCode = parameter.GetHashCode();
			return _mock._returnWithOneParameter?.TryGetValue(hashCode, out var returnValue) == true ? returnValue : throw new NullReferenceException("IRecordDependencyService#ReturnWithParameter() method has not been set up");
		}

		public RecordReturn? ReturnWithParameterNullable(in RecordParameter1 parameter)
		{
			var hashCode = parameter.GetHashCode();
			return _mock._returnWithOneParameterNullable?.TryGetValue(hashCode, out var returnValue) == true ? returnValue : null;
		}

		public RecordReturn ReturnWithMultipleParameters(RecordParameter1 parameter1, RecordParameter1 parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			return _mock._returnWithMultipleParameters?.TryGetValue(hashCodes, out var returnValue) == true ? returnValue : throw new NullReferenceException("IRecordDependencyService#ReturnWithMultipleParameters() method has not been set up");
		}

		public RecordReturn? ReturnWithMultipleParametersNullable(RecordParameter1 parameter1, RecordParameter1 parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			return _mock._returnWithMultipleParametersNullable?.TryGetValue(hashCodes, out var returnValue) == true ? returnValue : null;
		}
	}
}

[Obsolete("Will be generated")]
public static class RecordDependencyServiceMockEx
{
	public static ISetupReturn<RecordReturn> SetupReturn(this IMock<IRecordDependencyService> @this) =>
		((RecordDependencyServiceMock)@this).SetupReturn();

	public static ISetupReturn<RecordReturn?> SetupReturnNullable(this IMock<IRecordDependencyService> @this) =>
		((RecordDependencyServiceMock)@this).SetupReturnNullable();

	public static ISetupReturn<RecordReturn> SetupReturnWithOneParameter(this IMock<IRecordDependencyService> @this, in RecordParameter1 parameter) =>
		((RecordDependencyServiceMock)@this).SetupReturnWithOneParameter(parameter);

	public static ISetupReturn<RecordReturn?> SetupReturnWithOneParameterNullable(this IMock<IRecordDependencyService> @this, in RecordParameter1 parameter) =>
		((RecordDependencyServiceMock)@this).SetupReturnWithOneParameterNullable(parameter);

	public static ISetupReturn<RecordReturn> SetupReturnWithMultipleParameters(this IMock<IRecordDependencyService> @this, in RecordParameter1 parameter1, in RecordParameter1 parameter2) =>
		((RecordDependencyServiceMock)@this).SetupReturnWithMultipleParameters(parameter1, parameter2);

	public static ISetupReturn<RecordReturn?> SetupReturnWithMultipleParametersNullable(this IMock<IRecordDependencyService> @this, in RecordParameter1 parameter1, in RecordParameter1 parameter2) =>
		((RecordDependencyServiceMock)@this).SetupReturnWithMultipleParametersNullable(parameter1, parameter2);
}
