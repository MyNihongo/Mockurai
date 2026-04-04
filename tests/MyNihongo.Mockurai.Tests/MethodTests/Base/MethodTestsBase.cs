namespace MyNihongo.Mockurai.Tests.MethodTests;

public abstract class MethodTestsBase : TestsNonGenericBase
{
	private const string CustomCode = "";

	protected static string CreateInterfaceTestCode(string method)
	{
		return CreateInterfaceTestCode(method, CustomCode);
	}

	protected static GeneratedSources CreateInterfaceGeneratedSources(string methods, string proxy, string verifyNoOtherCalls, string invocations, params ReadOnlySpan<GeneratedSource> generatedSources)
	{
		var testsBase = GetInterfaceTestsBase();
		var mock = GetInterfaceMock(methods, proxy, verifyNoOtherCalls, invocations, string.Empty, string.Empty);

		return
		[
			testsBase,
			mock,
			..generatedSources,
		];
	}

	protected static string CreateClassTestCode(string method, bool isAbstract = false)
	{
		return CreateClassTestCode(method, CustomCode, isAbstract);
	}

	protected static GeneratedSources CreateClassGeneratedSources(string methods, string proxy, string verifyNoOtherCalls, string invocations, string extensions, string sequenceExtensions)
	{
		var testsBase = GetClassTestsBase();
		var mock = GetClassMock(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

		return
		[
			testsBase,
			mock,
		];
	}
}
