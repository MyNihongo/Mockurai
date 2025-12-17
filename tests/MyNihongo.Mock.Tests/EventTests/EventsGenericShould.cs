namespace MyNihongo.Mock.Tests.EventTests;

public sealed class EventsGenericShould : EventTestsBase
{
	[Fact]
	public async Task GenerateInterfaceEvent1()
	{
		const string @event = "event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent;";

		const string methods =
			"""
			// HandlerEvent
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

		const string proxy = $"public {@event}";

		var testCode = CreateInterfaceTestCode(@event);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceEvent2()
	{
		const string @event = "event System.EventHandler<string>? HandlerEvent;";

		const string methods =
			"""
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
			""";

		const string proxy = $"public {@event}";

		var testCode = CreateInterfaceTestCode(@event);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceMultipleEvents()
	{
		const string @event =
			"""
			event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent1;
			event System.EventHandler<string>? HandlerEvent2;
			""";

		const string methods =
			"""
			// HandlerEvent1
			private MyNihongo.Mock.Tests.SampleHandler1? _handlerEvent10;
			private Invocation<MyNihongo.Mock.Tests.SampleHandler1?>? _handlerEvent10AddInvocation;
			private Invocation<MyNihongo.Mock.Tests.SampleHandler1?>? _handlerEvent10RemoveInvocation;

			public void RaiseHandlerEvent1(int value)
			{
				_handlerEvent10?.Invoke(Object, value);
			}

			public void VerifyAddHandlerEvent1(in MyNihongo.Mock.Tests.SampleHandler1? handler, in Times times)
			{
				_handlerEvent10AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent1.add");
				_handlerEvent10AddInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent1(in MyNihongo.Mock.Tests.SampleHandler1? handler, long index)
			{
				_handlerEvent10AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent1.add");
				return _handlerEvent10AddInvocation.Verify(handler, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent1(in MyNihongo.Mock.Tests.SampleHandler1? handler, in Times times)
			{
				_handlerEvent10RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent1.remove");
				_handlerEvent10RemoveInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent1(in MyNihongo.Mock.Tests.SampleHandler1? handler, long index)
			{
				_handlerEvent10RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent1.remove");
				return _handlerEvent10RemoveInvocation.Verify(handler, index, _invocationProviders);
			}

			// HandlerEvent2
			private System.EventHandler<string>? _handlerEvent20;
			private Invocation<System.EventHandler<string>?>? _handlerEvent20AddInvocation;
			private Invocation<System.EventHandler<string>?>? _handlerEvent20RemoveInvocation;

			public void RaiseHandlerEvent2(string e)
			{
				_handlerEvent20?.Invoke(Object, e);
			}

			public void VerifyAddHandlerEvent2(in System.EventHandler<string>? handler, in Times times)
			{
				_handlerEvent20AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent2.add");
				_handlerEvent20AddInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent2(in System.EventHandler<string>? handler, long index)
			{
				_handlerEvent20AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent2.add");
				return _handlerEvent20AddInvocation.Verify(handler, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent2(in System.EventHandler<string>? handler, in Times times)
			{
				_handlerEvent20RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent2.remove");
				_handlerEvent20RemoveInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent2(in System.EventHandler<string>? handler, long index)
			{
				_handlerEvent20RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent2.remove");
				return _handlerEvent20RemoveInvocation.Verify(handler, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent1;
			public event System.EventHandler<string>? HandlerEvent2;
			""";

		var testCode = CreateInterfaceTestCode(@event);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClassEvent1()
	{
		const string @event =
			"""
			public virtual event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent;
			protected virtual event MyNihongo.Mock.Tests.SampleHandler1? ProtectedNotOverriden;
			public event MyNihongo.Mock.Tests.SampleHandler1? NotOverriden;
			""";

		const string methods =
			"""
			// HandlerEvent
			private MyNihongo.Mock.Tests.SampleHandler1? _handlerEvent0;
			private Invocation<MyNihongo.Mock.Tests.SampleHandler1?>? _handlerEvent0AddInvocation;
			private Invocation<MyNihongo.Mock.Tests.SampleHandler1?>? _handlerEvent0RemoveInvocation;

			public void RaiseHandlerEvent(int value)
			{
				_handlerEvent0?.Invoke(Object, value);
			}

			public void VerifyAddHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, in Times times)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.add");
				_handlerEvent0AddInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.add");
				return _handlerEvent0AddInvocation.Verify(handler, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.remove");
				_handlerEvent0RemoveInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.remove");
				return _handlerEvent0RemoveInvocation.Verify(handler, index, _invocationProviders);
			}
			""";

		const string proxy = "public override event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent;";

		var testCode = CreateClassTestCode(@event);
		var generatedSources = CreateClassGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClassMultipleEvents()
	{
		const string @event =
			"""
			public virtual event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent;
			public abstract event System.EventHandler<string>? HandlerAnotherEvent;
			""";

		const string methods =
			"""
			// HandlerEvent
			private MyNihongo.Mock.Tests.SampleHandler1? _handlerEvent0;
			private Invocation<MyNihongo.Mock.Tests.SampleHandler1?>? _handlerEvent0AddInvocation;
			private Invocation<MyNihongo.Mock.Tests.SampleHandler1?>? _handlerEvent0RemoveInvocation;

			public void RaiseHandlerEvent(int value)
			{
				_handlerEvent0?.Invoke(Object, value);
			}

			public void VerifyAddHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, in Times times)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.add");
				_handlerEvent0AddInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.add");
				return _handlerEvent0AddInvocation.Verify(handler, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.remove");
				_handlerEvent0RemoveInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.remove");
				return _handlerEvent0RemoveInvocation.Verify(handler, index, _invocationProviders);
			}

			// HandlerAnotherEvent
			private System.EventHandler<string>? _handlerAnotherEvent0;
			private Invocation<System.EventHandler<string>?>? _handlerAnotherEvent0AddInvocation;
			private Invocation<System.EventHandler<string>?>? _handlerAnotherEvent0RemoveInvocation;

			public void RaiseHandlerAnotherEvent(string e)
			{
				_handlerAnotherEvent0?.Invoke(Object, e);
			}

			public void VerifyAddHandlerAnotherEvent(in System.EventHandler<string>? handler, in Times times)
			{
				_handlerAnotherEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("Class.HandlerAnotherEvent.add");
				_handlerAnotherEvent0AddInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyAddHandlerAnotherEvent(in System.EventHandler<string>? handler, long index)
			{
				_handlerAnotherEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("Class.HandlerAnotherEvent.add");
				return _handlerAnotherEvent0AddInvocation.Verify(handler, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerAnotherEvent(in System.EventHandler<string>? handler, in Times times)
			{
				_handlerAnotherEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("Class.HandlerAnotherEvent.remove");
				_handlerAnotherEvent0RemoveInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerAnotherEvent(in System.EventHandler<string>? handler, long index)
			{
				_handlerAnotherEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("Class.HandlerAnotherEvent.remove");
				return _handlerAnotherEvent0RemoveInvocation.Verify(handler, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent;
			public override event System.EventHandler<string>? HandlerAnotherEvent;
			""";

		var testCode = CreateClassTestCode(@event, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
