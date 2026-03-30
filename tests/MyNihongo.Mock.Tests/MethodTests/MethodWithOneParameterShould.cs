namespace MyNihongo.Mock.Tests.MethodTests;

public sealed class MethodWithOneParameterShould : MethodTestsBase
{
	[Fact]
	public async Task GenerateInterface()
	{
		const string method = "void Invoke(int param);";

		const string methods =
			"""
			// Invoke
			private SetupWithParameter<int>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithParameter<int> SetupInvoke(in It<int> param)
			{
				_invoke0 ??= new SetupWithParameter<int>();
				_invoke0.SetupParameter(param.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in It<int> param, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})");
				_invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in It<int> param, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})");
				return _invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(int param)
			{
				_mock._invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, param);
				_mock._invoke0?.Invoke(param);
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
		const string method = "float Invoke(int param);";

		const string methods =
			"""
			// Invoke
			private SetupWithParameter<int, float>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithParameter<int, float> SetupInvoke(in It<int> param)
			{
				_invoke0 ??= new SetupWithParameter<int, float>();
				_invoke0.SetupParameter(param.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in It<int> param, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})");
				_invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in It<int> param, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})");
				return _invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public float Invoke(int param)
			{
				_mock._invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, param);
				return _mock._invoke0?.Execute(param, out var returnValue) == true ? returnValue! : default!;
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
	public async Task GenerateInterfaceWithOut()
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
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(out int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, default);
				if (_mock._invoke0 is not null)
				{
					_mock._invoke0.Invoke(out result);
				}
				else
				{
					result = default;
				}
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
	public async Task GenerateInterfaceWithOutReturn()
	{
		const string method = "decimal Invoke(out int result);";

		const string methods =
			"""
			// Invoke
			private SetupWithOutParameter<int, decimal>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithOutParameter<int, decimal> SetupInvoke(in ItOut<int> result)
			{
				_invoke0 ??= new SetupWithOutParameter<int, decimal>();
				return _invoke0;
			}

			public void VerifyInvoke(in ItOut<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public decimal Invoke(out int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, default);
				if (_mock._invoke0 is not null)
				{
					return _mock._invoke0.Execute(out result, out var returnValue) == true ? returnValue! : default!;
				}
				else
				{
					result = default;
					return default;
				}
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
	public async Task GenerateInterfaceWithIn()
	{
		const string method = "void Invoke(in int result);";

		const string methods =
			"""
			// Invoke
			private SetupWithInParameter<int>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithInParameter<int> SetupInvoke(in ItIn<int> result)
			{
				_invoke0 ??= new SetupWithInParameter<int>();
				_invoke0.SetupParameter(result.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItIn<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "in");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItIn<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "in");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(in int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "in");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, result);
				_mock._invoke0?.Invoke(in result);
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
	public async Task GenerateInterfaceWithInReturn()
	{
		const string method = "decimal Invoke(in int result);";

		const string methods =
			"""
			// Invoke
			private SetupWithInParameter<int, decimal>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithInParameter<int, decimal> SetupInvoke(in ItIn<int> result)
			{
				_invoke0 ??= new SetupWithInParameter<int, decimal>();
				_invoke0.SetupParameter(result.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItIn<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "in");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItIn<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "in");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public decimal Invoke(in int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "in");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, result);
				return _mock._invoke0?.Execute(in result, out var returnValue) == true ? returnValue! : default!;
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
	public async Task GenerateInterfaceWithRef()
	{
		const string method = "void Invoke(ref int result);";

		const string methods =
			"""
			// Invoke
			private SetupWithRefParameter<int>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithRefParameter<int> SetupInvoke(in ItRef<int> result)
			{
				_invoke0 ??= new SetupWithRefParameter<int>();
				_invoke0.SetupParameter(result.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItRef<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "ref");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItRef<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "ref");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(ref int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "ref");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, result);
				_mock._invoke0?.Invoke(ref result);
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
	public async Task GenerateInterfaceWithRefReturn()
	{
		const string method = "decimal Invoke(ref int result);";

		const string methods =
			"""
			// Invoke
			private SetupWithRefParameter<int, decimal>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithRefParameter<int, decimal> SetupInvoke(in ItRef<int> result)
			{
				_invoke0 ??= new SetupWithRefParameter<int, decimal>();
				_invoke0.SetupParameter(result.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItRef<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "ref");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItRef<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "ref");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public decimal Invoke(ref int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "ref");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, result);
				return _mock._invoke0?.Execute(ref result, out var returnValue) == true ? returnValue! : default!;
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
	public async Task GenerateInterfaceWithRefReadonly()
	{
		const string method = "void Invoke(ref readonly int result);";

		const string methods =
			"""
			// Invoke
			private SetupWithRefReadOnlyParameter<int>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithRefReadOnlyParameter<int> SetupInvoke(in ItRefReadOnly<int> result)
			{
				_invoke0 ??= new SetupWithRefReadOnlyParameter<int>();
				_invoke0.SetupParameter(result.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItRefReadOnly<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "ref readonly");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItRefReadOnly<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "ref readonly");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(ref readonly int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "ref readonly");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, result);
				_mock._invoke0?.Invoke(in result);
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
	public async Task GenerateInterfaceWithRefReadonlyReturn()
	{
		const string method = "decimal Invoke(ref readonly int result);";

		const string methods =
			"""
			// Invoke
			private SetupWithRefReadOnlyParameter<int, decimal>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithRefReadOnlyParameter<int, decimal> SetupInvoke(in ItRefReadOnly<int> result)
			{
				_invoke0 ??= new SetupWithRefReadOnlyParameter<int, decimal>();
				_invoke0.SetupParameter(result.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItRefReadOnly<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "ref readonly");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItRefReadOnly<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "ref readonly");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public decimal Invoke(ref readonly int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "ref readonly");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, result);
				return _mock._invoke0?.Execute(in result, out var returnValue) == true ? returnValue! : default!;
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
		const string method = "void Invoke<T>(T param);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithParameter<T> SetupInvoke<T>(in It<T> param)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithParameter<T>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupWithParameter<T>());
				invoke0.SetupParameter(param.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T>(in It<T> param, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})"));
				invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T>(in It<T> param, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})"));
				return invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T>(T param)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})"));
				invoke0Invocation.Register(_mock._invocationIndex, param);
				((SetupWithParameter<T>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Invoke(param);
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
	public async Task GenerateInterfaceGenericReturn()
	{
		const string method = "T2 Invoke<T1, T2>(T1 param);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithParameter<T1, T2> SetupInvoke<T1, T2>(in It<T1> param)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithParameter<T1, T2>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithParameter<T1, T2>());
				invoke0.SetupParameter(param.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in It<T1> param, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})"));
				invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in It<T1> param, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})"));
				return invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T2 Invoke<T1, T2>(T1 param)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})"));
				invoke0Invocation.Register(_mock._invocationIndex, param);
				return ((SetupWithParameter<T1, T2>?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(param, out var returnValue) == true ? returnValue! : default!;
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
	public async Task GenerateInterfaceGenericWithOut()
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
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T>(in ItOut<T> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})", prefix: "out"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T>(out T value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})", prefix: "out"));
				invoke0Invocation.Register(_mock._invocationIndex, default);
				if (_mock._invoke0?.TryGetValue(typeof(T), out var setup) == true)
				{
					((SetupWithOutParameter<T>)setup).Invoke(out value);
				}
				else
				{
					value = default;
				}
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
	public async Task GenerateInterfaceWithOutReturnGeneric()
	{
		const string method = "T2 Invoke<T1, T2>(out T1 result);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithOutParameter<T1, T2> SetupInvoke<T1, T2>(in ItOut<T1> result)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithOutParameter<T1, T2>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithOutParameter<T1, T2>());
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in ItOut<T1> result, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "out"));
				invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in ItOut<T1> result, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "out"));
				return invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T2 Invoke<T1, T2>(out T1 result)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "out"));
				invoke0Invocation.Register(_mock._invocationIndex, default);
				if (_mock._invoke0?.TryGetValue((typeof(T1), typeof(T2)), out var setup) == true)
				{
					return ((SetupWithOutParameter<T1, T2>)setup).Execute(out result, out var returnValue) == true ? returnValue! : default!;
				}
				else
				{
					result = default;
					return default;
				}
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
	public async Task GenerateInterfaceGenericWithIn()
	{
		const string method = "void Invoke<T>(in T value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithInParameter<T> SetupInvoke<T>(in ItIn<T> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithInParameter<T>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupWithInParameter<T>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T>(in ItIn<T> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})", prefix: "in"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T>(in ItIn<T> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})", prefix: "in"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T>(in T value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})", prefix: "in"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithInParameter<T>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Invoke(in value);
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
	public async Task GenerateInterfaceWithInReturnGeneric()
	{
		const string method = "T2 Invoke<T1, T2>(in T1 result);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithInParameter<T1, T2> SetupInvoke<T1, T2>(in ItIn<T1> result)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithInParameter<T1, T2>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithInParameter<T1, T2>());
				invoke0.SetupParameter(result.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in ItIn<T1> result, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "in"));
				invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in ItIn<T1> result, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "in"));
				return invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T2 Invoke<T1, T2>(in T1 result)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "in"));
				invoke0Invocation.Register(_mock._invocationIndex, result);
				return ((SetupWithInParameter<T1, T2>?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(in result, out var returnValue) == true ? returnValue! : default!;
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
	public async Task GenerateInterfaceGenericWithRef()
	{
		const string method = "void Invoke<T>(ref T value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithRefParameter<T> SetupInvoke<T>(in ItRef<T> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithRefParameter<T>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupWithRefParameter<T>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T>(in ItRef<T> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})", prefix: "ref"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T>(in ItRef<T> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})", prefix: "ref"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T>(ref T value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})", prefix: "ref"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefParameter<T>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Invoke(ref value);
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
	public async Task GenerateInterfaceWithRefReturnGeneric()
	{
		const string method = "T2 Invoke<T1, T2>(ref T1 result);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithRefParameter<T1, T2> SetupInvoke<T1, T2>(in ItRef<T1> result)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithRefParameter<T1, T2>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefParameter<T1, T2>());
				invoke0.SetupParameter(result.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in ItRef<T1> result, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref"));
				invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in ItRef<T1> result, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref"));
				return invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T2 Invoke<T1, T2>(ref T1 result)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref"));
				invoke0Invocation.Register(_mock._invocationIndex, result);
				return ((SetupWithRefParameter<T1, T2>?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(ref result, out var returnValue) == true ? returnValue! : default!;
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
	public async Task GenerateInterfaceGenericWithRefReadOnly()
	{
		const string method = "void Invoke<T>(ref readonly T value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithRefReadOnlyParameter<T> SetupInvoke<T>(in ItRefReadOnly<T> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithRefReadOnlyParameter<T>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupWithRefReadOnlyParameter<T>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T>(in ItRefReadOnly<T> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})", prefix: "ref readonly"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T>(in ItRefReadOnly<T> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})", prefix: "ref readonly"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T>(ref readonly T value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new Invocation<T>($"IInterface.Invoke<{key.Name}>({0})", prefix: "ref readonly"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefReadOnlyParameter<T>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Invoke(in value);
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
	public async Task GenerateInterfaceWithRefReadOnlyReturnGeneric()
	{
		const string method = "T2 Invoke<T1, T2>(ref readonly T1 result);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithRefReadOnlyParameter<T1, T2> SetupInvoke<T1, T2>(in ItRefReadOnly<T1> result)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithRefReadOnlyParameter<T1, T2>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefReadOnlyParameter<T1, T2>());
				invoke0.SetupParameter(result.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in ItRefReadOnly<T1> result, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref readonly"));
				invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in ItRefReadOnly<T1> result, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref readonly"));
				return invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T2 Invoke<T1, T2>(ref readonly T1 result)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "ref readonly"));
				invoke0Invocation.Register(_mock._invocationIndex, result);
				return ((SetupWithRefReadOnlyParameter<T1, T2>?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(in result, out var returnValue) == true ? returnValue! : default!;
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
	public async Task GenerateClass()
	{
		const string method =
			"""
			public virtual void Invoke(int parameter) {}
			protected virtual void Invoke2(in decimal parameter) {}
			public void Invoke3(ref float parameter) {}
			""";

		const string methods =
			"""
			// Invoke
			private SetupWithParameter<int>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithParameter<int> SetupInvoke(in It<int> parameter)
			{
				_invoke0 ??= new SetupWithParameter<int>();
				_invoke0.SetupParameter(parameter.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in It<int> parameter, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>("Class.Invoke({0})");
				_invoke0Invocation.Verify(parameter.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in It<int> parameter, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("Class.Invoke({0})");
				return _invoke0Invocation.Verify(parameter.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public override void Invoke(int parameter)
			{
				_mock._invoke0Invocation ??= new Invocation<int>("Class.Invoke({0})");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, parameter);
				_mock._invoke0?.Invoke(parameter);
			}
			""";

		const string extensions =
			"""
			// Invoke
			public SetupWithParameter<int> SetupInvoke(in It<int> parameter = default) =>
				((ClassMock)@this).SetupInvoke(parameter);

			public void VerifyInvoke(in It<int> parameter, in Times times) =>
				((ClassMock)@this).VerifyInvoke(parameter, times);

			public void VerifyInvoke(in It<int> parameter, System.Func<Times> times) =>
				((ClassMock)@this).VerifyInvoke(parameter, times());
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		var testCode = CreateClassTestCode(method);
		var generatedSources = CreateClassGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
