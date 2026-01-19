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
						InterfaceMock?.VerifyNoOtherCalls();
					}

					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interfaceMock: InterfaceMock
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
								InterfaceMock = new MockSequence<MyNihongo.Mock.Tests.IInterface>
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
			(
				"InterfaceMock.g.cs",
				"""
				namespace MyNihongo.Mock;

				public sealed class InterfaceMock : IMock<MyNihongo.Mock.Tests.IInterface>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public InterfaceMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public MyNihongo.Mock.Tests.IInterface Object => _proxy ??= new Proxy(this);



					public void VerifyNoOtherCalls()
					{

					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield break;
					}

					private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}


					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<MyNihongo.Mock.Tests.IInterface> @this)
					{
						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<MyNihongo.Mock.Tests.IInterface> @this)
					{
					
					}
				}
				"""
			),
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
