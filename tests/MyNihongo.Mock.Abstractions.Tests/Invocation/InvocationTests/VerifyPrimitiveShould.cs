namespace MyNihongo.Mock.Abstractions.Tests.Invocation.InvocationTests;

public sealed class VerifyPrimitiveShould : InvocationTestsBase
{
	[Fact]
	public void VerifyTimes()
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);

		const int expected = 2;
		fixture.Verify(Times.Exactly(expected));
	}

	[Theory]
	[InlineData(0)]
	[InlineData(2)]
	[InlineData(4)]
	public void ThrowIfVerifyTimesWrong(int expected)
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);
		fixture.Register(ref index);

		var actual = () => fixture.Verify(Times.Exactly(expected));

		var expectedMessage = $"Expected MyClass#MyMethod() to be called {expected} times, but instead it was called 3 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyNoInvocations()
	{
		var fixture = CreateFixture();
		fixture.Verify(Times.Never());
	}

	[Fact]
	public void ThrowIfVerifyTimesWrongForEmptySingle()
	{
		const int expected = 1;
		var fixture = CreateFixture();

		var actual = () => fixture.Verify(Times.Exactly(expected));

		const string expectedMessage = $"Expected MyClass#MyMethod() to be called 1 time, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Theory]
	[InlineData(2)]
	[InlineData(3)]
	public void ThrowIfVerifyTimesWrongForEmpty(int expected)
	{
		var fixture = CreateFixture();

		var actual = () => fixture.Verify(Times.Exactly(expected));

		var expectedMessage = $"Expected MyClass#MyMethod() to be called {expected} times, but instead it was called 0 times.";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyWithIndex()
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);

		const long expected1 = 2L;
		var actual1 = fixture.Verify(1L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 3L;
		var actual2 = fixture.Verify(2L);
		Assert.Equal(expected2, actual2);
	}

	[Fact]
	public void ThrowIfVerifyOutsideIndex()
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);

		const long verifyIndex = 3L;
		Action actual = () => fixture.Verify(verifyIndex);

		const string expectedMessage = "Expected MyClass#MyMethod() to be invoked at index 3, but there are no invocations.";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Theory]
	[InlineData(0L)]
	[InlineData(1L)]
	public void ThrowIfVerifyNoInvocations(long verifyIndex)
	{
		var fixture = CreateFixture();

		Action actual = () => fixture.Verify(verifyIndex);

		var expectedMessage = $"Expected MyClass#MyMethod() to be invoked at index {verifyIndex}, but there are no invocations.";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyIndexBefore()
	{
		var index = 100L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);

		const long expected1 = 102L;
		var actual1 = fixture.Verify(1L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 103L;
		var actual2 = fixture.Verify(102L);
		Assert.Equal(expected2, actual2);
	}
}
