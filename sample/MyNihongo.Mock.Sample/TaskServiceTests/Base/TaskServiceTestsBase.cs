namespace MyNihongo.Mock.Sample.TaskServiceTests;

public abstract class TaskServiceTestsBase
{
	protected readonly IMock<ITaskDependencyService> TaskDependencyServiceMock = new TaskDependencyServiceMock();

	protected ITaskService CreateFixture()
	{
		return new TaskService(
			taskDependencyService: TaskDependencyServiceMock.Object
		);
	}
}
