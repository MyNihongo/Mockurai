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
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task InheritFromMultipleInterfaces()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface1
			{
				void Invoke1();
			}

			public interface IInterface2
			{
				void Invoke2();
			}

			public interface IInterface : IInterface1, IInterface2
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

					// Invoke2
					private Setup? _invoke20;
					private Invocation? _invoke20Invocation;

					public Setup SetupInvoke2()
					{
						_invoke20 ??= new Setup();
						return _invoke20;
					}

					public void VerifyInvoke2(in Times times)
					{
						_invoke20Invocation ??= new Invocation("IInterface.Invoke2()");
						_invoke20Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyInvoke2(long index)
					{
						_invoke20Invocation ??= new Invocation("IInterface.Invoke2()");
						return _invoke20Invocation.Verify(index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_invoke10Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_invoke20Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
						yield return _invoke10Invocation;
						yield return _invoke20Invocation;
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

						public void Invoke2()
						{
							_mock._invoke20Invocation ??= new Invocation("IInterface.Invoke2()");
							_mock._invoke20Invocation.Register(_mock._invocationIndex);
							_mock._invoke20?.Invoke();
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

						public System.Collections.Generic.IEnumerable<IInvocation> Invoke2 => _mock._invoke20Invocation?.GetInvocations() ?? [];
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

						// Invoke2
						public ISetup<System.Action> SetupInvoke2() =>
							((InterfaceMock)@this).SetupInvoke2();

						public void VerifyInvoke2(in Times times) =>
							((InterfaceMock)@this).VerifyInvoke2(times);

						public void VerifyInvoke2(System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke2(times());
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

						// Invoke2
						public void Invoke2()
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke2(@this.VerifyIndex);
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

	[Fact]
	public async Task InheritFromInterfacesRecursively()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface1
			{
				void Invoke1();
			}

			public interface IInterface2 : IInterface1
			{
				void Invoke2();
			}

			public interface IInterface : IInterface2
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

					// Invoke2
					private Setup? _invoke20;
					private Invocation? _invoke20Invocation;

					public Setup SetupInvoke2()
					{
						_invoke20 ??= new Setup();
						return _invoke20;
					}

					public void VerifyInvoke2(in Times times)
					{
						_invoke20Invocation ??= new Invocation("IInterface.Invoke2()");
						_invoke20Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyInvoke2(long index)
					{
						_invoke20Invocation ??= new Invocation("IInterface.Invoke2()");
						return _invoke20Invocation.Verify(index, _invocationProviders);
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
						_invoke20Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_invoke10Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
						yield return _invoke20Invocation;
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

						public void Invoke2()
						{
							_mock._invoke20Invocation ??= new Invocation("IInterface.Invoke2()");
							_mock._invoke20Invocation.Register(_mock._invocationIndex);
							_mock._invoke20?.Invoke();
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

						public System.Collections.Generic.IEnumerable<IInvocation> Invoke2 => _mock._invoke20Invocation?.GetInvocations() ?? [];

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

						// Invoke2
						public ISetup<System.Action> SetupInvoke2() =>
							((InterfaceMock)@this).SetupInvoke2();

						public void VerifyInvoke2(in Times times) =>
							((InterfaceMock)@this).VerifyInvoke2(times);

						public void VerifyInvoke2(System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke2(times());

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

						// Invoke2
						public void Invoke2()
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke2(@this.VerifyIndex);
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
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task InheritFromInterfaceGeneric()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface1<T>
			{
				void Invoke1(T value);
			}

			public interface IInterface<T> : IInterface1<T>
			{
				T Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface<decimal>> InterfaceMock { get; }
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
					private readonly InterfaceMock<decimal> _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial IMock<Issues.Tests.IInterface<decimal>> InterfaceMock => _interfaceMock;

					protected virtual void VerifyNoOtherCalls()
					{
						InterfaceMock.VerifyNoOtherCalls();
					}

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
						public readonly IMockSequence<Issues.Tests.IInterface<decimal>> InterfaceMock;

						public VerifySequenceContext(IMock<Issues.Tests.IInterface<decimal>> interfaceMock)
						{
							VerifyIndex = new VerifyIndex();
							InterfaceMock = new MockSequence<Issues.Tests.IInterface<decimal>>
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
				"InterfaceMock_T_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InterfaceMock<T> : IMock<Issues.Tests.IInterface<T>>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public InterfaceMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public Issues.Tests.IInterface<T> Object => _proxy ??= new Proxy(this);

					public InvocationContainer Invocations => field ??= new InvocationContainer(this);

					// Invoke
					private Setup<T>? _invoke0;
					private Invocation? _invoke0Invocation;

					public Setup<T> SetupInvoke()
					{
						_invoke0 ??= new Setup<T>();
						return _invoke0;
					}

					public void VerifyInvoke(in Times times)
					{
						_invoke0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Invoke()");
						_invoke0Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyInvoke(long index)
					{
						_invoke0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Invoke()");
						return _invoke0Invocation.Verify(index, _invocationProviders);
					}

					// Invoke1
					private SetupWithParameter<T>? _invoke10;
					private Invocation<T>? _invoke10Invocation;

					public SetupWithParameter<T> SetupInvoke1(in It<T> value)
					{
						_invoke10 ??= new SetupWithParameter<T>();
						_invoke10.SetupParameter(value.ValueSetup);
						return _invoke10;
					}

					public void VerifyInvoke1(in It<T> value, in Times times)
					{
						_invoke10Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke1({{0}})");
						_invoke10Invocation.Verify(value.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke1(in It<T> value, long index)
					{
						_invoke10Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke1({{0}})");
						return _invoke10Invocation.Verify(value.ValueSetup, index, _invocationProviders);
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

					private sealed class Proxy : Issues.Tests.IInterface<T>
					{
						private readonly InterfaceMock<T> _mock;

						public Proxy(InterfaceMock<T> mock)
						{
							_mock = mock;
						}

						public T Invoke()
						{
							_mock._invoke0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Invoke()");
							_mock._invoke0Invocation.Register(_mock._invocationIndex);
							return _mock._invoke0?.Execute(out var returnValue) == true ? returnValue! : default!;
						}

						public void Invoke1(T value)
						{
							_mock._invoke10Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke1({{0}})");
							_mock._invoke10Invocation.Register(_mock._invocationIndex, value);
							_mock._invoke10?.Invoke(value);
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock<T> _mock;

						public InvocationContainer(InterfaceMock<T> mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<T>> Invoke1 => _mock._invoke10Invocation?.GetInvocationsWithArguments() ?? [];
					}
				}

				public static partial class MockExtensions
				{
					extension<T>(IMock<Issues.Tests.IInterface<T>> @this)
					{
						public InterfaceMock<T>.InvocationContainer Invocations => ((InterfaceMock<T>)@this).Invocations;

						public void VerifyNoOtherCalls() =>
							((InterfaceMock<T>)@this).VerifyNoOtherCalls();

						// Invoke
						public ISetup<System.Action, T, System.Func<T>> SetupInvoke() =>
							((InterfaceMock<T>)@this).SetupInvoke();

						public void VerifyInvoke(in Times times) =>
							((InterfaceMock<T>)@this).VerifyInvoke(times);

						public void VerifyInvoke(System.Func<Times> times) =>
							((InterfaceMock<T>)@this).VerifyInvoke(times());

						// Invoke1
						public ISetup<System.Action<T>> SetupInvoke1(in It<T> value = default) =>
							((InterfaceMock<T>)@this).SetupInvoke1(value);

						public void VerifyInvoke1(in It<T> value, in Times times) =>
							((InterfaceMock<T>)@this).VerifyInvoke1(value, times);

						public void VerifyInvoke1(in It<T> value, System.Func<Times> times) =>
							((InterfaceMock<T>)@this).VerifyInvoke1(value, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension<T>(IMockSequence<Issues.Tests.IInterface<T>> @this)
					{
						// Invoke
						public void Invoke()
						{
							var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// Invoke1
						public void Invoke1(in It<T> value)
						{
							var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke1(value, @this.VerifyIndex);
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

	[Fact]
	public async Task InheritFromInterfaceGenericType()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface1<T>
			{
				void Invoke1(T value);

				T Return();
			}

			public interface IInterface : IInterface1<decimal>
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
					private SetupWithParameter<decimal>? _invoke10;
					private Invocation<decimal>? _invoke10Invocation;

					public SetupWithParameter<decimal> SetupInvoke1(in It<decimal> value)
					{
						_invoke10 ??= new SetupWithParameter<decimal>();
						_invoke10.SetupParameter(value.ValueSetup);
						return _invoke10;
					}

					public void VerifyInvoke1(in It<decimal> value, in Times times)
					{
						_invoke10Invocation ??= new Invocation<decimal>("IInterface.Invoke1({0})");
						_invoke10Invocation.Verify(value.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke1(in It<decimal> value, long index)
					{
						_invoke10Invocation ??= new Invocation<decimal>("IInterface.Invoke1({0})");
						return _invoke10Invocation.Verify(value.ValueSetup, index, _invocationProviders);
					}

					// Return
					private Setup<decimal>? _return0;
					private Invocation? _return0Invocation;

					public Setup<decimal> SetupReturn()
					{
						_return0 ??= new Setup<decimal>();
						return _return0;
					}

					public void VerifyReturn(in Times times)
					{
						_return0Invocation ??= new Invocation("IInterface.Return()");
						_return0Invocation.Verify(times, _invocationProviders);
					}

					public long VerifyReturn(long index)
					{
						_return0Invocation ??= new Invocation("IInterface.Return()");
						return _return0Invocation.Verify(index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_invoke10Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_return0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
						yield return _invoke10Invocation;
						yield return _return0Invocation;
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

						public void Invoke1(decimal value)
						{
							_mock._invoke10Invocation ??= new Invocation<decimal>("IInterface.Invoke1({0})");
							_mock._invoke10Invocation.Register(_mock._invocationIndex, value);
							_mock._invoke10?.Invoke(value);
						}

						public decimal Return()
						{
							_mock._return0Invocation ??= new Invocation("IInterface.Return()");
							_mock._return0Invocation.Register(_mock._invocationIndex);
							return _mock._return0?.Execute(out var returnValue) == true ? returnValue! : default!;
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

						public System.Collections.Generic.IEnumerable<IInvocation<decimal>> Invoke1 => _mock._invoke10Invocation?.GetInvocationsWithArguments() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation> Return => _mock._return0Invocation?.GetInvocations() ?? [];
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
						public ISetup<System.Action<decimal>> SetupInvoke1(in It<decimal> value = default) =>
							((InterfaceMock)@this).SetupInvoke1(value);

						public void VerifyInvoke1(in It<decimal> value, in Times times) =>
							((InterfaceMock)@this).VerifyInvoke1(value, times);

						public void VerifyInvoke1(in It<decimal> value, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke1(value, times());

						// Return
						public ISetup<System.Action, decimal, System.Func<decimal>> SetupReturn() =>
							((InterfaceMock)@this).SetupReturn();

						public void VerifyReturn(in Times times) =>
							((InterfaceMock)@this).VerifyReturn(times);

						public void VerifyReturn(System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyReturn(times());
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
						public void Invoke1(in It<decimal> value)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke1(value, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						// Return
						public void Return()
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyReturn(@this.VerifyIndex);
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

	[Fact]
	public async Task InheritFromClass()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public abstract Class1
			{
				public abstract void Invoke1();
			}

			public abstract Class : Class1
			{
				public abstract void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<Class> ClassMock { get; }
			}
			""";

		GeneratedSources generatedSources = [];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task InheritFromClassRecursively()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public abstract Class1
			{
				public abstract void Invoke1();
			}

			public abstract Class2 : Class1
			{
				public abstract void Invoke1();
			}

			public abstract Class : Class2
			{
				public abstract void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<Class> ClassMock { get; }
			}
			""";

		GeneratedSources generatedSources = [];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task InheritFromClassGeneric()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public abstract Class1<T>
			{
				public abstract void Invoke1(T value);
			}

			public abstract Class<T> : Class1<T>
			{
				public abstract T Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<Class<float>> ClassMock { get; }
			}
			""";

		GeneratedSources generatedSources = [];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task InheritFromClassGenericType()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public abstract Class1<T>
			{
				void Invoke1(T value);
			}

			public abstract Class : Class1<float>
			{
				T Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<Class> ClassMock { get; }
			}
			""";

		GeneratedSources generatedSources = [];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task NotDuplicateOverridenMethod()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public abstract Class1
			{
				public virtual void Invoke() {}
			}

			public abstract Class : Class1
			{
				public override void Invoke() {}
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<Class> ClassMock { get; }
			}
			""";

		GeneratedSources generatedSources = [];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task NotDuplicateOverridenMethodGeneric()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public abstract Class1<T>
			{
				public virtual void Invoke(T value) {}
			}

			public abstract Class<T> : Class1<T>
			{
				public override void Invoke(T value) {}
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<Class<float>> ClassMock { get; }
			}
			""";

		GeneratedSources generatedSources = [];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task NotDuplicateOverridenMethodGenericType()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public abstract Class1<T>
			{
				public virtual void Invoke(T value) {}
			}

			public abstract Class : Class1<float>
			{
				public override void Invoke(float value) {}
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<Class> ClassMock { get; }
			}
			""";

		GeneratedSources generatedSources = [];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
