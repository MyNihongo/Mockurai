namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeWithParameterShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		const string paramCustomerId = "qOp5SNt2LVqzQ9qS";

		CreateFixture()
			.InvokeWithParameter(paramCustomerId);
	}

	[Fact]
	public void ExecuteWithoutSetupRef()
	{
		var inputValue = 123m;

		CreateFixture()
			.InvokeWithParameter(ref inputValue);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyInvokeWithParameter(It<string>.Any(), Times.Never);
		DependencyServiceMock.VerifyInvokeWithParameter(It<int>.Any(), Times.Never);
		DependencyServiceMock.VerifyInvokeWithParameter(ItRef<decimal>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyInvokeWithParameter(It<string>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService#InvokeWithParameter(any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotCalledOverload()
	{
		var actual = () => DependencyServiceMock.VerifyInvokeWithParameter(It<int>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService#InvokeWithParameter(any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotCalledRef()
	{
		var actual = () => DependencyServiceMock.VerifyInvokeWithParameter(ItRef<decimal>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService#InvokeWithParameter(ref any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string parameter = "ZFJ2XHcBRAuyJZJX",
			errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithParameter(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameter(parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupRef()
	{
		const string errorMessage = nameof(errorMessage);
		var inputValue = 123m;

		DependencyServiceMock
			.SetupInvokeWithParameter(ref inputValue)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameter(ref inputValue);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupAny()
	{
		const string parameter = "ZFJ2XHcBRAuyJZJX",
			errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithParameter(It<string>.Any())
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameter(parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupAnyOverload()
	{
		const int parameter = 123;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupInvokeWithParameter(It<int>.Any())
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameter(parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupAnyRef()
	{
		const string errorMessage = nameof(errorMessage);
		var inputValue = 123m;

		DependencyServiceMock
			.SetupInvokeWithParameter(ItRef<decimal>.Any())
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameter(ref inputValue);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);

		const string paramCustomerId = "ZFJ2XHcBRAuyJZJX",
			setupCustomerId = "another ID";

		DependencyServiceMock
			.SetupInvokeWithParameter(setupCustomerId)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithParameter(paramCustomerId);
	}

	[Fact]
	public void ExecuteWithAnotherSetupRef()
	{
		const string errorMessage = nameof(errorMessage);
		decimal setupValue = 321m, inputValue = 123m;

		DependencyServiceMock
			.SetupInvokeWithParameter(ref setupValue)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithParameter(ref inputValue);
	}

	[Fact]
	public void NotTreatZeroAsAny()
	{
		const string errorMessage = nameof(errorMessage);
		const int parameter = 0, anotherParameter = 1;

		DependencyServiceMock
			.SetupInvokeWithParameter(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameter(parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);

		CreateFixture()
			.InvokeWithParameter(anotherParameter);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int input = 123;

		DependencyServiceMock
			.SetupInvokeWithParameter(It<int>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupInvokeWithParameter(input)
			.Throws(new ArgumentException(errorMessage2));

		var actual = () => CreateFixture()
			.InvokeWithParameter(input);

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(parameter1, Times.Once);
		DependencyServiceMock.VerifyInvokeWithParameter(parameter2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesOverload()
	{
		const int parameter1 = 123, parameter2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(parameter1, Times.Once);
		DependencyServiceMock.VerifyInvokeWithParameter(parameter2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesRef()
	{
		decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(ref parameter1);
		fixture.InvokeWithParameter(ref parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(ref parameter1, Times.Once);
		DependencyServiceMock.VerifyInvokeWithParameter(ref parameter2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);
		var verify1 = It<string>.Where(x => x.EndsWith('1'));

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(verify1, Times.Once);
		DependencyServiceMock.VerifyInvokeWithParameter(parameter2, Times.AtMost(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhereOverload()
	{
		const int parameter1 = 123, parameter2 = 234;
		var verify1 = It<int>.Where(x => x < 200);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(verify1, Times.Once);
		DependencyServiceMock.VerifyInvokeWithParameter(parameter2, Times.AtMost(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);
		var verify = It<string>.Any();

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(verify, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAnyOverload()
	{
		const int parameter1 = 123, parameter2 = 234;
		var verify = It<int>.Any();

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(verify, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAnyRef()
	{
		decimal parameter1 = 123m, parameter2 = 234m;
		var verify = ItRef<decimal>.Any();

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(ref parameter1);
		fixture.InvokeWithParameter(ref parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(verify, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		var actual = () =>
		{
			var verify = It<string>.Any();
			DependencyServiceMock.VerifyInvokeWithParameter(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(any) to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: "parameter1"
			- 2: "parameter2"
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyTimesOverload()
	{
		const int parameter1 = 123, parameter2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		var actual = () =>
		{
			var verify = It<int>.Any();
			DependencyServiceMock.VerifyInvokeWithParameter(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(any) to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: 123
			- 2: 234
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyTimesRef()
	{
		decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(ref parameter1);
		fixture.InvokeWithParameter(ref parameter2);

		var actual = () =>
		{
			var verify = ItRef<decimal>.Any();
			DependencyServiceMock.VerifyInvokeWithParameter(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(ref any) to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: ref 123
			- 2: ref 234
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(parameter1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(String) to be verified, but the following invocations have not been verified:
			- 2: "parameter2"
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsOverload()
	{
		const int parameter1 = 123, parameter2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(parameter1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(Int32) to be verified, but the following invocations have not been verified:
			- 2: 234
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsRef()
	{
		decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(ref parameter1);
		fixture.InvokeWithParameter(ref parameter2);

		DependencyServiceMock.VerifyInvokeWithParameter(ref parameter1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(ref Decimal) to be verified, but the following invocations have not been verified:
			- 2: ref 234
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter(parameter1);
			ctx.DependencyServiceMock.InvokeWithParameter(parameter2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceOverload()
	{
		const int parameter1 = 123, parameter2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter(parameter1);
			ctx.DependencyServiceMock.InvokeWithParameter(parameter2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceReq()
	{
		decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(ref parameter1);
		fixture.InvokeWithParameter(ref parameter2);

		VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter(ref parameter1);
			ctx.DependencyServiceMock.InvokeWithParameter(ref parameter2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Equivalent(parameter1), verify2 = It<string>.Equivalent(parameter2);
			ctx.DependencyServiceMock.InvokeWithParameter(verify1);
			ctx.DependencyServiceMock.InvokeWithParameter(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalentOverload()
	{
		const int parameter1 = 123, parameter2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Equivalent(parameter1), verify2 = It<int>.Equivalent(parameter2);
			ctx.DependencyServiceMock.InvokeWithParameter(verify1);
			ctx.DependencyServiceMock.InvokeWithParameter(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			var verify = It<string>.Any();
			ctx.DependencyServiceMock.InvokeWithParameter(verify);
			ctx.DependencyServiceMock.InvokeWithParameter(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAnyOverload()
	{
		const int parameter1 = 123, parameter2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			var verify = It<int>.Any();
			ctx.DependencyServiceMock.InvokeWithParameter(verify);
			ctx.DependencyServiceMock.InvokeWithParameter(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAnyRef()
	{
		decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(ref parameter1);
		fixture.InvokeWithParameter(ref parameter2);

		VerifyInSequence(static ctx =>
		{
			var verify = ItRef<decimal>.Any();
			ctx.DependencyServiceMock.InvokeWithParameter(verify);
			ctx.DependencyServiceMock.InvokeWithParameter(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.StartsWith("pa")), verify2 = It<string>.Where(x => x.EndsWith("r2"));
			ctx.DependencyServiceMock.InvokeWithParameter(verify1);
			ctx.DependencyServiceMock.InvokeWithParameter(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhereOverload()
	{
		const int parameter1 = 123, parameter2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Where(x => x < 200), verify2 = It<int>.Where(x => x > 200);
			ctx.DependencyServiceMock.InvokeWithParameter(verify1);
			ctx.DependencyServiceMock.InvokeWithParameter(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter(parameter2);
			ctx.DependencyServiceMock.InvokeWithParameter(parameter1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter("parameter1") to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: "parameter1"
			- 2: "parameter2"
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceOverload()
	{
		const int parameter1 = 123, parameter2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter(parameter2);
			ctx.DependencyServiceMock.InvokeWithParameter(parameter1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(123) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: 123
			- 2: 234
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceRef()
	{
		decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(ref parameter1);
		fixture.InvokeWithParameter(ref parameter2);

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter(ref parameter2);
			ctx.DependencyServiceMock.InvokeWithParameter(ref parameter1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(ref 123) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: ref 123
			- 2: ref 234
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.EndsWith("r2")), verify2 = It<string>.Where(x => x.StartsWith("pa"));
			ctx.DependencyServiceMock.InvokeWithParameter(verify1);
			ctx.DependencyServiceMock.InvokeWithParameter(verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(where(predicate)) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: "parameter1"
			- 2: "parameter2"
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhereOverload()
	{
		const int parameter1 = 123, parameter2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Where(x => x > 200), verify2 = It<int>.Where(x => x < 200);
			ctx.DependencyServiceMock.InvokeWithParameter(verify1);
			ctx.DependencyServiceMock.InvokeWithParameter(verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(where(predicate)) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: 123
			- 2: 234
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter(parameter1);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.InvokeWithParameter(parameter2);
		});

		const string expectedMessage = "Expected IPrimitiveDependencyService#InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but there are no invocations.";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
	
	[Fact]
	public void ThrowInvalidMethodInSequenceEquivalent()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithSeveralParameters(123, 321);
		fixture.InvokeWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter(parameter1);
			ctx.DependencyServiceMock.InvokeWithParameter(It<string>.Equivalent("another param"));
			ctx.DependencyServiceMock.InvokeWithParameter(parameter2);
		});

		const string expectedMessage = "Expected IPrimitiveDependencyService#InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but there are no invocations.";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequenceOverload()
	{
		const int parameter1 = 123, parameter2 = 234;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(parameter1);
		fixture.InvokeWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter(parameter1);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(parameter1, parameter2);
			ctx.DependencyServiceMock.InvokeWithParameter(parameter2);
		});

		const string expectedMessage = "Expected IPrimitiveDependencyService#InvokeWithSeveralParameters(123, 234) to be invoked at index 2, but there are no invocations.";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequenceRef()
	{
		decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.InvokeWithParameter(ref parameter1);
		fixture.InvokeWithParameter(ref parameter2);

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithParameter(ref parameter1);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.InvokeWithParameter(ref parameter2);
		});

		const string expectedMessage = "Expected IPrimitiveDependencyService#InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but there are no invocations.";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
