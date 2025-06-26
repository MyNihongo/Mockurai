namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public abstract class PrimitiveTypeServiceTestsBase
{
	protected readonly IMock<IPrimitiveDependencyService> DependencyServiceMock/* = new PrimitiveDependencyServiceMock()*/;

	protected IPrimitiveTypeService CreateFixture()
	{
		return new PrimitiveTypeService(
			primitiveDependencyService: DependencyServiceMock.Object
		);
	}
}
