namespace MyNihongo.Mock.Sample._Generated.InvocationTests;

public sealed class VerifyNoOtherCallsShould : InvocationTestsBase
{
	[Fact]
	public void NotThrowIfNoInvocations()
	{
		var fixture = CreateFixture();
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfVerifiedTimes()
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);
		fixture.Register(ref index);

		fixture.Verify(Times.Exactly(3));
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfVerifiedTimesOnce()
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);

		fixture.Verify(Times.Once());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfIndicesNotVerified()
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod() to be verified, but the following invocations have not been verified
			- index 1
			- index 2
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfFirstIndexNotVerified()
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);
		fixture.Verify(2L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod() to be verified, but the following invocations have not been verified
			- index 1
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfLastIndexNotVerified()
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);
		fixture.Verify(1L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod() to be verified, but the following invocations have not been verified
			- index 2
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void NotThrowIfIndicesVerified()
	{
		var index = 0L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);
		fixture.Verify(1L);
		fixture.Verify(2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedWithOffset()
	{
		var index = 123L;

		var fixture = CreateFixture();
		fixture.Register(ref index);
		fixture.Register(ref index);
		fixture.Verify(124L);
		fixture.Verify(125L);

		fixture.VerifyNoOtherCalls();
	}
}
