namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeWithParameterAsyncShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public async Task ExecuteWithoutSetup()
	{
		const int paramCustomerId = 123;

		await CreateFixture()
			.InvokeWithParameterAsync(paramCustomerId);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyInvokeWithParameterAsync(It<int>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyInvokeWithParameterAsync(It<int>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.InvokeWithParameterAsync(any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowWithSetup()
	{
		const int parameter = 123;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithParameterAsync(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameterAsync(parameter);

		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowWithSetupAny()
	{
		const int parameter = 123;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithParameterAsync(It<int>.Any())
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameterAsync(parameter);

		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task ExecuteWithAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);

		const int paramCustomerId = 123,
			setupCustomerId = 321;

		DependencyServiceMock
			.SetupInvokeWithParameterAsync(setupCustomerId)
			.Throws(new InvalidOperationException(errorMessage));

		await CreateFixture()
			.InvokeWithParameterAsync(paramCustomerId);
	}

	[Fact]
	public async Task NotTreatZeroAsAny()
	{
		const string errorMessage = nameof(errorMessage);
		const int parameter = 0, anotherParameter = 1;

		DependencyServiceMock
			.SetupInvokeWithParameterAsync(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameterAsync(parameter);

		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);

		await CreateFixture()
			.InvokeWithParameterAsync(anotherParameter);
	}

	[Fact]
	public async Task TreatExactMatchesWithMorePriority()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int input = 123;

		DependencyServiceMock
			.SetupInvokeWithParameterAsync(It<int>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithParameterAsync(input)
			.Throws(new ArgumentException(errorMessage2));

		var actual = () => CreateFixture()
			.InvokeWithParameterAsync(input);

		var exception = await Assert.ThrowsAsync<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public async Task VerifyTimes()
	{
		const int parameter1 = 123, parameter2 = 321;

		var fixture = CreateFixture();
		await fixture.InvokeWithParameterAsync(parameter1);
		await fixture.InvokeWithParameterAsync(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameterAsync(parameter1, Times.Once);
		DependencyServiceMock.VerifyInvokeWithParameterAsync(parameter2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyTimesWhere()
	{
		const int parameter1 = 123, parameter2 = 321;

		var fixture = CreateFixture();
		await fixture.InvokeWithParameterAsync(parameter1);
		await fixture.InvokeWithParameterAsync(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameterAsync(It<int>.Where(x => x < 200), Times.Once);
		DependencyServiceMock.VerifyInvokeWithParameterAsync(parameter2, Times.AtMost(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyTimesAny()
	{
		const int parameter1 = 123, parameter2 = 321;

		var fixture = CreateFixture();
		await fixture.InvokeWithParameterAsync(parameter1);
		await fixture.InvokeWithParameterAsync(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameterAsync(It<int>.Any(), Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowVerifyTimes()
	{
		const int parameter1 = 123, parameter2 = 321;

		var fixture = CreateFixture();
		await fixture.InvokeWithParameterAsync(parameter1);
		await fixture.InvokeWithParameterAsync(parameter2);

		var actual = () =>
		{
			var verify = It<int>.Any();
			DependencyServiceMock.VerifyInvokeWithParameterAsync(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameterAsync(any) to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameterAsync(123)
			- 2: IPrimitiveDependencyService.InvokeWithParameterAsync(321)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowVerifyNoOtherCalls()
	{
		const int parameter1 = 123, parameter2 = 321;

		var fixture = CreateFixture();
		await fixture.InvokeWithParameterAsync(parameter1);
		await fixture.InvokeWithParameterAsync(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameterAsync(parameter1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameterAsync(Int32) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.InvokeWithParameterAsync(321)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task VerifyValidSequence()
	{
		const int parameter1 = 123, parameter2 = 321;

		var fixture = CreateFixture();
		await fixture.InvokeWithParameterAsync(parameter1);
		await fixture.InvokeWithParameterAsync(parameter2);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameterAsync(parameter1);
			ctx.DependencyServiceMock.InvokeWithParameterAsync(parameter2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyValidSequenceEquivalent()
	{
		const int parameter1 = 123, parameter2 = 321;

		var fixture = CreateFixture();
		await fixture.InvokeWithParameterAsync(parameter1);
		await fixture.InvokeWithParameterAsync(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Equivalent(parameter1), verify2 = It<int>.Equivalent(parameter2);
			ctx.DependencyServiceMock.InvokeWithParameterAsync(verify1);
			ctx.DependencyServiceMock.InvokeWithParameterAsync(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyValidSequenceAny()
	{
		const int parameter1 = 123, parameter2 = 321;

		var fixture = CreateFixture();
		await fixture.InvokeWithParameterAsync(parameter1);
		await fixture.InvokeWithParameterAsync(parameter2);

		VerifyInSequence(static ctx =>
		{
			var verify = It<int>.Any();
			ctx.DependencyServiceMock.InvokeWithParameterAsync(verify);
			ctx.DependencyServiceMock.InvokeWithParameterAsync(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyValidSequenceWhere()
	{
		const int parameter1 = 123, parameter2 = 321;

		var fixture = CreateFixture();
		await fixture.InvokeWithParameterAsync(parameter1);
		await fixture.InvokeWithParameterAsync(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Where(x => x > 300);
			ctx.DependencyServiceMock.InvokeWithParameterAsync(verify1);
			ctx.DependencyServiceMock.InvokeWithParameterAsync(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowInvalidSequence()
	{
		const int parameter1 = 123, parameter2 = 321;

		var fixture = CreateFixture();
		await fixture.InvokeWithParameterAsync(parameter1);
		await fixture.InvokeWithParameterAsync(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameterAsync(parameter2);
			ctx.DependencyServiceMock.InvokeWithParameterAsync(parameter1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameterAsync(123) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameterAsync(123)
			- 2: IPrimitiveDependencyService.InvokeWithParameterAsync(321)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidSequenceWhere()
	{
		const int parameter1 = 123, parameter2 = 321;

		var fixture = CreateFixture();
		await fixture.InvokeWithParameterAsync(parameter1);
		await fixture.InvokeWithParameterAsync(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Where(x => x > 300), verify2 = It<int>.Where(x => x > 100);
			ctx.DependencyServiceMock.InvokeWithParameterAsync(verify1);
			ctx.DependencyServiceMock.InvokeWithParameterAsync(verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameterAsync(where(predicate)) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameterAsync(123)
			- 2: IPrimitiveDependencyService.InvokeWithParameterAsync(321)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidMethodInSequence()
	{
		const int parameter1 = 123, parameter2 = 321;

		var fixture = CreateFixture();
		await fixture.InvokeWithParameterAsync(parameter1);
		await fixture.InvokeWithParameterAsync(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameterAsync(parameter1);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.InvokeWithParameterAsync(parameter2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameterAsync(123)
			- 2: IPrimitiveDependencyService.InvokeWithParameterAsync(321)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidMethodInSequenceEquivalent()
	{
		const int parameter1 = 123, parameter2 = 321;

		var fixture = CreateFixture();
		await fixture.InvokeWithParameterAsync(parameter1);
		fixture.InvokeWithSeveralParameters(123, 321);
		await fixture.InvokeWithParameterAsync(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameterAsync(parameter1);
			ctx.DependencyServiceMock.InvokeWithParameterAsync(It<int>.Equivalent(123_321));
			ctx.DependencyServiceMock.InvokeWithParameterAsync(parameter2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameterAsync(123321) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameterAsync(123)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321)
			- 3: IPrimitiveDependencyService.InvokeWithParameterAsync(321)
			  expected: 123321
			  actual: 321
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue = 2;

		DependencyServiceMock
			.SetupInvokeWithParameterAsync(It<int>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyInvokeWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue = 2;
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeWithParameterAsync(It<int>.Any())
			.Callback(x => callback += x)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		await fixture.InvokeWithParameterAsync(setValue);

		var actual2 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception2 = await Assert.ThrowsAsync<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception4 = await Assert.ThrowsAsync<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue, callback);

		DependencyServiceMock.VerifyInvokeWithParameterAsync(setValue, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue = 2;
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeWithParameterAsync(It<int>.Any())
			.Callback(x => callback += x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue, callback);

		DependencyServiceMock.VerifyInvokeWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue = 2;
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeWithParameterAsync(It<int>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x);

		var fixture = CreateFixture();

		var actual1 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * setValue, callback);

		DependencyServiceMock.VerifyInvokeWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue = 2;
		var callback = 0;

		DependencyServiceMock
			.SetupInvokeWithParameterAsync(It<int>.Any())
			.Callback(x => callback += x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x);

		var fixture = CreateFixture();

		var actual1 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => await fixture.InvokeWithParameterAsync(setValue);
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * setValue, callback);

		DependencyServiceMock.VerifyInvokeWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}
