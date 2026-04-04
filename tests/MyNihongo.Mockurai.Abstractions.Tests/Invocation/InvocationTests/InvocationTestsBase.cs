namespace MyNihongo.Mockurai.Abstractions.Tests.Invocation.InvocationTests;

public abstract class InvocationTestsBase
{
	protected static Mockurai.Invocation CreateFixture()
	{
		return new Mockurai.Invocation(name: "MyClass.MyMethod()");
	}
}
