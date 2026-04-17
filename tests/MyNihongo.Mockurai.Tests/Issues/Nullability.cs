namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class Nullability : TestsBase
{
	[Fact]
	public async Task GenerateNullableProperties()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

			public interface IInterface
			{
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected IMock<IInterface>? InterfaceMock { get; }
			}
			""";

		GeneratedSources generatedSources =
		[
			(
				"TestsBase.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai.Tests;

				public partial class TestsBase
				{

					protected void VerifyNoOtherCalls()
					{
						InterfaceMock?.VerifyNoOtherCalls();
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
						public readonly IMockSequence<MyNihongo.Mockurai.Tests.IInterface>? InterfaceMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface>? interfaceMock)
						{
							if (interfaceMock is not null)
							{
								InterfaceMock = new MockSequence<MyNihongo.Mockurai.Tests.IInterface>
								{
									VerifyIndex = _verifyIndex,
									Mock = interfaceMock,
								};
							}
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



					public void VerifyNoOtherCalls()
					{

					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield break;
					}

					private sealed class Proxy : MyNihongo.Mockurai.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}


					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<MyNihongo.Mockurai.Tests.IInterface> @this)
					{
						public void VerifyNoOtherCalls() =>
							((InterfaceMock)@this).VerifyNoOtherCalls();


					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<MyNihongo.Mockurai.Tests.IInterface> @this)
					{

					}
				}
				"""
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact(Skip = "implement later")]
	public async Task GenerateNullableFields()
	{
		throw new NotImplementedException();
	}

	[Fact]
	public async Task GenerateDifferentForNullability()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mockurai.Tests;

			public interface IInterface
			{
				void Invoke(string param1, float param2);
			}

			public abstract class Class
			{
				public abstract void Invoke(string? param1, float param2);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
				protected partial IMock<Class> ClassMock { get; }
			}
			""";

		string[] types = ["String", "Single"];
		TypeModel[] typesNullable =
		[
			new("String", 1, isNullable: true),
			new("Single", 2),
		];

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

					// ClassMock
					private readonly ClassMock _classMock = new(InvocationIndex.CounterValue);
					protected partial MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.Class> ClassMock => _classMock;

					protected void VerifyNoOtherCalls()
					{
						InterfaceMock.VerifyNoOtherCalls();
						ClassMock.VerifyNoOtherCalls();
					}

					protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
					{
						var ctx = new VerifySequenceContext(
							interfaceMock: InterfaceMock,
							classMock: ClassMock
						);

						verify(ctx);
					}

					protected sealed class VerifySequenceContext
					{
						private readonly VerifyIndex _verifyIndex = new();
						public readonly IMockSequence<MyNihongo.Mockurai.Tests.IInterface> InterfaceMock;
						public readonly IMockSequence<MyNihongo.Mockurai.Tests.Class> ClassMock;

						public VerifySequenceContext(MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.IInterface> interfaceMock, MyNihongo.Mockurai.IMock<MyNihongo.Mockurai.Tests.Class> classMock)
						{
							InterfaceMock = new MockSequence<MyNihongo.Mockurai.Tests.IInterface>
							{
								VerifyIndex = _verifyIndex,
								Mock = interfaceMock,
							};
							ClassMock = new MockSequence<MyNihongo.Mockurai.Tests.Class>
							{
								VerifyIndex = _verifyIndex,
								Mock = classMock,
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
					private SetupStringSingle? _invoke0;
					private InvocationStringSingle? _invoke0Invocation;

					public SetupStringSingle SetupInvoke(in It<string> param1, in It<float> param2)
					{
						_invoke0 ??= new SetupStringSingle();
						_invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return _invoke0;
					}

					public void VerifyInvoke(in It<string> param1, in It<float> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationStringSingle("IInterface.Invoke({0}, {1})");
						_invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke(in It<string> param1, in It<float> param2, long index)
					{
						_invoke0Invocation ??= new InvocationStringSingle("IInterface.Invoke({0}, {1})");
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

					private sealed class Proxy : MyNihongo.Mockurai.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}

						public void Invoke(string param1, float param2)
						{
							_mock._invoke0Invocation ??= new InvocationStringSingle("IInterface.Invoke({0}, {1})");
							_mock._invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							_mock._invoke0?.Invoke(param1, param2);
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
						public ISetup<SetupStringSingle.CallbackDelegate> SetupInvoke(in It<string> param1 = default, in It<float> param2 = default) =>
							((InterfaceMock)@this).SetupInvoke(param1, param2);

						public void VerifyInvoke(in It<string> param1, in It<float> param2, in Times times) =>
							((InterfaceMock)@this).VerifyInvoke(param1, param2, times);

						public void VerifyInvoke(in It<string> param1, in It<float> param2, System.Func<Times> times) =>
							((InterfaceMock)@this).VerifyInvoke(param1, param2, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<MyNihongo.Mockurai.Tests.IInterface> @this)
					{
						// Invoke
						public void Invoke(in It<string> param1, in It<float> param2)
						{
							var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvoke(param1, param2, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			(
				"ClassMock.g.cs",
				"""
				#nullable enable
				namespace MyNihongo.Mockurai;

				public sealed class ClassMock : IMock<MyNihongo.Mockurai.Tests.Class>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public ClassMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public MyNihongo.Mockurai.Tests.Class Object => _proxy ??= new Proxy(this);

					// Invoke
					private SetupStringNullableSingle? _invoke0;
					private InvocationStringNullableSingle? _invoke0Invocation;

					public SetupStringNullableSingle SetupInvoke(in It<string?> param1, in It<float> param2)
					{
						_invoke0 ??= new SetupStringNullableSingle();
						_invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
						return _invoke0;
					}

					public void VerifyInvoke(in It<string?> param1, in It<float> param2, in Times times)
					{
						_invoke0Invocation ??= new InvocationStringNullableSingle("Class.Invoke({0}, {1})");
						_invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
					}

					public long VerifyInvoke(in It<string?> param1, in It<float> param2, long index)
					{
						_invoke0Invocation ??= new InvocationStringNullableSingle("Class.Invoke({0}, {1})");
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

					private sealed class Proxy : MyNihongo.Mockurai.Tests.Class
					{
						private readonly ClassMock _mock;

						public Proxy(ClassMock mock)
						{
							_mock = mock;
						}

						public override void Invoke(string? param1, float param2)
						{
							_mock._invoke0Invocation ??= new InvocationStringNullableSingle("Class.Invoke({0}, {1})");
							_mock._invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
							_mock._invoke0?.Invoke(param1, param2);
						}
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<MyNihongo.Mockurai.Tests.Class> @this)
					{
						public void VerifyNoOtherCalls() =>
							((ClassMock)@this).VerifyNoOtherCalls();

						// Invoke
						public ISetup<SetupStringNullableSingle.CallbackDelegate> SetupInvoke(in It<string?> param1 = default, in It<float> param2 = default) =>
							((ClassMock)@this).SetupInvoke(param1, param2);

						public void VerifyInvoke(in It<string?> param1, in It<float> param2, in Times times) =>
							((ClassMock)@this).VerifyInvoke(param1, param2, times);

						public void VerifyInvoke(in It<string?> param1, in It<float> param2, System.Func<Times> times) =>
							((ClassMock)@this).VerifyInvoke(param1, param2, times());
					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<MyNihongo.Mockurai.Tests.Class> @this)
					{
						// Invoke
						public void Invoke(in It<string?> param1, in It<float> param2)
						{
							var nextIndex = ((ClassMock)@this.Mock).VerifyInvoke(param1, param2, @this.VerifyIndex);
							@this.VerifyIndex.Set(nextIndex);
						}
					}
				}
				"""
			),
			CreateSetupCode(types),
			CreateInvocationCode(types),
			CreateSetupCode(typesNullable),
			CreateInvocationCode(typesNullable),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
