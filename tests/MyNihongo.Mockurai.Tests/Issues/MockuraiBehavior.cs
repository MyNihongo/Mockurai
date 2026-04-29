namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class MockuraiBehavior : IssuesTestsBase
{
	[Fact]
	public async Task ExcludeFromVerifyNoOtherCalls()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface1
			{
				void Invoke();
			}

			public interface IInterface2
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface1> Interface1Mock { get; }

				[MyNihongo.Mockurai.MockuraiBehavior(SkipVerifyNoOtherCalls = true)]
				protected partial IMock<IInterface2> Interface2Mock { get; }
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
					// Interface1Mock
					private readonly Interface1Mock _interface1Mock = new(InvocationIndex.CounterValue);
					protected partial IMock<Issues.Tests.IInterface1> Interface1Mock => _interface1Mock;

					// Interface2Mock
					private readonly Interface2Mock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial IMock<Issues.Tests.IInterface2> Interface2Mock => _interface2Mock;

					protected virtual void VerifyNoOtherCalls()
					{
						Interface1Mock.VerifyNoOtherCalls();
					}

					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface1Mock: Interface1Mock,
							interface2Mock: Interface2Mock
						);

						verify(ctx);
					}

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<Issues.Tests.IInterface1> Interface1Mock;
						public readonly IMockSequence<Issues.Tests.IInterface2> Interface2Mock;

						public VerifySequenceContext(IMock<Issues.Tests.IInterface1> interface1Mock, IMock<Issues.Tests.IInterface2> interface2Mock)
						{
							VerifyIndex = new VerifyIndex();
							Interface1Mock = new MockSequence<Issues.Tests.IInterface1>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface1Mock,
							};
							Interface2Mock = new MockSequence<Issues.Tests.IInterface2>
							{
								VerifyIndex = VerifyIndex,
								Mock = interface2Mock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							Interface1Mock = ctx.Interface1Mock;
							Interface2Mock = ctx.Interface2Mock;
						}
					}
				}
				"""
			),
			(
				"Interface1Mock.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class Interface1Mock : IMock<Issues.Tests.IInterface1>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public Interface1Mock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public Issues.Tests.IInterface1 Object => _proxy ??= new Proxy(this);

					public InvocationContainer Invocations => field ??= new InvocationContainer(this);

					// Invoke
					private Setup? _invoke0;
					private Invocation? _invoke0Invocation;

					public Setup SetupInvoke()
					{
						_invoke0 ??= new Setup();
						return _invoke0;
					}

					public void VerifyInvoke(in Times times)
					{
						_invoke0Invocation ??= new Invocation("IInterface1.Invoke()");
						_invoke0Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyInvoke(long index)
					{
						_invoke0Invocation ??= new Invocation("IInterface1.Invoke()");
						return _invoke0Invocation.Verify(index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
					}

					private sealed class Proxy : Issues.Tests.IInterface1
					{
						private readonly Interface1Mock _mock;

						public Proxy(Interface1Mock mock)
						{
							_mock = mock;
						}

						public void Invoke()
						{
							_mock._invoke0Invocation ??= new Invocation("IInterface1.Invoke()");
							_mock._invoke0Invocation.Register(_mock._invocationIndex);
							_mock._invoke0?.Invoke();
						}
					}

					public sealed class InvocationContainer
					{
						private readonly Interface1Mock _mock;

						public InvocationContainer(Interface1Mock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.IInterface1> @this)
					{
						public Interface1Mock.InvocationContainer Invocations => ((Interface1Mock)@this).Invocations;

						public void VerifyNoOtherCalls() =>
							((Interface1Mock)@this).VerifyNoOtherCalls();

						// Invoke
						public ISetup<System.Action> SetupInvoke() =>
							((Interface1Mock)@this).SetupInvoke();

						public void VerifyInvoke(in Times times) =>
							((Interface1Mock)@this).VerifyInvoke(times);

						public void VerifyInvoke(System.Func<Times> times) =>
							((Interface1Mock)@this).VerifyInvoke(times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface1> @this)
					{
						// Invoke
						public void Invoke()
						{
							var nextIndex = ((Interface1Mock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			(
				"Interface2Mock.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class Interface2Mock : IMock<Issues.Tests.IInterface2>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public Interface2Mock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public Issues.Tests.IInterface2 Object => _proxy ??= new Proxy(this);

					public InvocationContainer Invocations => field ??= new InvocationContainer(this);

					// Invoke
					private Setup? _invoke0;
					private Invocation? _invoke0Invocation;

					public Setup SetupInvoke()
					{
						_invoke0 ??= new Setup();
						return _invoke0;
					}

					public void VerifyInvoke(in Times times)
					{
						_invoke0Invocation ??= new Invocation("IInterface2.Invoke()");
						_invoke0Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyInvoke(long index)
					{
						_invoke0Invocation ??= new Invocation("IInterface2.Invoke()");
						return _invoke0Invocation.Verify(index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
					}

					private sealed class Proxy : Issues.Tests.IInterface2
					{
						private readonly Interface2Mock _mock;

						public Proxy(Interface2Mock mock)
						{
							_mock = mock;
						}

						public void Invoke()
						{
							_mock._invoke0Invocation ??= new Invocation("IInterface2.Invoke()");
							_mock._invoke0Invocation.Register(_mock._invocationIndex);
							_mock._invoke0?.Invoke();
						}
					}

					public sealed class InvocationContainer
					{
						private readonly Interface2Mock _mock;

						public InvocationContainer(Interface2Mock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.IInterface2> @this)
					{
						public Interface2Mock.InvocationContainer Invocations => ((Interface2Mock)@this).Invocations;

						public void VerifyNoOtherCalls() =>
							((Interface2Mock)@this).VerifyNoOtherCalls();

						// Invoke
						public ISetup<System.Action> SetupInvoke() =>
							((Interface2Mock)@this).SetupInvoke();

						public void VerifyInvoke(in Times times) =>
							((Interface2Mock)@this).VerifyInvoke(times);

						public void VerifyInvoke(System.Func<Times> times) =>
							((Interface2Mock)@this).VerifyInvoke(times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface2> @this)
					{
						// Invoke
						public void Invoke()
						{
							var nextIndex = ((Interface2Mock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
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
