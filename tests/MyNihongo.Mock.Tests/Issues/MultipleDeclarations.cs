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

					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield break;
					}

					private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public void Invoke() {}

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

					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield break;
					}

					private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public void Invoke() {}

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
				decimal Invoke2(int param1, long param2);
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

					public SetupInt32Int64<decimal> SetupInvoke2(in It<int> param1, in It<long> param2)
					{
						_invoke20 ??= new SetupInt32Int64<decimal>();
						_invoke20.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return _invoke20;
					}

					public void VerifyInvoke2(in It<int> param1, in It<long> param2, in Times times)
					{
						_invoke20Invocation ??= new InvocationInt32Int64("IInterface.Invoke2({0}, {1})");
						_invoke20Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke2(in It<int> param1, in It<long> param2, long index)
					{
						_invoke20Invocation ??= new InvocationInt32Int64("IInterface.Invoke2({0}, {1})");
						return _invoke20Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{

					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield break;
					}

					private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public T Invoke<T>(int param1, long param2) {return default;}
						public decimal Invoke2(int param1, long param2) {return default;}

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

		throw new NotImplementedException();
	}
}
