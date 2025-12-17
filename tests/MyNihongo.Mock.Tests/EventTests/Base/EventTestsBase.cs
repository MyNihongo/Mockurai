namespace MyNihongo.Mock.Tests.EventTests;

public abstract class EventTestsBase : TestsNonGenericBase
{
	private const string CustomCode = "public delegate void SampleHandler1(object sender, int value);";

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

	protected static string CreateClassTestCode(string @event, bool isAbstract = false)
	{
		return CreateClassTestCode(@event, CustomCode, isAbstract);
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
