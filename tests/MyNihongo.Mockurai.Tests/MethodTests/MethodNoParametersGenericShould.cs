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
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
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
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
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
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
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
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
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
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
