namespace MyNihongo.Mock.Tests.Issues;

public sealed class NullableFields : TestsBase
{
	[Fact]
	public async Task GenerateNullableProperties()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mock.Tests;

			public interface IInterface
			{
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected IMock<IInterface>? InterfaceMock { get; }
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				namespace MyNihongo.Mock.Tests;

				public partial class TestsBase
				{

					protected void VerifyNoOtherCalls()
					{
						_interfaceMock?.VerifyNoOtherCalls();
					}

					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interfaceMock: _interfaceMock
						);

						verify(ctx);
					}

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface>? InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface>? interfaceMock)
						{
							if (interfaceMock is not null)
							{
								InterfaceMock = new MockSequence<MyNihongo.Mock.Tests.IInterface?>
								{
									VerifyIndex = _verifyIndex,
									Mock = interfaceMock,
								};
							}
						}
					}
				}
				"""
			),
			("InterfaceMock.g.cs", "aaa"),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact(Skip = "implement later")]
	public async Task GenerateNullableFields()
	{
		throw new NotImplementedException();
	}
}
