namespace MyNihongo.Mockurai.Tests.EventTests;

public sealed class EventGenericShould : EventGenericTestsBase
{
	[Fact]
	public async Task GenerateInterfaceEvent1()
	{
		const string @event = "event MyNihongo.Example.Tests.SampleHandler1? HandlerEvent;";

		const string methods =
			"""
			// HandlerEvent
			private MyNihongo.Example.Tests.SampleHandler1? _handlerEvent0;
			private Invocation<MyNihongo.Example.Tests.SampleHandler1?>? _handlerEvent0AddInvocation;
			private Invocation<MyNihongo.Example.Tests.SampleHandler1?>? _handlerEvent0RemoveInvocation;

			public void RaiseHandlerEvent(int value)
			{
				_handlerEvent0?.Invoke(Object, value);
			}

			public void VerifyAddHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Example.Tests.SampleHandler1?>($"IInterface<{typeof(T).Name}>.HandlerEvent.add += {{0}}");
				_handlerEvent0AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Example.Tests.SampleHandler1?>($"IInterface<{typeof(T).Name}>.HandlerEvent.add += {{0}}");
				return _handlerEvent0AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Example.Tests.SampleHandler1?>($"IInterface<{typeof(T).Name}>.HandlerEvent.remove -= {{0}}");
				_handlerEvent0RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Example.Tests.SampleHandler1?>($"IInterface<{typeof(T).Name}>.HandlerEvent.remove -= {{0}}");
				return _handlerEvent0RemoveInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public event MyNihongo.Example.Tests.SampleHandler1? HandlerEvent
			{
				add
				{
					_mock._handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Example.Tests.SampleHandler1?>($"IInterface<{typeof(T).Name}>.HandlerEvent.add += {{0}}");
					_mock._handlerEvent0AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 += value;
				}
				remove
				{
					_mock._handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Example.Tests.SampleHandler1?>($"IInterface<{typeof(T).Name}>.HandlerEvent.remove -= {{0}}");
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

		const string extensions =
			"""
			// HandlerEvent
			public void RaiseHandlerEvent(int value) =>
				((InterfaceMock<T>)@this).RaiseHandlerEvent(value);

			public void VerifyAddHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyAddHandlerEvent(value, times);

			public void VerifyAddHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyAddHandlerEvent(value, times());

			public void VerifyRemoveHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyRemoveHandlerEvent(value, times);

			public void VerifyRemoveHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyRemoveHandlerEvent(value, times());
			""";

		const string extensionsSequence =
			"""
			// HandlerEvent
			public void AddHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyAddHandlerEvent(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}

			public void RemoveHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyRemoveHandlerEvent(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(@event);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

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
			public event System.EventHandler<string>? HandlerEvent
			{
				add
				{
					_mock._handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface<T>.HandlerEvent.add += {0}");
					_mock._handlerEvent0AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 += value;
				}
				remove
				{
					_mock._handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface<T>.HandlerEvent.remove -= {0}");
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

		const string extensions =
			"""
			// HandlerEvent
			public void RaiseHandlerEvent(string e) =>
				((InterfaceMock<T>)@this).RaiseHandlerEvent(e);

			public void VerifyAddHandlerEvent(in It<System.EventHandler<string>?> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyAddHandlerEvent(value, times);

			public void VerifyAddHandlerEvent(in It<System.EventHandler<string>?> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyAddHandlerEvent(value, times());

			public void VerifyRemoveHandlerEvent(in It<System.EventHandler<string>?> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyRemoveHandlerEvent(value, times);

			public void VerifyRemoveHandlerEvent(in It<System.EventHandler<string>?> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyRemoveHandlerEvent(value, times());
			""";

		const string extensionsSequence =
			"""
			// HandlerEvent
			public void AddHandlerEvent(in It<System.EventHandler<string>?> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyAddHandlerEvent(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}

			public void RemoveHandlerEvent(in It<System.EventHandler<string>?> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyRemoveHandlerEvent(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(@event);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

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
			public event System.EventHandler<T>? HandlerEvent
			{
				add
				{
					_mock._handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<T>?>("IInterface<T>.HandlerEvent.add += {0}");
					_mock._handlerEvent0AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 += value;
				}
				remove
				{
					_mock._handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<T>?>("IInterface<T>.HandlerEvent.remove -= {0}");
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

		const string extensions =
			"""
			// HandlerEvent
			public void RaiseHandlerEvent(T e) =>
				((InterfaceMock<T>)@this).RaiseHandlerEvent(e);

			public void VerifyAddHandlerEvent(in It<System.EventHandler<T>?> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyAddHandlerEvent(value, times);

			public void VerifyAddHandlerEvent(in It<System.EventHandler<T>?> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyAddHandlerEvent(value, times());

			public void VerifyRemoveHandlerEvent(in It<System.EventHandler<T>?> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyRemoveHandlerEvent(value, times);

			public void VerifyRemoveHandlerEvent(in It<System.EventHandler<T>?> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyRemoveHandlerEvent(value, times());
			""";

		const string extensionsSequence =
			"""
			// HandlerEvent
			public void AddHandlerEvent(in It<System.EventHandler<T>?> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyAddHandlerEvent(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}

			public void RemoveHandlerEvent(in It<System.EventHandler<T>?> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyRemoveHandlerEvent(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(@event);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateClassEvent1()
	{
		const string @event =
			"""
			public virtual event MyNihongo.Example.Tests.SampleHandler1? HandlerEvent;
			protected virtual event MyNihongo.Example.Tests.SampleHandler1? ProtectedNotOverriden;
			public event MyNihongo.Example.Tests.SampleHandler1? NotOverriden;
			""";

		const string methods =
			"""
			// HandlerEvent
			private MyNihongo.Example.Tests.SampleHandler1? _handlerEvent0;
			private Invocation<MyNihongo.Example.Tests.SampleHandler1?>? _handlerEvent0AddInvocation;
			private Invocation<MyNihongo.Example.Tests.SampleHandler1?>? _handlerEvent0RemoveInvocation;

			public void RaiseHandlerEvent(int value)
			{
				_handlerEvent0?.Invoke(Object, value);
			}

			public void VerifyAddHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Example.Tests.SampleHandler1?>("Class<T>.HandlerEvent.add += {0}");
				_handlerEvent0AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Example.Tests.SampleHandler1?>("Class<T>.HandlerEvent.add += {0}");
				return _handlerEvent0AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Example.Tests.SampleHandler1?>("Class<T>.HandlerEvent.remove -= {0}");
				_handlerEvent0RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Example.Tests.SampleHandler1?>("Class<T>.HandlerEvent.remove -= {0}");
				return _handlerEvent0RemoveInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override event MyNihongo.Example.Tests.SampleHandler1? HandlerEvent
			{
				add
				{
					_mock._handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Example.Tests.SampleHandler1?>("Class<T>.HandlerEvent.add += {0}");
					_mock._handlerEvent0AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 += value;
				}
				remove
				{
					_mock._handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Example.Tests.SampleHandler1?>("Class<T>.HandlerEvent.remove -= {0}");
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

		const string extensions =
			"""
			// HandlerEvent
			public void RaiseHandlerEvent(int value) =>
				((ClassMock<T>)@this).RaiseHandlerEvent(value);

			public void VerifyAddHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, in Times times) =>
				((ClassMock<T>)@this).VerifyAddHandlerEvent(value, times);

			public void VerifyAddHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyAddHandlerEvent(value, times());

			public void VerifyRemoveHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, in Times times) =>
				((ClassMock<T>)@this).VerifyRemoveHandlerEvent(value, times);

			public void VerifyRemoveHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value, System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyRemoveHandlerEvent(value, times());
			""";

		const string extensionsSequence =
			"""
			// HandlerEvent
			public void AddHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value)
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyAddHandlerEvent(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}

			public void RemoveHandlerEvent(in It<MyNihongo.Example.Tests.SampleHandler1?> value)
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyRemoveHandlerEvent(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateClassTestCode(@event);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

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

		const string extensions =
			"""
			// HandlerEvent
			public void RaiseHandlerEvent(T e) =>
				((ClassMock<T>)@this).RaiseHandlerEvent(e);

			public void VerifyAddHandlerEvent(in It<System.EventHandler<T>?> value, in Times times) =>
				((ClassMock<T>)@this).VerifyAddHandlerEvent(value, times);

			public void VerifyAddHandlerEvent(in It<System.EventHandler<T>?> value, System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyAddHandlerEvent(value, times());

			public void VerifyRemoveHandlerEvent(in It<System.EventHandler<T>?> value, in Times times) =>
				((ClassMock<T>)@this).VerifyRemoveHandlerEvent(value, times);

			public void VerifyRemoveHandlerEvent(in It<System.EventHandler<T>?> value, System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyRemoveHandlerEvent(value, times());
			""";

		const string extensionsSequence =
			"""
			// HandlerEvent
			public void AddHandlerEvent(in It<System.EventHandler<T>?> value)
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyAddHandlerEvent(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}

			public void RemoveHandlerEvent(in It<System.EventHandler<T>?> value)
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyRemoveHandlerEvent(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateClassTestCode(@event, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
