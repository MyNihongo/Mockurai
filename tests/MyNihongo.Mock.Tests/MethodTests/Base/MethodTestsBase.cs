namespace MyNihongo.Mock.Tests.MethodTests;

public abstract class MethodTestsBase : TestsNonGenericBase
{
	private const string CustomCode = "";

	protected static string CreateInterfaceTestCode(string method)
	{
		return CreateInterfaceTestCode(method, CustomCode);
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

	protected static string CreateClassTestCode(string method, bool isAbstract = false)
	{
		return CreateClassTestCode(method, CustomCode, isAbstract);
	}

	protected static GeneratedSources CreateClassGeneratedSources(string methods, string proxy)
	{
		var testsBase = GetClassTestsBase();
		var mock = GetClassMock(methods, proxy);

		return
		[
			testsBase,
			mock,
		];
	}
}
