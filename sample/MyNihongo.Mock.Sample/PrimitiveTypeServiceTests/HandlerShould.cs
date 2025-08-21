namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class HandlerShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void VerifyAllInvocations()
	{
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixture();
		DependencyServiceMock.RaiseHandler(inputValue1);
		DependencyServiceMock.RaiseHandler(inputValue2);
		fixture.Dispose();

		Assert.Equal(inputValue1 + inputValue2, fixture.Sum);

		DependencyServiceMock.VerifyAddHandler(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler, Times.Once);
		DependencyServiceMock.VerifyRemoveHandler(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler, Times.Once);
		VerifyNoOtherCalls();
	}
}
