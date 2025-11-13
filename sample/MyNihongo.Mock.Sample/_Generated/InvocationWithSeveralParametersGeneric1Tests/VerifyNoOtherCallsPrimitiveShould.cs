namespace MyNihongo.Mock.Sample._Generated.InvocationWithSeveralParametersGeneric1Tests;

public sealed class VerifyNoOtherCallsPrimitiveShould : InvocationWithSeveralParametersTestsBase
{
	[Fact]
	public void NotThrowIfNoInvocations()
	{
		var fixture = CreateFixturePrimitive<float>();
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAny()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<float>.Any();
		var verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, 234);
		fixture.Register(index, 234f, 345);
		fixture.Verify(verify1, verify2, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhere()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<float>.Where(x => x > 100f);
		var verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, 234);
		fixture.Register(index, 234f, 345);
		fixture.Verify(verify1, verify2, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedValue()
	{
		var index = new InvocationIndex.Counter();

		const float setupValue1 = 123f;
		const int setupValue2 = 234;
		It<float> verify1 = setupValue1;
		It<int> verify2 = setupValue2;

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, setupValue1, setupValue2);
		fixture.Register(index, setupValue1, setupValue2);
		fixture.Verify(verify1, verify2, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyWhere1()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<float>.Where(x => x > 100f);
		var verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, 234);
		fixture.Register(index, 234f, 345);
		fixture.Verify(verify1, verify2, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyWhere2()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<float>.Any();
		var verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, 234);
		fixture.Register(index, 234f, 345);
		fixture.Verify(verify1, verify2, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyValue1()
	{
		var index = new InvocationIndex.Counter();

		const float setupValue1 = 123f;
		It<float> verify1 = setupValue1;
		var verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, setupValue1, 234);
		fixture.Verify(verify1, verify2, Times.Once());

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyValue2()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		var verify1 = It<float>.Any();
		It<int> verify2 = setupValue2;

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, setupValue2);
		fixture.Verify(verify1, verify2, Times.Once());

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereValue1()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		var verify1 = It<float>.Where(x => x > 100f);
		It<int> verify2 = setupValue2;

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, setupValue2);
		fixture.Register(index, 234f, setupValue2);
		fixture.Verify(verify1, verify2, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereValue2()
	{
		var index = new InvocationIndex.Counter();
		const float setupValue1 = 123f;
		It<float> verify1 = setupValue1;
		var verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, setupValue1, 234);
		fixture.Register(index, setupValue1, 345);
		fixture.Verify(verify1, verify2, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfNotVerified1()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, 234);
		fixture.Register(index, 234f, 345);
		fixture.Register(index, 345f, 456);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod<T>(Single, Int32) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod<T>(123, 234)
			- 2: MyClass.MyMethod<T>(234, 345)
			- 3: MyClass.MyMethod<T>(345, 456)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerified2()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, 234);
		fixture.Register(index, 234f, 345);
		fixture.Register(index, 345f, 456);
		fixture.Verify(It<float>.Any(), 234, Times.Once());

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod<T>(Single, Int32) to be verified, but the following invocations have not been verified:
			- 2: MyClass.MyMethod<T>(234, 345)
			- 3: MyClass.MyMethod<T>(345, 456)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerified3()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, 234);
		fixture.Register(index, 234f, 345);
		fixture.Register(index, 345f, 456);
		fixture.Verify(234f, 345, Times.Once());
		fixture.Verify(It<float>.Where(x => x > 300f), 456, Times.Once());

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod<T>(Single, Int32) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod<T>(123, 234)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void NotThrowVerifiedAnyWithIndex()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<float>.Any();
		var verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, 234);
		fixture.Register(index, 234f, 345);
		fixture.Verify(verify1, verify2, 1L);
		fixture.Verify(verify1, verify2, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereWithIndex()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<float>.Where(x => x > 100f);
		var verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, 234);
		fixture.Register(index, 234f, 345);
		fixture.Verify(verify1, verify2, 1L);
		fixture.Verify(verify1, verify2, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedValueWithIndex()
	{
		var index = new InvocationIndex.Counter();

		const float setupValue1 = 123f;
		const int setupValue2 = 234;
		It<float> verify1 = setupValue1;
		It<int> verify2 = setupValue2;

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, setupValue1, setupValue2);
		fixture.Register(index, setupValue1, setupValue2);
		fixture.Verify(verify1, verify2, 1L);
		fixture.Verify(verify1, verify2, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyWhereWithIndex1()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<float>.Where(x => x > 100f);
		var verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, 234);
		fixture.Register(index, 234f, 345);
		fixture.Verify(verify1, verify2, 1L);
		fixture.Verify(verify1, verify2, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyWhereWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<float>.Any();
		var verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, 234);
		fixture.Register(index, 234f, 345);
		fixture.Verify(verify1, verify2, 1L);
		fixture.Verify(verify1, verify2, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyValueWithIndex1()
	{
		var index = new InvocationIndex.Counter();

		const float setupValue1 = 123f;
		It<float> verify1 = setupValue1;
		var verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, setupValue1, 234);
		fixture.Verify(verify1, verify2, 1L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyValueWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		var verify1 = It<float>.Any();
		It<int> verify2 = setupValue2;

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, setupValue2);
		fixture.Verify(verify1, verify2, 1L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereValueWithIndex1()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		var verify1 = It<float>.Where(x => x > 100);
		It<int> verify2 = setupValue2;

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, setupValue2);
		fixture.Register(index, 234f, setupValue2);
		fixture.Verify(verify1, verify2, 1L);
		fixture.Verify(verify1, verify2, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereValueWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		const float setupValue1 = 123f;
		It<float> verify1 = setupValue1;
		var verify2 = It<int>.Where(x => x > 200f);

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, setupValue1, 234);
		fixture.Register(index, setupValue1, 345);
		fixture.Verify(verify1, verify2, 1L);
		fixture.Verify(verify1, verify2, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfNotVerifiedWithIndex1()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, 234);
		fixture.Register(index, 234f, 345);
		fixture.Register(index, 345f, 456);
		fixture.Verify(It<float>.Any(), 345, 2L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod<T>(Single, Int32) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod<T>(123, 234)
			- 3: MyClass.MyMethod<T>(345, 456)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerifiedWithIndex2()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive<float>();
		fixture.Register(index, 123f, 234);
		fixture.Register(index, 234f, 345);
		fixture.Register(index, 345f, 456);
		fixture.Verify(123, 234, 1L);
		fixture.Verify(It<float>.Where(x => x > 300), 456, 3L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod<T>(Single, Int32) to be verified, but the following invocations have not been verified:
			- 2: MyClass.MyMethod<T>(234, 345)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
