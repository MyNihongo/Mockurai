namespace MyNihongo.Mockurai.Tests.MethodTests;

public sealed class MethodNoParametersGenericAsyncShould : MethodGenericTestsBase
{
	[Fact]
	public async Task GenerateInterfaceTask()
	{
		const string method = "Task InvokeAsync();";

		const string methods =
			"""
			// InvokeAsync
			private Setup? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				_mock._invokeAsync0?.Invoke();
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync() =>
				((InterfaceMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceTaskReturn()
	{
		const string method = "Task<int> InvokeAsync();";

		const string methods =
			"""
			// InvokeAsync
			private Setup<int>? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup<int> SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup<int>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<int> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.Task.FromResult<int>(_mock._invokeAsync0?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, int, System.Func<int>> SetupInvokeAsync() =>
				((InterfaceMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceTaskGeneric()
	{
		const string method = "Task InvokeAsync<TInvoke>();";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync<TInvoke>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invokeAsync0 = (Setup)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new Setup());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke();
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync<TInvoke>() =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>();

			public void VerifyInvokeAsync<TInvoke>(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(times);

			public void VerifyInvokeAsync<TInvoke>(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceTaskReturnGeneric1()
	{
		const string method = "Task<T> InvokeAsync();";

		const string methods =
			"""
			// InvokeAsync
			private Setup<T>? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup<T> SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup<T>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.Task.FromResult<T>(_mock._invokeAsync0?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, T, System.Func<T>> SetupInvokeAsync() =>
				((InterfaceMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceTaskReturnGeneric2()
	{
		const string method = "Task<TReturn> InvokeAsync<TReturn>();";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup<TReturn> SetupInvokeAsync<TReturn>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (Setup<TReturn>)_invokeAsync0.GetOrAdd(typeof(TReturn), static _ => new Setup<TReturn>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TReturn>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TReturn>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<TReturn> InvokeAsync<TReturn>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.Task.FromResult<TReturn>(((Setup<TReturn>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TReturn)))?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, TReturn, System.Func<TReturn>> SetupInvokeAsync<TReturn>() =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TReturn>();

			public void VerifyInvokeAsync<TReturn>(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TReturn>(times);

			public void VerifyInvokeAsync<TReturn>(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TReturn>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TReturn>()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TReturn>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TReturn>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceValueTask()
	{
		const string method = "ValueTask InvokeAsync();";

		const string methods =
			"""
			// InvokeAsync
			private Setup? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				_mock._invokeAsync0?.Invoke();
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync() =>
				((InterfaceMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceValueTaskReturn()
	{
		const string method = "ValueTask<int> InvokeAsync();";

		const string methods =
			"""
			// InvokeAsync
			private Setup<int>? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup<int> SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup<int>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<int> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.ValueTask.FromResult<int>(_mock._invokeAsync0?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, int, System.Func<int>> SetupInvokeAsync() =>
				((InterfaceMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceValueTaskGeneric()
	{
		const string method = "ValueTask InvokeAsync<TInvoke>();";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync<TInvoke>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invokeAsync0 = (Setup)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new Setup());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke();
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync<TInvoke>() =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>();

			public void VerifyInvokeAsync<TInvoke>(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(times);

			public void VerifyInvokeAsync<TInvoke>(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceValueTaskReturnGeneric1()
	{
		const string method = "ValueTask<T> InvokeAsync();";

		const string methods =
			"""
			// InvokeAsync
			private Setup<T>? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup<T> SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup<T>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.ValueTask.FromResult<T>(_mock._invokeAsync0?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, T, System.Func<T>> SetupInvokeAsync() =>
				((InterfaceMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceValueTaskReturnGeneric2()
	{
		const string method = "ValueTask<TReturn> InvokeAsync<TReturn>();";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup<TReturn> SetupInvokeAsync<TReturn>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (Setup<TReturn>)_invokeAsync0.GetOrAdd(typeof(TReturn), static _ => new Setup<TReturn>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TReturn>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TReturn>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<TReturn> InvokeAsync<TReturn>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.ValueTask.FromResult<TReturn>(((Setup<TReturn>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TReturn)))?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, TReturn, System.Func<TReturn>> SetupInvokeAsync<TReturn>() =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TReturn>();

			public void VerifyInvokeAsync<TReturn>(in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TReturn>(times);

			public void VerifyInvokeAsync<TReturn>(System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TReturn>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TReturn>()
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TReturn>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TReturn>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassTask()
	{
		const string method =
			"""
			public virtual System.Threading.Tasks.Task InvokeAsync() { return System.Threading.Tasks.Task.CompletedTask; }
			protected virtual System.Threading.Tasks.Task InvokeAsync2() { return System.Threading.Tasks.Task.CompletedTask; }
			public System.Threading.Tasks.Task InvokeAsync3() { return System.Threading.Tasks.Task.CompletedTask; }
			""";

		const string methods =
			"""
			// InvokeAsync
			private Setup? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.Task InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				_mock._invokeAsync0?.Invoke();
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync() =>
				((ClassMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassTaskReturn()
	{
		const string method =
			"""
			public virtual System.Threading.Tasks.Task<decimal> InvokeAsync() { return System.Threading.Tasks.Task.FromResult(0m); }
			protected virtual System.Threading.Tasks.Task<decimal> InvokeAsync2() { return System.Threading.Tasks.Task.FromResult(0m); }
			public System.Threading.Tasks.Task<decimal> InvokeAsync3() { return System.Threading.Tasks.Task.FromResult(12m); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private Setup<decimal>? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup<decimal> SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup<decimal>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.Task<decimal> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.Task.FromResult<decimal>(_mock._invokeAsync0?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, decimal, System.Func<decimal>> SetupInvokeAsync() =>
				((ClassMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractTask()
	{
		const string method =
			"""
			public abstract System.Threading.Tasks.Task InvokeAsync();
			protected abstract System.Threading.Tasks.Task InvokeAsync2();
			public System.Threading.Tasks.Task InvokeAsync3() { return System.Threading.Tasks.Task.CompletedTask; }
			""";

		const string methods =
			"""
			// InvokeAsync
			private Setup? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.Task InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				_mock._invokeAsync0?.Invoke();
				return System.Threading.Tasks.Task.CompletedTask;
			}

			protected override System.Threading.Tasks.Task InvokeAsync2() {return System.Threading.Tasks.Task.CompletedTask;}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync() =>
				((ClassMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractTaskReturn()
	{
		const string method =
			"""
			public abstract System.Threading.Tasks.Task<int> InvokeAsync();
			protected abstract System.Threading.Tasks.Task<decimal> InvokeAsync2();
			public System.Threading.Tasks.Task<decimal> InvokeAsync3() { return System.Threading.Tasks.Task.FromResult(12m); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private Setup<int>? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup<int> SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup<int>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.Task<int> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.Task.FromResult<int>(_mock._invokeAsync0?.Execute(out var returnValue) == true ? returnValue! : default!);
			}

			protected override System.Threading.Tasks.Task<decimal> InvokeAsync2() {return System.Threading.Tasks.Task.FromResult<decimal>(default!);}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, int, System.Func<int>> SetupInvokeAsync() =>
				((ClassMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassTaskGeneric()
	{
		const string method =
			"""
			public virtual System.Threading.Tasks.Task InvokeAsync<TInvoke>() { return System.Threading.Tasks.Task.CompletedTask; }
			protected virtual System.Threading.Tasks.Task InvokeAsync2<TInvoke>() { return System.Threading.Tasks.Task.CompletedTask; }
			public System.Threading.Tasks.Task InvokeAsync3<TInvoke>() { return System.Threading.Tasks.Task.CompletedTask; }
			""";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync<TInvoke>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invokeAsync0 = (Setup)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new Setup());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.Task InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke();
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync<TInvoke>() =>
				((ClassMock<T>)@this).SetupInvokeAsync<TInvoke>();

			public void VerifyInvokeAsync<TInvoke>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times);

			public void VerifyInvokeAsync<TInvoke>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassTaskReturnGeneric()
	{
		const string method =
			"""
			public virtual System.Threading.Tasks.Task<decimal> InvokeAsync<TInvoke>() { return System.Threading.Tasks.Task.FromResult(0m); }
			protected virtual System.Threading.Tasks.Task<decimal> InvokeAsync2<TInvoke>() { return System.Threading.Tasks.Task.FromResult(0m); }
			public System.Threading.Tasks.Task<decimal> InvokeAsync3<TInvoke>() { return System.Threading.Tasks.Task.FromResult(12m); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup<decimal> SetupInvokeAsync<TInvoke>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (Setup<decimal>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new Setup<decimal>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.Task<decimal> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.Task.FromResult<decimal>(((Setup<decimal>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, decimal, System.Func<decimal>> SetupInvokeAsync<TInvoke>() =>
				((ClassMock<T>)@this).SetupInvokeAsync<TInvoke>();

			public void VerifyInvokeAsync<TInvoke>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times);

			public void VerifyInvokeAsync<TInvoke>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractTaskGeneric()
	{
		const string method =
			"""
			public abstract System.Threading.Tasks.Task InvokeAsync<TInvoke>();
			protected abstract System.Threading.Tasks.Task InvokeAsync2<TInvoke>();
			public System.Threading.Tasks.Task InvokeAsync3<TInvoke>() { return System.Threading.Tasks.Task.CompletedTask; }
			""";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync<TInvoke>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invokeAsync0 = (Setup)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new Setup());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.Task InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke();
				return System.Threading.Tasks.Task.CompletedTask;
			}

			protected override System.Threading.Tasks.Task InvokeAsync2<TInvoke>() {return System.Threading.Tasks.Task.CompletedTask;}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync<TInvoke>() =>
				((ClassMock<T>)@this).SetupInvokeAsync<TInvoke>();

			public void VerifyInvokeAsync<TInvoke>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times);

			public void VerifyInvokeAsync<TInvoke>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractTaskReturnGeneric()
	{
		const string method =
			"""
			public abstract System.Threading.Tasks.Task<int> InvokeAsync<TInvoke>();
			protected abstract System.Threading.Tasks.Task<decimal> InvokeAsync2<TInvoke>();
			public System.Threading.Tasks.Task<decimal> InvokeAsync3<TInvoke>() { return System.Threading.Tasks.Task.FromResult(12m); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup<int> SetupInvokeAsync<TInvoke>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (Setup<int>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new Setup<int>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.Task<int> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.Task.FromResult<int>(((Setup<int>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Execute(out var returnValue) == true ? returnValue! : default!);
			}

			protected override System.Threading.Tasks.Task<decimal> InvokeAsync2<TInvoke>() {return System.Threading.Tasks.Task.FromResult<decimal>(default!);}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, int, System.Func<int>> SetupInvokeAsync<TInvoke>() =>
				((ClassMock<T>)@this).SetupInvokeAsync<TInvoke>();

			public void VerifyInvokeAsync<TInvoke>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times);

			public void VerifyInvokeAsync<TInvoke>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassTaskReturnGeneric1()
	{
		const string method =
			"""
			public virtual System.Threading.Tasks.Task<T> InvokeAsync() { return System.Threading.Tasks.Task.FromResult<T>(default!); }
			protected virtual System.Threading.Tasks.Task<T> InvokeAsync2() { return System.Threading.Tasks.Task.FromResult<T>(default!); }
			public System.Threading.Tasks.Task<T> InvokeAsync3() { return System.Threading.Tasks.Task.FromResult<T>(default!); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private Setup<T>? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup<T> SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup<T>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.Task<T> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.Task.FromResult<T>(_mock._invokeAsync0?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, T, System.Func<T>> SetupInvokeAsync() =>
				((ClassMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractTaskReturnGeneric1()
	{
		const string method =
			"""
			public abstract System.Threading.Tasks.Task<T> InvokeAsync();
			protected abstract System.Threading.Tasks.Task<T> InvokeAsync2();
			public System.Threading.Tasks.Task<T> InvokeAsync3() { return System.Threading.Tasks.Task.FromResult<T>(default!); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private Setup<T>? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup<T> SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup<T>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.Task<T> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.Task.FromResult<T>(_mock._invokeAsync0?.Execute(out var returnValue) == true ? returnValue! : default!);
			}

			protected override System.Threading.Tasks.Task<T> InvokeAsync2() {return System.Threading.Tasks.Task.FromResult<T>(default!);}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, T, System.Func<T>> SetupInvokeAsync() =>
				((ClassMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassTaskReturnGeneric2()
	{
		const string method =
			"""
			public virtual System.Threading.Tasks.Task<TReturn> InvokeAsync<TReturn>() { return System.Threading.Tasks.Task.FromResult<TReturn>(default!); }
			protected virtual System.Threading.Tasks.Task<TReturn> InvokeAsync2<TReturn>() { return System.Threading.Tasks.Task.FromResult<TReturn>(default!); }
			public System.Threading.Tasks.Task<TReturn> InvokeAsync3<TReturn>() { return System.Threading.Tasks.Task.FromResult<TReturn>(default!); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup<TReturn> SetupInvokeAsync<TReturn>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (Setup<TReturn>)_invokeAsync0.GetOrAdd(typeof(TReturn), static _ => new Setup<TReturn>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TReturn>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TReturn>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.Task<TReturn> InvokeAsync<TReturn>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.Task.FromResult<TReturn>(((Setup<TReturn>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TReturn)))?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, TReturn, System.Func<TReturn>> SetupInvokeAsync<TReturn>() =>
				((ClassMock<T>)@this).SetupInvokeAsync<TReturn>();

			public void VerifyInvokeAsync<TReturn>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TReturn>(times);

			public void VerifyInvokeAsync<TReturn>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TReturn>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TReturn>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync<TReturn>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TReturn>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractTaskReturnGeneric2()
	{
		const string method =
			"""
			public abstract System.Threading.Tasks.Task<TReturn> InvokeAsync<TReturn>();
			protected abstract System.Threading.Tasks.Task<TReturn> InvokeAsync2<TReturn>();
			public System.Threading.Tasks.Task<TReturn> InvokeAsync3<TReturn>() { return System.Threading.Tasks.Task.FromResult<TReturn>(default!); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup<TReturn> SetupInvokeAsync<TReturn>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (Setup<TReturn>)_invokeAsync0.GetOrAdd(typeof(TReturn), static _ => new Setup<TReturn>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TReturn>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TReturn>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.Task<TReturn> InvokeAsync<TReturn>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.Task.FromResult<TReturn>(((Setup<TReturn>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TReturn)))?.Execute(out var returnValue) == true ? returnValue! : default!);
			}

			protected override System.Threading.Tasks.Task<TReturn> InvokeAsync2<TReturn>() {return System.Threading.Tasks.Task.FromResult<TReturn>(default!);}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, TReturn, System.Func<TReturn>> SetupInvokeAsync<TReturn>() =>
				((ClassMock<T>)@this).SetupInvokeAsync<TReturn>();

			public void VerifyInvokeAsync<TReturn>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TReturn>(times);

			public void VerifyInvokeAsync<TReturn>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TReturn>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TReturn>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync<TReturn>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TReturn>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassValueTask()
	{
		const string method =
			"""
			public virtual System.Threading.Tasks.ValueTask InvokeAsync() { return System.Threading.Tasks.ValueTask.CompletedTask; }
			protected virtual System.Threading.Tasks.ValueTask InvokeAsync2() { return System.Threading.Tasks.ValueTask.CompletedTask; }
			public System.Threading.Tasks.ValueTask InvokeAsync3() { return System.Threading.Tasks.ValueTask.CompletedTask; }
			""";

		const string methods =
			"""
			// InvokeAsync
			private Setup? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.ValueTask InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				_mock._invokeAsync0?.Invoke();
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync() =>
				((ClassMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassValueTaskReturn()
	{
		const string method =
			"""
			public virtual System.Threading.Tasks.ValueTask<decimal> InvokeAsync() { return System.Threading.Tasks.ValueTask.FromResult(0m); }
			protected virtual System.Threading.Tasks.ValueTask<decimal> InvokeAsync2() { return System.Threading.Tasks.ValueTask.FromResult(0m); }
			public System.Threading.Tasks.ValueTask<decimal> InvokeAsync3() { return System.Threading.Tasks.ValueTask.FromResult(12m); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private Setup<decimal>? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup<decimal> SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup<decimal>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.ValueTask<decimal> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.ValueTask.FromResult<decimal>(_mock._invokeAsync0?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, decimal, System.Func<decimal>> SetupInvokeAsync() =>
				((ClassMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractValueTask()
	{
		const string method =
			"""
			public abstract System.Threading.Tasks.ValueTask InvokeAsync();
			protected abstract System.Threading.Tasks.ValueTask InvokeAsync2();
			public System.Threading.Tasks.ValueTask InvokeAsync3() { return System.Threading.Tasks.ValueTask.CompletedTask; }
			""";

		const string methods =
			"""
			// InvokeAsync
			private Setup? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.ValueTask InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				_mock._invokeAsync0?.Invoke();
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}

			protected override System.Threading.Tasks.ValueTask InvokeAsync2() {return System.Threading.Tasks.ValueTask.CompletedTask;}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync() =>
				((ClassMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractValueTaskReturn()
	{
		const string method =
			"""
			public abstract System.Threading.Tasks.ValueTask<int> InvokeAsync();
			protected abstract System.Threading.Tasks.ValueTask<decimal> InvokeAsync2();
			public System.Threading.Tasks.ValueTask<decimal> InvokeAsync3() { return System.Threading.Tasks.ValueTask.FromResult(12m); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private Setup<int>? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup<int> SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup<int>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.ValueTask<int> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.ValueTask.FromResult<int>(_mock._invokeAsync0?.Execute(out var returnValue) == true ? returnValue! : default!);
			}

			protected override System.Threading.Tasks.ValueTask<decimal> InvokeAsync2() {return System.Threading.Tasks.ValueTask.FromResult<decimal>(default!);}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, int, System.Func<int>> SetupInvokeAsync() =>
				((ClassMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassValueTaskGeneric()
	{
		const string method =
			"""
			public virtual System.Threading.Tasks.ValueTask InvokeAsync<TInvoke>() { return System.Threading.Tasks.ValueTask.CompletedTask; }
			protected virtual System.Threading.Tasks.ValueTask InvokeAsync2<TInvoke>() { return System.Threading.Tasks.ValueTask.CompletedTask; }
			public System.Threading.Tasks.ValueTask InvokeAsync3<TInvoke>() { return System.Threading.Tasks.ValueTask.CompletedTask; }
			""";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync<TInvoke>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invokeAsync0 = (Setup)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new Setup());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.ValueTask InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke();
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync<TInvoke>() =>
				((ClassMock<T>)@this).SetupInvokeAsync<TInvoke>();

			public void VerifyInvokeAsync<TInvoke>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times);

			public void VerifyInvokeAsync<TInvoke>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassValueTaskReturnGeneric()
	{
		const string method =
			"""
			public virtual System.Threading.Tasks.ValueTask<decimal> InvokeAsync<TInvoke>() { return System.Threading.Tasks.ValueTask.FromResult(0m); }
			protected virtual System.Threading.Tasks.ValueTask<decimal> InvokeAsync2<TInvoke>() { return System.Threading.Tasks.ValueTask.FromResult(0m); }
			public System.Threading.Tasks.ValueTask<decimal> InvokeAsync3<TInvoke>() { return System.Threading.Tasks.ValueTask.FromResult(12m); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup<decimal> SetupInvokeAsync<TInvoke>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (Setup<decimal>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new Setup<decimal>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.ValueTask<decimal> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.ValueTask.FromResult<decimal>(((Setup<decimal>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, decimal, System.Func<decimal>> SetupInvokeAsync<TInvoke>() =>
				((ClassMock<T>)@this).SetupInvokeAsync<TInvoke>();

			public void VerifyInvokeAsync<TInvoke>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times);

			public void VerifyInvokeAsync<TInvoke>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractValueTaskGeneric()
	{
		const string method =
			"""
			public abstract System.Threading.Tasks.ValueTask InvokeAsync<TInvoke>();
			protected abstract System.Threading.Tasks.ValueTask InvokeAsync2<TInvoke>();
			public System.Threading.Tasks.ValueTask InvokeAsync3<TInvoke>() { return System.Threading.Tasks.ValueTask.CompletedTask; }
			""";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync<TInvoke>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invokeAsync0 = (Setup)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new Setup());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.ValueTask InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke();
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}

			protected override System.Threading.Tasks.ValueTask InvokeAsync2<TInvoke>() {return System.Threading.Tasks.ValueTask.CompletedTask;}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync<TInvoke>() =>
				((ClassMock<T>)@this).SetupInvokeAsync<TInvoke>();

			public void VerifyInvokeAsync<TInvoke>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times);

			public void VerifyInvokeAsync<TInvoke>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractValueTaskReturnGeneric()
	{
		const string method =
			"""
			public abstract System.Threading.Tasks.ValueTask<int> InvokeAsync<TInvoke>();
			protected abstract System.Threading.Tasks.ValueTask<decimal> InvokeAsync2<TInvoke>();
			public System.Threading.Tasks.ValueTask<decimal> InvokeAsync3<TInvoke>() { return System.Threading.Tasks.ValueTask.FromResult(12m); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup<int> SetupInvokeAsync<TInvoke>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (Setup<int>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new Setup<int>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.ValueTask<int> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.ValueTask.FromResult<int>(((Setup<int>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Execute(out var returnValue) == true ? returnValue! : default!);
			}

			protected override System.Threading.Tasks.ValueTask<decimal> InvokeAsync2<TInvoke>() {return System.Threading.Tasks.ValueTask.FromResult<decimal>(default!);}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, int, System.Func<int>> SetupInvokeAsync<TInvoke>() =>
				((ClassMock<T>)@this).SetupInvokeAsync<TInvoke>();

			public void VerifyInvokeAsync<TInvoke>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times);

			public void VerifyInvokeAsync<TInvoke>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TInvoke>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TInvoke>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassValueTaskReturnGeneric1()
	{
		const string method =
			"""
			public virtual System.Threading.Tasks.ValueTask<T> InvokeAsync() { return System.Threading.Tasks.ValueTask.FromResult<T>(default!); }
			protected virtual System.Threading.Tasks.ValueTask<T> InvokeAsync2() { return System.Threading.Tasks.ValueTask.FromResult<T>(default!); }
			public System.Threading.Tasks.ValueTask<T> InvokeAsync3() { return System.Threading.Tasks.ValueTask.FromResult<T>(default!); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private Setup<T>? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup<T> SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup<T>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.ValueTask<T> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.ValueTask.FromResult<T>(_mock._invokeAsync0?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, T, System.Func<T>> SetupInvokeAsync() =>
				((ClassMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractValueTaskReturnGeneric1()
	{
		const string method =
			"""
			public abstract System.Threading.Tasks.ValueTask<T> InvokeAsync();
			protected abstract System.Threading.Tasks.ValueTask<T> InvokeAsync2();
			public System.Threading.Tasks.ValueTask<T> InvokeAsync3() { return System.Threading.Tasks.ValueTask.FromResult<T>(default!); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private Setup<T>? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup<T> SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup<T>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.ValueTask<T> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation($"Class<{typeof(T).Name}>.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.ValueTask.FromResult<T>(_mock._invokeAsync0?.Execute(out var returnValue) == true ? returnValue! : default!);
			}

			protected override System.Threading.Tasks.ValueTask<T> InvokeAsync2() {return System.Threading.Tasks.ValueTask.FromResult<T>(default!);}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, T, System.Func<T>> SetupInvokeAsync() =>
				((ClassMock<T>)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync => _mock._invokeAsync0Invocation?.GetInvocations() ?? [];";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassValueTaskReturnGeneric2()
	{
		const string method =
			"""
			public virtual System.Threading.Tasks.ValueTask<TReturn> InvokeAsync<TReturn>() { return System.Threading.Tasks.ValueTask.FromResult<TReturn>(default!); }
			protected virtual System.Threading.Tasks.ValueTask<TReturn> InvokeAsync2<TReturn>() { return System.Threading.Tasks.ValueTask.FromResult<TReturn>(default!); }
			public System.Threading.Tasks.ValueTask<TReturn> InvokeAsync3<TReturn>() { return System.Threading.Tasks.ValueTask.FromResult<TReturn>(default!); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup<TReturn> SetupInvokeAsync<TReturn>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (Setup<TReturn>)_invokeAsync0.GetOrAdd(typeof(TReturn), static _ => new Setup<TReturn>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TReturn>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TReturn>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.ValueTask<TReturn> InvokeAsync<TReturn>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.ValueTask.FromResult<TReturn>(((Setup<TReturn>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TReturn)))?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, TReturn, System.Func<TReturn>> SetupInvokeAsync<TReturn>() =>
				((ClassMock<T>)@this).SetupInvokeAsync<TReturn>();

			public void VerifyInvokeAsync<TReturn>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TReturn>(times);

			public void VerifyInvokeAsync<TReturn>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TReturn>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TReturn>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync<TReturn>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TReturn>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateClassAbstractValueTaskReturnGeneric2()
	{
		const string method =
			"""
			public abstract System.Threading.Tasks.ValueTask<TReturn> InvokeAsync<TReturn>();
			protected abstract System.Threading.Tasks.ValueTask<TReturn> InvokeAsync2<TReturn>();
			public System.Threading.Tasks.ValueTask<TReturn> InvokeAsync3<TReturn>() { return System.Threading.Tasks.ValueTask.FromResult<TReturn>(default!); }
			""";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup<TReturn> SetupInvokeAsync<TReturn>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (Setup<TReturn>)_invokeAsync0.GetOrAdd(typeof(TReturn), static _ => new Setup<TReturn>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TReturn>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TReturn>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override System.Threading.Tasks.ValueTask<TReturn> InvokeAsync<TReturn>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.ValueTask.FromResult<TReturn>(((Setup<TReturn>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TReturn)))?.Execute(out var returnValue) == true ? returnValue! : default!);
			}

			protected override System.Threading.Tasks.ValueTask<TReturn> InvokeAsync2<TReturn>() {return System.Threading.Tasks.ValueTask.FromResult<TReturn>(default!);}
			""";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, TReturn, System.Func<TReturn>> SetupInvokeAsync<TReturn>() =>
				((ClassMock<T>)@this).SetupInvokeAsync<TReturn>();

			public void VerifyInvokeAsync<TReturn>(in Times times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TReturn>(times);

			public void VerifyInvokeAsync<TReturn>(System.Func<Times> times) =>
				((ClassMock<T>)@this).VerifyInvokeAsync<TReturn>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TReturn>()
			{
				var nextIndex = ((ClassMock<T>)@this.Mock).VerifyInvokeAsync<TReturn>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<TReturn>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TReturn), static key => new Invocation($"Class<{typeof(T).Name}>.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateClassTestCode(method, isAbstract: true);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}
}
