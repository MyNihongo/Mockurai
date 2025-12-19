namespace MyNihongo.Mock.Tests.EventTests;

public sealed class EventsShould : EventGenericTestsBase
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

			public void VerifyAddHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface<T>.HandlerEvent.add += {0}");
				_handlerEvent0AddInvocation.Verify(value, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface<T>.HandlerEvent.add += {0}");
				return _handlerEvent0AddInvocation.Verify(value, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface<T>.HandlerEvent.remove -= {0}");
				_handlerEvent0RemoveInvocation.Verify(value, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface<T>.HandlerEvent.remove -= {0}");
				return _handlerEvent0RemoveInvocation.Verify(value, index, _invocationProviders);
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
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface<T>.HandlerEvent.add");
				_handlerEvent0AddInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in System.EventHandler<string>? handler, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface<T>.HandlerEvent.add");
				return _handlerEvent0AddInvocation.Verify(handler, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in System.EventHandler<string>? handler, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface<T>.HandlerEvent.remove");
				_handlerEvent0RemoveInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in System.EventHandler<string>? handler, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface<T>.HandlerEvent.remove");
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
	public async Task GenerateInterfaceGenericEvent1()
	{
		const string @event = "event System.EventHandler<T>? HandlerEvent;";

		const string methods =
			"""
			// HandlerEvent
			private System.EventHandler<T>? _handlerEvent0;
			private Invocation<System.EventHandler<T>?>? _handlerEvent0AddInvocation;
			private Invocation<System.EventHandler<T>?>? _handlerEvent0RemoveInvocation;

			public void RaiseHandlerEvent(T e)
			{
				_handlerEvent0?.Invoke(Object, e);
			}

			public void VerifyAddHandlerEvent(in System.EventHandler<T>? handler, in Times times)
			{
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<T>?>("IInterface<T>.HandlerEvent.add");
				_handlerEvent0AddInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in System.EventHandler<T>? handler, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<T>?>("IInterface<T>.HandlerEvent.add");
				return _handlerEvent0AddInvocation.Verify(handler, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in System.EventHandler<T>? handler, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<T>?>("IInterface<T>.HandlerEvent.remove");
				_handlerEvent0RemoveInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in System.EventHandler<T>? handler, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<T>?>("IInterface<T>.HandlerEvent.remove");
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
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class<T>.HandlerEvent.add");
				_handlerEvent0AddInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class<T>.HandlerEvent.add");
				return _handlerEvent0AddInvocation.Verify(handler, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class<T>.HandlerEvent.remove");
				_handlerEvent0RemoveInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in MyNihongo.Mock.Tests.SampleHandler1? handler, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class<T>.HandlerEvent.remove");
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
	public async Task GenerateClassGenericEvent1()
	{
		const string @event =
			"""
			public abstract event System.EventHandler<T>? HandlerEvent;
			protected abstract event System.EventHandler<T>? ProtectedNotOverriden;
			public event System.EventHandler<T>? NotOverriden;
			""";

		const string methods =
			"""
			// HandlerEvent
			private System.EventHandler<T>? _handlerEvent0;
			private Invocation<System.EventHandler<T>?>? _handlerEvent0AddInvocation;
			private Invocation<System.EventHandler<T>?>? _handlerEvent0RemoveInvocation;

			public void RaiseHandlerEvent(T e)
			{
				_handlerEvent0?.Invoke(Object, e);
			}

			public void VerifyAddHandlerEvent(in System.EventHandler<T>? handler, in Times times)
			{
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<T>?>("Class<T>.HandlerEvent.add");
				_handlerEvent0AddInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in System.EventHandler<T>? handler, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<T>?>("Class<T>.HandlerEvent.add");
				return _handlerEvent0AddInvocation.Verify(handler, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in System.EventHandler<T>? handler, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<T>?>("Class<T>.HandlerEvent.remove");
				_handlerEvent0RemoveInvocation.Verify(handler, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in System.EventHandler<T>? handler, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<T>?>("Class<T>.HandlerEvent.remove");
				return _handlerEvent0RemoveInvocation.Verify(handler, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override event System.EventHandler<T>? HandlerEvent;
			protected override event System.EventHandler<T>? ProtectedNotOverriden;
			""";

		var testCode = CreateClassTestCode(@event, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
