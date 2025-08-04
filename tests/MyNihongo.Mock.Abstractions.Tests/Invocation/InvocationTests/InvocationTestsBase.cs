namespace MyNihongo.Mock.Abstractions.Tests.Invocation.InvocationTests;

public abstract class InvocationTestsBase
{
	protected static Mock.Invocation CreateFixture()
	{
		return new Mock.Invocation(name: "MyClass#MyMethod()");
	}
}
