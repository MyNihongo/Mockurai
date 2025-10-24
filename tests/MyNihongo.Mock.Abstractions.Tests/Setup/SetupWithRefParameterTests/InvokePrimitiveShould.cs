namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupWithRefParameterTests;

public sealed class InvokePrimitiveShould : SetupWithRefParameterTestsBase
{
	[Fact]
	public void ThrowForAnySetup()
	{
		const string errorMessage = nameof(errorMessage);
		var setup = It<int>.Any();

		var fixture = CreateFixture(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var input = 12345678;
		var actual = () => fixture.Invoke(ref input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowForValueSetup()
	{
		const string errorMessage = nameof(errorMessage);
		var input = 12345678;
		var setup = It<int>.Value(input);

		var fixture = CreateFixture(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var actual = () => fixture.Invoke(ref input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void NotThrowForValueSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue = 23456789;
		var setup = It<int>.Value(setupValue);

		var fixture = CreateFixture(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var input = 12345678;
		fixture.Invoke(ref input);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ThrowForWhereSetup(int input)
	{
		const string errorMessage = nameof(errorMessage);
		var setup = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var actual = () => fixture.Invoke(ref input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void NotThrowForWhereSetup(int input)
	{
		const string errorMessage = nameof(errorMessage);
		var setup = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		fixture.Invoke(ref input);
	}

	[Fact]
	public void PrioritiseWhereOverAnyThrowWhere()
	{
		const string errorMessage1 = nameof(errorMessage1);
		var setup1 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		var setup2 = It<int>.Where(static x => x > 10);

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
		fixture.Throws(new InvalidCastException(errorMessage2));

		var input = 12345678;
		var actual = () => fixture.Invoke(ref input);

		var exception = Assert.Throws<InvalidCastException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void PrioritiseWhereOverAnyThrowAny()
	{
		const string errorMessage1 = nameof(errorMessage1);
		var setup1 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		var setup2 = It<int>.Where(static x => x > 10);

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
		fixture.Throws(new InvalidCastException(errorMessage2));

		var input = 10;
		var actual = () => fixture.Invoke(ref input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrowValue()
	{
		const string errorMessage1 = nameof(errorMessage1);
		var setup1 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		var setup2 = It<int>.Where(static x => x > 10);

		var setupValue3 = 12345678;
		const string errorMessage3 = nameof(errorMessage3);
		var setup3 = It<int>.Value(setupValue3);

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3);
		fixture.Throws(new ArrayTypeMismatchException(errorMessage3));

		var actual = () => fixture.Invoke(ref setupValue3);

		var exception = Assert.Throws<ArrayTypeMismatchException>(actual);
		Assert.Equal(errorMessage3, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrowWhere()
	{
		const string errorMessage1 = nameof(errorMessage1);
		var setup1 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		var setup2 = It<int>.Where(static x => x > 10);

		const int setupValue3 = 12345678;
		const string errorMessage3 = nameof(errorMessage3);
		var setup3 = It<int>.Value(setupValue3);

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3);
		fixture.Throws(new ArrayTypeMismatchException(errorMessage3));

		var input = 11;
		var actual = () => fixture.Invoke(ref input);

		var exception = Assert.Throws<InvalidCastException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrowAny()
	{
		const string errorMessage1 = nameof(errorMessage1);
		var setup1 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		var setup2 = It<int>.Where(static x => x > 10);

		const int setupValue3 = 12345678;
		const string errorMessage3 = nameof(errorMessage3);
		var setup3 = It<int>.Value(setupValue3);

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3);
		fixture.Throws(new ArrayTypeMismatchException(errorMessage3));

		var input = 10;
		var actual = () => fixture.Invoke(ref input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void InvokeCallbackForAny()
	{
		var setup = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((ref int x) => callbackValue = x + 1);

		var inputValue = 12345678;
		fixture.Invoke(ref inputValue);

		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForAnyBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		var setup = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((ref int x) => callbackValue = x + 1);
		fixture.Throws(new Exception(expectedMessage));

		var inputValue = 12345678;
		var actual = () => fixture.Invoke(ref inputValue);

		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(expectedMessage, exception.Message);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void UpdateValueInCallbackForAny()
	{
		var setup = It<int>.Any();

		var fixture = CreateFixture(setup);
		fixture.Callback((ref int x) => x += 2);

		var inputValue = 12345678;
		fixture.Invoke(ref inputValue);

		const int expected = 12345680;
		Assert.Equal(expected, inputValue);
	}

	[Fact]
	public void InvokeCallbackForValue()
	{
		var setupValue = 12345678;
		var setup = It<int>.Value(setupValue);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((ref int x) => callbackValue = x + 1);
		fixture.Invoke(ref setupValue);

		Assert.Equal(setupValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForValueBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		const int setupValue = 12345678;
		var setup = It<int>.Value(setupValue);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((ref int x) => callbackValue = x + 1);
		fixture.Throws(new Exception(expectedMessage));

		var inputValue = 12345678;
		var actual = () => fixture.Invoke(ref inputValue);

		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(expectedMessage, exception.Message);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void UpdateValueInCallbackForValue()
	{
		var setupValue = 12345678;
		var setup = It<int>.Value(setupValue);

		var fixture = CreateFixture(setup);
		fixture.Callback((ref int x) => x += 2);
		fixture.Invoke(ref setupValue);

		const int expected = 12345680;
		Assert.Equal(expected, setupValue);
	}

	[Fact]
	public void NotInvokeCallbackForValue()
	{
		const int setupValue = 12345678;
		var setup = It<int>.Value(setupValue);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((ref int x) => callbackValue = x + 1);

		var inputValue = -64713;
		fixture.Invoke(ref inputValue);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotUpdateValueInCallbackForValue()
	{
		const int setupValue = 12345678;
		var setup = It<int>.Value(setupValue);

		var fixture = CreateFixture(setup);
		fixture.Callback((ref int x) => x += 2);

		var inputValue = -64713;
		fixture.Invoke(ref inputValue);

		const int expected = -64713;
		Assert.Equal(expected, inputValue);
	}

	[Fact]
	public void InvokeCallbackForWhere()
	{
		var setup = It<int>.Where(x => x > 10);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((ref int x) => callbackValue = x + 1);

		var inputValue = 12345678;
		fixture.Invoke(ref inputValue);

		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void UpdateValueInCallbackForWhere()
	{
		var setup = It<int>.Where(x => x > 10);

		var fixture = CreateFixture(setup);
		fixture.Callback((ref int x) => x += 2);

		var inputValue = 12345678;
		fixture.Invoke(ref inputValue);

		const int expected = 12345680;
		Assert.Equal(expected, inputValue);
	}

	[Fact]
	public void InvokeCallbackForWhereBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		var setup = It<int>.Where(x => x > 10);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((ref int x) => callbackValue = x + 1);
		fixture.Throws(new Exception(expectedMessage));

		var inputValue = 12345678;
		var actual = () => fixture.Invoke(ref inputValue);

		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(expectedMessage, exception.Message);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void NotInvokeCallbackForWhere()
	{
		var setup = It<int>.Where(x => x > 10);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((ref int x) => callbackValue = x + 1);

		var inputValue = -64713;
		fixture.Invoke(ref inputValue);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotUpdateValueInCallbackForWhere()
	{
		var setup = It<int>.Where(x => x > 10);

		var fixture = CreateFixture(setup);
		fixture.Callback((ref int x) => x += 2);

		var inputValue = -64713;
		fixture.Invoke(ref inputValue);

		const int expected = -64713;
		Assert.Equal(expected, inputValue);
	}

	[Fact]
	public void NotDuplicateSameSetup()
	{
		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Callback((ref int _) => { });
		fixture.Throws(new Exception());
		fixture.Callback((ref int _) => { Debug.WriteLine("output"); });

		const int expected = 1;
		var actual = GetSetupCount(fixture);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void InsertAllSetups()
	{
		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Callback((ref int _) => { });

		fixture.SetupParameter(It<int>.Any());
		fixture.Throws(new Exception());

		fixture.SetupParameter(It<int>.Any());
		fixture.Callback((ref int _) => { Debug.WriteLine("output"); });

		const int expected = 3;
		var actual = GetSetupCount(fixture);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowLastExceptionAny()
	{
		var fixture = CreateFixture<int>();

		fixture.SetupParameter(It<int>.Any());
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameter(It<int>.Any());
		fixture.Throws(new NullReferenceException(expectedMessage));

		var inputValue = 12345678;
		var actual = () => fixture.Invoke(ref inputValue);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowLastExceptionWhere()
	{
		var fixture = CreateFixture<int>();

		fixture.SetupParameter(It<int>.Where(x => x > 10));
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameter(It<int>.Where(x => x > 100));
		fixture.Throws(new NullReferenceException(expectedMessage));

		var inputValue = 12345678;
		var actual = () => fixture.Invoke(ref inputValue);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowLastExceptionValue()
	{
		var setupValue = 12345678;
		var fixture = CreateFixture<int>();

		fixture.SetupParameter(setupValue);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameter(setupValue);
		fixture.Throws(new NullReferenceException(expectedMessage));

		var actual = () => fixture.Invoke(ref setupValue);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		var parameter = 1234;
		var actual1 = () => fixture.Invoke(ref parameter);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(ref parameter);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(ref parameter);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Callback((ref int value) => value = callback++);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		var parameter = 1234;
		var actual1 = () => fixture.Invoke(ref parameter);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(ref parameter);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(ref parameter);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Throws(new COMException(errorMessage1));
		fixture.Callback((ref int value) => value = callback++);
		fixture.Throws(new NullReferenceException(errorMessage2));

		var parameter = 1234;
		var actual1 = () => fixture.Invoke(ref parameter);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(ref parameter);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(ref parameter);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback = 2;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		int callback1 = 10, callback2 = 0;

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Callback((ref int value) => value = callback1++);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Callback((ref int value) => value = callback2++);
		fixture.Throws(new NullReferenceException(errorMessage2));

		var parameter = 1234;
		var actual1 = () => fixture.Invoke(ref parameter);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(ref parameter);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(ref parameter);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback1 = 11, expectedCallback2 = 2;
		Assert.Equal(expectedCallback1, callback1);
		Assert.Equal(expectedCallback2, callback2);
	}
}
