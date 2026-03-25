namespace MyNihongo.Mock.Tests.MethodTests;

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
				_invoke0Invocation ??= new Invocation("IInterface<T>.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation("IInterface<T>.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation("IInterface<T>.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				_mock._invoke0?.Invoke();
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invoke0Invocation ??= new Invocation("IInterface<T>.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation("IInterface<T>.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public decimal Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation("IInterface<T>.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				return _mock._invoke0?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<T>.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<T>.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<T>.Invoke<{key.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Invoke();
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invoke0Invocation ??= new Invocation("IInterface<T>.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation("IInterface<T>.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T Invoke()
			{
				_mock._invoke0Invocation ??= new Invocation("IInterface<T>.Invoke()");
				_mock._invoke0Invocation.Register(_mock._invocationIndex);
				return _mock._invoke0?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<T>.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<TReturn>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<T>.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public TReturn Invoke<TReturn>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<T>.Invoke<{key.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				return ((Setup<TReturn>?)_mock._invoke0?.ValueOrDefault(typeof(TReturn)))?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
