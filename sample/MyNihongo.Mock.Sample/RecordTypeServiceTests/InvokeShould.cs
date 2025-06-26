// namespace MyNihongo.Mock.Sample.RecordTypeServiceTests;
//
// public sealed class InvokeShould : RecordTypeServiceTestsBase
// {
// 	[Fact]
// 	public void ExecuteWithoutSetup()
// 	{
// 		CreateFixture()
// 			.Invoke();
// 	}
//
// 	[Fact]
// 	public void ThrowWithSetup()
// 	{
// 		const string errorMessage = nameof(errorMessage);
//
// 		RecordDependencyServiceMock
// 			.SetupInvoke()
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		var actual = () => CreateFixture()
// 			.Invoke();
//
// 		var exception = Assert.Throws<InvalidOperationException>(actual);
// 		Assert.Equal(errorMessage, exception.Message);
// 	}
// }
