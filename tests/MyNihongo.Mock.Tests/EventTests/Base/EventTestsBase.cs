namespace MyNihongo.Mock.Tests.EventTests;

public abstract class EventTestsBase : TestsNonGenericBase
{
	private const string CustomCode = "public delegate void SampleHandler1(object sender, int value);";

	protected static string CreateInterfaceTestCode(string @event)
	{
		return CreateInterfaceTestCode(@event, CustomCode);
	}

	protected static GeneratedSources CreateInterfaceGeneratedSources(string methods, string proxy, string verifyNoOtherCalls, string invocations)
	{
		var testsBase = GetInterfaceTestsBase();
		var mock = GetInterfaceMock(methods, proxy, verifyNoOtherCalls, invocations);

		return
		[
			testsBase,
			mock,
		];
	}

	protected static string CreateClassTestCode(string @event, bool isAbstract = false)
	{
		return CreateClassTestCode(@event, CustomCode, isAbstract);
	}

	protected static GeneratedSources CreateClassGeneratedSources(string methods, string proxy, string verifyNoOtherCalls, string invocations, string extensions)
	{
		var testsBase = GetClassTestsBase();
		var mock = GetClassMock(methods, proxy, verifyNoOtherCalls, invocations, extensions);

		return
		[
			testsBase,
			mock,
		];
	}
}
