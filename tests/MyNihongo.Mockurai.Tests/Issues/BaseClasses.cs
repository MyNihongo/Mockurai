namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class BaseClasses : TestsBase
{
	[Fact]
	public async Task InvokeBaseClassMethods()
	{
		const string testCode =
			"""
			namespace MyNihongo.Indexer.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> Interface1Mock { get; }
			}

			[MockuraiGenerate]
			public abstract partial class TestsDerivedBase
			{
				protected partial IMock<IInterface> Interface2Mock { get; }
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Indexer.Tests;

				public partial class TestsBase
				{
					// Interface1Mock
					private readonly InterfaceMock _interface1Mock = new(InvocationIndex.CounterValue);
					protected partial IMock<MyNihongo.Indexer.Tests.IInterface> Interface1Mock => _interface1Mock;

					protected virtual void VerifyNoOtherCalls()
					{
						Interface1Mock.VerifyNoOtherCalls();
					}

					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface1Mock: Interface1Mock
						);

						verify(ctx);
					}

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Indexer.Tests.IInterface> Interface1Mock;

						public VerifySequenceContext(IMock<MyNihongo.Indexer.Tests.IInterface> interface1Mock)
						{
							Interface1Mock = new MockSequence<MyNihongo.Indexer.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interface1Mock,
							};
						}
					}
				}
				"""
			),
			(
				"TestsDerivedBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Indexer.Tests;

				public partial class TestsDerivedBase
				{
					// Interface2Mock
					private readonly InterfaceMock _interface2Mock = new(InvocationIndex.CounterValue);
					protected partial IMock<MyNihongo.Indexer.Tests.IInterface> Interface2Mock => _interface2Mock;

					protected virtual void VerifyNoOtherCalls()
					{
						base.VerifyNoOtherCalls();
						Interface2Mock.VerifyNoOtherCalls();
					}

					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interface2Mock: Interface2Mock
						);

						verify(ctx);
					}

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Indexer.Tests.IInterface> Interface2Mock;

						public VerifySequenceContext(IMock<MyNihongo.Indexer.Tests.IInterface> interface2Mock)
						{
							Interface2Mock = new MockSequence<MyNihongo.Indexer.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interface2Mock,
							};
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

				public sealed class InterfaceMock : IMock<MyNihongo.Indexer.Tests.IInterface>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public InterfaceMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public MyNihongo.Indexer.Tests.IInterface Object => _proxy ??= new Proxy(this);

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
						_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
						_invoke0Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyInvoke(long index)
					{
						_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
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

					private sealed class Proxy : MyNihongo.Indexer.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public void Invoke()
						{
							_mock._invoke0Invocation ??= new Invocation("IInterface.Invoke()");
							_mock._invoke0Invocation.Register(_mock._invocationIndex);
							_mock._invoke0?.Invoke();
						}
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<MyNihongo.Indexer.Tests.IInterface> @this)
					{
						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// Invoke
						public ISetup<System.Action> SetupInvoke() =>
							((InterfaceMock)@this).SetupInvoke();

						public void VerifyInvoke(in Times times) =>
							((InterfaceMock)@this).VerifyInvoke(times);

						public void VerifyInvoke(System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke(times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<MyNihongo.Indexer.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke()
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
