namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeAsyncShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public async Task ExecuteWithoutSetup()
	{
		await CreateFixture()
			.InvokeAsync();
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyInvokeAsync(Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyInvokeAsync(Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.InvokeAsync() to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeAsync()
			.Throws(new InvalidOperationException(errorMessage));

		var actual = async () => await CreateFixture()
			.InvokeAsync();

		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task VerifyTimes()
	{
		var fixture = CreateFixture();
		await fixture.InvokeAsync();
		await fixture.InvokeAsync();

		DependencyServiceMock.VerifyInvokeAsync(Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowVerifyTimes()
	{
		var fixture = CreateFixture();
		await fixture.InvokeAsync();
		await fixture.InvokeAsync();

		var actual = () => DependencyServiceMock.VerifyInvokeAsync(Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeAsync() to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeAsync()
			- 2: IPrimitiveDependencyService.InvokeAsync()
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowVerifyNoOtherCalls()
	{
		var fixture = CreateFixture();
		await fixture.InvokeAsync();
		fixture.InvokeWithParameter("value");

		DependencyServiceMock.VerifyInvokeAsync(Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(in String) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.InvokeWithParameter(in "value")
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task VerifyValidSequence()
	{
		var fixture = CreateFixture();
		await fixture.InvokeAsync();
		await fixture.InvokeAsync();

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeAsync();
			ctx.DependencyServiceMock.InvokeAsync();
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowInvalidSequence()
	{
		var fixture = CreateFixture();
		await fixture.InvokeAsync();
		await fixture.InvokeAsync();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeAsync();
			ctx.DependencyServiceMock.InvokeAsync();
			ctx.DependencyServiceMock.InvokeAsync();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeAsync() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeAsync()
			- 2: IPrimitiveDependencyService.InvokeAsync()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidMethodInSequence()
	{
		var fixture = CreateFixture();
		await fixture.InvokeAsync();
		await fixture.InvokeAsync();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeAsync();
			ctx.DependencyServiceMock.Return();
			ctx.DependencyServiceMock.InvokeAsync();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Return() to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeAsync()
			- 2: IPrimitiveDependencyService.InvokeAsync()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		DependencyServiceMock
			.SetupInvokeAsync()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = async () => await fixture.InvokeAsync();
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => await fixture.InvokeAsync();
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => await fixture.InvokeAsync();
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyInvokeAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeAsync()
			.Callback(() => callback++)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		await fixture.InvokeAsync();

		var actual2 = async () => await fixture.InvokeAsync();
		var exception2 = await Assert.ThrowsAsync<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = async () => await fixture.InvokeAsync();
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = async () => await fixture.InvokeAsync();
		var exception4 = await Assert.ThrowsAsync<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyInvokeAsync(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeAsync()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = async () => await fixture.InvokeAsync();
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => await fixture.InvokeAsync();
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => await fixture.InvokeAsync();
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyInvokeAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeAsync()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = async () => await fixture.InvokeAsync();
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => await fixture.InvokeAsync();
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => await fixture.InvokeAsync();
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2, callback);

		DependencyServiceMock.VerifyInvokeAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeAsync()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = async () => await fixture.InvokeAsync();
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => await fixture.InvokeAsync();
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => await fixture.InvokeAsync();
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3, callback);

		DependencyServiceMock.VerifyInvokeAsync(Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}
