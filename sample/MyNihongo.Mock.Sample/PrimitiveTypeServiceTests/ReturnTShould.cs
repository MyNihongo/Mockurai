namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnTShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const float expected = 0f;

		var actual = CreateFixture()
			.Return<float>();

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyReturn<float>(Times.Never);
		DependencyServiceMock.VerifyReturn<string>(Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyReturn<float>(Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.Return<Single>() to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const float setupCount = 5f;

		DependencyServiceMock
			.SetupReturn<float>()
			.Returns(setupCount);

		var actual = CreateFixture()
			.Return<float>();

		Assert.Equal(setupCount, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturn<float>()
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.Return<float>();

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		var fixture = CreateFixture();
		fixture.Return<float>();
		fixture.Return<float>();

		DependencyServiceMock.VerifyReturn<float>(Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		var fixture = CreateFixture();
		fixture.Return<float>();
		fixture.Return<float>();

		var actual = () => DependencyServiceMock.VerifyReturn<float>(Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Return<Single>() to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Return<Single>()
			- 2: IPrimitiveDependencyService.Return<Single>()
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		var fixture = CreateFixture();
		fixture.Return<float>();
		fixture.ReturnWithParameter("value");

		DependencyServiceMock.VerifyReturn<float>(Times.Once);

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
	public void VerifyValidSequence()
	{
		var fixture = CreateFixture();
		fixture.Return<float>();
		fixture.Return<float>();

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Return<float>();
			ctx.DependencyServiceMock.Return<float>();
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		var fixture = CreateFixture();
		fixture.Return<float>();
		fixture.Return<float>();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Return<float>();
			ctx.DependencyServiceMock.Return<float>();
			ctx.DependencyServiceMock.Return<float>();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Return<Single>() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Return<Single>()
			- 2: IPrimitiveDependencyService.Return<Single>()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		var fixture = CreateFixture();
		fixture.Return<float>();
		fixture.Return<float>();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Return<float>();
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.Return<float>();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.Return<Single>()
			- 2: IPrimitiveDependencyService.Return<Single>()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnDifferentValues()
	{
		const int setupValue1 = 123, setupValue2 = 321;

		DependencyServiceMock
			.SetupReturn<float>()
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.Return<float>());
		Assert.Equal(setupValue2, fixture.Return<float>());
		Assert.Equal(setupValue2, fixture.Return<float>());

		DependencyServiceMock.VerifyReturn<float>(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback1()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		var callback = 0;

		DependencyServiceMock
			.SetupReturn<float>()
			.Callback(() => callback++)
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(0, fixture.Return<float>());
		Assert.Equal(setupValue1, fixture.Return<float>());
		Assert.Equal(setupValue2, fixture.Return<float>());
		Assert.Equal(setupValue2, fixture.Return<float>());
		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyReturn<float>(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback2()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		var callback = 0;

		DependencyServiceMock
			.SetupReturn<float>()
			.Callback(() => callback++).And().Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.Return<float>());
		Assert.Equal(setupValue2, fixture.Return<float>());
		Assert.Equal(setupValue2, fixture.Return<float>());
		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyReturn<float>(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback3()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		var callback = 0;

		DependencyServiceMock
			.SetupReturn<float>()
			.Returns(setupValue1)
			.Returns(setupValue2).And().Callback(() => callback++);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.Return<float>());
		Assert.Equal(setupValue2, fixture.Return<float>());
		Assert.Equal(setupValue2, fixture.Return<float>());
		Assert.Equal(2, callback);

		DependencyServiceMock.VerifyReturn<float>(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback4()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		var callback = 0;

		DependencyServiceMock
			.SetupReturn<float>()
			.Callback(() => callback++).And().Returns(setupValue1)
			.Returns(setupValue2).And().Callback(() => callback++);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.Return<float>());
		Assert.Equal(setupValue2, fixture.Return<float>());
		Assert.Equal(setupValue2, fixture.Return<float>());
		Assert.Equal(3, callback);

		DependencyServiceMock.VerifyReturn<float>(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		DependencyServiceMock
			.SetupReturn<float>()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.Return<float>(); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.Return<float>(); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.Return<float>(); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyReturn<float>(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupReturn<float>()
			.Callback(() => callback++)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		Assert.Equal(0, fixture.Return<float>());

		var actual2 = () => { _ = fixture.Return<float>(); };
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => { _ = fixture.Return<float>(); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => { _ = fixture.Return<float>(); };
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyReturn<float>(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupReturn<float>()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.Return<float>(); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.Return<float>(); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.Return<float>(); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyReturn<float>(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupReturn<float>()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.Return<float>(); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.Return<float>(); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.Return<float>(); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2, callback);

		DependencyServiceMock.VerifyReturn<float>(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupReturn<float>()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.Return<float>(); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.Return<float>(); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.Return<float>(); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3, callback);

		DependencyServiceMock.VerifyReturn<float>(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue = 123;

		DependencyServiceMock
			.SetupReturn<float>()
			.Throws(new COMException(errorMessage))
			.Returns(setupValue);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.Return<float>(); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		Assert.Equal(setupValue, fixture.Return<float>());
		Assert.Equal(setupValue, fixture.Return<float>());

		DependencyServiceMock.VerifyReturn<float>(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue = 123;

		DependencyServiceMock
			.SetupReturn<float>()
			.Returns(setupValue)
			.Throws(new COMException(errorMessage));

		var fixture = CreateFixture();

		Assert.Equal(setupValue, fixture.Return<float>());

		var actual2 = () => { _ = fixture.Return<float>(); };
		var exception1 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage, exception1.Message);

		var actual3 = () => { _ = fixture.Return<float>(); };
		var exception3 = Assert.Throws<COMException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		DependencyServiceMock.VerifyReturn<float>(Times.Exactly(3));
		VerifyNoOtherCalls();
	}
	
	[Fact]
	public void SetupDifferentTypes()
	{
		const string errorMessage = nameof(errorMessage);
		const float setupValue = 123f;
		var callback = 0;

		DependencyServiceMock
			.SetupReturn<float>()
			.Returns(setupValue);

		DependencyServiceMock
			.SetupReturn<string>()
			.Throws(new OutOfMemoryException(errorMessage));

		DependencyServiceMock
			.SetupReturn<short>()
			.Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = fixture.Return<float>();
		Assert.Equal(setupValue, actual1);

		var actual2 = () => fixture.Return<string>();
		var exception = Assert.Throws<OutOfMemoryException>(actual2);
		Assert.Equal(errorMessage, exception.Message);

		fixture.Return<short>();
		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}
	
	[Fact]
	public void VerifyDifferentTypes()
	{
		var fixture = CreateFixture();
		fixture.Return<string>();
		fixture.Return<decimal>();
		fixture.Return<float>();
		fixture.Return<string>();

		DependencyServiceMock.VerifyReturn<string>(Times.Exactly(2));
		DependencyServiceMock.VerifyReturn<decimal>(Times.Once);
		DependencyServiceMock.VerifyReturn<float>(Times.Once);
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyDifferentTypesInSequence()
	{
		var fixture = CreateFixture();
		fixture.Return<string>();
		fixture.Return<decimal>();
		fixture.Return<float>();

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Return<string>();
			ctx.DependencyServiceMock.Return<decimal>();
			ctx.DependencyServiceMock.Return<float>();
		});
		VerifyNoOtherCalls();
	}
}
