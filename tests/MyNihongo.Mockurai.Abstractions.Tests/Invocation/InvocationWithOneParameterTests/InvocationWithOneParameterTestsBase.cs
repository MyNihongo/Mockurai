namespace MyNihongo.Mockurai.Abstractions.Tests.Invocation.InvocationWithOneParameterTests;

public abstract class InvocationWithOneParameterTestsBase
{
	protected static Invocation<T> CreateFixture<T>()
	{
		return new Invocation<T>(name: "MyClass.MyMethod({0})");
	}
}
