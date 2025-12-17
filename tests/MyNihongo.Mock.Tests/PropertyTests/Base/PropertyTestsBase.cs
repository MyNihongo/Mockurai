namespace MyNihongo.Mock.Tests.PropertyTests;

public abstract class PropertyTestsBase : TestsNonGenericBase
{
	private const string CustomCode = "public sealed record Record(int age, string Name);";

	protected static string CreateInterfaceTestCode(string property)
	{
		return CreateInterfaceTestCode(property, CustomCode);
	}

	protected static GeneratedSources CreateInterfaceGeneratedSources(string methods, string proxy)
	{
		var testsBase = GetInterfaceTestsBase();
		var mock = GetInterfaceMock(methods, proxy);

		return
		[
			testsBase,
			mock,
		];
	}
}
