namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnWithParameterAsyncShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public async Task ReturnValueWithoutSetup()
	{
		const string paramCustomerId = "qOp5SNt2LVqzQ9qS";
		const int expected = 0;

		var actual = await CreateFixture()
			.ReturnWithParameterAsync(paramCustomerId);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyReturnWithParameterAsync(It<string>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyReturnWithParameterAsync(It<string>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.ReturnWithParameterAsync(any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task ReturnValueWithSetup()
	{
		const string parameter = "ZFJ2XHcBRAuyJZJX";
		const int nameSetup = 123;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(parameter)
			.Returns(nameSetup);

		var actual = await CreateFixture()
			.ReturnWithParameterAsync(parameter);

		Assert.Equal(nameSetup, actual);
	}

	[Fact]
	public async Task ReturnValueWithSetupAny()
	{
		const string parameter = "ZFJ2XHcBRAuyJZJX";
		const int nameSetup = 123;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(It<string>.Any())
			.Returns(nameSetup);

		var actual = await CreateFixture()
			.ReturnWithParameterAsync(parameter);

		Assert.Equal(nameSetup, actual);
	}

	[Fact]
	public async Task ReturnValueWithAnotherSetup()
	{
		const string paramCustomerId = "ZFJ2XHcBRAuyJZJX",
			setupCustomerId = "another ID";

		const int nameSetup = 123, expected = 0;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(setupCustomerId)
			.Returns(nameSetup);

		var actual = await CreateFixture()
			.ReturnWithParameterAsync(paramCustomerId);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task ThrowWithSetup()
	{
		const string parameter = "ZFJ2XHcBRAuyJZJX",
			errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithParameterAsync(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = async () => await CreateFixture()
			.ReturnWithParameterAsync(parameter);

		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public async Task ReturnValueForThrowsWithAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);

		const string paramCustomerId = "ZFJ2XHcBRAuyJZJX",
			setupCustomerId = "another ID";
		const int expected = 0;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(setupCustomerId)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = await CreateFixture()
			.ReturnWithParameterAsync(paramCustomerId);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public async Task VerifyTimes()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		await fixture.ReturnWithParameterAsync(parameter1);
		await fixture.ReturnWithParameterAsync(parameter2);

		DependencyServiceMock.VerifyReturnWithParameterAsync(parameter1, Times.Once);
		DependencyServiceMock.VerifyReturnWithParameterAsync(parameter2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyTimesWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		await fixture.ReturnWithParameterAsync(parameter1);
		await fixture.ReturnWithParameterAsync(parameter2);

		DependencyServiceMock.VerifyReturnWithParameterAsync(It<string>.Where(x => x.EndsWith('1')), Times.Once);
		DependencyServiceMock.VerifyReturnWithParameterAsync(parameter2, Times.AtMost(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyTimesAny()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		await fixture.ReturnWithParameterAsync(parameter1);
		await fixture.ReturnWithParameterAsync(parameter2);

		DependencyServiceMock.VerifyReturnWithParameterAsync(It<string>.Any(), Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowVerifyTimes()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		await fixture.ReturnWithParameterAsync(parameter1);
		await fixture.ReturnWithParameterAsync(parameter2);

		var actual = () =>
		{
			var verify = It<string>.Any();
			DependencyServiceMock.VerifyReturnWithParameterAsync(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameterAsync(any) to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithParameterAsync("parameter1")
			- 2: IPrimitiveDependencyService.ReturnWithParameterAsync("parameter2")
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowVerifyNoOtherCalls()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		await fixture.ReturnWithParameterAsync(parameter1);
		await fixture.ReturnWithParameterAsync(parameter2);

		DependencyServiceMock.VerifyReturnWithParameterAsync(parameter1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameterAsync(String) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.ReturnWithParameterAsync("parameter2")
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task VerifyValidSequence()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		await fixture.ReturnWithParameterAsync(parameter1);
		await fixture.ReturnWithParameterAsync(parameter2);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithParameterAsync(parameter1);
			ctx.DependencyServiceMock.ReturnWithParameterAsync(parameter2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyValidSequenceEquivalent()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		await fixture.ReturnWithParameterAsync(parameter1);
		await fixture.ReturnWithParameterAsync(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Equivalent(parameter1), verify2 = It<string>.Equivalent(parameter2);
			ctx.DependencyServiceMock.ReturnWithParameterAsync(verify1);
			ctx.DependencyServiceMock.ReturnWithParameterAsync(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyValidSequenceAny()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		await fixture.ReturnWithParameterAsync(parameter1);
		await fixture.ReturnWithParameterAsync(parameter2);

		VerifyInSequence(static ctx =>
		{
			var verify = It<string>.Any();
			ctx.DependencyServiceMock.ReturnWithParameterAsync(verify);
			ctx.DependencyServiceMock.ReturnWithParameterAsync(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task VerifyValidSequenceWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		await fixture.ReturnWithParameterAsync(parameter1);
		await fixture.ReturnWithParameterAsync(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.StartsWith("pa")), verify2 = It<string>.Where(x => x.EndsWith("r2"));
			ctx.DependencyServiceMock.ReturnWithParameterAsync(verify1);
			ctx.DependencyServiceMock.ReturnWithParameterAsync(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowInvalidSequence()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		await fixture.ReturnWithParameterAsync(parameter1);
		await fixture.ReturnWithParameterAsync(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithParameterAsync(parameter2);
			ctx.DependencyServiceMock.ReturnWithParameterAsync(parameter1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameterAsync("parameter1") to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithParameterAsync("parameter1")
			- 2: IPrimitiveDependencyService.ReturnWithParameterAsync("parameter2")
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidSequenceWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		await fixture.ReturnWithParameterAsync(parameter1);
		await fixture.ReturnWithParameterAsync(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.EndsWith("r2")), verify2 = It<string>.Where(x => x.StartsWith("pa"));
			ctx.DependencyServiceMock.ReturnWithParameterAsync(verify1);
			ctx.DependencyServiceMock.ReturnWithParameterAsync(verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameterAsync(where(predicate)) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithParameterAsync("parameter1")
			- 2: IPrimitiveDependencyService.ReturnWithParameterAsync("parameter2")
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidMethodInSequence()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		await fixture.ReturnWithParameterAsync(parameter1);
		await fixture.ReturnWithParameterAsync(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithParameterAsync(parameter1);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.ReturnWithParameterAsync(parameter2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithParameterAsync("parameter1")
			- 2: IPrimitiveDependencyService.ReturnWithParameterAsync("parameter2")
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ReturnDifferentValues()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const string setValue = nameof(setValue);

		DependencyServiceMock
			.SetupReturnWithParameterAsync(It<string>.Any())
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setupValue2, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setupValue2, await fixture.ReturnWithParameterAsync(setValue));

		DependencyServiceMock.VerifyReturnWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnDifferentValuesWithCallback1()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(It<string>.Any())
			.Callback(x => callback += x.Length)
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(0, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setupValue1, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setupValue2, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setupValue2, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setValue.Length, callback);

		DependencyServiceMock.VerifyReturnWithParameterAsync(setValue, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnDifferentValuesWithCallback2()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(It<string>.Any())
			.Callback(x => callback += x.Length).And().Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setupValue2, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setupValue2, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setValue.Length, callback);

		DependencyServiceMock.VerifyReturnWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnDifferentValuesWithCallback3()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(It<string>.Any())
			.Returns(setupValue1)
			.Returns(setupValue2).And().Callback(x => callback += x.Length);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setupValue2, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setupValue2, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(2 * setValue.Length, callback);

		DependencyServiceMock.VerifyReturnWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnDifferentValuesWithCallback4()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(It<string>.Any())
			.Callback(x => callback += x.Length).And().Returns(setupValue1)
			.Returns(setupValue2).And().Callback(x => callback += x.Length);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setupValue2, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setupValue2, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(3 * setValue.Length, callback);

		DependencyServiceMock.VerifyReturnWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);

		DependencyServiceMock
			.SetupReturnWithParameterAsync(It<string>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyReturnWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(It<string>.Any())
			.Callback(x => callback += x.Length)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		Assert.Equal(0, await fixture.ReturnWithParameterAsync(setValue));

		var actual2 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception2 = await Assert.ThrowsAsync<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception4 = await Assert.ThrowsAsync<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue.Length, callback);

		DependencyServiceMock.VerifyReturnWithParameterAsync(setValue, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(It<string>.Any())
			.Callback(x => callback += x.Length).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue.Length, callback);

		DependencyServiceMock.VerifyReturnWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(It<string>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x.Length);

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * setValue.Length, callback);

		DependencyServiceMock.VerifyReturnWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(It<string>.Any())
			.Callback(x => callback += x.Length).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x.Length);

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception2 = await Assert.ThrowsAsync<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception3 = await Assert.ThrowsAsync<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * setValue.Length, callback);

		DependencyServiceMock.VerifyReturnWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage), setValue = nameof(setValue);
		const int setupValue = 123;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(It<string>.Any())
			.Throws(new COMException(errorMessage))
			.Returns(setupValue);

		var fixture = CreateFixture();

		var actual1 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		Assert.Equal(setupValue, await fixture.ReturnWithParameterAsync(setValue));
		Assert.Equal(setupValue, await fixture.ReturnWithParameterAsync(setValue));

		DependencyServiceMock.VerifyReturnWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public async Task ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage), setValue = nameof(setValue);
		const int setupValue = 123;

		DependencyServiceMock
			.SetupReturnWithParameterAsync(It<string>.Any())
			.Returns(setupValue)
			.Throws(new COMException(errorMessage));

		var fixture = CreateFixture();

		Assert.Equal(setupValue, await fixture.ReturnWithParameterAsync(setValue));

		var actual2 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception1 = await Assert.ThrowsAsync<COMException>(actual2);
		Assert.Equal(errorMessage, exception1.Message);

		var actual3 = async () => { _ = await fixture.ReturnWithParameterAsync(setValue); };
		var exception3 = await Assert.ThrowsAsync<COMException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		DependencyServiceMock.VerifyReturnWithParameterAsync(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}
