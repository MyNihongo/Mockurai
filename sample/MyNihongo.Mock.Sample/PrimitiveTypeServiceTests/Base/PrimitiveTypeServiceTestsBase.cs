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
	private readonly PrimitiveDependencyServiceMock _dependencyServiceMock = new PrimitiveDependencyServiceMock();

	protected partial IMock<IPrimitiveDependencyService> DependencyServiceMock => _dependencyServiceMock;

	protected void VerifyInSequence()
	{
		
	}

	protected void VerifyNoOtherCalls()
	{
		_dependencyServiceMock.VerifyNoOtherCalls();
	}
}
