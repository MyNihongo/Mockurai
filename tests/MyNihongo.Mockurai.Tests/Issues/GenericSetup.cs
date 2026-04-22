namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class GenericSetup : IssuesTestsBase
{
	[Fact]
	public async Task AdjustGenericTypeNameForTReturns()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

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

					private sealed class Proxy : Issues.Tests.IInterface
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

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(int param1, TReturns returnValue)>> Invoke<TReturns>()
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationInt32T1<TReturns>)_mock._invoke0Invocation.GetOrAdd(typeof(TReturns), static key => new InvocationInt32T1<TReturns>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							return invoke0Invocation.GetInvocationsWithArguments() ?? [];
						}
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
					extension(IMockSequence<Issues.Tests.IInterface> @this)
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
			namespace Issues.Tests;

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

					private sealed class Proxy : Issues.Tests.IInterface
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

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(System.Collections.Generic.IReadOnlyList<T> param1, int param2)>> Invoke<T>()
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationIReadOnlyListT1Int32<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIReadOnlyListT1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							return invoke0Invocation.GetInvocationsWithArguments() ?? [];
						}
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
					extension(IMockSequence<Issues.Tests.IInterface> @this)
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
			namespace Issues.Tests;

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

					private sealed class Proxy : Issues.Tests.IInterface
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

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>> param1, T param2)>> Invoke<T>()
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationIDictionaryT1IListT1T1<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIDictionaryT1IListT1T1<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							return invoke0Invocation.GetInvocationsWithArguments() ?? [];
						}
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
					extension(IMockSequence<Issues.Tests.IInterface> @this)
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
			namespace Issues.Tests;

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
			new("IDictionary", [("T1", true), ("System.Collections.Generic", "IList", [("T1", true)])], 1, @namespace: "System.Collections.Generic"),
			new("ICollection", [("T1", true)], 2, @namespace: "System.Collections.Generic"),
		};

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
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupIDictionaryT1IListT1ICollectionT1<T, decimal> SetupInvoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1, in It<System.Collections.Generic.ICollection<T>> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupIDictionaryT1IListT1ICollectionT1<T, decimal>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupIDictionaryT1IListT1ICollectionT1<T, decimal>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1, in It<System.Collections.Generic.ICollection<T>> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationIDictionaryT1IListT1ICollectionT1<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIDictionaryT1IListT1ICollectionT1<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1, in It<System.Collections.Generic.ICollection<T>> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationIDictionaryT1IListT1ICollectionT1<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIDictionaryT1IListT1ICollectionT1<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
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

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public decimal Invoke<T>(System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>> param1, System.Collections.Generic.ICollection<T> param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationIDictionaryT1IListT1ICollectionT1<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIDictionaryT1IListT1ICollectionT1<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							return ((SetupIDictionaryT1IListT1ICollectionT1<T, decimal>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>> param1, System.Collections.Generic.ICollection<T> param2)>> Invoke<T>()
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationIDictionaryT1IListT1ICollectionT1<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIDictionaryT1IListT1ICollectionT1<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							return invoke0Invocation.GetInvocationsWithArguments() ?? [];
						}
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
						public ISetup<SetupIDictionaryT1IListT1ICollectionT1<T, decimal>.CallbackDelegate, decimal, SetupIDictionaryT1IListT1ICollectionT1<T, decimal>.ReturnsCallbackDelegate> SetupInvoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1 = default, in It<System.Collections.Generic.ICollection<T>> param2 = default) =>
							((InterfaceMock)@this).SetupInvoke<T>(param1, param2);

						public void VerifyInvoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1, in It<System.Collections.Generic.ICollection<T>> param2, in Times times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times);

						public void VerifyInvoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1, in It<System.Collections.Generic.ICollection<T>> param2, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke<T>(in It<System.Collections.Generic.IDictionary<T, System.Collections.Generic.IList<T>>> param1, in It<System.Collections.Generic.ICollection<T>> param2)
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
	public async Task NestedGenericParametersDeep()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

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
			new("IDictionary", ["String", ("System.Collections.Generic", "IList", [("T1", true)])], 1, @namespace: "System.Collections.Generic"),
			new("Int32", 2),
		};

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
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupIDictionaryStringIListT1Int32<T, decimal> SetupInvoke<T>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<T>>> param1, in It<int> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupIDictionaryStringIListT1Int32<T, decimal>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupIDictionaryStringIListT1Int32<T, decimal>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<T>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<T>>> param1, in It<int> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationIDictionaryStringIListT1Int32<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIDictionaryStringIListT1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<T>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<T>>> param1, in It<int> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationIDictionaryStringIListT1Int32<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIDictionaryStringIListT1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
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

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public decimal Invoke<T>(System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<T>> param1, int param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationIDictionaryStringIListT1Int32<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIDictionaryStringIListT1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							return ((SetupIDictionaryStringIListT1Int32<T, decimal>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<T>> param1, int param2)>> Invoke<T>()
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationIDictionaryStringIListT1Int32<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationIDictionaryStringIListT1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							return invoke0Invocation.GetInvocationsWithArguments() ?? [];
						}
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
						public ISetup<SetupIDictionaryStringIListT1Int32<T, decimal>.CallbackDelegate, decimal, SetupIDictionaryStringIListT1Int32<T, decimal>.ReturnsCallbackDelegate> SetupInvoke<T>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<T>>> param1 = default, in It<int> param2 = default) =>
							((InterfaceMock)@this).SetupInvoke<T>(param1, param2);

						public void VerifyInvoke<T>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<T>>> param1, in It<int> param2, in Times times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times);

						public void VerifyInvoke<T>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<T>>> param1, in It<int> param2, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke<T>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<T>>> param1, in It<int> param2)
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
	public async Task NestedGenericParametersMultiple()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

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
			new("IList", [("T1", true)], 1, @namespace: "System.Collections.Generic"),
			new("ICollection", [("T2", true)], 2, @namespace: "System.Collections.Generic"),
		};

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
					private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
					private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

					public SetupIListT1ICollectionT2<TParam, TReturn, decimal> SetupInvoke<TParam, TReturn>(in It<System.Collections.Generic.IList<TParam>> param1, in It<System.Collections.Generic.ICollection<TReturn>> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
						var invoke0 = (SetupIListT1ICollectionT2<TParam, TReturn, decimal>)_invoke0.GetOrAdd((typeof(TParam), typeof(TReturn)), static _ => new SetupIListT1ICollectionT2<TParam, TReturn, decimal>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<TParam, TReturn>(in It<System.Collections.Generic.IList<TParam>> param1, in It<System.Collections.Generic.ICollection<TReturn>> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
						var invoke0Invocation = (InvocationIListT1ICollectionT2<TParam, TReturn>)_invoke0Invocation.GetOrAdd((typeof(TParam), typeof(TReturn)), static key => new InvocationIListT1ICollectionT2<TParam, TReturn>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}}, {{1}})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<TParam, TReturn>(in It<System.Collections.Generic.IList<TParam>> param1, in It<System.Collections.Generic.ICollection<TReturn>> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
						var invoke0Invocation = (InvocationIListT1ICollectionT2<TParam, TReturn>)_invoke0Invocation.GetOrAdd((typeof(TParam), typeof(TReturn)), static key => new InvocationIListT1ICollectionT2<TParam, TReturn>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}}, {{1}})"));
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

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public decimal Invoke<TParam, TReturn>(System.Collections.Generic.IList<TParam> param1, System.Collections.Generic.ICollection<TReturn> param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
							var invoke0Invocation = (InvocationIListT1ICollectionT2<TParam, TReturn>)_mock._invoke0Invocation.GetOrAdd((typeof(TParam), typeof(TReturn)), static key => new InvocationIListT1ICollectionT2<TParam, TReturn>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}}, {{1}})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							return ((SetupIListT1ICollectionT2<TParam, TReturn, decimal>?)_mock._invoke0?.ValueOrDefault((typeof(TParam), typeof(TReturn))))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(System.Collections.Generic.IList<TParam> param1, System.Collections.Generic.ICollection<TReturn> param2)>> Invoke<TParam, TReturn>()
						{
							_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
							var invoke0Invocation = (InvocationIListT1ICollectionT2<TParam, TReturn>)_mock._invoke0Invocation.GetOrAdd((typeof(TParam), typeof(TReturn)), static key => new InvocationIListT1ICollectionT2<TParam, TReturn>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}}, {{1}})"));
							return invoke0Invocation.GetInvocationsWithArguments() ?? [];
						}
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
						public ISetup<SetupIListT1ICollectionT2<TParam, TReturn, decimal>.CallbackDelegate, decimal, SetupIListT1ICollectionT2<TParam, TReturn, decimal>.ReturnsCallbackDelegate> SetupInvoke<TParam, TReturn>(in It<System.Collections.Generic.IList<TParam>> param1 = default, in It<System.Collections.Generic.ICollection<TReturn>> param2 = default) =>
							((InterfaceMock)@this).SetupInvoke<TParam, TReturn>(param1, param2);

						public void VerifyInvoke<TParam, TReturn>(in It<System.Collections.Generic.IList<TParam>> param1, in It<System.Collections.Generic.ICollection<TReturn>> param2, in Times times) =>
							((InterfaceMock)@this).VerifyInvoke<TParam, TReturn>(param1, param2, times);

						public void VerifyInvoke<TParam, TReturn>(in It<System.Collections.Generic.IList<TParam>> param1, in It<System.Collections.Generic.ICollection<TReturn>> param2, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke<TParam, TReturn>(param1, param2, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke<TParam, TReturn>(in It<System.Collections.Generic.IList<TParam>> param1, in It<System.Collections.Generic.ICollection<TReturn>> param2)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke<TParam, TReturn>(param1, param2, @this.VerifyIndex);
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
	public async Task NestedGenericParametersDeepMultiple()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

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
			new("IDictionary", ["String", ("System.Collections.Generic", "IList", [("T1", true)])], 1, @namespace: "System.Collections.Generic"),
			new("ICollection", [("T2", true)], 2, @namespace: "System.Collections.Generic"),
		};

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
					private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
					private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

					public SetupIDictionaryStringIListT1ICollectionT2<TParam, TReturn, decimal> SetupInvoke<TParam, TReturn>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<TParam>>> param1, in It<System.Collections.Generic.ICollection<TReturn>> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
						var invoke0 = (SetupIDictionaryStringIListT1ICollectionT2<TParam, TReturn, decimal>)_invoke0.GetOrAdd((typeof(TParam), typeof(TReturn)), static _ => new SetupIDictionaryStringIListT1ICollectionT2<TParam, TReturn, decimal>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<TParam, TReturn>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<TParam>>> param1, in It<System.Collections.Generic.ICollection<TReturn>> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
						var invoke0Invocation = (InvocationIDictionaryStringIListT1ICollectionT2<TParam, TReturn>)_invoke0Invocation.GetOrAdd((typeof(TParam), typeof(TReturn)), static key => new InvocationIDictionaryStringIListT1ICollectionT2<TParam, TReturn>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}}, {{1}})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<TParam, TReturn>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<TParam>>> param1, in It<System.Collections.Generic.ICollection<TReturn>> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
						var invoke0Invocation = (InvocationIDictionaryStringIListT1ICollectionT2<TParam, TReturn>)_invoke0Invocation.GetOrAdd((typeof(TParam), typeof(TReturn)), static key => new InvocationIDictionaryStringIListT1ICollectionT2<TParam, TReturn>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}}, {{1}})"));
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

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public decimal Invoke<TParam, TReturn>(System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<TParam>> param1, System.Collections.Generic.ICollection<TReturn> param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
							var invoke0Invocation = (InvocationIDictionaryStringIListT1ICollectionT2<TParam, TReturn>)_mock._invoke0Invocation.GetOrAdd((typeof(TParam), typeof(TReturn)), static key => new InvocationIDictionaryStringIListT1ICollectionT2<TParam, TReturn>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}}, {{1}})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							return ((SetupIDictionaryStringIListT1ICollectionT2<TParam, TReturn, decimal>?)_mock._invoke0?.ValueOrDefault((typeof(TParam), typeof(TReturn))))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<TParam>> param1, System.Collections.Generic.ICollection<TReturn> param2)>> Invoke<TParam, TReturn>()
						{
							_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
							var invoke0Invocation = (InvocationIDictionaryStringIListT1ICollectionT2<TParam, TReturn>)_mock._invoke0Invocation.GetOrAdd((typeof(TParam), typeof(TReturn)), static key => new InvocationIDictionaryStringIListT1ICollectionT2<TParam, TReturn>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}}, {{1}})"));
							return invoke0Invocation.GetInvocationsWithArguments() ?? [];
						}
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
						public ISetup<SetupIDictionaryStringIListT1ICollectionT2<TParam, TReturn, decimal>.CallbackDelegate, decimal, SetupIDictionaryStringIListT1ICollectionT2<TParam, TReturn, decimal>.ReturnsCallbackDelegate> SetupInvoke<TParam, TReturn>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<TParam>>> param1 = default, in It<System.Collections.Generic.ICollection<TReturn>> param2 = default) =>
							((InterfaceMock)@this).SetupInvoke<TParam, TReturn>(param1, param2);

						public void VerifyInvoke<TParam, TReturn>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<TParam>>> param1, in It<System.Collections.Generic.ICollection<TReturn>> param2, in Times times) =>
							((InterfaceMock)@this).VerifyInvoke<TParam, TReturn>(param1, param2, times);

						public void VerifyInvoke<TParam, TReturn>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<TParam>>> param1, in It<System.Collections.Generic.ICollection<TReturn>> param2, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke<TParam, TReturn>(param1, param2, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke<TParam, TReturn>(in It<System.Collections.Generic.IDictionary<string, System.Collections.Generic.IList<TParam>>> param1, in It<System.Collections.Generic.ICollection<TReturn>> param2)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke<TParam, TReturn>(param1, param2, @this.VerifyIndex);
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
	public async Task NestedArrayParameters()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

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
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupArray1T1Int32<T, decimal> SetupInvoke<T>(in It<T[]> param1, in It<int> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupArray1T1Int32<T, decimal>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupArray1T1Int32<T, decimal>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<T>(in It<T[]> param1, in It<int> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationArray1T1Int32<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray1T1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<T>(in It<T[]> param1, in It<int> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationArray1T1Int32<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray1T1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
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

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public decimal Invoke<T>(T[] param1, int param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationArray1T1Int32<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray1T1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							return ((SetupArray1T1Int32<T, decimal>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(T[] param1, int param2)>> Invoke<T>()
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationArray1T1Int32<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray1T1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							return invoke0Invocation.GetInvocationsWithArguments() ?? [];
						}
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
						public ISetup<SetupArray1T1Int32<T, decimal>.CallbackDelegate, decimal, SetupArray1T1Int32<T, decimal>.ReturnsCallbackDelegate> SetupInvoke<T>(in It<T[]> param1 = default, in It<int> param2 = default) =>
							((InterfaceMock)@this).SetupInvoke<T>(param1, param2);

						public void VerifyInvoke<T>(in It<T[]> param1, in It<int> param2, in Times times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times);

						public void VerifyInvoke<T>(in It<T[]> param1, in It<int> param2, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke<T>(in It<T[]> param1, in It<int> param2)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke<T>(param1, param2, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			(
				"SetupArray1T1Int32_T1_TReturns_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupArray1T1Int32<T1, TReturns> : ISetupCallbackJoin<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>, ISetupCallbackReset<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsJoin<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsReset<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(T1[] parameter1, int parameter2);
					public delegate TReturns? ReturnsCallbackDelegate(T1[] parameter1, int parameter2);

					public bool Execute(T1[] parameter1, int parameter2, out TReturns? returnValue)
					{
						if (_setups is null)
							goto Default;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2);

							if (x.Exception is not null)
								throw x.Exception;

							if (x.Returns is not null)
							{
								returnValue = x.Returns(parameter1, parameter2);
								return true;
							}

							goto Default;
						}

						Default:
						returnValue = default!;
						return false;
					}

					public void SetupParameters(in ItSetup<T1[]> parameter1, in ItSetup<int> parameter2)
					{
						_currentSetup = new Item(parameter1, parameter2);

						_setups ??= new SetupContainer<Item>(SortComparer);
						_setups.Add(_currentSetup);
					}

					public void Returns(TReturns? returns)
					{
						Returns((_, _) => returns);
					}

					public void Returns(in ReturnsCallbackDelegate returns)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(returns);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupReturnsThrowsJoin<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetupCallbackJoin<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupCallbackStart<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupCallbackReset<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsJoin<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupReturnsThrowsReset<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupCallbackJoin<SetupArray1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<T1[]>? Parameter1;
						public readonly ItSetup<int>? Parameter2;

						public Item(in ItSetup<T1[]>? parameter1, in ItSetup<int>? parameter2)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in ReturnsCallbackDelegate returns)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Returns = returns;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(returns: returns);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;
						public ReturnsCallbackDelegate? Returns;

						public ItemSetup(in ReturnsCallbackDelegate? returns = null, in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Returns = returns;
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}
				}
				"""
			),
			(
				"InvocationArray1T1Int32_T1_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InvocationArray1T1Int32<T1> : IInvocationVerify
				{
					private readonly string _name;
					private readonly string? _parameter1Prefix, _parameter2Prefix;
					private readonly InvocationContainer<Item> _invocations = [];

					public InvocationArray1T1Int32(string name, string? parameter1Prefix = null, string? parameter2Prefix = null)
					{
						_name = name;
						_parameter1Prefix = parameter1Prefix;
						_parameter2Prefix = parameter2Prefix;
					}

					public void Register(in InvocationIndex.Counter index, T1[] parameter1, int parameter2)
					{
						var invokedIndex = index.Increment();
						_invocations.Add(new Item(invokedIndex, parameter1, parameter2, invocation: this));
					}

					public void Verify(in ItSetup<T1[]> parameter1, in ItSetup<int> parameter2, in Times times, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpan();

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						var count = 0;
						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							count++;
						}

						if (times.Predicate(count))
							return;

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifyCountException(verifyName, times, count, invocations);
					}

					public long Verify(in ItSetup<T1[]> parameter1, in ItSetup<int> parameter2, long index, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpanFrom(index);

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							return span[i].Index + 1;
						}

						if (invocationProviders is null)
						{
							span = _invocations.GetItemsSpanBefore(index);
							for (var i = 0; i < span.Length; i++)
								verifyOutput.Insert(i, (span[i], null));
						}

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
					}

					public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
						if (unverifiedItems is null)
							return;

						var typeNameParameter1 = !string.IsNullOrEmpty(_parameter1Prefix) ? $"{_parameter1Prefix} {typeof(T1).Name}[]" : $"{typeof(T1).Name}[]";
						var typeNameParameter2 = !string.IsNullOrEmpty(_parameter2Prefix) ? $"{_parameter2Prefix} int" : "int";
						var verifyName = string.Format(_name, typeNameParameter1, typeNameParameter2);
						throw new MockUnverifiedException(verifyName, unverifiedItems);
					}

					public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
					{
						return _invocations;
					}

					public System.Collections.Generic.IEnumerable<IInvocation<(T1[] parameter1, int parameter2)>> GetInvocationsWithArguments()
					{
						return _invocations;
					}

					private sealed class Item : IInvocation<(T1[] parameter1, int parameter2)>
					{
						private readonly (T1[] parameter1, int parameter2) _argument;
						private readonly string? _jsonSnapshotParameter1, _jsonSnapshotParameter2;
						private readonly InvocationArray1T1Int32<T1> _invocation;

						public Item(long index, T1[] parameter1, int parameter2, InvocationArray1T1Int32<T1> invocation)
						{
							_argument = (parameter1, parameter2);
							_invocation = invocation;
							Index = index;

							try
							{
								_jsonSnapshotParameter1 = parameter1.ToJsonString();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter2 = parameter2.ToJsonString();
							}
							catch
							{
								// Swallow
							}
						}

						public long Index { get; }

						public bool IsVerified { get; set; }

						public (T1[] parameter1, int parameter2) Arguments => _argument;

						public T1[] GetParameter1(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter1)
								? System.Text.Json.JsonSerializer.Deserialize<T1[]>(_jsonSnapshotParameter1)!
								: _argument.parameter1;
						}

						public int GetParameter2(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter2)
								? System.Text.Json.JsonSerializer.Deserialize<int>(_jsonSnapshotParameter2)!
								: _argument.parameter2;
						}

						public override string ToString()
						{
							var stringBuilder = new System.Text.StringBuilder();

							// parameter1
							if (!string.IsNullOrEmpty(_invocation._parameter1Prefix))
								stringBuilder.Append($"{_invocation._parameter1Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter1))
								stringBuilder.Append(_jsonSnapshotParameter1);
							else
								stringBuilder.Append(_argument.parameter1);
							var parameter1 = stringBuilder.ToString();

							// parameter2
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter2Prefix))
								stringBuilder.Append($"{_invocation._parameter2Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter2))
								stringBuilder.Append(_jsonSnapshotParameter2);
							else
								stringBuilder.Append(_argument.parameter2);
							var parameter2 = stringBuilder.ToString();

							var stringValue = string.Format(_invocation._name, parameter1, parameter2);
							return $"{Index}: {stringValue}";
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
	public async Task NestedArrayParametersDeep()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface
			{
				decimal Invoke<T>(System.Collections.Generic.ICollection<T>[] param1, int param2);
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
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupArray1ICollectionT1Int32<T, decimal> SetupInvoke<T>(in It<global::System.Collections.Generic.ICollection<T>[]> param1, in It<int> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupArray1ICollectionT1Int32<T, decimal>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupArray1ICollectionT1Int32<T, decimal>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<T>(in It<global::System.Collections.Generic.ICollection<T>[]> param1, in It<int> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationArray1ICollectionT1Int32<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray1ICollectionT1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<T>(in It<global::System.Collections.Generic.ICollection<T>[]> param1, in It<int> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationArray1ICollectionT1Int32<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray1ICollectionT1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
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

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public decimal Invoke<T>(global::System.Collections.Generic.ICollection<T>[] param1, int param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationArray1ICollectionT1Int32<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray1ICollectionT1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							return ((SetupArray1ICollectionT1Int32<T, decimal>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(global::System.Collections.Generic.ICollection<T>[] param1, int param2)>> Invoke<T>()
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationArray1ICollectionT1Int32<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray1ICollectionT1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							return invoke0Invocation.GetInvocationsWithArguments() ?? [];
						}
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
						public ISetup<SetupArray1ICollectionT1Int32<T, decimal>.CallbackDelegate, decimal, SetupArray1ICollectionT1Int32<T, decimal>.ReturnsCallbackDelegate> SetupInvoke<T>(in It<global::System.Collections.Generic.ICollection<T>[]> param1 = default, in It<int> param2 = default) =>
							((InterfaceMock)@this).SetupInvoke<T>(param1, param2);

						public void VerifyInvoke<T>(in It<global::System.Collections.Generic.ICollection<T>[]> param1, in It<int> param2, in Times times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times);

						public void VerifyInvoke<T>(in It<global::System.Collections.Generic.ICollection<T>[]> param1, in It<int> param2, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke<T>(in It<global::System.Collections.Generic.ICollection<T>[]> param1, in It<int> param2)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke<T>(param1, param2, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			(
				"SetupArray1ICollectionT1Int32_T1_TReturns_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupArray1ICollectionT1Int32<T1, TReturns> : ISetupCallbackJoin<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>, ISetupCallbackReset<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsJoin<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsReset<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(System.Collections.Generic.ICollection<T1>[] parameter1, int parameter2);
					public delegate TReturns? ReturnsCallbackDelegate(System.Collections.Generic.ICollection<T1>[] parameter1, int parameter2);

					public bool Execute(System.Collections.Generic.ICollection<T1>[] parameter1, int parameter2, out TReturns? returnValue)
					{
						if (_setups is null)
							goto Default;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2);

							if (x.Exception is not null)
								throw x.Exception;

							if (x.Returns is not null)
							{
								returnValue = x.Returns(parameter1, parameter2);
								return true;
							}

							goto Default;
						}

						Default:
						returnValue = default!;
						return false;
					}

					public void SetupParameters(in ItSetup<System.Collections.Generic.ICollection<T1>[]> parameter1, in ItSetup<int> parameter2)
					{
						_currentSetup = new Item(parameter1, parameter2);

						_setups ??= new SetupContainer<Item>(SortComparer);
						_setups.Add(_currentSetup);
					}

					public void Returns(TReturns? returns)
					{
						Returns((_, _) => returns);
					}

					public void Returns(in ReturnsCallbackDelegate returns)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(returns);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupReturnsThrowsJoin<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetupCallbackJoin<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupCallbackStart<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupCallbackReset<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsJoin<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupReturnsThrowsReset<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupCallbackJoin<SetupArray1ICollectionT1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1ICollectionT1Int32<T1, TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<System.Collections.Generic.ICollection<T1>[]>? Parameter1;
						public readonly ItSetup<int>? Parameter2;

						public Item(in ItSetup<System.Collections.Generic.ICollection<T1>[]>? parameter1, in ItSetup<int>? parameter2)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in ReturnsCallbackDelegate returns)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Returns = returns;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(returns: returns);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;
						public ReturnsCallbackDelegate? Returns;

						public ItemSetup(in ReturnsCallbackDelegate? returns = null, in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Returns = returns;
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}
				}
				"""
			),
			(
				"InvocationArray1ICollectionT1Int32_T1_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InvocationArray1ICollectionT1Int32<T1> : IInvocationVerify
				{
					private readonly string _name;
					private readonly string? _parameter1Prefix, _parameter2Prefix;
					private readonly InvocationContainer<Item> _invocations = [];

					public InvocationArray1ICollectionT1Int32(string name, string? parameter1Prefix = null, string? parameter2Prefix = null)
					{
						_name = name;
						_parameter1Prefix = parameter1Prefix;
						_parameter2Prefix = parameter2Prefix;
					}

					public void Register(in InvocationIndex.Counter index, System.Collections.Generic.ICollection<T1>[] parameter1, int parameter2)
					{
						var invokedIndex = index.Increment();
						_invocations.Add(new Item(invokedIndex, parameter1, parameter2, invocation: this));
					}

					public void Verify(in ItSetup<System.Collections.Generic.ICollection<T1>[]> parameter1, in ItSetup<int> parameter2, in Times times, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpan();

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						var count = 0;
						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							count++;
						}

						if (times.Predicate(count))
							return;

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifyCountException(verifyName, times, count, invocations);
					}

					public long Verify(in ItSetup<System.Collections.Generic.ICollection<T1>[]> parameter1, in ItSetup<int> parameter2, long index, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpanFrom(index);

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							return span[i].Index + 1;
						}

						if (invocationProviders is null)
						{
							span = _invocations.GetItemsSpanBefore(index);
							for (var i = 0; i < span.Length; i++)
								verifyOutput.Insert(i, (span[i], null));
						}

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
					}

					public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
						if (unverifiedItems is null)
							return;

						var typeNameParameter1 = !string.IsNullOrEmpty(_parameter1Prefix) ? $"{_parameter1Prefix} System.Collections.Generic.ICollection<{typeof(T1).Name}>[]" : $"System.Collections.Generic.ICollection<{typeof(T1).Name}>[]";
						var typeNameParameter2 = !string.IsNullOrEmpty(_parameter2Prefix) ? $"{_parameter2Prefix} int" : "int";
						var verifyName = string.Format(_name, typeNameParameter1, typeNameParameter2);
						throw new MockUnverifiedException(verifyName, unverifiedItems);
					}

					public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
					{
						return _invocations;
					}

					public System.Collections.Generic.IEnumerable<IInvocation<(System.Collections.Generic.ICollection<T1>[] parameter1, int parameter2)>> GetInvocationsWithArguments()
					{
						return _invocations;
					}

					private sealed class Item : IInvocation<(System.Collections.Generic.ICollection<T1>[] parameter1, int parameter2)>
					{
						private readonly (System.Collections.Generic.ICollection<T1>[] parameter1, int parameter2) _argument;
						private readonly string? _jsonSnapshotParameter1, _jsonSnapshotParameter2;
						private readonly InvocationArray1ICollectionT1Int32<T1> _invocation;

						public Item(long index, System.Collections.Generic.ICollection<T1>[] parameter1, int parameter2, InvocationArray1ICollectionT1Int32<T1> invocation)
						{
							_argument = (parameter1, parameter2);
							_invocation = invocation;
							Index = index;

							try
							{
								_jsonSnapshotParameter1 = parameter1.ToJsonString();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter2 = parameter2.ToJsonString();
							}
							catch
							{
								// Swallow
							}
						}

						public long Index { get; }

						public bool IsVerified { get; set; }

						public (System.Collections.Generic.ICollection<T1>[] parameter1, int parameter2) Arguments => _argument;

						public System.Collections.Generic.ICollection<T1>[] GetParameter1(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter1)
								? System.Text.Json.JsonSerializer.Deserialize<System.Collections.Generic.ICollection<T1>[]>(_jsonSnapshotParameter1)!
								: _argument.parameter1;
						}

						public int GetParameter2(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter2)
								? System.Text.Json.JsonSerializer.Deserialize<int>(_jsonSnapshotParameter2)!
								: _argument.parameter2;
						}

						public override string ToString()
						{
							var stringBuilder = new System.Text.StringBuilder();

							// parameter1
							if (!string.IsNullOrEmpty(_invocation._parameter1Prefix))
								stringBuilder.Append($"{_invocation._parameter1Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter1))
								stringBuilder.Append(_jsonSnapshotParameter1);
							else
								stringBuilder.Append(_argument.parameter1);
							var parameter1 = stringBuilder.ToString();

							// parameter2
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter2Prefix))
								stringBuilder.Append($"{_invocation._parameter2Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter2))
								stringBuilder.Append(_jsonSnapshotParameter2);
							else
								stringBuilder.Append(_argument.parameter2);
							var parameter2 = stringBuilder.ToString();

							var stringValue = string.Format(_invocation._name, parameter1, parameter2);
							return $"{Index}: {stringValue}";
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
	public async Task NestedArrayParametersMulti()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface
			{
				decimal Invoke<T>(T[,] param1, int param2);
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
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupArray2T1Int32<T, decimal> SetupInvoke<T>(in It<T[,]> param1, in It<int> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupArray2T1Int32<T, decimal>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupArray2T1Int32<T, decimal>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<T>(in It<T[,]> param1, in It<int> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationArray2T1Int32<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray2T1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<T>(in It<T[,]> param1, in It<int> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationArray2T1Int32<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray2T1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
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

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public decimal Invoke<T>(T[,] param1, int param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationArray2T1Int32<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray2T1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							return ((SetupArray2T1Int32<T, decimal>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(T[,] param1, int param2)>> Invoke<T>()
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationArray2T1Int32<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray2T1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							return invoke0Invocation.GetInvocationsWithArguments() ?? [];
						}
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
						public ISetup<SetupArray2T1Int32<T, decimal>.CallbackDelegate, decimal, SetupArray2T1Int32<T, decimal>.ReturnsCallbackDelegate> SetupInvoke<T>(in It<T[,]> param1 = default, in It<int> param2 = default) =>
							((InterfaceMock)@this).SetupInvoke<T>(param1, param2);

						public void VerifyInvoke<T>(in It<T[,]> param1, in It<int> param2, in Times times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times);

						public void VerifyInvoke<T>(in It<T[,]> param1, in It<int> param2, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke<T>(in It<T[,]> param1, in It<int> param2)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke<T>(param1, param2, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			(
				"SetupArray2T1Int32_T1_TReturns_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupArray2T1Int32<T1, TReturns> : ISetupCallbackJoin<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>, ISetupCallbackReset<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsJoin<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsReset<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(T1[,] parameter1, int parameter2);
					public delegate TReturns? ReturnsCallbackDelegate(T1[,] parameter1, int parameter2);

					public bool Execute(T1[,] parameter1, int parameter2, out TReturns? returnValue)
					{
						if (_setups is null)
							goto Default;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2);

							if (x.Exception is not null)
								throw x.Exception;

							if (x.Returns is not null)
							{
								returnValue = x.Returns(parameter1, parameter2);
								return true;
							}

							goto Default;
						}

						Default:
						returnValue = default!;
						return false;
					}

					public void SetupParameters(in ItSetup<T1[,]> parameter1, in ItSetup<int> parameter2)
					{
						_currentSetup = new Item(parameter1, parameter2);

						_setups ??= new SetupContainer<Item>(SortComparer);
						_setups.Add(_currentSetup);
					}

					public void Returns(TReturns? returns)
					{
						Returns((_, _) => returns);
					}

					public void Returns(in ReturnsCallbackDelegate returns)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(returns);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupReturnsThrowsJoin<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetupCallbackJoin<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupCallbackStart<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupCallbackReset<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsJoin<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupReturnsThrowsReset<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupCallbackJoin<SetupArray2T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray2T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<T1[,]>? Parameter1;
						public readonly ItSetup<int>? Parameter2;

						public Item(in ItSetup<T1[,]>? parameter1, in ItSetup<int>? parameter2)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in ReturnsCallbackDelegate returns)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Returns = returns;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(returns: returns);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;
						public ReturnsCallbackDelegate? Returns;

						public ItemSetup(in ReturnsCallbackDelegate? returns = null, in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Returns = returns;
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}
				}
				"""
			),
			(
				"InvocationArray2T1Int32_T1_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InvocationArray2T1Int32<T1> : IInvocationVerify
				{
					private readonly string _name;
					private readonly string? _parameter1Prefix, _parameter2Prefix;
					private readonly InvocationContainer<Item> _invocations = [];

					public InvocationArray2T1Int32(string name, string? parameter1Prefix = null, string? parameter2Prefix = null)
					{
						_name = name;
						_parameter1Prefix = parameter1Prefix;
						_parameter2Prefix = parameter2Prefix;
					}

					public void Register(in InvocationIndex.Counter index, T1[,] parameter1, int parameter2)
					{
						var invokedIndex = index.Increment();
						_invocations.Add(new Item(invokedIndex, parameter1, parameter2, invocation: this));
					}

					public void Verify(in ItSetup<T1[,]> parameter1, in ItSetup<int> parameter2, in Times times, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpan();

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						var count = 0;
						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							count++;
						}

						if (times.Predicate(count))
							return;

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifyCountException(verifyName, times, count, invocations);
					}

					public long Verify(in ItSetup<T1[,]> parameter1, in ItSetup<int> parameter2, long index, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpanFrom(index);

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							return span[i].Index + 1;
						}

						if (invocationProviders is null)
						{
							span = _invocations.GetItemsSpanBefore(index);
							for (var i = 0; i < span.Length; i++)
								verifyOutput.Insert(i, (span[i], null));
						}

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
					}

					public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
						if (unverifiedItems is null)
							return;

						var typeNameParameter1 = !string.IsNullOrEmpty(_parameter1Prefix) ? $"{_parameter1Prefix} {typeof(T1).Name}[,]" : $"{typeof(T1).Name}[,]";
						var typeNameParameter2 = !string.IsNullOrEmpty(_parameter2Prefix) ? $"{_parameter2Prefix} int" : "int";
						var verifyName = string.Format(_name, typeNameParameter1, typeNameParameter2);
						throw new MockUnverifiedException(verifyName, unverifiedItems);
					}

					public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
					{
						return _invocations;
					}

					public System.Collections.Generic.IEnumerable<IInvocation<(T1[,] parameter1, int parameter2)>> GetInvocationsWithArguments()
					{
						return _invocations;
					}

					private sealed class Item : IInvocation<(T1[,] parameter1, int parameter2)>
					{
						private readonly (T1[,] parameter1, int parameter2) _argument;
						private readonly string? _jsonSnapshotParameter1, _jsonSnapshotParameter2;
						private readonly InvocationArray2T1Int32<T1> _invocation;

						public Item(long index, T1[,] parameter1, int parameter2, InvocationArray2T1Int32<T1> invocation)
						{
							_argument = (parameter1, parameter2);
							_invocation = invocation;
							Index = index;

							try
							{
								_jsonSnapshotParameter1 = parameter1.ToJsonString();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter2 = parameter2.ToJsonString();
							}
							catch
							{
								// Swallow
							}
						}

						public long Index { get; }

						public bool IsVerified { get; set; }

						public (T1[,] parameter1, int parameter2) Arguments => _argument;

						public T1[,] GetParameter1(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter1)
								? System.Text.Json.JsonSerializer.Deserialize<T1[,]>(_jsonSnapshotParameter1)!
								: _argument.parameter1;
						}

						public int GetParameter2(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter2)
								? System.Text.Json.JsonSerializer.Deserialize<int>(_jsonSnapshotParameter2)!
								: _argument.parameter2;
						}

						public override string ToString()
						{
							var stringBuilder = new System.Text.StringBuilder();

							// parameter1
							if (!string.IsNullOrEmpty(_invocation._parameter1Prefix))
								stringBuilder.Append($"{_invocation._parameter1Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter1))
								stringBuilder.Append(_jsonSnapshotParameter1);
							else
								stringBuilder.Append(_argument.parameter1);
							var parameter1 = stringBuilder.ToString();

							// parameter2
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter2Prefix))
								stringBuilder.Append($"{_invocation._parameter2Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter2))
								stringBuilder.Append(_jsonSnapshotParameter2);
							else
								stringBuilder.Append(_argument.parameter2);
							var parameter2 = stringBuilder.ToString();

							var stringValue = string.Format(_invocation._name, parameter1, parameter2);
							return $"{Index}: {stringValue}";
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
	public async Task NestedArrayParametersJagged()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

			public interface IInterface
			{
				decimal Invoke<T>(T[][] param1, int param2);
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
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupArray1Array1T1Int32<T, decimal> SetupInvoke<T>(in It<T[][]> param1, in It<int> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupArray1Array1T1Int32<T, decimal>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupArray1Array1T1Int32<T, decimal>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<T>(in It<T[][]> param1, in It<int> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationArray1Array1T1Int32<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray1Array1T1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<T>(in It<T[][]> param1, in It<int> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationArray1Array1T1Int32<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray1Array1T1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
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

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public decimal Invoke<T>(T[][] param1, int param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationArray1Array1T1Int32<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray1Array1T1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							return ((SetupArray1Array1T1Int32<T, decimal>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
						}
					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(T[][] param1, int param2)>> Invoke<T>()
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationArray1Array1T1Int32<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationArray1Array1T1Int32<T>($"IInterface.Invoke<{key.Name}>({{0}}, {{1}})"));
							return invoke0Invocation.GetInvocationsWithArguments() ?? [];
						}
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
						public ISetup<SetupArray1Array1T1Int32<T, decimal>.CallbackDelegate, decimal, SetupArray1Array1T1Int32<T, decimal>.ReturnsCallbackDelegate> SetupInvoke<T>(in It<T[][]> param1 = default, in It<int> param2 = default) =>
							((InterfaceMock)@this).SetupInvoke<T>(param1, param2);

						public void VerifyInvoke<T>(in It<T[][]> param1, in It<int> param2, in Times times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times);

						public void VerifyInvoke<T>(in It<T[][]> param1, in It<int> param2, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke<T>(param1, param2, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke<T>(in It<T[][]> param1, in It<int> param2)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke<T>(param1, param2, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			(
				"SetupArray1Array1T1Int32_T1_TReturns_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class SetupArray1Array1T1Int32<T1, TReturns> : ISetupCallbackJoin<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>, ISetupCallbackReset<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsJoin<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>, ISetupReturnsThrowsReset<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>
				{
					private static readonly Comparer SortComparer = new();
					private SetupContainer<Item>? _setups;
					private Item? _currentSetup;

					public delegate void CallbackDelegate(T1[][] parameter1, int parameter2);
					public delegate TReturns? ReturnsCallbackDelegate(T1[][] parameter1, int parameter2);

					public bool Execute(T1[][] parameter1, int parameter2, out TReturns? returnValue)
					{
						if (_setups is null)
							goto Default;

						foreach (var setup in _setups)
						{
							if (setup.Parameter1.HasValue && !setup.Parameter1.Value.Check(parameter1))
								continue;
							if (setup.Parameter2.HasValue && !setup.Parameter2.Value.Check(parameter2))
								continue;

							var x = setup.GetSetup();
							x.Callback?.Invoke(parameter1, parameter2);

							if (x.Exception is not null)
								throw x.Exception;

							if (x.Returns is not null)
							{
								returnValue = x.Returns(parameter1, parameter2);
								return true;
							}

							goto Default;
						}

						Default:
						returnValue = default!;
						return false;
					}

					public void SetupParameters(in ItSetup<T1[][]> parameter1, in ItSetup<int> parameter2)
					{
						_currentSetup = new Item(parameter1, parameter2);

						_setups ??= new SetupContainer<Item>(SortComparer);
						_setups.Add(_currentSetup);
					}

					public void Returns(TReturns? returns)
					{
						Returns((_, _) => returns);
					}

					public void Returns(in ReturnsCallbackDelegate returns)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(returns);
					}

					public void Callback(in CallbackDelegate callback)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(callback);
					}

					public void Throws(in System.Exception exception)
					{
						if (_currentSetup is null)
							throw new System.InvalidOperationException("Parameters are not set, call SetupParameters first!");

						_currentSetup.Add(exception);
					}

					ISetupReturnsThrowsJoin<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in TReturns returns)
					{
						Returns(returns);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetup<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Returns(in ReturnsCallbackDelegate returns)
					{
						Returns(returns);
						return this;
					}

					ISetupCallbackJoin<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupCallbackStart<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetup<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupCallbackReset<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Callback(in CallbackDelegate callback)
					{
						Callback(callback);
						return this;
					}

					ISetupReturnsThrowsJoin<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsStart<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetup<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsReset<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.Throws(in System.Exception exception)
					{
						Throws(exception);
						return this;
					}

					ISetupCallbackReset<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupReturnsThrowsJoin<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					ISetupReturnsThrowsReset<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate> ISetupCallbackJoin<SetupArray1Array1T1Int32<T1, TReturns>.CallbackDelegate, TReturns, SetupArray1Array1T1Int32<T1, TReturns>.ReturnsCallbackDelegate>.And()
					{
						_currentSetup?.AndContinue = true;
						return this;
					}

					private sealed class Item
					{
						private readonly System.Collections.Generic.Queue<ItemSetup> _queue = [];
						private ItemSetup? _currentSetup;
						public bool AndContinue;

						public readonly ItSetup<T1[][]>? Parameter1;
						public readonly ItSetup<int>? Parameter2;

						public Item(in ItSetup<T1[][]>? parameter1, in ItSetup<int>? parameter2)
						{
							Parameter1 = parameter1;
							Parameter2 = parameter2;
						}

						public void Add(in CallbackDelegate callback)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Callback = callback;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(callback: callback);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in ReturnsCallbackDelegate returns)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Returns = returns;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(returns: returns);
								_queue.Enqueue(_currentSetup);
							}
						}

						public void Add(in System.Exception exception)
						{
							if (AndContinue && _currentSetup is not null)
							{
								_currentSetup.Exception = exception;
								AndContinue = false;
								_currentSetup = null;
							}
							else
							{
								_currentSetup = new ItemSetup(exception: exception);
								_queue.Enqueue(_currentSetup);
							}
						}

						public ItemSetup GetSetup()
						{
							return _queue.Count switch
							{
								0 => ItemSetup.Default,
								1 => _queue.Peek(),
								_ => _queue.Dequeue(),
							};
						}
					}

					private sealed class ItemSetup
					{
						public static readonly ItemSetup Default = new();

						public CallbackDelegate? Callback;
						public System.Exception? Exception;
						public ReturnsCallbackDelegate? Returns;

						public ItemSetup(in ReturnsCallbackDelegate? returns = null, in CallbackDelegate? callback = null, in System.Exception? exception = null)
						{
							Returns = returns;
							Callback = callback;
							Exception = exception;
						}
					}

					private sealed class Comparer: System.Collections.Generic.IComparer<Item>
					{
						public int Compare(Item? x, Item? y)
						{
							var xSort = 0;
							var ySort = 0;

							if (x is not null)
							{
								if (x.Parameter1.HasValue)
									xSort += x.Parameter1.Value.Sort;
								if (x.Parameter2.HasValue)
									xSort += x.Parameter2.Value.Sort;
							}

							if (y is not null)
							{
								if (y.Parameter1.HasValue)
									ySort += y.Parameter1.Value.Sort;
								if (y.Parameter2.HasValue)
									ySort += y.Parameter2.Value.Sort;
							}

							return xSort.CompareTo(ySort);
						}
					}
				}
				"""
			),
			(
				"InvocationArray1Array1T1Int32_T1_.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class InvocationArray1Array1T1Int32<T1> : IInvocationVerify
				{
					private readonly string _name;
					private readonly string? _parameter1Prefix, _parameter2Prefix;
					private readonly InvocationContainer<Item> _invocations = [];

					public InvocationArray1Array1T1Int32(string name, string? parameter1Prefix = null, string? parameter2Prefix = null)
					{
						_name = name;
						_parameter1Prefix = parameter1Prefix;
						_parameter2Prefix = parameter2Prefix;
					}

					public void Register(in InvocationIndex.Counter index, T1[][] parameter1, int parameter2)
					{
						var invokedIndex = index.Increment();
						_invocations.Add(new Item(invokedIndex, parameter1, parameter2, invocation: this));
					}

					public void Verify(in ItSetup<T1[][]> parameter1, in ItSetup<int> parameter2, in Times times, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpan();

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						var count = 0;
						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							count++;
						}

						if (times.Predicate(count))
							return;

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifyCountException(verifyName, times, count, invocations);
					}

					public long Verify(in ItSetup<T1[][]> parameter1, in ItSetup<int> parameter2, long index, System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var span = _invocations.GetItemsSpanFrom(index);

						var verifyOutput = new System.Collections.Generic.List<(Item, (string, ComparisonResult?)[]?)>();
						System.Runtime.InteropServices.CollectionsMarshal.SetCount(verifyOutput, span.Length);

						for (var i = 0; i < span.Length; i++)
						{
							var verifyParameter1 = span[i].GetParameter1(parameter1.Type);
							var verifyParameter2 = span[i].GetParameter2(parameter2.Type);
							(string, ComparisonResult?)[]? verifyResults = null;

							if (!parameter1.Check(verifyParameter1, out var result))
							{
								verifyResults = [("parameter1", result)];
							}
							if (!parameter2.Check(verifyParameter2, out result))
							{
								verifyResults = verifyResults is not null
									? [..verifyResults, ("parameter2", result)]
									: [("parameter2", result)];
							}

							if (verifyResults is not null)
							{
								verifyOutput[i] = (span[i], verifyResults);
								continue;
							}

							verifyOutput[i] = (span[i], null);
							span[i].IsVerified = true;
							return span[i].Index + 1;
						}

						if (invocationProviders is null)
						{
							span = _invocations.GetItemsSpanBefore(index);
							for (var i = 0; i < span.Length; i++)
								verifyOutput.Insert(i, (span[i], null));
						}

						var invocations = verifyOutput.GetStrings(invocationProviders);
						var verifyName = string.Format(_name, parameter1.ToString(_parameter1Prefix), parameter2.ToString(_parameter2Prefix));
						throw new MockVerifySequenceOutOfRangeException(verifyName, index, invocations);
					}

					public void VerifyNoOtherCalls(System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>>? invocationProviders = null)
					{
						var unverifiedItems = _invocations.GetUnverifiedInvocations(invocationProviders);
						if (unverifiedItems is null)
							return;

						var typeNameParameter1 = !string.IsNullOrEmpty(_parameter1Prefix) ? $"{_parameter1Prefix} {typeof(T1).Name}[][]" : $"{typeof(T1).Name}[][]";
						var typeNameParameter2 = !string.IsNullOrEmpty(_parameter2Prefix) ? $"{_parameter2Prefix} int" : "int";
						var verifyName = string.Format(_name, typeNameParameter1, typeNameParameter2);
						throw new MockUnverifiedException(verifyName, unverifiedItems);
					}

					public System.Collections.Generic.IEnumerable<IInvocation> GetInvocations()
					{
						return _invocations;
					}

					public System.Collections.Generic.IEnumerable<IInvocation<(T1[][] parameter1, int parameter2)>> GetInvocationsWithArguments()
					{
						return _invocations;
					}

					private sealed class Item : IInvocation<(T1[][] parameter1, int parameter2)>
					{
						private readonly (T1[][] parameter1, int parameter2) _argument;
						private readonly string? _jsonSnapshotParameter1, _jsonSnapshotParameter2;
						private readonly InvocationArray1Array1T1Int32<T1> _invocation;

						public Item(long index, T1[][] parameter1, int parameter2, InvocationArray1Array1T1Int32<T1> invocation)
						{
							_argument = (parameter1, parameter2);
							_invocation = invocation;
							Index = index;

							try
							{
								_jsonSnapshotParameter1 = parameter1.ToJsonString();
							}
							catch
							{
								// Swallow
							}

							try
							{
								_jsonSnapshotParameter2 = parameter2.ToJsonString();
							}
							catch
							{
								// Swallow
							}
						}

						public long Index { get; }

						public bool IsVerified { get; set; }

						public (T1[][] parameter1, int parameter2) Arguments => _argument;

						public T1[][] GetParameter1(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter1)
								? System.Text.Json.JsonSerializer.Deserialize<T1[][]>(_jsonSnapshotParameter1)!
								: _argument.parameter1;
						}

						public int GetParameter2(SetupType setupType)
						{
							return setupType == SetupType.Equivalent && !string.IsNullOrEmpty(_jsonSnapshotParameter2)
								? System.Text.Json.JsonSerializer.Deserialize<int>(_jsonSnapshotParameter2)!
								: _argument.parameter2;
						}

						public override string ToString()
						{
							var stringBuilder = new System.Text.StringBuilder();

							// parameter1
							if (!string.IsNullOrEmpty(_invocation._parameter1Prefix))
								stringBuilder.Append($"{_invocation._parameter1Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter1))
								stringBuilder.Append(_jsonSnapshotParameter1);
							else
								stringBuilder.Append(_argument.parameter1);
							var parameter1 = stringBuilder.ToString();

							// parameter2
							stringBuilder.Clear();
							if (!string.IsNullOrEmpty(_invocation._parameter2Prefix))
								stringBuilder.Append($"{_invocation._parameter2Prefix} ");
							if (!string.IsNullOrEmpty(_jsonSnapshotParameter2))
								stringBuilder.Append(_jsonSnapshotParameter2);
							else
								stringBuilder.Append(_argument.parameter2);
							var parameter2 = stringBuilder.ToString();

							var stringValue = string.Format(_invocation._name, parameter1, parameter2);
							return $"{Index}: {stringValue}";
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
