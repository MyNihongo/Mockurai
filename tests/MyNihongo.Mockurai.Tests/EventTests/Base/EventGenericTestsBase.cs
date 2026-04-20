namespace MyNihongo.Mockurai.Tests.EventTests;

public abstract class EventGenericTestsBase : TestsGenericBase
{
	private const string CustomCode =
		"""
		public delegate void SampleHandler1(object sender, int value);
		public delegate void SampleHandler2<T>(object sender, T value);
		""";

	protected static string CreateInterfaceTestCode(string @event)
	{
		return CreateInterfaceTestCode(@event, CustomCode);
	}

	protected static GeneratedSources CreateInterfaceGeneratedSources(string methods, string proxy, string invocationContainer, string verifyNoOtherCalls, string invocations, string extensions, string sequenceExtensions)
	{
		var testsBase = GetInterfaceTestsBase();
		var mock = GetInterfaceMock(methods, proxy, invocationContainer, verifyNoOtherCalls, invocations, extensions, sequenceExtensions);

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

	protected static GeneratedSources CreateClassGeneratedSources(string methods, string proxy, string verifyNoOtherCalls, string invocations, string extensions, string sequenceExtensions, string invocationContainer)
	{
		var testsBase = GetClassTestsBase();
		var mock = GetClassMock(methods, proxy, verifyNoOtherCalls, invocations, extensions, sequenceExtensions, invocationContainer);

		return
		[
			testsBase,
			mock,
		];
	}
}
