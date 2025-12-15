namespace MyNihongo.Mock.Tests.EventTests;

public abstract class EventTestsBase : TestsBase
{
	private const string CustomCode = "public delegate void SampleHandler1(object sender, int value);";

	protected static string CreateInterfaceTestCode(string @event)
	{
		return CreateInterfaceTestCode(@event, CustomCode);
	}

	protected static string CreateClassTestCode(string @event)
	{
		return CreateClassTestCode(@event, CustomCode);
	}

	protected static GeneratedSources CreateInterfaceGeneratedSources(string methods, string @event)
	{
		var testsBase = GetInterfaceTestsBase();
		var mock = GetInterfaceMock(methods, @event);

		return
		[
			testsBase,
			("InterfaceMock.g.cs", mock),
		];
	}
}
