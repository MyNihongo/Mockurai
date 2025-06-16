namespace MyNihongo.Mock.Sample.ClassTypeServiceTests;

public abstract class ClassTypeServiceTestsBase
{
	protected readonly Mock<IClassDependencyService> ClassDependencyServiceMock = new ClassDependencyServiceMock();

	protected IClassTypeService CreateFixture()
	{
		return new ClassTypeService(
			classDependencyService: ClassDependencyServiceMock.Object
		);
	}
}
