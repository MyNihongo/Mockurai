namespace MyNihongo.Mock.Sample._Generated.InvocationWithSeveralParametersGeneric1Tests;

public abstract class InvocationWithSeveralParametersTestsBase
{
	protected static InvocationT1Int<T> CreateFixturePrimitive<T>()
	{
		return new InvocationT1Int<T>(name: "MyClass.MyMethod<T>({0}, {1})");
	}
}
