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

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

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

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
