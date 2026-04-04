namespace MyNihongo.Mockurai.Sample._Generated.InvocationWithSeveralParametersTests;

public sealed class VerifyPrimitiveShould : InvocationWithSeveralParametersTestsBase
{
	[Fact]
	public void VerifyAny()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);

		const int expected = 2;
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyWhere()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);

		const int expected = 2;
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyValue()
	{
		var index = new InvocationIndex.Counter();

		const int setupValue1 = 123, setupValue2 = 234;
		It<int> verify1 = setupValue1, verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, setupValue2);
		fixture.Register(index, setupValue1, setupValue2);

		const int expected = 2;
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyAnyWhereAll1()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);

		const int expected = 2;
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyAnyWhereAll2()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);

		const int expected = 2;
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyAnyWherePartial1()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Where(x => x > 200), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);

		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
	}

	[Fact]
	public void VerifyAnyWherePartial2()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Where(x => x > 300);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);

		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
	}

	[Fact]
	public void VerifyAnyValue1()
	{
		var index = new InvocationIndex.Counter();

		const int setupValue1 = 123;
		It<int> verify1 = setupValue1, verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, 234);
		fixture.Register(index, 234, 345);

		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
	}

	[Fact]
	public void VerifyAnyValue2()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		It<int> verify1 = It<int>.Any(), verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, setupValue2);
		fixture.Register(index, 234, 345);

		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
	}

	[Fact]
	public void VerifyWhereValueAll1()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, setupValue2);
		fixture.Register(index, 234, setupValue2);

		const int expected = 2;
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyWhereValueAll2()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue1 = 123;
		It<int> verify1 = setupValue1, verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, 234);
		fixture.Register(index, setupValue1, 345);

		const int expected = 2;
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyWhereValuePartial1()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		It<int> verify1 = It<int>.Where(x => x > 200), verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, setupValue2);
		fixture.Register(index, 234, setupValue2);

		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
	}

	[Fact]
	public void VerifyWhereValuePartial2()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue1 = 123;
		It<int> verify1 = setupValue1, verify2 = It<int>.Where(x => x > 300);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, 234);
		fixture.Register(index, setupValue1, 345);

		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
	}

	[Fact]
	public void VerifyEmptyAny()
	{
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Never());
	}

	[Fact]
	public void VerifyEmptyWhere()
	{
		It<int> verify1 = It<int>.Where(x => x > 0), verify2 = It<int>.Where(x => x < 0);

		var fixture = CreateFixturePrimitive();
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Never());
	}

	[Fact]
	public void VerifyEmptyValue()
	{
		It<int> verify1 = 123, verify2 = 321;

		var fixture = CreateFixturePrimitive();
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Never());
	}

	[Fact]
	public void ThrowIfVerifyAnyWhere1()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Where(x => x > 200), verify2 = It<int>.Any();
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(where(predicate), any) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";

		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyAnyWhere2()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Any(), verify2 = It<int>.Where(x => x < 100);
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(any, where(predicate)) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";

		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyAnyValue1()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, 234);

		var actual = () =>
		{
			It<int> verify1 = inputValue1 + 1, verify2 = It<int>.Any();
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(124, any) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";

		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyWhereAny2()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, inputValue2);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Any(), verify2 = inputValue2 + 1;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(any, 235) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";

		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyWhere1()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Where(x => x < 200);
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(where(predicate), where(predicate)) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";

		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyWhere2()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Where(x => x < 100), verify2 = It<int>.Where(x => x > 200);
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(where(predicate), where(predicate)) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";

		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyWhere3()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Where(x => x > 200), verify2 = It<int>.Where(x => x < 200);
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(where(predicate), where(predicate)) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";

		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyValue1()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, inputValue2);

		var actual = () =>
		{
			It<int> verify1 = inputValue2, verify2 = inputValue1;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(234, 123) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";

		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyValue2()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, inputValue2);

		var actual = () =>
		{
			It<int> verify1 = inputValue1, verify2 = inputValue1;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 123) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";

		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyValue3()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, inputValue2);

		var actual = () =>
		{
			It<int> verify1 = inputValue2, verify2 = inputValue2;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(234, 234) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";

		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyAnyWithIndex()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);

		const long expected1 = 2L;
		var actual1 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 3L;
		var actual2 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);
		Assert.Equal(expected2, actual2);
	}

	[Fact]
	public void VerifyWhereWithIndex()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);

		const long expected1 = 2L;
		var actual1 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 3L;
		var actual2 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);
		Assert.Equal(expected2, actual2);
	}

	[Fact]
	public void VerifyValueWithIndex()
	{
		var index = new InvocationIndex.Counter();

		const int setupValue1 = 123, setupValue2 = 234;
		It<int> verify1 = setupValue1, verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, setupValue2);
		fixture.Register(index, setupValue1, setupValue2);

		const long expected1 = 2L;
		var actual1 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 3L;
		var actual2 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);
		Assert.Equal(expected2, actual2);
	}

	[Fact]
	public void VerifyAnyWhereAllWithIndex1()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);

		const long expected1 = 2L;
		var actual1 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 3L;
		var actual2 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);
		Assert.Equal(expected2, actual2);
	}

	[Fact]
	public void VerifyAnyWhereAllWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);

		const long expected1 = 2L;
		var actual1 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 3L;
		var actual2 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);
		Assert.Equal(expected2, actual2);
	}

	[Fact]
	public void VerifyAnyWherePartialWithIndex1()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Where(x => x > 200), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);

		const long expected = 3L;
		var actual = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyAnyWherePartialWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Where(x => x > 300);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);
		fixture.Register(index, 234, 345);

		const long expected = 3L;
		var actual = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyAnyValueWithIndex1()
	{
		var index = new InvocationIndex.Counter();

		const int setupValue1 = 123;
		It<int> verify1 = setupValue1, verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, 234);
		fixture.Register(index, 234, 345);

		const long expected = 2L;
		var actual = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyAnyValueWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		It<int> verify1 = It<int>.Any(), verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, setupValue2);
		fixture.Register(index, 234, 345);

		const long expected = 2L;
		var actual = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyWhereValueAllWithIndex1()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, setupValue2);
		fixture.Register(index, 234, setupValue2);

		const long expected1 = 2L;
		var actual1 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 3L;
		var actual2 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);
		Assert.Equal(expected2, actual2);
	}

	[Fact]
	public void VerifyWhereValueAllWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue1 = 123;
		It<int> verify1 = setupValue1, verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, 234);
		fixture.Register(index, setupValue1, 345);

		const long expected1 = 2L;
		var actual1 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		Assert.Equal(expected1, actual1);

		const long expected2 = 3L;
		var actual2 = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 2L);
		Assert.Equal(expected2, actual2);
	}

	[Fact]
	public void VerifyWhereValuePartialWithIndex1()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue2 = 234;
		It<int> verify1 = It<int>.Where(x => x > 200), verify2 = setupValue2;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, setupValue2);
		fixture.Register(index, 234, setupValue2);

		const long expected = 3L;
		var actual = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void VerifyWhereValuePartialWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		const int setupValue1 = 123;
		It<int> verify1 = setupValue1, verify2 = It<int>.Where(x => x > 300);

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, setupValue1, 234);
		fixture.Register(index, setupValue1, 345);

		const long expected = 3L;
		var actual = fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowIfVerifyEmptyAnyWithIndex()
	{
		var fixture = CreateFixturePrimitive();

		var actual = () =>
		{
			It<int> verify1 = It<int>.Any(), verify2 = It<int>.Any();
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 0L);
		};

		const string expectedMessage = "Expected MyClass.MyMethod(any, any) to be invoked at index 0, but there are no invocations.";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyEmptyWhereWithIndex()
	{
		var fixture = CreateFixturePrimitive();

		var actual = () =>
		{
			It<int> verify1 = It<int>.Where(x => x > 0), verify2 = It<int>.Where(x => x < 0);
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 0L);
		};

		const string expectedMessage = "Expected MyClass.MyMethod(where(predicate), where(predicate)) to be invoked at index 0, but there are no invocations.";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyEmptyValueWithIndex()
	{
		var fixture = CreateFixturePrimitive();

		var actual = () =>
		{
			It<int> verify1 = 123, verify2 = 321;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 0L);
		};

		const string expectedMessage = "Expected MyClass.MyMethod(123, 321) to be invoked at index 0, but there are no invocations.";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyAnyWhereWithIndex1()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Where(x => x > 200), verify2 = It<int>.Any();
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(where(predicate), any) to be invoked at index 1, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyAnyWhereWithIndex2()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Any(), verify2 = It<int>.Where(x => x < 100);
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(any, where(predicate)) to be invoked at index 1, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyAnyValueWithIndex1()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, 234);

		var actual = () =>
		{
			It<int> verify1 = inputValue1 + 1, verify2 = It<int>.Any();
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(124, any) to be invoked at index 1, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyWhereAnyWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, inputValue2);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Any(), verify2 = inputValue2 + 1;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(any, 235) to be invoked at index 1, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyWhereWithIndex1()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Where(x => x < 200);
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(where(predicate), where(predicate)) to be invoked at index 1, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyWhereWithIndex2()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Where(x => x < 100), verify2 = It<int>.Where(x => x > 200);
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(where(predicate), where(predicate)) to be invoked at index 1, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyWhereWithIndex3()
	{
		var index = new InvocationIndex.Counter();

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, 123, 234);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Where(x => x > 200), verify2 = It<int>.Where(x => x < 200);
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(where(predicate), where(predicate)) to be invoked at index 1, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyValueWithIndex1()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, inputValue2);

		var actual = () =>
		{
			It<int> verify1 = inputValue2, verify2 = inputValue1;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(234, 123) to be invoked at index 1, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyValueWithIndex2()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, inputValue2);

		var actual = () =>
		{
			It<int> verify1 = inputValue1, verify2 = inputValue1;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 123) to be invoked at index 1, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowIfVerifyValueWithIndex3()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, inputValue2);

		var actual = () =>
		{
			It<int> verify1 = inputValue2, verify2 = inputValue2;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, 1L);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(234, 234) to be invoked at index 1, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 234)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyEquivalent()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, inputValue2);
		fixture.Register(index, inputValue1, inputValue2 + 2);

		It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
	}

	[Fact]
	public void ThrowVerifyNotEquivalent1()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1 + 1, inputValue2 + 2);
		fixture.Register(index, inputValue1 + 3, inputValue2 + 4);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 234) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod(124, 236)
			  - parameter1:
			    expected: 123
			    actual: 124
			  - parameter2:
			    expected: 234
			    actual: 236
			- 2: MyClass.MyMethod(126, 238)
			  - parameter1:
			    expected: 123
			    actual: 126
			  - parameter2:
			    expected: 234
			    actual: 238
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNotEquivalent2()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, inputValue2 + 2);
		fixture.Register(index, inputValue1 + 3, inputValue2 + 4);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 234) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 236)
			  - parameter2:
			    expected: 234
			    actual: 236
			- 2: MyClass.MyMethod(126, 238)
			  - parameter1:
			    expected: 123
			    actual: 126
			  - parameter2:
			    expected: 234
			    actual: 238
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNotEquivalent3()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1 + 1, inputValue2);
		fixture.Register(index, inputValue1 + 3, inputValue2 + 4);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 234) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod(124, 234)
			  - parameter1:
			    expected: 123
			    actual: 124
			- 2: MyClass.MyMethod(126, 238)
			  - parameter1:
			    expected: 123
			    actual: 126
			  - parameter2:
			    expected: 234
			    actual: 238
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNotEquivalentOneMatched1()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1 + 1, inputValue2 + 2);
		fixture.Register(index, inputValue1, inputValue2);
		fixture.Register(index, inputValue1 + 3, inputValue2 + 4);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
			const int expected = 2;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(expected));
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 234) to be called 2 times, but instead it was called 1 time.
			Performed invocations:
			- 1: MyClass.MyMethod(124, 236)
			  - parameter1:
			    expected: 123
			    actual: 124
			  - parameter2:
			    expected: 234
			    actual: 236
			- 2: MyClass.MyMethod(123, 234)
			- 3: MyClass.MyMethod(126, 238)
			  - parameter1:
			    expected: 123
			    actual: 126
			  - parameter2:
			    expected: 234
			    actual: 238
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNotEquivalentOneMatched2()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, inputValue2 + 2);
		fixture.Register(index, inputValue1, inputValue2);
		fixture.Register(index, inputValue1 + 3, inputValue2 + 4);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
			const int expected = 2;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(expected));
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 234) to be called 2 times, but instead it was called 1 time.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 236)
			  - parameter2:
			    expected: 234
			    actual: 236
			- 2: MyClass.MyMethod(123, 234)
			- 3: MyClass.MyMethod(126, 238)
			  - parameter1:
			    expected: 123
			    actual: 126
			  - parameter2:
			    expected: 234
			    actual: 238
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNotEquivalentOneMatched3()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1 + 1, inputValue2);
		fixture.Register(index, inputValue1, inputValue2);
		fixture.Register(index, inputValue1 + 3, inputValue2 + 4);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
			const int expected = 2;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, Times.Exactly(expected));
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 234) to be called 2 times, but instead it was called 1 time.
			Performed invocations:
			- 1: MyClass.MyMethod(124, 234)
			  - parameter1:
			    expected: 123
			    actual: 124
			- 2: MyClass.MyMethod(123, 234)
			- 3: MyClass.MyMethod(126, 238)
			  - parameter1:
			    expected: 123
			    actual: 126
			  - parameter2:
			    expected: 234
			    actual: 238
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void VerifyEquivalentIndex()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, inputValue2);
		fixture.Register(index, inputValue1, inputValue2 + 2);

		It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
		const long expected = 1L;
		fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, expected);
	}

	[Fact]
	public void ThrowVerifyNotEquivalentIndex1()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1 + 1, inputValue2 + 2);
		fixture.Register(index, inputValue1 + 3, inputValue2 + 4);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
			const long expected = 3;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, expected);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 234) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(124, 236)
			- 2: MyClass.MyMethod(126, 238)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNotEquivalentIndex2()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, inputValue2 + 2);
		fixture.Register(index, inputValue1 + 3, inputValue2 + 4);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
			const long expected = 3;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, expected);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 234) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 236)
			- 2: MyClass.MyMethod(126, 238)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNotEquivalentIndex3()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1 + 1, inputValue2);
		fixture.Register(index, inputValue1 + 3, inputValue2 + 4);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
			const long expected = 3;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, expected);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 234) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(124, 234)
			- 2: MyClass.MyMethod(126, 238)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNotEquivalentOneMatchedIndex1()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1 + 1, inputValue2 + 2);
		fixture.Register(index, inputValue1, inputValue2);
		fixture.Register(index, inputValue1 + 3, inputValue2 + 4);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
			const long expected = 3L;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, expected);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 234) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(124, 236)
			- 2: MyClass.MyMethod(123, 234)
			- 3: MyClass.MyMethod(126, 238)
			  - parameter1:
			    expected: 123
			    actual: 126
			  - parameter2:
			    expected: 234
			    actual: 238
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNotEquivalentOneMatchedIndex2()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1, inputValue2 + 2);
		fixture.Register(index, inputValue1, inputValue2);
		fixture.Register(index, inputValue1 + 3, inputValue2 + 4);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
			const long expected = 3L;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, expected);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 234) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(123, 236)
			- 2: MyClass.MyMethod(123, 234)
			- 3: MyClass.MyMethod(126, 238)
			  - parameter1:
			    expected: 123
			    actual: 126
			  - parameter2:
			    expected: 234
			    actual: 238
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNotEquivalentOneMatchedIndex3()
	{
		var index = new InvocationIndex.Counter();
		const int inputValue1 = 123, inputValue2 = 234;

		var fixture = CreateFixturePrimitive();
		fixture.Register(index, inputValue1 + 1, inputValue2);
		fixture.Register(index, inputValue1, inputValue2);
		fixture.Register(index, inputValue1 + 3, inputValue2 + 4);

		var actual = () =>
		{
			It<int> verify1 = It<int>.Equivalent(inputValue1), verify2 = It<int>.Equivalent(inputValue2);
			const long expected = 3L;
			fixture.Verify(verify1.ValueSetup, verify2.ValueSetup, expected);
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod(123, 234) to be invoked at index 3, but it has not been called.
			Performed invocations:
			- 1: MyClass.MyMethod(124, 234)
			- 2: MyClass.MyMethod(123, 234)
			- 3: MyClass.MyMethod(126, 238)
			  - parameter1:
			    expected: 123
			    actual: 126
			  - parameter2:
			    expected: 234
			    actual: 238
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
