namespace MyNihongo.Mock.Sample._Generated.InvocationWithOneParameterTests;

public sealed class VerifyPrimitiveShould : InvocationWithOneParameterTestsBase
{
	[Fact]
	public void VerifyAny()
	{
		var index = 0L;
		var verify = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		const int expected = 3;
		fixture.Verify(verify, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyWhereAll()
	{
		var index = 0L;
		var verify = It<int>.Where(x => x > 0);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		const int expected = 3;
		fixture.Verify(verify, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyWherePartial()
	{
		var index = 0L;
		var verify = It<int>.Where(x => x > 300);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		fixture.Verify(verify, Times.Once());
	}

	[Fact]
	public void VerifyWhereNone()
	{
		var index = 0L;
		var verify = It<int>.Where(x => x < 0);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		fixture.Verify(verify, Times.Never());
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(3)]
	public void ThrowVerifyTimesWrong(int expected)
	{
		var index = 0L;

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		var actual = () =>
		{
			var verify = It<int>.Where(x => x > 200);
			fixture.Verify(verify, Times.Exactly(expected));
		};

		var expectedMessage = $"Expected MyClass#MyMethod(Int32) to be called {expected} times, but instead it was called 2 times";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyValue()
	{
		var index = 0L;

		const int setupValue = 123;
		var verify = It<int>.Value(setupValue);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, setupValue);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		fixture.Verify(verify, Times.Once());
	}

	[Fact]
	public void VerifyValueExactly()
	{
		var index = 0L;

		const int setupValue = 123;
		var verify = It<int>.Value(setupValue);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, setupValue);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, setupValue);
		fixture.Register(ref index, 345);

		const int expected = 2;
		fixture.Verify(verify, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyValueNever()
	{
		var index = 0L;

		const int setupValue = 84324235;
		var verify = It<int>.Value(setupValue);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		fixture.Verify(verify, Times.Never());
	}

	[Theory]
	[InlineData(0)]
	[InlineData(1)]
	[InlineData(3)]
	public void ThrowVerifyValueTimesWrong(int expected)
	{
		var index = 0L;
		const int setupValue = 123;

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, setupValue);
		fixture.Register(ref index, setupValue);
		fixture.Register(ref index, 345);

		var actual = () =>
		{
			var verify = It<int>.Value(setupValue);
			fixture.Verify(verify, Times.Exactly(expected));
		};

		var expectedMessage = $"Expected MyClass#MyMethod(Int32) to be called {expected} times, but instead it was called 2 times";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyEmpty()
	{
		var verify = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Verify(verify, Times.Never());
	}

	[Fact]
	public void VerifyAnyWithIndex()
	{
		var index = 0L;
		var verify = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		fixture.Verify(verify, 1L);
		fixture.Verify(verify, 2L);
		fixture.Verify(verify, 3L);
	}

	[Fact]
	public void VerifyWhereWithIndex()
	{
		var index = 0L;
		var verify = It<int>.Where(x => x > 200);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		const long expected1 = 3L;
		var actual1 = fixture.Verify(verify, 2L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 4L;
		var actual2 = fixture.Verify(verify, 3L);
		Assert.Equal(expected2, actual2);
	}

	[Fact]
	public void VerifyValueWithIndex()
	{
		var index = 0L;
		const int setupValue = 345;
		var verify = It<int>.Value(setupValue);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, setupValue);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, setupValue);

		const long expected1 = 3L;
		var actual1 = fixture.Verify(verify, 2L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 5L;
		var actual2 = fixture.Verify(verify, 4L);
		Assert.Equal(expected2, actual2);
	}

	[Fact]
	public void ThrowVerifyAnyIndexOutsideRange()
	{
		var index = 0L;

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		const long verifyIndex = 4L;
		var actual = () =>
		{
			var verify = It<int>.Any();
			fixture.Verify(verify, verifyIndex);
		};

		const string expectedMessage = "Expected MyClass#MyMethod(Int32) to be invoked at index 4, but there are no invocations";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyWhereIndexOutsideRange()
	{
		var index = 0L;

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		const long verifyIndex = 2L;
		var actual = () =>
		{
			var verify = It<int>.Where(x => x < 200);
			fixture.Verify(verify, verifyIndex);
		};

		const string expectedMessage = "Expected MyClass#MyMethod(Int32) to be invoked at index 2, but there are no invocations";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyValueIndexOutsideRange()
	{
		var index = 0L;
		const int setupValue = 123;

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, setupValue);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		const long verifyIndex = 2L;
		var actual = () =>
		{
			var verify = It<int>.Value(setupValue);
			fixture.Verify(verify, verifyIndex);
		};

		const string expectedMessage = "Expected MyClass#MyMethod(Int32) to be invoked at index 2, but there are no invocations";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Theory]
	[InlineData(0L)]
	[InlineData(1L)]
	public void ThrowVerifyAnyNoInvocations(long verifyIndex)
	{
		var fixture = CreateFixture<int>();

		var actual = () =>
		{
			var verify = It<int>.Any();
			fixture.Verify(verify, verifyIndex);
		};

		var expectedMessage = $"Expected MyClass#MyMethod(Int32) to be invoked at index {verifyIndex}, but there are no invocations";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Theory]
	[InlineData(0L)]
	[InlineData(1L)]
	public void ThrowVerifyWhereNoInvocations(long verifyIndex)
	{
		var fixture = CreateFixture<int>();

		var actual = () =>
		{
			var verify = It<int>.Where(x => x > 123);
			fixture.Verify(verify, verifyIndex);
		};

		var expectedMessage = $"Expected MyClass#MyMethod(Int32) to be invoked at index {verifyIndex}, but there are no invocations";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Theory]
	[InlineData(0L)]
	[InlineData(1L)]
	public void ThrowVerifyValueNoInvocations(long verifyIndex)
	{
		var fixture = CreateFixture<int>();

		var actual = () =>
		{
			var verify = It<int>.Value(123);
			fixture.Verify(verify, verifyIndex);
		};

		var expectedMessage = $"Expected MyClass#MyMethod(Int32) to be invoked at index {verifyIndex}, but there are no invocations";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyAnyIndexBefore()
	{
		var index = 100L;
		var verify = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);

		const long expected1 = 102L;
		var actual1 = fixture.Verify(verify, 1L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 103L;
		var actual2 = fixture.Verify(verify, 102L);
		Assert.Equal(expected2, actual2);
	}

	[Fact]
	public void VerifyWhereIndexBefore()
	{
		var index = 100L;
		var verify = It<int>.Where(x => x > 200);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		const long expected1 = 103L;
		var actual1 = fixture.Verify(verify, 1L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 104L;
		var actual2 = fixture.Verify(verify, 103L);
		Assert.Equal(expected2, actual2);
	}

	[Fact]
	public void VerifyValueIndexBefore()
	{
		var index = 100L;

		const int verifyValue1 = 234;
		var verify1 = It<int>.Value(verifyValue1);

		const int verifyValue2 = 345;
		var verify2 = It<int>.Value(verifyValue2);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 987);
		fixture.Register(ref index, verifyValue1);
		fixture.Register(ref index, 987);
		fixture.Register(ref index, verifyValue2);

		const long expected1 = 103L;
		var actual1 = fixture.Verify(verify1, 1L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 105L;
		var actual2 = fixture.Verify(verify2, 103L);
		Assert.Equal(expected2, actual2);
	}
}
