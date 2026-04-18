namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

[MockuraiGenerate]
public abstract partial class PrimitiveTypeServiceTestsDerivedBase : PrimitiveTypeServiceTestsBase
{
	protected partial IMock<IPrimitiveDependencyService> DependencyServiceDerivedMock { get; }
}
