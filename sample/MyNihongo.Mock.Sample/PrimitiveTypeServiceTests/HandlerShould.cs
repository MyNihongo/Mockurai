namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class HandlerShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void VerifyAllInvocations()
	{
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixture(subscribeToHandler: true);
		DependencyServiceMock.RaiseHandler(inputValue1);
		DependencyServiceMock.RaiseHandler(inputValue2);
		fixture.Dispose();
		fixture.Dispose();

		Assert.Equal(inputValue1 + inputValue2, fixture.Sum);

		DependencyServiceMock.VerifyAddHandler(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler, Times.Once);
		DependencyServiceMock.VerifyRemoveHandler(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler, Times.Exactly(2));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfInvalidFunction()
	{
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixture(subscribeToHandler: true);
		DependencyServiceMock.RaiseHandler(inputValue1);
		DependencyServiceMock.RaiseHandler(inputValue2);
		fixture.Dispose();

		var actual = () => DependencyServiceMock.VerifyAddHandler((_, _) => { }, Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Handler.add to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Handler.add
			- 2: IPrimitiveDependencyService.Handler.remove
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfAddNotVerified()
	{
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixture(subscribeToHandler: true);
		DependencyServiceMock.RaiseHandler(inputValue1);
		DependencyServiceMock.RaiseHandler(inputValue2);
		fixture.Dispose();

		DependencyServiceMock.VerifyRemoveHandler(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler, Times.Once);

		var actual = () => VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Handler.add to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.Handler.add
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfRemoveNotVerified()
	{
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixture(subscribeToHandler: true);
		DependencyServiceMock.RaiseHandler(inputValue1);
		DependencyServiceMock.RaiseHandler(inputValue2);
		fixture.Dispose();

		DependencyServiceMock.VerifyAddHandler(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler, Times.Once);

		var actual = () => VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Handler.add to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.Handler.remove
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixture(subscribeToHandler: true);
		DependencyServiceMock.RaiseHandler(inputValue1);
		DependencyServiceMock.RaiseHandler(inputValue2);
		fixture.Dispose();

		VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.AddHandler(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler);
			ctx.DependencyServiceMock.RemoveHandler(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfInvalidSequence()
	{
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixture(subscribeToHandler: true);
		DependencyServiceMock.RaiseHandler(inputValue1);
		DependencyServiceMock.RaiseHandler(inputValue2);
		fixture.Dispose();

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.RemoveHandler(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler);
			ctx.DependencyServiceMock.AddHandler(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Handler.add to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Handler.add
			- 2: IPrimitiveDependencyService.Handler.remove
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixture(subscribeToHandler: true);
		DependencyServiceMock.RaiseHandler(inputValue1);
		DependencyServiceMock.RaiseHandler(inputValue2);
		fixture.Dispose();

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.AddHandler(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.RemoveHandler(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Handler.add
			- 2: IPrimitiveDependencyService.Handler.remove
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
