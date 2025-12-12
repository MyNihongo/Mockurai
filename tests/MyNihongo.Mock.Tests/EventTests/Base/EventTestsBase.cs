namespace MyNihongo.Mock.Tests.EventTests;

public abstract class EventTestsBase : TestsBase
{
	protected static string CreateTestCode(string @event)
	{
		return
			$$"""
			  namespace MyNihongo.Mock.Tests;

			  public delegate void SampleHandler1(object sender, int value);

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

	protected static GeneratedSources CreateGeneratedSources(string methods, string @event)
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

		var mock =
			$$"""
			  namespace MyNihongo.Mock;

			  public sealed class InterfaceMock : IMock<MyNihongo.Mock.Tests.IInterface>
			  {
			  	private readonly InvocationIndex.Counter _invocationIndex;
			  	private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
			  	private Proxy? _proxy;

			  	public InterfaceMock(InvocationIndex.Counter invocationIndex)
			  	{
			  		_invocationIndex = invocationIndex;
			  		_invocationProviders = GetInvocations;
			  	}

			  	public MyNihongo.Mock.Tests.IInterface Object => _proxy ??= new Proxy(this);

			  	// HandlerEvent
			  {{methods.Indent(1)}}

			  	public void VerifyNoOtherCalls()
			  	{

			  	}

			  	private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
			  	{
			  		yield break;
			  	}

			  	private sealed class Proxy : MyNihongo.Mock.Tests.IInterface
			  	{
			  		private readonly InterfaceMock _mock;

			  		public Proxy(InterfaceMock mock)
			  		{
			  			_mock = mock;
			  		}

			  		public {{@event}}

			  	}
			  }

			  public static partial class MockExtensions
			  {
			  	extension(IMock<MyNihongo.Mock.Tests.IInterface> @this)
			  	{
			  		public void VerifyNoOtherCalls() =>
			  			((InterfaceMock)@this).VerifyNoOtherCalls();

			  		
			  	}
			  }

			  public static partial class MockSequenceExtensions
			  {
			  	extension(IMockSequence<MyNihongo.Mock.Tests.IInterface> @this)
			  	{
			  	
			  	}
			  }
			  """;

		return
		[
			("TestsBase.g.cs", testsBase),
			("InterfaceMock.g.cs", mock),
		];
	}
}
