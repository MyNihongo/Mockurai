// namespace MyNihongo.Mock.Sample.StructTypeServiceTests;
//
// public sealed class InvokeShould : StructTypeServiceTestsBase
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
// 		StructDependencyServiceMock
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
