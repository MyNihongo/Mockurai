namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class GetSetGenericShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		var actual = CreateFixture()
			.GetSetGeneric;

		Assert.Null(actual);
	}

	[Fact]
	public void VerifyIfNotCalledGet()
	{
		DependencyGenericServiceMock.VerifyGetGetSet(Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalledGet()
	{
		var actual = () => DependencyGenericServiceMock.VerifyGetGetSet(Times.Once);

		const string errorMessage =
			"Expected IPrimitiveDependencyService<String>.GetSet.get to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const string setupValue = nameof(setupValue);

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Returns(setupValue);

		var actual = CreateFixture()
			.GetSetGeneric;

		Assert.Equal(setupValue, actual);
	}

	[Fact]
	public void ThrowWithSetupGet()
	{
		const string errorMessage = nameof(errorMessage);

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => { _ = CreateFixture().GetSetGeneric; };

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void VerifyTimesGet()
	{
		var fixture = CreateFixture();
		_ = fixture.GetSetGeneric;
		_ = fixture.GetSetGeneric;

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Exactly(2));
		DependencyGenericServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimesGet()
	{
		var fixture = CreateFixture();
		_ = fixture.GetSetGeneric;
		_ = fixture.GetSetGeneric;

		var actual = () => DependencyGenericServiceMock.VerifyGetGetSet(Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.GetSet.get to be called 1 time, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.GetSet.get
			- 2: IPrimitiveDependencyService<String>.GetSet.get
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsGet()
	{
		var fixture = CreateFixture();
		_ = fixture.GetSetGeneric;
		fixture.ReturnWithParameterGeneric<float>("value");

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Once);

		var actual = () => DependencyGenericServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.ReturnWithParameter<Single>(String) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService<String>.ReturnWithParameter<Single>("value")
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequenceGet()
	{
		var fixture = CreateFixture();
		_ = fixture.GetSetGeneric;
		_ = fixture.GetSetGeneric;

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.GetGetSet();
			ctx.DependencyGenericServiceMock.GetGetSet();
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequenceGet()
	{
		var fixture = CreateFixture();
		_ = fixture.GetSetGeneric;
		_ = fixture.GetSetGeneric;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.GetGetSet();
			ctx.DependencyGenericServiceMock.GetGetSet();
			ctx.DependencyGenericServiceMock.GetGetSet();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.GetSet.get to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.GetSet.get
			- 2: IPrimitiveDependencyService<String>.GetSet.get
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithoutSetup()
	{
		const string parameter = nameof(parameter);

		CreateFixture().GetSetGeneric = parameter;
	}

	[Fact]
	public void VerifyIfNotCalledSet()
	{
		DependencyGenericServiceMock.VerifySetGetSet(It<string>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalledSet()
	{
		var actual = () => DependencyGenericServiceMock.VerifySetGetSet(It<string>.Any(), Times.Once);

		const string errorMessage =
			"Expected IPrimitiveDependencyService<String>.GetSet.set = any to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupSet()
	{
		const string parameter = nameof(parameter);
		const string errorMessage = nameof(errorMessage);

		DependencyGenericServiceMock
			.SetupSetGetSet(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture().GetSetGeneric = parameter;

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);

		const string paramCustomerId = nameof(paramCustomerId),
			setupCustomerId = nameof(setupCustomerId);

		DependencyGenericServiceMock
			.SetupSetGetSet(setupCustomerId)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture().GetSetGeneric = paramCustomerId;
	}

	[Fact]
	public void NotTreatZeroAsAny()
	{
		const string errorMessage = nameof(errorMessage);
		const string parameter = nameof(parameter), anotherParameter = nameof(anotherParameter);

		DependencyGenericServiceMock
			.SetupSetGetSet(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture().GetSetGeneric = parameter;

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);

		CreateFixture().GetSetGeneric = anotherParameter;
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string input = nameof(input);

		DependencyGenericServiceMock
			.SetupSetGetSet(It<string>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyGenericServiceMock
			.SetupSetGetSet(input)
			.Throws(new ArgumentException(errorMessage2));

		Action actual = () => CreateFixture().GetSetGeneric = input;

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void VerifyTimesSet()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetSetGeneric = parameter1;
		fixture.GetSetGeneric = parameter2;

		DependencyGenericServiceMock.VerifySetGetSet(parameter1, Times.Once);
		DependencyGenericServiceMock.VerifySetGetSet(parameter2, Times.AtLeast(1));
		DependencyGenericServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);
		var verify1 = It<string>.Where(x => x.EndsWith('1'));

		var fixture = CreateFixture();
		fixture.GetSetGeneric = parameter1;
		fixture.GetSetGeneric = parameter2;

		DependencyGenericServiceMock.VerifySetGetSet(verify1, Times.Once);
		DependencyGenericServiceMock.VerifySetGetSet(parameter2, Times.AtMost(1));
		DependencyGenericServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);
		var verify = It<string>.Any();

		var fixture = CreateFixture();
		fixture.GetSetGeneric = parameter1;
		fixture.GetSetGeneric = parameter2;

		DependencyGenericServiceMock.VerifySetGetSet(verify, Times.Exactly(2));
		DependencyGenericServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimesSet()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetSetGeneric = parameter1;
		fixture.GetSetGeneric = parameter2;

		var actual = () =>
		{
			var verify = It<string>.Any();
			DependencyGenericServiceMock.VerifySetGetSet(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.GetSet.set = any to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.GetSet.set = "parameter1"
			- 2: IPrimitiveDependencyService<String>.GetSet.set = "parameter2"
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsSet()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetSetGeneric = parameter1;
		fixture.GetSetGeneric = parameter2;

		DependencyGenericServiceMock.VerifySetGetSet(parameter1, Times.Once);

		var actual = () => DependencyGenericServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.GetSet.set = String to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService<String>.GetSet.set = "parameter2"
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequenceSet()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetSetGeneric = parameter1;
		fixture.GetSetGeneric = parameter2;

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.SetGetSet(parameter1);
			ctx.DependencyGenericServiceMock.SetGetSet(parameter2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetSetGeneric = parameter1;
		fixture.GetSetGeneric = parameter2;

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Equivalent(parameter1), verify2 = It<string>.Equivalent(parameter2);
			ctx.DependencyGenericServiceMock.SetGetSet(verify1);
			ctx.DependencyGenericServiceMock.SetGetSet(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetSetGeneric = parameter1;
		fixture.GetSetGeneric = parameter2;

		VerifyInSequence(static ctx =>
		{
			var verify = It<string>.Any();
			ctx.DependencyGenericServiceMock.SetGetSet(verify);
			ctx.DependencyGenericServiceMock.SetGetSet(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetSetGeneric = parameter1;
		fixture.GetSetGeneric = parameter2;

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.EndsWith('1')),
				verify2 = It<string>.Where(x => x.EndsWith("2"));
			ctx.DependencyGenericServiceMock.SetGetSet(verify1);
			ctx.DependencyGenericServiceMock.SetGetSet(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequenceSet()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetSetGeneric = parameter1;
		fixture.GetSetGeneric = parameter2;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.SetGetSet(parameter2);
			ctx.DependencyGenericServiceMock.SetGetSet(parameter1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.GetSet.set = "parameter1" to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.GetSet.set = "parameter1"
			- 2: IPrimitiveDependencyService<String>.GetSet.set = "parameter2"
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetSetGeneric = parameter1;
		fixture.GetSetGeneric = parameter2;

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.EndsWith('2')),
				verify2 = It<string>.Where(x => x.EndsWith("1"));
			ctx.DependencyGenericServiceMock.SetGetSet(verify1);
			ctx.DependencyGenericServiceMock.SetGetSet(verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.GetSet.set = where(predicate) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.GetSet.set = "parameter1"
			- 2: IPrimitiveDependencyService<String>.GetSet.set = "parameter2"
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.GetSetGeneric = parameter1;
		_ = fixture.GetSetGeneric;
		_ = fixture.GetSetGeneric;
		fixture.GetSetGeneric = parameter2;
		_ = fixture.GetSetGeneric;
		fixture.GetSetGeneric = parameter1;

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.SetGetSet(parameter1);
			ctx.DependencyGenericServiceMock.GetGetSet();
			ctx.DependencyGenericServiceMock.GetGetSet();
			ctx.DependencyGenericServiceMock.SetGetSet(parameter2);
			ctx.DependencyGenericServiceMock.GetGetSet();
			ctx.DependencyGenericServiceMock.SetGetSet(parameter1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const string parameter1 = nameof(parameter1);

		var fixture = CreateFixture();
		fixture.GetSetGeneric = parameter1;
		_ = fixture.GetSetGeneric;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.SetGetSet(parameter1);
			ctx.DependencyGenericServiceMock.InvokeWithParameter("some value");
			ctx.DependencyGenericServiceMock.GetGetSet();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.InvokeWithParameter("some value") to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.GetSet.set = "parameter1"
			- 2: IPrimitiveDependencyService<String>.GetSet.get
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnDifferentValuesGet()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.GetSetGeneric);
		Assert.Equal(setupValue2, fixture.GetSetGeneric);
		Assert.Equal(setupValue2, fixture.GetSetGeneric);

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallbackGet1()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Callback(() => callback++)
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Null(fixture.GetSetGeneric);
		Assert.Equal(setupValue1, fixture.GetSetGeneric);
		Assert.Equal(setupValue2, fixture.GetSetGeneric);
		Assert.Equal(setupValue2, fixture.GetSetGeneric);
		Assert.Equal(1, callback);

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallbackGet2()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Callback(() => callback++).And().Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.GetSetGeneric);
		Assert.Equal(setupValue2, fixture.GetSetGeneric);
		Assert.Equal(setupValue2, fixture.GetSetGeneric);
		Assert.Equal(1, callback);

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallbackGet3()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Returns(setupValue1)
			.Returns(setupValue2).And().Callback(() => callback++);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.GetSetGeneric);
		Assert.Equal(setupValue2, fixture.GetSetGeneric);
		Assert.Equal(setupValue2, fixture.GetSetGeneric);
		Assert.Equal(2, callback);

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallbackGet4()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Callback(() => callback++).And().Returns(setupValue1)
			.Returns(setupValue2).And().Callback(() => callback++);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.GetSetGeneric);
		Assert.Equal(setupValue2, fixture.GetSetGeneric);
		Assert.Equal(setupValue2, fixture.GetSetGeneric);
		Assert.Equal(3, callback);

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsGet()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetSetGeneric;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetSetGeneric;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetSetGeneric;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackGet1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Callback(() => callback++)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		Assert.Null(fixture.GetSetGeneric);

		var actual2 = () => fixture.GetSetGeneric;
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.GetSetGeneric;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture.GetSetGeneric;
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(1, callback);

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackGet2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetSetGeneric;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetSetGeneric;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetSetGeneric;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(1, callback);

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackGet3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetSetGeneric;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetSetGeneric;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetSetGeneric;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2, callback);

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackGet4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Callback(() => callback++).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(() => callback++);

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetSetGeneric;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetSetGeneric;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetSetGeneric;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3, callback);

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowExceptionWithReturnGet()
	{
		const string errorMessage = nameof(errorMessage), setupValue = nameof(setupValue);

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Throws(new COMException(errorMessage))
			.Returns(setupValue);

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetSetGeneric;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		Assert.Equal(setupValue, fixture.GetSetGeneric);
		Assert.Equal(setupValue, fixture.GetSetGeneric);

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnWithThrowExceptionGet()
	{
		const string errorMessage = nameof(errorMessage), setupValue = nameof(setupValue);

		DependencyGenericServiceMock
			.SetupGetGetSet()
			.Returns(setupValue)
			.Throws(new COMException(errorMessage));

		var fixture = CreateFixture();

		Assert.Equal(setupValue, fixture.GetSetGeneric);

		var actual2 = () => fixture.GetSetGeneric;
		var exception1 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage, exception1.Message);

		var actual3 = () => fixture.GetSetGeneric;
		var exception3 = Assert.Throws<COMException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		DependencyGenericServiceMock.VerifyGetGetSet(Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsSet()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);

		DependencyGenericServiceMock
			.SetupSetGetSet()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetSetGeneric = setValue;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetSetGeneric = setValue;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetSetGeneric = setValue;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyGenericServiceMock.VerifySetGetSet(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackSet1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupSetGetSet()
			.Callback(x => callback += x.Length)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		fixture.GetSetGeneric = setValue;

		var actual2 = () => fixture.GetSetGeneric = setValue;
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.GetSetGeneric = setValue;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture.GetSetGeneric = setValue;
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue.Length, callback);

		DependencyGenericServiceMock.VerifySetGetSet(setValue, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackSet2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupSetGetSet()
			.Callback(x => callback += x.Length).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetSetGeneric = setValue;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetSetGeneric = setValue;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetSetGeneric = setValue;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue.Length, callback);

		DependencyGenericServiceMock.VerifySetGetSet(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackSet3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupSetGetSet()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x.Length);

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetSetGeneric = setValue;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetSetGeneric = setValue;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetSetGeneric = setValue;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * setValue.Length, callback);

		DependencyGenericServiceMock.VerifySetGetSet(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackSet4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		var callback = 0;

		DependencyGenericServiceMock
			.SetupSetGetSet()
			.Callback(x => callback += x.Length).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x.Length);

		var fixture = CreateFixture();

		var actual1 = () => fixture.GetSetGeneric = setValue;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.GetSetGeneric = setValue;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.GetSetGeneric = setValue;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * setValue.Length, callback);

		DependencyGenericServiceMock.VerifySetGetSet(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}