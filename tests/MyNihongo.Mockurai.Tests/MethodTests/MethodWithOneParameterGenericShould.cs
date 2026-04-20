namespace MyNihongo.Mockurai.Tests.MethodTests;

public sealed class MethodWithOneParameterGenericShould : MethodGenericTestsBase
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
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
				_invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in It<int> param, long index)
			{
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
				return _invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(int param)
			{
				_mock._invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, param);
				_mock._invoke0?.Invoke(param);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action<int>> SetupInvoke(in It<int> param = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(param);

			public void VerifyInvoke(in It<int> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(param, times);

			public void VerifyInvoke(in It<int> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(param, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in It<int> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<int>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

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
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
				_invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in It<int> param, long index)
			{
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
				return _invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public float Invoke(int param)
			{
				_mock._invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, param);
				return _mock._invoke0?.Execute(param, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action<int>, float, System.Func<int, float>> SetupInvoke(in It<int> param = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(param);

			public void VerifyInvoke(in It<int> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(param, times);

			public void VerifyInvoke(in It<int> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(param, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in It<int> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<int>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

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
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "out");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "out");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(out int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "out");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invoke0 is not null)
				{
					_mock._invoke0.Invoke(out result!);
				}
				else
				{
					result = default!;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionOut<int>> SetupInvoke(in ItOut<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(result);

			public void VerifyInvoke(in ItOut<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times);

			public void VerifyInvoke(in ItOut<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItOut<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<int>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

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
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "out");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "out");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public decimal Invoke(out int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "out");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invoke0 is not null)
				{
					return _mock._invoke0.Execute(out result!, out var returnValue) == true ? returnValue! : default!;
				}
				else
				{
					result = default!;
					return default!;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionOut<int>, decimal, System.FuncOut<int, decimal>> SetupInvoke(in ItOut<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(result);

			public void VerifyInvoke(in ItOut<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times);

			public void VerifyInvoke(in ItOut<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItOut<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<int>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

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
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "in");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItIn<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "in");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(in int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "in");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, result);
				_mock._invoke0?.Invoke(in result);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionIn<int>> SetupInvoke(in ItIn<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(result);

			public void VerifyInvoke(in ItIn<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times);

			public void VerifyInvoke(in ItIn<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItIn<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<int>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithInReturn()
	{
		const string method = "float Invoke(in int result);";

		const string methods =
			"""
			// Invoke
			private SetupWithInParameter<int, float>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithInParameter<int, float> SetupInvoke(in ItIn<int> result)
			{
				_invoke0 ??= new SetupWithInParameter<int, float>();
				_invoke0.SetupParameter(result.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItIn<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "in");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItIn<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "in");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public float Invoke(in int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "in");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, result);
				return _mock._invoke0?.Execute(in result, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionIn<int>, float, System.FuncIn<int, float>> SetupInvoke(in ItIn<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(result);

			public void VerifyInvoke(in ItIn<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times);

			public void VerifyInvoke(in ItIn<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItIn<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<int>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

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
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItRef<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(ref int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, result);
				_mock._invoke0?.Invoke(ref result);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRef<int>> SetupInvoke(in ItRef<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(result);

			public void VerifyInvoke(in ItRef<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times);

			public void VerifyInvoke(in ItRef<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItRef<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<int>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReturn()
	{
		const string method = "float Invoke(ref int result);";

		const string methods =
			"""
			// Invoke
			private SetupWithRefParameter<int, float>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithRefParameter<int, float> SetupInvoke(in ItRef<int> result)
			{
				_invoke0 ??= new SetupWithRefParameter<int, float>();
				_invoke0.SetupParameter(result.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItRef<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItRef<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public float Invoke(ref int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, result);
				return _mock._invoke0?.Execute(ref result, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRef<int>, float, System.FuncRef<int, float>> SetupInvoke(in ItRef<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(result);

			public void VerifyInvoke(in ItRef<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times);

			public void VerifyInvoke(in ItRef<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItRef<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<int>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

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
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref readonly");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItRefReadOnly<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref readonly");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(ref readonly int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref readonly");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, result);
				_mock._invoke0?.Invoke(in result);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRefReadOnly<int>> SetupInvoke(in ItRefReadOnly<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(result);

			public void VerifyInvoke(in ItRefReadOnly<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times);

			public void VerifyInvoke(in ItRefReadOnly<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItRefReadOnly<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<int>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceWithRefReadonlyReturn()
	{
		const string method = "float Invoke(ref readonly int result);";

		const string methods =
			"""
			// Invoke
			private SetupWithRefReadOnlyParameter<int, float>? _invoke0;
			private Invocation<int>? _invoke0Invocation;

			public SetupWithRefReadOnlyParameter<int, float> SetupInvoke(in ItRefReadOnly<int> result)
			{
				_invoke0 ??= new SetupWithRefReadOnlyParameter<int, float>();
				_invoke0.SetupParameter(result.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItRefReadOnly<int> result, in Times times)
			{
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref readonly");
				_invoke0Invocation.Verify(result.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItRefReadOnly<int> result, long index)
			{
				_invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref readonly");
				return _invoke0Invocation.Verify(result.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public float Invoke(ref readonly int result)
			{
				_mock._invoke0Invocation ??= new Invocation<int>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref readonly");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, result);
				return _mock._invoke0?.Execute(in result, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRefReadOnly<int>, float, System.FuncRefReadOnly<int, float>> SetupInvoke(in ItRefReadOnly<int> result = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(result);

			public void VerifyInvoke(in ItRefReadOnly<int> result, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times);

			public void VerifyInvoke(in ItRefReadOnly<int> result, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(result, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItRefReadOnly<int> result)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(result, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<int>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric()
	{
		const string method = "void Invoke<TInvoke>(TInvoke param);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithParameter<TInvoke> SetupInvoke<TInvoke>(in It<TInvoke> param)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithParameter<TInvoke>)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithParameter<TInvoke>());
				invoke0.SetupParameter(param.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in It<TInvoke> param, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})"));
				invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(in It<TInvoke> param, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})"));
				return invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<TInvoke>(TInvoke param)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})"));
				invoke0Invocation.Register(_mock._invocationIndex, param);
				((SetupWithParameter<TInvoke>?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Invoke(param);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action<TInvoke>> SetupInvoke<TInvoke>(in It<TInvoke> param = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<TInvoke>(param);

			public void VerifyInvoke<TInvoke>(in It<TInvoke> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(param, times);

			public void VerifyInvoke<TInvoke>(in It<TInvoke> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(param, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>(in It<TInvoke> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<TInvoke>(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<TInvoke>> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericReturn()
	{
		const string method = "float Invoke<TInvoke>(TInvoke param);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithParameter<TInvoke, float> SetupInvoke<TInvoke>(in It<TInvoke> param)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithParameter<TInvoke, float>)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithParameter<TInvoke, float>());
				invoke0.SetupParameter(param.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in It<TInvoke> param, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})"));
				invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(in It<TInvoke> param, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})"));
				return invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public float Invoke<TInvoke>(TInvoke param)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})"));
				invoke0Invocation.Register(_mock._invocationIndex, param);
				return ((SetupWithParameter<TInvoke, float>?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Execute(param, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action<TInvoke>, float, System.Func<TInvoke, float>> SetupInvoke<TInvoke>(in It<TInvoke> param = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<TInvoke>(param);

			public void VerifyInvoke<TInvoke>(in It<TInvoke> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(param, times);

			public void VerifyInvoke<TInvoke>(in It<TInvoke> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(param, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>(in It<TInvoke> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<TInvoke>(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<TInvoke>> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithOut()
	{
		const string method = "void Invoke<TInvoke>(out TInvoke value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithOutParameter<TInvoke> SetupInvoke<TInvoke>(in ItOut<TInvoke> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithOutParameter<TInvoke>)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithOutParameter<TInvoke>());
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in ItOut<TInvoke> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "out"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(in ItOut<TInvoke> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "out"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<TInvoke>(out TInvoke value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "out"));
				invoke0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invoke0?.TryGetValue(typeof(TInvoke), out var setup) == true)
				{
					((SetupWithOutParameter<TInvoke>)setup).Invoke(out value!);
				}
				else
				{
					value = default!;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionOut<TInvoke>> SetupInvoke<TInvoke>(in ItOut<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<TInvoke>(value);

			public void VerifyInvoke<TInvoke>(in ItOut<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times);

			public void VerifyInvoke<TInvoke>(in ItOut<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>(in ItOut<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<TInvoke>> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "out"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithOutReturn()
	{
		const string method = "decimal Invoke<TInvoke>(out TInvoke value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithOutParameter<TInvoke, decimal> SetupInvoke<TInvoke>(in ItOut<TInvoke> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithOutParameter<TInvoke, decimal>)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithOutParameter<TInvoke, decimal>());
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in ItOut<TInvoke> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "out"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(in ItOut<TInvoke> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "out"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public decimal Invoke<TInvoke>(out TInvoke value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "out"));
				invoke0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invoke0?.TryGetValue(typeof(TInvoke), out var setup) == true)
				{
					return ((SetupWithOutParameter<TInvoke, decimal>)setup).Execute(out value!, out var returnValue) == true ? returnValue! : default!;
				}
				else
				{
					value = default!;
					return default!;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionOut<TInvoke>, decimal, System.FuncOut<TInvoke, decimal>> SetupInvoke<TInvoke>(in ItOut<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<TInvoke>(value);

			public void VerifyInvoke<TInvoke>(in ItOut<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times);

			public void VerifyInvoke<TInvoke>(in ItOut<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>(in ItOut<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<TInvoke>> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "out"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithIn()
	{
		const string method = "void Invoke<TInvoke>(in TInvoke value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithInParameter<TInvoke> SetupInvoke<TInvoke>(in ItIn<TInvoke> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithInParameter<TInvoke>)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithInParameter<TInvoke>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in ItIn<TInvoke> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "in"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(in ItIn<TInvoke> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "in"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<TInvoke>(in TInvoke value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "in"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithInParameter<TInvoke>?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Invoke(in value);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionIn<TInvoke>> SetupInvoke<TInvoke>(in ItIn<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<TInvoke>(value);

			public void VerifyInvoke<TInvoke>(in ItIn<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times);

			public void VerifyInvoke<TInvoke>(in ItIn<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>(in ItIn<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<TInvoke>> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "in"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithInReturn()
	{
		const string method = "float Invoke<TInvoke>(in TInvoke value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithInParameter<TInvoke, float> SetupInvoke<TInvoke>(in ItIn<TInvoke> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithInParameter<TInvoke, float>)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithInParameter<TInvoke, float>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in ItIn<TInvoke> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "in"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(in ItIn<TInvoke> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "in"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public float Invoke<TInvoke>(in TInvoke value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "in"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				return ((SetupWithInParameter<TInvoke, float>?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Execute(in value, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionIn<TInvoke>, float, System.FuncIn<TInvoke, float>> SetupInvoke<TInvoke>(in ItIn<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<TInvoke>(value);

			public void VerifyInvoke<TInvoke>(in ItIn<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times);

			public void VerifyInvoke<TInvoke>(in ItIn<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>(in ItIn<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<TInvoke>> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "in"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRef()
	{
		const string method = "void Invoke<TInvoke>(ref TInvoke value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithRefParameter<TInvoke> SetupInvoke<TInvoke>(in ItRef<TInvoke> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithRefParameter<TInvoke>)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithRefParameter<TInvoke>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in ItRef<TInvoke> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(in ItRef<TInvoke> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<TInvoke>(ref TInvoke value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefParameter<TInvoke>?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Invoke(ref value);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRef<TInvoke>> SetupInvoke<TInvoke>(in ItRef<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<TInvoke>(value);

			public void VerifyInvoke<TInvoke>(in ItRef<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times);

			public void VerifyInvoke<TInvoke>(in ItRef<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>(in ItRef<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<TInvoke>> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefReturn()
	{
		const string method = "float Invoke<TInvoke>(ref TInvoke value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithRefParameter<TInvoke, float> SetupInvoke<TInvoke>(in ItRef<TInvoke> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithRefParameter<TInvoke, float>)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithRefParameter<TInvoke, float>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in ItRef<TInvoke> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(in ItRef<TInvoke> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public float Invoke<TInvoke>(ref TInvoke value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				return ((SetupWithRefParameter<TInvoke, float>?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Execute(ref value, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRef<TInvoke>, float, System.FuncRef<TInvoke, float>> SetupInvoke<TInvoke>(in ItRef<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<TInvoke>(value);

			public void VerifyInvoke<TInvoke>(in ItRef<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times);

			public void VerifyInvoke<TInvoke>(in ItRef<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>(in ItRef<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<TInvoke>> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefReadOnly()
	{
		const string method = "void Invoke<TInvoke>(ref readonly TInvoke value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithRefReadOnlyParameter<TInvoke> SetupInvoke<TInvoke>(in ItRefReadOnly<TInvoke> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithRefReadOnlyParameter<TInvoke>)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithRefReadOnlyParameter<TInvoke>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in ItRefReadOnly<TInvoke> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref readonly"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(in ItRefReadOnly<TInvoke> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref readonly"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<TInvoke>(ref readonly TInvoke value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref readonly"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefReadOnlyParameter<TInvoke>?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Invoke(in value);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRefReadOnly<TInvoke>> SetupInvoke<TInvoke>(in ItRefReadOnly<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<TInvoke>(value);

			public void VerifyInvoke<TInvoke>(in ItRefReadOnly<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times);

			public void VerifyInvoke<TInvoke>(in ItRefReadOnly<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>(in ItRefReadOnly<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<TInvoke>> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref readonly"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGenericWithRefReadOnlyReturn()
	{
		const string method = "float Invoke<TInvoke>(ref readonly TInvoke value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>? _invoke0;
			private InvocationDictionary? _invoke0Invocation;

			public SetupWithRefReadOnlyParameter<TInvoke, float> SetupInvoke<TInvoke>(in ItRefReadOnly<TInvoke> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<System.Type, object>();
				var invoke0 = (SetupWithRefReadOnlyParameter<TInvoke, float>)_invoke0.GetOrAdd(typeof(TInvoke), static _ => new SetupWithRefReadOnlyParameter<TInvoke, float>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<TInvoke>(in ItRefReadOnly<TInvoke> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref readonly"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<TInvoke>(in ItRefReadOnly<TInvoke> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref readonly"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public float Invoke<TInvoke>(ref readonly TInvoke value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref readonly"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				return ((SetupWithRefReadOnlyParameter<TInvoke, float>?)_mock._invoke0?.ValueOrDefault(typeof(TInvoke)))?.Execute(in value, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRefReadOnly<TInvoke>, float, System.FuncRefReadOnly<TInvoke, float>> SetupInvoke<TInvoke>(in ItRefReadOnly<TInvoke> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<TInvoke>(value);

			public void VerifyInvoke<TInvoke>(in ItRefReadOnly<TInvoke> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times);

			public void VerifyInvoke<TInvoke>(in ItRefReadOnly<TInvoke> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<TInvoke>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<TInvoke>(in ItRefReadOnly<TInvoke> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<TInvoke>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<TInvoke>> Invoke<TInvoke>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary();
				var invoke0Invocation = (Invocation<TInvoke>)_mock._invoke0Invocation.GetOrAdd(typeof(TInvoke), static key => new Invocation<TInvoke>($"IInterface<{typeof(T).Name}>.Invoke<{key.Name}>({{0}})", prefix: "ref readonly"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1()
	{
		const string method = "void Invoke(T param);";

		const string methods =
			"""
			// Invoke
			private SetupWithParameter<T>? _invoke0;
			private Invocation<T>? _invoke0Invocation;

			public SetupWithParameter<T> SetupInvoke(in It<T> param)
			{
				_invoke0 ??= new SetupWithParameter<T>();
				_invoke0.SetupParameter(param.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in It<T> param, in Times times)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
				_invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in It<T> param, long index)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
				return _invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(T param)
			{
				_mock._invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, param);
				_mock._invoke0?.Invoke(param);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action<T>> SetupInvoke(in It<T> param = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(param);

			public void VerifyInvoke(in It<T> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(param, times);

			public void VerifyInvoke(in It<T> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(param, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in It<T> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<T>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1Return()
	{
		const string method = "T Invoke(T param);";

		const string methods =
			"""
			// Invoke
			private SetupWithParameter<T, T>? _invoke0;
			private Invocation<T>? _invoke0Invocation;

			public SetupWithParameter<T, T> SetupInvoke(in It<T> param)
			{
				_invoke0 ??= new SetupWithParameter<T, T>();
				_invoke0.SetupParameter(param.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in It<T> param, in Times times)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
				_invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in It<T> param, long index)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
				return _invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T Invoke(T param)
			{
				_mock._invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, param);
				return _mock._invoke0?.Execute(param, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action<T>, T, System.Func<T, T>> SetupInvoke(in It<T> param = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(param);

			public void VerifyInvoke(in It<T> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(param, times);

			public void VerifyInvoke(in It<T> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(param, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in It<T> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<T>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithOut()
	{
		const string method = "void Invoke(out T value);";

		const string methods =
			"""
			// Invoke
			private SetupWithOutParameter<T>? _invoke0;
			private Invocation<T>? _invoke0Invocation;

			public SetupWithOutParameter<T> SetupInvoke(in ItOut<T> value)
			{
				_invoke0 ??= new SetupWithOutParameter<T>();
				return _invoke0;
			}

			public void VerifyInvoke(in ItOut<T> value, in Times times)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "out");
				_invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<T> value, long index)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "out");
				return _invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(out T value)
			{
				_mock._invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "out");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invoke0 is not null)
				{
					_mock._invoke0.Invoke(out value!);
				}
				else
				{
					value = default!;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionOut<T>> SetupInvoke(in ItOut<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(value);

			public void VerifyInvoke(in ItOut<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times);

			public void VerifyInvoke(in ItOut<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItOut<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<T>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithOutReturn()
	{
		const string method = "T Invoke(out T value);";

		const string methods =
			"""
			// Invoke
			private SetupWithOutParameter<T, T>? _invoke0;
			private Invocation<T>? _invoke0Invocation;

			public SetupWithOutParameter<T, T> SetupInvoke(in ItOut<T> value)
			{
				_invoke0 ??= new SetupWithOutParameter<T, T>();
				return _invoke0;
			}

			public void VerifyInvoke(in ItOut<T> value, in Times times)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "out");
				_invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItOut<T> value, long index)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "out");
				return _invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T Invoke(out T value)
			{
				_mock._invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "out");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invoke0 is not null)
				{
					return _mock._invoke0.Execute(out value!, out var returnValue) == true ? returnValue! : default!;
				}
				else
				{
					value = default!;
					return default!;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionOut<T>, T, System.FuncOut<T, T>> SetupInvoke(in ItOut<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(value);

			public void VerifyInvoke(in ItOut<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times);

			public void VerifyInvoke(in ItOut<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItOut<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<T>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithIn()
	{
		const string method = "void Invoke(in T value);";

		const string methods =
			"""
			// Invoke
			private SetupWithInParameter<T>? _invoke0;
			private Invocation<T>? _invoke0Invocation;

			public SetupWithInParameter<T> SetupInvoke(in ItIn<T> value)
			{
				_invoke0 ??= new SetupWithInParameter<T>();
				_invoke0.SetupParameter(value.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItIn<T> value, in Times times)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "in");
				_invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItIn<T> value, long index)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "in");
				return _invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(in T value)
			{
				_mock._invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "in");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, value);
				_mock._invoke0?.Invoke(in value);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionIn<T>> SetupInvoke(in ItIn<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(value);

			public void VerifyInvoke(in ItIn<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times);

			public void VerifyInvoke(in ItIn<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItIn<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<T>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithInReturn()
	{
		const string method = "T Invoke(in T value);";

		const string methods =
			"""
			// Invoke
			private SetupWithInParameter<T, T>? _invoke0;
			private Invocation<T>? _invoke0Invocation;

			public SetupWithInParameter<T, T> SetupInvoke(in ItIn<T> value)
			{
				_invoke0 ??= new SetupWithInParameter<T, T>();
				_invoke0.SetupParameter(value.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItIn<T> value, in Times times)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "in");
				_invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItIn<T> value, long index)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "in");
				return _invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T Invoke(in T value)
			{
				_mock._invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "in");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, value);
				return _mock._invoke0?.Execute(in value, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionIn<T>, T, System.FuncIn<T, T>> SetupInvoke(in ItIn<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(value);

			public void VerifyInvoke(in ItIn<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times);

			public void VerifyInvoke(in ItIn<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItIn<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<T>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithRef()
	{
		const string method = "void Invoke(ref T value);";

		const string methods =
			"""
			// Invoke
			private SetupWithRefParameter<T>? _invoke0;
			private Invocation<T>? _invoke0Invocation;

			public SetupWithRefParameter<T> SetupInvoke(in ItRef<T> value)
			{
				_invoke0 ??= new SetupWithRefParameter<T>();
				_invoke0.SetupParameter(value.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItRef<T> value, in Times times)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref");
				_invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItRef<T> value, long index)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref");
				return _invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(ref T value)
			{
				_mock._invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, value);
				_mock._invoke0?.Invoke(ref value);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRef<T>> SetupInvoke(in ItRef<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(value);

			public void VerifyInvoke(in ItRef<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times);

			public void VerifyInvoke(in ItRef<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItRef<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<T>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithRefReturn()
	{
		const string method = "T Invoke(ref T value);";

		const string methods =
			"""
			// Invoke
			private SetupWithRefParameter<T, T>? _invoke0;
			private Invocation<T>? _invoke0Invocation;

			public SetupWithRefParameter<T, T> SetupInvoke(in ItRef<T> value)
			{
				_invoke0 ??= new SetupWithRefParameter<T, T>();
				_invoke0.SetupParameter(value.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItRef<T> value, in Times times)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref");
				_invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItRef<T> value, long index)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref");
				return _invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T Invoke(ref T value)
			{
				_mock._invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, value);
				return _mock._invoke0?.Execute(ref value, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRef<T>, T, System.FuncRef<T, T>> SetupInvoke(in ItRef<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(value);

			public void VerifyInvoke(in ItRef<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times);

			public void VerifyInvoke(in ItRef<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItRef<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<T>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithRefReadOnly()
	{
		const string method = "void Invoke(ref readonly T value);";

		const string methods =
			"""
			// Invoke
			private SetupWithRefReadOnlyParameter<T>? _invoke0;
			private Invocation<T>? _invoke0Invocation;

			public SetupWithRefReadOnlyParameter<T> SetupInvoke(in ItRefReadOnly<T> value)
			{
				_invoke0 ??= new SetupWithRefReadOnlyParameter<T>();
				_invoke0.SetupParameter(value.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItRefReadOnly<T> value, in Times times)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref readonly");
				_invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItRefReadOnly<T> value, long index)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref readonly");
				return _invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke(ref readonly T value)
			{
				_mock._invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref readonly");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, value);
				_mock._invoke0?.Invoke(in value);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRefReadOnly<T>> SetupInvoke(in ItRefReadOnly<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(value);

			public void VerifyInvoke(in ItRefReadOnly<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times);

			public void VerifyInvoke(in ItRefReadOnly<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItRefReadOnly<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<T>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric1WithRefReadOnlyReturn()
	{
		const string method = "T Invoke(ref readonly T value);";

		const string methods =
			"""
			// Invoke
			private SetupWithRefReadOnlyParameter<T, T>? _invoke0;
			private Invocation<T>? _invoke0Invocation;

			public SetupWithRefReadOnlyParameter<T, T> SetupInvoke(in ItRefReadOnly<T> value)
			{
				_invoke0 ??= new SetupWithRefReadOnlyParameter<T, T>();
				_invoke0.SetupParameter(value.ValueSetup);
				return _invoke0;
			}

			public void VerifyInvoke(in ItRefReadOnly<T> value, in Times times)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref readonly");
				_invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in ItRefReadOnly<T> value, long index)
			{
				_invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref readonly");
				return _invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T Invoke(ref readonly T value)
			{
				_mock._invoke0Invocation ??= new Invocation<T>($"IInterface<{typeof(T).Name}>.Invoke({{0}})", prefix: "ref readonly");
				_mock._invoke0Invocation.Register(_mock._invocationIndex, value);
				return _mock._invoke0?.Execute(in value, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRefReadOnly<T>, T, System.FuncRefReadOnly<T, T>> SetupInvoke(in ItRefReadOnly<T> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke(value);

			public void VerifyInvoke(in ItRefReadOnly<T> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times);

			public void VerifyInvoke(in ItRefReadOnly<T> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke(in ItRefReadOnly<T> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer = "public System.Collections.Generic.IEnumerable<IInvocation<T>> Invoke => _mock._invoke0Invocation?.GetInvocationsWithArguments() ?? [];";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2()
	{
		const string method = "void Invoke<T1, T2>(T1 param);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithParameter<T1> SetupInvoke<T1, T2>(in It<T1> param)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithParameter<T1>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithParameter<T1>());
				invoke0.SetupParameter(param.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in It<T1> param, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in It<T1> param, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				return invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T1, T2>(T1 param)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				invoke0Invocation.Register(_mock._invocationIndex, param);
				((SetupWithParameter<T1>?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke(param);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action<T1>> SetupInvoke<T1, T2>(in It<T1> param = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<T1, T2>(param);

			public void VerifyInvoke<T1, T2>(in It<T1> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(param, times);

			public void VerifyInvoke<T1, T2>(in It<T1> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(param, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T1, T2>(in It<T1> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<T1, T2>(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<T1>> Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2Return()
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
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in It<T1> param, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				return invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T2 Invoke<T1, T2>(T1 param)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				invoke0Invocation.Register(_mock._invocationIndex, param);
				return ((SetupWithParameter<T1, T2>?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(param, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.Action<T1>, T2, System.Func<T1, T2>> SetupInvoke<T1, T2>(in It<T1> param = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<T1, T2>(param);

			public void VerifyInvoke<T1, T2>(in It<T1> param, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(param, times);

			public void VerifyInvoke<T1, T2>(in It<T1> param, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(param, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T1, T2>(in It<T1> param)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<T1, T2>(param, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<T1>> Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithOut()
	{
		const string method = "void Invoke<T1, T2>(out T1 value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithOutParameter<T1> SetupInvoke<T1, T2>(in ItOut<T1> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithOutParameter<T1>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithOutParameter<T1>());
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in ItOut<T1> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in ItOut<T1> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T1, T2>(out T1 value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				invoke0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invoke0?.TryGetValue((typeof(T1), typeof(T2)), out var setup) == true)
				{
					((SetupWithOutParameter<T1>)setup).Invoke(out value!);
				}
				else
				{
					value = default!;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionOut<T1>> SetupInvoke<T1, T2>(in ItOut<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<T1, T2>(value);

			public void VerifyInvoke<T1, T2>(in ItOut<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times);

			public void VerifyInvoke<T1, T2>(in ItOut<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T1, T2>(in ItOut<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<T1>> Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithOutReturn()
	{
		const string method = "T2 Invoke<T1, T2>(out T1 value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithOutParameter<T1, T2> SetupInvoke<T1, T2>(in ItOut<T1> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithOutParameter<T1, T2>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithOutParameter<T1, T2>());
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in ItOut<T1> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in ItOut<T1> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T2 Invoke<T1, T2>(out T1 value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				invoke0Invocation.Register(_mock._invocationIndex, default!);
				if (_mock._invoke0?.TryGetValue((typeof(T1), typeof(T2)), out var setup) == true)
				{
					return ((SetupWithOutParameter<T1, T2>)setup).Execute(out value!, out var returnValue) == true ? returnValue! : default!;
				}
				else
				{
					value = default!;
					return default!;
				}
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionOut<T1>, T2, System.FuncOut<T1, T2>> SetupInvoke<T1, T2>(in ItOut<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<T1, T2>(value);

			public void VerifyInvoke<T1, T2>(in ItOut<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times);

			public void VerifyInvoke<T1, T2>(in ItOut<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T1, T2>(in ItOut<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<T1>> Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "out"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithIn()
	{
		const string method = "void Invoke<T1, T2>(in T1 value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithInParameter<T1> SetupInvoke<T1, T2>(in ItIn<T1> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithInParameter<T1>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithInParameter<T1>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in ItIn<T1> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in ItIn<T1> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T1, T2>(in T1 value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithInParameter<T1>?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke(in value);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionIn<T1>> SetupInvoke<T1, T2>(in ItIn<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<T1, T2>(value);

			public void VerifyInvoke<T1, T2>(in ItIn<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times);

			public void VerifyInvoke<T1, T2>(in ItIn<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T1, T2>(in ItIn<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<T1>> Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithInReturn()
	{
		const string method = "T2 Invoke<T1, T2>(in T1 value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithInParameter<T1, T2> SetupInvoke<T1, T2>(in ItIn<T1> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithInParameter<T1, T2>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithInParameter<T1, T2>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in ItIn<T1> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in ItIn<T1> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T2 Invoke<T1, T2>(in T1 value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				return ((SetupWithInParameter<T1, T2>?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(in value, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionIn<T1>, T2, System.FuncIn<T1, T2>> SetupInvoke<T1, T2>(in ItIn<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<T1, T2>(value);

			public void VerifyInvoke<T1, T2>(in ItIn<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times);

			public void VerifyInvoke<T1, T2>(in ItIn<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T1, T2>(in ItIn<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<T1>> Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "in"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithRef()
	{
		const string method = "void Invoke<T1, T2>(ref T1 value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithRefParameter<T1> SetupInvoke<T1, T2>(in ItRef<T1> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithRefParameter<T1>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefParameter<T1>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in ItRef<T1> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in ItRef<T1> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T1, T2>(ref T1 value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefParameter<T1>?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke(ref value);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRef<T1>> SetupInvoke<T1, T2>(in ItRef<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<T1, T2>(value);

			public void VerifyInvoke<T1, T2>(in ItRef<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times);

			public void VerifyInvoke<T1, T2>(in ItRef<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T1, T2>(in ItRef<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<T1>> Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithRefReturn()
	{
		const string method = "T2 Invoke<T1, T2>(ref T1 value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithRefParameter<T1, T2> SetupInvoke<T1, T2>(in ItRef<T1> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithRefParameter<T1, T2>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefParameter<T1, T2>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in ItRef<T1> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in ItRef<T1> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T2 Invoke<T1, T2>(ref T1 value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				return ((SetupWithRefParameter<T1, T2>?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(ref value, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRef<T1>, T2, System.FuncRef<T1, T2>> SetupInvoke<T1, T2>(in ItRef<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<T1, T2>(value);

			public void VerifyInvoke<T1, T2>(in ItRef<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times);

			public void VerifyInvoke<T1, T2>(in ItRef<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T1, T2>(in ItRef<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<T1>> Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithRefReadOnly()
	{
		const string method = "void Invoke<T1, T2>(ref readonly T1 value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithRefReadOnlyParameter<T1> SetupInvoke<T1, T2>(in ItRefReadOnly<T1> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithRefReadOnlyParameter<T1>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefReadOnlyParameter<T1>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in ItRefReadOnly<T1> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in ItRefReadOnly<T1> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T1, T2>(ref readonly T1 value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				((SetupWithRefReadOnlyParameter<T1>?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke(in value);
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRefReadOnly<T1>> SetupInvoke<T1, T2>(in ItRefReadOnly<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<T1, T2>(value);

			public void VerifyInvoke<T1, T2>(in ItRefReadOnly<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times);

			public void VerifyInvoke<T1, T2>(in ItRefReadOnly<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T1, T2>(in ItRefReadOnly<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<T1>> Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceGeneric2WithRefReadOnlyReturn()
	{
		const string method = "T2 Invoke<T1, T2>(ref readonly T1 value);";

		const string methods =
			"""
			// Invoke
			private System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>? _invoke0;
			private InvocationDictionary<(System.Type, System.Type)>? _invoke0Invocation;

			public SetupWithRefReadOnlyParameter<T1, T2> SetupInvoke<T1, T2>(in ItRefReadOnly<T1> value)
			{
				_invoke0 ??= new System.Collections.Concurrent.ConcurrentDictionary<(System.Type, System.Type), object>();
				var invoke0 = (SetupWithRefReadOnlyParameter<T1, T2>)_invoke0.GetOrAdd((typeof(T1), typeof(T2)), static _ => new SetupWithRefReadOnlyParameter<T1, T2>());
				invoke0.SetupParameter(value.ValueSetup);
				return invoke0;
			}

			public void VerifyInvoke<T1, T2>(in ItRefReadOnly<T1> value, in Times times)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				invoke0Invocation.Verify(value.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke<T1, T2>(in ItRefReadOnly<T1> value, long index)
			{
				_invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				return invoke0Invocation.Verify(value.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public T2 Invoke<T1, T2>(ref readonly T1 value)
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				invoke0Invocation.Register(_mock._invocationIndex, value);
				return ((SetupWithRefReadOnlyParameter<T1, T2>?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Execute(in value, out var returnValue) == true ? returnValue! : default!;
			}
			""";

		const string verifyNoOtherCalls = "_invoke0Invocation?.VerifyNoOtherCalls(_invocationProviders);";
		const string invocations = "yield return _invoke0Invocation;";

		const string extensions =
			"""
			// Invoke
			public ISetup<System.ActionRefReadOnly<T1>, T2, System.FuncRefReadOnly<T1, T2>> SetupInvoke<T1, T2>(in ItRefReadOnly<T1> value = default) =>
				((InterfaceMock<T>)@this).SetupInvoke<T1, T2>(value);

			public void VerifyInvoke<T1, T2>(in ItRefReadOnly<T1> value, in Times times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times);

			public void VerifyInvoke<T1, T2>(in ItRefReadOnly<T1> value, System.Func<Times> times) =>
				((InterfaceMock<T>)@this).VerifyInvoke<T1, T2>(value, times());
			""";

		const string sequenceExtensions =
			"""
			// Invoke
			public void Invoke<T1, T2>(in ItRefReadOnly<T1> value)
			{
				var nextIndex = ((InterfaceMock<T>)@this.Mock).VerifyInvoke<T1, T2>(value, @this.VerifyIndex);
				@this.VerifyIndex.Set(nextIndex);
			}
			""";

		const string invocationContainer =
			"""
			public System.Collections.Generic.IEnumerable<IInvocation<T1>> Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation<T1>)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation<T1>($"IInterface<{typeof(T).Name}>.Invoke<{key.Item1.Name}, {key.Item2.Name}>({{0}})", prefix: "ref readonly"));
				return invoke0Invocation.GetInvocationsWithArguments() ?? [];
			}
			""";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
