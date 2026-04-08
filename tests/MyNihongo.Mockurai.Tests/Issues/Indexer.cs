namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class Indexer : TestsBase
{
	[Fact]
	public async Task GenerateIndexerInterface()
	{
		const string testCode =
			"""
			namespace MyNihongo.Indexer.Tests;

			public interface IInterface
			{
				string? this[string key] { get; set; }
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
			}
			""";

		var types = new TypeModel[]
		{
			new("String", "key"),
			new("String", "value", isNullable: true),
		};

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Indexer.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial IMock<MyNihongo.Indexer.Tests.IInterface> InterfaceMock => _interfaceMock;

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
						public readonly IMockSequence<MyNihongo.Indexer.Tests.IInterface> InterfaceMock;

						public VerifySequenceContext(IMock<MyNihongo.Indexer.Tests.IInterface> interfaceMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Indexer.Tests.IInterface>
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

					// this[]
					private SetupWithParameter<string, string?>? _indexer0Get;
					private Invocation<string>? _indexer0GetInvocation;
					private SetupStringString? _indexer0Set;
					private InvocationStringString? _indexer0SetInvocation;

					public SetupWithParameter<string, string?> SetupGetIndexer(in It<string> key)
					{
						_indexer0Get ??= new SetupWithParameter<string, string?>();
						_indexer0Get.SetupParameter(key.ValueSetup);
						return _indexer0Get;
					}

					public void VerifyGetIndexer(in It<string> key, in Times times)
					{
						_indexer0GetInvocation ??= new Invocation<string>("IInterface.This[{0}].get");
						_indexer0GetInvocation.Verify(key.ValueSetup, times, _invocationProviders);
					}

					public long VerifyGetIndexer(in It<string> key, long index)
					{
						_indexer0GetInvocation ??= new Invocation<string>("IInterface.This[{0}].get");
						return _indexer0GetInvocation.Verify(key.ValueSetup, index, _invocationProviders);
					}

					public SetupStringString SetupSetIndexer(in It<string> key, in It<string?> value)
					{
						_indexer0Set ??= new SetupStringString();
						_indexer0Set.SetupParameters(key.ValueSetup, value.ValueSetup);
						return _indexer0Set;
					}

					public void VerifySetIndexer(in It<string> key, in It<string?> value, in Times times)
					{
						_indexer0SetInvocation ??= new InvocationStringString("IInterface.This[{0}].set = {1}");
						_indexer0SetInvocation.Verify(key.ValueSetup, value.ValueSetup, times, _invocationProviders);
					}

					public long VerifySetIndexer(in It<string> key, in It<string?> value, long index)
					{
						_indexer0SetInvocation ??= new InvocationStringString("IInterface.This[{0}].set = {1}");
						return _indexer0SetInvocation.Verify(key.ValueSetup, value.ValueSetup, index, _invocationProviders);
					}

					public void VerifyNoOtherCalls()
					{
						_indexer0GetInvocation?.VerifyNoOtherCalls(_invocationProviders);
						_indexer0SetInvocation?.VerifyNoOtherCalls(_invocationProviders);
					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield return _indexer0GetInvocation;
						yield return _indexer0SetInvocation;
					}

					private sealed class Proxy : MyNihongo.Indexer.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public string? this[string key]
						{
							get
							{
								_mock._indexer0GetInvocation ??= new Invocation<string>("IInterface.This[{0}].get");
								_mock._indexer0GetInvocation.Register(_mock._invocationIndex, key);
								return _mock._indexer0Get?.Execute(key, out var returnValue) == true ? returnValue! : default!;
							}
							set
							{
								_mock._indexer0SetInvocation ??= new InvocationStringString("IInterface.This[{0}].set = {1}");
								_mock._indexer0SetInvocation.Register(_mock._invocationIndex, key, value);
								_mock._indexer0Set?.Invoke(key, value);
							}
						}
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<MyNihongo.Indexer.Tests.IInterface> @this)
					{
						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();

						// this[]
						public ISetup<System.Action<string>, string?, System.Func<string, string?>> SetupGetIndexer(in It<string> key = default) =>
							((InterfaceMock)@this).SetupGetIndexer(key);

						public void VerifyGetIndexer(in It<string> key, in Times times) =>
							((InterfaceMock)@this).VerifyGetIndexer(key, times);

						public void VerifyGetIndexer(in It<string> key, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyGetIndexer(key, times());

						public ISetup<SetupStringString.CallbackDelegate> SetupSetIndexer(in It<string> key = default, in It<string?> value = default) =>
							((InterfaceMock)@this).SetupSetIndexer(key, value);

						public void VerifySetIndexer(in It<string> key, in It<string?> value, in Times times) =>
							((InterfaceMock)@this).VerifySetIndexer(key, value, times);

						public void VerifySetIndexer(in It<string> key, in It<string?> value, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifySetIndexer(key, value, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<MyNihongo.Indexer.Tests.IInterface> @this)
					{
						// this[]
						public void GetIndexer(in It<string> key)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyGetIndexer(key, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}

						public void SetIndexer(in It<string> key, in It<string?> value)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifySetIndexer(key, value, @this.VerifyIndex);
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
}
