namespace MyNihongo.Mock.Tests.Issues;

public sealed class MultipleDeclarations
{
	[Fact]
	public async Task NotGenerateDuplicateMethodSetupAndInvocationFromDifferentClasses()
	{
		const string testCode =
			"""
			namespace MyNihongo.Mock.Tests;

			public interface IInterface
			{
				void Invoke(int param1, double param2);
			}

			public abstract class AbstractClass
			{
				public abstract void Invoke(int value1, double value2);
			}

			[MockuraiGenerate]
			public abstract partial class TestsBase
			{
				protected partial IMock<IInterface> InterfaceMock { get; }
				protected partial IMock<AbstractClass> AbstractClassMock { get; }
			}
			""";

		throw new NotImplementedException();
	}
}
