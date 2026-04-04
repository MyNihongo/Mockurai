namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnAsyncShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public async Task ReturnValueWithoutSetup()
	{
		const bool expected = false;

		var actual = await CreateFixture()
			.ReturnAsync();

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyReturnAsync(Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyReturnAsync(Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.ReturnAsync() to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task ReturnValueWithSetup()
	{
		const bool setupCount = true;

		DependencyServiceMock
			.SetupReturnAsync()
			.Returns(setupCount);

		var actual = await CreateFixture()
			.ReturnAsync();

		Assert.Equal(setupCount, actual);
	}

	[Fact]
	public async Task ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnAsync()
			.Throws(new InvalidOperationException(errorMessage));

		var actual = async () => await CreateFixture()
			.ReturnAsync();

		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task VerifyTimes()
	{
		var fixture = CreateFixture();
		await fixture.ReturnAsync();
		await fixture.ReturnAsync();

		DependencyServiceMock.VerifyReturnAsync(Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowVerifyTimes()
	{
		var fixture = CreateFixture();
		await fixture.ReturnAsync();
		await fixture.ReturnAsync();

		var actual = () => DependencyServiceMock.VerifyReturnAsync(Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnAsync() to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnAsync()
			- 2: IPrimitiveDependencyService.ReturnAsync()
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowVerifyNoOtherCalls()
	{
		var fixture = CreateFixture();
		await fixture.ReturnAsync();
		fixture.ReturnWithParameter("value");

		DependencyServiceMock.VerifyReturnAsync(Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter(in String) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.ReturnWithParameter(in "value")
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task VerifyValidSequence()
	{
		var fixture = CreateFixture();
		await fixture.ReturnAsync();
		await fixture.ReturnAsync();

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnAsync();
			ctx.DependencyServiceMock.ReturnAsync();
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowInvalidSequence()
	{
		var fixture = CreateFixture();
		await fixture.ReturnAsync();
		await fixture.ReturnAsync();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnAsync();
			ctx.DependencyServiceMock.ReturnAsync();
			ctx.DependencyServiceMock.ReturnAsync();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnAsync() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnAsync()
			- 2: IPrimitiveDependencyService.ReturnAsync()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidMethodInSequence()
	{
		var fixture = CreateFixture();
		await fixture.ReturnAsync();
		await fixture.ReturnAsync();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnAsync();
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.ReturnAsync();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnAsync()
			- 2: IPrimitiveDependencyService.ReturnAsync()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ReturnDifferentValues()
	{
		const bool setupValue1 = true, setupValue2 = false;

		DependencyServiceMock
			.SetupReturnAsync()
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, await fixture.ReturnAsync());
		Assert.Equal(setupValue2, await fixture.ReturnAsync());
		Assert.Equal(setupValue2, await fixture.ReturnAsync());

		DependencyServiceMock.VerifyReturnAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnDifferentValuesWithCallback1()
	{
		const bool setupValue1 = false, setupValue2 = true;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnAsync()
			.Callback(() => callback++)
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.False(await fixture.ReturnAsync());
		Assert.Equal(setupValue1, await fixture.ReturnAsync());
		Assert.Equal(setupValue2, await fixture.ReturnAsync());
		Assert.Equal(setupValue2, await fixture.ReturnAsync());
		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyReturnAsync(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnDifferentValuesWithCallback2()
	{
		const bool setupValue1 = true, setupValue2 = false;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnAsync()
			.Callback(() => callback++).And().Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, await fixture.ReturnAsync());
		Assert.Equal(setupValue2, await fixture.ReturnAsync());
		Assert.Equal(setupValue2, await fixture.ReturnAsync());
		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyReturnAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnDifferentValuesWithCallback3()
	{
		const bool setupValue1 = true, setupValue2 = false;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnAsync()
			.Returns(setupValue1)
			.Returns(setupValue2).And().Callback(() => callback++);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, await fixture.ReturnAsync());
		Assert.Equal(setupValue2, await fixture.ReturnAsync());
		Assert.Equal(setupValue2, await fixture.ReturnAsync());
		Assert.Equal(2, callback);

		DependencyServiceMock.VerifyReturnAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnDifferentValuesWithCallback4()
	{
		const bool setupValue1 = true, setupValue2 = false;
		var callback = 0;

		DependencyServiceMock
			.SetupReturnAsync()
			.Callback(() => callback++).And().Returns(setupValue1)
			.Returns(setupValue2).And().Callback(() => callback++);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, await fixture.ReturnAsync());
		Assert.Equal(setupValue2, await fixture.ReturnAsync());
		Assert.Equal(setupValue2, await fixture.ReturnAsync());
		Assert.Equal(3, callback);

		DependencyServiceMock.VerifyReturnAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		DependencyServiceMock
			.SetupReturnAsync()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnAsync(); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => { _ = await fixture.ReturnAsync(); };
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnAsync(); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyReturnAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupReturnAsync()
			.Callback(() => callback++)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		Assert.False(await fixture.ReturnAsync());

		var actual2 = async () => { _ = await fixture.ReturnAsync(); };
		var exception2 = await Assert.ThrowsAsync<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnAsync(); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = async () => { _ = await fixture.ReturnAsync(); };
		var exception4 = await Assert.ThrowsAsync<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyReturnAsync(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupReturnAsync()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnAsync(); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => { _ = await fixture.ReturnAsync(); };
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnAsync(); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyReturnAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupReturnAsync()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnAsync(); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => { _ = await fixture.ReturnAsync(); };
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnAsync(); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2, callback);

		DependencyServiceMock.VerifyReturnAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupReturnAsync()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnAsync(); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => { _ = await fixture.ReturnAsync(); };
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnAsync(); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3, callback);

		DependencyServiceMock.VerifyReturnAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowExceptionWithReturnAsync()
	{
		const string errorMessage = nameof(errorMessage);
		const bool setupValue = true;

		DependencyServiceMock
			.SetupReturnAsync()
			.Throws(new COMException(errorMessage))
			.Returns(setupValue);

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnAsync(); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		Assert.Equal(setupValue, await fixture.ReturnAsync());
		Assert.Equal(setupValue, await fixture.ReturnAsync());

		DependencyServiceMock.VerifyReturnAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage);
		const bool setupValue = false;

		DependencyServiceMock
			.SetupReturnAsync()
			.Returns(setupValue)
			.Throws(new COMException(errorMessage));

		var fixture = CreateFixture();

		Assert.Equal(setupValue, await fixture.ReturnAsync());

		var actual2 = async () => { _ = await fixture.ReturnAsync(); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual2);
		Assert.Equal(errorMessage, exception1.Message);

		var actual3 = async () => { _ = await fixture.ReturnAsync(); };
		var exception3 = await Assert.ThrowsAsync<COMException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		DependencyServiceMock.VerifyReturnAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}
