namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class InconsistentAccessibility : IssuesTestsBase
{
	[Fact]
	public async Task GenerateInternalVisibility()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			internal interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				internal partial IMock<IInterface> InterfaceMock { get; }
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
					internal partial IMock<Issues.Tests.IInterface> InterfaceMock => _interfaceMock;

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
						internal readonly IMockSequence<Issues.Tests.IInterface> InterfaceMock;

						internal VerifySequenceContext(IMock<Issues.Tests.IInterface> interfaceMock)
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

				internal sealed class InterfaceMock : IMock<Issues.Tests.IInterface>
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

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
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
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.IInterface> @this)
					{
						internal InterfaceMock.InvocationContainer Invocations => ((InterfaceMock)@this).Invocations;

						internal void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// Invoke
						internal ISetup<System.Action> SetupInvoke() =>
							((InterfaceMock)@this).SetupInvoke();

						internal void VerifyInvoke(in Times times) =>
							((InterfaceMock)@this).VerifyInvoke(times);

						internal void VerifyInvoke(System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke(times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// Invoke
						internal void Invoke()
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

	[Fact]
	public async Task GenerateInternalVisibilityNested()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			internal interface INestedInterface
			{
				void Invoke();
			}

			public interface IInterface<T>
			{
				void Invoke(T value);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				internal partial IMock<IInterface<INestedInterface>> InterfaceMock { get; }
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
					private readonly InterfaceMock<Issues.Tests.INestedInterface> _interfaceMock = new(InvocationIndex.CounterValue);
					internal partial IMock<Issues.Tests.IInterface<Issues.Tests.INestedInterface>> InterfaceMock => _interfaceMock;

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
						internal readonly IMockSequence<Issues.Tests.IInterface<Issues.Tests.INestedInterface>> InterfaceMock;

						internal VerifySequenceContext(IMock<Issues.Tests.IInterface<Issues.Tests.INestedInterface>> interfaceMock)
						{
							VerifyIndex = new VerifyIndex();
							InterfaceMock = new MockSequence<Issues.Tests.IInterface<Issues.Tests.INestedInterface>>
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
					private SetupWithParameter<T>? _invoke0;
					private Invocation<T>? _invoke0Invocation;

					public SetupWithParameter<T> SetupInvoke(in It<T> value)
					{
						_invoke0 ??= new SetupWithParameter<T>();
						_invoke0.SetupParameter(value.ValueSetup);
						return _invoke0;
					}

					public void VerifyInvoke(in It<T> value, in Times times)
					{
						_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
						_invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke(in It<T> value, long index)
					{
						_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
						return _invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
					}

					private sealed class Proxy : Issues.Tests.IInterface<T>
					{
						private readonly InterfaceMock<T> _mock;

						public Proxy(InterfaceMock<T> mock)
						{
							_mock = mock;
						}

						public void Invoke(T value)
						{
							_mock._invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
							_mock._invoke0Invocation.Register(_mock._invocationIndex, value);
							_mock._invoke0?.Invoke(value);
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock<T> _mock;

						public InvocationContainer(InterfaceMock<T> mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<T>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];
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
						public ISetup<System.Action<T>> SetupInvoke(in It<T> value = default) =>
							((InterfaceMock<T>)@this).SetupInvoke(value);

						public void VerifyInvoke(in It<T> value, in Times times) =>
							((InterfaceMock<T>)@this).VerifyInvoke(value, times);

						public void VerifyInvoke(in It<T> value, System.Func<Times> times) =>
							((InterfaceMock<T>)@this).VerifyInvoke(value, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension<T>(IMockSequence<Issues.Tests.IInterface<T>> @this)
					{
						// Invoke
						public void Invoke(in It<T> value)
						{
							var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(value, @this.VerifyIndex);
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
	public async Task GenerateInternalVisibilityNestedMultiple()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface INestedInterface1
			{
				void Invoke();
			}

			internal interface INestedInterface2
			{
				void Invoke();
			}

			public interface IInterface<T1, T2>
			{
				void Invoke(T1 value, T2 param);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				internal partial IMock<IInterface<INestedInterface1, INestedInterface2>> InterfaceMock { get; }
			}
			""";

		var types = new TypeModel[]
		{
			new("T1", 1, isGeneric: true),
			new("T2", 2, isGeneric: true),
		};

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
					private readonly InterfaceMock<Issues.Tests.INestedInterface1, Issues.Tests.INestedInterface2> _interfaceMock = new(InvocationIndex.CounterValue);
					internal partial IMock<Issues.Tests.IInterface<Issues.Tests.INestedInterface1, Issues.Tests.INestedInterface2>> InterfaceMock => _interfaceMock;

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
						internal readonly IMockSequence<Issues.Tests.IInterface<Issues.Tests.INestedInterface1, Issues.Tests.INestedInterface2>> InterfaceMock;

						internal VerifySequenceContext(IMock<Issues.Tests.IInterface<Issues.Tests.INestedInterface1, Issues.Tests.INestedInterface2>> interfaceMock)
						{
							VerifyIndex = new VerifyIndex();
							InterfaceMock = new MockSequence<Issues.Tests.IInterface<Issues.Tests.INestedInterface1, Issues.Tests.INestedInterface2>>
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
				"InterfaceMock_T1_T2_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InterfaceMock<T1, T2> : IMock<Issues.Tests.IInterface<T1, T2>>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public InterfaceMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public Issues.Tests.IInterface<T1, T2> Object => _proxy ??= new Proxy(this);

					public InvocationContainer Invocations => field ??= new InvocationContainer(this);

					// Invoke
					private SetupT1T2<T1, T2>? _invoke0;
					private InvocationT1T2<T1, T2>? _invoke0Invocation;

					public SetupT1T2<T1, T2> SetupInvoke(in It<T1> value, in It<T2> param)
					{
						_invoke0 ??= new SetupT1T2<T1, T2>();
						_invoke0.SetupParameters(value.ValueSetup, param.ValueSetup);
						return _invoke0;
					}

					public void VerifyInvoke(in It<T1> value, in It<T2> param, in Times times)
					{
						_invoke0Invocation ??= new InvocationT1T2<T1, T2>($"IInterface<{typeof(T1).Name}, {typeof(T2).Name}>.Invoke({{0}}, {{1}})");
						_invoke0Invocation.Verify(value.ValueSetup, param.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke(in It<T1> value, in It<T2> param, long index)
					{
						_invoke0Invocation ??= new InvocationT1T2<T1, T2>($"IInterface<{typeof(T1).Name}, {typeof(T2).Name}>.Invoke({{0}}, {{1}})");
						return _invoke0Invocation.Verify(value.ValueSetup, param.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
					}

					private sealed class Proxy : Issues.Tests.IInterface<T1, T2>
					{
						private readonly InterfaceMock<T1, T2> _mock;

						public Proxy(InterfaceMock<T1, T2> mock)
						{
							_mock = mock;
						}

						public void Invoke(T1 value, T2 param)
						{
							_mock._invoke0Invocation ??= new InvocationT1T2<T1, T2>($"IInterface<{typeof(T1).Name}, {typeof(T2).Name}>.Invoke({{0}}, {{1}})");
							_mock._invoke0Invocation.Register(_mock._invocationIndex, value, param);
							_mock._invoke0?.Invoke(value, param);
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock<T1, T2> _mock;

						public InvocationContainer(InterfaceMock<T1, T2> mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(T1 value, T2 param)>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];
					}
				}

				public static partial class MockExtensions
				{
					extension<T1, T2>(IMock<Issues.Tests.IInterface<T1, T2>> @this)
					{
						public InterfaceMock<T1, T2>.InvocationContainer Invocations => ((InterfaceMock<T1, T2>)@this).Invocations;

						public void VerifyNoOtherCalls() =>
							((InterfaceMock<T1, T2>)@this).VerifyNoOtherCalls();

						// Invoke
						public ISetup<SetupT1T2<T1, T2>.CallbackDelegate> SetupInvoke(in It<T1> value = default, in It<T2> param = default) =>
							((InterfaceMock<T1, T2>)@this).SetupInvoke(value, param);

						public void VerifyInvoke(in It<T1> value, in It<T2> param, in Times times) =>
							((InterfaceMock<T1, T2>)@this).VerifyInvoke(value, param, times);

						public void VerifyInvoke(in It<T1> value, in It<T2> param, System.Func<Times> times) =>
							((InterfaceMock<T1, T2>)@this).VerifyInvoke(value, param, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension<T1, T2>(IMockSequence<Issues.Tests.IInterface<T1, T2>> @this)
					{
						// Invoke
						public void Invoke(in It<T1> value, in It<T2> param)
						{
							var nextIndex = ((InterfaceMock<T1, T2>)@this.Mock).VerifyInvoke(value, param, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			CreateSetupCode(types),
			CreateInvocationCode(types),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceProtectedGetters()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface
			{
				int Property { protected get; set; }
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

					// Property
					private Setup<int>? _property0Get;
					private Invocation? _property0GetInvocation;
					private SetupWithParameter<int>? _property0Set;
					private Invocation<int>? _property0SetInvocation;

					public Setup<int> SetupGetProperty()
					{
						_property0Get ??= new Setup<int>();
						return _property0Get;
					}

					public void VerifyGetProperty(in Times times)
					{
						_property0GetInvocation ??= new Invocation("IInterface.Property.get");
						_property0GetInvocation.Verify(times, _invocationProviders);
					}

					public long VerifyGetProperty(long index)
					{
						_property0GetInvocation ??= new Invocation("IInterface.Property.get");
						return _property0GetInvocation.Verify(index, _invocationProviders);
					}

					public SetupWithParameter<int> SetupSetProperty(in It<int> value)
					{
						_property0Set ??= new SetupWithParameter<int>();
						_property0Set.SetupParameter(value.ValueSetup);
						return _property0Set;
					}

					public void VerifySetProperty(in It<int> value, in Times times)
					{
						_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
						_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
					}

					public long VerifySetProperty(in It<int> value, long index)
					{
						_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
						return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_property0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_property0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _property0GetInvocation;
						yield return _property0SetInvocation;
					}

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public int Property
						{
							get
							{
								_mock._property0GetInvocation ??= new Invocation("IInterface.Property.get");
								_mock._property0GetInvocation.Register(_mock._invocationIndex);
								return _mock._property0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
							}
							set
							{
								_mock._property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
								_mock._property0SetInvocation.Register(_mock._invocationIndex, value);
								_mock._property0Set?.Invoke(value);
							}
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation> PropertyGet => _mock._property0GetInvocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<int>> PropertySet => _mock._property0SetInvocation?.GetInvocationsWithArguments() ?? [];
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.IInterface> @this)
					{
						public InterfaceMock.InvocationContainer Invocations => ((InterfaceMock)@this).Invocations;

						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// Property
						public ISetup<System.Action, int, System.Func<int>> SetupGetProperty() =>
							((InterfaceMock)@this).SetupGetProperty();

						public void VerifyGetProperty(in Times times) =>
							((InterfaceMock)@this).VerifyGetProperty(times);

						public void VerifyGetProperty(System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyGetProperty(times());

						public ISetup<System.Action<int>> SetupSetProperty(in It<int> value = default) =>
							((InterfaceMock)@this).SetupSetProperty(value);

						public void VerifySetProperty(in It<int> value, in Times times) =>
							((InterfaceMock)@this).VerifySetProperty(value, times);

						public void VerifySetProperty(in It<int> value, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifySetProperty(value, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// Property
						public void GetProperty()
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyGetProperty(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						public void SetProperty(in It<int> value)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
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
	public async Task GenerateInterfaceProtectedSetters()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface
			{
				int Property { get; protected set; }
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

					// Property
					private Setup<int>? _property0Get;
					private Invocation? _property0GetInvocation;
					private SetupWithParameter<int>? _property0Set;
					private Invocation<int>? _property0SetInvocation;

					public Setup<int> SetupGetProperty()
					{
						_property0Get ??= new Setup<int>();
						return _property0Get;
					}

					public void VerifyGetProperty(in Times times)
					{
						_property0GetInvocation ??= new Invocation("IInterface.Property.get");
						_property0GetInvocation.Verify(times, _invocationProviders);
					}

					public long VerifyGetProperty(long index)
					{
						_property0GetInvocation ??= new Invocation("IInterface.Property.get");
						return _property0GetInvocation.Verify(index, _invocationProviders);
					}

					public SetupWithParameter<int> SetupSetProperty(in It<int> value)
					{
						_property0Set ??= new SetupWithParameter<int>();
						_property0Set.SetupParameter(value.ValueSetup);
						return _property0Set;
					}

					public void VerifySetProperty(in It<int> value, in Times times)
					{
						_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
						_property0SetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
					}

					public long VerifySetProperty(in It<int> value, long index)
					{
						_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
						return _property0SetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_property0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_property0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _property0GetInvocation;
						yield return _property0SetInvocation;
					}

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public int Property
						{
							get
							{
								_mock._property0GetInvocation ??= new Invocation("IInterface.Property.get");
								_mock._property0GetInvocation.Register(_mock._invocationIndex);
								return _mock._property0Get?.Execute(out var returnValue) == true ? returnValue! : default!;
							}
							set
							{
								_mock._property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
								_mock._property0SetInvocation.Register(_mock._invocationIndex, value);
								_mock._property0Set?.Invoke(value);
							}
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation> PropertyGet => _mock._property0GetInvocation?.GetInvocations() ?? [];

						public System.Collections.Generic.IEnumerable<IInvocation<int>> PropertySet => _mock._property0SetInvocation?.GetInvocationsWithArguments() ?? [];
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.IInterface> @this)
					{
						public InterfaceMock.InvocationContainer Invocations => ((InterfaceMock)@this).Invocations;

						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// Property
						public ISetup<System.Action, int, System.Func<int>> SetupGetProperty() =>
							((InterfaceMock)@this).SetupGetProperty();

						public void VerifyGetProperty(in Times times) =>
							((InterfaceMock)@this).VerifyGetProperty(times);

						public void VerifyGetProperty(System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyGetProperty(times());

						public ISetup<System.Action<int>> SetupSetProperty(in It<int> value = default) =>
							((InterfaceMock)@this).SetupSetProperty(value);

						public void VerifySetProperty(in It<int> value, in Times times) =>
							((InterfaceMock)@this).VerifySetProperty(value, times);

						public void VerifySetProperty(in It<int> value, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifySetProperty(value, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// Property
						public void GetProperty()
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyGetProperty(@this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						public void SetProperty(in It<int> value)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifySetProperty(value, @this.VerifyIndex);
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
