namespace MyNihongo.Mockurai.Tests.Issues;

public abstract class IssuesTestsBase : TestsBase
{
	protected static readonly GeneratedSource TestsBase =
	(
		"TestsBase.g.cs",
		"""
		#nullable enable
		namespace Issues.Tests;

		public partial class TestsBase
		{
			// InterfaceMock
			private readonly InterfaceMock _interfaceMock = new(InvocationIndex.CounterValue);
			protected partial IMock<Issues.Tests.IInterface> InterfaceMock => _interfaceMock;

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

				public VerifySequenceContext(IMock<Issues.Tests.IInterface> interfaceMock)
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
