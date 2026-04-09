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
}
