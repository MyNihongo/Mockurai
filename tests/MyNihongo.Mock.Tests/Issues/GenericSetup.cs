namespace MyNihongo.Mock.Tests.Issues;

public sealed class GenericSetup : TestsBase
{
	[Fact]
	public async Task AdjustGenericTypeNameForTReturns()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mock.Tests;

			public interface IInterface
			{
				TReturns Invoke<TReturns>(int param1, TReturns returnValue);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				""
			),
			(
				"InterfaceMock.g.cs",
				""
			),
			(
				"SetupInt32TReturns_TReturns_.g.cs",
				""
			),
			(
				"InvocationInt32TReturns.g.cs",
				""
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
