namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeWithSeveralParametersGenericShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		const float parameter1 = 2025f;
		const string parameter2 = nameof(parameter2);

		CreateFixture()
			.InvokeWithSeveralParametersGeneric(parameter1, parameter2);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters(It<float>.Any(), default, Times.Never);
		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters(It<string>.Any(), default, Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters(It<float>.Any(), It<string>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<Single>(any, any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const float parameter1 = 2025f;
		const string parameter2 = nameof(parameter2);

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, float>(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParametersGeneric(parameter1, parameter2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithInvalidSequence1()
	{
		const string parameter1 = nameof(parameter1);
		const string parameter2 = nameof(parameter2);
		const string errorMessage = nameof(errorMessage);

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, string>(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithSeveralParametersGeneric<string>(parameter1, parameter1);
	}

	[Fact]
	public void ExecuteWithInvalidSequence2()
	{
		const string parameter1 = nameof(parameter1);
		const string parameter2 = nameof(parameter2);
		const string errorMessage = nameof(errorMessage);

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, string>(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithSeveralParametersGeneric<string>(parameter2, parameter1);
	}

	[Fact]
	public void ExecuteWithInvalidSequence3()
	{
		const string parameter1 = nameof(parameter1);
		const string parameter2 = nameof(parameter2);
		const string errorMessage = nameof(errorMessage);

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, string>(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithSeveralParametersGeneric<string>(parameter2, parameter2);
	}

	[Fact]
	public void NotTreatZeroAsAny()
	{
		const string errorMessage = nameof(errorMessage);
		const float parameter1 = 0f, anotherParameter2 = 1f;
		const string parameter2 = nameof(parameter2);

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, float>(parameter1, parameter2)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParametersGeneric(parameter1, parameter2);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);

		CreateFixture()
			.InvokeWithSeveralParametersGeneric(anotherParameter2, parameter2);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const string input1 = nameof(input1), input2 = nameof(input2);

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters(It<string>.Any(), It<string>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, string>(input1, It<string>.Any())
			.Throws(new ArgumentException(errorMessage2));

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters(It<string>.Any(), input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, string>(input1, input2)
			.Throws(new IndexOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParametersGeneric<string>(input1, input2);

		var exception = Assert.Throws<IndexOutOfRangeException>(actual);
		Assert.Equal(errorMessage4, exception.Message);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const string input1 = nameof(input1), input2 = nameof(input2), anotherParameter = nameof(anotherParameter);

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters(It<string>.Any(), It<string>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, string>(input1, It<string>.Any())
			.Throws(new ArgumentException(errorMessage2));

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters(It<string>.Any(), input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, string>(input1, input2)
			.Throws(new ArgumentOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParametersGeneric<string>(input1, anotherParameter);

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const string input1 = nameof(input1), input2 = nameof(input2), anotherParameter = nameof(anotherParameter);

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters(It<string>.Any(), It<string>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, string>(input1, It<string>.Any())
			.Throws(new ArgumentException(errorMessage2));

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters(It<string>.Any(), input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, string>(input1, input2)
			.Throws(new ArgumentOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParametersGeneric<string>(anotherParameter, input2);

		var exception = Assert.Throws<OutOfMemoryException>(actual);
		Assert.Equal(errorMessage3, exception.Message);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2), errorMessage3 = nameof(errorMessage3), errorMessage4 = nameof(errorMessage4);
		const string input1 = nameof(input1), input2 = nameof(input2), anotherParameter = nameof(anotherParameter);

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters(It<string>.Any(), It<string>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, string>(input1, It<string>.Any())
			.Throws(new ArgumentException(errorMessage2));

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters(It<string>.Any(), input2)
			.Throws(new OutOfMemoryException(errorMessage3));

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, string>(input1, input2)
			.Throws(new ArgumentOutOfRangeException(errorMessage4));

		var actual = () => CreateFixture()
			.InvokeWithSeveralParametersGeneric(anotherParameter, anotherParameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		const float parameterValue1 = 123f;
		const string parameterValue2 = nameof(parameterValue2);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue2);

		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters<string, float>(parameterValue1, parameterValue2, Times.Once);
		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters<string, string>(parameterValue2, parameterValue2, Times.Once);
		DependencyGenericServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const string parameterValue1 = nameof(parameterValue1);
		const string parameterValue2 = nameof(parameterValue2);
		var verify1 = It<string>.Where(x => x.Length > 2);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric(parameterValue2, parameterValue2);

		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters(verify1, parameterValue2, Times.Exactly(2));
		DependencyGenericServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const string parameterValue1 = nameof(parameterValue1);
		const string parameterValue2 = nameof(parameterValue2);
		var verify1 = It<string>.Any();

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue2);

		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters(verify1, parameterValue2, Times.Exactly(2));
		DependencyGenericServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const string parameterValue1 = nameof(parameterValue1);
		const string parameterValue2 = nameof(parameterValue2);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue2);

		var actual = () =>
		{
			var verify1 = It<string>.Any();
			DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters(verify1, parameterValue2, Times.Exactly(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>(any, "parameterValue2") to be called 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue1", "parameterValue2")
			- 2: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue2", "parameterValue2")
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		const string parameterValue1 = nameof(parameterValue1);
		const string parameterValue2 = nameof(parameterValue2);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue1);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue1, parameterValue1);

		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters<string, string>(parameterValue1, parameterValue1, Times.Once);
		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters<string, string>(parameterValue2, parameterValue2, Times.Once);

		var actual = () => DependencyGenericServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>(String, String) to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue1", "parameterValue2")
			- 3: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue2", "parameterValue1")
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const string parameterValue1 = nameof(parameterValue1);
		const string parameterValue2 = nameof(parameterValue2);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, string>(parameterValue1, parameterValue2);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, string>(parameterValue2, parameterValue2);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, string>(parameterValue2, parameterValue1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		const string parameterValue1 = nameof(parameterValue1);
		const string parameterValue2 = nameof(parameterValue2);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.EndsWith('1')), verify2 = It<string>.Where(x => x.EndsWith('2'));
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify1, verify2);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify2, verify2);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify2, verify1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		const string parameterValue1 = nameof(parameterValue1);
		const string parameterValue2 = nameof(parameterValue2);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Equivalent(parameterValue1), verify2 = It<string>.Equivalent(parameterValue2);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify1, verify2);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify2, verify2);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify2, verify1);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		const string parameterValue1 = nameof(parameterValue1);
		const string parameterValue2 = nameof(parameterValue2);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue1);

		VerifyInSequence(static ctx =>
		{
			var verify = It<string>.Any();
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify, verify);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify, verify);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify, verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		const string parameterValue1 = nameof(parameterValue1);
		const string parameterValue2 = nameof(parameterValue2);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, string>(parameterValue1, parameterValue2);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, string>(parameterValue2, parameterValue1);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, string>(parameterValue2, parameterValue2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue2", "parameterValue2") to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue1", "parameterValue2")
			- 2: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue2", "parameterValue2")
			- 3: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue2", "parameterValue1")
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhere()
	{
		const string parameterValue1 = nameof(parameterValue1);
		const string parameterValue2 = nameof(parameterValue2);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.EndsWith('1')), verify2 = It<string>.Where(x => x.EndsWith('2'));
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify2, verify2);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify2, verify1);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify1, verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>(where(predicate), where(predicate)) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue1", "parameterValue2")
			- 2: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue2", "parameterValue2")
			- 3: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue2", "parameterValue1")
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceEquivalent()
	{
		const string parameterValue1 = nameof(parameterValue1);
		const string parameterValue2 = nameof(parameterValue2);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Equivalent(parameterValue1), verify2 = It<string>.Equivalent(parameterValue2);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify2, verify2);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify1, verify2);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify2, verify1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue1", "parameterValue2") to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue1", "parameterValue2")
			- 2: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue2", "parameterValue2")
			- 3: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue2", "parameterValue1")
			  - parameter1:
			    expected: "parameterValue1"
			    actual: "parameterValue2"
			  - parameter2:
			    expected: "parameterValue2"
			    actual: "parameterValue1"
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceAny()
	{
		const string parameterValue1 = nameof(parameterValue1);
		const string parameterValue2 = nameof(parameterValue2);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			var verify = It<string>.Any();
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify, verify);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify, verify);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify, verify);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters(verify, verify);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>(any, any) to be invoked at index 4, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue1", "parameterValue2")
			- 2: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue2", "parameterValue2")
			- 3: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue2", "parameterValue1")
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const string parameterValue1 = nameof(parameterValue1);
		const string parameterValue2 = nameof(parameterValue2);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue1, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue2);
		fixture.InvokeWithSeveralParametersGeneric<string>(parameterValue2, parameterValue1);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, string>(parameterValue1, parameterValue2);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, string>(parameterValue2, parameterValue2);
			ctx.DependencyGenericServiceMock.Return<string, decimal>();
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, string>(parameterValue2, parameterValue1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.Return<Decimal>() to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue1", "parameterValue2")
			- 2: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue2", "parameterValue2")
			- 3: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<String>("parameterValue2", "parameterValue1")
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue1 = 3f;
		const string setValue2 = nameof(setValue2);

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<string>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters<string, float>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue1 = 3f;
		const string setValue2 = nameof(setValue2);
		var callback = 0f;

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<string>.Any())
			.Callback((x, y) => callback += x + y.Length)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);

		var actual2 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue1 + setValue2.Length, callback);

		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters<string, float>(setValue1, setValue2, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue1 = 3f;
		const string setValue2 = nameof(setValue2);
		var callback = 0f;

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<string>.Any())
			.Callback((x, y) => callback += x + y.Length).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue1 + setValue2.Length, callback);

		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters<string, float>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue1 = 3f;
		const string setValue2 = nameof(setValue2);
		var callback = 0f;

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<string>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, y) => callback += x + y.Length);

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * (setValue1 + setValue2.Length), callback);

		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters<string, float>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float setValue1 = 3f;
		const string setValue2 = nameof(setValue2);
		var callback = 0f;

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters(It<float>.Any(), It<string>.Any())
			.Callback((x, y) => callback += x + y.Length).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, y) => callback += x + y.Length);

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithSeveralParametersGeneric(setValue1, setValue2);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * (setValue1 + setValue2.Length), callback);

		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters<string, float>(setValue1, setValue2, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void SetupDifferentTypes()
	{
		const string errorMessage = nameof(errorMessage);
		const string stringValue = nameof(stringValue);
		const float floatValue = 123f;
		var callback = 0;

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, float>()
			.Throws(new NullReferenceException(errorMessage));

		DependencyGenericServiceMock
			.SetupInvokeWithSeveralParameters<string, string>()
			.Callback((x, _) => callback += x.Length);

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(stringValue, stringValue);

		var actual = () => fixture.InvokeWithSeveralParametersGeneric(floatValue, stringValue);
		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(errorMessage, exception.Message);

		Assert.Equal(stringValue.Length, callback);
	}

	[Fact]
	public void VerifyDifferentTypes()
	{
		const float floatParam = 123f;
		const string stringParam = nameof(stringParam);
		const int intParam = 456;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(stringParam, stringParam);
		fixture.InvokeWithSeveralParametersGeneric(intParam, stringParam);
		fixture.InvokeWithSeveralParametersGeneric(floatParam, stringParam);
		fixture.InvokeWithSeveralParametersGeneric<string>(stringParam, stringParam);

		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters<string, string>(stringParam, stringParam, Times.Exactly(2));
		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters<string, int>(intParam, stringParam, Times.Once);
		DependencyGenericServiceMock.VerifyInvokeWithSeveralParameters<string, float>(floatParam, stringParam, Times.Once);
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyDifferentTypesInSequence()
	{
		const float floatParam = 123f;
		const string stringParam = nameof(stringParam);
		const int intParam = 456;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParametersGeneric<string>(stringParam, stringParam);
		fixture.InvokeWithSeveralParametersGeneric(intParam, stringParam);
		fixture.InvokeWithSeveralParametersGeneric(floatParam, stringParam);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, string>(stringParam, stringParam);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, int>(intParam, stringParam);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, float>(floatParam, stringParam);
		});
		VerifyNoOtherCalls();
	}
}
