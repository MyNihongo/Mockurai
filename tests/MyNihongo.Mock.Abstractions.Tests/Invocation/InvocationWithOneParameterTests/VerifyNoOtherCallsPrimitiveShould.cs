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
		var index = new InvocationIndex.Counter();

		var verify = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, 234);
		fixture.Register(index, 345);

		fixture.Verify(verify.ValueSetup, Times.Exactly(3));
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfOnceTimesAny()
	{
		var index = new InvocationIndex.Counter();

		var verify = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);

		fixture.Verify(verify.ValueSetup, Times.Once());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfNeverTimesAny()
	{
		var verify = It<int>.Any();
		var fixture = CreateFixture<int>();

		fixture.Verify(verify.ValueSetup, Times.Never());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfVerifiedExactlyTimesWhere()
	{
		var index = new InvocationIndex.Counter();

		var verify = It<int>.Where(x => x > 0);

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, 234);
		fixture.Register(index, 345);

		fixture.Verify(verify.ValueSetup, Times.Exactly(3));
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfOnceTimesWhere()
	{
		var index = new InvocationIndex.Counter();

		var verify = It<int>.Where(x => x <= 123);

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);

		fixture.Verify(verify.ValueSetup, Times.Once());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfNeverTimesWhere()
	{
		var verify = It<int>.Where(x => x > 0);
		var fixture = CreateFixture<int>();

		fixture.Verify(verify.ValueSetup, Times.Never());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfVerifiedExactlyTimesValue()
	{
		var index = new InvocationIndex.Counter();

		const int verifyValue = 123;
		var verify = It<int>.Value(verifyValue);

		var fixture = CreateFixture<int>();
		fixture.Register(index, verifyValue);
		fixture.Register(index, verifyValue);
		fixture.Register(index, verifyValue);

		fixture.Verify(verify.ValueSetup, Times.Exactly(3));
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfOnceTimesValue()
	{
		var index = new InvocationIndex.Counter();

		const int verifyValue = 123;
		var verify = It<int>.Value(verifyValue);

		var fixture = CreateFixture<int>();
		fixture.Register(index, verifyValue);

		fixture.Verify(verify.ValueSetup, Times.Once());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfNeverTimesValue()
	{
		var verify = It<int>.Value(123);
		var fixture = CreateFixture<int>();

		fixture.Verify(verify.ValueSetup, Times.Never());
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfNotVerified()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, 234);
		fixture.Register(index, 345);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod(123)
			- 2: MyClass.MyMethod(234)
			- 3: MyClass.MyMethod(345)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerifiedWhere1()
	{
		var index = new InvocationIndex.Counter();
		var verify = It<int>.Where(x => x > 300);

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, 234);
		fixture.Register(index, 345);
		fixture.Verify(verify.ValueSetup, Times.Once());

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod(123)
			- 2: MyClass.MyMethod(234)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerifiedWhere2()
	{
		var index = new InvocationIndex.Counter();
		var verify = It<int>.Where(x => x > 200);

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, 234);
		fixture.Register(index, 345);
		fixture.Verify(verify.ValueSetup, Times.Exactly(2));

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod(123)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerifiedValue1()
	{
		var index = new InvocationIndex.Counter();

		const int setupValue = 123;
		var verify = It<int>.Value(setupValue);

		var fixture = CreateFixture<int>();
		fixture.Register(index, setupValue);
		fixture.Register(index, 234);
		fixture.Register(index, 345);
		fixture.Verify(verify.ValueSetup, Times.Once());

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 2: MyClass.MyMethod(234)
			- 3: MyClass.MyMethod(345)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerifiedValue2()
	{
		var index = new InvocationIndex.Counter();

		const int setupValue = 123;
		var verify = It<int>.Value(setupValue);

		var fixture = CreateFixture<int>();
		fixture.Register(index, setupValue);
		fixture.Register(index, 234);
		fixture.Register(index, setupValue);
		fixture.Verify(verify.ValueSetup, Times.Exactly(2));

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 2: MyClass.MyMethod(234)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfFirstIndexNotVerifiedValue()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123;
		var verify1 = It<int>.Value(inputValue1);

		var fixture = CreateFixture<int>();
		fixture.Register(index, inputValue1);
		fixture.Register(index, 234);
		fixture.Verify(verify1.ValueSetup, 1L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 2: MyClass.MyMethod(234)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfLastIndexNotVerifiedValue()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue2 = 234;
		var verify2 = It<int>.Value(inputValue2);

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, inputValue2);
		fixture.Verify(verify2.ValueSetup, 2L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod(123)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfFirstIndexNotVerifiedWhere()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<int>.Where(x => x > 100);

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, 234);
		fixture.Verify(verify1.ValueSetup, 1L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 2: MyClass.MyMethod(234)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfLastIndexNotVerifiedWhere()
	{
		var index = new InvocationIndex.Counter();
		var verify2 = It<int>.Where(x => x > 100);

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, 234);
		fixture.Verify(verify2.ValueSetup, 2L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod(123)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfFirstIndexNotVerifiedAny()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, 234);
		fixture.Verify(verify1.ValueSetup, 1L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 2: MyClass.MyMethod(234)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfLastIndexNotVerifiedAny()
	{
		var index = new InvocationIndex.Counter();
		var verify2 = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, 234);
		fixture.Verify(verify2.ValueSetup, 2L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(Int32) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod(123)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedValue()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;
		It<int> verify1 = inputValue1, verify2 = inputValue2;

		var fixture = CreateFixture<int>();
		fixture.Register(index, inputValue1);
		fixture.Register(index, inputValue2);
		fixture.Verify(verify1.ValueSetup, 1L);
		fixture.Verify(verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedWhere()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Where(x => x < 200), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, 234);
		fixture.Verify(verify1.ValueSetup, 1L);
		fixture.Verify(verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedAny()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, 234);
		fixture.Verify(verify1.ValueSetup, 1L);
		fixture.Verify(verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedWithOffsetValue()
	{
		var index = new InvocationIndex.Counter(100L);
		const int inputValue1 = 123, inputValue2 = 234;
		It<int> verify1 = inputValue1, verify2 = inputValue2;

		var fixture = CreateFixture<int>();
		fixture.Register(index, inputValue1);
		fixture.Register(index, inputValue2);
		fixture.Verify(verify1.ValueSetup, 1L);
		fixture.Verify(verify2.ValueSetup, 102L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedWithOffsetWhere()
	{
		var index = new InvocationIndex.Counter(100L);
		It<int> verify1 = It<int>.Where(x => x < 200), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, 234);
		fixture.Verify(verify1.ValueSetup, 1L);
		fixture.Verify(verify2.ValueSetup, 102L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowIfIndicesVerifiedWithOffsetAny()
	{
		var index = new InvocationIndex.Counter(100L);
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Any();

		var fixture = CreateFixture<int>();
		fixture.Register(index, 123);
		fixture.Register(index, 234);
		fixture.Verify(verify1.ValueSetup, 1L);
		fixture.Verify(verify2.ValueSetup, 102L);

		fixture.VerifyNoOtherCalls();
	}
}
