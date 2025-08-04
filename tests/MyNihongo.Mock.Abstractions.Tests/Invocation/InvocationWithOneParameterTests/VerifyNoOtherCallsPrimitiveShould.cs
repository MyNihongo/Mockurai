namespace MyNihongo.Mock.Abstractions.Tests.Invocation.InvocationWithOneParameterTests;

public sealed class VerifyNoOtherCallsPrimitiveShould : InvocationWithOneParameterTestsBase
{
	[Fact]
	public void NotThrowIfNoInvocations()
	{
		var fixture = CreateFixture<int>();
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfVerifiedExactlyTimesAny()
	{
		var index = 0L;

		var verify = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		fixture.Verify(verify, Times.Exactly(3));
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfOnceTimesAny()
	{
		var index = 0L;

		var verify = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);

		fixture.Verify(verify, Times.Once());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfNeverTimesAny()
	{
		var verify = It<int>.Any();
		var fixture = CreateFixture<int>();

		fixture.Verify(verify, Times.Never());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfVerifiedExactlyTimesWhere()
	{
		var index = 0L;

		var verify = It<int>.Where(x => x > 0);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		fixture.Verify(verify, Times.Exactly(3));
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfOnceTimesWhere()
	{
		var index = 0L;

		var verify = It<int>.Where(x => x <= 123);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);

		fixture.Verify(verify, Times.Once());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfNeverTimesWhere()
	{
		var verify = It<int>.Where(x => x > 0);
		var fixture = CreateFixture<int>();

		fixture.Verify(verify, Times.Never());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfVerifiedExactlyTimesValue()
	{
		var index = 0L;

		const int verifyValue = 123;
		var verify = It<int>.Value(verifyValue);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, verifyValue);
		fixture.Register(ref index, verifyValue);
		fixture.Register(ref index, verifyValue);

		fixture.Verify(verify, Times.Exactly(3));
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfOnceTimesValue()
	{
		var index = 0L;

		const int verifyValue = 123;
		var verify = It<int>.Value(verifyValue);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, verifyValue);

		fixture.Verify(verify, Times.Once());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfNeverTimesValue()
	{
		var verify = It<int>.Value(123);
		var fixture = CreateFixture<int>();

		fixture.Verify(verify, Times.Never());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfNotVerified()
	{
		var index = 0L;

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 1: 123
			- 2: 234
			- 3: 345
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerifiedWhere1()
	{
		var index = 0L;
		var verify = It<int>.Where(x => x > 300);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);
		fixture.Verify(verify, Times.Once());

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 1: 123
			- 2: 234
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerifiedWhere2()
	{
		var index = 0L;
		var verify = It<int>.Where(x => x > 200);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);
		fixture.Verify(verify, Times.Exactly(2));

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 1: 123
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerifiedValue1()
	{
		var index = 0L;

		const int setupValue = 123;
		var verify = It<int>.Value(setupValue);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, setupValue);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, 345);
		fixture.Verify(verify, Times.Once());

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 2: 234
			- 3: 345
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerifiedValue2()
	{
		var index = 0L;

		const int setupValue = 123;
		var verify = It<int>.Value(setupValue);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, setupValue);
		fixture.Register(ref index, 234);
		fixture.Register(ref index, setupValue);
		fixture.Verify(verify, Times.Exactly(2));

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 2: 234
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfFirstIndexNotVerifiedValue()
	{
		var index = 0L;
		const int inputValue1 = 123;
		var verify1 = It<int>.Value(inputValue1);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, inputValue1);
		fixture.Register(ref index, 234);
		fixture.Verify(verify1, 1L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 2: 234
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfLastIndexNotVerifiedValue()
	{
		var index = 0L;
		const int inputValue2 = 234;
		var verify2 = It<int>.Value(inputValue2);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, inputValue2);
		fixture.Verify(verify2, 2L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 1: 123
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfFirstIndexNotVerifiedWhere()
	{
		var index = 0L;
		var verify1 = It<int>.Where(x => x > 100);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Verify(verify1, 1L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 2: 234
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfLastIndexNotVerifiedWhere()
	{
		var index = 0L;
		var verify2 = It<int>.Where(x => x > 100);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Verify(verify2, 2L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 1: 123
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfFirstIndexNotVerifiedAny()
	{
		var index = 0L;
		var verify1 = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Verify(verify1, 1L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 2: 234
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfLastIndexNotVerifiedAny()
	{
		var index = 0L;
		var verify2 = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Verify(verify2, 2L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string exceptionMessage =
			"""
			Expected MyClass#MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 1: 123
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedValue()
	{
		var index = 0L;
		const int inputValue1 = 123, inputValue2 = 234;
		It<int> verify1 = inputValue1, verify2 = inputValue2;

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, inputValue1);
		fixture.Register(ref index, inputValue2);
		fixture.Verify(verify1, 1L);
		fixture.Verify(verify2, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedWhere()
	{
		var index = 0L;
		It<int> verify1 = It<int>.Where(x => x < 200), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Verify(verify1, 1L);
		fixture.Verify(verify2, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedAny()
	{
		var index = 0L;
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Verify(verify1, 1L);
		fixture.Verify(verify2, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedWithOffsetValue()
	{
		var index = 100L;
		const int inputValue1 = 123, inputValue2 = 234;
		It<int> verify1 = inputValue1, verify2 = inputValue2;

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, inputValue1);
		fixture.Register(ref index, inputValue2);
		fixture.Verify(verify1, 1L);
		fixture.Verify(verify2, 102L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedWithOffsetWhere()
	{
		var index = 100L;
		It<int> verify1 = It<int>.Where(x => x < 200), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Verify(verify1, 1L);
		fixture.Verify(verify2, 102L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedWithOffsetAny()
	{
		var index = 100L;
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(ref index, 123);
		fixture.Register(ref index, 234);
		fixture.Verify(verify1, 1L);
		fixture.Verify(verify2, 102L);

		fixture.VerifyNoOtherCalls();
	}
}
