namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupWithInParameterTests;

public sealed class InvokePrimitiveShould : SetupWithInParameterTestsBase
{
	[Fact]
	public void ThrowForAnySetup()
	{
		const string errorMessage = nameof(errorMessage);
		var setup = It<int>.Any();

		var fixture = CreateFixture(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int input = 12345678;
		var actual = () => fixture.Invoke(input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowForValueSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const int input = 12345678;
		var setup = It<int>.Value(input);

		var fixture = CreateFixture(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		var actual = () => fixture.Invoke(input);

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

		const int input = 12345678;
		fixture.Invoke(input);
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

		var actual = () => fixture.Invoke(input);

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

		fixture.Invoke(input);
	}

	[Fact]
	public void PrioritiseWhereOverAnyThrowWhere()
	{
		const string errorMessage1 = nameof(errorMessage1);
		var setup1 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		var setup2 = It<int>.Where(static x => x > 10);

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage2));

		const int input = 12345678;
		var actual = () => fixture.Invoke(input);

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
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage2));

		const int input = 10;
		var actual = () => fixture.Invoke(input);

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

		const int setupValue3 = 12345678;
		const string errorMessage3 = nameof(errorMessage3);
		var setup3 = It<int>.Value(setupValue3);

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3.ValueSetup);
		fixture.Throws(new ArrayTypeMismatchException(errorMessage3));

		var actual = () => fixture.Invoke(setupValue3);

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
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3.ValueSetup);
		fixture.Throws(new ArrayTypeMismatchException(errorMessage3));

		const int input = 11;
		var actual = () => fixture.Invoke(input);

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
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3.ValueSetup);
		fixture.Throws(new ArrayTypeMismatchException(errorMessage3));

		const int input = 10;
		var actual = () => fixture.Invoke(input);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void InvokeCallbackForAny()
	{
		var setup = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((in x) => callbackValue = x + 1);

		const int inputValue = 12345678;
		fixture.Invoke(inputValue);

		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForAnyBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		var setup = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((in x) => callbackValue = x + 1);
		fixture.And();
		fixture.Throws(new Exception(expectedMessage));

		const int inputValue = 12345678;
		var actual = () => fixture.Invoke(inputValue);

		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(expectedMessage, exception.Message);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForValue()
	{
		const int setupValue = 12345678;
		var setup = It<int>.Value(setupValue);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((in x) => callbackValue = x + 1);
		fixture.Invoke(setupValue);

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
		fixture.Throws(new Exception(expectedMessage));
		fixture.And();
		fixture.Callback((in x) => callbackValue = x + 1);

		const int inputValue = 12345678;
		var actual = () => fixture.Invoke(inputValue);

		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(expectedMessage, exception.Message);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void NotInvokeCallbackForValue()
	{
		const int setupValue = 12345678;
		var setup = It<int>.Value(setupValue);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((in x) => callbackValue = x + 1);

		const int inputValue = -64713;
		fixture.Invoke(inputValue);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForWhere()
	{
		var setup = It<int>.Where(x => x > 10);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((in x) => callbackValue = x + 1);

		const int inputValue = 12345678;
		fixture.Invoke(inputValue);

		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForWhereBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		var setup = It<int>.Where(x => x > 10);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback((in x) => callbackValue = x + 1);
		fixture.And();
		fixture.Throws(new Exception(expectedMessage));

		const int inputValue = 12345678;
		var actual = () => fixture.Invoke(inputValue);

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
		fixture.Callback((in x) => callbackValue = x + 1);

		const int inputValue = -64713;
		fixture.Invoke(inputValue);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotDuplicateSameSetup()
	{
		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback(_ => { });
		fixture.Throws(new Exception());
		fixture.Callback(_ => { Debug.WriteLine("output"); });

		const int expected = 1;
		var actual = GetSetupCount(fixture);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void InsertAllSetups()
	{
		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback(_ => { });

		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Throws(new Exception());

		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback(_ => { Debug.WriteLine("output"); });

		const int expected = 3;
		var actual = GetSetupCount(fixture);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowLastExceptionAny()
	{
		var fixture = CreateFixture<int>();

		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		const int inputValue = 12345678;
		var actual = () => fixture.Invoke(inputValue);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowLastExceptionWhere()
	{
		var fixture = CreateFixture<int>();

		fixture.SetupParameter(It<int>.Where(x => x > 10).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameter(It<int>.Where(x => x > 100).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		const int inputValue = 12345678;
		var actual = () => fixture.Invoke(inputValue);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowLastExceptionValue()
	{
		const int setupValue = 12345678;
		var fixture = CreateFixture<int>();

		fixture.SetupParameter(It<int>.Value(setupValue).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameter(It<int>.Value(setupValue).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		var actual = () => fixture.Invoke(setupValue);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		const int parameter = 1234;
		var actual1 = () => fixture.Invoke(parameter);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(parameter);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(parameter);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback(_ => callback++);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		const int parameter = 1234;
		fixture.Invoke(parameter);

		var actual2 = () => fixture.Invoke(parameter);
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.Invoke(parameter);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		var actual4 = () => fixture.Invoke(parameter);
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback(_ => callback++);
		fixture.And();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		const int parameter = 1234;
		var actual1 = () => fixture.Invoke(parameter);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(parameter);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(parameter);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Callback(_ => callback++);
		fixture.And();
		fixture.Throws(new NullReferenceException(errorMessage2));

		const int parameter = 1234;
		var actual1 = () => fixture.Invoke(parameter);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(parameter);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(parameter);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback = 2;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		int callback1 = 10, callback2 = 0;

		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback(_ => callback1++);
		fixture.And();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));
		fixture.And();
		fixture.Callback(_ => callback2++);

		const int parameter = 1234;
		var actual1 = () => fixture.Invoke(parameter);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(parameter);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(parameter);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback1 = 11, expectedCallback2 = 2;
		Assert.Equal(expectedCallback1, callback1);
		Assert.Equal(expectedCallback2, callback2);
	}
}
