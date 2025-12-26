namespace MyNihongo.Mock.Tests.MethodTests;

public sealed class MethodNoParametersShould : MethodTestsBase
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
				_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public void Invoke() {}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

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
			private Setup? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup SetupInvoke()
			{
				_invoke0 ??= new Setup();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public void Invoke() {}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceReturnGeneric()
	{
		const string method = "TReturn Invoke<T, TReturn>();";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public Setup<TReturn> SetupInvoke<T, TReturn>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (Setup<TReturn>)_invoke0.GetOrAdd((typeof(T), typeof(TReturn)), static _ => new Setup<TReturn>());
				return invoke0;
			}

			public void VerifyInvoke<T, TReturn>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T), typeof(TReturn)), static key => new Invocation($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T), typeof(TReturn)), static key => new Invocation($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public TReturn Invoke<T, TReturn>() {}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithOutParameterReturn()
	{
		const string method = "decimal Invoke(out int result);";

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
				_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public void Invoke() {}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithOutParameterReturnGeneric()
	{
		const string method = "T2 Invoke<T1, T2>(out T1 result);";

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
				_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public void Invoke() {}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithOutParameter()
	{
		const string method = "void Invoke(out int result);";

		const string methods =
			"""
			// Invoke
			private SetupWithOutParameter<int>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithOutParameter<int> SetupInvoke(in ItOut<int> result)
			{
				_invoke0 ??= new SetupWithOutParameter<int>();
				return _invoke0;
			}

			public void VerifyInvoke(in ItOut<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				_invoke0Invocation.Verify(result, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				return _invoke0Invocation.Verify(result, index, _invocationProviders);
			}
			""";

		const string proxy = "public void Invoke(out int result) {result = default;}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithGenericParameter()
	{
		const string method = "void Invoke<T>();";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public Setup SetupInvoke<T>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invoke0 = _invoke0.GetOrAdd(typeof(T), static _ => new Setup());
				return invoke0;
			}

			public void VerifyInvoke<T>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.Invoke<{key.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<T>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.Invoke<{key.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public void Invoke<T>() {}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithGenericParameters()
	{
		const string method = "void Invoke<T1, T2>();";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), Setup>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public Setup SetupInvoke<T1, T2>()
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), Setup>();
				var invoke0 = _invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new Setup());
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				return invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public void Invoke<T1, T2>() {}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithGenericOutParameter()
	{
		const string method = "void Invoke<T>(out T value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithOutParameter<T> SetupInvoke<T>(in ItOut<T> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithOutParameter<T>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupWithOutParameter<T>());
				return invoke0;
			}

			public void VerifyInvoke<T>(in ItOut<T> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})", prefix: "out"));
				invoke0Invocation.Verify(value, times, _invocationProviders);
			}

			public long VerifyInvoke<T>(in ItOut<T> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})", prefix: "out"));
				return invoke0Invocation.Verify(value, index, _invocationProviders);
			}
			""";

		const string proxy = "public void Invoke<T>(out T value) {value = default;}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

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
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public System.Threading.Tasks.Task InvokeAsync() {return System.Threading.Tasks.Task.CompletedTask;}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
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
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public System.Threading.Tasks.Task<int> InvokeAsync() {return System.Threading.Tasks.Task.FromResult<int>(default);}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceTaskGeneric()
	{
		const string method = "Task InvokeAsync<T>();";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync<T>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invokeAsync0 = (Setup)_invokeAsync0.GetOrAdd(typeof(T), static _ => new Setup());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public System.Threading.Tasks.Task InvokeAsync<T>() {return System.Threading.Tasks.Task.CompletedTask;}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceTaskReturnGeneric()
	{
		const string method = "Task<T> InvokeAsync<T>();";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup<T> SetupInvokeAsync<T>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (Setup<T>)_invokeAsync0.GetOrAdd(typeof(T), static _ => new Setup<T>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public System.Threading.Tasks.Task<T> InvokeAsync<T>() {return System.Threading.Tasks.Task.FromResult<T>(default);}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
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
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public System.Threading.Tasks.ValueTask InvokeAsync() {return System.Threading.Tasks.ValueTask.CompletedTask;}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceValueTaskReturn()
	{
		const string method = "ValueTask<float> InvokeAsync();";

		const string methods =
			"""
			// InvokeAsync
			private Setup<float>? _invokeAsync0;
			private Invocation? _invokeAsync0Invocation;

			public Setup<float> SetupInvokeAsync()
			{
				_invokeAsync0 ??= new Setup<float>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public System.Threading.Tasks.ValueTask<float> InvokeAsync() {return System.Threading.Tasks.ValueTask.FromResult<float>(default);}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceValueTaskGeneric()
	{
		const string method = "ValueTask InvokeAsync<T>();";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public Setup SetupInvokeAsync<T>()
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, Setup>();
				var invokeAsync0 = (Setup)_invokeAsync0.GetOrAdd(typeof(T), static _ => new Setup());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T>(in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T>(long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public System.Threading.Tasks.ValueTask InvokeAsync<T>() {return System.Threading.Tasks.ValueTask.CompletedTask;}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceValueTaskReturnGeneric()
	{
		const string method = "ValueTask<T> InvokeAsync<T>();";

		const string methods =
			"""
			// Invoke
			private SetupWithOutParameter<int>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithOutParameter<int> SetupInvoke(in ItOut<int> result)
			{
				_invoke0 ??= new SetupWithOutParameter<int>();
				return _invoke0;
			}

			public void VerifyInvoke(in ItOut<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				_invoke0Invocation.Verify(result, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				return _invoke0Invocation.Verify(result, index, _invocationProviders);
			}
			""";

		const string proxy = "public void Invoke(out int result) {result = default;}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceTaskOut()
	{
		const string method = "Task InvokeAsync(out int result);";

		const string methods =
			"""
			// Invoke
			private SetupWithOutParameter<int>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithOutParameter<int> SetupInvoke(in ItOut<int> result)
			{
				_invoke0 ??= new SetupWithOutParameter<int>();
				return _invoke0;
			}

			public void VerifyInvoke(in ItOut<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				_invoke0Invocation.Verify(result, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				return _invoke0Invocation.Verify(result, index, _invocationProviders);
			}
			""";

		const string proxy = "public void Invoke(out int result) {result = default;}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceTaskGenericOut()
	{
		const string method = "Task InvokeAsync<T>(out T result);";

		const string methods =
			"""
			// Invoke
			private SetupWithOutParameter<int>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithOutParameter<int> SetupInvoke(in ItOut<int> result)
			{
				_invoke0 ??= new SetupWithOutParameter<int>();
				return _invoke0;
			}

			public void VerifyInvoke(in ItOut<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				_invoke0Invocation.Verify(result, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				return _invoke0Invocation.Verify(result, index, _invocationProviders);
			}
			""";

		const string proxy = "public void Invoke(out int result) {result = default;}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
