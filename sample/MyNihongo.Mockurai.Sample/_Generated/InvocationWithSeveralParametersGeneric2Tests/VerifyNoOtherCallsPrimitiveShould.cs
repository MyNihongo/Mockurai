namespace MyNihongo.Mockurai.Sample._Generated.InvocationWithSeveralParametersGeneric2Tests;

public sealed class VerifyNoOtherCallsPrimitiveShould : InvocationWithSeveralParametersTestsBase
{
	[Fact]
	public void NotThrowIfNoInvocations()
	{
		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAny()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<double>.Any();
		var verify2 = It<decimal>.Any();

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, 234m);
		fixture.Register(index, 234d, 345m);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhere()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<double>.Where(x => x > 100d);
		var verify2 = It<decimal>.Where(x => x > 200m);

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, 234m);
		fixture.Register(index, 234d, 345m);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedValue()
	{
		var index = new InvocationIndex.Counter();

		const double setupValue1 = 123d;
		const decimal setupValue2 = 234m;
		It<double> verify1 = setupValue1;
		It<decimal> verify2 = setupValue2;

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, setupValue1, setupValue2);
		fixture.Register(index, setupValue1, setupValue2);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyWhere1()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<double>.Where(x => x > 100);
		var verify2 = It<decimal>.Any();

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, 234m);
		fixture.Register(index, 234d, 345m);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyWhere2()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<double>.Any();
		var verify2 = It<decimal>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, 234m);
		fixture.Register(index, 234d, 345m);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyValue1()
	{
		var index = new InvocationIndex.Counter();

		const double setupValue1 = 123d;
		It<double> verify1 = setupValue1;
		var verify2 = It<decimal>.Any();

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, setupValue1, 234);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyValue2()
	{
		var index = new InvocationIndex.Counter();
		const decimal setupValue2 = 234;
		var verify1 = It<double>.Any();
		It<decimal> verify2 = setupValue2;

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, setupValue2);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereValue1()
	{
		var index = new InvocationIndex.Counter();
		const decimal setupValue2 = 234m;
		var verify1 = It<double>.Where(x => x > 100);
		It<decimal> verify2 = setupValue2;

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, setupValue2);
		fixture.Register(index, 234d, setupValue2);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereValue2()
	{
		var index = new InvocationIndex.Counter();
		const double setupValue1 = 123d;
		It<double> verify1 = setupValue1;
		var verify2 = It<decimal>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, setupValue1, 234m);
		fixture.Register(index, setupValue1, 345m);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(2));

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfNotVerified1()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, 234m);
		fixture.Register(index, 234d, 345m);
		fixture.Register(index, 345d, 456m);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod<T1, T2>(Double, Decimal) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod<T1, T2>(123, 234)
			- 2: MyClass.MyMethod<T1, T2>(234, 345)
			- 3: MyClass.MyMethod<T1, T2>(345, 456)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerified2()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, 234m);
		fixture.Register(index, 234d, 345m);
		fixture.Register(index, 345d, 456m);
		fixture.Verify(It<double>.Any().ValueSetup, It<decimal>.Value(234m).ValueSetup, Times.Once());

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod<T1, T2>(Double, Decimal) to be verified, but the following invocations have not been verified:
			- 2: MyClass.MyMethod<T1, T2>(234, 345)
			- 3: MyClass.MyMethod<T1, T2>(345, 456)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerified3()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, 234m);
		fixture.Register(index, 234d, 345m);
		fixture.Register(index, 345d, 456m);
		fixture.Verify(It<double>.Value(234d).ValueSetup, It<decimal>.Value(345m).ValueSetup, Times.Once());
		fixture.Verify(It<double>.Where(x => x > 300).ValueSetup, It<decimal>.Value(456m).ValueSetup, Times.Once());

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod<T1, T2>(Double, Decimal) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod<T1, T2>(123, 234)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void NotThrowVerifiedAnyWithIndex()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<double>.Any();
		var verify2 = It<decimal>.Any();

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, 234m);
		fixture.Register(index, 234d, 345m);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereWithIndex()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<double>.Where(x => x > 100d);
		var verify2 = It<decimal>.Where(x => x > 200m);

		var fixture = CreateFixturePrimitive<double, decimal>();
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
		It<double> verify1 = setupValue1;
		It<decimal> verify2 = setupValue2;

		var fixture = CreateFixturePrimitive<double, decimal>();
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
		var verify1 = It<double>.Where(x => x > 100);
		var verify2 = It<decimal>.Any();

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, 234m);
		fixture.Register(index, 234d, 345m);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyWhereWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		var verify1 = It<double>.Any();
		var verify2 = It<decimal>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, 234m);
		fixture.Register(index, 234d, 345m);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyValueWithIndex1()
	{
		var index = new InvocationIndex.Counter();

		const double setupValue1 = 123;
		It<double> verify1 = setupValue1;
		var verify2 = It<decimal>.Any();

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, setupValue1, 234m);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedAnyValueWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		const decimal setupValue2 = 234m;
		var verify1 = It<double>.Any();
		It<decimal> verify2 = setupValue2;

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, setupValue2);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereValueWithIndex1()
	{
		var index = new InvocationIndex.Counter();
		const decimal setupValue2 = 234m;
		var verify1 = It<double>.Where(x => x > 100);
		It<decimal> verify2 = setupValue2;

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, setupValue2);
		fixture.Register(index, 234d, setupValue2);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void NotThrowVerifiedWhereValueWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		const double setupValue1 = 123d;
		It<double> verify1 = setupValue1;
		var verify2 = It<decimal>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, setupValue1, 234m);
		fixture.Register(index, setupValue1, 345m);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);

		fixture.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfNotVerifiedWithIndex1()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, 234m);
		fixture.Register(index, 234d, 345m);
		fixture.Register(index, 345d, 456m);
		fixture.Verify(It<double>.Any().ValueSetup, It<decimal>.Value(345).ValueSetup, 2L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod<T1, T2>(Double, Decimal) to be verified, but the following invocations have not been verified:
			- 1: MyClass.MyMethod<T1, T2>(123, 234)
			- 3: MyClass.MyMethod<T1, T2>(345, 456)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfNotVerifiedWithIndex2()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive<double, decimal>();
		fixture.Register(index, 123d, 234m);
		fixture.Register(index, 234d, 345m);
		fixture.Register(index, 345d, 456m);
		fixture.Verify(It<double>.Value(123d).ValueSetup, It<decimal>.Value(234m).ValueSetup, 1L);
		fixture.Verify(It<double>.Where(x => x > 300).ValueSetup, It<decimal>.Value(456m).ValueSetup, 3L);

		var actual = () => fixture.VerifyNoOtherCalls();

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod<T1, T2>(Double, Decimal) to be verified, but the following invocations have not been verified:
			- 2: MyClass.MyMethod<T1, T2>(234, 345)
			""";

		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
