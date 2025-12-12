namespace MyNihongo.Mock.Tests.EventTests;

public abstract class EventGenericTestsBase : TestsBase
{
	protected static string CreateTestCode(string @event)
	{
		return
			$$"""
			  namespace MyNihongo.Mock.Tests;

			  public delegate void SampleHandler1(object sender, int value);
			  public delegate void SampleHandler2<T>(object sender, T value);

			  public interface IInterface<T>
			  {
			  	{{@event}}
			  }

			  [MockuraiGenerate]
			  public abstract partial class TestsBase
			  {
			  	protected partial IMock<IInterface<string>> InterfaceMock { get; }
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
				private readonly InterfaceMock<string> _interfaceMock = new(InvocationIndex.CounterValue);
				protected partial MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface<string>> InterfaceMock => _interfaceMock;

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
					public readonly IMockSequence<MyNihongo.Mock.Tests.IInterface<string>> InterfaceMock;

					public VerifySequenceContext(MyNihongo.Mock.IMock<MyNihongo.Mock.Tests.IInterface<string>> interfaceMock)
					{
						InterfaceMock = new MockSequence<MyNihongo.Mock.Tests.IInterface<string>>
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

			  public sealed class InterfaceMock<T> : IMock<MyNihongo.Mock.Tests.IInterface<T>>
			  {
			  	private readonly InvocationIndex.Counter _invocationIndex;
			  	private readonly System.Func<System.Collections.Generic.IEnumerable<IInvocationProvider?>> _invocationProviders;
			  	private Proxy? _proxy;

			  	public InterfaceMock(InvocationIndex.Counter invocationIndex)
			  	{
			  		_invocationIndex = invocationIndex;
			  		_invocationProviders = GetInvocations;
			  	}

			  	public MyNihongo.Mock.Tests.IInterface<T> Object => _proxy ??= new Proxy(this);

			  	// HandlerEvent
			  {{methods.Indent(1)}}

			  	public void VerifyNoOtherCalls()
			  	{

			  	}

			  	private System.Collections.Generic.IEnumerable<IInvocationProvider?> GetInvocations()
			  	{
			  		yield break;
			  	}

			  	private sealed class Proxy : MyNihongo.Mock.Tests.IInterface<T>
			  	{
			  		private readonly InterfaceMock<T> _mock;

			  		public Proxy(InterfaceMock<T> mock)
			  		{
			  			_mock = mock;
			  		}

			  		public {{@event}}

			  	}
			  }

			  public static partial class MockExtensions
			  {
			  	extension<T>(IMock<MyNihongo.Mock.Tests.IInterface<T>> @this)
			  	{
			  		public void VerifyNoOtherCalls() =>
			  			((InterfaceMock<T>)@this).VerifyNoOtherCalls();

			  		
			  	}
			  }

			  public static partial class MockSequenceExtensions
			  {
			  	extension<T>(IMockSequence<MyNihongo.Mock.Tests.IInterface<T>> @this)
			  	{
			  	
			  	}
			  }
			  """;

		return
		[
			("TestsBase.g.cs", testsBase),
			("InterfaceMock_T_.g.cs", mock),
		];
	}
}
