namespace MyNihongo.Mockurai.Tests.PropertyTests;

public abstract class PropertyTestsBase : TestsNonGenericBase
{
	private const string CustomCode = "public sealed record Record(int age, string Name);";

	protected static string CreateInterfaceTestCode(string property)
	{
		return CreateInterfaceTestCode(property, CustomCode);
	}

	protected static GeneratedSources CreateInterfaceGeneratedSources(string methods, string proxy, string verifyNoOtherCalls, string invocations, string extensions, string extensionsSequence)
	{
		var testsBase = GetInterfaceTestsBase();
		var mock = GetInterfaceMock(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		return
		[
			testsBase,
			mock,
		];
	}

	protected static string CreateClassTestCode(string property, bool isAbstract = false)
	{
		return CreateClassTestCode(property, CustomCode, isAbstract);
	}

	protected static GeneratedSources CreateClassGeneratedSources(string methods, string proxy, string verifyNoOtherCalls, string invocations, string extensions, string extensionsSequence)
	{
		var testsBase = GetClassTestsBase();
		var mock = GetClassMock(methods, proxy, verifyNoOtherCalls, invocations, extensions, extensionsSequence);

		return
		[
			testsBase,
			mock,
		];
	}
}
