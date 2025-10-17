namespace MyNihongo.Mock.Abstractions.Tests.Invocation.InvocationTests;

public sealed class VerifyNoOtherCallsPrimitiveShould : InvocationTestsBase
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
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture();
		fixture.Register(index);
		fixture.Register(index);
		fixture.Register(index);

		fixture.Verify(Times.Exactly(3));
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfVerifiedTimesOnce()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture();
		fixture.Register(index);

		fixture.Verify(Times.Once());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfNotVerified()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture();
		fixture.Register(index);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod() to be verified, but the following invocations have not been verified:
			- index 1
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfIndicesNotVerified()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture();
		fixture.Register(index);
		fixture.Register(index);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod() to be verified, but the following invocations have not been verified:
			- index 1
			- index 2
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfFirstIndexNotVerified()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture();
		fixture.Register(index);
		fixture.Register(index);
		fixture.Verify(2L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod() to be verified, but the following invocations have not been verified:
			- index 1
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfLastIndexNotVerified()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture();
		fixture.Register(index);
		fixture.Register(index);
		fixture.Verify(1L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod() to be verified, but the following invocations have not been verified:
			- index 2
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void NotThrowIfIndicesVerified()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture();
		fixture.Register(index);
		fixture.Register(index);
		fixture.Verify(1L);
		fixture.Verify(2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedWithOffset()
	{
		var index = new InvocationIndex.Counter(123L);

		var fixture = CreateFixture();
		fixture.Register(index);
		fixture.Register(index);
		fixture.Verify(124L);
		fixture.Verify(125L);

		fixture.VerifyNoOtherCalls();
	}
}
