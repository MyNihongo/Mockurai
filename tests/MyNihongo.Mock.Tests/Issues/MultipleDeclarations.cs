namespace MyNihongo.Mock.Tests.Issues;

public sealed class MultipleDeclarations : TestsBase
{
	[Fact]
	public async Task NotGenerateDuplicateMocksFromSameClass()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mock.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock1 { get; }
				protected partial IMock<IInterface> InterfaceMock2 { get; }
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
					// InterfaceMock1
					private readonly InterfaceMock _interfaceMock1 = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock1 => _interfaceMock1;

					// InterfaceMock2
					private readonly InterfaceMock _interfaceMock2 = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock2 => _interfaceMock2;

					protected void VerifyNoOtherCalls()
					{
						InterfaceMock1.VerifyNoOtherCalls();
						InterfaceMock2.VerifyNoOtherCalls();
					}

					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interfaceMock1: InterfaceMock1,
							interfaceMock2: InterfaceMock2
						);

						verify(ctx);
					}

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock1;
						public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock2;

						public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock1, MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock2)
						{
							InterfaceMock1 = new MockSequence<MyNihongo.Mock.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock1,
							};
							InterfaceMock2 = new MockSequence<MyNihongo.Mock.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock2,
							};
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

					private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
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

	[Fact]
	public async Task NotGenerateDuplicateMocksFromDifferentClasses()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mock.Tests;

			public interface IInterface
			{
				void Invoke();
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase1
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase2
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase1.g.cs",
				"""
				namespace MyNihongo.Mock.Tests;

				public partial class TestsBase1
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock => _interfaceMock;

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
						public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mock.Tests.IInterface>
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
				"TestsBase2.g.cs",
				"""
				namespace MyNihongo.Mock.Tests;

				public partial class TestsBase2
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock => _interfaceMock;

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
						public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mock.Tests.IInterface>
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

					private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
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

	[Fact]
	public async Task NotGenerateDuplicateMethodSetupAndInvocationFromSameClass()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mock.Tests;

			public interface IInterface
			{
				T Invoke<T>(int param1, long param2);
				decimal Invoke2(int param2, long param1);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock1 { get; }
			}
			""";

		string[] types = ["Int32", "Int64"];
		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				namespace MyNihongo.Mock.Tests;

				public partial class TestsBase
				{
					// InterfaceMock1
					private readonly InterfaceMock _interfaceMock1 = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock1 => _interfaceMock1;

					protected void VerifyNoOtherCalls()
					{
						InterfaceMock1.VerifyNoOtherCalls();
					}

					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interfaceMock1: InterfaceMock1
						);

						verify(ctx);
					}

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock1;

						public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock1)
						{
							InterfaceMock1 = new MockSequence<MyNihongo.Mock.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock1,
							};
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

					// Invoke
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupInt32Int64<T> SetupInvoke<T>(in It<int> param1, in It<long> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupInt32Int64<T>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupInt32Int64<T>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<T>(in It<int> param1, in It<long> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationInt32Int64)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32Int64($"IInterface.Invoke<{key.Name}>({0}, {1})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<T>(in It<int> param1, in It<long> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationInt32Int64)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32Int64($"IInterface.Invoke<{key.Name}>({0}, {1})"));
						return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
					}

					// Invoke2
					private SetupInt32Int64<decimal>? _invoke20;
					private InvocationInt32Int64? _invoke20Invocation;

					public SetupInt32Int64<decimal> SetupInvoke2(in It<int> param2, in It<long> param1)
					{
						_invoke20 ??= new SetupInt32Int64<decimal>();
						_invoke20.SetupParameters(param2.ValueSetup, param1.ValueSetup);
						return _invoke20;
					}

					public void VerifyInvoke2(in It<int> param2, in It<long> param1, in Times times)
					{
						_invoke20Invocation ??= new InvocationInt32Int64("IInterface.Invoke2({0}, {1})");
						_invoke20Invocation.Verify(param2.ValueSetup, param1.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke2(in It<int> param2, in It<long> param1, long index)
					{
						_invoke20Invocation ??= new InvocationInt32Int64("IInterface.Invoke2({0}, {1})");
						return _invoke20Invocation.Verify(param2.ValueSetup, param1.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_invoke20Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
						yield return _invoke20Invocation;
					}

					private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public T Invoke<T>(int param1, long param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationInt32Int64)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32Int64($"IInterface.Invoke<{key.Name}>({0}, {1})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							return ((SetupInt32Int64<T>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
						}

						public decimal Invoke2(int param2, long param1)
						{
							_mock._invoke20Invocation ??= new InvocationInt32Int64("IInterface.Invoke2({0}, {1})");
							_mock._invoke20Invocation.Register(_mock._invocationIndex, param2, param1);
							return _mock._invoke20?.Execute(param2, param1, out var returnValue) == true ? returnValue! : default!;
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
			CreateSetupReturnsCode(types),
			CreateInvocationCode(types),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateDifferentMethodSetupAndInvocationIfSequenceDifferent()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mock.Tests;

			public interface IInterface
			{
				T Invoke<T>(int param1, long param2);
				decimal Invoke2(long param2, int param1);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock1 { get; }
			}
			""";

		string[] types1 = ["Int32", "Int64"];
		TypeModel[] types2 = [new("Int64", 2), new("Int32", 1)];
		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				namespace MyNihongo.Mock.Tests;

				public partial class TestsBase
				{
					// InterfaceMock1
					private readonly InterfaceMock _interfaceMock1 = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock1 => _interfaceMock1;

					protected void VerifyNoOtherCalls()
					{
						InterfaceMock1.VerifyNoOtherCalls();
					}

					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interfaceMock1: InterfaceMock1
						);

						verify(ctx);
					}

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock1;

						public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock1)
						{
							InterfaceMock1 = new MockSequence<MyNihongo.Mock.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock1,
							};
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

					// Invoke
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupInt32Int64<T> SetupInvoke<T>(in It<int> param1, in It<long> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupInt32Int64<T>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupInt32Int64<T>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<T>(in It<int> param1, in It<long> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationInt32Int64)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32Int64($"IInterface.Invoke<{key.Name}>({0}, {1})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<T>(in It<int> param1, in It<long> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationInt32Int64)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32Int64($"IInterface.Invoke<{key.Name}>({0}, {1})"));
						return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
					}

					// Invoke2
					private SetupInt64Int32<decimal>? _invoke20;
					private InvocationInt64Int32? _invoke20Invocation;

					public SetupInt64Int32<decimal> SetupInvoke2(in It<long> param2, in It<int> param1)
					{
						_invoke20 ??= new SetupInt64Int32<decimal>();
						_invoke20.SetupParameters(param2.ValueSetup, param1.ValueSetup);
						return _invoke20;
					}

					public void VerifyInvoke2(in It<long> param2, in It<int> param1, in Times times)
					{
						_invoke20Invocation ??= new InvocationInt64Int32("IInterface.Invoke2({0}, {1})");
						_invoke20Invocation.Verify(param2.ValueSetup, param1.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke2(in It<long> param2, in It<int> param1, long index)
					{
						_invoke20Invocation ??= new InvocationInt64Int32("IInterface.Invoke2({0}, {1})");
						return _invoke20Invocation.Verify(param2.ValueSetup, param1.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_invoke20Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
						yield return _invoke20Invocation;
					}

					private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public T Invoke<T>(int param1, long param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationInt32Int64)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32Int64($"IInterface.Invoke<{key.Name}>({0}, {1})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							return ((SetupInt32Int64<T>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
						}

						public decimal Invoke2(long param2, int param1)
						{
							_mock._invoke20Invocation ??= new InvocationInt64Int32("IInterface.Invoke2({0}, {1})");
							_mock._invoke20Invocation.Register(_mock._invocationIndex, param2, param1);
							return _mock._invoke20?.Execute(param2, param1, out var returnValue) == true ? returnValue! : default!;
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
			CreateSetupReturnsCode(types1),
			CreateInvocationCode(types1),
			CreateSetupReturnsCode(types2),
			CreateInvocationCode(types2),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateDifferentMethodSetupAndInvocationIfRefDifferent()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mock.Tests;

			public interface IInterface
			{
				T Invoke<T>(int param1, long param2);
				decimal Invoke2(int param1, in long param2);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock1 { get; }
			}
			""";

		string[] types1 = ["Int32", "Int64"];
		TypeModel[] types2 = [new("Int32", 1), new("Int64", 2, "in")];
		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				namespace MyNihongo.Mock.Tests;

				public partial class TestsBase
				{
					// InterfaceMock1
					private readonly InterfaceMock _interfaceMock1 = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock1 => _interfaceMock1;

					protected void VerifyNoOtherCalls()
					{
						InterfaceMock1.VerifyNoOtherCalls();
					}

					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interfaceMock1: InterfaceMock1
						);

						verify(ctx);
					}

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock1;

						public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock1)
						{
							InterfaceMock1 = new MockSequence<MyNihongo.Mock.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock1,
							};
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

					// Invoke
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupInt32Int64<T> SetupInvoke<T>(in It<int> param1, in It<long> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupInt32Int64<T>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupInt32Int64<T>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<T>(in It<int> param1, in It<long> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationInt32Int64)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32Int64($"IInterface.Invoke<{key.Name}>({0}, {1})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<T>(in It<int> param1, in It<long> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationInt32Int64)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32Int64($"IInterface.Invoke<{key.Name}>({0}, {1})"));
						return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
					}

					// Invoke2
					private SetupInt32InInt64<decimal>? _invoke20;
					private InvocationInt32InInt64? _invoke20Invocation;

					public SetupInt32InInt64<decimal> SetupInvoke2(in It<int> param1, in ItIn<long> param2)
					{
						_invoke20 ??= new SetupInt32InInt64<decimal>();
						_invoke20.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return _invoke20;
					}

					public void VerifyInvoke2(in It<int> param1, in ItIn<long> param2, in Times times)
					{
						_invoke20Invocation ??= new InvocationInt32InInt64("IInterface.Invoke2({0}, {1})", prefixParam2: "in");
						_invoke20Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke2(in It<int> param1, in ItIn<long> param2, long index)
					{
						_invoke20Invocation ??= new InvocationInt32InInt64("IInterface.Invoke2({0}, {1})", prefixParam2: "in");
						return _invoke20Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_invoke20Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
						yield return _invoke20Invocation;
					}

					private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public T Invoke<T>(int param1, long param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationInt32Int64)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32Int64($"IInterface.Invoke<{key.Name}>({0}, {1})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							return ((SetupInt32Int64<T>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
						}

						public decimal Invoke2(int param1, in long param2)
						{
							_mock._invoke20Invocation ??= new InvocationInt32InInt64("IInterface.Invoke2({0}, {1})", prefixParam2: "in");
							_mock._invoke20Invocation.Register(_mock._invocationIndex, param1, param2);
							return _mock._invoke20?.Execute(param1, in param2, out var returnValue) == true ? returnValue! : default!;
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
			CreateSetupReturnsCode(types1),
			CreateInvocationCode(types1),
			CreateSetupReturnsCode(types2),
			CreateInvocationCode(types2),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task NotGenerateDuplicateMethodSetupAndInvocationFromDifferentClasses()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mock.Tests;

			public interface IInterface
			{
				void Invoke(int param1, double param2);
			}

			public abstract class AbstractClass
			{
				public abstract void Invoke(int value1, double value2);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
				protected partial IMock<AbstractClass> AbstractClassMock { get; }
			}
			""";

		string[] types = ["Int32", "Double"];
		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				namespace MyNihongo.Mock.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock => _interfaceMock;

					// AbstractClassMock
					private readonly AbstractClassMock _abstractClassMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.AbstractClass> AbstractClassMock => _abstractClassMock;

					protected void VerifyNoOtherCalls()
					{
						InterfaceMock.VerifyNoOtherCalls();
						AbstractClassMock.VerifyNoOtherCalls();
					}

					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interfaceMock: InterfaceMock,
							abstractClassMock: AbstractClassMock
						);

						verify(ctx);
					}

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock;
						public readonly IMockSequence<MyNihongo.Mock.Tests.AbstractClass> AbstractClassMock;

						public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock, MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.AbstractClass> abstractClassMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mock.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock,
							};
							AbstractClassMock = new MockSequence<MyNihongo.Mock.Tests.AbstractClass>
							{
								VerifyIndex = _verifyIndex,
								Mock = abstractClassMock,
							};
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

					// Invoke
					private SetupInt32Double? _invoke0;
					private InvocationInt32Double? _invoke0Invocation;

					public SetupInt32Double SetupInvoke(in It<int> param1, in It<double> param2)
					{
						_invoke0 ??= new SetupInt32Double();
						_invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return _invoke0;
					}

					public void VerifyInvoke(in It<int> param1, in It<double> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationInt32Double("IInterface.Invoke({0}, {1})");
						_invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke(in It<int> param1, in It<double> param2, long index)
					{
						_invoke0Invocation ??= new InvocationInt32Double("IInterface.Invoke({0}, {1})");
						return _invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
					}

					private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public void Invoke(int param1, double param2)
						{
							_mock._invoke0Invocation ??= new InvocationInt32Double("IInterface.Invoke({0}, {1})");
							_mock._invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							_mock._invoke0?.Invoke(param1, param2);
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
			(
				"AbstractClassMock.g.cs",
				"""
				namespace MyNihongo.Mock;

				public sealed class AbstractClassMock : IMock<MyNihongo.Mock.Tests.AbstractClass>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public AbstractClassMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public MyNihongo.Mock.Tests.AbstractClass Object => _proxy ??= new Proxy(this);

					// Invoke
					private SetupInt32Double? _invoke0;
					private InvocationInt32Double? _invoke0Invocation;

					public SetupInt32Double SetupInvoke(in It<int> value1, in It<double> value2)
					{
						_invoke0 ??= new SetupInt32Double();
						_invoke0.SetupParameters(value1.ValueSetup, value2.ValueSetup);
						return _invoke0;
					}

					public void VerifyInvoke(in It<int> value1, in It<double> value2, in Times times)
					{
						_invoke0Invocation ??= new InvocationInt32Double("AbstractClass.Invoke({0}, {1})");
						_invoke0Invocation.Verify(value1.ValueSetup, value2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke(in It<int> value1, in It<double> value2, long index)
					{
						_invoke0Invocation ??= new InvocationInt32Double("AbstractClass.Invoke({0}, {1})");
						return _invoke0Invocation.Verify(value1.ValueSetup, value2.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
					}

					private sealed class Proxy : MyNihongo.Mock.Tests.AbstractClass
					{
						private readonly AbstractClassMock _mock;

						public Proxy(AbstractClassMock mock)
						{
							_mock = mock;
						}

						public override void Invoke(int value1, double value2)
						{
							_mock._invoke0Invocation ??= new InvocationInt32Double("AbstractClass.Invoke({0}, {1})");
							_mock._invoke0Invocation.Register(_mock._invocationIndex, value1, value2);
							_mock._invoke0?.Invoke(value1, value2);
						}
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<MyNihongo.Mock.Tests.AbstractClass> @this)
					{
						public void VerifyNoOtherCalls() =>
							((AbstractClassMock)@this).VerifyNoOtherCalls();

						
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<MyNihongo.Mock.Tests.AbstractClass> @this)
					{
					
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
	public async Task NotGenerateDuplicateMethodSetupAndInvocationForSameGenericTypes()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mock.Tests;

			public interface IInterface
			{
				void Invoke<TStuff>(int param1, TStuff param2);
				void Invoke2<TValue>(int param1, TValue param2);
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
				namespace MyNihongo.Mock.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock => _interfaceMock;

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
						public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mock.Tests.IInterface>
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

					// Invoke
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupInt32T1<TStuff> SetupInvoke<TStuff>(in It<int> param1, in It<TStuff> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupInt32T1<TStuff>)_invoke0.GetOrAdd(typeof(TStuff), static _ => new SetupInt32T1<TStuff>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<TStuff>(in It<int> param1, in It<TStuff> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationInt32T1<TStuff>)_invoke0Invocation.GetOrAdd(typeof(TStuff), static key => new InvocationInt32T1<TStuff>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<TStuff>(in It<int> param1, in It<TStuff> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationInt32T1<TStuff>)_invoke0Invocation.GetOrAdd(typeof(TStuff), static key => new InvocationInt32T1<TStuff>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
						return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
					}

					// Invoke2
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke20;
					private InvocationDictionary? _invoke20Invocation;

					public SetupInt32T1<TValue> SetupInvoke2<TValue>(in It<int> param1, in It<TValue> param2)
					{
						_invoke20 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke20 = (SetupInt32T1<TValue>)_invoke20.GetOrAdd(typeof(TValue), static _ => new SetupInt32T1<TValue>());
						invoke20.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke20;
					}

					public void VerifyInvoke2<TValue>(in It<int> param1, in It<TValue> param2, in Times times)
					{
						_invoke20Invocation ??= new InvocationDictionary();
						var invoke20Invocation = (InvocationInt32T1<TValue>)_invoke20Invocation.GetOrAdd(typeof(TValue), static key => new InvocationInt32T1<TValue>($"IInterface.Invoke2<{key.Name}>({0}, {1})"));
						invoke20Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke2<TValue>(in It<int> param1, in It<TValue> param2, long index)
					{
						_invoke20Invocation ??= new InvocationDictionary();
						var invoke20Invocation = (InvocationInt32T1<TValue>)_invoke20Invocation.GetOrAdd(typeof(TValue), static key => new InvocationInt32T1<TValue>($"IInterface.Invoke2<{key.Name}>({0}, {1})"));
						return invoke20Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_invoke20Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
						yield return _invoke20Invocation;
					}

					private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public void Invoke<TStuff>(int param1, TStuff param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationInt32T1<TStuff>)_mock._invoke0Invocation.GetOrAdd(typeof(TStuff), static key => new InvocationInt32T1<TStuff>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							((SetupInt32T1<TStuff>?)_mock._invoke0?.ValueOrDefault(typeof(TStuff)))?.Invoke(param1, param2);
						}

						public void Invoke2<TValue>(int param1, TValue param2)
						{
							_mock._invoke20Invocation ??= new InvocationDictionary();
							var invoke20Invocation = (InvocationInt32T1<TValue>)_mock._invoke20Invocation.GetOrAdd(typeof(TValue), static key => new InvocationInt32T1<TValue>($"IInterface.Invoke2<{key.Name}>({0}, {1})"));
							invoke20Invocation.Register(_mock._invocationIndex, param1, param2);
							((SetupInt32T1<TValue>?)_mock._invoke20?.ValueOrDefault(typeof(TValue)))?.Invoke(param1, param2);
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
			CreateSetupCode(types),
			CreateInvocationCode(types),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateDifferentMethodSetupAndInvocationForGenericTypesWithDifferentOrder()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mock.Tests;

			public interface IInterface
			{
				void Invoke<TStuff>(int param1, TStuff param2);
				void Invoke2<TValue>(TValue param1, int param2);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		var types1 = new TypeModel[]
		{
			new("Int32", 1),
			new("T1", 2, isGeneric: true),
		};

		var types2 = new TypeModel[]
		{
			new("T1", 1, isGeneric: true),
			new("Int32", 2),
		};

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				namespace MyNihongo.Mock.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock => _interfaceMock;

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
						public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mock.Tests.IInterface>
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

					// Invoke
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
					private InvocationDictionary? _invoke0Invocation;

					public SetupInt32T1<TStuff> SetupInvoke<TStuff>(in It<int> param1, in It<TStuff> param2)
					{
						_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke0 = (SetupInt32T1<TStuff>)_invoke0.GetOrAdd(typeof(TStuff), static _ => new SetupInt32T1<TStuff>());
						invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke0;
					}

					public void VerifyInvoke<TStuff>(in It<int> param1, in It<TStuff> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationInt32T1<TStuff>)_invoke0Invocation.GetOrAdd(typeof(TStuff), static key => new InvocationInt32T1<TStuff>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
						invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke<TStuff>(in It<int> param1, in It<TStuff> param2, long index)
					{
						_invoke0Invocation ??= new InvocationDictionary();
						var invoke0Invocation = (InvocationInt32T1<TStuff>)_invoke0Invocation.GetOrAdd(typeof(TStuff), static key => new InvocationInt32T1<TStuff>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
						return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
					}

					// Invoke2
					private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke20;
					private InvocationDictionary? _invoke20Invocation;

					public SetupT1Int32<TValue> SetupInvoke2<TValue>(in It<TValue> param1, in It<int> param2)
					{
						_invoke20 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
						var invoke20 = (SetupT1Int32<TValue>)_invoke20.GetOrAdd(typeof(TValue), static _ => new SetupT1Int32<TValue>());
						invoke20.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return invoke20;
					}

					public void VerifyInvoke2<TValue>(in It<TValue> param1, in It<int> param2, in Times times)
					{
						_invoke20Invocation ??= new InvocationDictionary();
						var invoke20Invocation = (InvocationT1Int32<TValue>)_invoke20Invocation.GetOrAdd(typeof(TValue), static key => new InvocationT1Int32<TValue>($"IInterface.Invoke2<{key.Name}>({0}, {1})"));
						invoke20Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke2<TValue>(in It<TValue> param1, in It<int> param2, long index)
					{
						_invoke20Invocation ??= new InvocationDictionary();
						var invoke20Invocation = (InvocationT1Int32<TValue>)_invoke20Invocation.GetOrAdd(typeof(TValue), static key => new InvocationT1Int32<TValue>($"IInterface.Invoke2<{key.Name}>({0}, {1})"));
						return invoke20Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);
						_invoke20Invocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _invoke0Invocation;
						yield return _invoke20Invocation;
					}

					private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public void Invoke<TStuff>(int param1, TStuff param2)
						{
							_mock._invoke0Invocation ??= new InvocationDictionary();
							var invoke0Invocation = (InvocationInt32T1<TStuff>)_mock._invoke0Invocation.GetOrAdd(typeof(TStuff), static key => new InvocationInt32T1<TStuff>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
							invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							((SetupInt32T1<TStuff>?)_mock._invoke0?.ValueOrDefault(typeof(TStuff)))?.Invoke(param1, param2);
						}

						public void Invoke2<TValue>(TValue param1, int param2)
						{
							_mock._invoke20Invocation ??= new InvocationDictionary();
							var invoke20Invocation = (InvocationT1Int32<TValue>)_mock._invoke20Invocation.GetOrAdd(typeof(TValue), static key => new InvocationT1Int32<TValue>($"IInterface.Invoke2<{key.Name}>({0}, {1})"));
							invoke20Invocation.Register(_mock._invocationIndex, param1, param2);
							((SetupT1Int32<TValue>?)_mock._invoke20?.ValueOrDefault(typeof(TValue)))?.Invoke(param1, param2);
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
			CreateSetupCode(types1),
			CreateInvocationCode(types1),
			CreateSetupCode(types2),
			CreateInvocationCode(types2),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateMultipleSetupsAndOneInvocation()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mock.Tests;

			public interface IInterface
			{
				void Invoke(int param1, int param2);
				decimal Invoke(int param1, int param2);
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
				namespace MyNihongo.Mock.Tests;

				public partial class TestsBase
				{
					// InterfaceMock1
					private readonly InterfaceMock _interfaceMock1 = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock1 => _interfaceMock1;

					// InterfaceMock2
					private readonly InterfaceMock _interfaceMock2 = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock2 => _interfaceMock2;

					protected void VerifyNoOtherCalls()
					{
						InterfaceMock1.VerifyNoOtherCalls();
						InterfaceMock2.VerifyNoOtherCalls();
					}

					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interfaceMock1: InterfaceMock1,
							interfaceMock2: InterfaceMock2
						);

						verify(ctx);
					}

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock1;
						public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock2;

						public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock1, MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock2)
						{
							InterfaceMock1 = new MockSequence<MyNihongo.Mock.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock1,
							};
							InterfaceMock2 = new MockSequence<MyNihongo.Mock.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock2,
							};
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

					private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
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
}
