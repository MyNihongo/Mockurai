namespace MyNihongo.Mock.Tests.MethodTests;

public abstract class MethodTestsBase : TestsNonGenericBase
{
	private const string CustomCode = "";

	protected static string CreateInterfaceTestCode(string @event)
	{
		return CreateInterfaceTestCode(@event, CustomCode);
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
