namespace MyNihongo.Mockurai.Tests.PropertyTests;

public class PropertyGenericTestsBase : TestsGenericBase
{
	private const string CustomCode = "public sealed record Record<T>(int age, T Name);";

	protected static string CreateInterfaceTestCode(string property)
	{
		return CreateInterfaceTestCode(property, CustomCode);
	}

	protected static GeneratedSources CreateInterfaceGeneratedSources(string methods, string proxy, string verifyNoOtherCalls, string invocations)
	{
		var testsBase = GetInterfaceTestsBase();
		var mock = GetInterfaceMock(methods, proxy, verifyNoOtherCalls, invocations, string.Empty, string.Empty);

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

	protected static GeneratedSources CreateClassGeneratedSources(string methods, string proxy, string verifyNoOtherCalls, string invocations)
	{
		var testsBase = GetClassTestsBase();
		var mock = GetClassMock(methods, proxy, verifyNoOtherCalls, invocations, string.Empty, string.Empty);

		return
		[
			testsBase,
			mock,
		];
	}
}
