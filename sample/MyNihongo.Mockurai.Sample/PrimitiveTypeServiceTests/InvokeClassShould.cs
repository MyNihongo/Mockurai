namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeClassShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		CreateFixture()
			.InvokeClass();
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		PrimitiveDependencyBaseMock.VerifyInvoke(Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => PrimitiveDependencyBaseMock.VerifyInvoke(Times.Once);

		const string errorMessage = "Expected PrimitiveDependencyBase.Invoke() to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);

		PrimitiveDependencyBaseMock
			.SetupInvoke()
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeClass();

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		var fixture = CreateFixture();
		fixture.InvokeClass();
		fixture.InvokeClass();

		PrimitiveDependencyBaseMock.VerifyInvoke(Times.Exactly(2));
		PrimitiveDependencyBaseMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		var fixture = CreateFixture();
		fixture.InvokeClass();
		fixture.InvokeClass();

		var actual = () => PrimitiveDependencyBaseMock.VerifyInvoke(Times.Once);

		const string expectedMessage =
			"""
			Expected PrimitiveDependencyBase.Invoke() to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1: PrimitiveDependencyBase.Invoke()
			- 2: PrimitiveDependencyBase.Invoke()
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		var fixture = CreateFixture();
		fixture.InvokeClass();
		fixture.InvokeWithParameterClass(123f);

		PrimitiveDependencyBaseMock.VerifyInvoke(Times.Once);

		var actual = () => PrimitiveDependencyBaseMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected PrimitiveDependencyBase.InvokeWithParameter(Single) to be verified, but the following invocations have not been verified:
			- 2: PrimitiveDependencyBase.InvokeWithParameter(123)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		var fixture = CreateFixture();
		fixture.InvokeClass();
		fixture.InvokeClass();

		VerifyInSequence(static ctx =>
		{
			ctx.PrimitiveDependencyBaseMock.Invoke();
			ctx.PrimitiveDependencyBaseMock.Invoke();
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		var fixture = CreateFixture();
		fixture.InvokeClass();
		fixture.InvokeClass();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.PrimitiveDependencyBaseMock.Invoke();
			ctx.PrimitiveDependencyBaseMock.Invoke();
			ctx.PrimitiveDependencyBaseMock.Invoke();
		});

		const string expectedMessage =
			"""
			Expected PrimitiveDependencyBase.Invoke() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: PrimitiveDependencyBase.Invoke()
			- 2: PrimitiveDependencyBase.Invoke()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		var fixture = CreateFixture();
		fixture.InvokeClass();
		fixture.InvokeClass();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.PrimitiveDependencyBaseMock.Invoke();
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(321f);
			ctx.PrimitiveDependencyBaseMock.Invoke();
		});

		const string expectedMessage =
			"""
			Expected PrimitiveDependencyBase.InvokeWithParameter(321) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: PrimitiveDependencyBase.Invoke()
			- 2: PrimitiveDependencyBase.Invoke()
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		PrimitiveDependencyBaseMock
			.SetupInvoke()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeClass();
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeClass();
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeClass();
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		PrimitiveDependencyBaseMock.VerifyInvoke(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		PrimitiveDependencyBaseMock
			.SetupInvoke()
			.Callback(() => callback++)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		fixture.InvokeClass();

		var actual2 = () => fixture.InvokeClass();
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.InvokeClass();
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture.InvokeClass();
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(1, callback);

		PrimitiveDependencyBaseMock.VerifyInvoke(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		PrimitiveDependencyBaseMock
			.SetupInvoke()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeClass();
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeClass();
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeClass();
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(1, callback);

		PrimitiveDependencyBaseMock.VerifyInvoke(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		PrimitiveDependencyBaseMock
			.SetupInvoke()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeClass();
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeClass();
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeClass();
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2, callback);

		PrimitiveDependencyBaseMock.VerifyInvoke(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		PrimitiveDependencyBaseMock
			.SetupInvoke()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeClass();
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeClass();
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeClass();
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3, callback);

		PrimitiveDependencyBaseMock.VerifyInvoke(Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}