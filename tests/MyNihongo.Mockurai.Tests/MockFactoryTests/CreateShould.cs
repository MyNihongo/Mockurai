namespace MyNihongo.Mockurai.Tests.MockFactoryTests;

public sealed class CreateShould : MockFactoryTestsBase
{
	[Fact]
	public async Task GenerateMockWithPrivateField()
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

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace Issues.Tests;

				public partial class TestsBase
				{
					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls()
					{
						_interfaceMock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interfaceMock: _interfaceMock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<Issues.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<Issues.Tests.IInterface> interfaceMock)
						{
							VerifyIndex = new VerifyIndex();
							InterfaceMock = new MockSequence<Issues.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interfaceMock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							InterfaceMock = ctx.InterfaceMock;
						}
					}
				}
				"""
			),
			InterfaceMock,
			CreateInterfaceMockInterceptors(testCode),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Theory]
	[InlineData("InterfaceMock")]
	[InlineData("interfaceMock")]
	public async Task GenerateMockWithProtectedField(string fieldName)
	{
		var testCode =
			$$"""
			  namespace Issues.Tests;

			  public interface IInterface
			  {
			  	void Invoke();
			  }

			  [MockuraiGenerate]
			  public abstract partial class TestsBase
			  {
			  	protected readonly IMock<IInterface> {{fieldName}} = Mock.Create<IInterface>();
			  }
			  """;

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				$$"""
				  #nullable enable
				  namespace Issues.Tests;

				  public partial class TestsBase
				  {
				  	[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
				  	protected virtual void VerifyNoOtherCalls()
				  	{
				  		{{fieldName}}.VerifyNoOtherCalls();
				  	}

				  	[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
				  	protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
				  	{
				  		var ctx = new VerifySequenceContext(
				  			interfaceMock: {{fieldName}}
				  		);

				  		verify(ctx);
				  	}

				  	protected class VerifySequenceContext
				  	{
				  		protected readonly VerifyIndex VerifyIndex;
				  		public readonly IMockSequence<Issues.Tests.IInterface> InterfaceMock;

				  		public VerifySequenceContext(MyNihongo.Mockurai.IMock<Issues.Tests.IInterface> interfaceMock)
				  		{
				  			VerifyIndex = new VerifyIndex();
				  			InterfaceMock = new MockSequence<Issues.Tests.IInterface>
				  			{
				  				VerifyIndex = VerifyIndex,
				  				Mock = interfaceMock,
				  			};
				  		}

				  		protected VerifySequenceContext(VerifySequenceContext ctx)
				  		{
				  			VerifyIndex = ctx.VerifyIndex;
				  			InterfaceMock = ctx.InterfaceMock;
				  		}
				  	}
				  }
				  """
			),
			InterfaceMock,
			CreateInterfaceMockInterceptors(testCode),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}
}
