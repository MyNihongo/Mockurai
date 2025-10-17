namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnWithParameterShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const string paramCustomerId = "qOp5SNt2LVqzQ9qS",
			expected = "name:,age:32";

		var actual = CreateFixture()
			.ReturnWithParameter(paramCustomerId);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithoutSetupRef()
	{
		const int expected = 1;
		var parameterValue = 123d;

		var actual = CreateFixture()
			.ReturnWithParameter(ref parameterValue);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyReturnWithParameter(It<string>.Any(), Times.Never);
		DependencyServiceMock.VerifyReturnWithParameter(ItRef<double>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyReturnWithParameter(It<string>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.ReturnWithParameter(any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotCalledRef()
	{
		var actual = () => DependencyServiceMock.VerifyReturnWithParameter(ItRef<double>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.ReturnWithParameter(ref any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const string parameter = "ZFJ2XHcBRAuyJZJX",
			nameSetup = "Okayama Issei",
			expected = "name:Okayama Issei,age:32";

		DependencyServiceMock
			.SetupReturnWithParameter(parameter)
			.Returns(nameSetup);

		var actual = CreateFixture()
			.ReturnWithParameter(parameter);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithSetupRef()
	{
		const int setupValue = 234, expected = 235;
		var inputValue = 123d;

		DependencyServiceMock
			.SetupReturnWithParameter(ref inputValue)
			.Returns(setupValue);

		var actual = CreateFixture()
			.ReturnWithParameter(ref inputValue);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithSetupAny()
	{
		const string parameter = "ZFJ2XHcBRAuyJZJX",
			nameSetup = "Okayama Issei",
			expected = "name:Okayama Issei,age:32";

		DependencyServiceMock
			.SetupReturnWithParameter(It<string>.Any())
			.Returns(nameSetup);

		var actual = CreateFixture()
			.ReturnWithParameter(parameter);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithSetupAnyRef()
	{
		const int setupValue = 234, expected = 235;
		var inputValue = 123d;

		DependencyServiceMock
			.SetupReturnWithParameter(ItRef<double>.Any())
			.Returns(setupValue);

		var actual = CreateFixture()
			.ReturnWithParameter(ref inputValue);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithAnotherSetup()
	{
		const string paramCustomerId = "ZFJ2XHcBRAuyJZJX",
			setupCustomerId = "another ID",
			setupName = "Okayama Issei",
			expected = "name:,age:32";

		DependencyServiceMock
			.SetupReturnWithParameter(setupCustomerId)
			.Returns(setupName);

		var actual = CreateFixture()
			.ReturnWithParameter(paramCustomerId);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithAnotherSetupRef()
	{
		const int setupValue = 234, expected = 1;
		double inputValue = 123d, setupInputValue = 987d;

		DependencyServiceMock
			.SetupReturnWithParameter(ref setupInputValue)
			.Returns(setupValue);

		var actual = CreateFixture()
			.ReturnWithParameter(ref inputValue);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string parameter = "ZFJ2XHcBRAuyJZJX",
			errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithParameter(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithParameter(parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupRef()
	{
		const string errorMessage = nameof(errorMessage);
		var setupValue = 123d;

		DependencyServiceMock
			.SetupReturnWithParameter(ref setupValue)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithParameter(ref setupValue);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueForThrowsWithAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);

		const string paramCustomerId = "ZFJ2XHcBRAuyJZJX",
			setupCustomerId = "another ID",
			expected = "name:,age:32";

		DependencyServiceMock
			.SetupReturnWithParameter(setupCustomerId)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithParameter(paramCustomerId);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueForThrowsWithAnotherSetupRef()
	{
		const string errorMessage = nameof(errorMessage);

		const int expected = 1;
		double inputValue = 123d, setupInputValue = 987d;

		DependencyServiceMock
			.SetupReturnWithParameter(ref setupInputValue)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithParameter(ref inputValue);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyTimes()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		DependencyServiceMock.VerifyReturnWithParameter(parameter1, Times.Once);
		DependencyServiceMock.VerifyReturnWithParameter(parameter2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesRef()
	{
		double parameter1 = 123d, parameter2 = 234d;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(ref parameter1);
		fixture.ReturnWithParameter(ref parameter2);

		DependencyServiceMock.VerifyReturnWithParameter(ref parameter1, Times.Once);
		DependencyServiceMock.VerifyReturnWithParameter(ref parameter2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);
		var verify1 = It<string>.Where(x => x.EndsWith('1'));

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		DependencyServiceMock.VerifyReturnWithParameter(verify1, Times.Once);
		DependencyServiceMock.VerifyReturnWithParameter(parameter2, Times.AtMost(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);
		var verify = It<string>.Any();

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		DependencyServiceMock.VerifyReturnWithParameter(verify, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAnyRef()
	{
		double parameter1 = 123d, parameter2 = 234d;
		var verify = ItRef<double>.Any();

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(ref parameter1);
		fixture.ReturnWithParameter(ref parameter2);

		DependencyServiceMock.VerifyReturnWithParameter(verify, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		var actual = () =>
		{
			var verify = It<string>.Any();
			DependencyServiceMock.VerifyReturnWithParameter(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter(any) to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: "parameter1"
			- 2: "parameter2"
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyTimesRef()
	{
		double parameter1 = 123d, parameter2 = 234d;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(ref parameter1);
		fixture.ReturnWithParameter(ref parameter2);

		var actual = () =>
		{
			var verify = ItRef<double>.Any();
			DependencyServiceMock.VerifyReturnWithParameter(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter(ref any) to be called at least 3 times, but instead it was called 2 times.
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
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		DependencyServiceMock.VerifyReturnWithParameter(parameter1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter(String) to be verified, but the following invocations have not been verified:
			- 2: "parameter2"
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsRef()
	{
		double parameter1 = 123d, parameter2 = 234d;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(ref parameter1);
		fixture.ReturnWithParameter(ref parameter2);

		DependencyServiceMock.VerifyReturnWithParameter(ref parameter1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter(ref Double) to be verified, but the following invocations have not been verified:
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
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithParameter(parameter1);
			ctx.DependencyServiceMock.ReturnWithParameter(parameter2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceRef()
	{
		double parameter1 = 123d, parameter2 = 234d;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(ref parameter1);
		fixture.ReturnWithParameter(ref parameter2);

		VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithParameter(ref parameter1);
			ctx.DependencyServiceMock.ReturnWithParameter(ref parameter2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Equivalent(parameter1), verify2 = It<string>.Equivalent(parameter2);
			ctx.DependencyServiceMock.ReturnWithParameter(verify1);
			ctx.DependencyServiceMock.ReturnWithParameter(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			var verify = It<string>.Any();
			ctx.DependencyServiceMock.ReturnWithParameter(verify);
			ctx.DependencyServiceMock.ReturnWithParameter(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAnyRef()
	{
		double parameter1 = 123d, parameter2 = 234d;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(ref parameter1);
		fixture.ReturnWithParameter(ref parameter2);

		VerifyInSequence(static ctx =>
		{
			var verify = ItRef<double>.Any();
			ctx.DependencyServiceMock.ReturnWithParameter(verify);
			ctx.DependencyServiceMock.ReturnWithParameter(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.StartsWith("pa")), verify2 = It<string>.Where(x => x.EndsWith("r2"));
			ctx.DependencyServiceMock.ReturnWithParameter(verify1);
			ctx.DependencyServiceMock.ReturnWithParameter(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithParameter(parameter2);
			ctx.DependencyServiceMock.ReturnWithParameter(parameter1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter("parameter1") to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: "parameter1"
			- 2: "parameter2"
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceRef()
	{
		double parameter1 = 123d, parameter2 = 234d;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(ref parameter1);
		fixture.ReturnWithParameter(ref parameter2);

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithParameter(ref parameter2);
			ctx.DependencyServiceMock.ReturnWithParameter(ref parameter1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter(ref 123) to be invoked at index 3, but it has not been called.
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
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.EndsWith("r2")), verify2 = It<string>.Where(x => x.StartsWith("pa"));
			ctx.DependencyServiceMock.ReturnWithParameter(verify1);
			ctx.DependencyServiceMock.ReturnWithParameter(verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithParameter(where(predicate)) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: "parameter1"
			- 2: "parameter2"
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const string parameter1 = nameof(parameter1), parameter2 = nameof(parameter2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(parameter1);
		fixture.ReturnWithParameter(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithParameter(parameter1);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.ReturnWithParameter(parameter2);
		});

		const string expectedMessage = "Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but there are no invocations.";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequenceRef()
	{
		double parameter1 = 123d, parameter2 = 234d;

		var fixture = CreateFixture();
		fixture.ReturnWithParameter(ref parameter1);
		fixture.ReturnWithParameter(ref parameter2);

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithParameter(ref parameter1);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(123, 321);
			ctx.DependencyServiceMock.ReturnWithParameter(ref parameter2);
		});

		const string expectedMessage = "Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321) to be invoked at index 2, but there are no invocations.";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
