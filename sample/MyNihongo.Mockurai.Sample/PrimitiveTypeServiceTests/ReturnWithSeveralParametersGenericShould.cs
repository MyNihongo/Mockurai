// ReSharper disable AccessToModifiedClosure

namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnWithSeveralParametersGenericShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const string parameter1 = "name";
		const float parameter2 = 2f;

		var actual = CreateFixture()
			.ReturnWithSeveralParametersGeneric(parameter1, parameter2);

		Assert.Null(actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(default, default, Times.Never);
		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, string, double>(It<string>.Any(), default, Times.Never);
		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, string>(It<float>.Any(), default, Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyGenericServiceMock.VerifyReturnWithSeveralParameters(It<float>.Any(), It<double>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(any, any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const string resultSetup = nameof(resultSetup);
		const double parameter2 = 3d;
		const float parameter1 = 2f;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>(It<float>.Value(parameter1), parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithSeveralParametersGeneric(parameter1, parameter2);

		Assert.Equal(resultSetup, actual);
	}

	[Fact]
	public void ReturnValueWithInvalidSequence1()
	{
		const string resultSetup = nameof(resultSetup);
		const string parameter1 = "parameter1";
		const double parameter2 = 123d;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, string, double>(It<string>.Value(parameter1), parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithSeveralParametersGeneric(parameter1, parameter1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnValueWithInvalidSequence2()
	{
		const string resultSetup = nameof(resultSetup);
		const string parameter1 = "parameter1";
		const double parameter2 = 123d;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, string, double>(It<string>.Value(parameter1), parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithSeveralParametersGeneric(parameter2, parameter1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnValueWithInvalidSequence3()
	{
		const double parameter1 = 15d;
		const string parameter2 = nameof(parameter2);
		const string resultSetup = nameof(resultSetup);

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, double, string>(It<double>.Value(parameter1), parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithSeveralParametersGeneric(parameter2, parameter2);

		Assert.Null(actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const float parameter1 = 2f;
		const double parameter2 = 123d;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>(It<float>.Value(parameter1), parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParametersGeneric(parameter1, parameter2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueForThrowsWithInvalidSequence1()
	{
		const string parameter1 = "parameter1";
		const double parameter2 = 123d;
		const string errorMessage = nameof(errorMessage);

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, string, double>(It<string>.Value(parameter1), parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParametersGeneric(parameter1, parameter1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnValueForThrowsWithInvalidSequence2()
	{
		const string parameter1 = nameof(parameter1);
		const double parameter2 = 123d;
		const string errorMessage = nameof(errorMessage);

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, string, double>(It<string>.Value(parameter1), parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParametersGeneric(parameter2, parameter1);

		Assert.Null(actual);
	}

	[Fact]
	public void ReturnValueForThrowsWithInvalidSequence3()
	{
		const string parameter1 = nameof(parameter1);
		const double parameter2 = 123d;
		const string errorMessage = nameof(errorMessage);

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, string, double>(It<string>.Value(parameter1), parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParametersGeneric(parameter2, parameter2);

		Assert.Null(actual);
	}

	[Fact]
	public void VerifyTimes()
	{
		const float parameterValue1 = 123f, parameterValue3 = 789f;
		const double parameterValue2 = 234d;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue2);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(parameterValue1, parameterValue2, Times.Once);
		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(parameterValue3, parameterValue2, Times.Once);
		DependencyGenericServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const float parameterValue1 = 123f, parameterValue3 = 789f;
		const double parameterValue2 = 234d;
		var verify1 = It<float>.Any();

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue2);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(verify1, parameterValue2, Times.Exactly(2));
		DependencyGenericServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const float parameterValue1 = 123f, parameterValue3 = 789f;
		const double parameterValue2 = 234d;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue2);

		var actual = () =>
		{
			var verify1 = It<float>.Any();
			DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(verify1, parameterValue2, Times.Exactly(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(any, 234) to be called 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(123, 234)
			- 2: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, 234)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		const float parameterValue1 = 123f, parameterValue3 = 789f;
		const double parameterValue2 = 234d, parameterValue4 = 456d;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue4);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue4);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(parameterValue3, parameterValue2, Times.Once);
		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(parameterValue1, parameterValue4, Times.Once);

		var actual = () => DependencyGenericServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(Single, Double) to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(123, 234)
			- 3: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, 456)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const float parameterValue1 = 123f, parameterValue3 = 789f;
		const double parameterValue2 = 234d, parameterValue4 = 456d;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue4);

		VerifyInSequence(ctx =>
		{
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue1, parameterValue2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, parameterValue2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, parameterValue4);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		const float parameterValue1 = 123f, parameterValue3 = 789f;
		const double parameterValue2 = 234d, parameterValue4 = 456d;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue4);

		VerifyInSequence(ctx =>
		{
			It<double> verify2 = It<double>.Where(x => x < 300), verify4 = It<double>.Where(x => x > 400);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue1, verify2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, verify2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, verify4);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		const float parameterValue1 = 123f, parameterValue3 = 789f;
		const double parameterValue2 = 234d, parameterValue4 = 456d;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue4);

		VerifyInSequence(ctx =>
		{
			It<double> verify2 = It<double>.Equivalent(parameterValue2), verify4 = It<double>.Equivalent(parameterValue4);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue1, verify2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, verify2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, verify4);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		const float parameterValue1 = 123f, parameterValue3 = 789f;
		const double parameterValue2 = 234d, parameterValue4 = 456d;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue4);

		VerifyInSequence(static ctx =>
		{
			var verify1 = It<float>.Any();
			var verify2 = It<double>.Any();
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters(verify1, verify2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters(verify1, verify2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters(verify1, verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		const float parameterValue1 = 123f, parameterValue3 = 789f;
		const double parameterValue2 = 234d, parameterValue4 = 456d;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue4);

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue1, parameterValue2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, parameterValue4);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, parameterValue2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, 234) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(123, 234)
			- 2: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, 234)
			- 3: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, 456)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhere()
	{
		const float parameterValue1 = 123f, parameterValue3 = 789f;
		const double parameterValue2 = 234d, parameterValue4 = 456d;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue4);

		var actual = () => VerifyInSequence(ctx =>
		{
			It<double> verify2 = It<double>.Where(x => x < 300), verify4 = It<double>.Where(x => x > 400);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue1, verify2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, verify4);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, where(predicate)) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(123, 234)
			- 2: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, 234)
			- 3: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, 456)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceEquivalent()
	{
		const float parameterValue1 = 123f, parameterValue3 = 789f;
		const double parameterValue2 = 234d, parameterValue4 = 456d;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue4);

		var actual = () => VerifyInSequence(ctx =>
		{
			It<double> verify2 = It<double>.Equivalent(parameterValue2), verify4 = It<double>.Equivalent(parameterValue4);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, verify2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue1, verify2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, verify4);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(123, 234) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(123, 234)
			- 2: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, 234)
			- 3: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, 456)
			  - parameter2:
			    expected: 234
			    actual: 456
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceAny()
	{
		const float parameterValue1 = 123f, parameterValue3 = 789f;
		const double parameterValue2 = 234d, parameterValue4 = 456d;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue4);

		var actual = () => VerifyInSequence(static ctx =>
		{
			var verify1 = It<float>.Any();
			var verify2 = It<double>.Any();
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters(verify1, verify2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters(verify1, verify2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters(verify1, verify2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters(verify1, verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(any, any) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(123, 234)
			- 2: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, 234)
			- 3: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, 456)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const float parameterValue1 = 123f, parameterValue3 = 789f;
		const double parameterValue2 = 234d, parameterValue4 = 456d;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParametersGeneric(parameterValue3, parameterValue4);

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue1, parameterValue2);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, parameterValue2);
			ctx.DependencyGenericServiceMock.GetGetSet();
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, double>(parameterValue3, parameterValue4);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.GetSet.get to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(123, 234)
			- 2: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, 234)
			- 3: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Single, Double>(789, 456)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnDifferentValues()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		const float setValue1 = 2f;
		const double setValue2 = 3d;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback1()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		const float setValue1 = 2f;
		const double setValue2 = 3d;
		var callback = 0d;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((x, y) => callback += x + y)
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Null(fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setupValue1, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setValue1 + setValue2, callback);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(setValue1, setValue2, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback2()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		const float setValue1 = 2f;
		const double setValue2 = 3d;
		var callback = 0d;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((x, y) => callback += x + y).And().Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setValue1 + setValue2, callback);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback3()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		const float setValue1 = 2f;
		const double setValue2 = 3d;
		var callback = 0d;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Returns(setupValue1)
			.Returns(setupValue2).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(2 * (setValue1 + setValue2), callback);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback4()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		const float setValue1 = 2f;
		const double setValue2 = 3d;
		var callback = 0d;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((x, y) => callback += x + y).And().Returns(setupValue1)
			.Returns(setupValue2).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(3 * (setValue1 + setValue2), callback);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue1 = 2f;
		const double setValue2 = 3d;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue1 = 2f;
		const double setValue2 = 3d;
		var callback = 0d;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((x, y) => callback += x + y)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		Assert.Null(fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));

		var actual2 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue1 + setValue2, callback);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(setValue1, setValue2, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue1 = 2f;
		const double setValue2 = 3d;
		var callback = 0d;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((x, y) => callback += x + y).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue1 + setValue2, callback);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue1 = 2f;
		const double setValue2 = 3d;
		var callback = 0d;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * (setValue1 + setValue2), callback);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue1 = 2f;
		const double setValue2 = 3d;
		var callback = 0d;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((x, y) => callback += x + y).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, y) => callback += x + y);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * (setValue1 + setValue2), callback);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage);
		const float setValue1 = 2f;
		const double setValue2 = 3d;
		const string setupValue = nameof(setupValue);

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Throws(new COMException(errorMessage))
			.Returns(setupValue);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		Assert.Equal(setupValue, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));
		Assert.Equal(setupValue, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage);
		const float setValue1 = 2f;
		const double setValue2 = 3d;
		const string setupValue = nameof(setupValue);

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Returns(setupValue)
			.Throws(new COMException(errorMessage));

		var fixture = CreateFixture();

		Assert.Equal(setupValue, fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2));

		var actual2 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage, exception1.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParametersGeneric(setValue1, setValue2); };
		var exception3 = Assert.Throws<COMException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void SetupDifferentTypes()
	{
		const string errorMessage = nameof(errorMessage);
		const string stringValue = "stringValue";
		const double doubleValue = 123d;
		const float floatValue = 234f;
		const int intValue = 456;
		var callback = 0f;

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, int, double>()
			.Throws(new NullReferenceException(errorMessage));

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((x, _) => callback += x);

		DependencyGenericServiceMock
			.SetupReturnWithSeveralParameters<string, double, float>()
			.Returns(stringValue);

		var fixture = CreateFixture();
		var actual1 = fixture.ReturnWithSeveralParametersGeneric(floatValue, doubleValue);
		Assert.Null(actual1);

		var actual2 = fixture.ReturnWithSeveralParametersGeneric(doubleValue, floatValue);
		Assert.Equal(stringValue, actual2);

		Action actual3 = () => fixture.ReturnWithSeveralParametersGeneric(intValue, doubleValue);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		Assert.Equal(floatValue, callback);
	}

	[Fact]
	public void VerifyDifferentTypes()
	{
		const float floatParam = 123f;
		const string stringParam = "stringParam";
		const decimal decimalParam = 123m;
		const int intParam = 456;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(stringParam, intParam);
		fixture.ReturnWithSeveralParametersGeneric(floatParam, intParam);
		fixture.ReturnWithSeveralParametersGeneric(decimalParam, intParam);
		fixture.ReturnWithSeveralParametersGeneric(stringParam, intParam);

		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, string, int>(stringParam, intParam, Times.Exactly(2));
		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, float, int>(floatParam, intParam, Times.Once);
		DependencyGenericServiceMock.VerifyReturnWithSeveralParameters<string, decimal, int>(decimalParam, intParam, Times.Once);
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyDifferentTypesInSequence()
	{
		const float floatParam = 123f;
		const string stringParam = "stringParam";
		const decimal decimalParam = 123m;
		const int intParam = 456;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParametersGeneric(stringParam, intParam);
		fixture.ReturnWithSeveralParametersGeneric(floatParam, intParam);
		fixture.ReturnWithSeveralParametersGeneric(decimalParam, intParam);
		fixture.ReturnWithSeveralParametersGeneric(stringParam, intParam);

		VerifyInSequence(ctx =>
		{
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, string, int>(stringParam, intParam);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, float, int>(floatParam, intParam);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, decimal, int>(decimalParam, intParam);
			ctx.DependencyGenericServiceMock.ReturnWithSeveralParameters<string, string, int>(stringParam, intParam);
		});
		VerifyNoOtherCalls();
	}
}