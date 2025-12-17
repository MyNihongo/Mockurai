namespace MyNihongo.Mock.Tests.PropertyTests;

public sealed class PropertiesShould : PropertyTestsBase
{
	[Fact]
	public async Task GenerateInterfacePropertyGet()
	{
		const string property = "int Property { get; }";

		const string methods =
			"""
			TODO
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
