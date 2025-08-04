namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupWithOneParameterTests;

public sealed class InvokePrimitiveShould : SetupWithOneParameterTestsBase
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
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
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
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
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
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3);
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
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3);
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
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3);
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
		fixture.Callback(x => callbackValue = x + 1);

		const int inputValue = 12345678;
		fixture.Invoke(inputValue);

		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForAnyBeforeThrows()
	{
		const string exceptionMessage = nameof(exceptionMessage);
		var setup = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Throws(new Exception(exceptionMessage));
		fixture.Callback(x => callbackValue = x + 1);

		const int inputValue = 12345678;
		var actual = () => fixture.Invoke(inputValue);

		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForValue()
	{
		const int setupValue = 12345678;
		var setup = It<int>.Value(setupValue);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback(x => callbackValue = x + 1);
		fixture.Invoke(setupValue);

		Assert.Equal(setupValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForValueBeforeThrows()
	{
		const string exceptionMessage = nameof(exceptionMessage);
		const int setupValue = 12345678;
		var setup = It<int>.Value(setupValue);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Throws(new Exception(exceptionMessage));
		fixture.Callback(x => callbackValue = x + 1);

		const int inputValue = 12345678;
		var actual = () => fixture.Invoke(inputValue);

		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void NotInvokeCallbackForValue()
	{
		const int setupValue = 12345678;
		var setup = It<int>.Value(setupValue);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback(x => callbackValue = x + 1);

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
		fixture.Callback(x => callbackValue = x + 1);

		const int inputValue = 12345678;
		fixture.Invoke(inputValue);

		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForWhereBeforeThrows()
	{
		const string exceptionMessage = nameof(exceptionMessage);
		var setup = It<int>.Where(x => x > 10);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Throws(new Exception(exceptionMessage));
		fixture.Callback(x => callbackValue = x + 1);

		const int inputValue = 12345678;
		var actual = () => fixture.Invoke(inputValue);

		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void NotInvokeCallbackForWhere()
	{
		var setup = It<int>.Where(x => x > 10);
		var callbackValue = 0;

		var fixture = CreateFixture(setup);
		fixture.Callback(x => callbackValue = x + 1);

		const int inputValue = -64713;
		fixture.Invoke(inputValue);

		const int expected = 0;
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotDuplicateSameSetup()
	{
		var fixture = CreateFixture<int>();
		fixture.SetupParameter(It<int>.Any());
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
		fixture.SetupParameter(It<int>.Any());
		fixture.Callback(_ => { });

		fixture.SetupParameter(It<int>.Any());
		fixture.Throws(new Exception());

		fixture.SetupParameter(It<int>.Any());
		fixture.Callback(_ => { Debug.WriteLine("output"); });

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

		const string exceptionMessage = nameof(exceptionMessage);
		fixture.SetupParameter(It<int>.Any());
		fixture.Throws(new NullReferenceException(exceptionMessage));

		const int inputValue = 12345678;
		var actual = () => fixture.Invoke(inputValue);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowLastExceptionWhere()
	{
		var fixture = CreateFixture<int>();

		fixture.SetupParameter(It<int>.Where(x => x > 10));
		fixture.Throws(new Exception("random text"));

		const string exceptionMessage = nameof(exceptionMessage);
		fixture.SetupParameter(It<int>.Where(x => x > 100));
		fixture.Throws(new NullReferenceException(exceptionMessage));

		const int inputValue = 12345678;
		var actual = () => fixture.Invoke(inputValue);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}

	[Fact]
	public void ThrowLastExceptionValue()
	{
		const int setupValue = 12345678;
		var fixture = CreateFixture<int>();

		fixture.SetupParameter(setupValue);
		fixture.Throws(new Exception("random text"));

		const string exceptionMessage = nameof(exceptionMessage);
		fixture.SetupParameter(setupValue);
		fixture.Throws(new NullReferenceException(exceptionMessage));

		var actual = () => fixture.Invoke(setupValue);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}
}
