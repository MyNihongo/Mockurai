namespace MyNihongo.Mockurai.Tests.MethodTests;

public sealed class MethodNoParametersGenericShould : MethodGenericTestsBase
{
	[Fact]
	public async Task GenerateInterface()
	{
		const string method = "void Invoke();";

		const string methods =
			"""
			// Invoke
			private Setup? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup SetupInvoke()
			{
				_invoke0 ??= new Setup();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				_mock._invoke0?.Invoke();
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke() =>
				((InterfaceMock<T>)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceReturn()
	{
		const string method = "decimal Invoke();";

		const string methods =
			"""
			// Invoke
			private Setup<decimal>? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup<decimal> SetupInvoke()
			{
				_invoke0 ??= new Setup<decimal>();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public decimal Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				return _mock._invoke0?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, decimal, System.Func<decimal>> SetupInvoke() =>
				((InterfaceMock<T>)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceGeneric()
	{
		const string method = "void Invoke<TInvoke>();";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public Setup SetupInvoke<TInvoke>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invoke0 = (Setup)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new Setup());
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Invoke();
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.GetInvocations() ?? [];
			}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke<TInvoke>() =>
				((InterfaceMock<T>)@this).SetupInvoke<TInvoke>();

			public void VerifyInvoke<TInvoke>(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(times);

			public void VerifyInvoke<TInvoke>(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceReturnGeneric1()
	{
		const string method = "T Invoke();";

		const string methods =
			"""
			// Invoke
			private Setup<T>? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup<T> SetupInvoke()
			{
				_invoke0 ??= new Setup<T>();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				return _mock._invoke0?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, T, System.Func<T>> SetupInvoke() =>
				((InterfaceMock<T>)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceReturnGeneric2()
	{
		const string method = "TReturn Invoke<TReturn>();";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public Setup<TReturn> SetupInvoke<TReturn>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (Setup<TReturn>)_invoke0.GetOrAdd(typeof(TReturn), static _ => new Setup<TReturn>());
				return invoke0;
			}

			public void VerifyInvoke<TReturn>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<TReturn>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public TReturn Invoke<TReturn>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				return ((Setup<TReturn>?)_mock._invoke0?.ValueOrDefault(typeof(TReturn)))?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> Invoke<TReturn>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.GetInvocations() ?? [];
			}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, TReturn, System.Func<TReturn>> SetupInvoke<TReturn>() =>
				((InterfaceMock<T>)@this).SetupInvoke<TReturn>();

			public void VerifyInvoke<TReturn>(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TReturn>(times);

			public void VerifyInvoke<TReturn>(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TReturn>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TReturn>()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<TReturn>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClass()
	{
		const string method =
			"""
			public virtual void Invoke() {}
			protected virtual void Invoke2() {}
			public void Invoke3() {}
			""";

		const string methods =
			"""
			// Invoke
			private Setup? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup SetupInvoke()
			{
				_invoke0 ??= new Setup();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override void Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				_mock._invoke0?.Invoke();
			}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke() =>
				((ClassMock<T>)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((ClassMock<T>)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassReturn()
	{
		const string method =
			"""
			public virtual decimal Invoke() { return 0m; }
			protected virtual decimal Invoke2() { return 0m; }
			public decimal Invoke3() { return 12m; }
			""";

		const string methods =
			"""
			// Invoke
			private Setup<decimal>? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup<decimal> SetupInvoke()
			{
				_invoke0 ??= new Setup<decimal>();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override decimal Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				return _mock._invoke0?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, decimal, System.Func<decimal>> SetupInvoke() =>
				((ClassMock<T>)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((ClassMock<T>)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstract()
	{
		const string method =
			"""
			public abstract void Invoke();
			protected abstract void Invoke2();
			public void Invoke3() {}
			""";

		const string methods =
			"""
			// Invoke
			private Setup? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup SetupInvoke()
			{
				_invoke0 ??= new Setup();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override void Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				_mock._invoke0?.Invoke();
			}

			protected override void Invoke2() {}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke() =>
				((ClassMock<T>)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((ClassMock<T>)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractReturn()
	{
		const string method =
			"""
			public abstract int Invoke();
			protected abstract decimal Invoke2();
			public decimal Invoke3() { return 12m; }
			""";

		const string methods =
			"""
			// Invoke
			private Setup<int>? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup<int> SetupInvoke()
			{
				_invoke0 ??= new Setup<int>();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override int Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				return _mock._invoke0?.Execute(out var returnValue) == true ? returnValue! : default!;
			}

			protected override decimal Invoke2() {return default!;}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, int, System.Func<int>> SetupInvoke() =>
				((ClassMock<T>)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((ClassMock<T>)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassGeneric()
	{
		const string method =
			"""
			public virtual void Invoke<TInvoke>() {}
			protected virtual void Invoke2<TInvoke>() {}
			public void Invoke3<TInvoke>() {}
			""";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public Setup SetupInvoke<TInvoke>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invoke0 = (Setup)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new Setup());
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override void Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Invoke();
			}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke<TInvoke>() =>
				((ClassMock<T>)@this).SetupInvoke<TInvoke>();

			public void VerifyInvoke<TInvoke>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvoke<TInvoke>(times);

			public void VerifyInvoke<TInvoke>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvoke<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvoke<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassReturnGeneric()
	{
		const string method =
			"""
			public virtual decimal Invoke<TInvoke>() { return 0m; }
			protected virtual decimal Invoke2<TInvoke>() { return 0m; }
			public decimal Invoke3<TInvoke>() { return 12m; }
			""";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public Setup<decimal> SetupInvoke<TInvoke>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (Setup<decimal>)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new Setup<decimal>());
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override decimal Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				return ((Setup<decimal>?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, decimal, System.Func<decimal>> SetupInvoke<TInvoke>() =>
				((ClassMock<T>)@this).SetupInvoke<TInvoke>();

			public void VerifyInvoke<TInvoke>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvoke<TInvoke>(times);

			public void VerifyInvoke<TInvoke>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvoke<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvoke<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractGeneric()
	{
		const string method =
			"""
			public abstract void Invoke<TInvoke>();
			protected abstract void Invoke2<TInvoke>();
			public void Invoke3<TInvoke>() {}
			""";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public Setup SetupInvoke<TInvoke>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invoke0 = (Setup)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new Setup());
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override void Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Invoke();
			}

			protected override void Invoke2<TInvoke>() {}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action> SetupInvoke<TInvoke>() =>
				((ClassMock<T>)@this).SetupInvoke<TInvoke>();

			public void VerifyInvoke<TInvoke>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvoke<TInvoke>(times);

			public void VerifyInvoke<TInvoke>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvoke<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvoke<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractReturnGeneric()
	{
		const string method =
			"""
			public abstract int Invoke<TInvoke>();
			protected abstract decimal Invoke2<TInvoke>();
			public decimal Invoke3<TInvoke>() { return 12m; }
			""";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public Setup<int> SetupInvoke<TInvoke>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (Setup<int>)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new Setup<int>());
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override int Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				return ((Setup<int>?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Execute(out var returnValue) == true ? returnValue! : default!;
			}

			protected override decimal Invoke2<TInvoke>() {return default!;}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, int, System.Func<int>> SetupInvoke<TInvoke>() =>
				((ClassMock<T>)@this).SetupInvoke<TInvoke>();

			public void VerifyInvoke<TInvoke>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvoke<TInvoke>(times);

			public void VerifyInvoke<TInvoke>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvoke<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvoke<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassReturnGeneric1()
	{
		const string method =
			"""
			public virtual T Invoke() { return default!; }
			protected virtual T Invoke2() { return default!; }
			public T Invoke3() { return default!; }
			""";

		const string methods =
			"""
			// Invoke
			private Setup<T>? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup<T> SetupInvoke()
			{
				_invoke0 ??= new Setup<T>();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override T Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				return _mock._invoke0?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, T, System.Func<T>> SetupInvoke() =>
				((ClassMock<T>)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((ClassMock<T>)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractReturnGeneric1()
	{
		const string method =
			"""
			public abstract T Invoke();
			protected abstract T Invoke2();
			public T Invoke3() { return default!; }
			""";

		const string methods =
			"""
			// Invoke
			private Setup<T>? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup<T> SetupInvoke()
			{
				_invoke0 ??= new Setup<T>();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override T Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				return _mock._invoke0?.Execute(out var returnValue) == true ? returnValue! : default!;
			}

			protected override T Invoke2() {return default!;}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, T, System.Func<T>> SetupInvoke() =>
				((ClassMock<T>)@this).SetupInvoke();

			public void VerifyInvoke(in Times times) =>
				((ClassMock<T>)@this).VerifyInvoke(times);

			public void VerifyInvoke(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvoke(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvoke(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> Invoke => _mock._invoke0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassReturnGeneric2()
	{
		const string method =
			"""
			public virtual TReturn Invoke<TReturn>() { return default!; }
			protected virtual TReturn Invoke2<TReturn>() { return default!; }
			public TReturn Invoke3<TReturn>() { return default!; }
			""";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public Setup<TReturn> SetupInvoke<TReturn>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (Setup<TReturn>)_invoke0.GetOrAdd(typeof(TReturn), static _ => new Setup<TReturn>());
				return invoke0;
			}

			public void VerifyInvoke<TReturn>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<TReturn>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override TReturn Invoke<TReturn>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				return ((Setup<TReturn>?)_mock._invoke0?.ValueOrDefault(typeof(TReturn)))?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, TReturn, System.Func<TReturn>> SetupInvoke<TReturn>() =>
				((ClassMock<T>)@this).SetupInvoke<TReturn>();

			public void VerifyInvoke<TReturn>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvoke<TReturn>(times);

			public void VerifyInvoke<TReturn>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvoke<TReturn>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TReturn>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvoke<TReturn>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> Invoke<TReturn>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractReturnGeneric2()
	{
		const string method =
			"""
			public abstract TReturn Invoke<TReturn>();
			protected abstract TReturn Invoke2<TReturn>();
			public TReturn Invoke3<TReturn>() { return default!; }
			""";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public Setup<TReturn> SetupInvoke<TReturn>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (Setup<TReturn>)_invoke0.GetOrAdd(typeof(TReturn), static _ => new Setup<TReturn>());
				return invoke0;
			}

			public void VerifyInvoke<TReturn>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<TReturn>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override TReturn Invoke<TReturn>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				return ((Setup<TReturn>?)_mock._invoke0?.ValueOrDefault(typeof(TReturn)))?.Execute(out var returnValue) == true ? returnValue! : default!;
			}

			protected override TReturn Invoke2<TReturn>() {return default!;}
			""";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action, TReturn, System.Func<TReturn>> SetupInvoke<TReturn>() =>
				((ClassMock<T>)@this).SetupInvoke<TReturn>();

			public void VerifyInvoke<TReturn>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvoke<TReturn>(times);

			public void VerifyInvoke<TReturn>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvoke<TReturn>(times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TReturn>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvoke<TReturn>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";
		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> Invoke<TReturn>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.Invoke<{key.Name}>()"));
				return invoke0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}
}
