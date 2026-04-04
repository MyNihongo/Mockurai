namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class ReturnWithParameterGenericShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ReturnValueWithoutSetup()
	{
		const string setupValue = nameof(setupValue);

		var actual = CreateFixture()
			.ReturnWithParameterGeneric<float>(setupValue);

		Assert.Equal(0f, actual);
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyGenericServiceMock.VerifyReturnWithParameter<string, string>(It<string>.Any(), Times.Never);
		DependencyGenericServiceMock.VerifyReturnWithParameter<string, int>(It<string>.Any(), Times.Never);
		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(It<string>.Any(), Times.Never);
		DependencyGenericServiceMock.VerifyReturnWithParameter<string, decimal>(It<string>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalled()
	{
		var actual = () => DependencyGenericServiceMock.VerifyReturnWithParameter<string, int>(It<string>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService<String>.ReturnWithParameter<Int32>(any) to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetup()
	{
		const string setupValue = "Okayama Issei";
		const float expected = 123f;

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>(setupValue)
			.Returns(expected);

		var actual = CreateFixture()
			.ReturnWithParameterGeneric<float>(setupValue);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithSetupAny()
	{
		const string setupValue = "Okayama Issei";
		const float expected = 123f;

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>(It<string>.Any())
			.Returns(expected);

		var actual = CreateFixture()
			.ReturnWithParameterGeneric<float>(setupValue);

		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnValueWithAnotherSetup()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		const float expected = 123f;

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>(setupValue1)
			.Returns(expected);

		var actual = CreateFixture()
			.ReturnWithParameterGeneric<float>(setupValue2);

		Assert.Equal(0f, actual);
	}

	[Fact]
	public void ThrowWithSetup()
	{
		const string setupValue = nameof(setupValue);
		const string errorMessage = nameof(errorMessage);

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>(setupValue)
			.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => CreateFixture()
			.ReturnWithParameterGeneric<float>(setupValue);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueForThrowsWithAnotherSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>(setupValue1)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()
			.ReturnWithParameterGeneric<float>(setupValue2);

		Assert.Equal(0f, actual);
	}

	[Fact]
	public void VerifyTimes()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(setupValue1);
		fixture.ReturnWithParameterGeneric<float>(setupValue2);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setupValue1, Times.Once);
		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setupValue2, Times.AtLeast(1));
		DependencyGenericServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhere()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var verify1 = It<string>.Where(x => x.EndsWith('1'));

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(setupValue1);
		fixture.ReturnWithParameterGeneric<float>(setupValue2);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(verify1, Times.Once);
		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setupValue2, Times.AtMost(1));
		DependencyGenericServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAny()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var verify = It<string>.Any();

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(setupValue1);
		fixture.ReturnWithParameterGeneric<float>(setupValue2);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(verify, Times.Exactly(2));
		DependencyGenericServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimes()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(setupValue1);
		fixture.ReturnWithParameterGeneric<float>(setupValue2);

		var actual = () =>
		{
			var verify = It<string>.Any();
			DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.ReturnWithParameter<Single>(any) to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.ReturnWithParameter<Single>("setupValue1")
			- 2: IPrimitiveDependencyService<String>.ReturnWithParameter<Single>("setupValue2")
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(setupValue1);
		fixture.ReturnWithParameterGeneric<float>(setupValue2);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setupValue1, Times.Once);

		var actual = () => DependencyGenericServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.ReturnWithParameter<Single>(String) to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService<String>.ReturnWithParameter<Single>("setupValue2")
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequence()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(setupValue1);
		fixture.ReturnWithParameterGeneric<float>(setupValue2);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(setupValue1);
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(setupValue2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalent()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(setupValue1);
		fixture.ReturnWithParameterGeneric<float>(setupValue2);

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Equivalent(setupValue1), verify2 = It<string>.Equivalent(setupValue2);
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(verify1);
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAny()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(setupValue1);
		fixture.ReturnWithParameterGeneric<float>(setupValue2);

		VerifyInSequence(static ctx =>
		{
			var verify = It<string>.Any();
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(verify);
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhere()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(setupValue1);
		fixture.ReturnWithParameterGeneric<float>(setupValue2);

		VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.EndsWith('1')), verify2 = It<string>.Where(x => x.EndsWith('2'));
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(verify1);
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequence()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(setupValue1);
		fixture.ReturnWithParameterGeneric<float>(setupValue2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(setupValue2);
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(setupValue1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.ReturnWithParameter<Single>("setupValue1") to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.ReturnWithParameter<Single>("setupValue1")
			- 2: IPrimitiveDependencyService<String>.ReturnWithParameter<Single>("setupValue2")
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhere()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(setupValue1);
		fixture.ReturnWithParameterGeneric<float>(setupValue2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<string> verify1 = It<string>.Where(x => x.EndsWith('2')), verify2 = It<string>.Where(x => x.EndsWith('1'));
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(verify1);
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.ReturnWithParameter<Single>(where(predicate)) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.ReturnWithParameter<Single>("setupValue1")
			- 2: IPrimitiveDependencyService<String>.ReturnWithParameter<Single>("setupValue2")
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequence()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(setupValue1);
		fixture.ReturnWithParameterGeneric<float>(setupValue2);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(setupValue1);
			ctx.DependencyGenericServiceMock.InvokeWithSeveralParameters<string, int>(123, "some value");
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(setupValue2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<Int32>(123, "some value") to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService<String>.ReturnWithParameter<Single>("setupValue1")
			- 2: IPrimitiveDependencyService<String>.ReturnWithParameter<Single>("setupValue2")
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnDifferentValues()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;
		const string setValue = nameof(setValue);

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>()
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameterGeneric<float>(setValue));

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback1()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;
		const string setValue = nameof(setValue);
		float callback = 0;

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>()
			.Callback(x => callback += x.Length)
			.Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(0f, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setupValue1, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setValue.Length, callback);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setValue, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback2()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;
		const string setValue = nameof(setValue);
		float callback = 0;

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>()
			.Callback(x => callback += x.Length).And().Returns(setupValue1)
			.Returns(setupValue2);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setValue.Length, callback);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback3()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;
		const string setValue = nameof(setValue);
		float callback = 0;

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>()
			.Returns(setupValue1)
			.Returns(setupValue2).And().Callback(x => callback += x.Length);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(2 * setValue.Length, callback);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback4()
	{
		const float setupValue1 = 123f, setupValue2 = 321f;
		const string setValue = nameof(setValue);
		float callback = 0;

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>()
			.Callback(x => callback += x.Length).And().Returns(setupValue1)
			.Returns(setupValue2).And().Callback(x => callback += x.Length);

		var fixture = CreateFixture();

		Assert.Equal(setupValue1, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setupValue2, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(3 * setValue.Length, callback);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		float callback = 0;

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>()
			.Callback(x => callback += x.Length)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		Assert.Equal(0f, fixture.ReturnWithParameterGeneric<float>(setValue));

		var actual2 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(setValue.Length, callback);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setValue, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		float callback = 0;

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>()
			.Callback(x => callback += x.Length).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(setValue.Length, callback);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		float callback = 0;

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x.Length);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * setValue.Length, callback);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const string setValue = nameof(setValue);
		float callback = 0;

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>()
			.Callback(x => callback += x.Length).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x.Length);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * setValue.Length, callback);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage);
		const float setupValue = 123f;
		const string setValue = nameof(setValue);

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>()
			.Throws(new COMException(errorMessage))
			.Returns(setupValue);

		var fixture = CreateFixture();

		var actual1 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		Assert.Equal(setupValue, fixture.ReturnWithParameterGeneric<float>(setValue));
		Assert.Equal(setupValue, fixture.ReturnWithParameterGeneric<float>(setValue));

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage);
		const float setupValue = 123f;
		const string setValue = nameof(setValue);

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>()
			.Returns(setupValue)
			.Throws(new COMException(errorMessage));

		var fixture = CreateFixture();

		Assert.Equal(setupValue, fixture.ReturnWithParameterGeneric<float>(setValue));

		var actual2 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception1 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage, exception1.Message);

		var actual3 = () => { _ = fixture.ReturnWithParameterGeneric<float>(setValue); };
		var exception3 = Assert.Throws<COMException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(setValue, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void SetupDifferentTypes()
	{
		const string errorMessage = nameof(errorMessage);
		const float setupValue1 = 321f;
		const int setupValue2 = 234;
		const string parameterValue = nameof(parameterValue);
		var callback = 0f;

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, float>()
			.Returns(setupValue1);

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, int>()
			.Returns(setupValue2);

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, decimal>()
			.Throws(new OutOfMemoryException(errorMessage));

		DependencyGenericServiceMock
			.SetupReturnWithParameter<string, short>()
			.Callback(x => callback += x.Length);

		var fixture = CreateFixture();

		var actual1 = fixture.ReturnWithParameterGeneric<float>(parameterValue);
		Assert.Equal(setupValue1, actual1);

		var actual2 = fixture.ReturnWithParameterGeneric<int>(parameterValue);
		Assert.Equal(setupValue2, actual2);

		var actual3 = () => (object)fixture.ReturnWithParameterGeneric<decimal>(parameterValue);
		var exception = Assert.Throws<OutOfMemoryException>(actual3);
		Assert.Equal(errorMessage, exception.Message);

		fixture.ReturnWithParameterGeneric<short>(parameterValue);
		Assert.Equal(parameterValue.Length, callback);
	}

	[Fact]
	public void VerifyDifferentTypes()
	{
		const string stringParameter = nameof(stringParameter);

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(stringParameter);
		fixture.ReturnWithParameterGeneric<float>(stringParameter);
		fixture.ReturnWithParameterGeneric<int>(stringParameter);
		fixture.ReturnWithParameterGeneric<decimal>(stringParameter);
		fixture.ReturnWithParameterGeneric<string>(stringParameter);
		fixture.ReturnWithParameterGeneric<short>(stringParameter);

		DependencyGenericServiceMock.VerifyReturnWithParameter<string, float>(stringParameter, Times.Exactly(2));
		DependencyGenericServiceMock.VerifyReturnWithParameter<string, int>(stringParameter, Times.Once);
		DependencyGenericServiceMock.VerifyReturnWithParameter<string, decimal>(stringParameter, Times.Once);
		DependencyGenericServiceMock.VerifyReturnWithParameter<string, string>(stringParameter, Times.Once);
		DependencyGenericServiceMock.VerifyReturnWithParameter<string, short>(stringParameter, Times.Once);
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyDifferentTypesInSequence()
	{
		const string stringParameter = nameof(stringParameter);

		var fixture = CreateFixture();
		fixture.ReturnWithParameterGeneric<float>(stringParameter);
		fixture.ReturnWithParameterGeneric<float>(stringParameter);
		fixture.ReturnWithParameterGeneric<string>(stringParameter);
		fixture.ReturnWithParameterGeneric<decimal>(stringParameter);
		fixture.ReturnWithParameterGeneric<int>(stringParameter);
		fixture.ReturnWithParameterGeneric<short>(stringParameter);

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(stringParameter);
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, float>(stringParameter);
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, string>(stringParameter);
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, decimal>(stringParameter);
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, int>(stringParameter);
			ctx.DependencyGenericServiceMock.ReturnWithParameter<string, short>(stringParameter);
		});
		VerifyNoOtherCalls();
	}
}