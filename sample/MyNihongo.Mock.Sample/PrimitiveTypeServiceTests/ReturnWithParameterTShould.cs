namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnWithParameterTShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const float setupValue = 123f;

		var actual = CreateFixture()
			.ReturnWithParameter<float, decimal>(setupValue);

		Assert.Equal(0m, actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyReturnWithParameter<string, string>(It<string>.Any(), Times.Never);
		DependencyServiceMock.VerifyReturnWithParameter<string, int>(It<string>.Any(), Times.Never);
		DependencyServiceMock.VerifyReturnWithParameter<int, string>(It<int>.Any(), Times.Never);
		DependencyServiceMock.VerifyReturnWithParameter<int, int>(It<int>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyReturnWithParameter<float, int>(It<float>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.ReturnWithParameter<Single, Int32>(any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const float setupValue = 123f;
		const string expected = "Okayama Issei";

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>(setupValue)
			.Returns(expected);

		var actual = CreateFixture()
			.ReturnWithParameter<float, string>(setupValue);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithSetupAny()
	{
		const float setupValue = 123f;
		const string expected = "Okayama Issei";

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>(It<float>.Any())
			.Returns(expected);

		var actual = CreateFixture()
			.ReturnWithParameter<float, string>(setupValue);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithAnotherSetup()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;
		const string expected = "Okayama Issei";

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>(setupValue1)
			.Returns(expected);

		var actual = CreateFixture()
			.ReturnWithParameter<float, string>(setupValue2);

		Assert.Null(actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const float setupValue = 123f;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>(setupValue)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithParameter<float, string>(setupValue);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueForThrowsWithAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const float setupValue1 = 123f, setupValue2 = 321f;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>(setupValue1)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithParameter<float, string>(setupValue2);

		Assert.Null(actual);
	}

	[Fact]
	public void VerifyTimes()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(setupValue1);
		fixture.ReturnWithParameter<float, string>(setupValue2);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setupValue1, Times.Once);
		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setupValue2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;
		var verify1 = It<float>.Where(x => x < 200f);

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(setupValue1);
		fixture.ReturnWithParameter<float, string>(setupValue2);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(verify1, Times.Once);
		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setupValue2, Times.AtMost(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;
		var verify = It<float>.Any();

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(setupValue1);
		fixture.ReturnWithParameter<float, string>(setupValue2);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(verify, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(setupValue1);
		fixture.ReturnWithParameter<float, string>(setupValue2);

		var actual = () =>
		{
			var verify = It<float>.Any();
			DependencyServiceMock.VerifyReturnWithParameter<float, string>(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter<Single, String>(any) to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 2: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(321)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(setupValue1);
		fixture.ReturnWithParameter<float, string>(setupValue2);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setupValue1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter<Single, String>(Single) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(321)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(setupValue1);
		fixture.ReturnWithParameter<float, string>(setupValue2);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(setupValue1);
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(setupValue2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(setupValue1);
		fixture.ReturnWithParameter<float, string>(setupValue2);

		VerifyInSequence(static ctx =>
		{
			It<float> verify1 = It<float>.Equivalent(setupValue1), verify2 = It<float>.Equivalent(setupValue2);
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(verify1);
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(setupValue1);
		fixture.ReturnWithParameter<float, string>(setupValue2);

		VerifyInSequence(static ctx =>
		{
			var verify = It<float>.Any();
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(verify);
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(setupValue1);
		fixture.ReturnWithParameter<float, string>(setupValue2);

		VerifyInSequence(static ctx =>
		{
			It<float> verify1 = It<float>.Where(x => x > 100f), verify2 = It<float>.Where(x => x > 300f);
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(verify1);
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(setupValue1);
		fixture.ReturnWithParameter<float, string>(setupValue2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(setupValue2);
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(setupValue1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 2: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(321)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhere()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(setupValue1);
		fixture.ReturnWithParameter<float, string>(setupValue2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<float> verify1 = It<float>.Where(x => x > 300f), verify2 = It<float>.Where(x => x > 100f);
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(verify1);
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter<Single, String>(where(predicate)) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 2: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(321)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(setupValue1);
		fixture.ReturnWithParameter<float, string>(setupValue2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(setupValue1);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(setupValue2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 2: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(321)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnDifferentValues()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		const float setValue = 123f;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>()
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameter<float, string>(setValue));

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback1()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		const float setValue = 123f;
		float callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>()
			.Callback(x => callback += x)
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Null(fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setupValue1, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setValue, callback);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setValue, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback2()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		const float setValue = 123f;
		float callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>()
			.Callback(x => callback += x).And().Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setValue, callback);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback3()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		const float setValue = 123f;
		float callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>()
			.Returns(setupValue1)
			.Returns(setupValue2).And().Callback(x => callback += x);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(2 * setValue, callback);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback4()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		const float setValue = 123f;
		float callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>()
			.Callback(x => callback += x).And().Returns(setupValue1)
			.Returns(setupValue2).And().Callback(x => callback += x);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(3 * setValue, callback);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue = 123f;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue = 123f;
		float callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>()
			.Callback(x => callback += x)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		Assert.Null(fixture.ReturnWithParameter<float, string>(setValue));

		var actual2 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue, callback);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setValue, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue = 123f;
		float callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>()
			.Callback(x => callback += x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue, callback);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue = 123f;
		float callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * setValue, callback);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue = 123f;
		float callback = 0;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>()
			.Callback(x => callback += x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * setValue, callback);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage), setupValue = nameof(setupValue);
		const float setValue = 123f;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>()
			.Throws(new COMException(errorMessage))
			.Returns(setupValue);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		Assert.Equal(setupValue, fixture.ReturnWithParameter<float, string>(setValue));
		Assert.Equal(setupValue, fixture.ReturnWithParameter<float, string>(setValue));

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage), setupValue = nameof(setupValue);
		const float setValue = 123f;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>()
			.Returns(setupValue)
			.Throws(new COMException(errorMessage));

		var fixture = CreateFixture();

		Assert.Equal(setupValue, fixture.ReturnWithParameter<float, string>(setValue));

		var actual2 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception1 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage, exception1.Message);

		var actual3 = () => { _ = fixture.ReturnWithParameter<float, string>(setValue); };
		var exception3 = Assert.Throws<COMException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void SetupDifferentTypes()
	{
		const string errorMessage = nameof(errorMessage), setupValue1 = nameof(setupValue1);
		const float setupValue2 = 123f;
		var callback = 0f;

		DependencyServiceMock
			.SetupReturnWithParameter<float, string>()
			.Returns(setupValue1);

		DependencyServiceMock
			.SetupReturnWithParameter<string, float>()
			.Returns(setupValue2);

		DependencyServiceMock
			.SetupReturnWithParameter<string, string>()
			.Throws(new OutOfMemoryException(errorMessage));

		DependencyServiceMock
			.SetupReturnWithParameter<float, float>()
			.Callback(x => callback += x);

		var fixture = CreateFixture();

		var actual1 = fixture.ReturnWithParameter<float, string>(setupValue2);
		Assert.Equal(setupValue1, actual1);

		var actual2 = fixture.ReturnWithParameter<string, float>(setupValue1);
		Assert.Equal(setupValue2, actual2);

		var actual3 = () => fixture.ReturnWithParameter<string, string>(setupValue1);
		var exception = Assert.Throws<OutOfMemoryException>(actual3);
		Assert.Equal(errorMessage, exception.Message);

		fixture.ReturnWithParameter<float, float>(setupValue2);
		Assert.Equal(setupValue2, callback);
	}

	[Fact]
	public void VerifyDifferentTypes()
	{
		const float floatParameter1 = 123f, floatParameter2 = 321f;
		const string stringParameter = nameof(stringParameter);

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(floatParameter1);
		fixture.ReturnWithParameter<float, string>(floatParameter2);
		fixture.ReturnWithParameter<string, float>(stringParameter);
		fixture.ReturnWithParameter<float, float>(floatParameter1);
		fixture.ReturnWithParameter<string, string>(stringParameter);
		fixture.ReturnWithParameter<float, string>(floatParameter1);

		DependencyServiceMock.VerifyReturnWithParameter<float, string>(floatParameter1, Times.Exactly(2));
		DependencyServiceMock.VerifyReturnWithParameter<float, string>(floatParameter2, Times.Once);
		DependencyServiceMock.VerifyReturnWithParameter<float, float>(floatParameter1, Times.Once);
		DependencyServiceMock.VerifyReturnWithParameter<string, float>(stringParameter, Times.Once);
		DependencyServiceMock.VerifyReturnWithParameter<string, string>(stringParameter, Times.Once);
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyDifferentTypesInSequence()
	{
		const float floatParameter1 = 123f, floatParameter2 = 321f;
		const string stringParameter = nameof(stringParameter);

		var fixture = CreateFixture();
		fixture.ReturnWithParameter<float, string>(floatParameter1);
		fixture.ReturnWithParameter<float, string>(floatParameter2);
		fixture.ReturnWithParameter<string, float>(stringParameter);
		fixture.ReturnWithParameter<float, float>(floatParameter1);
		fixture.ReturnWithParameter<string, string>(stringParameter);
		fixture.ReturnWithParameter<float, string>(floatParameter1);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(floatParameter1);
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(floatParameter2);
			ctx.DependencyServiceMock.ReturnWithParameter<string, float>(stringParameter);
			ctx.DependencyServiceMock.ReturnWithParameter<float, float>(floatParameter1);
			ctx.DependencyServiceMock.ReturnWithParameter<string, string>(stringParameter);
			ctx.DependencyServiceMock.ReturnWithParameter<float, string>(floatParameter1);
		});
		VerifyNoOtherCalls();
	}
}
