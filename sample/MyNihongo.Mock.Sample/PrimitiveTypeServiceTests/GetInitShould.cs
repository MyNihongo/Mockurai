namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class GetInitShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		var actual = CreateFixture()
			.GetInit;

		Assert.Null(actual);
	}

	[Fact]
	public void VerifyIfNotCalledGet()
	{
		DependencyServiceMock.VerifyGetGetInit(Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalledGet()
	{
		var actual = () => DependencyServiceMock.VerifyGetGetInit(Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.GetInit.get to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const string setupValue = nameof(setupValue);

		DependencyServiceMock
			.SetupGetGetInit()
			.Returns(setupValue);

		var actual = CreateFixture()
			.GetInit;

		Assert.Equal(setupValue, actual);
	}

	[Fact]
	public void ThrowWithSetupGet()
	{
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupGetGetInit()
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => { _ = CreateFixture().GetInit; };

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void VerifyTimesGet()
	{
		var fixture = CreateFixture();
		_ = fixture.GetInit;
		_ = fixture.GetInit;

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimesGet()
	{
		var fixture = CreateFixture();
		_ = fixture.GetInit;
		_ = fixture.GetInit;

		var actual = () => DependencyServiceMock.VerifyGetGetInit(Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.GetInit.get to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.GetInit.get
			- 2: IPrimitiveDependencyService.GetInit.get
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsGet()
	{
		var fixture = CreateFixture();
		_ = fixture.GetInit;
		fixture.ReturnWithParameter("value");

		DependencyServiceMock.VerifyGetGetInit(Times.Once);

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
	public void VerifyValidSequenceGet()
	{
		var fixture = CreateFixture();
		_ = fixture.GetInit;
		_ = fixture.GetInit;

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.GetGetInit();
			ctx.DependencyServiceMock.GetGetInit();
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequenceGet()
	{
		var fixture = CreateFixture();
		_ = fixture.GetInit;
		_ = fixture.GetInit;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.GetGetInit();
			ctx.DependencyServiceMock.GetGetInit();
			ctx.DependencyServiceMock.GetGetInit();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.GetInit.get to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.GetInit.get
			- 2: IPrimitiveDependencyService.GetInit.get
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithoutSetup()
	{
		const string parameter = nameof(parameter);

		CreateFixture().GetInit = parameter;
	}

	[Fact]
	public void VerifyIfNotCalledSet()
	{
		DependencyServiceMock.VerifySetGetInit(It<string>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalledSet()
	{
		var actual = () => DependencyServiceMock.VerifySetGetInit(It<string>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.GetInit.set = any to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupSet()
	{
		const string parameter = nameof(parameter);
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupSetGetInit(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture().GetInit = parameter;

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);

		const string paramCustomerId = nameof(paramCustomerId),
			setupCustomerId = nameof(setupCustomerId);

		DependencyServiceMock
			.SetupSetGetInit(setupCustomerId)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture().GetInit = paramCustomerId;
	}

	[Fact]
	public void NotTreatZeroAsAny()
	{
		const string errorMessage = nameof(errorMessage);
		const string parameter = nameof(parameter), anotherParameter = nameof(anotherParameter);

		DependencyServiceMock
			.SetupSetGetInit(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture().GetInit = parameter;

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);

		CreateFixture().GetInit = anotherParameter;
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string input = nameof(input);

		DependencyServiceMock
			.SetupSetGetInit(It<string>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupSetGetInit(input)
			.Throws(new ArgumentException(errorMessage2));

		Action actual = () => CreateFixture().GetInit = input;

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void VerifyTimesSet()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetInit = parameter1;
		fixture.GetInit = parameter2;

		DependencyServiceMock.VerifySetGetInit(parameter1, Times.Once);
		DependencyServiceMock.VerifySetGetInit(parameter2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);
		var verify1 = It<string>.Where(x => x.EndsWith('1'));

		var fixture = CreateFixture();
		fixture.GetInit = parameter1;
		fixture.GetInit = parameter2;

		DependencyServiceMock.VerifySetGetInit(verify1, Times.Once);
		DependencyServiceMock.VerifySetGetInit(parameter2, Times.AtMost(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);
		var verify = It<string>.Any();

		var fixture = CreateFixture();
		fixture.GetInit = parameter1;
		fixture.GetInit = parameter2;

		DependencyServiceMock.VerifySetGetInit(verify, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimesSet()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetInit = parameter1;
		fixture.GetInit = parameter2;

		var actual = () =>
		{
			var verify = It<string>.Any();
			DependencyServiceMock.VerifySetGetInit(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.GetInit.set = any to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.GetInit.set = "parameter1"
			- 2: IPrimitiveDependencyService.GetInit.set = "parameter2"
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsSet()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetInit = parameter1;
		fixture.GetInit = parameter2;

		DependencyServiceMock.VerifySetGetInit(parameter1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.GetInit.set = String to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.GetInit.set = "parameter2"
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequenceSet()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetInit = parameter1;
		fixture.GetInit = parameter2;

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.SetGetInit(parameter1);
			ctx.DependencyServiceMock.SetGetInit(parameter2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetInit = parameter1;
		fixture.GetInit = parameter2;

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Equivalent(parameter1), verify2 = It<string>.Equivalent(parameter2);
			ctx.DependencyServiceMock.SetGetInit(verify1);
			ctx.DependencyServiceMock.SetGetInit(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetInit = parameter1;
		fixture.GetInit = parameter2;

		VerifyInSequence(static ctx =>
		{
			var verify = It<string>.Any();
			ctx.DependencyServiceMock.SetGetInit(verify);
			ctx.DependencyServiceMock.SetGetInit(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetInit = parameter1;
		fixture.GetInit = parameter2;

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.EndsWith('1')), verify2 = It<string>.Where(x => x.EndsWith("2"));
			ctx.DependencyServiceMock.SetGetInit(verify1);
			ctx.DependencyServiceMock.SetGetInit(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequenceSet()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetInit = parameter1;
		fixture.GetInit = parameter2;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.SetGetInit(parameter2);
			ctx.DependencyServiceMock.SetGetInit(parameter1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.GetInit.set = "parameter1" to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.GetInit.set = "parameter1"
			- 2: IPrimitiveDependencyService.GetInit.set = "parameter2"
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetInit = parameter1;
		fixture.GetInit = parameter2;

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.EndsWith('2')), verify2 = It<string>.Where(x => x.EndsWith("1"));
			ctx.DependencyServiceMock.SetGetInit(verify1);
			ctx.DependencyServiceMock.SetGetInit(verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.GetInit.set = where(predicate) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.GetInit.set = "parameter1"
			- 2: IPrimitiveDependencyService.GetInit.set = "parameter2"
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetInit = parameter1;
		_ = fixture.GetInit;
		_ = fixture.GetInit;
		fixture.GetInit = parameter2;
		_ = fixture.GetInit;
		fixture.GetInit = parameter1;

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.SetGetInit(parameter1);
			ctx.DependencyServiceMock.GetGetInit();
			ctx.DependencyServiceMock.GetGetInit();
			ctx.DependencyServiceMock.SetGetInit(parameter2);
			ctx.DependencyServiceMock.GetGetInit();
			ctx.DependencyServiceMock.SetGetInit(parameter1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const string parameter1 = nameof(parameter1);

		var fixture = CreateFixture();
		fixture.GetInit = parameter1;
		_ = fixture.GetInit;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.SetGetInit(parameter1);
			ctx.DependencyServiceMock.InvokeWithParameter(ItIn<int>.Value(123));
			ctx.DependencyServiceMock.GetGetInit();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(in 123) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.GetInit.set = "parameter1"
			- 2: IPrimitiveDependencyService.GetInit.get
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnDifferentValuesGet()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		DependencyServiceMock
			.SetupGetGetInit()
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.GetInit);
		Assert.Equal(setupValue2, fixture.GetInit);
		Assert.Equal(setupValue2, fixture.GetInit);

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallbackGet1()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		DependencyServiceMock
			.SetupGetGetInit()
			.Callback(() => callback++)
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Null(fixture.GetInit);
		Assert.Equal(setupValue1, fixture.GetInit);
		Assert.Equal(setupValue2, fixture.GetInit);
		Assert.Equal(setupValue2, fixture.GetInit);
		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallbackGet2()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		DependencyServiceMock
			.SetupGetGetInit()
			.Callback(() => callback++).And().Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.GetInit);
		Assert.Equal(setupValue2, fixture.GetInit);
		Assert.Equal(setupValue2, fixture.GetInit);
		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallbackGet3()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		DependencyServiceMock
			.SetupGetGetInit()
			.Returns(setupValue1)
			.Returns(setupValue2).And().Callback(() => callback++);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.GetInit);
		Assert.Equal(setupValue2, fixture.GetInit);
		Assert.Equal(setupValue2, fixture.GetInit);
		Assert.Equal(2, callback);

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallbackGet4()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		DependencyServiceMock
			.SetupGetGetInit()
			.Callback(() => callback++).And().Returns(setupValue1)
			.Returns(setupValue2).And().Callback(() => callback++);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.GetInit);
		Assert.Equal(setupValue2, fixture.GetInit);
		Assert.Equal(setupValue2, fixture.GetInit);
		Assert.Equal(3, callback);

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsGet()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		DependencyServiceMock
			.SetupGetGetInit()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetInit;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetInit;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetInit;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackGet1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupGetGetInit()
			.Callback(() => callback++)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		Assert.Null(fixture.GetInit);

		var actual2 = () => fixture.GetInit;
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.GetInit;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture.GetInit;
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackGet2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupGetGetInit()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetInit;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetInit;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetInit;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(1, callback);

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackGet3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupGetGetInit()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetInit;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetInit;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetInit;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2, callback);

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackGet4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyServiceMock
			.SetupGetGetInit()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetInit;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetInit;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetInit;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3, callback);

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowExceptionWithReturnGet()
	{
		const string errorMessage = nameof(errorMessage), setupValue = nameof(setupValue);

		DependencyServiceMock
			.SetupGetGetInit()
			.Throws(new COMException(errorMessage))
			.Returns(setupValue);

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetInit;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		Assert.Equal(setupValue, fixture.GetInit);
		Assert.Equal(setupValue, fixture.GetInit);

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnWithThrowExceptionGet()
	{
		const string errorMessage = nameof(errorMessage), setupValue = nameof(setupValue);

		DependencyServiceMock
			.SetupGetGetInit()
			.Returns(setupValue)
			.Throws(new COMException(errorMessage));

		var fixture = CreateFixture();

		Assert.Equal(setupValue, fixture.GetInit);

		var actual2 = () => fixture.GetInit;
		var exception1 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage, exception1.Message);

		var actual3 = () => fixture.GetInit;
		var exception3 = Assert.Throws<COMException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		DependencyServiceMock.VerifyGetGetInit(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsSet()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);

		DependencyServiceMock
			.SetupSetGetInit()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetInit = setValue;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetInit = setValue;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetInit = setValue;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifySetGetInit(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackSet1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyServiceMock
			.SetupSetGetInit()
			.Callback(x => callback += x.Length)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		fixture.GetInit = setValue;

		var actual2 = () => fixture.GetInit = setValue;
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.GetInit = setValue;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture.GetInit = setValue;
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue.Length, callback);

		DependencyServiceMock.VerifySetGetInit(setValue, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackSet2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyServiceMock
			.SetupSetGetInit()
			.Callback(x => callback += x.Length).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetInit = setValue;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetInit = setValue;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetInit = setValue;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue.Length, callback);

		DependencyServiceMock.VerifySetGetInit(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackSet3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyServiceMock
			.SetupSetGetInit()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x.Length);

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetInit = setValue;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetInit = setValue;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetInit = setValue;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * setValue.Length, callback);

		DependencyServiceMock.VerifySetGetInit(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackSet4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyServiceMock
			.SetupSetGetInit()
			.Callback(x => callback += x.Length).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x.Length);

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetInit = setValue;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetInit = setValue;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetInit = setValue;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * setValue.Length, callback);

		DependencyServiceMock.VerifySetGetInit(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}
