namespace MyNihongo.Mock.Tests.EventTests;

public abstract class EventTestsBase : TestsBase
{
	protected static string CreateTestCode(string @event)
	{
		return
			$$"""
			  namespace MyNihongo.Mock.Tests;

			  public interface IInterface
			  {
			  	{{@event}}
			  }

			  [MockuraiGenerate]
			  public abstract partial class TestsBase
			  {
			  	protected partial IMock<IInterface> InterfaceMock { get; }
			  }
			  """;
	}
}
