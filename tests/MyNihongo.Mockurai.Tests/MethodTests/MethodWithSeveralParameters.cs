namespace MyNihongo.Mockurai.Tests.MethodTests;

public sealed class MethodWithSeveralParameters : MethodTestsBase
{
	[Fact]
	public async Task GenerateInterface()
	{
		const string method = "void Invoke(int param1, float param2);";

		const string methods =
			"""
			// Invoke
			private SetupInt32Single? _invoke0;
			private InvocationInt32Single? _invoke0Invocation;

			public SetupInt32Single SetupInvoke(in It<int> param1, in It<float> param2)
			{
				_invoke0 ??= new SetupInt32Single();
				_invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in It<int> param1, in It<float> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationInt32Single("IInterface.Invoke({0}, {1})");
				_invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in It<int> param1, in It<float> param2, long index)
			{
				_invoke0Invocation ??= new InvocationInt32Single("IInterface.Invoke({0}, {1})");
				return _invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(int param1, float param2)
			{
				_mock._invoke0Invocation ??= new InvocationInt32Single("IInterface.Invoke({0}, {1})");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
				_mock._invoke0?.Invoke(param1, param2);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		string[] types = ["Int32", "Single"];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceReturn()
	{
		const string method = "decimal Invoke(float param1, int param2);";

		const string methods =
			"""
			// Invoke
			private SetupSingleInt32<decimal>? _invoke0;
			private InvocationSingleInt32? _invoke0Invocation;

			public SetupSingleInt32<decimal> SetupInvoke(in It<float> param1, in It<int> param2)
			{
				_invoke0 ??= new SetupSingleInt32<decimal>();
				_invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in It<float> param1, in It<int> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationSingleInt32("IInterface.Invoke({0}, {1})");
				_invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in It<float> param1, in It<int> param2, long index)
			{
				_invoke0Invocation ??= new InvocationSingleInt32("IInterface.Invoke({0}, {1})");
				return _invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public decimal Invoke(float param1, int param2)
			{
				_mock._invoke0Invocation ??= new InvocationSingleInt32("IInterface.Invoke({0}, {1})");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
				return _mock._invoke0?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		string[] types = ["Single", "Int32"];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupReturnsCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceThreeParams()
	{
		const string method = "void Invoke(int param1, float param2, string param3);";

		const string methods =
			"""
			// Invoke
			private SetupInt32SingleString? _invoke0;
			private InvocationInt32SingleString? _invoke0Invocation;

			public SetupInt32SingleString SetupInvoke(in It<int> param1, in It<float> param2, in It<string> param3)
			{
				_invoke0 ??= new SetupInt32SingleString();
				_invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup, param3.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in It<int> param1, in It<float> param2, in It<string> param3, in Times times)
			{
				_invoke0Invocation ??= new InvocationInt32SingleString("IInterface.Invoke({0}, {1}, {2})");
				_invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, param3.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in It<int> param1, in It<float> param2, in It<string> param3, long index)
			{
				_invoke0Invocation ??= new InvocationInt32SingleString("IInterface.Invoke({0}, {1}, {2})");
				return _invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, param3.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(int param1, float param2, string param3)
			{
				_mock._invoke0Invocation ??= new InvocationInt32SingleString("IInterface.Invoke({0}, {1}, {2})");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, param1, param2, param3);
				_mock._invoke0?.Invoke(param1, param2, param3);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		string[] types = ["Int32", "Single", "String"];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1()
	{
		const string method = "void Invoke<T>(T param1, float param2);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupT1Single<T> SetupInvoke<T>(in It<T> param1, in It<float> param2)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupT1Single<T>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupT1Single<T>());
				invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T>(in It<T> param1, in It<float> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationT1Single<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationT1Single<T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T>(in It<T> param1, in It<float> param2, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationT1Single<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationT1Single<T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T>(T param1, float param2)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationT1Single<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationT1Single<T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
				((SetupT1Single<T>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Invoke(param1, param2);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		TypeModel[] types =
		[
			new("T1", 1, isGeneric: true),
			new("Single", 2),
		];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2()
	{
		const string method = "void Invoke<T>(int param1, T param2);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupInt32T1<T> SetupInvoke<T>(in It<int> param1, in It<T> param2)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupInt32T1<T>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupInt32T1<T>());
				invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T>(in It<int> param1, in It<T> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationInt32T1<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32T1<T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T>(in It<int> param1, in It<T> param2, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationInt32T1<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32T1<T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T>(int param1, T param2)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationInt32T1<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32T1<T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
				((SetupInt32T1<T>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Invoke(param1, param2);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		TypeModel[] types =
		[
			new("Int32", 1),
			new("T1", 2, isGeneric: true),
		];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric3()
	{
		const string method = "void Invoke<T>(T param1, T param2);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupT1T2<T, T> SetupInvoke<T>(in It<T> param1, in It<T> param2)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupT1T2<T, T>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupT1T2<T, T>());
				invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T>(in It<T> param1, in It<T> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationT1T2<T, T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationT1T2<T, T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T>(in It<T> param1, in It<T> param2, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationT1T2<T, T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationT1T2<T, T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T>(T param1, T param2)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationT1T2<T, T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationT1T2<T, T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
				((SetupT1T2<T, T>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Invoke(param1, param2);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		TypeModel[] types =
		[
			new("T1", 1, isGeneric: true),
			new("T2", 2, isGeneric: true),
		];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRef()
	{
		const string method = "void Invoke<T>(ref T param1, T param2);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupT1T2<T, T> SetupInvoke<T>(in It<T> param1, in It<T> param2)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupT1T2<T, T>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupT1T2<T, T>());
				invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T>(in It<T> param1, in It<T> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationT1T2<T, T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationT1T2<T, T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T>(in It<T> param1, in It<T> param2, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationT1T2<T, T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationT1T2<T, T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T>(T param1, T param2)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationT1T2<T, T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationT1T2<T, T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
				((SetupT1T2<T, T>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Invoke(param1, param2);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		TypeModel[] types =
		[
			new("T1", 1, refType: "ref", isGeneric: true),
			new("T2", 2, isGeneric: true),
		];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceMultipleGeneric1()
	{
		const string method = "void Invoke<T2, T1>(T1 param1, float param2);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupT1Single<T1> SetupInvoke<T2, T1>(in It<T1> param1, in It<float> param2)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupT1Single<T1>)_invoke0.GetOrAdd((typeof(T2), typeof(T1)), static _ => new SetupT1Single<T1>());
				invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T2, T1>(in It<T1> param1, in It<float> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (InvocationT1Single<T1>)_invoke0Invocation.GetOrAdd((typeof(T2), typeof(T1)), static key => new InvocationT1Single<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0}, {1})"));
				invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T2, T1>(in It<T1> param1, in It<float> param2, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (InvocationT1Single<T1>)_invoke0Invocation.GetOrAdd((typeof(T2), typeof(T1)), static key => new InvocationT1Single<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0}, {1})"));
				return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T2, T1>(T1 param1, float param2)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (InvocationT1Single<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T2), typeof(T1)), static key => new InvocationT1Single<T1>($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>({0}, {1})"));
				invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
				((SetupT1Single<T1>?)_mock._invoke0?.ValueOrDefault((typeof(T2), typeof(T1))))?.Invoke(param1, param2);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		TypeModel[] types =
		[
			new("T1", 1, isGeneric: true),
			new("Single", 2),
		];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceReturnGeneric1()
	{
		const string method = "T Invoke<T>(T param1, T param2);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupT1T2<T, T, T> SetupInvoke<T>(in It<T> param1, in It<T> param2)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupT1T2<T, T, T>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupT1T2<T, T, T>());
				invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T>(in It<T> param1, in It<T> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationT1T2<T, T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationT1T2<T, T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T>(in It<T> param1, in It<T> param2, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationT1T2<T, T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationT1T2<T, T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T Invoke<T>(T param1, T param2)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationT1T2<T, T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationT1T2<T, T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
				return ((SetupT1T2<T, T, T>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		TypeModel[] types =
		[
			new("T1", 1, isGeneric: true),
			new("T2", 2, isGeneric: true),
		];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupReturnsCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceReturnGeneric2()
	{
		const string method = "decimal Invoke<T>(int param1, T param2);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupInt32T1<T, decimal> SetupInvoke<T>(in It<int> param1, in It<T> param2)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupInt32T1<T, decimal>)_invoke0.GetOrAdd(typeof(T), static _ => new SetupInt32T1<T, decimal>());
				invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T>(in It<int> param1, in It<T> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationInt32T1<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32T1<T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T>(in It<int> param1, in It<T> param2, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationInt32T1<T>)_invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32T1<T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				return invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public decimal Invoke<T>(int param1, T param2)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (InvocationInt32T1<T>)_mock._invoke0Invocation.GetOrAdd(typeof(T), static key => new InvocationInt32T1<T>($"IInterface.Invoke<{key.Name}>({0}, {1})"));
				invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
				return ((SetupInt32T1<T, decimal>?)_mock._invoke0?.ValueOrDefault(typeof(T)))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		TypeModel[] types =
		[
			new("Int32", 1),
			new("T1", 2, isGeneric: true),
		];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupReturnsCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithIn()
	{
		const string method = "void Invoke(in int param1, float param2);";

		const string methods =
			"""
			// Invoke
			private SetupInInt32Single? _invoke0;
			private InvocationInInt32Single? _invoke0Invocation;

			public SetupInInt32Single SetupInvoke(in ItIn<int> param1, in It<float> param2)
			{
				_invoke0 ??= new SetupInInt32Single();
				_invoke0.SetupParameters(param1.ValueSetup, param2.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItIn<int> param1, in It<float> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationInInt32Single("IInterface.Invoke({0}, {1})", prefixParam1: "in");
				_invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItIn<int> param1, in It<float> param2, long index)
			{
				_invoke0Invocation ??= new InvocationInInt32Single("IInterface.Invoke({0}, {1})", prefixParam1: "in");
				return _invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(in int param1, float param2)
			{
				_mock._invoke0Invocation ??= new InvocationInInt32Single("IInterface.Invoke({0}, {1})", prefixParam1: "in");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, param1, param2);
				_mock._invoke0?.Invoke(in param1, param2);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		TypeModel[] types =
		[
			new("Int32", 1, refType: "in"),
			new("Single", 2),
		];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithInOut()
	{
		const string method = "void Invoke(in int param1, out float param2);";

		const string methods =
			"""
			// Invoke
			private SetupInInt32OutSingle? _invoke0;
			private InvocationInInt32OutSingle? _invoke0Invocation;

			public SetupInInt32OutSingle SetupInvoke(in ItIn<int> param1, in ItOut<float> param2)
			{
				_invoke0 ??= new SetupInInt32OutSingle();
				_invoke0.SetupParameter(param1.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItIn<int> param1, in ItOut<float> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationInInt32OutSingle("IInterface.Invoke({0}, {1})", prefixParam1: "in", prefixParam2: "out");
				_invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItIn<int> param1, in ItOut<float> param2, long index)
			{
				_invoke0Invocation ??= new InvocationInInt32OutSingle("IInterface.Invoke({0}, {1})", prefixParam1: "in", prefixParam2: "out");
				return _invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(in int param1, out float param2)
			{
				_mock._invoke0Invocation ??= new InvocationInInt32OutSingle("IInterface.Invoke({0}, {1})", prefixParam1: "in", prefixParam2: "out");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, param1, default);
				if (_mock._invoke0 is not null)
				{
					_mock._invoke0.Invoke(in param1, out param2);
				}
				else
				{
					param2 = default;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		TypeModel[] types =
		[
			new("Int32", 1, refType: "in"),
			new("Single", 2, refType: "out"),
		];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceReturnOutRef()
	{
		const string method = "decimal Invoke(out int param1, ref float param2);";

		const string methods =
			"""
			// Invoke
			private SetupOutInt32RefSingle<decimal>? _invoke0;
			private InvocationOutInt32RefSingle? _invoke0Invocation;

			public SetupOutInt32RefSingle<decimal> SetupInvoke(in ItOut<int> param1, in ItRef<float> param2)
			{
				_invoke0 ??= new SetupOutInt32RefSingle<decimal>();
				_invoke0.SetupParameter(param2.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItOut<int> param1, in ItRef<float> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationOutInt32RefSingle("IInterface.Invoke({0}, {1})", prefixParam1: "out", prefixParam2: "ref");
				_invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<int> param1, in ItRef<float> param2, long index)
			{
				_invoke0Invocation ??= new InvocationOutInt32RefSingle("IInterface.Invoke({0}, {1})", prefixParam1: "out", prefixParam2: "ref");
				return _invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public decimal Invoke(out int param1, ref float param2)
			{
				_mock._invoke0Invocation ??= new InvocationOutInt32RefSingle("IInterface.Invoke({0}, {1})", prefixParam1: "out", prefixParam2: "ref");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, default, param2);
				if (_mock._invoke0 is not null)
				{
					return _mock._invoke0.Execute(out param1, ref param2, out var returnValue) == true ? returnValue! : default!;
				}
				else
				{
					param1 = default;
					return default;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		TypeModel[] types =
		[
			new("Int32", 1, refType: "out"),
			new("Single", 2, refType: "ref"),
		];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupReturnsCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceOutOut()
	{
		const string method = "void Invoke(out int param1, out float param2);";

		const string methods =
			"""
			// Invoke
			private SetupOutInt32OutSingle? _invoke0;
			private InvocationOutInt32OutSingle? _invoke0Invocation;

			public SetupOutInt32OutSingle SetupInvoke(in ItOut<int> param1, in ItOut<float> param2)
			{
				_invoke0 ??= new SetupOutInt32OutSingle();
				return _invoke0;
			}

			public void VerifyInvoke(in ItOut<int> param1, in ItOut<float> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationOutInt32OutSingle("IInterface.Invoke({0}, {1})", prefixParam1: "out", prefixParam2: "out");
				_invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<int> param1, in ItOut<float> param2, long index)
			{
				_invoke0Invocation ??= new InvocationOutInt32OutSingle("IInterface.Invoke({0}, {1})", prefixParam1: "out", prefixParam2: "out");
				return _invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(out int param1, out float param2)
			{
				_mock._invoke0Invocation ??= new InvocationOutInt32OutSingle("IInterface.Invoke({0}, {1})", prefixParam1: "out", prefixParam2: "out");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, default, default);
				if (_mock._invoke0 is not null)
				{
					_mock._invoke0.Invoke(out param1, out param2);
				}
				else
				{
					param1 = default;
					param2 = default;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		TypeModel[] types =
		[
			new("Int32", 1, refType: "out"),
			new("Single", 2, refType: "out"),
		];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceReturnOutOut()
	{
		const string method = "decimal Invoke(out int param1, out float param2);";

		const string methods =
			"""
			// Invoke
			private SetupOutInt32OutSingle<decimal>? _invoke0;
			private InvocationOutInt32OutSingle? _invoke0Invocation;

			public SetupOutInt32OutSingle<decimal> SetupInvoke(in ItOut<int> param1, in ItOut<float> param2)
			{
				_invoke0 ??= new SetupOutInt32OutSingle<decimal>();
				return _invoke0;
			}

			public void VerifyInvoke(in ItOut<int> param1, in ItOut<float> param2, in Times times)
			{
				_invoke0Invocation ??= new InvocationOutInt32OutSingle("IInterface.Invoke({0}, {1})", prefixParam1: "out", prefixParam2: "out");
				_invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<int> param1, in ItOut<float> param2, long index)
			{
				_invoke0Invocation ??= new InvocationOutInt32OutSingle("IInterface.Invoke({0}, {1})", prefixParam1: "out", prefixParam2: "out");
				return _invoke0Invocation.Verify(param1.ValueSetup, param2.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public decimal Invoke(out int param1, out float param2)
			{
				_mock._invoke0Invocation ??= new InvocationOutInt32OutSingle("IInterface.Invoke({0}, {1})", prefixParam1: "out", prefixParam2: "out");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, default, default);
				if (_mock._invoke0 is not null)
				{
					return _mock._invoke0.Execute(out param1, out param2, out var returnValue) == true ? returnValue! : default!;
				}
				else
				{
					param1 = default;
					param2 = default;
					return default;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		TypeModel[] types =
		[
			new("Int32", 1, refType: "out"),
			new("Single", 2, refType: "out"),
		];
		var testCode = CreateInterfaceTestCode(method);
		var setupCode = CreateSetupReturnsCode(types);
		var invocationCode = CreateInvocationCode(types);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, setupCode, invocationCode);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
