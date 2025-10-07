namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class GetInitShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		var actual = CreateFixture()
			.GetInit;

		Assert.Empty(actual);
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
			- 1
			- 2
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
			Expected IPrimitiveDependencyService.ReturnWithParameter(String) to be verified, but the following invocations have not been verified:
			- 2: "value"
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
			- 1
			- 2
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
			- 1: "parameter1"
			- 2: "parameter2"
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
			- 2: "parameter2"
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
			- 1: "parameter1"
			- 2: "parameter2"
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
			- 1: "parameter1"
			- 2: "parameter2"
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
			ctx.DependencyServiceMock.InvokeWithParameter(123);
			ctx.DependencyServiceMock.GetGetInit();
		});

		const string expectedMessage = "Expected IPrimitiveDependencyService.InvokeWithParameter(123) to be invoked at index 2, but there are no invocations.";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
