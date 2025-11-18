namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public abstract partial class PrimitiveTypeServiceTestsBase
{
	protected partial IMock<IPrimitiveDependencyService> DependencyServiceMock { get; }
	
	protected partial IMock<IPrimitiveDependencyService<string>> DependencyGenericServiceMock { get; }

	protected IPrimitiveTypeService CreateFixture(bool subscribeToHandler = false)
	{
		return new PrimitiveTypeService(
			primitiveDependencyService: DependencyServiceMock.Object,
			primitiveDependencyGenericService: DependencyGenericServiceMock.Object,
			subscribeToHandler
		);
	}
}

public partial class PrimitiveTypeServiceTestsBase
{
	private readonly PrimitiveDependencyServiceMock _dependencyServiceMock = new(InvocationIndex.CounterValue);
	private readonly PrimitiveDependencyServiceMock<string> _dependencyServiceGenericMock = new(InvocationIndex.CounterValue);

	protected partial IMock<IPrimitiveDependencyService> DependencyServiceMock => _dependencyServiceMock;
	protected partial IMock<IPrimitiveDependencyService<string>> DependencyGenericServiceMock => _dependencyServiceGenericMock;

	protected void VerifyInSequence(Action<VerifySequenceContext> verify)
	{
		var ctx = new VerifySequenceContext(
			dependencyServiceMock: _dependencyServiceMock,
			dependencyGenericServiceMock: _dependencyServiceGenericMock
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
		public readonly IMockSequence<IPrimitiveDependencyService<string>> DependencyGenericServiceMock;

		public VerifySequenceContext(in PrimitiveDependencyServiceMock dependencyServiceMock, in PrimitiveDependencyServiceMock<string> dependencyGenericServiceMock)
		{
			DependencyServiceMock = new MockSequence<IPrimitiveDependencyService>
			{
				VerifyIndex = _verifyIndex,
				Mock = dependencyServiceMock,
			};
			DependencyGenericServiceMock = new MockSequence<IPrimitiveDependencyService<string>>
			{
				VerifyIndex = _verifyIndex,
				Mock = dependencyGenericServiceMock,
			};
		}
	}
}
