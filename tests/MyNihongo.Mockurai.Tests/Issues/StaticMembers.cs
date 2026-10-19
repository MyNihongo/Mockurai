namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class StaticMembers : IssuesTestsBase
{
	[Fact]
	public async Task SkipStaticAbstractInterfaceMember()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterfaceBase
			{
				static abstract object CreateDefaultClientConfig();
			}

			public interface IInterface : IInterfaceBase
			{
				object CreateConfig();
				
				static object IInterfaceBase.CreateDefaultClientConfig()
				{
					return new object();
				}
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
				"""
				#nullable enable
				namespace Issues.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<Issues.Tests.IInterface> InterfaceMock => _interfaceMock;

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected virtual void VerifyNoOtherCalls()
					{
						InterfaceMock.VerifyNoOtherCalls();
					}

					[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interfaceMock: InterfaceMock
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
			(
				"InterfaceMock.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InterfaceMock : IMock<Issues.Tests.IInterface>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public InterfaceMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public Issues.Tests.IInterface Object => _proxy ??= new Proxy(this);

					public InvocationContainer Invocations => field ??= new InvocationContainer(this);

					// CreateConfig
					private Setup<object>? _createConfig0;
					private Invocation? _createConfig0Invocation;

					public Setup<object> SetupCreateConfig()
					{
						_createConfig0 ??= new Setup<object>();
						return _createConfig0;
					}

					public void VerifyCreateConfig(in Times times)
					{
						_createConfig0Invocation ??= new Invocation("IInterface.CreateConfig()");
						_createConfig0Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyCreateConfig(long index)
					{
						_createConfig0Invocation ??= new Invocation("IInterface.CreateConfig()");
						return _createConfig0Invocation.Verify(index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_createConfig0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _createConfig0Invocation;
					}

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public object CreateConfig()
						{
							_mock._createConfig0Invocation ??= new Invocation("IInterface.CreateConfig()");
							_mock._createConfig0Invocation.Register(_mock._invocationIndex);
							return _mock._createConfig0?.Execute(out var returnValue) == true ? returnValue! : default!;
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation> CreateConfig => _mock._createConfig0Invocation?.GetInvocations() ?? [];
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.IInterface> @this)
					{
						public InterfaceMock.InvocationContainer Invocations => ((InterfaceMock)@this).Invocations;

						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// CreateConfig
						public ISetup<System.Action, object, System.Func<object>> SetupCreateConfig() =>
							((InterfaceMock)@this).SetupCreateConfig();

						public void VerifyCreateConfig(in Times times) =>
							((InterfaceMock)@this).VerifyCreateConfig(times);

						public void VerifyCreateConfig(System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyCreateConfig(times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// CreateConfig
						public void CreateConfig()
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyCreateConfig(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}
}
