namespace MyNihongo.Mock.Sample.ValueTaskServiceTests;

public abstract class ValueTaskServiceTestsBase
{
	protected readonly IMock<IValueTaskDependencyService> ValueTaskDependencyServiceMock = new ValueTaskDependencyServiceMock();

	protected IValueTaskService CreateFixture()
	{
		return new ValueTaskService(
			taskDependencyService: ValueTaskDependencyServiceMock.Object
		);
	}
}
