namespace MyNihongo.Mock.Sample._Generated.InvocationWithSeveralParametersTests;

public sealed class VerifyPrimitiveShould : InvocationWithSeveralParametersTestsBase
{
	[Fact]
	public void VerifyAny()
	{
		var index = 0L;
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);
		fixture.Register(ref index, 234, 345);

		const int expected = 2;
		fixture.Verify(verify1, verify2, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyWhere()
	{
		var index = 0L;
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);
		fixture.Register(ref index, 234, 345);

		const int expected = 2;
		fixture.Verify(verify1, verify2, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyValue()
	{
		var index = 0L;

		const int setupValue1 = 123, setupValue2 = 234;
		It<int> verify1 = setupValue1, verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, setupValue1, setupValue2);
		fixture.Register(ref index, setupValue1, setupValue2);

		const int expected = 2;
		fixture.Verify(verify1, verify2, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyAnyWhereAll1()
	{
		var index = 0L;
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);
		fixture.Register(ref index, 234, 345);

		const int expected = 2;
		fixture.Verify(verify1, verify2, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyAnyWhereAll2()
	{
		var index = 0L;
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);
		fixture.Register(ref index, 234, 345);

		const int expected = 2;
		fixture.Verify(verify1, verify2, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyAnyWherePartial1()
	{
		var index = 0L;
		It<int> verify1 = It<int>.Where(x => x > 200), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);
		fixture.Register(ref index, 234, 345);

		fixture.Verify(verify1, verify2, Times.Once());
	}

	[Fact]
	public void VerifyAnyWherePartial2()
	{
		var index = 0L;
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Where(x => x > 300);

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);
		fixture.Register(ref index, 234, 345);

		fixture.Verify(verify1, verify2, Times.Once());
	}

	[Fact]
	public void VerifyAnyValue1()
	{
		var index = 0L;

		const int setupValue1 = 123;
		It<int> verify1 = setupValue1, verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, setupValue1, 234);
		fixture.Register(ref index, 234, 345);

		fixture.Verify(verify1, verify2, Times.Once());
	}

	[Fact]
	public void VerifyAnyValue2()
	{
		var index = 0L;
		const int setupValue2 = 234;
		It<int> verify1 = It<int>.Any(), verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);
		fixture.Register(ref index, 234, 345);

		fixture.Verify(verify1, verify2, Times.Once());
	}

	[Fact]
	public void VerifyWhereValueAll1()
	{
		var index = 0L;
		const int setupValue2 = 234;
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, setupValue2);
		fixture.Register(ref index, 234, setupValue2);

		const int expected = 2;
		fixture.Verify(verify1, verify2, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyWhereValueAll2()
	{
		var index = 0L;
		const int setupValue1 = 123;
		It<int> verify1 = setupValue1, verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, setupValue1, 234);
		fixture.Register(ref index, setupValue1, 345);

		const int expected = 2;
		fixture.Verify(verify1, verify2, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyWhereValuePartial1()
	{
		var index = 0L;
		const int setupValue2 = 234;
		It<int> verify1 = It<int>.Where(x => x > 200), verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, setupValue2);
		fixture.Register(ref index, 234, setupValue2);

		fixture.Verify(verify1, verify2, Times.Once());
	}

	[Fact]
	public void VerifyWhereValuePartial2()
	{
		var index = 0L;
		const int setupValue1 = 123;
		It<int> verify1 = setupValue1, verify2 = It<int>.Where(x => x > 300);

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, setupValue1, 234);
		fixture.Register(ref index, setupValue1, 345);

		fixture.Verify(verify1, verify2, Times.Once());
	}

	[Fact]
	public void ThrowIfVerifyAnyWhere1()
	{
		var index = 0L;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Where(x => x > 200), verify2 = It<int>.Any();
			fixture.Verify(verify1, verify2, Times.Once());
		};

		const string exceptionMessage = "Expected MyClass#MyMethod(Int32, Int32) to be called 1 times, but instead it was called 0 times";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyAnyWhere2()
	{
		var index = 0L;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Any(), verify2 = It<int>.Where(x => x < 100);
			fixture.Verify(verify1, verify2, Times.Once());
		};

		const string exceptionMessage = "Expected MyClass#MyMethod(Int32, Int32) to be called 1 times, but instead it was called 0 times";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyAnyValue1()
	{
		var index = 0L;
		const int inputValue1 = 123;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, inputValue1, 234);

		var actual = () =>
		{
			It<int> verify1 = inputValue1 + 1, verify2 = It<int>.Any();
			fixture.Verify(verify1, verify2, Times.Once());
		};

		const string exceptionMessage = "Expected MyClass#MyMethod(Int32, Int32) to be called 1 times, but instead it was called 0 times";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyWhereAny2()
	{
		var index = 0L;
		const int inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, inputValue2);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Any(), verify2 = inputValue2 + 1;
			fixture.Verify(verify1, verify2, Times.Once());
		};

		const string exceptionMessage = "Expected MyClass#MyMethod(Int32, Int32) to be called 1 times, but instead it was called 0 times";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyWhere1()
	{
		var index = 0L;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Where(x => x < 200);
			fixture.Verify(verify1, verify2, Times.Once());
		};

		const string exceptionMessage = "Expected MyClass#MyMethod(Int32, Int32) to be called 1 times, but instead it was called 0 times";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyWhere2()
	{
		var index = 0L;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Where(x => x < 100), verify2 = It<int>.Where(x => x > 200);
			fixture.Verify(verify1, verify2, Times.Once());
		};

		const string exceptionMessage = "Expected MyClass#MyMethod(Int32, Int32) to be called 1 times, but instead it was called 0 times";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyWhere3()
	{
		var index = 0L;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Where(x => x > 200), verify2 = It<int>.Where(x => x < 200);
			fixture.Verify(verify1, verify2, Times.Once());
		};

		const string exceptionMessage = "Expected MyClass#MyMethod(Int32, Int32) to be called 1 times, but instead it was called 0 times";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyValue1()
	{
		var index = 0L;
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, inputValue1, inputValue2);

		var actual = () =>
		{
			It<int> verify1 = inputValue2, verify2 = inputValue1;
			fixture.Verify(verify1, verify2, Times.Once());
		};

		const string exceptionMessage = "Expected MyClass#MyMethod(Int32, Int32) to be called 1 times, but instead it was called 0 times";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyValue2()
	{
		var index = 0L;
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, inputValue1, inputValue2);

		var actual = () =>
		{
			It<int> verify1 = inputValue1, verify2 = inputValue1;
			fixture.Verify(verify1, verify2, Times.Once());
		};

		const string exceptionMessage = "Expected MyClass#MyMethod(Int32, Int32) to be called 1 times, but instead it was called 0 times";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyValue3()
	{
		var index = 0L;
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, inputValue1, inputValue2);

		var actual = () =>
		{
			It<int> verify1 = inputValue2, verify2 = inputValue2;
			fixture.Verify(verify1, verify2, Times.Once());
		};

		const string exceptionMessage = "Expected MyClass#MyMethod(Int32, Int32) to be called 1 times, but instead it was called 0 times";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}
}
