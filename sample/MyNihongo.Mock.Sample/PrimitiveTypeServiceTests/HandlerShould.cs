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

		DependencyServiceMock.VerifyAddHandler(It<PrimitiveHandler?>.Value(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler), Times.Once);
		DependencyServiceMock.VerifyRemoveHandler(It<PrimitiveHandler?>.Value(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler), Times.Exactly(2));
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

		var actual = () => DependencyServiceMock.VerifyAddHandler(It<PrimitiveHandler?>.Value((_, _) => { }), Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Handler.add += MyNihongo.Mock.Sample.PrimitiveHandler to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Handler.add += MyNihongo.Mock.Sample.PrimitiveHandler
			- 2: IPrimitiveDependencyService.Handler.remove -= MyNihongo.Mock.Sample.PrimitiveHandler
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

		DependencyServiceMock.VerifyRemoveHandler(It<PrimitiveHandler?>.Value(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler), Times.Once);

		var actual = () => VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Handler.add += PrimitiveHandler to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.Handler.add += MyNihongo.Mock.Sample.PrimitiveHandler
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

		DependencyServiceMock.VerifyAddHandler(It<PrimitiveHandler?>.Value(((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler), Times.Once);

		var actual = () => VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Handler.remove -= PrimitiveHandler to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.Handler.remove -= MyNihongo.Mock.Sample.PrimitiveHandler
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
			PrimitiveHandler handler = ((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler;
			ctx.DependencyServiceMock.AddHandler(It<PrimitiveHandler?>.Value(handler));
			ctx.DependencyServiceMock.RemoveHandler(It<PrimitiveHandler?>.Value(handler));
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
			PrimitiveHandler handler = ((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler;
			ctx.DependencyServiceMock.RemoveHandler(It<PrimitiveHandler?>.Value(handler));
			ctx.DependencyServiceMock.AddHandler(It<PrimitiveHandler?>.Value(handler));
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Handler.add += MyNihongo.Mock.Sample.PrimitiveHandler to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Handler.add += MyNihongo.Mock.Sample.PrimitiveHandler
			- 2: IPrimitiveDependencyService.Handler.remove -= MyNihongo.Mock.Sample.PrimitiveHandler
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
			PrimitiveHandler handler = ((PrimitiveTypeService)fixture).PrimitiveDependencyServiceOnHandler;
			ctx.DependencyServiceMock.AddHandler(It<PrimitiveHandler?>.Value(handler));
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.RemoveHandler(It<PrimitiveHandler?>.Value(handler));
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Handler.add += MyNihongo.Mock.Sample.PrimitiveHandler
			- 2: IPrimitiveDependencyService.Handler.remove -= MyNihongo.Mock.Sample.PrimitiveHandler
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
