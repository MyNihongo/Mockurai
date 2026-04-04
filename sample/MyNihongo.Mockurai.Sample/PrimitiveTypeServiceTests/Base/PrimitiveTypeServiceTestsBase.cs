namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

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
