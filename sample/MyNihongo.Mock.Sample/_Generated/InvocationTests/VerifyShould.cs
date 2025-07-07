namespace MyNihongo.Mock.Sample._Generated.InvocationTests;

public sealed class VerifyShould : InvocationTestsBase
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
	[InlineData(1)]
	[InlineData(3)]
	public void ThrowIfVerifyTimesWrong(int expected)
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);

		var action = () => fixture.Verify(Times.Exactly(expected));

		var expectedMessage = $"Expected MyClass#MyMethod() to be called {expected} times, but instead it was called 2 times";
		var exception = Assert.Throws<MockVerifyCountException>(action);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyNoInvocations()
	{
		var fixture = CreateFixture();
		fixture.Verify(Times.Never());
	}

	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	[InlineData(3)]
	public void ThrowIfVerifyTimesWrongForEmpty(int expected)
	{
		var fixture = CreateFixture();

		var action = () => fixture.Verify(Times.Exactly(expected));

		var expectedMessage = $"Expected MyClass#MyMethod() to be called {expected} times, but instead it was called 0 times";
		var exception = Assert.Throws<MockVerifyCountException>(action);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyWithIndex()
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);

		fixture.Verify(1L);
		fixture.Verify(2L);
	}

	[Fact]
	public void ThrowIfVerifyOutsideIndex()
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);

		const long verifyIndex = 3L;
		var action = () => fixture.Verify(verifyIndex);

		const string expectedMessage = "Expected MyClass#MyMethod() to be invoked at index 3, but there are no invocations";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(action);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Theory]
	[InlineData(0L)]
	[InlineData(1L)]
	public void ThrowIfVerifyNoInvocations(long verifyIndex)
	{
		var fixture = CreateFixture();

		var action = () => fixture.Verify(verifyIndex);

		var expectedMessage = $"Expected MyClass#MyMethod() to be invoked at index {verifyIndex}, but there are no invocations";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(action);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
