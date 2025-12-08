namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public abstract partial class PrimitiveTypeServiceTestsBase
{
	protected partial IMock<IPrimitiveDependencyService> DependencyServiceMock { get; }
	
	protected partial IMock<IPrimitiveDependencyService<string>> DependencyGenericServiceMock { get; }
	
	protected partial IMock<PrimitiveDependency> PrimitiveDependencyMock { get; }
	
	protected partial IMock<PrimitiveDependencyBase> PrimitiveDependencyBaseMock { get; }

	protected IPrimitiveTypeService CreateFixture(bool subscribeToHandler = false)
	{
		return new PrimitiveTypeService(
			primitiveDependencyService: DependencyServiceMock.Object,
			primitiveDependencyGenericService: DependencyGenericServiceMock.Object,
			primitiveDependency: PrimitiveDependencyMock.Object,
			primitiveDependencyBase: PrimitiveDependencyBaseMock.Object,
			subscribeToHandler
		);
	}
}

public partial class PrimitiveTypeServiceTestsBase
{
	private readonly PrimitiveDependencyServiceMock _dependencyServiceMock = new(InvocationIndex.CounterValue);
	private readonly PrimitiveDependencyServiceMock<string> _dependencyServiceGenericMock = new(InvocationIndex.CounterValue);
	private readonly PrimitiveDependencyMock _primitiveDependencyMock = new(InvocationIndex.CounterValue);
	private readonly PrimitiveDependencyBaseMock _primitiveDependencyBaseMock = new(InvocationIndex.CounterValue);

	protected partial IMock<IPrimitiveDependencyService> DependencyServiceMock => _dependencyServiceMock;
	protected partial IMock<IPrimitiveDependencyService<string>> DependencyGenericServiceMock => _dependencyServiceGenericMock;
	protected partial IMock<PrimitiveDependency> PrimitiveDependencyMock => _primitiveDependencyMock;
	protected partial IMock<PrimitiveDependencyBase> PrimitiveDependencyBaseMock => _primitiveDependencyBaseMock;

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
		_dependencyServiceGenericMock.VerifyNoOtherCalls();
		_primitiveDependencyMock.VerifyNoOtherCalls();
		_primitiveDependencyBaseMock.VerifyNoOtherCalls();
	}

	protected sealed class VerifySequenceContext
	{
		private readonly VerifyIndex _verifyIndex = new();
		public readonly IMockSequence<IPrimitiveDependencyService> DependencyServiceMock;
		public readonly IMockSequence<IPrimitiveDependencyService<string>> DependencyGenericServiceMock;
		public readonly IMockSequence<PrimitiveDependency> PrimitiveDependencyMock;
		public readonly IMockSequence<PrimitiveDependencyBase> PrimitiveDependencyBaseMock;

		public VerifySequenceContext(in PrimitiveDependencyServiceMock dependencyServiceMock, in PrimitiveDependencyServiceMock<string> dependencyGenericServiceMock, in PrimitiveDependencyMock primitiveDependencyMock, in PrimitiveDependencyBaseMock primitiveDependencyBaseMock)
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
			PrimitiveDependencyMock = new MockSequence<PrimitiveDependency>
			{
				VerifyIndex = _verifyIndex,
				Mock = primitiveDependencyMock,
			};
			PrimitiveDependencyBaseMock = new MockSequence<PrimitiveDependencyBase>
			{
				VerifyIndex = _verifyIndex,
				Mock = primitiveDependencyBaseMock,
			};
		}
	}
}
