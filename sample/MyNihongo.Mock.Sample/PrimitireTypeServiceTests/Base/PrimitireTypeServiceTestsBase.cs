namespace MyNihongo.Mock.Sample.PrimitireTypeServiceTests;

public abstract class PrimitireTypeServiceTestsBase
{
	protected readonly IMock<IPrimitiveDependencyService> DependencyServiceMock = new SampleMock();

	protected IPrimitiveTypeService CreateFixture()
	{
		return new PrimitiveTypeService(
			primitiveDependencyService: DependencyServiceMock.Object
		);
	}
}
