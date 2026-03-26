namespace MyNihongo.Mock.Tests.MethodTests;

public sealed class MethodWithOneParameterAsyncShould : MethodTestsBase
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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})");
				_invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in It<int> param, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})");
				return _invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(int param)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				_mock._invokeAsync0?.Invoke(param);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})");
				_invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in It<int> param, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})");
				return _invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(int param)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				_mock._invokeAsync0?.Invoke(param);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})");
				_invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in It<int> param, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})");
				return _invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<float> InvokeAsync(int param)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				return System.Threading.Tasks.Task.FromResult<float>(_mock._invokeAsync0?.Execute(param, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})");
				_invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in It<int> param, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})");
				return _invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<float> InvokeAsync(int param)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				return System.Threading.Tasks.ValueTask.FromResult<float>(_mock._invokeAsync0?.Execute(param, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "out");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItOut<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "out");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(out int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "out");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, default);
				if (_mock._invokeAsync0 is not null)
				{
					_mock._invokeAsync0.Invoke(out result);
					return System.Threading.Tasks.Task.CompletedTask;
				}
				else
				{
					result = default;
					return System.Threading.Tasks.Task.CompletedTask;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "out");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItOut<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "out");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(out int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "out");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, default);
				if (_mock._invokeAsync0 is not null)
				{
					_mock._invokeAsync0.Invoke(out result);
					return System.Threading.Tasks.ValueTask.CompletedTask;
				}
				else
				{
					result = default;
					return System.Threading.Tasks.ValueTask.CompletedTask;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "out");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItOut<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "out");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<decimal> InvokeAsync(out int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "out");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, default);
				if (_mock._invokeAsync0 is not null)
				{
					return System.Threading.Tasks.Task.FromResult<decimal>(_mock._invokeAsync0.Execute(out result, out var returnValue) == true ? returnValue! : default!);
				}
				else
				{
					result = default;
					return System.Threading.Tasks.Task.FromResult<decimal>(default!);
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "out");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItOut<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "out");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<decimal> InvokeAsync(out int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "out");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, default);
				if (_mock._invokeAsync0 is not null)
				{
					return System.Threading.Tasks.ValueTask.FromResult<decimal>(_mock._invokeAsync0.Execute(out result, out var returnValue) == true ? returnValue! : default!);
				}
				else
				{
					result = default;
					return System.Threading.Tasks.ValueTask.FromResult<decimal>(default!);
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "in");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItIn<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "in");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(in int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "in");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				_mock._invokeAsync0?.Invoke(in result);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "in");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItIn<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "in");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(in int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "in");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				_mock._invokeAsync0?.Invoke(in result);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithInReturnTask()
	{
		const string method = "Task<decimal> InvokeAsync(in int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithInParameter<int, decimal>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithInParameter<int, decimal> SetupInvokeAsync(in ItIn<int> result)
			{
				_invokeAsync0 ??= new SetupWithInParameter<int, decimal>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItIn<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "in");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItIn<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "in");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<decimal> InvokeAsync(in int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "in");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.Task.FromResult<decimal>(_mock._invokeAsync0?.Execute(in result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithInReturnValueTask()
	{
		const string method = "ValueTask<decimal> InvokeAsync(in int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithInParameter<int, decimal>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithInParameter<int, decimal> SetupInvokeAsync(in ItIn<int> result)
			{
				_invokeAsync0 ??= new SetupWithInParameter<int, decimal>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItIn<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "in");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItIn<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "in");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<decimal> InvokeAsync(in int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "in");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.ValueTask.FromResult<decimal>(_mock._invokeAsync0?.Execute(in result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRef<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(ref int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				_mock._invokeAsync0?.Invoke(ref result);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRef<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(ref int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				_mock._invokeAsync0?.Invoke(ref result);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReturnTask()
	{
		const string method = "Task<decimal> InvokeAsync(ref int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefParameter<int, decimal>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithRefParameter<int, decimal> SetupInvokeAsync(in ItRef<int> result)
			{
				_invokeAsync0 ??= new SetupWithRefParameter<int, decimal>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRef<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRef<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<decimal> InvokeAsync(ref int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.Task.FromResult<decimal>(_mock._invokeAsync0?.Execute(ref result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReturnValueTask()
	{
		const string method = "ValueTask<decimal> InvokeAsync(ref int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefParameter<int, decimal>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithRefParameter<int, decimal> SetupInvokeAsync(in ItRef<int> result)
			{
				_invokeAsync0 ??= new SetupWithRefParameter<int, decimal>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRef<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRef<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<decimal> InvokeAsync(ref int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.ValueTask.FromResult<decimal>(_mock._invokeAsync0?.Execute(ref result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref readonly");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRefReadOnly<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref readonly");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync(ref readonly int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref readonly");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				_mock._invokeAsync0?.Invoke(in result);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

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
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref readonly");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRefReadOnly<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref readonly");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync(ref readonly int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref readonly");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				_mock._invokeAsync0?.Invoke(in result);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReadonlyReturnTask()
	{
		const string method = "Task<decimal> InvokeAsync(ref readonly int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefReadOnlyParameter<int, decimal>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<int, decimal> SetupInvokeAsync(in ItRefReadOnly<int> result)
			{
				_invokeAsync0 ??= new SetupWithRefReadOnlyParameter<int, decimal>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref readonly");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRefReadOnly<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref readonly");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<decimal> InvokeAsync(ref readonly int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref readonly");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.Task.FromResult<decimal>(_mock._invokeAsync0?.Execute(in result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReadonlyReturnValueTask()
	{
		const string method = "ValueTask<decimal> InvokeAsync(ref readonly int result);";

		const string methods =
			"""
			// InvokeAsync
			private SetupWithRefReadOnlyParameter<int, decimal>? _invokeAsync0;
			private Invocation<int>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<int, decimal> SetupInvokeAsync(in ItRefReadOnly<int> result)
			{
				_invokeAsync0 ??= new SetupWithRefReadOnlyParameter<int, decimal>();
				_invokeAsync0.SetupParameter(result.ValueSetup);
				return _invokeAsync0;
			}

			public void VerifyInvokeAsync(in ItRefReadOnly<int> result, in Times times)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref readonly");
				_invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync(in ItRefReadOnly<int> result, long index)
			{
				_invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref readonly");
				return _invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<decimal> InvokeAsync(ref readonly int result)
			{
				_mock._invokeAsync0Invocation ??= new Invocation<int>("IInterface.InvokeAsync({0})", prefix: "ref readonly");
				_mock._invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.ValueTask.FromResult<decimal>(_mock._invokeAsync0?.Execute(in result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericTask()
	{
		const string method = "Task InvokeAsync<T>(T param);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithParameter<T> SetupInvokeAsync<T>(in It<T> param)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithParameter<T>)_invokeAsync0.GetOrAdd(typeof(T), static _ => new SetupWithParameter<T>());
				invokeAsync0.SetupParameter(param.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T>(in It<T> param, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})"));
				invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T>(in It<T> param, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})"));
				return invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<T>(T param)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				((SetupWithParameter<T>?)_mock._invokeAsync0?.ValueOrDefault(typeof(T)))?.Invoke(param);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericValueTask()
	{
		const string method = "ValueTask InvokeAsync<T>(T param);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithParameter<T> SetupInvokeAsync<T>(in It<T> param)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithParameter<T>)_invokeAsync0.GetOrAdd(typeof(T), static _ => new SetupWithParameter<T>());
				invokeAsync0.SetupParameter(param.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T>(in It<T> param, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})"));
				invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T>(in It<T> param, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})"));
				return invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<T>(T param)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				((SetupWithParameter<T>?)_mock._invokeAsync0?.ValueOrDefault(typeof(T)))?.Invoke(param);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericReturnTask()
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
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})"));
				invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in It<T1> param, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})"));
				return invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T2> InvokeAsync<T1, T2>(T1 param)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				return System.Threading.Tasks.Task.FromResult<T2>(((SetupWithParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(param, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericReturnValueTask()
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
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})"));
				invokeAsync0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in It<T1> param, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})"));
				return invokeAsync0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T2> InvokeAsync<T1, T2>(T1 param)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, param);
				return System.Threading.Tasks.ValueTask.FromResult<T2>(((SetupWithParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(param, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithOutTask()
	{
		const string method = "Task InvokeAsync<T>(out T value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithOutParameter<T> SetupInvokeAsync<T>(in ItOut<T> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithOutParameter<T>)_invokeAsync0.GetOrAdd(typeof(T), static _ => new SetupWithOutParameter<T>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T>(in ItOut<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "out"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T>(in ItOut<T> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "out"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<T>(out T value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "out"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, default);
				if (_mock._invokeAsync0?.TryGetValue(typeof(T), out var setup) == true)
				{
					((SetupWithOutParameter<T>)setup).Invoke(out value);
					return System.Threading.Tasks.Task.CompletedTask;
				}
				else
				{
					value = default;
					return System.Threading.Tasks.Task.CompletedTask;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithOutValueTask()
	{
		const string method = "ValueTask InvokeAsync<T>(out T value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithOutParameter<T> SetupInvokeAsync<T>(in ItOut<T> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithOutParameter<T>)_invokeAsync0.GetOrAdd(typeof(T), static _ => new SetupWithOutParameter<T>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T>(in ItOut<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "out"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T>(in ItOut<T> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "out"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<T>(out T value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "out"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, default);
				if (_mock._invokeAsync0?.TryGetValue(typeof(T), out var setup) == true)
				{
					((SetupWithOutParameter<T>)setup).Invoke(out value);
					return System.Threading.Tasks.ValueTask.CompletedTask;
				}
				else
				{
					value = default;
					return System.Threading.Tasks.ValueTask.CompletedTask;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithOutReturnGenericTask()
	{
		const string method = "Task<T2> InvokeAsync<T1, T2>(out T1 result);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithOutParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItOut<T1> result)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithOutParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithOutParameter<T1, T2>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> result, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "out"));
				invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItOut<T1> result, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "out"));
				return invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T2> InvokeAsync<T1, T2>(out T1 result)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "out"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, default);
				if (_mock._invokeAsync0?.TryGetValue((typeof(T1), typeof(T2)), out var setup) == true)
				{
					return System.Threading.Tasks.Task.FromResult<T2>(((SetupWithOutParameter<T1, T2>)setup).Execute(out result, out var returnValue) == true ? returnValue! : default!);
				}
				else
				{
					result = default;
					return System.Threading.Tasks.Task.FromResult<T2>(default!);
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithOutReturnGenericValueTask()
	{
		const string method = "ValueTask<T2> InvokeAsync<T1, T2>(out T1 result);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithOutParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItOut<T1> result)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithOutParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithOutParameter<T1, T2>());
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItOut<T1> result, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "out"));
				invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItOut<T1> result, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "out"));
				return invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T2> InvokeAsync<T1, T2>(out T1 result)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "out"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, default);
				if (_mock._invokeAsync0?.TryGetValue((typeof(T1), typeof(T2)), out var setup) == true)
				{
					return System.Threading.Tasks.ValueTask.FromResult<T2>(((SetupWithOutParameter<T1, T2>)setup).Execute(out result, out var returnValue) == true ? returnValue! : default!);
				}
				else
				{
					result = default;
					return System.Threading.Tasks.ValueTask.FromResult<T2>(default!);
				}
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithInTask()
	{
		const string method = "Task InvokeAsync<T>(in T value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithInParameter<T> SetupInvokeAsync<T>(in ItIn<T> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithInParameter<T>)_invokeAsync0.GetOrAdd(typeof(T), static _ => new SetupWithInParameter<T>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T>(in ItIn<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "in"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T>(in ItIn<T> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "in"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<T>(in T value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "in"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithInParameter<T>?)_mock._invokeAsync0?.ValueOrDefault(typeof(T)))?.Invoke(in value);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithInValueTask()
	{
		const string method = "ValueTask InvokeAsync<T>(in T value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithInParameter<T> SetupInvokeAsync<T>(in ItIn<T> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithInParameter<T>)_invokeAsync0.GetOrAdd(typeof(T), static _ => new SetupWithInParameter<T>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T>(in ItIn<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "in"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T>(in ItIn<T> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "in"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<T>(in T value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "in"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithInParameter<T>?)_mock._invokeAsync0?.ValueOrDefault(typeof(T)))?.Invoke(in value);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithInReturnGenericTask()
	{
		const string method = "Task<T2> InvokeAsync<T1, T2>(in T1 result);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithInParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItIn<T1> result)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithInParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithInParameter<T1, T2>());
				invokeAsync0.SetupParameter(result.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> result, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "in"));
				invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItIn<T1> result, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "in"));
				return invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T2> InvokeAsync<T1, T2>(in T1 result)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "in"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.Task.FromResult<T2>(((SetupWithInParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(in result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithInReturnGenericValueTask()
	{
		const string method = "ValueTask<T2> InvokeAsync<T1, T2>(in T1 result);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithInParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItIn<T1> result)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithInParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithInParameter<T1, T2>());
				invokeAsync0.SetupParameter(result.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItIn<T1> result, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "in"));
				invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItIn<T1> result, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "in"));
				return invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T2> InvokeAsync<T1, T2>(in T1 result)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "in"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.ValueTask.FromResult<T2>(((SetupWithInParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(in result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefTask()
	{
		const string method = "Task InvokeAsync<T>(ref T value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithRefParameter<T> SetupInvokeAsync<T>(in ItRef<T> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithRefParameter<T>)_invokeAsync0.GetOrAdd(typeof(T), static _ => new SetupWithRefParameter<T>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T>(in ItRef<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "ref"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T>(in ItRef<T> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "ref"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<T>(ref T value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "ref"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefParameter<T>?)_mock._invokeAsync0?.ValueOrDefault(typeof(T)))?.Invoke(ref value);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefValueTask()
	{
		const string method = "ValueTask InvokeAsync<T>(ref T value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithRefParameter<T> SetupInvokeAsync<T>(in ItRef<T> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithRefParameter<T>)_invokeAsync0.GetOrAdd(typeof(T), static _ => new SetupWithRefParameter<T>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T>(in ItRef<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "ref"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T>(in ItRef<T> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "ref"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<T>(ref T value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "ref"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefParameter<T>?)_mock._invokeAsync0?.ValueOrDefault(typeof(T)))?.Invoke(ref value);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReturnGenericTask()
	{
		const string method = "Task<T2> InvokeAsync<T1, T2>(ref T1 result);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithRefParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItRef<T1> result)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithRefParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefParameter<T1, T2>());
				invokeAsync0.SetupParameter(result.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> result, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref"));
				invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItRef<T1> result, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref"));
				return invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T2> InvokeAsync<T1, T2>(ref T1 result)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.Task.FromResult<T2>(((SetupWithRefParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(ref result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReturnGenericValueTask()
	{
		const string method = "ValueTask<T2> InvokeAsync<T1, T2>(ref T1 result);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithRefParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItRef<T1> result)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithRefParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefParameter<T1, T2>());
				invokeAsync0.SetupParameter(result.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItRef<T1> result, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref"));
				invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItRef<T1> result, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref"));
				return invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T2> InvokeAsync<T1, T2>(ref T1 result)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.ValueTask.FromResult<T2>(((SetupWithRefParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(ref result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefReadOnlyTask()
	{
		const string method = "Task InvokeAsync<T>(ref readonly T value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<T> SetupInvokeAsync<T>(in ItRefReadOnly<T> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithRefReadOnlyParameter<T>)_invokeAsync0.GetOrAdd(typeof(T), static _ => new SetupWithRefReadOnlyParameter<T>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T>(in ItRefReadOnly<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "ref readonly"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T>(in ItRefReadOnly<T> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "ref readonly"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task InvokeAsync<T>(ref readonly T value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "ref readonly"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefReadOnlyParameter<T>?)_mock._invokeAsync0?.ValueOrDefault(typeof(T)))?.Invoke(in value);
				return System.Threading.Tasks.Task.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefReadOnlyValueTask()
	{
		const string method = "ValueTask InvokeAsync<T>(ref readonly T value);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invokeAsync0;
			private InvocationDictionary? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<T> SetupInvokeAsync<T>(in ItRefReadOnly<T> value)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invokeAsync0 = (SetupWithRefReadOnlyParameter<T>)_invokeAsync0.GetOrAdd(typeof(T), static _ => new SetupWithRefReadOnlyParameter<T>());
				invokeAsync0.SetupParameter(value.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T>(in ItRefReadOnly<T> value, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "ref readonly"));
				invokeAsync0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T>(in ItRefReadOnly<T> value, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "ref readonly"));
				return invokeAsync0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask InvokeAsync<T>(ref readonly T value)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary();
				var invokeAsync0Invocation = (Invocation<T>)_mock._invokeAsync0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.InvokeAsync<{key.Name}>({0})", prefix: "ref readonly"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefReadOnlyParameter<T>?)_mock._invokeAsync0?.ValueOrDefault(typeof(T)))?.Invoke(in value);
				return System.Threading.Tasks.ValueTask.CompletedTask;
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReadOnlyReturnGenericTask()
	{
		const string method = "Task<T2> InvokeAsync<T1, T2>(ref readonly T1 result);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItRefReadOnly<T1> result)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithRefReadOnlyParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefReadOnlyParameter<T1, T2>());
				invokeAsync0.SetupParameter(result.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> result, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref readonly"));
				invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> result, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref readonly"));
				return invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.Task<T2> InvokeAsync<T1, T2>(ref readonly T1 result)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref readonly"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.Task.FromResult<T2>(((SetupWithRefReadOnlyParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(in result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReadOnlyReturnGenericValueTask()
	{
		const string method = "ValueTask<T2> InvokeAsync<T1, T2>(ref readonly T1 result);";

		const string methods =
			"""
			// InvokeAsync
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invokeAsync0;
			private InvocationDictionary<(System.Type, System.Type)>? _invokeAsync0Invocation;

			public SetupWithRefReadOnlyParameter<T1, T2> SetupInvokeAsync<T1, T2>(in ItRefReadOnly<T1> result)
			{
				_invokeAsync0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invokeAsync0 = (SetupWithRefReadOnlyParameter<T1, T2>)_invokeAsync0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefReadOnlyParameter<T1, T2>());
				invokeAsync0.SetupParameter(result.ValueSetup);
				return invokeAsync0;
			}

			public void VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> result, in Times times)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref readonly"));
				invokeAsync0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvokeAsync<T1, T2>(in ItRefReadOnly<T1> result, long index)
			{
				_invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref readonly"));
				return invokeAsync0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public System.Threading.Tasks.ValueTask<T2> InvokeAsync<T1, T2>(ref readonly T1 result)
			{
				_mock._invokeAsync0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invokeAsync0Invocation = (Invocation<T1>)_mock._invokeAsync0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.InvokeAsync<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref readonly"));
				invokeAsync0Invocation.Register(_mock._invocationIndex, result);
				return System.Threading.Tasks.ValueTask.FromResult<T2>(((SetupWithRefReadOnlyParameter<T1, T2>?)_mock._invokeAsync0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(in result, out var returnValue) == true ? returnValue! : default!);
			}
			""";

		const string verifyNoOtherCalls = "_invokeAsync0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invokeAsync0Invocation;";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
