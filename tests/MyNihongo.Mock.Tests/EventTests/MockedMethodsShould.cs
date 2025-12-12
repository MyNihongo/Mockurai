namespace MyNihongo.Mock.Tests.EventTests;

public sealed class MockedMethodsShould : EventTestsBase
{
	[Fact]
	public async Task GenerateNonGenericEvent1()
	{
		const string methods =
			"""
			private MyNihongo.Mock.Tests.SampleHandler1? _handlerEvent0;
			private Invocation<MyNihongo.Mock.Tests.SampleHandler1?>? _handlerEvent0AddInvocation;
			private Invocation<MyNihongo.Mock.Tests.SampleHandler1?>? _handlerEvent0RemoveInvocation;

			public void RaiseHandlerEvent(int value)
			{
				_handlerEvent0?.Invoke(Object, value);
			}

			public void VerifyAddHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, in Times times)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent.add");
				_handlerEvent0AddInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent.add");
				return _handlerEvent0AddInvocation.Verify(handler, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent.remove");
				_handlerEvent0RemoveInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent.remove");
				return _handlerEvent0RemoveInvocation.Verify(handler, index, _invocationProviders);
			}
			""";

		const string @event = "event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent;";
		var testCode = CreateTestCode(@event);
		var generatedSources = CreateGeneratedSources(methods, @event);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateNonGenericEvent2()
	{
		const string methods =
			"""
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
			""";

		const string @event = "event System.EventHandler<string>? HandlerEvent;";
		var testCode = CreateTestCode(@event);
		var generatedSources = CreateGeneratedSources(methods, @event);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
