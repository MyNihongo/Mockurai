// namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;
//
// public sealed class InvokeShould : PrimitiveTypeServiceTestsBase
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
// 		DependencyServiceMock
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
