namespace MyNihongo.Mock.Sample.RecordTypeServiceTests;

public abstract class RecordTypeServiceTestsBase
{
	protected readonly IMock<IRecordDependencyService> RecordDependencyServiceMock/* = new RecordDependencyServiceMock()*/;

	protected IRecordTypeService CreateFixture()
	{
		return new RecordTypeService(
			recordDependencyService: RecordDependencyServiceMock.Object
		);
	}
}
