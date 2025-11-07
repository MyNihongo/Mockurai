namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class SetOnlyShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		const decimal parameter = 1234.567m;

		CreateFixture().SetOnly = parameter;
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifySetSetOnly(It<decimal>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyServiceMock.VerifySetSetOnly(It<decimal>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.SetOnly.set = any to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const decimal parameter = 1234.567m;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupSetSetOnly(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture().SetOnly = parameter;

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);

		const decimal paramCustomerId = 1234.567m,
			setupCustomerId = 4567.54m;

		DependencyServiceMock
			.SetupSetSetOnly(setupCustomerId)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture().SetOnly = paramCustomerId;
	}

	[Fact]
	public void NotTreatZeroAsAny()
	{
		const string errorMessage = nameof(errorMessage);
		const decimal parameter = 0m, anotherParameter = 1m;

		DependencyServiceMock
			.SetupSetSetOnly(parameter)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture().SetOnly = parameter;

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);

		CreateFixture().SetOnly = anotherParameter;
	}

	[Fact]
	public void TreatExactMatchesWithMorePriority()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const decimal input = 123m;

		DependencyServiceMock
			.SetupSetSetOnly(It<decimal>.Any())
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupSetSetOnly(input)
			.Throws(new ArgumentException(errorMessage2));

		Action actual = () => CreateFixture().SetOnly = input;

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void VerifyTimes()
	{
		const decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.SetOnly = parameter1;
		fixture.SetOnly = parameter2;

		DependencyServiceMock.VerifySetSetOnly(parameter1, Times.Once);
		DependencyServiceMock.VerifySetSetOnly(parameter2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const decimal parameter1 = 123m, parameter2 = 234m;
		var verify1 = It<decimal>.Where(x => x < 200m);

		var fixture = CreateFixture();
		fixture.SetOnly = parameter1;
		fixture.SetOnly = parameter2;

		DependencyServiceMock.VerifySetSetOnly(verify1, Times.Once);
		DependencyServiceMock.VerifySetSetOnly(parameter2, Times.AtMost(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const decimal parameter1 = 123m, parameter2 = 234m;
		var verify = It<decimal>.Any();

		var fixture = CreateFixture();
		fixture.SetOnly = parameter1;
		fixture.SetOnly = parameter2;

		DependencyServiceMock.VerifySetSetOnly(verify, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.SetOnly = parameter1;
		fixture.SetOnly = parameter2;

		var actual = () =>
		{
			var verify = It<decimal>.Any();
			DependencyServiceMock.VerifySetSetOnly(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.SetOnly.set = any to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.SetOnly.set = 123
			- 2: IPrimitiveDependencyService.SetOnly.set = 234
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		const decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.SetOnly = parameter1;
		fixture.SetOnly = parameter2;

		DependencyServiceMock.VerifySetSetOnly(parameter1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.SetOnly.set = Decimal to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.SetOnly.set = 234
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.SetOnly = parameter1;
		fixture.SetOnly = parameter2;

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.SetSetOnly(parameter1);
			ctx.DependencyServiceMock.SetSetOnly(parameter2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		const decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.SetOnly = parameter1;
		fixture.SetOnly = parameter2;

		VerifyInSequence(static ctx =>
		{
			It<decimal> verify1 = It<decimal>.Equivalent(parameter1), verify2 = It<decimal>.Equivalent(parameter2);
			ctx.DependencyServiceMock.SetSetOnly(verify1);
			ctx.DependencyServiceMock.SetSetOnly(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		const decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.SetOnly = parameter1;
		fixture.SetOnly = parameter2;

		VerifyInSequence(static ctx =>
		{
			var verify = It<decimal>.Any();
			ctx.DependencyServiceMock.SetSetOnly(verify);
			ctx.DependencyServiceMock.SetSetOnly(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		const decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.SetOnly = parameter1;
		fixture.SetOnly = parameter2;

		VerifyInSequence(static ctx =>
		{
			It<decimal> verify1 = It<decimal>.Where(x => x < 200m), verify2 = It<decimal>.Where(x => x > 200m);
			ctx.DependencyServiceMock.SetSetOnly(verify1);
			ctx.DependencyServiceMock.SetSetOnly(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		const decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.SetOnly = parameter1;
		fixture.SetOnly = parameter2;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.SetSetOnly(parameter2);
			ctx.DependencyServiceMock.SetSetOnly(parameter1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.SetOnly.set = 123 to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.SetOnly.set = 123
			- 2: IPrimitiveDependencyService.SetOnly.set = 234
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhere()
	{
		const decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.SetOnly = parameter1;
		fixture.SetOnly = parameter2;

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<decimal> verify1 = It<decimal>.Where(x => x > 200m), verify2 = It<decimal>.Where(x => x < 200m);
			ctx.DependencyServiceMock.SetSetOnly(verify1);
			ctx.DependencyServiceMock.SetSetOnly(verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.SetOnly.set = where(predicate) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.SetOnly.set = 123
			- 2: IPrimitiveDependencyService.SetOnly.set = 234
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const decimal parameter1 = 123m, parameter2 = 234m;

		var fixture = CreateFixture();
		fixture.SetOnly = parameter1;
		fixture.SetOnly = parameter2;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.SetSetOnly(parameter1);
			ctx.DependencyServiceMock.Return();
			ctx.DependencyServiceMock.SetSetOnly(parameter2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Return() to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.SetOnly.set = 123
			- 2: IPrimitiveDependencyService.SetOnly.set = 234
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const decimal setValue = 123m;

		DependencyServiceMock
			.SetupSetSetOnly()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { fixture.SetOnly = setValue; };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { fixture.SetOnly = setValue; };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { fixture.SetOnly = setValue; };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifySetSetOnly(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const decimal setValue = 123m;
		var callback = 0;

		DependencyServiceMock
			.SetupSetSetOnly()
			.Callback(x => callback += (int)x)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		fixture.SetOnly = setValue;

		var actual2 = () => { fixture.SetOnly = setValue; };
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => { fixture.SetOnly = setValue; };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => { fixture.SetOnly = setValue; };
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue, callback);

		DependencyServiceMock.VerifySetSetOnly(setValue, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const decimal setValue = 123m;
		var callback = 0;

		DependencyServiceMock
			.SetupSetSetOnly()
			.Callback(x => callback += (int)x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { fixture.SetOnly = setValue; };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { fixture.SetOnly = setValue; };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { fixture.SetOnly = setValue; };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue, callback);

		DependencyServiceMock.VerifySetSetOnly(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const decimal setValue = 123m;
		var callback = 0;

		DependencyServiceMock
			.SetupSetSetOnly()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += (int)x);

		var fixture = CreateFixture();

		var actual1 = () => { fixture.SetOnly = setValue; };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { fixture.SetOnly = setValue; };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { fixture.SetOnly = setValue; };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * setValue, callback);

		DependencyServiceMock.VerifySetSetOnly(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const decimal setValue = 123m;
		var callback = 0;

		DependencyServiceMock
			.SetupSetSetOnly()
			.Callback(x => callback += (int)x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += (int)x);

		var fixture = CreateFixture();

		var actual1 = () => { fixture.SetOnly = setValue; };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { fixture.SetOnly = setValue; };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { fixture.SetOnly = setValue; };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * setValue, callback);

		DependencyServiceMock.VerifySetSetOnly(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}
