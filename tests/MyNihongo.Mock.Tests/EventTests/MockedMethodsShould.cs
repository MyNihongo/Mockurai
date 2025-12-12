namespace MyNihongo.Mock.Tests.EventTests;

public sealed class MockedMethodsShould : EventTestsBase
{
	[Fact]
	public async Task GenerateNonGenericEvent()
	{
		const string methods =
			"""
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
			""";

		var testCode = CreateTestCode("event System.EventHandler<string>? HandlerEvent;");
		var generatedSources = CreateGeneratedSources(methods);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
