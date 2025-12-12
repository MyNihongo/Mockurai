namespace MyNihongo.Mock.Tests.EventTests;

public abstract class EventTestsBase : TestsBase
{
	protected static string CreateTestCode(string @event)
	{
		return
			$$"""
			  namespace MyNihongo.Mock.Tests;
			  
			  public delegate void SampleHandler1(object sender, int value);
			  public delegate void SampleHandler2<T1, T2>(object sender, T2 value);

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

	protected static GeneratedSources CreateGeneratedSources(string expected)
	{
		const string testsBase =
			"""
			namespace MyNihongo.Mock.Tests;

			public partial class TestsBase
			{
				// InterfaceMock
				private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
				protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> InterfaceMock => _interfaceMock;

				protected void VerifyNoOtherCalls()
				{
					_interfaceMock.VerifyNoOtherCalls();
				}

				protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
				{
					var ctx = new VerifySequenceContext(
						interfaceMock: _interfaceMock
					);

					verify(ctx);
				}

				protected sealed class VerifySequenceContext
				{
					private readonly VerifyIndex _verifyIndex = new();
					public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface> InterfaceMock;

					public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface> interfaceMock)
					{
						InterfaceMock = new MockSequence<MyNihongo.Mock.Tests.IInterface>
						{
							VerifyIndex = _verifyIndex,
							Mock = interfaceMock,
						};
					}
				}
			}
			""";

		return
		[
			("TestsBase.g.cs", testsBase),
			("InterfaceMock.g.cs", expected),
		];
	}
}
