namespace MyNihongo.Mock.Sample.TaskServiceTests;

public abstract class TaskServiceTestsBase
{
	protected readonly IMock<ITaskDependencyService> TaskDependencyServiceMock;

	protected ITaskService CreateFixture()
	{
		return new TaskService(
			taskDependencyService: TaskDependencyServiceMock.Object
		);
	}
}
