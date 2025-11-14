namespace MyNihongo.Mock.Sample._Generated.InvocationWithSeveralParametersGeneric2Tests;

public abstract class InvocationWithSeveralParametersTestsBase
{
	protected static InvocationT1T2<T1, T2> CreateFixturePrimitive<T1, T2>()
	{
		return new InvocationT1T2<T1, T2>(name: "MyClass.MyMethod<T1, T2>({0}, {1})");
	}
}
