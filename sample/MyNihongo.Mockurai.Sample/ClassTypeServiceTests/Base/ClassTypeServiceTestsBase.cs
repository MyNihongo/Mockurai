namespace MyNihongo.Mockurai.Sample.ClassTypeServiceTests;

[MockuraiGenerate]
public abstract partial class ClassTypeServiceTestsBase
{
	protected partial IMock<IClassDependencyService> ClassDependencyServiceMock { get; }

	protected IClassTypeService CreateFixture()
	{
		return new ClassTypeService(
			classDependencyService: ClassDependencyServiceMock.Object
		);
	}
}
