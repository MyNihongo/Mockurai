namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class InvokeWithParameterClassShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		const float paramCustomerId = 123f;

		CreateFixture()
			.InvokeWithParameterClass(paramCustomerId);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(It<float>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(It<float>.Any(), Times.Once);

		const string errorMessage = "Expected PrimitiveDependencyBase.InvokeWithParameter(any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const float parameter = 123f;
		const string errorMessage = nameof(errorMessage);

		PrimitiveDependencyBaseMock
			.SetupInvokeWithParameter(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameterClass(parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupAny()
	{
		const float parameter = 123f;
		const string errorMessage = nameof(errorMessage);

		PrimitiveDependencyBaseMock
			.SetupInvokeWithParameter(It<float>.Any())
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameterClass(parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);

		const float paramCustomerId = 123f,
			setupCustomerId = 321f;

		PrimitiveDependencyBaseMock
			.SetupInvokeWithParameter(setupCustomerId)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()
			.InvokeWithParameterClass(paramCustomerId);
	}

	[Fact]
	public void NotTreatZeroAsAny()
	{
		const string errorMessage = nameof(errorMessage);
		const int parameter = 0, anotherParameter = 1;

		PrimitiveDependencyBaseMock
			.SetupInvokeWithParameter(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()
			.InvokeWithParameterClass(parameter);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);

		CreateFixture()
			.InvokeWithParameterClass(anotherParameter);
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const float input = 123f;

		PrimitiveDependencyBaseMock
			.SetupInvokeWithParameter(It<float>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		PrimitiveDependencyBaseMock
			.SetupInvokeWithParameter(input)
			.Throws(new ArgumentException(errorMessage2));

		var actual = () => CreateFixture()
			.InvokeWithParameterClass(input);

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameterClass(parameter1);
		fixture.InvokeWithParameterClass(parameter2);

		PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(parameter1, Times.Once);
		PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(parameter2, Times.AtLeast(1));
		PrimitiveDependencyBaseMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const float parameter1 = 123f, parameter2 = 321f;
		var verify1 = It<float>.Where(x => x < 200f);

		var fixture = CreateFixture();
		fixture.InvokeWithParameterClass(parameter1);
		fixture.InvokeWithParameterClass(parameter2);

		PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(verify1, Times.Once);
		PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(parameter2, Times.AtMost(1));
		PrimitiveDependencyBaseMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const float parameter1 = 123f, parameter2 = 321f;
		var verify = It<float>.Any();

		var fixture = CreateFixture();
		fixture.InvokeWithParameterClass(parameter1);
		fixture.InvokeWithParameterClass(parameter2);

		PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(verify, Times.Exactly(2));
		PrimitiveDependencyBaseMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameterClass(parameter1);
		fixture.InvokeWithParameterClass(parameter2);

		var actual = () =>
		{
			var verify = It<float>.Any();
			PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected PrimitiveDependencyBase.InvokeWithParameter(any) to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: PrimitiveDependencyBase.InvokeWithParameter(123)
			- 2: PrimitiveDependencyBase.InvokeWithParameter(321)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameterClass(parameter1);
		fixture.InvokeWithParameterClass(parameter2);

		PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(parameter1, Times.Once);

		var actual = () => PrimitiveDependencyBaseMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected PrimitiveDependencyBase.InvokeWithParameter(Single) to be verified, but the following invocations have not been verified:
			- 2: PrimitiveDependencyBase.InvokeWithParameter(321)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameterClass(parameter1);
		fixture.InvokeWithParameterClass(parameter2);

		VerifyInSequence(static ctx =>
		{
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(parameter1);
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(parameter2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameterClass(parameter1);
		fixture.InvokeWithParameterClass(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<float> verify1 = It<float>.Equivalent(parameter1), verify2 = It<float>.Equivalent(parameter2);
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(verify1);
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameterClass(parameter1);
		fixture.InvokeWithParameterClass(parameter2);

		VerifyInSequence(static ctx =>
		{
			var verify = It<float>.Any();
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(verify);
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameterClass(parameter1);
		fixture.InvokeWithParameterClass(parameter2);

		VerifyInSequence(static ctx =>
		{
			It<float> verify1 = It<float>.Where(x => x > 100f), verify2 = It<float>.Where(x => x > 300f);
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(verify1);
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameterClass(parameter1);
		fixture.InvokeWithParameterClass(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(parameter2);
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(parameter1);
		});

		const string expectedMessage =
			"""
			Expected PrimitiveDependencyBase.InvokeWithParameter(123) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: PrimitiveDependencyBase.InvokeWithParameter(123)
			- 2: PrimitiveDependencyBase.InvokeWithParameter(321)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhere()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameterClass(parameter1);
		fixture.InvokeWithParameterClass(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<float> verify1 = It<float>.Where(x => x > 300f), verify2 = It<float>.Where(x => x < 200f);
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(verify1);
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(verify2);
		});

		const string expectedMessage =
			"""
			Expected PrimitiveDependencyBase.InvokeWithParameter(where(predicate)) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: PrimitiveDependencyBase.InvokeWithParameter(123)
			- 2: PrimitiveDependencyBase.InvokeWithParameter(321)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameterClass(parameter1);
		fixture.InvokeWithParameterClass(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(parameter1);
			ctx.PrimitiveDependencyBaseMock.Invoke();
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(parameter2);
		});

		const string expectedMessage =
			"""
			Expected PrimitiveDependencyBase.Invoke() to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: PrimitiveDependencyBase.InvokeWithParameter(123)
			- 2: PrimitiveDependencyBase.InvokeWithParameter(321)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequenceEquivalent()
	{
		const float parameter1 = 123f, parameter2 = 321f;

		var fixture = CreateFixture();
		fixture.InvokeWithParameterClass(parameter1);
		fixture.InvokeClass();
		fixture.InvokeWithParameterClass(parameter2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(parameter1);
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(It<float>.Equivalent(parameter1 + parameter2));
			ctx.PrimitiveDependencyBaseMock.InvokeWithParameter(parameter2);
		});

		const string expectedMessage =
			"""
			Expected PrimitiveDependencyBase.InvokeWithParameter(444) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: PrimitiveDependencyBase.InvokeWithParameter(123)
			- 2: PrimitiveDependencyBase.Invoke()
			- 3: PrimitiveDependencyBase.InvokeWithParameter(321)
			  expected: 444
			  actual: 321
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue = 2;

		PrimitiveDependencyBaseMock
			.SetupInvokeWithParameter(It<float>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithParameterClass(setValue);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithParameterClass(setValue);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithParameterClass(setValue);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue = 2;
		var callback = 0f;

		PrimitiveDependencyBaseMock
			.SetupInvokeWithParameter(It<float>.Any())
			.Callback(x => callback += x)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		fixture.InvokeWithParameterClass(setValue);

		var actual2 = () => fixture.InvokeWithParameterClass(setValue);
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.InvokeWithParameterClass(setValue);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture.InvokeWithParameterClass(setValue);
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue, callback);

		PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(setValue, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue = 2;
		var callback = 0f;

		PrimitiveDependencyBaseMock
			.SetupInvokeWithParameter(It<float>.Any())
			.Callback(x => callback += x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithParameterClass(setValue);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithParameterClass(setValue);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithParameterClass(setValue);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue, callback);

		PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue = 2;
		var callback = 0f;

		PrimitiveDependencyBaseMock
			.SetupInvokeWithParameter(It<float>.Any())
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x);

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithParameterClass(setValue);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithParameterClass(setValue);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithParameterClass(setValue);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * setValue, callback);

		PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int setValue = 2;
		var callback = 0f;

		PrimitiveDependencyBaseMock
			.SetupInvokeWithParameter(It<float>.Any())
			.Callback(x => callback += x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x);

		var fixture = CreateFixture();

		var actual1 = () => fixture.InvokeWithParameterClass(setValue);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.InvokeWithParameterClass(setValue);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.InvokeWithParameterClass(setValue);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * setValue, callback);

		PrimitiveDependencyBaseMock.VerifyInvokeWithParameter(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}