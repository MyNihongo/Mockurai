namespace MyNihongo.Mockurai.Sample._Generated.InvocationWithSeveralParametersTests;

public sealed class VerifyNoOtherCallsPrimitiveShould : InvocationWithSeveralParametersTestsBase
{
	[Fact]
	public void NotThrowIfNoInvocations()
	{
		var fixture = CreateFixturePrimitive();
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAny()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhere()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedValue()
	{
		var index = new InvocationIndex.Counter();

		const int setupValue1 = 123, setupValue2 = 234;
		It<int> verify1 = setupValue1, verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, setupValue2);
		fixture.Register(index, setupValue1, setupValue2);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyWhere1()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyWhere2()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyValue1()
	{
		var index = new InvocationIndex.Counter();

		const int setupValue1 = 123;
		It<int> verify1 = setupValue1, verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, 234);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyValue2()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		It<int> verify1 = It<int>.Any(), verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, setupValue2);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereValue1()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, setupValue2);
		fixture.Register(index, 234, setupValue2);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereValue2()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue1 = 123;
		It<int> verify1 = setupValue1, verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, 234);
		fixture.Register(index, setupValue1, 345);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfNotVerified1()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);
		fixture.Register(index, 345, 456);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(int, int) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod(123, 234)
			- 2: MyClass.MyMethod(234, 345)
			- 3: MyClass.MyMethod(345, 456)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerified2()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);
		fixture.Register(index, 345, 456);
		fixture.Verify(It<int>.Any().ValueSetup, It<int>.Value(234).ValueSetup, Times.Once());

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(int, int) to be verified, but the following invocations have not been verified:
			- 2: MyClass.MyMethod(234, 345)
			- 3: MyClass.MyMethod(345, 456)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerified3()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);
		fixture.Register(index, 345, 456);
		fixture.Verify(It<int>.Value(234).ValueSetup, It<int>.Value(345).ValueSetup, Times.Once());
		fixture.Verify(It<int>.Where(x => x > 300).ValueSetup, It<int>.Value(456).ValueSetup, Times.Once());

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(int, int) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod(123, 234)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void NotThrowVerifiedAnyWithIndex()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereWithIndex()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedValueWithIndex()
	{
		var index = new InvocationIndex.Counter();

		const int setupValue1 = 123, setupValue2 = 234;
		It<int> verify1 = setupValue1, verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, setupValue2);
		fixture.Register(index, setupValue1, setupValue2);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyWhereWithIndex1()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyWhereWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyValueWithIndex1()
	{
		var index = new InvocationIndex.Counter();

		const int setupValue1 = 123;
		It<int> verify1 = setupValue1, verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, 234);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyValueWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		It<int> verify1 = It<int>.Any(), verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, setupValue2);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereValueWithIndex1()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, setupValue2);
		fixture.Register(index, 234, setupValue2);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereValueWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue1 = 123;
		It<int> verify1 = setupValue1, verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, 234);
		fixture.Register(index, setupValue1, 345);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfNotVerifiedWithIndex1()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);
		fixture.Register(index, 345, 456);
		fixture.Verify(It<int>.Any().ValueSetup, It<int>.Value(345).ValueSetup, 2L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(int, int) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod(123, 234)
			- 3: MyClass.MyMethod(345, 456)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerifiedWithIndex2()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);
		fixture.Register(index, 345, 456);
		fixture.Verify(It<int>.Value(123).ValueSetup, It<int>.Value(234).ValueSetup, 1L);
		fixture.Verify(It<int>.Where(x => x > 300).ValueSetup, It<int>.Value(456).ValueSetup, 3L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(int, int) to be verified, but the following invocations have not been verified:
			- 2: MyClass.MyMethod(234, 345)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
