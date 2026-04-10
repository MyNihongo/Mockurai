namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class NestedClasses : TestsBase
{
	[Fact]
	public async Task GenerateWithNestedDelegate()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

			public abstract class Container
			{
				public delegate Task ChangesHandler<T>(System.Collections.Generic.IReadOnlyCollection<T> changes, System.Threading.CancellationToken cancellationToken);

				public abstract void Execute<T>(string processorName, ChangesHandler<T> onChangesDelegate);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<Container> ContainerMock { get; }
			}
			""";

		GeneratedSources generatedSources = [];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
