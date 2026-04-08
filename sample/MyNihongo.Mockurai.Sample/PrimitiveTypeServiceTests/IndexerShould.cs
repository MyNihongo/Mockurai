namespace MyNihongo.Mockurai.Sample.PrimitiveTypeServiceTests;

public sealed class IndexerShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ExecuteWithoutSetup()
	{
		const int key = 123;
		const string value = nameof(value);

		var fixture = CreateFixture();
		_ = fixture[key];
		fixture[key] = value;
	}

	[Fact]
	public void VerifyIfNotCalled()
	{
		DependencyServiceMock.VerifyGetIndexer(It<int>.Any(), Times.Never);
		DependencyServiceMock.VerifySetIndexer(It<int>.Any(), It<string?>.Any(), Times.Never);
	}

	[Fact]
	public void ThrowIfNotCalledGet()
	{
		var actual = () => DependencyServiceMock.VerifyGetIndexer(It<int>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.This[any].get to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotCalledSet()
	{
		var actual = () => DependencyServiceMock.VerifySetIndexer(It<int>.Any(), It<string?>.Any(), Times.Once);

		const string errorMessage = "Expected IPrimitiveDependencyService.This[any].set = any to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueWithSetupGet()
	{
		const int key = 123;
		const string? value = nameof(value);

		DependencyServiceMock
			.SetupGetIndexer(key)
			.Returns(value);

		var actual = CreateFixture()[key];
		Assert.Equal(value, actual);
	}

	[Fact]
	public void ReturnValueWithAnotherSetupGet()
	{
		const int key = 123, anotherKey = 234;
		const string? value = nameof(value);

		DependencyServiceMock
			.SetupGetIndexer(key)
			.Returns(value);

		var actual = CreateFixture()[anotherKey];
		Assert.Empty(actual);
	}

	[Fact]
	public void ReturnValueWithWhereSetupGet()
	{
		const int key = 123;
		const string? value = nameof(value);
		var keySetup = It<int>.Where(x => x < 200);

		DependencyServiceMock
			.SetupGetIndexer(keySetup)
			.Returns(value);

		var actual = CreateFixture()[key];
		Assert.Equal(value, actual);
	}

	[Fact]
	public void ReturnValueWithAnotherWhereSetupGet()
	{
		const int key = 234;
		const string? value = nameof(value);
		var keySetup = It<int>.Where(x => x < 200);

		DependencyServiceMock
			.SetupGetIndexer(keySetup)
			.Returns(value);

		var actual = CreateFixture()[key];
		Assert.Empty(actual);
	}

	[Fact]
	public void ReturnValueWithSetupAnyGet()
	{
		const int key = 123;
		const string? value = nameof(value);

		DependencyServiceMock
			.SetupGetIndexer()
			.Returns(value);

		var actual = CreateFixture()[key];
		Assert.Equal(value, actual);
	}

	[Fact]
	public void ThrowWithSetupGet()
	{
		const int key = 123;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupGetIndexer(key)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()[key];

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupSet()
	{
		const int key = 123;
		const string value = nameof(value);
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupSetIndexer(key, value)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()[key] = value;

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupAnyGet()
	{
		const int key = 123;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupGetIndexer()
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()[key];

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowWithSetupAnySet()
	{
		const int key = 123;
		const string value = nameof(value);
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupSetIndexer()
			.Throws(new InvalidOperationException(errorMessage));

		var actual = () => CreateFixture()[key] = value;

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ExecuteWithAnotherSetupGet()
	{
		const string errorMessage = nameof(errorMessage);
		const int key = 123, anotherKey = 456;

		DependencyServiceMock
			.SetupGetIndexer(key)
			.Throws(new InvalidOperationException(errorMessage));

		var actual = CreateFixture()[anotherKey];
		Assert.Empty(actual);
	}

	[Theory]
	[InlineData("value")]
	[InlineData("anotherValue")]
	public void ExecuteWithAnotherSetupSet1(string anotherValue)
	{
		const int key = 123, anotherKey = 456;
		const string value = nameof(value);
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupSetIndexer(key, value)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()[anotherKey] = anotherValue;
	}

	[Fact]
	public void ExecuteWithAnotherSetupSet2()
	{
		const int key = 123;
		const string value = nameof(value), anotherValue = nameof(anotherValue);
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupSetIndexer(key, value)
			.Throws(new InvalidOperationException(errorMessage));

		CreateFixture()[key] = anotherValue;
	}

	[Fact]
	public void NotTreatZeroAsAnyGet()
	{
		const int key = 0, anotherKey = 1;
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupGetIndexer(key)
			.Throws(new InvalidOperationException(errorMessage));

		var actual1 = () => CreateFixture()[key];

		var exception = Assert.Throws<InvalidOperationException>(actual1);
		Assert.Equal(errorMessage, exception.Message);

		var actual2 = CreateFixture()[anotherKey];
		Assert.Empty(actual2);
	}

	[Fact]
	public void NotTreatZeroAsAnySet()
	{
		const int key = 0, anotherKey = 1;
		const string value = nameof(value);
		const string errorMessage = nameof(errorMessage);

		DependencyServiceMock
			.SetupSetIndexer(key, value)
			.Throws(new InvalidOperationException(errorMessage));

		var actual1 = () => CreateFixture()[key] = value;

		var exception = Assert.Throws<InvalidOperationException>(actual1);
		Assert.Equal(errorMessage, exception.Message);

		CreateFixture()[anotherKey] = value;
	}

	[Fact]
	public void TreatExactMatchesWithMorePriorityGet()
	{
		const int key = 123;
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		DependencyServiceMock
			.SetupGetIndexer()
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupGetIndexer(key)
			.Throws(new ArgumentException(errorMessage2));

		var actual = () => CreateFixture()[key];

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void TreatExactMatchesWithMorePrioritySet()
	{
		const int key = 123;
		const string value = nameof(value);
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		DependencyServiceMock
			.SetupSetIndexer(value: value)
			.Throws(new InvalidOperationException(errorMessage1));

		DependencyServiceMock
			.SetupSetIndexer(key, value)
			.Throws(new ArgumentException(errorMessage2));

		var actual = () => CreateFixture()[key] = value;

		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void VerifyTimesGet()
	{
		const int key1 = 123, key2 = 456;

		var fixture = CreateFixture();
		_ = fixture[key1];
		_ = fixture[key2];

		DependencyServiceMock.VerifyGetIndexer(key1, Times.Once);
		DependencyServiceMock.VerifyGetIndexer(key2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesSet()
	{
		const int key1 = 123, key2 = 456;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		DependencyServiceMock.VerifySetIndexer(key1, value1, Times.Once);
		DependencyServiceMock.VerifySetIndexer(key2, value2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhereGet()
	{
		const int key1 = 123, key2 = 234;
		var verifyKey1 = It<int>.Where(x => x < 200);

		var fixture = CreateFixture();
		_ = fixture[key1];
		_ = fixture[key2];

		DependencyServiceMock.VerifyGetIndexer(verifyKey1, Times.Once);
		DependencyServiceMock.VerifyGetIndexer(key2, Times.AtMost(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesWhereSet()
	{
		const int key1 = 123, key2 = 456;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var verifyKey1 = It<int>.Where(x => x < 200);
		var verifyValue2 = It<string?>.Where(x => x?.EndsWith('2') == true);

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		DependencyServiceMock.VerifySetIndexer(verifyKey1, value1, Times.Once);
		DependencyServiceMock.VerifySetIndexer(key2, verifyValue2, Times.AtLeast(1));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAnyGet()
	{
		const int key1 = 123, key2 = 234;
		var verify = It<int>.Any();

		var fixture = CreateFixture();
		_ = fixture[key1];
		_ = fixture[key2];

		DependencyServiceMock.VerifyGetIndexer(verify, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyTimesAnySet()
	{
		const int key1 = 123, key2 = 456;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var verifyKey = It<int>.Any();
		var verifyValue = It<string?>.Any();

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		DependencyServiceMock.VerifySetIndexer(verifyKey, verifyValue, Times.Exactly(2));
		DependencyServiceMock.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowVerifyTimesGet()
	{
		const int key1 = 123, key2 = 234;

		var fixture = CreateFixture();
		_ = fixture[key1];
		_ = fixture[key2];

		var actual = () =>
		{
			var verify = It<int>.Any();
			DependencyServiceMock.VerifyGetIndexer(verify, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.This[any].get to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.This[123].get
			- 2: IPrimitiveDependencyService.This[234].get
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyTimesSet()
	{
		const int key1 = 123, key2 = 234;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		var actual = () =>
		{
			var verifyKey = It<int>.Any();
			var verifyValue = It<string?>.Any();
			DependencyServiceMock.VerifySetIndexer(verifyKey, verifyValue, Times.AtLeast(3));
		};

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.This[any].set = any to be called at least 3 times, but instead it was called 2 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.This[123].set = "value1"
			- 2: IPrimitiveDependencyService.This[234].set = "value2"
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsGet()
	{
		const int key1 = 123, key2 = 234;

		var fixture = CreateFixture();
		_ = fixture[key1];
		_ = fixture[key2];

		DependencyServiceMock.VerifyGetIndexer(key1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.This[Int32].get to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.This[234].get
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsSet()
	{
		const int key1 = 123, key2 = 234;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		DependencyServiceMock.VerifySetIndexer(key1, value1, Times.Once);

		var actual = () => DependencyServiceMock.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.This[int].set = string? to be verified, but the following invocations have not been verified:
			- 2: IPrimitiveDependencyService.This[234].set = "value2"
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValidSequenceGet()
	{
		const int key1 = 123, key2 = 234;

		var fixture = CreateFixture();
		_ = fixture[key1];
		_ = fixture[key2];

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.GetIndexer(key1);
			ctx.DependencyServiceMock.GetIndexer(key2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceSet()
	{
		const int key1 = 123, key2 = 234;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.SetIndexer(key1, value1);
			ctx.DependencyServiceMock.SetIndexer(key2, value2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalentGet()
	{
		const int key1 = 123, key2 = 234;

		var fixture = CreateFixture();
		_ = fixture[key1];
		_ = fixture[key2];

		VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Equivalent(key1), verify2 = It<int>.Equivalent(key2);
			ctx.DependencyServiceMock.GetIndexer(verify1);
			ctx.DependencyServiceMock.GetIndexer(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceEquivalentSet()
	{
		const int key1 = 123, key2 = 234;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		VerifyInSequence(static ctx =>
		{
			It<int> verifyKey1 = It<int>.Equivalent(key1), verifyKey2 = It<int>.Equivalent(key2);
			It<string?> verifyValue1 = It<string?>.Equivalent(value1), verifyValue2 = It<string?>.Equivalent(value2);
			ctx.DependencyServiceMock.SetIndexer(verifyKey1, verifyValue1);
			ctx.DependencyServiceMock.SetIndexer(verifyKey2, verifyValue2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAnyGet()
	{
		const int key1 = 123, key2 = 234;

		var fixture = CreateFixture();
		_ = fixture[key1];
		_ = fixture[key2];

		VerifyInSequence(static ctx =>
		{
			var verify = It<int>.Any();
			ctx.DependencyServiceMock.GetIndexer(verify);
			ctx.DependencyServiceMock.GetIndexer(verify);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceAnySet()
	{
		const int key1 = 123, key2 = 234;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		VerifyInSequence(static ctx =>
		{
			var verifyKey = It<int>.Any();
			var verifyValue = It<string?>.Any();
			ctx.DependencyServiceMock.SetIndexer(verifyKey, verifyValue);
			ctx.DependencyServiceMock.SetIndexer(verifyKey, verifyValue);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhereGet()
	{
		const int key1 = 123, key2 = 234;

		var fixture = CreateFixture();
		_ = fixture[key1];
		_ = fixture[key2];

		VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Where(x => x < 200), verify2 = It<int>.Where(x => x > 200);
			ctx.DependencyServiceMock.GetIndexer(verify1);
			ctx.DependencyServiceMock.GetIndexer(verify2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void VerifyValidSequenceWhereSet()
	{
		const int key1 = 123, key2 = 234;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		VerifyInSequence(static ctx =>
		{
			It<int> verifyKey1 = It<int>.Where(x => x < 200), verifyKey2 = It<int>.Where(x => x > 200);
			It<string?> verifyValue1 = It<string?>.Where(x => x?.EndsWith('1') == true), verifyValue2 = It<string?>.Where(x => x?.EndsWith('2') == true);
			ctx.DependencyServiceMock.SetIndexer(verifyKey1, verifyValue1);
			ctx.DependencyServiceMock.SetIndexer(verifyKey2, verifyValue2);
		});
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowInvalidSequenceGet()
	{
		const int key1 = 123, key2 = 234;

		var fixture = CreateFixture();
		_ = fixture[key1];
		_ = fixture[key2];

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.GetIndexer(key2);
			ctx.DependencyServiceMock.GetIndexer(key1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(in 123) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameter(in 123)
			- 2: IPrimitiveDependencyService.InvokeWithParameter(in 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceSet1()
	{
		const int key1 = 123, key2 = 234;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.SetIndexer(key2, value1);
			ctx.DependencyServiceMock.SetIndexer(key1, value2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(in 123) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameter(in 123)
			- 2: IPrimitiveDependencyService.InvokeWithParameter(in 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceSet2()
	{
		const int key1 = 123, key2 = 234;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.SetIndexer(key1, value2);
			ctx.DependencyServiceMock.SetIndexer(key1, value1);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(in 123) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameter(in 123)
			- 2: IPrimitiveDependencyService.InvokeWithParameter(in 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhereGet()
	{
		const int key1 = 123, key2 = 234;

		var fixture = CreateFixture();
		_ = fixture[key1];
		_ = fixture[key2];

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<int> verify1 = It<int>.Where(x => x > 200), verify2 = It<int>.Where(x => x < 200);
			ctx.DependencyServiceMock.GetIndexer(verify1);
			ctx.DependencyServiceMock.GetIndexer(verify2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(in where(predicate)) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameter(in 123)
			- 2: IPrimitiveDependencyService.InvokeWithParameter(in 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhereSet1()
	{
		const int key1 = 123, key2 = 234;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<int> verifyKey1 = It<int>.Where(x => x > 200), verifyKey2 = It<int>.Where(x => x < 200);
			It<string?> verifyValue1 = It<string?>.Where(x => x?.EndsWith('1') == false), verifyValue2 = It<string?>.Where(x => x?.EndsWith('2') == false);
			ctx.DependencyServiceMock.SetIndexer(verifyKey1, verifyValue1);
			ctx.DependencyServiceMock.SetIndexer(verifyKey2, verifyValue2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(in where(predicate)) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameter(in 123)
			- 2: IPrimitiveDependencyService.InvokeWithParameter(in 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceWhereSet2()
	{
		const int key1 = 123, key2 = 234;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		var actual = () => VerifyInSequence(static ctx =>
		{
			It<int> verifyKey1 = It<int>.Where(x => x > 200), verifyKey2 = It<int>.Where(x => x < 200);
			It<string?> verifyValue1 = It<string?>.Where(x => x?.EndsWith('1') == false), verifyValue2 = It<string?>.Where(x => x?.EndsWith('2') == false);
			ctx.DependencyServiceMock.SetIndexer(verifyKey1, verifyValue1);
			ctx.DependencyServiceMock.SetIndexer(verifyKey2, verifyValue2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(in where(predicate)) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameter(in 123)
			- 2: IPrimitiveDependencyService.InvokeWithParameter(in 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequenceGet()
	{
		const int key1 = 123, key2 = 234;

		var fixture = CreateFixture();
		_ = fixture[key1];
		_ = fixture[key2];

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.GetIndexer(key1);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(key1, key2);
			ctx.DependencyServiceMock.GetIndexer(key2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 234) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameter(in 123)
			- 2: IPrimitiveDependencyService.InvokeWithParameter(in 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidMethodInSequenceSet()
	{
		const int key1 = 123, key2 = 234;
		const string value1 = nameof(value1), value2 = nameof(value2);

		var fixture = CreateFixture();
		fixture[key1] = value1;
		fixture[key2] = value2;

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.SetIndexer(key1, value1);
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(key1, key2);
			ctx.DependencyServiceMock.SetIndexer(key2, value2);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 234) to be invoked at index 2, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.InvokeWithParameter(in 123)
			- 2: IPrimitiveDependencyService.InvokeWithParameter(in 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowDifferentExceptionsGet()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int key = 2;

		DependencyServiceMock
			.SetupGetIndexer()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture[key];
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture[key];
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture[key];
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifyGetIndexer(key, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsSet()
	{
		const int key = 2;
		const string value = nameof(value);
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		DependencyServiceMock
			.SetupSetIndexer()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture[key] = value;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture[key] = value;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture[key] = value;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		DependencyServiceMock.VerifySetIndexer(key, value, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackGet1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int key = 2;
		var callback = 0;

		DependencyServiceMock
			.SetupGetIndexer()
			.Callback(x => callback += x)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		_ = fixture[key];

		var actual2 = () => fixture[key];
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture[key];
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture[key];
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(key, callback);

		DependencyServiceMock.VerifyGetIndexer(key, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackSet1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int key = 2;
		const string value = nameof(value);
		var callback = 0;

		DependencyServiceMock
			.SetupSetIndexer()
			.Callback((x, _) => callback += x)
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		fixture[key] = value;

		var actual2 = () => fixture[key] = value;
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture[key] = value;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture[key] = value;
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		Assert.Equal(key, callback);

		DependencyServiceMock.VerifySetIndexer(key, value, Times.Exactly(4));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackGet2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int key = 2;
		var callback = 0;

		DependencyServiceMock
			.SetupGetIndexer()
			.Callback(x => callback += x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture[key];
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture[key];
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture[key];
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(key, callback);

		DependencyServiceMock.VerifyGetIndexer(key, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackSet2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int key = 2;
		const string value = nameof(value);
		var callback = 0;

		DependencyServiceMock
			.SetupSetIndexer()
			.Callback((x, _) => callback += x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2));

		var fixture = CreateFixture();

		var actual1 = () => fixture[key] = value;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture[key] = value;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture[key] = value;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(key, callback);

		DependencyServiceMock.VerifySetIndexer(key, value, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackGet3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int key = 2;
		var callback = 0;

		DependencyServiceMock
			.SetupGetIndexer()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x);

		var fixture = CreateFixture();

		var actual1 = () => fixture[key];
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture[key];
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture[key];
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * key, callback);

		DependencyServiceMock.VerifyGetIndexer(key, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackSet3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int key = 2;
		const string value = nameof(value);
		var callback = 0;

		DependencyServiceMock
			.SetupSetIndexer()
			.Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, _) => callback += x);

		var fixture = CreateFixture();

		var actual1 = () => fixture[key] = value;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture[key] = value;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture[key] = value;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(2 * key, callback);

		DependencyServiceMock.VerifySetIndexer(key, value, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackGet4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int key = 2;
		var callback = 0;

		DependencyServiceMock
			.SetupGetIndexer()
			.Callback(x => callback += x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback(x => callback += x);

		var fixture = CreateFixture();

		var actual1 = () => fixture[key];
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture[key];
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture[key];
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * key, callback);

		DependencyServiceMock.VerifyGetIndexer(key, Times.Exactly(3));
		VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallbackSet4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		const int key = 2;
		const string value = nameof(value);
		var callback = 0;

		DependencyServiceMock
			.SetupSetIndexer()
			.Callback((x, _) => callback += x).And().Throws(new COMException(errorMessage1))
			.Throws(new NullReferenceException(errorMessage2)).And().Callback((x, _) => callback += x);

		var fixture = CreateFixture();

		var actual1 = () => fixture[key] = value;
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture[key] = value;
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture[key] = value;
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Assert.Equal(3 * key, callback);

		DependencyServiceMock.VerifySetIndexer(key, value, Times.Exactly(3));
		VerifyNoOtherCalls();
	}
}
