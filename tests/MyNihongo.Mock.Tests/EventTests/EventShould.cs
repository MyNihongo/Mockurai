namespace MyNihongo.Mock.Tests.EventTests;

public sealed class EventShould : EventTestsBase
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
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent.add += {0}");
				_handlerEvent0AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent.add += {0}");
				return _handlerEvent0AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent.remove -= {0}");
				_handlerEvent0RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent.remove -= {0}");
				return _handlerEvent0RemoveInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent
			{
				add
				{
					_mock._handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent.add += {0}");
					_mock._handlerEvent0AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 += value;
				}
				remove
				{
					_mock._handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent.remove -= {0}");
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
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent.add += {0}");
				_handlerEvent0AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in It<System.EventHandler<string>?> value, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent.add += {0}");
				return _handlerEvent0AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in It<System.EventHandler<string>?> value, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent.remove -= {0}");
				_handlerEvent0RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in It<System.EventHandler<string>?> value, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent.remove -= {0}");
				return _handlerEvent0RemoveInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public event System.EventHandler<string>? HandlerEvent
			{
				add
				{
					_mock._handlerEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent.add += {0}");
					_mock._handlerEvent0AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent0 += value;
				}
				remove
				{
					_mock._handlerEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent.remove -= {0}");
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

			public void VerifyAddHandlerEvent1(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent10AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent1.add += {0}");
				_handlerEvent10AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent1(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent10AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent1.add += {0}");
				return _handlerEvent10AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent1(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent10RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent1.remove -= {0}");
				_handlerEvent10RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent1(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent10RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent1.remove -= {0}");
				return _handlerEvent10RemoveInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			// HandlerEvent2
			private System.EventHandler<string>? _handlerEvent20;
			private Invocation<System.EventHandler<string>?>? _handlerEvent20AddInvocation;
			private Invocation<System.EventHandler<string>?>? _handlerEvent20RemoveInvocation;

			public void RaiseHandlerEvent2(string e)
			{
				_handlerEvent20?.Invoke(Object, e);
			}

			public void VerifyAddHandlerEvent2(in It<System.EventHandler<string>?> value, in Times times)
			{
				_handlerEvent20AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent2.add += {0}");
				_handlerEvent20AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent2(in It<System.EventHandler<string>?> value, long index)
			{
				_handlerEvent20AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent2.add += {0}");
				return _handlerEvent20AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent2(in It<System.EventHandler<string>?> value, in Times times)
			{
				_handlerEvent20RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent2.remove -= {0}");
				_handlerEvent20RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent2(in It<System.EventHandler<string>?> value, long index)
			{
				_handlerEvent20RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent2.remove -= {0}");
				return _handlerEvent20RemoveInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public event MyNihongo.Mock.Tests.SampleHandler1? HandlerEvent1
			{
				add
				{
					_mock._handlerEvent10AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent1.add += {0}");
					_mock._handlerEvent10AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent10 += value;
				}
				remove
				{
					_mock._handlerEvent10RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("IInterface.HandlerEvent1.remove -= {0}");
					_mock._handlerEvent10RemoveInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent10 -= value;
				}
			}

			public event System.EventHandler<string>? HandlerEvent2
			{
				add
				{
					_mock._handlerEvent20AddInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent2.add += {0}");
					_mock._handlerEvent20AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent20 += value;
				}
				remove
				{
					_mock._handlerEvent20RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("IInterface.HandlerEvent2.remove -= {0}");
					_mock._handlerEvent20RemoveInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerEvent20 -= value;
				}
			}
			""";

		const string verifyNoOtherCalls =
			"""
			_handlerEvent10AddInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_handlerEvent10RemoveInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_handlerEvent20AddInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_handlerEvent20RemoveInvocation?.VerifyNoOtherCalls(_invocationProviders);
			""";

		const string invocations =
			"""
			yield return _handlerEvent10AddInvocation;
			yield return _handlerEvent10RemoveInvocation;
			yield return _handlerEvent20AddInvocation;
			yield return _handlerEvent20RemoveInvocation;
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
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.add += {0}");
				_handlerEvent0AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.add += {0}");
				return _handlerEvent0AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.remove -= {0}");
				_handlerEvent0RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.remove -= {0}");
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

		var testCode = CreateClassTestCode(@event);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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

			public void VerifyAddHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.add += {0}");
				_handlerEvent0AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0AddInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.add += {0}");
				return _handlerEvent0AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, in Times times)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.remove -= {0}");
				_handlerEvent0RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerEvent(in It<MyNihongo.Mock.Tests.SampleHandler1?> value, long index)
			{
				_handlerEvent0RemoveInvocation ??= new Invocation<MyNihongo.Mock.Tests.SampleHandler1?>("Class.HandlerEvent.remove -= {0}");
				return _handlerEvent0RemoveInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			// HandlerAnotherEvent
			private System.EventHandler<string>? _handlerAnotherEvent0;
			private Invocation<System.EventHandler<string>?>? _handlerAnotherEvent0AddInvocation;
			private Invocation<System.EventHandler<string>?>? _handlerAnotherEvent0RemoveInvocation;

			public void RaiseHandlerAnotherEvent(string e)
			{
				_handlerAnotherEvent0?.Invoke(Object, e);
			}

			public void VerifyAddHandlerAnotherEvent(in It<System.EventHandler<string>?> value, in Times times)
			{
				_handlerAnotherEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("Class.HandlerAnotherEvent.add += {0}");
				_handlerAnotherEvent0AddInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyAddHandlerAnotherEvent(in It<System.EventHandler<string>?> value, long index)
			{
				_handlerAnotherEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("Class.HandlerAnotherEvent.add += {0}");
				return _handlerAnotherEvent0AddInvocation.Verify(value.ValueSetup, index, _invocationProviders);
			}

			public void VerifyRemoveHandlerAnotherEvent(in It<System.EventHandler<string>?> value, in Times times)
			{
				_handlerAnotherEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("Class.HandlerAnotherEvent.remove -= {0}");
				_handlerAnotherEvent0RemoveInvocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyRemoveHandlerAnotherEvent(in It<System.EventHandler<string>?> value, long index)
			{
				_handlerAnotherEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("Class.HandlerAnotherEvent.remove -= {0}");
				return _handlerAnotherEvent0RemoveInvocation.Verify(value.ValueSetup, index, _invocationProviders);
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

			public override event System.EventHandler<string>? HandlerAnotherEvent
			{
				add
				{
					_mock._handlerAnotherEvent0AddInvocation ??= new Invocation<System.EventHandler<string>?>("Class.HandlerAnotherEvent.add += {0}");
					_mock._handlerAnotherEvent0AddInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerAnotherEvent0 += value;
				}
				remove
				{
					_mock._handlerAnotherEvent0RemoveInvocation ??= new Invocation<System.EventHandler<string>?>("Class.HandlerAnotherEvent.remove -= {0}");
					_mock._handlerAnotherEvent0RemoveInvocation.Register(_mock._invocationIndex, value);
					_mock._handlerAnotherEvent0 -= value;
				}
			}
			""";

		const string verifyNoOtherCalls =
			"""
			_handlerEvent0AddInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_handlerEvent0RemoveInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_handlerAnotherEvent0AddInvocation?.VerifyNoOtherCalls(_invocationProviders);
			_handlerAnotherEvent0RemoveInvocation?.VerifyNoOtherCalls(_invocationProviders);
			""";

		const string invocations =
			"""
			yield return _handlerEvent0AddInvocation;
			yield return _handlerEvent0RemoveInvocation;
			yield return _handlerAnotherEvent0AddInvocation;
			yield return _handlerAnotherEvent0RemoveInvocation;
			""";

		var testCode = CreateClassTestCode(@event, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
