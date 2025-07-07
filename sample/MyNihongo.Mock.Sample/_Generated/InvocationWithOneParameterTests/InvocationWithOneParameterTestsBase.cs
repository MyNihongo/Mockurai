namespace MyNihongo.Mock.Sample._Generated.InvocationWithOneParameterTests;

public abstract class InvocationWithOneParameterTestsBase
{
	protected static Invocation<T> CreateFixture<T>()
	{
		return new Invocation<T>(name: $"MyClass#MyMethod({typeof(T).Name})");
	}
}
