namespace MyNihongo.Mock.Tests.PropertyTests;

public sealed class PropertiesShould : PropertyTestsBase
{
	[Fact]
	public async Task GenerateInterfacePropertyGet()
	{
		const string property = "int Property { get; }";

		const string methods =
			"""
			// Property
			private Setup<int>? _property0Get;
			private Invocation? _property0GetInvocation;

			public Setup<int> SetupGetProperty()
			{
				_property0Get ??= new Setup<int>();
				return _property0Get;
			}

			public void VerifyGetProperty(in Times times)
			{
				_property0GetInvocation ??= new Invocation("IInterface.Property.get");
				_property0GetInvocation.Verify(times, _invocationProviders);
			}

			public long VerifyGetProperty(long index)
			{
				_property0GetInvocation ??= new Invocation("IInterface.Property.get");
				return _property0GetInvocation.Verify(index, _invocationProviders);
			}
			""";

		const string proxy = $"public {property}";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfacePropertySet()
	{
		const string property = "int Property { set; }";

		const string methods =
			"""
			// Property
			private SetupWithParameter<int>? _property0Set;
			private Invocation<int>? _property0SetInvocation;

			public SetupWithParameter<int> SetupSetProperty(in It<int> value)
			{
				_property0Set ??= new SetupWithParameter<int>();
				_property0Set.SetupParameter(value);
				return _property0Set;
			}

			public void VerifySetProperty(in It<int> value, in Times times)
			{
				_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
				_property0SetInvocation.Verify(value, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
				return _property0SetInvocation.Verify(value, index, _invocationProviders);
			}
			""";

		const string proxy = "public int Property { get; set; }";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfacePropertyInit()
	{
		const string property = "int Property { init; }";

		const string methods =
			"""
			// Property
			private SetupWithParameter<int>? _property0Set;
			private Invocation<int>? _property0SetInvocation;

			public SetupWithParameter<int> SetupSetProperty(in It<int> value)
			{
				_property0Set ??= new SetupWithParameter<int>();
				_property0Set.SetupParameter(value);
				return _property0Set;
			}

			public void VerifySetProperty(in It<int> value, in Times times)
			{
				_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
				_property0SetInvocation.Verify(value, times, _invocationProviders);
			}

			public long VerifySetProperty(in It<int> value, long index)
			{
				_property0SetInvocation ??= new Invocation<int>("IInterface.Property.set = {0}");
				return _property0SetInvocation.Verify(value, index, _invocationProviders);
			}
			""";

		const string proxy = "public int Property { get; init; }";

		var testCode = CreateInterfaceTestCode(property);
		var generatedSources = CreateInterfaceGeneratedSources(methods, proxy);

		var ctx = CreateFixture(testCode, generatedSources);
		await ctx.RunAsync();
	}

	[Fact]
	public async Task GenerateInterfacePropertyGetSet()
	{
		const string property = "";
	}

	[Fact]
	public async Task GenerateInterfacePropertyGetInit()
	{
		const string property = "";
	}
}
