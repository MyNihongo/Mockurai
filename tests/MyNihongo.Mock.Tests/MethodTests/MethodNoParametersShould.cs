namespace MyNihongo.Mock.Tests.MethodTests;

public sealed class MethodNoParametersShould : MethodTestsBase
{
	[Fact]
	public async Task GenerateInterface()
	{
		const string method = "void Invoke();";

		const string methods =
			"""
			// Invoke
			private Setup? _invoke0;
			private Invocation? _invoke0Invocation;

			public Setup SetupInvoke()
			{
				_invoke0 ??= new Setup();
				return _invoke0;
			}

			public void VerifyInvoke(in Times times)
			{
				_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				_invoke0Invocation.Verify(times, _invocationProviders);
			}

			public long VerifyInvoke(long index)
			{
				_invoke0Invocation ??= new Invocation("IInterface.Invoke()");
				return _invoke0Invocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = "public void Invoke() {}";

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

		const string proxy = "public void Invoke(out int result) {}";

		var testCode = CreateInterfaceTestCode(method);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}
}
