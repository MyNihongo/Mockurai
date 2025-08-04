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

public abstract partial class PrimitiveTypeServiceTestsBase
{
	private readonly IMock<IPrimitiveDependencyService> _dependencyServiceMock = new PrimitiveDependencyServiceMock();

	protected partial IMock<IPrimitiveDependencyService> DependencyServiceMock => _dependencyServiceMock;
}
