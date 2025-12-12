using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Testing;

namespace MyNihongo.Mock.Tests.EventTests;

public sealed class MockedMethodsShould : EventTestsBase
{
	[Fact]
	public async Task GenerateNonGenericEvent()
	{
		var expected1 =
			"""
			namespace MyNihongo.Mock.Tests;

			public partial class TestsBase
			{
				// InterfaceMock
				private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
				protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock => _interfaceMock;

				protected void VerifyNoOtherCalls()
				{
					_interfaceMock.VerifyNoOtherCalls();
				}

				protected void VerifyInSequence(Action<VerifySequenceContext> verify)
				{
					var ctx = new VerifySequenceContext(
						interfaceMock: _interfaceMock
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
			""";

		var expected2 =
			"""
			namespace MyNihongo.Mock;

			public sealed class InterfaceMock : IMock<MyNihongo.Mock.Tests.IInterface>
			{
				private readonly InvocationIndex.Counter _invocationIndex;
				private readonly Func<IEnumerable<IInvocationProvider?>> _invocationProviders;
				private Proxy? _proxy;

				public InterfaceMock(InvocationIndex.Counter invocationIndex)
				{
					_invocationIndex = invocationIndex;
					_invocationProviders = GetInvocations;
				}

				public IPrimitiveDependencyService Object => _proxy ??= new Proxy(this);

				// HandlerEvent
				private System.EventHandler<string>? _handlerEvent0;
				private Invocation<System.EventHandler<string>?>? _handlerEvent0AddInvocation;
				private Invocation<System.EventHandler<string>?>? _handlerEvent0RemoveInvocation;

				public void RaiseHandlerEvent(string e)
				{
					_handlerEvent0?.Invoke(Object, e);
				}

				public void VerifyAddHandlerEvent(in System.EventHandler<string>? handler, in Times times)
				{
					_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent.add");
					_handlerEvent0AddInvocation.Verify(handler, times, _invocationProviders);
				}

				public long VerifyAddHandlerEvent(in System.EventHandler<string>? handler, long index)
				{
					_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent.add");
					return _handlerEvent0AddInvocation.Verify(handler, index, _invocationProviders);
				}

				public void VerifyRemoveHandlerEvent(in System.EventHandler<string>? handler, in Times times)
				{
					_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent.remove");
					_handlerEvent0RemoveInvocation.Verify(handler, times, _invocationProviders);
				}

				public long VerifyRemoveHandlerEvent(in System.EventHandler<string>? handler, long index)
				{
					_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent.remove");
					return _handlerEvent0RemoveInvocation.Verify(handler, index, _invocationProviders);
				}


				public void VerifyNoOtherCalls()
				{
				
				}

				private IEnumerable<IInvocationProvider?> GetInvocations()
				{
				
				}

				private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
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
			""";

		var ctx = new CSharpSourceGeneratorTest<SourceGenerator, DefaultVerifier>
		{
			ReferenceAssemblies = ReferenceAssemblies.Net.Net80,
			TestBehaviors = TestBehaviors.SkipGeneratedCodeCheck | TestBehaviors.SkipSuppressionCheck | TestBehaviors.SkipGeneratedSourcesCheck,
			TestCode =
				"""
				namespace MyNihongo.Mock.Tests;

				public interface IInterface
				{
					event System.EventHandler<string>? HandlerEvent;
				}

				[MockuraiGenerate]
				public abstract partial class TestsBase
				{
					protected partial IMock<IInterface> InterfaceMock { get; }
				}
				""",
			TestState =
			{
				AdditionalReferences = { typeof(MockuraiGenerateAttribute).Assembly },
				GeneratedSources =
				{
					(typeof(SourceGenerator), "TestsBase.g.cs", expected1),
					(typeof(SourceGenerator), "InterfaceMock.g.cs", expected2),
					(typeof(SourceGenerator), "_Usings.g.cs", "global using MyNihongo.Mock;"),
				},
			},
		};

		await ctx.RunAsync();
	}
}

// public sealed class Fuck : CSharpSourceGeneratorTest<SourceGenerator, DefaultVerifier>
// {
// 	public Fuck()
// 	{
// 		this.VerifyDiagnosticsAsync()
// 	}
// }
