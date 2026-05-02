namespace MyNihongo.Mockurai.Tests.Issues;

public abstract class IssuesTestsBase : TestsBase
{
	protected static GeneratedSource GetTestsBaseSource(string fileName = "TestsBase", bool addNamespace = true)
	{
		var namespacePrefix = addNamespace ? "MyNihongo.Mockurai." : string.Empty;

		return (
			$"{fileName}.g.cs",
			$$"""
			  #nullable enable
			  namespace Issues.Tests;

			  public partial class {{fileName}}
			  {
			  	// InterfaceMock
			  	private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
			  	protected partial {{namespacePrefix}}IMock<Issues.Tests.IInterface> InterfaceMock => _interfaceMock;

			  	protected virtual void VerifyNoOtherCalls()
			  	{
			  		InterfaceMock.VerifyNoOtherCalls();
			  	}

			  	protected void VerifyInSequence(System.Action<VerifySequenceContext> verify)
			  	{
			  		var ctx = new VerifySequenceContext(
			  			interfaceMock: InterfaceMock
			  		);

			  		verify(ctx);
			  	}

			  	protected class VerifySequenceContext
			  	{
			  		protected readonly VerifyIndex VerifyIndex;
			  		public readonly IMockSequence<Issues.Tests.IInterface> InterfaceMock;

			  		public VerifySequenceContext({{namespacePrefix}}IMock<Issues.Tests.IInterface> interfaceMock)
			  		{
			  			VerifyIndex = new VerifyIndex();
			  			InterfaceMock = new MockSequence<Issues.Tests.IInterface>
			  			{
			  				VerifyIndex = VerifyIndex,
			  				Mock = interfaceMock,
			  			};
			  		}

			  		protected VerifySequenceContext(VerifySequenceContext ctx)
			  		{
			  			VerifyIndex = ctx.VerifyIndex;
			  			InterfaceMock = ctx.InterfaceMock;
			  		}
			  	}
			  }
			  """
		);
	}
}
