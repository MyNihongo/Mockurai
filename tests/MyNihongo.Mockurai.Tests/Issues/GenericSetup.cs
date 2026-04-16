namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class GenericSetup : TestsBase
{
	[Fact]
	public async Task AdjustGenericTypeNameForTReturns()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

			public interface IInterface
			{
				TReturns Invoke<TReturns>(int param1, TReturns returnValue);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		var types = new TypeModel[]
		{
			new("Int32", 1),
			new("T1", 2, isGeneric: true),
		};

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock => _interfaceMock;

					protected void VerifyNoOtherCalls()
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

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> interfaceMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mockurai.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock,
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

					// Invoke
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupInt32T1<TReturns, TReturns> SetupInvoke<TReturns>(in It<int> param1, in It<TReturns> returnValue)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupInt32T1<TReturns, TReturns>)_invoke0.GetOrAdd(typeof(TReturns), static _ => new SetupInt32T1<TReturns, TReturns>());
						invoke0.SetupParameters(param1.ValueSetup, returnValue.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<TReturns>(in It<int> param1, in It<TReturns> returnValue, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationInt32T1<TReturns>)_invoke0Invocation.GetOrAdd(typeof(TReturns), static key => new InvocationInt32T1<TReturns>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
						invoke0Invocation.Verify(param1.ValueSetup, returnValue.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<TReturns>(in It<int> param1, in It<TReturns> returnValue, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationInt32T1<TReturns>)_invoke0Invocation.GetOrAdd(typeof(TReturns), static key => new InvocationInt32T1<TReturns>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
						return invoke0Invocation.Verify(param1.ValueSetup, returnValue.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
					}

					private sealed class Proxy : MyNihongo.Mockurai.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public TReturns Invoke<TReturns>(int param1, TReturns returnValue)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationInt32T1<TReturns>)_mock._invoke0Invocation.GetOrAdd(typeof(TReturns), static key => new InvocationInt32T1<TReturns>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, returnValue);
							return ((SetupInt32T1<TReturns, TReturns>?)_mock._invoke0?.ValueOrDefault(typeof(TReturns)))?.Execute(param1, returnValue, out var _returnValue) == true ? _returnValue! : default!;
						}
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<MyNihongo.Mockurai.Tests.IInterface> @this)
					{
						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// Invoke
						public ISetup<SetupInt32T1<TReturns, TReturns>.CallbackDelegate, TReturns, SetupInt32T1<TReturns, TReturns>.ReturnsCallbackDelegate> SetupInvoke<TReturns>(in It<int> param1 = default, in It<TReturns> returnValue = default) =>
							((InterfaceMock)@this).SetupInvoke<TReturns>(param1, returnValue);

						public void VerifyInvoke<TReturns>(in It<int> param1, in It<TReturns> returnValue, in Times times) =>
							((InterfaceMock)@this).VerifyInvoke<TReturns>(param1, returnValue, times);

						public void VerifyInvoke<TReturns>(in It<int> param1, in It<TReturns> returnValue, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke<TReturns>(param1, returnValue, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<MyNihongo.Mockurai.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke<TReturns>(in It<int> param1, in It<TReturns> returnValue)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke<TReturns>(param1, returnValue, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			CreateSetupReturnsCode(types),
			CreateInvocationCode(types),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task NestedGenericParameters()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

			public interface IInterface
			{
				decimal Invoke<T>(System.Collections.Generic.IReadOnlyList<T> param1, int param2);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		var types = new TypeModel[]
		{
			new("IReadOnlyList", [("T1", true)], 1, @namespace: "System.Collections.Generic"),
			new("Int32", 2),
		};

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock => _interfaceMock;

					protected void VerifyNoOtherCalls()
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

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> interfaceMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mockurai.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock,
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

					// Invoke
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupIReadOnlyListT1Int32<T, decimal> SetupInvoke<T>(in It<System.Collections.Generic.IReadOnlyList<T>> param1, in It<int> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupIReadOnlyListT1Int32<T, decimal>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupIReadOnlyListT1Int32<T, decimal>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<T>(in It<System.Collections.Generic.IReadOnlyList<T>> param1, in It<int> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationIReadOnlyListT1Int32<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIReadOnlyListT1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<T>(in It<System.Collections.Generic.IReadOnlyList<T>> param1, in It<int> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationIReadOnlyListT1Int32<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIReadOnlyListT1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
						return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
					}

					private sealed class Proxy : MyNihongo.Mockurai.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public decimal Invoke<T>(System.Collections.Generic.IReadOnlyList<T> param1, int param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationIReadOnlyListT1Int32<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIReadOnlyListT1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							return ((SetupIReadOnlyListT1Int32<T, decimal>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
						}
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<MyNihongo.Mockurai.Tests.IInterface> @this)
					{
						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// Invoke
						public ISetup<SetupIReadOnlyListT1Int32<T, decimal>.CallbackDelegate, decimal, SetupIReadOnlyListT1Int32<T, decimal>.ReturnsCallbackDelegate> SetupInvoke<T>(in It<System.Collections.Generic.IReadOnlyList<T>> param1 = default, in It<int> param2 = default) =>
							((InterfaceMock)@this).SetupInvoke<T>(param1, param2);

						public void VerifyInvoke<T>(in It<System.Collections.Generic.IReadOnlyList<T>> param1, in It<int> param2, in Times times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times);

						public void VerifyInvoke<T>(in It<System.Collections.Generic.IReadOnlyList<T>> param1, in It<int> param2, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<MyNihongo.Mockurai.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke<T>(in It<System.Collections.Generic.IReadOnlyList<T>> param1, in It<int> param2)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke<T>(param1, param2, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			CreateSetupReturnsCode(types),
			CreateInvocationCode(types),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task NestedGenericParametersDuplicated1()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

			public interface IInterface
			{
				decimal Invoke<T>(System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>> param1, T param2);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		var types = new TypeModel[]
		{
			new("IDictionary", [("T1", true), ("System.Collections.Generic", "IList", [("T1", true)])], 1, @namespace: "System.Collections.Generic"),
			new("T1", 2, isGeneric: true),
		};

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock => _interfaceMock;

					protected void VerifyNoOtherCalls()
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

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> interfaceMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mockurai.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock,
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

					// Invoke
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupIDictionaryT1IListT1T1<T, decimal> SetupInvoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1, in It<T> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupIDictionaryT1IListT1T1<T, decimal>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupIDictionaryT1IListT1T1<T, decimal>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1, in It<T> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationIDictionaryT1IListT1T1<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIDictionaryT1IListT1T1<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1, in It<T> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationIDictionaryT1IListT1T1<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIDictionaryT1IListT1T1<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
						return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
					}

					private sealed class Proxy : MyNihongo.Mockurai.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public decimal Invoke<T>(System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>> param1, T param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationIDictionaryT1IListT1T1<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIDictionaryT1IListT1T1<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							return ((SetupIDictionaryT1IListT1T1<T, decimal>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
						}
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<MyNihongo.Mockurai.Tests.IInterface> @this)
					{
						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// Invoke
						public ISetup<SetupIDictionaryT1IListT1T1<T, decimal>.CallbackDelegate, decimal, SetupIDictionaryT1IListT1T1<T, decimal>.ReturnsCallbackDelegate> SetupInvoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1 = default, in It<T> param2 = default) =>
							((InterfaceMock)@this).SetupInvoke<T>(param1, param2);

						public void VerifyInvoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1, in It<T> param2, in Times times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times);

						public void VerifyInvoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1, in It<T> param2, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<MyNihongo.Mockurai.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1, in It<T> param2)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke<T>(param1, param2, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			CreateSetupReturnsCode(types),
			CreateInvocationCode(types),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task NestedGenericParametersDuplicated2()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

			public interface IInterface
			{
				decimal Invoke<T>(System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>> param1, System.Collections.Generic.ICollection<T> param2);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		var types = new TypeModel[]
		{
			new("T", 1, isGeneric: true),
			new("Int32", 2),
		};

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock => _interfaceMock;

					protected void VerifyNoOtherCalls()
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

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> interfaceMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mockurai.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock,
							};
						}
					}
				}
				"""
			),
			(
				"InterfaceMock.g.cs",
				"""

				"""
			),
			(
				"SetupIReadOnlyListInt32_TReturns_.g.cs",
				""
			),
			(
				"InvocationIReadOnlyListInt32.g.cs",
				""
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task NestedGenericParametersDeep()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

			public interface IInterface
			{
				decimal Invoke<T>(System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<T>> param1, int param2);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		var types = new TypeModel[]
		{
			new("System.Collections.Generic.IDictionary", ["String", ("System.Collections.Generic.IList", [("T", true)])], 1),
			new("Int32", 2),
		};

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock => _interfaceMock;

					protected void VerifyNoOtherCalls()
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

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> interfaceMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mockurai.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock,
							};
						}
					}
				}
				"""
			),
			(
				"InterfaceMock.g.cs",
				"""

				"""
			),
			(
				"SetupIReadOnlyListInt32_TReturns_.g.cs",
				""
			),
			(
				"InvocationIReadOnlyListInt32.g.cs",
				""
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task NestedGenericParametersMultiple()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

			public interface IInterface
			{
				decimal Invoke<TParam, TReturn>(System.Collections.Generic.IList<TParam> param1, System.Collections.Generic.ICollection<TReturn> param2);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		var types = new TypeModel[]
		{
			new("T", 1, isGeneric: true),
			new("Int32", 2),
		};

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock => _interfaceMock;

					protected void VerifyNoOtherCalls()
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

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> interfaceMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mockurai.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock,
							};
						}
					}
				}
				"""
			),
			(
				"InterfaceMock.g.cs",
				"""

				"""
			),
			(
				"SetupIReadOnlyListInt32_TReturns_.g.cs",
				""
			),
			(
				"InvocationIReadOnlyListInt32.g.cs",
				""
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task NestedGenericParametersDeepMultiple()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

			public interface IInterface
			{
				decimal Invoke<TParam, TReturn>(System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<TParam>> param1, System.Collections.Generic.ICollection<TReturn> param2);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		var types = new TypeModel[]
		{
			new("T", 1, isGeneric: true),
			new("Int32", 2),
		};

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock => _interfaceMock;

					protected void VerifyNoOtherCalls()
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

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> interfaceMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mockurai.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock,
							};
						}
					}
				}
				"""
			),
			(
				"InterfaceMock.g.cs",
				"""

				"""
			),
			(
				"SetupIReadOnlyListInt32_TReturns_.g.cs",
				""
			),
			(
				"InvocationIReadOnlyListInt32.g.cs",
				""
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task NestedArrayParameters()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

			public interface IInterface
			{
				decimal Invoke<T>(T[] param1, int param2);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		var types = new TypeModel[]
		{
			new("T", 1, isGeneric: true),
			new("Int32", 2),
		};

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock => _interfaceMock;

					protected void VerifyNoOtherCalls()
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

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> interfaceMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mockurai.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock,
							};
						}
					}
				}
				"""
			),
			(
				"InterfaceMock.g.cs",
				"""

				"""
			),
			(
				"SetupIReadOnlyListInt32_TReturns_.g.cs",
				""
			),
			(
				"InvocationIReadOnlyListInt32.g.cs",
				""
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
