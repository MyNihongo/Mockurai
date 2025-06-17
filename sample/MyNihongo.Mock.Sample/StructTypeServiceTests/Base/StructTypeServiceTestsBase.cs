namespace MyNihongo.Mock.Sample.StructTypeServiceTests;

public abstract class StructTypeServiceTestsBase
{
	protected readonly Mock<IStructDependencyService> StructDependencyServiceMock = new StructDependencyServiceMock();

	protected IStructTypeService CreateFixture()
	{
		return new StructTypeService(
			structDependencyService: StructDependencyServiceMock.Object
		);
	}
}
