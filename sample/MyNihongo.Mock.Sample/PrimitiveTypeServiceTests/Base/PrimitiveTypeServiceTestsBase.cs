namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

[MockuraiGenerate]
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
	protected void VerifyNoOtherCalls()
	{
		_dependencyServiceMock.VerifyNoOtherCalls();
		_dependencyGenericServiceMock.VerifyNoOtherCalls();
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
