namespace MyNihongo.Mockurai.Tests.Issues;

public sealed class Nullability : IssuesTestsBase
{
	[Fact]
	public async Task GenerateNullableProperties()
	{
		const string testCode =
			"""
			namespace Issues.Tests;

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
				namespace Issues.Tests;

				public partial class TestsBase
				{

					protected virtual void VerifyNoOtherCalls()
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

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<Issues.Tests.IInterface>? InterfaceMock;

						public VerifySequenceContext(IMock<Issues.Tests.IInterface>? interfaceMock)
						{
							VerifyIndex = new VerifyIndex();
							if (interfaceMock is not null)
							{
								InterfaceMock = new MockSequence<Issues.Tests.IInterface>
								{
									VerifyIndex = VerifyIndex,
									Mock = interfaceMock,
								};
							}
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



					public void VerifyNoOtherCalls()
					{

					}

					private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
					{
						yield break;
					}

					private sealed class Proxy : Issues.Tests.IInterface
					{
						private readonly InterfaceMock _mock;

						public Proxy(InterfaceMock mock)
						{
							_mock = mock;
						}


					}

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
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


					}
				}

				public static partial class MockSequenceExtensions
				{
					extension(IMockSequence<Issues.Tests.IInterface> @this)
					{

					}
				}
				"""
			),
		];

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
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
			namespace Issues.Tests;

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
				namespace Issues.Tests;

				public partial class TestsBase
				{
					// InterfaceMock
					private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
					protected partial IMock<Issues.Tests.IInterface> InterfaceMock => _interfaceMock;

					// ClassMock
					private readonly ClassMock _classMock = new(InvocationIndex.CounterValue);
					protected partial IMock<Issues.Tests.Class> ClassMock => _classMock;

					protected virtual void VerifyNoOtherCalls()
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

					protected class VerifySequenceContext
					{
						protected readonly VerifyIndex VerifyIndex;
						public readonly IMockSequence<Issues.Tests.IInterface> InterfaceMock;
						public readonly IMockSequence<Issues.Tests.Class> ClassMock;

						public VerifySequenceContext(IMock<Issues.Tests.IInterface> interfaceMock, IMock<Issues.Tests.Class> classMock)
						{
							VerifyIndex = new VerifyIndex();
							InterfaceMock = new MockSequence<Issues.Tests.IInterface>
							{
								VerifyIndex = VerifyIndex,
								Mock = interfaceMock,
							};
							ClassMock = new MockSequence<Issues.Tests.Class>
							{
								VerifyIndex = VerifyIndex,
								Mock = classMock,
							};
						}

						protected VerifySequenceContext(VerifySequenceContext ctx)
						{
							VerifyIndex = ctx.VerifyIndex;
							InterfaceMock = ctx.InterfaceMock;
							ClassMock = ctx.ClassMock;
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

					private sealed class Proxy : Issues.Tests.IInterface
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

					public sealed class InvocationContainer
					{
						private readonly InterfaceMock _mock;

						public InvocationContainer(InterfaceMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(string param1, float param2)>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];
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
					extension(IMockSequence<Issues.Tests.IInterface> @this)
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

				public sealed class ClassMock : IMock<Issues.Tests.Class>
				{
					private readonly InvocationIndex.Counter _invocationIndex;
					private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
					private Proxy? _proxy;

					public ClassMock(InvocationIndex.Counter invocationIndex)
					{
						_invocationIndex = invocationIndex;
						_invocationProviders = GetInvocations;
					}

					public Issues.Tests.Class Object => _proxy ??= new Proxy(this);

					public InvocationContainer Invocations => field ??= new InvocationContainer(this);

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

					private sealed class Proxy : Issues.Tests.Class
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

					public sealed class InvocationContainer
					{
						private readonly ClassMock _mock;

						public InvocationContainer(ClassMock mock)
						{
							_mock = mock;
						}

						public System.Collections.Generic.IEnumerable<IInvocation<(string? param1, float param2)>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];
					}
				}

				public static partial class MockExtensions
				{
					extension(IMock<Issues.Tests.Class> @this)
					{
						public ClassMock.InvocationContainer Invocations => ((ClassMock)@this).Invocations;

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
					extension(IMockSequence<Issues.Tests.Class> @this)
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
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}
}
