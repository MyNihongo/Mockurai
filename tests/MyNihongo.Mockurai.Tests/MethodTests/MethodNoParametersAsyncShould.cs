namespace MyNihongo.Mockurai.Tests.MethodTests;

public sealed class MethodNoParametersAsyncShould : MethodTestsBase
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
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
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
				((InterfaceMock)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((InterfaceMock)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
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
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<int> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
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
				((InterfaceMock)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((InterfaceMock)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
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

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<T>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invokeAsync0?.ValueOrDefault(typeof(T)))?.Invoke();
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync<T>() =>
				((InterfaceMock)@this).SetupInvokeAsync<T>();

			public void VerifyInvokeAsync<T>(in Times times) =>
				((InterfaceMock)@this).VerifyInvokeAsync<T>(times);

			public void VerifyInvokeAsync<T>(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyInvokeAsync<T>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T>()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvokeAsync<T>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<T>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
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

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T> InvokeAsync<T>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.Task.FromResult<T>(((Setup<T>?)_mock._invokeAsync0?.ValueOrDefault(typeof(T)))?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, T, System.Func<T>> SetupInvokeAsync<T>() =>
				((InterfaceMock)@this).SetupInvokeAsync<T>();

			public void VerifyInvokeAsync<T>(in Times times) =>
				((InterfaceMock)@this).VerifyInvokeAsync<T>(times);

			public void VerifyInvokeAsync<T>(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyInvokeAsync<T>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T>()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvokeAsync<T>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<T>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
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
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				_invokeAsync0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvokeAsync(long index)
			{
				_invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				return _invokeAsync0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
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
				((InterfaceMock)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((InterfaceMock)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
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

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<float> InvokeAsync()
			{
				_mock._invokeAsync0Invocation ??= new Invocation("IInterface.InvokeAsync()");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.ValueTask.FromResult<float>(_mock._invokeAsync0?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, float, System.Func<float>> SetupInvokeAsync() =>
				((InterfaceMock)@this).SetupInvokeAsync();

			public void VerifyInvokeAsync(in Times times) =>
				((InterfaceMock)@this).VerifyInvokeAsync(times);

			public void VerifyInvokeAsync(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyInvokeAsync(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
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

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<T>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invokeAsync0?.ValueOrDefault(typeof(T)))?.Invoke();
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action> SetupInvokeAsync<T>() =>
				((InterfaceMock)@this).SetupInvokeAsync<T>();

			public void VerifyInvokeAsync<T>(in Times times) =>
				((InterfaceMock)@this).VerifyInvokeAsync<T>(times);

			public void VerifyInvokeAsync<T>(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyInvokeAsync<T>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T>()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvokeAsync<T>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<T>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}

	[Fact]
	public async Task GenerateInterfaceValueTaskReturnGeneric()
	{
		const string method = "ValueTask<T> InvokeAsync<T>();";

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

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T> InvokeAsync<T>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
				invokeAsync0Invocation.Register(_mock._invocationIndex);
				return System.Threading.Tasks.ValueTask.FromResult<T>(((Setup<T>?)_mock._invokeAsync0?.ValueOrDefault(typeof(T)))?.Execute(out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action, T, System.Func<T>> SetupInvokeAsync<T>() =>
				((InterfaceMock)@this).SetupInvokeAsync<T>();

			public void VerifyInvokeAsync<T>(in Times times) =>
				((InterfaceMock)@this).VerifyInvokeAsync<T>(times);

			public void VerifyInvokeAsync<T>(System.Func<Times> times) =>
				((InterfaceMock)@this).VerifyInvokeAsync<T>(times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T>()
			{
				var nextIndex = ((InterfaceMock)@this.Mock).VerifyInvokeAsync<T>(@this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation> InvokeAsync<T>()
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation($"IInterface.InvokeAsync<{key.Name}>()"));
				return invokeAsync0Invocation.GetInvocations() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync(TestContext.Current.CancellationToken);
	}
}
