namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class GetOnlyGenericShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		var actual = CreateFixture()
			.GetOnlyGeneric;

		Assert.Null(actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyGenericServiceMock.VerifyGetGetOnly(Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService<String>.GetOnly.get to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const string setupValue = nameof(setupValue);

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Returns(setupValue);

		var actual = CreateFixture()
			.GetOnlyGeneric;

		Assert.Equal(setupValue, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => { _ = CreateFixture().GetOnlyGeneric; };

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		var fixture = CreateFixture();
		_ = fixture.GetOnlyGeneric;
		_ = fixture.GetOnlyGeneric;

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Exactly(2));
		DependencyGenericServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		var fixture = CreateFixture();
		_ = fixture.GetOnlyGeneric;
		_ = fixture.GetOnlyGeneric;

		var actual = () => DependencyGenericServiceMock.VerifyGetGetOnly(Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.GetOnly.get to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.GetOnly.get
			- 2: IPrimitiveDependencyService<String>.GetOnly.get
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		var fixture = CreateFixture();
		_ = fixture.GetOnlyGeneric;
		fixture.ReturnWithParameterGeneric<decimal>("value");

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Once);

		var actual = () => DependencyGenericServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.ReturnWithParameter<Decimal>(String) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService<String>.ReturnWithParameter<Decimal>("value")
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		var fixture = CreateFixture();
		_ = fixture.GetOnlyGeneric;
		_ = fixture.GetOnlyGeneric;

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.GetGetOnly();
			ctx.DependencyGenericServiceMock.GetGetOnly();
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		var fixture = CreateFixture();
		_ = fixture.GetOnlyGeneric;
		_ = fixture.GetOnlyGeneric;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.GetGetOnly();
			ctx.DependencyGenericServiceMock.GetGetOnly();
			ctx.DependencyGenericServiceMock.GetGetOnly();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.GetOnly.get to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.GetOnly.get
			- 2: IPrimitiveDependencyService<String>.GetOnly.get
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		var fixture = CreateFixture();
		_ = fixture.GetOnlyGeneric;
		_ = fixture.GetOnlyGeneric;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.GetGetOnly();
			ctx.DependencyGenericServiceMock.GetGetOnly();
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, int>(123, "some value");
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<Int32>(123, "some value") to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.GetOnly.get
			- 2: IPrimitiveDependencyService<String>.GetOnly.get
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnDifferentValues()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.GetOnlyGeneric);
		Assert.Equal(setupValue2, fixture.GetOnlyGeneric);
		Assert.Equal(setupValue2, fixture.GetOnlyGeneric);

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback1()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Callback(() => callback++)
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Null(fixture.GetOnlyGeneric);
		Assert.Equal(setupValue1, fixture.GetOnlyGeneric);
		Assert.Equal(setupValue2, fixture.GetOnlyGeneric);
		Assert.Equal(setupValue2, fixture.GetOnlyGeneric);
		Assert.Equal(1, callback);

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback2()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Callback(() => callback++).And().Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.GetOnlyGeneric);
		Assert.Equal(setupValue2, fixture.GetOnlyGeneric);
		Assert.Equal(setupValue2, fixture.GetOnlyGeneric);
		Assert.Equal(1, callback);

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback3()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Returns(setupValue1)
			.Returns(setupValue2).And().Callback(() => callback++);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.GetOnlyGeneric);
		Assert.Equal(setupValue2, fixture.GetOnlyGeneric);
		Assert.Equal(setupValue2, fixture.GetOnlyGeneric);
		Assert.Equal(2, callback);

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback4()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Callback(() => callback++).And().Returns(setupValue1)
			.Returns(setupValue2).And().Callback(() => callback++);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.GetOnlyGeneric);
		Assert.Equal(setupValue2, fixture.GetOnlyGeneric);
		Assert.Equal(setupValue2, fixture.GetOnlyGeneric);
		Assert.Equal(3, callback);

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.GetOnlyGeneric; };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.GetOnlyGeneric; };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.GetOnlyGeneric; };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Callback(() => callback++)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		Assert.Null(fixture.GetOnlyGeneric);

		var actual2 = () => { _ = fixture.GetOnlyGeneric; };
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => { _ = fixture.GetOnlyGeneric; };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => { _ = fixture.GetOnlyGeneric; };
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(1, callback);

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.GetOnlyGeneric; };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.GetOnlyGeneric; };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.GetOnlyGeneric; };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(1, callback);

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.GetOnlyGeneric; };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.GetOnlyGeneric; };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.GetOnlyGeneric; };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2, callback);

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.GetOnlyGeneric; };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.GetOnlyGeneric; };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.GetOnlyGeneric; };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3, callback);

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage);
		const string setupValue = nameof(setupValue);

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Throws(new COMException(errorMessage))
			.Returns(setupValue);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.GetOnlyGeneric; };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		Assert.Equal(setupValue, fixture.GetOnlyGeneric);
		Assert.Equal(setupValue, fixture.GetOnlyGeneric);

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage);
		const string setupValue = nameof(setupValue);

		DependencyGenericServiceMock
			.SetupGetGetOnly()
			.Returns(setupValue)
			.Throws(new COMException(errorMessage));

		var fixture = CreateFixture();

		Assert.Equal(setupValue, fixture.GetOnlyGeneric);

		var actual2 = () => { _ = fixture.GetOnlyGeneric; };
		var exception1 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage, exception1.Message);

		var actual3 = () => { _ = fixture.GetOnlyGeneric; };
		var exception3 = Assert.Throws<COMException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		DependencyGenericServiceMock.VerifyGetGetOnly(Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}