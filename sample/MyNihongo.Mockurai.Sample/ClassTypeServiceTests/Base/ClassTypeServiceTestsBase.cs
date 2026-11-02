namespace MyNihongo.Mockurai.Sample.ClassTypeServiceTests;

[MockuraiGenerate]
public abstract partial class ClassTypeServiceTestsBase
{
	protected readonly IMock<IClassDependencyService> ClassDependencyServiceMock = Mock.Create<IClassDependencyService>();

	protected IClassTypeService CreateFixture()
	{
		return new ClassTypeService(
			classDependencyService: ClassDependencyServiceMock.Object
		);
	}
}
