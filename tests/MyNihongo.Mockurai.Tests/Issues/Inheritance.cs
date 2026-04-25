namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class Inheritance : IssuesTestsBase
{
	[Fact]
	public async Task InheritFromOneInterface()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface1
			{
				void Invoke1();
			}

			public interface IInterface : IInterface1
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		GeneratedSources generatedSources =
		[
			GetTestsBaseSource(),
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

					// Invoke1
					private Setup? _invoke10;
					private Invocation? _invoke10Invocation;

					public Setup SetupInvoke1()
					{
						_invoke10 ??= new Setup();
						return _invoke10;
					}

					public void VerifyInvoke1(in Times times)
					{
						_invoke10Invocation ??= new Invocation("IInterface.Invoke1()");
						_invoke10Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyInvoke1(long index)
					{
						_invoke10Invocation ??= new Invocation("IInterface.Invoke1()");
						return _invoke10Invocation.Verify(index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_invoke10Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
						yield return _invoke10Invocation;
					}

					private sealed class Proxy : Issues.Tests.IInterface
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

						public void Invoke1()
						{
							_mock._invoke10Invocation ??= new Invocation("IInterface.Invoke1()");
							_mock._invoke10Invocation.Register(_mock._invocationIndex);
							_mock._invoke10?.Invoke();
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> Invoke1 => _mock._invoke10Invocation?.GetInvocations() ?? [];
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.IInterface> @this)
					{
						public InterfaceMock.InvocationContainer Invocations => ((InterfaceMock)@this).Invocations;

						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// Invoke
						public ISetup<System.Action> SetupInvoke() =>
							((InterfaceMock)@this).SetupInvoke();

						public void VerifyInvoke(in Times times) =>
							((InterfaceMock)@this).VerifyInvoke(times);

						public void VerifyInvoke(System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke(times());

						// Invoke1
						public ISetup<System.Action> SetupInvoke1() =>
							((InterfaceMock)@this).SetupInvoke1();

						public void VerifyInvoke1(in Times times) =>
							((InterfaceMock)@this).VerifyInvoke1(times);

						public void VerifyInvoke1(System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke1(times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke()
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// Invoke1
						public void Invoke1()
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke1(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			)
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
