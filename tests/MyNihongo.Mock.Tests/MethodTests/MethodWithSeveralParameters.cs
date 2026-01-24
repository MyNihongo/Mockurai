namespace MyNihongo.Mock.Tests.MethodTests;

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

		const string proxy = "public void Invoke(int param1, float param2) {}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfaceReturn()
	{
		const string method = "decimal Invoke(int param1, float param2);";

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

		const string proxy = "public void Invoke(int param1, float param2) {}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
