namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class Nullability : TestsBase
{
	[Fact]
	public async Task GenerateNullableProperties()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

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
				#nullable enable
				namespace MyNihongo.Mockurai.Tests;

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
						public readonly IMockSequence<MyNihongo.Mockurai.Tests.IInterface>? InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface>? interfaceMock)
						{
							if (interfaceMock is not null)
							{
								InterfaceMock = new MockSequence<MyNihongo.Mockurai.Tests.IInterface>
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
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InterfaceMock : IMock<MyNihongo.Mockurai.Tests.IInterface>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public InterfaceMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public MyNihongo.Mockurai.Tests.IInterface Object => _proxy ??= new Proxy(this);



					public void VerifyNoOtherCalls()
					{

					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield break;
					}

					private sealed class Proxy : MyNihongo.Mockurai.Tests.IInterface
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
					extension(IMock<MyNihongo.Mockurai.Tests.IInterface> @this)
					{
						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();


					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<MyNihongo.Mockurai.Tests.IInterface> @this)
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

	[Fact]
	public async Task GenerateDifferentForNullability()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

			public interface IInterface
			{
				void Invoke(string param1, float param2);
			}

			public abstract class Class
			{
				public abstract void Invoke(string? param1, float param2);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
				protected partial IMock<Class> ClassMock { get; }
			}
			""";

		GeneratedSources generatedSources = [];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
