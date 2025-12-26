namespace MyNihongo.Mock.Tests.MethodTests;

public sealed class MethodWithOneParameterShould : MethodTestsBase
{
	[Fact]
	public async Task GenerateInterfaceWithOutParameterReturn()
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
				_invoke0Invocation.Verify(result, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface.Invoke({0})", prefix: "out");
				return _invoke0Invocation.Verify(result, index, _invocationProviders);
			}
			""";

		const string proxy = "public decimal Invoke(out int result) {result = default;return default;}";

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
				invoke0Invocation.Verify(result, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in ItOut<T1> result, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0})", prefix: "out"));
				return invoke0Invocation.Verify(result, index, _invocationProviders);
			}
			""";

		const string proxy = "public T2 Invoke<T1, T2>(out T1 result) {result = default;return default;}";

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
}
