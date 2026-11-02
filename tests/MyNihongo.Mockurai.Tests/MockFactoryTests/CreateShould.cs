namespace MyNihongo.Mockurai.Tests.MockFactoryTests;

public sealed class CreateShould : TestsBase
{
	[Fact]
	public async Task CreateMocksWithFactory()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				private readonly IMock<IInterface> _interfaceMock = Mock.Create<IInterface>();
			}
			""";

		GeneratedSources generatedSources = [];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}
}
