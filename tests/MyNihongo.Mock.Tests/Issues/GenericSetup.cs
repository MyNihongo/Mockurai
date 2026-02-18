namespace MyNihongo.Mock.Tests.Issues;

public sealed class GenericSetup
{
	[Fact]
	public async Task AdjustGenericTypeNameForTReturns()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mock.Tests;

			public interface IInterface
			{
				TReturns Invoke<TReturns>(int param1, long returnValue);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		throw new NotImplementedException();
	}
}
