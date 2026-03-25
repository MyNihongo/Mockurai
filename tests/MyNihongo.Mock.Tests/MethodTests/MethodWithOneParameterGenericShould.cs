namespace MyNihongo.Mock.Tests.MethodTests;

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
				_invoke0Invocation ??= new Invocation<int>("IInterface<T>.Invoke({0})");
				_invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in It<int> param, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface<T>.Invoke({0})");
				return _invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke();
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
				_invoke0Invocation ??= new Invocation<int>("IInterface<T>.Invoke({0})");
				_invoke0Invocation.Verify(param.ValueSetup, times, _invocationProviders);
			}

			public long VerifyInvoke(in It<int> param, long index)
			{
				_invoke0Invocation ??= new Invocation<int>("IInterface<T>.Invoke({0})");
				return _invoke0Invocation.Verify(param.ValueSetup, index, _invocationProviders);
			}
			""";

		const string proxy =
			"""
			public void Invoke<T1, T2>()
			{
				_mock._invoke0Invocation ??= new InvocationDictionary<(System.Type, System.Type)>();
				var invoke0Invocation = (Invocation)_mock._invoke0Invocation.GetOrAdd((typeof(T1), typeof(T2)), static key => new Invocation($"IInterface.Invoke<{key.Item1.Name}, {key.Item2.Name}>()"));
				invoke0Invocation.Register(_mock._invocationIndex);
				((Setup?)_mock._invoke0?.ValueOrDefault((typeof(T1), typeof(T2))))?.Invoke();
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
