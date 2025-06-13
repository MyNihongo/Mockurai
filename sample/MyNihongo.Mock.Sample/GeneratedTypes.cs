namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class SampleMock : Mock<IPrimitiveDependencyService>
{
	private Setup<int>? _return;
	private SetupWithParameter<string>? _returnWithOneParameter;
	private SetupWithMultipleParameters<decimal>? _returnWithMultipleParameters;

	public Setup<int> SetupReturn() =>
		_return ??= new Setup<int>();

	public SetupWithParameter<string> SetupReturnWithOneParameter(in string parameter)
	{
		_returnWithOneParameter ??= new SetupWithParameter<string>();

		var hashCode = parameter.GetHashCode();
		_returnWithOneParameter.SetupParameters(hashCode);
		return _returnWithOneParameter;
	}

	public SetupWithMultipleParameters<decimal> SetupReturnWithMultipleParameters(in int parameter1, in int parameter2)
	{
		_returnWithMultipleParameters ??= new SetupWithMultipleParameters<decimal>();

		var hashCodes = new[]
		{
			parameter1.GetHashCode(),
			parameter2.GetHashCode(),
		};

		_returnWithMultipleParameters.SetupParameters(hashCodes);
		return _returnWithMultipleParameters;
	}

	protected override IPrimitiveDependencyService CreateObject() =>
		new Proxy(this);

	private sealed class Proxy : IPrimitiveDependencyService
	{
		private readonly SampleMock _mock;

		public Proxy(SampleMock mock)
		{
			_mock = mock;
		}

		public int Return() =>
			_mock._return?.Value ?? 0;

		public string ReturnWithParameter(in string parameter)
		{
			var hashcode = parameter.GetHashCode();
			return _mock._returnWithOneParameter?.GetValue(hashcode) ?? string.Empty;
		}

		public decimal ReturnWithMultipleParameters(int parameter1, int parameter2)
		{
			Span<int> hashCodes = stackalloc int[] { parameter1.GetHashCode(), parameter2.GetHashCode() };
			return _mock._returnWithMultipleParameters?.GetValue(hashCodes) ?? 0m;
		}
	}
}

[Obsolete("Will be generated")]
public static class SampleEx
{
	public static ISetupReturn<int> SetupReturn(this IMock<IPrimitiveDependencyService> @this) =>
		((SampleMock)@this).SetupReturn();

	public static ISetupReturn<string> SetupReturnWithOneParameter(this IMock<IPrimitiveDependencyService> @this, in string parameter) =>
		((SampleMock)@this).SetupReturnWithOneParameter(parameter);

	public static ISetupReturn<decimal> SetupReturnWithMultipleParameters(this IMock<IPrimitiveDependencyService> @this, in int parameter1, in int parameter2) =>
		((SampleMock)@this).SetupReturnWithMultipleParameters(parameter1, parameter2);
}
