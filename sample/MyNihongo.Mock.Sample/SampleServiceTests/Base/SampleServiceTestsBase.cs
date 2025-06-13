namespace MyNihongo.Mock.Sample.SampleServiceTests;

public abstract class SampleServiceTestsBase
{
	protected readonly IMock<IDependencyService> DependencyServiceMock = new SampleMock();

	protected ISampleService CreateFixture()
	{
		return new SampleService(
			dependencyService: DependencyServiceMock.Object
		);
	}
}
