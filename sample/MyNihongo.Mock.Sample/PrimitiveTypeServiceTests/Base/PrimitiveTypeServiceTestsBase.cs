namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public abstract partial class PrimitiveTypeServiceTestsBase
{
	protected partial IMock<IPrimitiveDependencyService> DependencyServiceMock { get; }

	protected IPrimitiveTypeService CreateFixture()
	{
		return new PrimitiveTypeService(
			primitiveDependencyService: DependencyServiceMock.Object
		);
	}
}

public partial class PrimitiveTypeServiceTestsBase
{
	private readonly PrimitiveDependencyServiceMock _dependencyServiceMock = new();

	protected partial IMock<IPrimitiveDependencyService> DependencyServiceMock => _dependencyServiceMock;

	protected void VerifyInSequence(Action<VerifySequenceContext> verify)
	{
		var ctx = new VerifySequenceContext(
			dependencyServiceMock: _dependencyServiceMock
		);

		verify(ctx);
	}

	protected void VerifyNoOtherCalls()
	{
		_dependencyServiceMock.VerifyNoOtherCalls();
	}

	protected sealed class VerifySequenceContext
	{
		private readonly VerifyIndex _verifyIndex = new();
		public readonly IMockSequence<IPrimitiveDependencyService> DependencyServiceMock;

		public VerifySequenceContext(in PrimitiveDependencyServiceMock dependencyServiceMock)
		{
			DependencyServiceMock = new MockSequence<IPrimitiveDependencyService>
			{
				VerifyIndex = _verifyIndex,
				Mock = dependencyServiceMock,
			};
		}
	}
}
