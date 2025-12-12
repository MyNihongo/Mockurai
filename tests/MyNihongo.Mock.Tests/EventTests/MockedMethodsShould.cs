namespace MyNihongo.Mock.Tests.EventTests;

public sealed class MockedMethodsShould : EventTestsBase
{
	[Fact]
	public async Task GenerateNonGenericEvent()
	{
		var expected2 =
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

					public event System.EventHandler<string>? HandlerEvent;

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

		var testCode = CreateTestCode("event System.EventHandler<string>? HandlerEvent;");
		var generatedSources = CreateGeneratedSources(expected2);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
