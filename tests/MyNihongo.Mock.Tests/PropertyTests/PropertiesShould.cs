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
		const string property = "";
	}

	[Fact]
	public async Task GenerateInterfacePropertyInit()
	{
		const string property = "";
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
