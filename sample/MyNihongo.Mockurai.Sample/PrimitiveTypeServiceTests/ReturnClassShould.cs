namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnClassShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const int expected = 0;

		var actual = CreateFixture()
			.ReturnClass();

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		PrimitiveDependencyMock.VerifyReturn(Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => PrimitiveDependencyMock.VerifyReturn(Times.Once);

		const string errorMessage = "Expected PrimitiveDependency.Return() to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const int setupCount = 5;

		PrimitiveDependencyMock
			.SetupReturn()
			.Returns(setupCount);

		var actual = CreateFixture()
			.ReturnClass();

		Assert.Equal(setupCount, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);

		PrimitiveDependencyMock
			.SetupReturn()
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnClass();

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		var fixture = CreateFixture();
		fixture.ReturnClass();
		fixture.ReturnClass();

		PrimitiveDependencyMock.VerifyReturn(Times.Exactly(2));
		PrimitiveDependencyMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		var fixture = CreateFixture();
		fixture.ReturnClass();
		fixture.ReturnClass();

		var actual = () => PrimitiveDependencyMock.VerifyReturn(Times.Once);

		const string expectedMessage =
			"""
			Expected PrimitiveDependency.Return() to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1: PrimitiveDependency.Return()
			- 2: PrimitiveDependency.Return()
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		var fixture = CreateFixture();
		fixture.ReturnClass();

		var actual = () => PrimitiveDependencyMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected PrimitiveDependency.Return() to be verified, but the following invocations have not been verified:
			- 1: PrimitiveDependency.Return()
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		var fixture = CreateFixture();
		fixture.ReturnClass();
		fixture.ReturnClass();

		VerifyInSequence(static ctx =>
		{
			ctx.PrimitiveDependencyMock.Return();
			ctx.PrimitiveDependencyMock.Return();
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		var fixture = CreateFixture();
		fixture.ReturnClass();
		fixture.ReturnClass();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.PrimitiveDependencyMock.Return();
			ctx.PrimitiveDependencyMock.Return();
			ctx.PrimitiveDependencyMock.Return();
		});

		const string expectedMessage =
			"""
			Expected PrimitiveDependency.Return() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: PrimitiveDependency.Return()
			- 2: PrimitiveDependency.Return()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		var fixture = CreateFixture();
		fixture.ReturnClass();
		fixture.ReturnClass();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.PrimitiveDependencyMock.Return();
			ctx.PrimitiveDependencyMock.Return();
			ctx.PrimitiveDependencyMock.Return();
		});

		const string expectedMessage =
			"""
			Expected PrimitiveDependency.Return() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: PrimitiveDependency.Return()
			- 2: PrimitiveDependency.Return()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnDifferentValues()
	{
		const int setupValue1 = 123, setupValue2 = 321;

		PrimitiveDependencyMock
			.SetupReturn()
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnClass());
		Assert.Equal(setupValue2, fixture.ReturnClass());
		Assert.Equal(setupValue2, fixture.ReturnClass());

		PrimitiveDependencyMock.VerifyReturn(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback1()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		var callback = 0;

		PrimitiveDependencyMock
			.SetupReturn()
			.Callback(() => callback++)
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(0, fixture.ReturnClass());
		Assert.Equal(setupValue1, fixture.ReturnClass());
		Assert.Equal(setupValue2, fixture.ReturnClass());
		Assert.Equal(setupValue2, fixture.ReturnClass());
		Assert.Equal(1, callback);

		PrimitiveDependencyMock.VerifyReturn(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback2()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		var callback = 0;

		PrimitiveDependencyMock
			.SetupReturn()
			.Callback(() => callback++).And().Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnClass());
		Assert.Equal(setupValue2, fixture.ReturnClass());
		Assert.Equal(setupValue2, fixture.ReturnClass());
		Assert.Equal(1, callback);

		PrimitiveDependencyMock.VerifyReturn(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback3()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		var callback = 0;

		PrimitiveDependencyMock
			.SetupReturn()
			.Returns(setupValue1)
			.Returns(setupValue2).And().Callback(() => callback++);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnClass());
		Assert.Equal(setupValue2, fixture.ReturnClass());
		Assert.Equal(setupValue2, fixture.ReturnClass());
		Assert.Equal(2, callback);

		PrimitiveDependencyMock.VerifyReturn(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback4()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		var callback = 0;

		PrimitiveDependencyMock
			.SetupReturn()
			.Callback(() => callback++).And().Returns(setupValue1)
			.Returns(setupValue2).And().Callback(() => callback++);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnClass());
		Assert.Equal(setupValue2, fixture.ReturnClass());
		Assert.Equal(setupValue2, fixture.ReturnClass());
		Assert.Equal(3, callback);

		PrimitiveDependencyMock.VerifyReturn(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		PrimitiveDependencyMock
			.SetupReturn()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnClass(); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnClass(); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnClass(); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		PrimitiveDependencyMock.VerifyReturn(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		PrimitiveDependencyMock
			.SetupReturn()
			.Callback(() => callback++)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		Assert.Equal(0, fixture.ReturnClass());

		var actual2 = () => { _ = fixture.ReturnClass(); };
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnClass(); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => { _ = fixture.ReturnClass(); };
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(1, callback);

		PrimitiveDependencyMock.VerifyReturn(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		PrimitiveDependencyMock
			.SetupReturn()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnClass(); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnClass(); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnClass(); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(1, callback);

		PrimitiveDependencyMock.VerifyReturn(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		PrimitiveDependencyMock
			.SetupReturn()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnClass(); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnClass(); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnClass(); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2, callback);

		PrimitiveDependencyMock.VerifyReturn(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		PrimitiveDependencyMock
			.SetupReturn()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnClass(); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnClass(); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnClass(); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3, callback);

		PrimitiveDependencyMock.VerifyReturn(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue = 123;

		PrimitiveDependencyMock
			.SetupReturn()
			.Throws(new COMException(errorMessage))
			.Returns(setupValue);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnClass(); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		Assert.Equal(setupValue, fixture.ReturnClass());
		Assert.Equal(setupValue, fixture.ReturnClass());

		PrimitiveDependencyMock.VerifyReturn(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue = 123;

		PrimitiveDependencyMock
			.SetupReturn()
			.Returns(setupValue)
			.Throws(new COMException(errorMessage));

		var fixture = CreateFixture();

		Assert.Equal(setupValue, fixture.ReturnClass());

		var actual2 = () => { _ = fixture.ReturnClass(); };
		var exception1 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage, exception1.Message);

		var actual3 = () => { _ = fixture.ReturnClass(); };
		var exception3 = Assert.Throws<COMException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		PrimitiveDependencyMock.VerifyReturn(Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}