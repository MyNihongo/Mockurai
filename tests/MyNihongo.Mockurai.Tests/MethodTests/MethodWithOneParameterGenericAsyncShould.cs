namespace MyNihongo.Mockurai.Tests.MethodTests;

public sealed class MethodWithOneParameterGenericAsyncShould : MethodGenericTestsBase
{
	[Fact]
	public async Task GenerateInterfaceTask()
	{
		const string method = "Task InvokeAsync(int param);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithParameter<int>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithParameter<int> SetupInvokeAsync(in It<int> param)
			{
				_invokeAsync0 ??= new SetupWithParameter<int>();
				_invokeAsync0.SetupParameter(param.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in It<int> param, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in It<int> param, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				return _invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(int param)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				_mock._invokeAsync0?.Invoke(param);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<int>> SetupInvokeAsync(in It<int> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(param);

			public void VerifyInvokeAsync(in It<int> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times);

			public void VerifyInvokeAsync(in It<int> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in It<int> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceValueTask()
	{
		const string method = "ValueTask InvokeAsync(int param);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithParameter<int>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithParameter<int> SetupInvokeAsync(in It<int> param)
			{
				_invokeAsync0 ??= new SetupWithParameter<int>();
				_invokeAsync0.SetupParameter(param.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in It<int> param, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in It<int> param, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				return _invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(int param)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				_mock._invokeAsync0?.Invoke(param);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<int>> SetupInvokeAsync(in It<int> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(param);

			public void VerifyInvokeAsync(in It<int> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times);

			public void VerifyInvokeAsync(in It<int> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in It<int> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceReturnTask()
	{
		const string method = "Task<float> InvokeAsync(int param);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithParameter<int, float>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithParameter<int, float> SetupInvokeAsync(in It<int> param)
			{
				_invokeAsync0 ??= new SetupWithParameter<int, float>();
				_invokeAsync0.SetupParameter(param.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in It<int> param, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in It<int> param, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				return _invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<float> InvokeAsync(int param)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				return System.Threading.Tasks.Task.FromResult<float>(_mock._invokeAsync0?.Execute(param, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<int>, float, System.Func<int, float>> SetupInvokeAsync(in It<int> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(param);

			public void VerifyInvokeAsync(in It<int> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times);

			public void VerifyInvokeAsync(in It<int> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in It<int> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceReturnValueTask()
	{
		const string method = "ValueTask<float> InvokeAsync(int param);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithParameter<int, float>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithParameter<int, float> SetupInvokeAsync(in It<int> param)
			{
				_invokeAsync0 ??= new SetupWithParameter<int, float>();
				_invokeAsync0.SetupParameter(param.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in It<int> param, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in It<int> param, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				return _invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<float> InvokeAsync(int param)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				return System.Threading.Tasks.ValueTask.FromResult<float>(_mock._invokeAsync0?.Execute(param, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<int>, float, System.Func<int, float>> SetupInvokeAsync(in It<int> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(param);

			public void VerifyInvokeAsync(in It<int> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times);

			public void VerifyInvokeAsync(in It<int> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in It<int> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithOutTask()
	{
		const string method = "Task InvokeAsync(out int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithOutParameter<int>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithOutParameter<int> SetupInvokeAsync(in ItOut<int> result)
			{
				_invokeAsync0 ??= new SetupWithOutParameter<int>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItOut<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItOut<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(out int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0 is not null)
				{
					_mock._invokeAsync0.Invoke(out result!);
					return System.Threading.Tasks.Task.CompletedTask;
				}
				else
				{
					result = default!;
					return System.Threading.Tasks.Task.CompletedTask;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<int>> SetupInvokeAsync(in ItOut<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItOut<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItOut<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItOut<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithOutValueTask()
	{
		const string method = "ValueTask InvokeAsync(out int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithOutParameter<int>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithOutParameter<int> SetupInvokeAsync(in ItOut<int> result)
			{
				_invokeAsync0 ??= new SetupWithOutParameter<int>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItOut<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItOut<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(out int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0 is not null)
				{
					_mock._invokeAsync0.Invoke(out result!);
					return System.Threading.Tasks.ValueTask.CompletedTask;
				}
				else
				{
					result = default!;
					return System.Threading.Tasks.ValueTask.CompletedTask;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<int>> SetupInvokeAsync(in ItOut<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItOut<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItOut<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItOut<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithOutReturnTask()
	{
		const string method = "Task<decimal> InvokeAsync(out int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithOutParameter<int, decimal>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithOutParameter<int, decimal> SetupInvokeAsync(in ItOut<int> result)
			{
				_invokeAsync0 ??= new SetupWithOutParameter<int, decimal>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItOut<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItOut<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<decimal> InvokeAsync(out int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0 is not null)
				{
					return System.Threading.Tasks.Task.FromResult<decimal>(_mock._invokeAsync0.Execute(out result!, out var returnValue) == true ? returnValue! : default!);
				}
				else
				{
					result = default!;
					return System.Threading.Tasks.Task.FromResult<decimal>(default!);
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<int>, decimal, System.FuncOut<int, decimal>> SetupInvokeAsync(in ItOut<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItOut<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItOut<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItOut<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithOutReturnValueTask()
	{
		const string method = "ValueTask<decimal> InvokeAsync(out int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithOutParameter<int, decimal>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithOutParameter<int, decimal> SetupInvokeAsync(in ItOut<int> result)
			{
				_invokeAsync0 ??= new SetupWithOutParameter<int, decimal>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItOut<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItOut<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<decimal> InvokeAsync(out int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0 is not null)
				{
					return System.Threading.Tasks.ValueTask.FromResult<decimal>(_mock._invokeAsync0.Execute(out result!, out var returnValue) == true ? returnValue! : default!);
				}
				else
				{
					result = default!;
					return System.Threading.Tasks.ValueTask.FromResult<decimal>(default!);
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<int>, decimal, System.FuncOut<int, decimal>> SetupInvokeAsync(in ItOut<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItOut<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItOut<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItOut<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithInTask()
	{
		const string method = "Task InvokeAsync(in int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithInParameter<int>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithInParameter<int> SetupInvokeAsync(in ItIn<int> result)
			{
				_invokeAsync0 ??= new SetupWithInParameter<int>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItIn<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItIn<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(in int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				_mock._invokeAsync0?.Invoke(in result);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<int>> SetupInvokeAsync(in ItIn<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItIn<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItIn<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItIn<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithInValueTask()
	{
		const string method = "ValueTask InvokeAsync(in int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithInParameter<int>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithInParameter<int> SetupInvokeAsync(in ItIn<int> result)
			{
				_invokeAsync0 ??= new SetupWithInParameter<int>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItIn<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItIn<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(in int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				_mock._invokeAsync0?.Invoke(in result);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<int>> SetupInvokeAsync(in ItIn<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItIn<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItIn<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItIn<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithInReturnTask()
	{
		const string method = "Task<float> InvokeAsync(in int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithInParameter<int, float>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithInParameter<int, float> SetupInvokeAsync(in ItIn<int> result)
			{
				_invokeAsync0 ??= new SetupWithInParameter<int, float>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItIn<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItIn<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<float> InvokeAsync(in int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.Task.FromResult<float>(_mock._invokeAsync0?.Execute(in result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<int>, float, System.FuncIn<int, float>> SetupInvokeAsync(in ItIn<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItIn<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItIn<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItIn<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithInReturnValueTask()
	{
		const string method = "ValueTask<float> InvokeAsync(in int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithInParameter<int, float>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithInParameter<int, float> SetupInvokeAsync(in ItIn<int> result)
			{
				_invokeAsync0 ??= new SetupWithInParameter<int, float>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItIn<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItIn<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<float> InvokeAsync(in int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.ValueTask.FromResult<float>(_mock._invokeAsync0?.Execute(in result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<int>, float, System.FuncIn<int, float>> SetupInvokeAsync(in ItIn<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItIn<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItIn<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItIn<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefTask()
	{
		const string method = "Task InvokeAsync(ref int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefParameter<int>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithRefParameter<int> SetupInvokeAsync(in ItRef<int> result)
			{
				_invokeAsync0 ??= new SetupWithRefParameter<int>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRef<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRef<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(ref int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				_mock._invokeAsync0?.Invoke(ref result);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<int>> SetupInvokeAsync(in ItRef<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItRef<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItRef<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRef<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefValueTask()
	{
		const string method = "ValueTask InvokeAsync(ref int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefParameter<int>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithRefParameter<int> SetupInvokeAsync(in ItRef<int> result)
			{
				_invokeAsync0 ??= new SetupWithRefParameter<int>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRef<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRef<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(ref int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				_mock._invokeAsync0?.Invoke(ref result);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<int>> SetupInvokeAsync(in ItRef<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItRef<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItRef<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRef<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReturnTask()
	{
		const string method = "Task<float> InvokeAsync(ref int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefParameter<int, float>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithRefParameter<int, float> SetupInvokeAsync(in ItRef<int> result)
			{
				_invokeAsync0 ??= new SetupWithRefParameter<int, float>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRef<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRef<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<float> InvokeAsync(ref int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.Task.FromResult<float>(_mock._invokeAsync0?.Execute(ref result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<int>, float, System.FuncRef<int, float>> SetupInvokeAsync(in ItRef<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItRef<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItRef<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRef<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReturnValueTask()
	{
		const string method = "ValueTask<float> InvokeAsync(ref int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefParameter<int, float>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithRefParameter<int, float> SetupInvokeAsync(in ItRef<int> result)
			{
				_invokeAsync0 ??= new SetupWithRefParameter<int, float>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRef<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRef<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<float> InvokeAsync(ref int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.ValueTask.FromResult<float>(_mock._invokeAsync0?.Execute(ref result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<int>, float, System.FuncRef<int, float>> SetupInvokeAsync(in ItRef<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItRef<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItRef<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRef<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReadonlyTask()
	{
		const string method = "Task InvokeAsync(ref readonly int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefReadOnlyParameter<int>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<int> SetupInvokeAsync(in ItRefReadOnly<int> result)
			{
				_invokeAsync0 ??= new SetupWithRefReadOnlyParameter<int>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRefReadOnly<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(ref readonly int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				_mock._invokeAsync0?.Invoke(in result);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<int>> SetupInvokeAsync(in ItRefReadOnly<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRefReadOnly<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReadonlyValueTask()
	{
		const string method = "ValueTask InvokeAsync(ref readonly int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefReadOnlyParameter<int>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<int> SetupInvokeAsync(in ItRefReadOnly<int> result)
			{
				_invokeAsync0 ??= new SetupWithRefReadOnlyParameter<int>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRefReadOnly<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(ref readonly int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				_mock._invokeAsync0?.Invoke(in result);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<int>> SetupInvokeAsync(in ItRefReadOnly<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRefReadOnly<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReadonlyReturnTask()
	{
		const string method = "Task<float> InvokeAsync(ref readonly int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefReadOnlyParameter<int, float>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<int, float> SetupInvokeAsync(in ItRefReadOnly<int> result)
			{
				_invokeAsync0 ??= new SetupWithRefReadOnlyParameter<int, float>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRefReadOnly<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<float> InvokeAsync(ref readonly int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.Task.FromResult<float>(_mock._invokeAsync0?.Execute(in result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<int>, float, System.FuncRefReadOnly<int, float>> SetupInvokeAsync(in ItRefReadOnly<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRefReadOnly<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReadonlyReturnValueTask()
	{
		const string method = "ValueTask<float> InvokeAsync(ref readonly int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefReadOnlyParameter<int, float>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<int, float> SetupInvokeAsync(in ItRefReadOnly<int> result)
			{
				_invokeAsync0 ??= new SetupWithRefReadOnlyParameter<int, float>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRefReadOnly<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<float> InvokeAsync(ref readonly int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.ValueTask.FromResult<float>(_mock._invokeAsync0?.Execute(in result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<int>, float, System.FuncRefReadOnly<int, float>> SetupInvokeAsync(in ItRefReadOnly<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(result);

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times);

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(result, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRefReadOnly<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	// ==================== Generic (TInvoke with ConcurrentDictionary) ====================
	// Tests 11-20 from source, each with Task and ValueTask variants
	// Due to size, these are continued below...

	[Fact]
	public async Task GenerateInterfaceGenericTask()
	{
		const string method = "Task InvokeAsync<TInvoke>(TInvoke param);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithParameter<TInvoke> SetupInvokeAsync<TInvoke>(in It<TInvoke> param)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithParameter<TInvoke>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithParameter<TInvoke>());
				invokeAsync0.SetupParameter(param.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})"));
				invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})"));
				return invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<TInvoke>(TInvoke param)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				((SetupWithParameter<TInvoke>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke(param);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<TInvoke>> SetupInvokeAsync<TInvoke>(in It<TInvoke> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(param);

			public void VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(param, times);

			public void VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in It<TInvoke> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericValueTask()
	{
		const string method = "ValueTask InvokeAsync<TInvoke>(TInvoke param);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithParameter<TInvoke> SetupInvokeAsync<TInvoke>(in It<TInvoke> param)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithParameter<TInvoke>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithParameter<TInvoke>());
				invokeAsync0.SetupParameter(param.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})"));
				invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})"));
				return invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<TInvoke>(TInvoke param)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				((SetupWithParameter<TInvoke>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke(param);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<TInvoke>> SetupInvokeAsync<TInvoke>(in It<TInvoke> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(param);

			public void VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(param, times);

			public void VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in It<TInvoke> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericReturnTask()
	{
		const string method = "Task<float> InvokeAsync<TInvoke>(TInvoke param);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithParameter<TInvoke, float> SetupInvokeAsync<TInvoke>(in It<TInvoke> param)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithParameter<TInvoke, float>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithParameter<TInvoke, float>());
				invokeAsync0.SetupParameter(param.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})"));
				invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})"));
				return invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<float> InvokeAsync<TInvoke>(TInvoke param)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				return System.Threading.Tasks.Task.FromResult<float>(((SetupWithParameter<TInvoke, float>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Execute(param, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<TInvoke>, float, System.Func<TInvoke, float>> SetupInvokeAsync<TInvoke>(in It<TInvoke> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(param);

			public void VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(param, times);

			public void VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in It<TInvoke> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericReturnValueTask()
	{
		const string method = "ValueTask<float> InvokeAsync<TInvoke>(TInvoke param);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithParameter<TInvoke, float> SetupInvokeAsync<TInvoke>(in It<TInvoke> param)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithParameter<TInvoke, float>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithParameter<TInvoke, float>());
				invokeAsync0.SetupParameter(param.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})"));
				invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})"));
				return invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<float> InvokeAsync<TInvoke>(TInvoke param)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				return System.Threading.Tasks.ValueTask.FromResult<float>(((SetupWithParameter<TInvoke, float>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Execute(param, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<TInvoke>, float, System.Func<TInvoke, float>> SetupInvokeAsync<TInvoke>(in It<TInvoke> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(param);

			public void VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(param, times);

			public void VerifyInvokeAsync<TInvoke>(in It<TInvoke> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in It<TInvoke> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithOutTask()
	{
		const string method = "Task InvokeAsync<TInvoke>(out TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithOutParameter<TInvoke> SetupInvokeAsync<TInvoke>(in ItOut<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithOutParameter<TInvoke>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithOutParameter<TInvoke>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "out"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<TInvoke>(out TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0?.TryGetValue(typeof(TInvoke), out var setup) == true)
				{
					((SetupWithOutParameter<TInvoke>)setup).Invoke(out value!);
					return System.Threading.Tasks.Task.CompletedTask;
				}
				else
				{
					value = default!;
					return System.Threading.Tasks.Task.CompletedTask;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<TInvoke>> SetupInvokeAsync<TInvoke>(in ItOut<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItOut<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithOutValueTask()
	{
		const string method = "ValueTask InvokeAsync<TInvoke>(out TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithOutParameter<TInvoke> SetupInvokeAsync<TInvoke>(in ItOut<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithOutParameter<TInvoke>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithOutParameter<TInvoke>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "out"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<TInvoke>(out TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0?.TryGetValue(typeof(TInvoke), out var setup) == true)
				{
					((SetupWithOutParameter<TInvoke>)setup).Invoke(out value!);
					return System.Threading.Tasks.ValueTask.CompletedTask;
				}
				else
				{
					value = default!;
					return System.Threading.Tasks.ValueTask.CompletedTask;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<TInvoke>> SetupInvokeAsync<TInvoke>(in ItOut<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItOut<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithOutReturnTask()
	{
		const string method = "Task<decimal> InvokeAsync<TInvoke>(out TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithOutParameter<TInvoke, decimal> SetupInvokeAsync<TInvoke>(in ItOut<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithOutParameter<TInvoke, decimal>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithOutParameter<TInvoke, decimal>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "out"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<decimal> InvokeAsync<TInvoke>(out TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0?.TryGetValue(typeof(TInvoke), out var setup) == true)
				{
					return System.Threading.Tasks.Task.FromResult<decimal>(((SetupWithOutParameter<TInvoke, decimal>)setup).Execute(out value!, out var returnValue) == true ? returnValue! : default!);
				}
				else
				{
					value = default!;
					return System.Threading.Tasks.Task.FromResult<decimal>(default!);
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<TInvoke>, decimal, System.FuncOut<TInvoke, decimal>> SetupInvokeAsync<TInvoke>(in ItOut<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItOut<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithOutReturnValueTask()
	{
		const string method = "ValueTask<decimal> InvokeAsync<TInvoke>(out TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithOutParameter<TInvoke, decimal> SetupInvokeAsync<TInvoke>(in ItOut<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithOutParameter<TInvoke, decimal>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithOutParameter<TInvoke, decimal>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "out"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<decimal> InvokeAsync<TInvoke>(out TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0?.TryGetValue(typeof(TInvoke), out var setup) == true)
				{
					return System.Threading.Tasks.ValueTask.FromResult<decimal>(((SetupWithOutParameter<TInvoke, decimal>)setup).Execute(out value!, out var returnValue) == true ? returnValue! : default!);
				}
				else
				{
					value = default!;
					return System.Threading.Tasks.ValueTask.FromResult<decimal>(default!);
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<TInvoke>, decimal, System.FuncOut<TInvoke, decimal>> SetupInvokeAsync<TInvoke>(in ItOut<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItOut<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItOut<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithInTask()
	{
		const string method = "Task InvokeAsync<TInvoke>(in TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithInParameter<TInvoke> SetupInvokeAsync<TInvoke>(in ItIn<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithInParameter<TInvoke>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithInParameter<TInvoke>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "in"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<TInvoke>(in TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithInParameter<TInvoke>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke(in value);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<TInvoke>> SetupInvokeAsync<TInvoke>(in ItIn<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItIn<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithInValueTask()
	{
		const string method = "ValueTask InvokeAsync<TInvoke>(in TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithInParameter<TInvoke> SetupInvokeAsync<TInvoke>(in ItIn<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithInParameter<TInvoke>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithInParameter<TInvoke>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "in"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<TInvoke>(in TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithInParameter<TInvoke>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke(in value);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<TInvoke>> SetupInvokeAsync<TInvoke>(in ItIn<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItIn<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithInReturnTask()
	{
		const string method = "Task<float> InvokeAsync<TInvoke>(in TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithInParameter<TInvoke, float> SetupInvokeAsync<TInvoke>(in ItIn<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithInParameter<TInvoke, float>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithInParameter<TInvoke, float>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "in"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<float> InvokeAsync<TInvoke>(in TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.Task.FromResult<float>(((SetupWithInParameter<TInvoke, float>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Execute(in value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<TInvoke>, float, System.FuncIn<TInvoke, float>> SetupInvokeAsync<TInvoke>(in ItIn<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItIn<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithInReturnValueTask()
	{
		const string method = "ValueTask<float> InvokeAsync<TInvoke>(in TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithInParameter<TInvoke, float> SetupInvokeAsync<TInvoke>(in ItIn<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithInParameter<TInvoke, float>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithInParameter<TInvoke, float>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "in"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<float> InvokeAsync<TInvoke>(in TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.ValueTask.FromResult<float>(((SetupWithInParameter<TInvoke, float>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Execute(in value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<TInvoke>, float, System.FuncIn<TInvoke, float>> SetupInvokeAsync<TInvoke>(in ItIn<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItIn<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItIn<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefTask()
	{
		const string method = "Task InvokeAsync<TInvoke>(ref TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithRefParameter<TInvoke> SetupInvokeAsync<TInvoke>(in ItRef<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithRefParameter<TInvoke>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithRefParameter<TInvoke>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<TInvoke>(ref TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefParameter<TInvoke>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke(ref value);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<TInvoke>> SetupInvokeAsync<TInvoke>(in ItRef<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItRef<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefValueTask()
	{
		const string method = "ValueTask InvokeAsync<TInvoke>(ref TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithRefParameter<TInvoke> SetupInvokeAsync<TInvoke>(in ItRef<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithRefParameter<TInvoke>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithRefParameter<TInvoke>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<TInvoke>(ref TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefParameter<TInvoke>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke(ref value);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<TInvoke>> SetupInvokeAsync<TInvoke>(in ItRef<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItRef<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefReturnTask()
	{
		const string method = "Task<float> InvokeAsync<TInvoke>(ref TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithRefParameter<TInvoke, float> SetupInvokeAsync<TInvoke>(in ItRef<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithRefParameter<TInvoke, float>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithRefParameter<TInvoke, float>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<float> InvokeAsync<TInvoke>(ref TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.Task.FromResult<float>(((SetupWithRefParameter<TInvoke, float>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Execute(ref value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<TInvoke>, float, System.FuncRef<TInvoke, float>> SetupInvokeAsync<TInvoke>(in ItRef<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItRef<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefReturnValueTask()
	{
		const string method = "ValueTask<float> InvokeAsync<TInvoke>(ref TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithRefParameter<TInvoke, float> SetupInvokeAsync<TInvoke>(in ItRef<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithRefParameter<TInvoke, float>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithRefParameter<TInvoke, float>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<float> InvokeAsync<TInvoke>(ref TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.ValueTask.FromResult<float>(((SetupWithRefParameter<TInvoke, float>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Execute(ref value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<TInvoke>, float, System.FuncRef<TInvoke, float>> SetupInvokeAsync<TInvoke>(in ItRef<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItRef<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItRef<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefReadOnlyTask()
	{
		const string method = "Task InvokeAsync<TInvoke>(ref readonly TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<TInvoke> SetupInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithRefReadOnlyParameter<TInvoke>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithRefReadOnlyParameter<TInvoke>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref readonly"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<TInvoke>(ref readonly TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefReadOnlyParameter<TInvoke>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke(in value);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<TInvoke>> SetupInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefReadOnlyValueTask()
	{
		const string method = "ValueTask InvokeAsync<TInvoke>(ref readonly TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<TInvoke> SetupInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithRefReadOnlyParameter<TInvoke>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithRefReadOnlyParameter<TInvoke>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref readonly"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<TInvoke>(ref readonly TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefReadOnlyParameter<TInvoke>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Invoke(in value);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<TInvoke>> SetupInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefReadOnlyReturnTask()
	{
		const string method = "Task<float> InvokeAsync<TInvoke>(ref readonly TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<TInvoke, float> SetupInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithRefReadOnlyParameter<TInvoke, float>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithRefReadOnlyParameter<TInvoke, float>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref readonly"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<float> InvokeAsync<TInvoke>(ref readonly TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.Task.FromResult<float>(((SetupWithRefReadOnlyParameter<TInvoke, float>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Execute(in value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<TInvoke>, float, System.FuncRefReadOnly<TInvoke, float>> SetupInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefReadOnlyReturnValueTask()
	{
		const string method = "ValueTask<float> InvokeAsync<TInvoke>(ref readonly TInvoke value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<TInvoke, float> SetupInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithRefReadOnlyParameter<TInvoke, float>)_invokeAsync0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithRefReadOnlyParameter<TInvoke, float>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref readonly"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<float> InvokeAsync<TInvoke>(ref readonly TInvoke value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<TInvoke>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.ValueTask.FromResult<float>(((SetupWithRefReadOnlyParameter<TInvoke, float>?)_mock._invokeAsync0?.ValueOrDefault(typeof(TInvoke)))?.Execute(in value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<TInvoke>, float, System.FuncRefReadOnly<TInvoke, float>> SetupInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<TInvoke>(value);

			public void VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times);

			public void VerifyInvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<TInvoke>(in ItRefReadOnly<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1Task()
	{
		const string method = "Task InvokeAsync(T param);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithParameter<T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithParameter<T> SetupInvokeAsync(in It<T> param)
			{
				_invokeAsync0 ??= new SetupWithParameter<T>();
				_invokeAsync0.SetupParameter(param.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in It<T> param, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in It<T> param, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				return _invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(T param)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				_mock._invokeAsync0?.Invoke(param);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<T>> SetupInvokeAsync(in It<T> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(param);

			public void VerifyInvokeAsync(in It<T> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times);

			public void VerifyInvokeAsync(in It<T> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in It<T> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1ValueTask()
	{
		const string method = "ValueTask InvokeAsync(T param);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithParameter<T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithParameter<T> SetupInvokeAsync(in It<T> param)
			{
				_invokeAsync0 ??= new SetupWithParameter<T>();
				_invokeAsync0.SetupParameter(param.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in It<T> param, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in It<T> param, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				return _invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(T param)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				_mock._invokeAsync0?.Invoke(param);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<T>> SetupInvokeAsync(in It<T> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(param);

			public void VerifyInvokeAsync(in It<T> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times);

			public void VerifyInvokeAsync(in It<T> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in It<T> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1ReturnTask()
	{
		const string method = "Task<T> InvokeAsync(T param);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithParameter<T, T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithParameter<T, T> SetupInvokeAsync(in It<T> param)
			{
				_invokeAsync0 ??= new SetupWithParameter<T, T>();
				_invokeAsync0.SetupParameter(param.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in It<T> param, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in It<T> param, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				return _invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T> InvokeAsync(T param)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				return System.Threading.Tasks.Task.FromResult<T>(_mock._invokeAsync0?.Execute(param, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<T>, T, System.Func<T, T>> SetupInvokeAsync(in It<T> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(param);

			public void VerifyInvokeAsync(in It<T> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times);

			public void VerifyInvokeAsync(in It<T> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in It<T> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1ReturnValueTask()
	{
		const string method = "ValueTask<T> InvokeAsync(T param);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithParameter<T, T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithParameter<T, T> SetupInvokeAsync(in It<T> param)
			{
				_invokeAsync0 ??= new SetupWithParameter<T, T>();
				_invokeAsync0.SetupParameter(param.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in It<T> param, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in It<T> param, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				return _invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T> InvokeAsync(T param)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				return System.Threading.Tasks.ValueTask.FromResult<T>(_mock._invokeAsync0?.Execute(param, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<T>, T, System.Func<T, T>> SetupInvokeAsync(in It<T> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(param);

			public void VerifyInvokeAsync(in It<T> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times);

			public void VerifyInvokeAsync(in It<T> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in It<T> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithOutTask()
	{
		const string method = "Task InvokeAsync(out T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithOutParameter<T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithOutParameter<T> SetupInvokeAsync(in ItOut<T> value)
			{
				_invokeAsync0 ??= new SetupWithOutParameter<T>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItOut<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItOut<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(out T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0 is not null)
				{
					_mock._invokeAsync0.Invoke(out value!);
					return System.Threading.Tasks.Task.CompletedTask;
				}
				else
				{
					value = default!;
					return System.Threading.Tasks.Task.CompletedTask;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<T>> SetupInvokeAsync(in ItOut<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItOut<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItOut<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItOut<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithOutValueTask()
	{
		const string method = "ValueTask InvokeAsync(out T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithOutParameter<T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithOutParameter<T> SetupInvokeAsync(in ItOut<T> value)
			{
				_invokeAsync0 ??= new SetupWithOutParameter<T>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItOut<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItOut<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(out T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0 is not null)
				{
					_mock._invokeAsync0.Invoke(out value!);
					return System.Threading.Tasks.ValueTask.CompletedTask;
				}
				else
				{
					value = default!;
					return System.Threading.Tasks.ValueTask.CompletedTask;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<T>> SetupInvokeAsync(in ItOut<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItOut<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItOut<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItOut<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithOutReturnTask()
	{
		const string method = "Task<T> InvokeAsync(out T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithOutParameter<T, T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithOutParameter<T, T> SetupInvokeAsync(in ItOut<T> value)
			{
				_invokeAsync0 ??= new SetupWithOutParameter<T, T>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItOut<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItOut<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T> InvokeAsync(out T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0 is not null)
				{
					return System.Threading.Tasks.Task.FromResult<T>(_mock._invokeAsync0.Execute(out value!, out var returnValue) == true ? returnValue! : default!);
				}
				else
				{
					value = default!;
					return System.Threading.Tasks.Task.FromResult<T>(default!);
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<T>, T, System.FuncOut<T, T>> SetupInvokeAsync(in ItOut<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItOut<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItOut<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItOut<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithOutReturnValueTask()
	{
		const string method = "ValueTask<T> InvokeAsync(out T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithOutParameter<T, T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithOutParameter<T, T> SetupInvokeAsync(in ItOut<T> value)
			{
				_invokeAsync0 ??= new SetupWithOutParameter<T, T>();
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItOut<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItOut<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T> InvokeAsync(out T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "out");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0 is not null)
				{
					return System.Threading.Tasks.ValueTask.FromResult<T>(_mock._invokeAsync0.Execute(out value!, out var returnValue) == true ? returnValue! : default!);
				}
				else
				{
					value = default!;
					return System.Threading.Tasks.ValueTask.FromResult<T>(default!);
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<T>, T, System.FuncOut<T, T>> SetupInvokeAsync(in ItOut<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItOut<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItOut<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItOut<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithInTask()
	{
		const string method = "Task InvokeAsync(in T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithInParameter<T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithInParameter<T> SetupInvokeAsync(in ItIn<T> value)
			{
				_invokeAsync0 ??= new SetupWithInParameter<T>();
				_invokeAsync0.SetupParameter(value.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItIn<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItIn<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(in T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				_mock._invokeAsync0?.Invoke(in value);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<T>> SetupInvokeAsync(in ItIn<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItIn<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItIn<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItIn<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithInValueTask()
	{
		const string method = "ValueTask InvokeAsync(in T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithInParameter<T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithInParameter<T> SetupInvokeAsync(in ItIn<T> value)
			{
				_invokeAsync0 ??= new SetupWithInParameter<T>();
				_invokeAsync0.SetupParameter(value.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItIn<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItIn<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(in T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				_mock._invokeAsync0?.Invoke(in value);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<T>> SetupInvokeAsync(in ItIn<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItIn<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItIn<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItIn<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithInReturnTask()
	{
		const string method = "Task<T> InvokeAsync(in T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithInParameter<T, T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithInParameter<T, T> SetupInvokeAsync(in ItIn<T> value)
			{
				_invokeAsync0 ??= new SetupWithInParameter<T, T>();
				_invokeAsync0.SetupParameter(value.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItIn<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItIn<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T> InvokeAsync(in T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.Task.FromResult<T>(_mock._invokeAsync0?.Execute(in value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<T>, T, System.FuncIn<T, T>> SetupInvokeAsync(in ItIn<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItIn<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItIn<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItIn<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithInReturnValueTask()
	{
		const string method = "ValueTask<T> InvokeAsync(in T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithInParameter<T, T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithInParameter<T, T> SetupInvokeAsync(in ItIn<T> value)
			{
				_invokeAsync0 ??= new SetupWithInParameter<T, T>();
				_invokeAsync0.SetupParameter(value.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItIn<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItIn<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T> InvokeAsync(in T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "in");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.ValueTask.FromResult<T>(_mock._invokeAsync0?.Execute(in value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<T>, T, System.FuncIn<T, T>> SetupInvokeAsync(in ItIn<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItIn<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItIn<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItIn<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithRefTask()
	{
		const string method = "Task InvokeAsync(ref T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefParameter<T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithRefParameter<T> SetupInvokeAsync(in ItRef<T> value)
			{
				_invokeAsync0 ??= new SetupWithRefParameter<T>();
				_invokeAsync0.SetupParameter(value.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRef<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRef<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(ref T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				_mock._invokeAsync0?.Invoke(ref value);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<T>> SetupInvokeAsync(in ItRef<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItRef<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItRef<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRef<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithRefValueTask()
	{
		const string method = "ValueTask InvokeAsync(ref T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefParameter<T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithRefParameter<T> SetupInvokeAsync(in ItRef<T> value)
			{
				_invokeAsync0 ??= new SetupWithRefParameter<T>();
				_invokeAsync0.SetupParameter(value.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRef<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRef<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(ref T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				_mock._invokeAsync0?.Invoke(ref value);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<T>> SetupInvokeAsync(in ItRef<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItRef<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItRef<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRef<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithRefReturnTask()
	{
		const string method = "Task<T> InvokeAsync(ref T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefParameter<T, T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithRefParameter<T, T> SetupInvokeAsync(in ItRef<T> value)
			{
				_invokeAsync0 ??= new SetupWithRefParameter<T, T>();
				_invokeAsync0.SetupParameter(value.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRef<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRef<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T> InvokeAsync(ref T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.Task.FromResult<T>(_mock._invokeAsync0?.Execute(ref value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<T>, T, System.FuncRef<T, T>> SetupInvokeAsync(in ItRef<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItRef<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItRef<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRef<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithRefReturnValueTask()
	{
		const string method = "ValueTask<T> InvokeAsync(ref T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefParameter<T, T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithRefParameter<T, T> SetupInvokeAsync(in ItRef<T> value)
			{
				_invokeAsync0 ??= new SetupWithRefParameter<T, T>();
				_invokeAsync0.SetupParameter(value.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRef<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRef<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T> InvokeAsync(ref T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.ValueTask.FromResult<T>(_mock._invokeAsync0?.Execute(ref value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<T>, T, System.FuncRef<T, T>> SetupInvokeAsync(in ItRef<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItRef<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItRef<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRef<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithRefReadOnlyTask()
	{
		const string method = "Task InvokeAsync(ref readonly T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefReadOnlyParameter<T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<T> SetupInvokeAsync(in ItRefReadOnly<T> value)
			{
				_invokeAsync0 ??= new SetupWithRefReadOnlyParameter<T>();
				_invokeAsync0.SetupParameter(value.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRefReadOnly<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRefReadOnly<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(ref readonly T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				_mock._invokeAsync0?.Invoke(in value);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<T>> SetupInvokeAsync(in ItRefReadOnly<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItRefReadOnly<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItRefReadOnly<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRefReadOnly<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithRefReadOnlyValueTask()
	{
		const string method = "ValueTask InvokeAsync(ref readonly T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefReadOnlyParameter<T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<T> SetupInvokeAsync(in ItRefReadOnly<T> value)
			{
				_invokeAsync0 ??= new SetupWithRefReadOnlyParameter<T>();
				_invokeAsync0.SetupParameter(value.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRefReadOnly<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRefReadOnly<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(ref readonly T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				_mock._invokeAsync0?.Invoke(in value);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<T>> SetupInvokeAsync(in ItRefReadOnly<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItRefReadOnly<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItRefReadOnly<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRefReadOnly<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithRefReadOnlyReturnTask()
	{
		const string method = "Task<T> InvokeAsync(ref readonly T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefReadOnlyParameter<T, T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<T, T> SetupInvokeAsync(in ItRefReadOnly<T> value)
			{
				_invokeAsync0 ??= new SetupWithRefReadOnlyParameter<T, T>();
				_invokeAsync0.SetupParameter(value.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRefReadOnly<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRefReadOnly<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T> InvokeAsync(ref readonly T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.Task.FromResult<T>(_mock._invokeAsync0?.Execute(in value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<T>, T, System.FuncRefReadOnly<T, T>> SetupInvokeAsync(in ItRefReadOnly<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItRefReadOnly<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItRefReadOnly<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRefReadOnly<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithRefReadOnlyReturnValueTask()
	{
		const string method = "ValueTask<T> InvokeAsync(ref readonly T value);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefReadOnlyParameter<T, T>? _invokeAsync0;
			private Invocation<T>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<T, T> SetupInvokeAsync(in ItRefReadOnly<T> value)
			{
				_invokeAsync0 ??= new SetupWithRefReadOnlyParameter<T, T>();
				_invokeAsync0.SetupParameter(value.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRefReadOnly<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRefReadOnly<T> value, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				return _invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T> InvokeAsync(ref readonly T value)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.InvokeAsync({{0}})", prefix: "ref readonly");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.ValueTask.FromResult<T>(_mock._invokeAsync0?.Execute(in value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<T>, T, System.FuncRefReadOnly<T, T>> SetupInvokeAsync(in ItRefReadOnly<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync(value);

			public void VerifyInvokeAsync(in ItRefReadOnly<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times);

			public void VerifyInvokeAsync(in ItRefReadOnly<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync(in ItRefReadOnly<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2Task()
	{
		const string method = "Task InvokeAsync<T1, T2>(T1 param);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithParameter<T1> SetupInvokeAsync<T1, T2>(in It<T1> param)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithParameter<T1>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithParameter<T1>());
				invokeAsync0.SetupParameter(param.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in It<T1> param, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in It<T1> param, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				return invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<T1, T2>(T1 param)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				((SetupWithParameter<T1>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke(param);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<T1>> SetupInvokeAsync<T1, T2>(in It<T1> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(param);

			public void VerifyInvokeAsync<T1, T2>(in It<T1> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(param, times);

			public void VerifyInvokeAsync<T1, T2>(in It<T1> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in It<T1> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2ValueTask()
	{
		const string method = "ValueTask InvokeAsync<T1, T2>(T1 param);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithParameter<T1> SetupInvokeAsync<T1, T2>(in It<T1> param)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithParameter<T1>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithParameter<T1>());
				invokeAsync0.SetupParameter(param.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in It<T1> param, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in It<T1> param, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				return invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<T1, T2>(T1 param)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				((SetupWithParameter<T1>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke(param);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<T1>> SetupInvokeAsync<T1, T2>(in It<T1> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(param);

			public void VerifyInvokeAsync<T1, T2>(in It<T1> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(param, times);

			public void VerifyInvokeAsync<T1, T2>(in It<T1> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in It<T1> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2ReturnTask()
	{
		const string method = "Task<T2> InvokeAsync<T1, T2>(T1 param);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithParameter<T1, T2> SetupInvokeAsync<T1, T2>(in It<T1> param)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithParameter<T1, T2>());
				invokeAsync0.SetupParameter(param.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in It<T1> param, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in It<T1> param, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				return invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T2> InvokeAsync<T1, T2>(T1 param)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				return System.Threading.Tasks.Task.FromResult<T2>(((SetupWithParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(param, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<T1>, T2, System.Func<T1, T2>> SetupInvokeAsync<T1, T2>(in It<T1> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(param);

			public void VerifyInvokeAsync<T1, T2>(in It<T1> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(param, times);

			public void VerifyInvokeAsync<T1, T2>(in It<T1> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in It<T1> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2ReturnValueTask()
	{
		const string method = "ValueTask<T2> InvokeAsync<T1, T2>(T1 param);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithParameter<T1, T2> SetupInvokeAsync<T1, T2>(in It<T1> param)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithParameter<T1, T2>());
				invokeAsync0.SetupParameter(param.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in It<T1> param, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in It<T1> param, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				return invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T2> InvokeAsync<T1, T2>(T1 param)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				return System.Threading.Tasks.ValueTask.FromResult<T2>(((SetupWithParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(param, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.Action<T1>, T2, System.Func<T1, T2>> SetupInvokeAsync<T1, T2>(in It<T1> param = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(param);

			public void VerifyInvokeAsync<T1, T2>(in It<T1> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(param, times);

			public void VerifyInvokeAsync<T1, T2>(in It<T1> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(param, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in It<T1> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithOutTask()
	{
		const string method = "Task InvokeAsync<T1, T2>(out T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithOutParameter<T1> SetupInvokeAsync<T1, T2>(in ItOut<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithOutParameter<T1>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithOutParameter<T1>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<T1, T2>(out T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0?.TryGetValue((typeof(T1), typeof(T2)), out var setup) == true)
				{
					((SetupWithOutParameter<T1>)setup).Invoke(out value!);
					return System.Threading.Tasks.Task.CompletedTask;
				}
				else
				{
					value = default!;
					return System.Threading.Tasks.Task.CompletedTask;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<T1>> SetupInvokeAsync<T1, T2>(in ItOut<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItOut<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithOutValueTask()
	{
		const string method = "ValueTask InvokeAsync<T1, T2>(out T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithOutParameter<T1> SetupInvokeAsync<T1, T2>(in ItOut<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithOutParameter<T1>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithOutParameter<T1>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<T1, T2>(out T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0?.TryGetValue((typeof(T1), typeof(T2)), out var setup) == true)
				{
					((SetupWithOutParameter<T1>)setup).Invoke(out value!);
					return System.Threading.Tasks.ValueTask.CompletedTask;
				}
				else
				{
					value = default!;
					return System.Threading.Tasks.ValueTask.CompletedTask;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<T1>> SetupInvokeAsync<T1, T2>(in ItOut<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItOut<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithOutReturnTask()
	{
		const string method = "Task<T2> InvokeAsync<T1, T2>(out T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithOutParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItOut<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithOutParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithOutParameter<T1, T2>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T2> InvokeAsync<T1, T2>(out T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0?.TryGetValue((typeof(T1), typeof(T2)), out var setup) == true)
				{
					return System.Threading.Tasks.Task.FromResult<T2>(((SetupWithOutParameter<T1, T2>)setup).Execute(out value!, out var returnValue) == true ? returnValue! : default!);
				}
				else
				{
					value = default!;
					return System.Threading.Tasks.Task.FromResult<T2>(default!);
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<T1>, T2, System.FuncOut<T1, T2>> SetupInvokeAsync<T1, T2>(in ItOut<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItOut<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithOutReturnValueTask()
	{
		const string method = "ValueTask<T2> InvokeAsync<T1, T2>(out T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithOutParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItOut<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithOutParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithOutParameter<T1, T2>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T2> InvokeAsync<T1, T2>(out T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invokeAsync0?.TryGetValue((typeof(T1), typeof(T2)), out var setup) == true)
				{
					return System.Threading.Tasks.ValueTask.FromResult<T2>(((SetupWithOutParameter<T1, T2>)setup).Execute(out value!, out var returnValue) == true ? returnValue! : default!);
				}
				else
				{
					value = default!;
					return System.Threading.Tasks.ValueTask.FromResult<T2>(default!);
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionOut<T1>, T2, System.FuncOut<T1, T2>> SetupInvokeAsync<T1, T2>(in ItOut<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItOut<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithInTask()
	{
		const string method = "Task InvokeAsync<T1, T2>(in T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithInParameter<T1> SetupInvokeAsync<T1, T2>(in ItIn<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithInParameter<T1>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithInParameter<T1>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<T1, T2>(in T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithInParameter<T1>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke(in value);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<T1>> SetupInvokeAsync<T1, T2>(in ItIn<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItIn<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithInValueTask()
	{
		const string method = "ValueTask InvokeAsync<T1, T2>(in T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithInParameter<T1> SetupInvokeAsync<T1, T2>(in ItIn<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithInParameter<T1>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithInParameter<T1>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<T1, T2>(in T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithInParameter<T1>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke(in value);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<T1>> SetupInvokeAsync<T1, T2>(in ItIn<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItIn<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithInReturnTask()
	{
		const string method = "Task<T2> InvokeAsync<T1, T2>(in T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithInParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItIn<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithInParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithInParameter<T1, T2>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T2> InvokeAsync<T1, T2>(in T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.Task.FromResult<T2>(((SetupWithInParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(in value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<T1>, T2, System.FuncIn<T1, T2>> SetupInvokeAsync<T1, T2>(in ItIn<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItIn<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithInReturnValueTask()
	{
		const string method = "ValueTask<T2> InvokeAsync<T1, T2>(in T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithInParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItIn<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithInParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithInParameter<T1, T2>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T2> InvokeAsync<T1, T2>(in T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.ValueTask.FromResult<T2>(((SetupWithInParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(in value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionIn<T1>, T2, System.FuncIn<T1, T2>> SetupInvokeAsync<T1, T2>(in ItIn<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItIn<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithRefTask()
	{
		const string method = "Task InvokeAsync<T1, T2>(ref T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithRefParameter<T1> SetupInvokeAsync<T1, T2>(in ItRef<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithRefParameter<T1>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefParameter<T1>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<T1, T2>(ref T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefParameter<T1>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke(ref value);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<T1>> SetupInvokeAsync<T1, T2>(in ItRef<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItRef<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithRefValueTask()
	{
		const string method = "ValueTask InvokeAsync<T1, T2>(ref T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithRefParameter<T1> SetupInvokeAsync<T1, T2>(in ItRef<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithRefParameter<T1>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefParameter<T1>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<T1, T2>(ref T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefParameter<T1>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke(ref value);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<T1>> SetupInvokeAsync<T1, T2>(in ItRef<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItRef<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithRefReturnTask()
	{
		const string method = "Task<T2> InvokeAsync<T1, T2>(ref T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithRefParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItRef<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithRefParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefParameter<T1, T2>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T2> InvokeAsync<T1, T2>(ref T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.Task.FromResult<T2>(((SetupWithRefParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(ref value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<T1>, T2, System.FuncRef<T1, T2>> SetupInvokeAsync<T1, T2>(in ItRef<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItRef<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithRefReturnValueTask()
	{
		const string method = "ValueTask<T2> InvokeAsync<T1, T2>(ref T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithRefParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItRef<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithRefParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefParameter<T1, T2>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T2> InvokeAsync<T1, T2>(ref T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.ValueTask.FromResult<T2>(((SetupWithRefParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(ref value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRef<T1>, T2, System.FuncRef<T1, T2>> SetupInvokeAsync<T1, T2>(in ItRef<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItRef<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithRefReadOnlyTask()
	{
		const string method = "Task InvokeAsync<T1, T2>(ref readonly T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<T1> SetupInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithRefReadOnlyParameter<T1>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefReadOnlyParameter<T1>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<T1, T2>(ref readonly T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefReadOnlyParameter<T1>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke(in value);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<T1>> SetupInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItRefReadOnly<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithRefReadOnlyValueTask()
	{
		const string method = "ValueTask InvokeAsync<T1, T2>(ref readonly T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<T1> SetupInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithRefReadOnlyParameter<T1>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefReadOnlyParameter<T1>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<T1, T2>(ref readonly T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefReadOnlyParameter<T1>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke(in value);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<T1>> SetupInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItRefReadOnly<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithRefReadOnlyReturnTask()
	{
		const string method = "Task<T2> InvokeAsync<T1, T2>(ref readonly T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithRefReadOnlyParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefReadOnlyParameter<T1, T2>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T2> InvokeAsync<T1, T2>(ref readonly T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.Task.FromResult<T2>(((SetupWithRefReadOnlyParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(in value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<T1>, T2, System.FuncRefReadOnly<T1, T2>> SetupInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItRefReadOnly<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithRefReadOnlyReturnValueTask()
	{
		const string method = "ValueTask<T2> InvokeAsync<T1, T2>(ref readonly T1 value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithRefReadOnlyParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefReadOnlyParameter<T1, T2>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T2> InvokeAsync<T1, T2>(ref readonly T1 value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				return System.Threading.Tasks.ValueTask.FromResult<T2>(((SetupWithRefReadOnlyParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(in value, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		const string extensions =
			"""
			// InvokeAsync
			public ISetup<System.ActionRefReadOnly<T1>, T2, System.FuncRefReadOnly<T1, T2>> SetupInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvokeAsync<T1, T2>(value);

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times);

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvokeAsync<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// InvokeAsync
			public void InvokeAsync<T1, T2>(in ItRefReadOnly<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvokeAsync<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer: "", verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
