namespace MyNihongo.Mock.Tests.EventTests;

public sealed class EventGenericShould : EventGenericTestsBase
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
				_handlerEvent0AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface<T>.HandlerEvent.add += {0}");
				return _handlerEvent0AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface<T>.HandlerEvent.remove -= {0}");
				_handlerEvent0RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface<T>.HandlerEvent.remove -= {0}");
				return _handlerEvent0RemoveInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent
			{
				add
				{
					_mock._handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.add += {0}");
					_mock._handlerEvent0AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 += value;
				}
				remove
				{
					_mock._handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.remove -= {0}");
					_mock._handlerEvent0RemoveInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 -= value;
				}
			}
			""";

		const string verifyNoOtherCalls =
			"""
			_handlerEvent0AddInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_handlerEvent0RemoveInvocation?.VerifyNoOtherCalls(_invocationProviders);
			""";

		const string invocations =
			"""
			yield return _handlerEvent0AddInvocation;
			yield return _handlerEvent0RemoveInvocation;
			""";

		var testCode = CreateInterfaceTestCode(@event);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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

			public void VerifyAddHandlerEvent(in It<System.EventHandler<string>?> value, in Times times)
			{
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface<T>.HandlerEvent.add += {0}");
				_handlerEvent0AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in It<System.EventHandler<string>?> value, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface<T>.HandlerEvent.add += {0}");
				return _handlerEvent0AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in It<System.EventHandler<string>?> value, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface<T>.HandlerEvent.remove -= {0}");
				_handlerEvent0RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in It<System.EventHandler<string>?> value, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface<T>.HandlerEvent.remove -= {0}");
				return _handlerEvent0RemoveInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent
			{
				add
				{
					_mock._handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.add += {0}");
					_mock._handlerEvent0AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 += value;
				}
				remove
				{
					_mock._handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.remove -= {0}");
					_mock._handlerEvent0RemoveInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 -= value;
				}
			}
			""";

		const string verifyNoOtherCalls =
			"""
			_handlerEvent0AddInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_handlerEvent0RemoveInvocation?.VerifyNoOtherCalls(_invocationProviders);
			""";

		const string invocations =
			"""
			yield return _handlerEvent0AddInvocation;
			yield return _handlerEvent0RemoveInvocation;
			""";

		var testCode = CreateInterfaceTestCode(@event);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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

			public void VerifyAddHandlerEvent(in It<System.EventHandler<T>?> value, in Times times)
			{
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<T>?>("IInterface<T>.HandlerEvent.add += {0}");
				_handlerEvent0AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in It<System.EventHandler<T>?> value, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<T>?>("IInterface<T>.HandlerEvent.add += {0}");
				return _handlerEvent0AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in It<System.EventHandler<T>?> value, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<T>?>("IInterface<T>.HandlerEvent.remove -= {0}");
				_handlerEvent0RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in It<System.EventHandler<T>?> value, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<T>?>("IInterface<T>.HandlerEvent.remove -= {0}");
				return _handlerEvent0RemoveInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent
			{
				add
				{
					_mock._handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.add += {0}");
					_mock._handlerEvent0AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 += value;
				}
				remove
				{
					_mock._handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.remove -= {0}");
					_mock._handlerEvent0RemoveInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 -= value;
				}
			}
			""";

		const string verifyNoOtherCalls =
			"""
			_handlerEvent0AddInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_handlerEvent0RemoveInvocation?.VerifyNoOtherCalls(_invocationProviders);
			""";

		const string invocations =
			"""
			yield return _handlerEvent0AddInvocation;
			yield return _handlerEvent0RemoveInvocation;
			""";

		var testCode = CreateInterfaceTestCode(@event);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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

			public void VerifyAddHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class<T>.HandlerEvent.add += {0}");
				_handlerEvent0AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class<T>.HandlerEvent.add += {0}");
				return _handlerEvent0AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class<T>.HandlerEvent.remove -= {0}");
				_handlerEvent0RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class<T>.HandlerEvent.remove -= {0}");
				return _handlerEvent0RemoveInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent
			{
				add
				{
					_mock._handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class<T>.HandlerEvent.add += {0}");
					_mock._handlerEvent0AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 += value;
				}
				remove
				{
					_mock._handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class<T>.HandlerEvent.remove -= {0}");
					_mock._handlerEvent0RemoveInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 -= value;
				}
			}
			""";

		const string verifyNoOtherCalls =
			"""
			_handlerEvent0AddInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_handlerEvent0RemoveInvocation?.VerifyNoOtherCalls(_invocationProviders);
			""";

		const string invocations =
			"""
			yield return _handlerEvent0AddInvocation;
			yield return _handlerEvent0RemoveInvocation;
			""";

		var testCode = CreateClassTestCode(@event);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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

			public void VerifyAddHandlerEvent(in It<System.EventHandler<T>?> value, in Times times)
			{
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<T>?>("Class<T>.HandlerEvent.add += {0}");
				_handlerEvent0AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in It<System.EventHandler<T>?> value, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<T>?>("Class<T>.HandlerEvent.add += {0}");
				return _handlerEvent0AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in It<System.EventHandler<T>?> value, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<T>?>("Class<T>.HandlerEvent.remove -= {0}");
				_handlerEvent0RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in It<System.EventHandler<T>?> value, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<T>?>("Class<T>.HandlerEvent.remove -= {0}");
				return _handlerEvent0RemoveInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override event System.EventHandler<T>? HandlerEvent
			{
				add
				{
					_mock._handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<T>?>("Class<T>.HandlerEvent.add += {0}");
					_mock._handlerEvent0AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 += value;
				}
				remove
				{
					_mock._handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<T>?>("Class<T>.HandlerEvent.remove -= {0}");
					_mock._handlerEvent0RemoveInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 -= value;
				}
			}

			protected override event System.EventHandler<T>? ProtectedNotOverriden;
			""";

		const string verifyNoOtherCalls =
			"""
			_handlerEvent0AddInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_handlerEvent0RemoveInvocation?.VerifyNoOtherCalls(_invocationProviders);
			""";

		const string invocations =
			"""
			yield return _handlerEvent0AddInvocation;
			yield return _handlerEvent0RemoveInvocation;
			""";

		var testCode = CreateClassTestCode(@event, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
