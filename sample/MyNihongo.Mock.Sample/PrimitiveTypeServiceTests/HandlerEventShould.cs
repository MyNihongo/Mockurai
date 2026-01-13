namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class HandlerEventShould : PrimitiveTypeServiceTestsBase
{
	private string _result = string.Empty;

	private void OnEventHandler(object? sender, string data)
	{
		_result = string.Concat(_result, data);
	}

	[Fact]
	public void VerifyAllInvocations()
	{
		const string inputValue1 = nameof(inputValue1), inputValue2 = nameof(inputValue2);

		var fixture = CreateFixture();
		fixture.HandlerEvent += OnEventHandler;
		DependencyServiceMock.RaiseHandlerEvent(inputValue1);
		DependencyServiceMock.RaiseHandlerEvent(inputValue2);
		fixture.HandlerEvent -= OnEventHandler;
		fixture.HandlerEvent -= OnEventHandler;
		DependencyServiceMock.RaiseHandlerEvent("value that will not be accepted");

		Assert.Equal(inputValue1 + inputValue2, _result);

		DependencyServiceMock.VerifyAddHandlerEvent(It<EventHandler<string>?>.Value(OnEventHandler), Times.Once);
		DependencyServiceMock.VerifyRemoveHandlerEvent(It<EventHandler<string>?>.Value(OnEventHandler), Times.Exactly(2));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfInvalidFunction()
	{
		const string inputValue1 = nameof(inputValue1), inputValue2 = nameof(inputValue2);

		var fixture = CreateFixture();
		fixture.HandlerEvent += OnEventHandler;
		DependencyServiceMock.RaiseHandlerEvent(inputValue1);
		DependencyServiceMock.RaiseHandlerEvent(inputValue2);
		fixture.HandlerEvent -= OnEventHandler;

		var actual = () => DependencyServiceMock.VerifyAddHandlerEvent(It<EventHandler<string>?>.Value((_, _) => { }), Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.HandlerEvent.add to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.HandlerEvent.add
			- 2: IPrimitiveDependencyService.HandlerEvent.remove
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfAddNotVerified()
	{
		const string inputValue1 = nameof(inputValue1), inputValue2 = nameof(inputValue2);

		var fixture = CreateFixture();
		fixture.HandlerEvent += OnEventHandler;
		DependencyServiceMock.RaiseHandlerEvent(inputValue1);
		DependencyServiceMock.RaiseHandlerEvent(inputValue2);
		fixture.HandlerEvent -= OnEventHandler;

		DependencyServiceMock.VerifyRemoveHandlerEvent(It<EventHandler<string>?>.Value(OnEventHandler), Times.Once);

		var actual = () => VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.HandlerEvent.add to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.HandlerEvent.add
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfRemoveNotVerified()
	{
		const string inputValue1 = nameof(inputValue1), inputValue2 = nameof(inputValue2);

		var fixture = CreateFixture();
		fixture.HandlerEvent += OnEventHandler;
		DependencyServiceMock.RaiseHandlerEvent(inputValue1);
		DependencyServiceMock.RaiseHandlerEvent(inputValue2);
		fixture.HandlerEvent -= OnEventHandler;

		DependencyServiceMock.VerifyAddHandlerEvent(It<EventHandler<string>?>.Value(OnEventHandler), Times.Once);

		var actual = () => VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.HandlerEvent.remove to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.HandlerEvent.remove
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const string inputValue1 = nameof(inputValue1), inputValue2 = nameof(inputValue2);

		var fixture = CreateFixture();
		fixture.HandlerEvent += OnEventHandler;
		DependencyServiceMock.RaiseHandlerEvent(inputValue1);
		DependencyServiceMock.RaiseHandlerEvent(inputValue2);
		fixture.HandlerEvent -= OnEventHandler;

		VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.AddHandlerEvent(OnEventHandler);
			ctx.DependencyServiceMock.RemoveHandlerEvent(OnEventHandler);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfInvalidSequence()
	{
		const string inputValue1 = nameof(inputValue1), inputValue2 = nameof(inputValue2);

		var fixture = CreateFixture();
		fixture.HandlerEvent += OnEventHandler;
		DependencyServiceMock.RaiseHandlerEvent(inputValue1);
		DependencyServiceMock.RaiseHandlerEvent(inputValue2);
		fixture.HandlerEvent -= OnEventHandler;

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.RemoveHandlerEvent(OnEventHandler);
			ctx.DependencyServiceMock.AddHandlerEvent(OnEventHandler);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.HandlerEvent.add to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.HandlerEvent.add
			- 2: IPrimitiveDependencyService.HandlerEvent.remove
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const string inputValue1 = nameof(inputValue1), inputValue2 = nameof(inputValue2);

		var fixture = CreateFixture();
		fixture.HandlerEvent += OnEventHandler;
		DependencyServiceMock.RaiseHandlerEvent(inputValue1);
		DependencyServiceMock.RaiseHandlerEvent(inputValue2);
		fixture.HandlerEvent -= OnEventHandler;

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.AddHandlerEvent(OnEventHandler);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.RemoveHandlerEvent(OnEventHandler);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.HandlerEvent.add
			- 2: IPrimitiveDependencyService.HandlerEvent.remove
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
