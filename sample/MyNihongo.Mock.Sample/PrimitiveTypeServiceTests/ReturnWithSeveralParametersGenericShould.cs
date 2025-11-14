// ReSharper disable AccessToModifiedClosure

namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnWithSeveralParametersGenericShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const double expected = 0d;
		var parameter1 = "name";
		const float parameter2 = 2f;

		var actual = CreateFixture()
			.ReturnWithSeveralParameters<string, float, double>(ref parameter1, parameter2);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(default, default, Times.Never);
		DependencyServiceMock.VerifyReturnWithSeveralParameters<float, string, double>(default, It<string>.Any(), Times.Never);
		DependencyServiceMock.VerifyReturnWithSeveralParameters<double, float, string>(ItRef<double>.Any(), default, Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ItRef<string>.Any(), It<float>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref any, any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const double resultSetup = 15d;
		var parameter1 = "name";
		const float parameter2 = 2f;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>(ItRef<string>.Value(parameter1), parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithSeveralParameters<string, float, double>(ref parameter1, parameter2);

		Assert.Equal(resultSetup, actual);
	}

	[Fact]
	public void ReturnValueWithInvalidSequence1()
	{
		const double expected = 0d;
		const double resultSetup = 15d;
		var parameter1 = "parameter1";
		const string parameter2 = nameof(parameter2);

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, string, double>(ItRef<string>.Value(parameter1), parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithSeveralParameters<string, string, double>(ref parameter1, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithInvalidSequence2()
	{
		const double expected = 0d;
		const double resultSetup = 15d;
		const string parameter1 = "parameter1";
		var parameter2 = "parameter2";

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, string, double>(ItRef<string>.Value(parameter1), parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithSeveralParameters<string, string, double>(ref parameter2, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithInvalidSequence3()
	{
		const double expected = 0d;
		const double resultSetup = 15d;
		const string parameter1 = "parameter1";
		var parameter2 = "parameter2";

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, string, double>(ItRef<string>.Value(parameter1), parameter2)
			.Returns(resultSetup);

		var actual = CreateFixture()
			.ReturnWithSeveralParameters<string, string, double>(ref parameter2, parameter2);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);
		var parameter1 = "parameter1";
		const float parameter2 = 2f;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>(ItRef<string>.Value(parameter1), parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithSeveralParameters<string, float, double>(ref parameter1, parameter2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueForThrowsWithInvalidSequence1()
	{
		const double expected = 0d;
		var parameter1 = "parameter1";
		const string parameter2 = nameof(parameter2);
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, string, double>(ItRef<string>.Value(parameter1), parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParameters<string, string, double>(ref parameter1, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueForThrowsWithInvalidSequence2()
	{
		const double expected = 0d;
		const string parameter1 = nameof(parameter1);
		var parameter2 = "parameter2";
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, string, double>(ItRef<string>.Value(parameter1), parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParameters<string, string, double>(ref parameter2, parameter1);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueForThrowsWithInvalidSequence3()
	{
		const double expected = 0d;
		const string parameter1 = nameof(parameter1);
		var parameter2 = "parameter2";
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, string, double>(ItRef<string>.Value(parameter1), parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithSeveralParameters<string, string, double>(ref parameter2, parameter2);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyTimes()
	{
		string parameterValue1 = "parameterValue1", parameterValue3 = "parameterValue3";
		const float parameterValue2 = 234f;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2, Times.Once);
		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2, Times.Once);
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		string parameterValue1 = "parameterValue1", parameterValue3 = "parameterValue3";
		const float parameterValue2 = 234f;
		var verify1 = ItRef<string>.Any();

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(verify1, parameterValue2, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		string parameterValue1 = "parameterValue1", parameterValue3 = "parameterValue3";
		const float parameterValue2 = 234f;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);

		var actual = () =>
		{
			var verify1 = ItRef<string>.Any();
			DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(verify1, parameterValue2, Times.Exactly(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref any, 234) to be called 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue1", 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", 234)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		string parameterValue1 = "parameterValue1", parameterValue3 = "parameterValue3";
		const float parameterValue2 = 234f, parameterValue4 = 456f;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue4);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue4);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2, Times.Once);
		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue4, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref System.String, Single) to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue1", 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", 456)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		string parameterValue1 = "parameterValue1", parameterValue3 = "parameterValue3";
		const float parameterValue2 = 234f, parameterValue4 = 456f;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue4);

		VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue4);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		string parameterValue1 = "parameterValue1", parameterValue3 = "parameterValue3";
		const float parameterValue2 = 234f, parameterValue4 = 456f;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue4);

		VerifyInSequence(ctx =>
		{
			It<float> verify2 = It<float>.Where(x => x < 300), verify4 = It<float>.Where(x => x > 400);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, verify4);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		string parameterValue1 = "parameterValue1", parameterValue3 = "parameterValue3";
		const float parameterValue2 = 234f, parameterValue4 = 456f;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue4);

		VerifyInSequence(ctx =>
		{
			It<float> verify2 = It<float>.Equivalent(parameterValue2), verify4 = It<float>.Equivalent(parameterValue4);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, verify4);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		string parameterValue1 = "parameterValue1", parameterValue3 = "parameterValue3";
		const float parameterValue2 = 234f, parameterValue4 = 456f;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue4);

		VerifyInSequence(static ctx =>
		{
			var verify1 = ItRef<string>.Any();
			var verify2 = It<float>.Any();
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(verify1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(verify1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(verify1, verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		string parameterValue1 = "parameterValue1", parameterValue3 = "parameterValue3";
		const float parameterValue2 = 234f, parameterValue4 = 456f;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue4);

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue4);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", 234) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue1", 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", 456)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhere()
	{
		string parameterValue1 = "parameterValue1", parameterValue3 = "parameterValue3";
		const float parameterValue2 = 234f, parameterValue4 = 456f;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue4);

		var actual = () => VerifyInSequence(ctx =>
		{
			It<float> verify2 = It<float>.Where(x => x < 300), verify4 = It<float>.Where(x => x > 400);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, verify4);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", where(predicate)) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue1", 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", 456)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceEquivalent()
	{
		string parameterValue1 = "parameterValue1", parameterValue3 = "parameterValue3";
		const float parameterValue2 = 234f, parameterValue4 = 456f;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue4);

		var actual = () => VerifyInSequence(ctx =>
		{
			It<float> verify2 = It<float>.Equivalent(parameterValue2), verify4 = It<float>.Equivalent(parameterValue4);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, verify4);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue1", 234) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue1", 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", 456)
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
		string parameterValue1 = "parameterValue1", parameterValue3 = "parameterValue3";
		const float parameterValue2 = 234f, parameterValue4 = 456f;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue4);

		var actual = () => VerifyInSequence(static ctx =>
		{
			var verify1 = ItRef<string>.Any();
			var verify2 = It<float>.Any();
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(verify1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(verify1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(verify1, verify2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(verify1, verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref any, any) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue1", 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", 456)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		string parameterValue1 = "parameterValue1", parameterValue3 = "parameterValue3";
		const float parameterValue2 = 234f, parameterValue4 = 456f;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue4);

		var actual = () => VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue1, parameterValue2);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue2);
			ctx.DependencyServiceMock.GetGetInit();
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref parameterValue3, parameterValue4);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.GetInit.get to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue1", 234)
			- 2: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", 234)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref "parameterValue3", 456)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnDifferentValues()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		var setValue1 = "setValue1";
		const float setValue2 = 3f;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback1()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		var setValue1 = "setValue1";
		const float setValue2 = 3f;
		var callback = 0f;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((ref x, y) => callback += x.Length + y)
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(0d, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setupValue1, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setValue1.Length + setValue2, callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback2()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		var setValue1 = "setValue1";
		const float setValue2 = 3f;
		var callback = 0f;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((ref x, y) => callback += x.Length + y).And().Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setValue1.Length + setValue2, callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback3()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		var setValue1 = "setValue1";
		const float setValue2 = 3f;
		var callback = 0f;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Returns(setupValue1)
			.Returns(setupValue2).And().Callback((ref x, y) => callback += x.Length + y);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(2 * (setValue1.Length + setValue2), callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback4()
	{
		const int setupValue1 = 123, setupValue2 = 321;
		var setValue1 = "setValue1";
		const float setValue2 = 3f;
		var callback = 0f;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((ref x, y) => callback += x.Length + y).And().Returns(setupValue1)
			.Returns(setupValue2).And().Callback((ref x, y) => callback += x.Length + y);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setupValue2, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(3 * (setValue1.Length + setValue2), callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var setValue1 = "setValue1";
		const float setValue2 = 3f;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var setValue1 = "setValue1";
		const float setValue2 = 3f;
		var callback = 0f;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((ref x, y) => callback += x.Length + y)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		Assert.Equal(0d, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));

		var actual2 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue1.Length + setValue2, callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var setValue1 = "setValue1";
		const float setValue2 = 3f;
		var callback = 0f;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((ref x, y) => callback += x.Length + y).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue1.Length + setValue2, callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var setValue1 = "setValue1";
		const float setValue2 = 3f;
		var callback = 0f;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((ref x, y) => callback += x.Length + y);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * (setValue1.Length + setValue2), callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var setValue1 = "setValue1";
		const float setValue2 = 3f;
		var callback = 0f;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((ref x, y) => callback += x.Length + y).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((ref x, y) => callback += x.Length + y);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * (setValue1.Length + setValue2), callback);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage);
		var setValue1 = "setValue1";
		const float setValue2 = 3f;
		const double setupValue = 736d;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Throws(new COMException(errorMessage))
			.Returns(setupValue);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		Assert.Equal(setupValue, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));
		Assert.Equal(setupValue, fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage);
		var setValue1 = "setValue1";
		const float setValue2 = 3f;
		const double setupValue = 736d;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Returns(setupValue)
			.Throws(new COMException(errorMessage));

		var fixture = CreateFixture();

		Assert.Equal(Convert.ToDouble(setupValue), fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2));

		var actual2 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception1 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage, exception1.Message);

		var actual3 = () => { _ = fixture.ReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2); };
		var exception3 = Assert.Throws<COMException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void SetupDifferentTypes()
	{
		const string errorMessage = nameof(errorMessage);
		var stringValue = "stringValue";
		var floatValue = 123f;
		const int intValue = 456;
		var callback = 0f;

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<float, string, double>()
			.Throws(new NullReferenceException(errorMessage));

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, float, double>()
			.Callback((ref x, _) => callback += x.Length);

		DependencyServiceMock
			.SetupReturnWithSeveralParameters<string, double, float>()
			.Returns(floatValue);

		var fixture = CreateFixture();
		var actual1 = fixture.ReturnWithSeveralParameters<string, float, double>(ref stringValue, intValue);
		Assert.Equal(0d, actual1);

		var actual2 = fixture.ReturnWithSeveralParameters<string, double, float>(ref stringValue, intValue);
		Assert.Equal(floatValue, actual2);

		Action actual3 = () => fixture.ReturnWithSeveralParameters<float, string, double>(ref floatValue, stringValue);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		Assert.Equal(stringValue.Length, callback);
	}

	[Fact]
	public void VerifyDifferentTypes()
	{
		var floatParam = 123f;
		var stringParam = "stringParam";
		var decimalParam = 123m;
		const int intParam = 456;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref stringParam, intParam);
		fixture.ReturnWithSeveralParameters<float, double, string>(ref floatParam, intParam);
		fixture.ReturnWithSeveralParameters<decimal, float, string>(ref decimalParam, intParam);
		fixture.ReturnWithSeveralParameters<string, float, double>(ref stringParam, intParam);

		DependencyServiceMock.VerifyReturnWithSeveralParameters<string, float, double>(ref stringParam, intParam, Times.Exactly(2));
		DependencyServiceMock.VerifyReturnWithSeveralParameters<decimal, float, string>(ref decimalParam, intParam, Times.Once);
		DependencyServiceMock.VerifyReturnWithSeveralParameters<float, double, string>(ref floatParam, intParam, Times.Once);
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyDifferentTypesInSequence()
	{
		var floatParam = 123f;
		var stringParam = "stringParam";
		var decimalParam = 123m;
		const int intParam = 456;

		var fixture = CreateFixture();
		fixture.ReturnWithSeveralParameters<string, float, double>(ref stringParam, intParam);
		fixture.ReturnWithSeveralParameters<float, double, string>(ref floatParam, intParam);
		fixture.ReturnWithSeveralParameters<decimal, float, string>(ref decimalParam, intParam);

		VerifyInSequence(ctx =>
		{
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<string, float, double>(ref stringParam, intParam);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<float, double, string>(ref floatParam, intParam);
			ctx.DependencyServiceMock.ReturnWithSeveralParameters<decimal, float, string>(ref decimalParam, intParam);
		});
		VerifyNoOtherCalls();
	}
}
