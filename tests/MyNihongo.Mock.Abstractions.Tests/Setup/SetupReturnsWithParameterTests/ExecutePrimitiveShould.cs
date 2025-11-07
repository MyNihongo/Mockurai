namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupReturnsWithParameterTests;

public sealed class ExecutePrimitiveShould : SetupReturnsWithParameterTestsBase
{
	[Fact]
	public void ReturnForAnySetup()
	{
		const string returnValue = nameof(returnValue);
		var setup = It<int>.Any();

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(returnValue);

		const int input = 12345678;
		var hasValue = fixture.Execute(input, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ReturnForValueSetup()
	{
		const string returnValue = nameof(returnValue);
		const int setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(setupValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ReturnDefaultNullForValueSetup()
	{
		const string returnValue = nameof(returnValue);
		const int setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(returnValue);

		const int inputValue = -123245;
		var hasValue = fixture.Execute(inputValue, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForValueSetup()
	{
		const int setupValue = 12345678, returnValue = 787383423;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, int>(setup);
		fixture.Returns(returnValue);

		const int inputValue = -123245;
		var hasValue = fixture.Execute(inputValue, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ReturnForWhereSetup(int inputValue)
	{
		const string returnValue = nameof(returnValue);
		var setup = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(inputValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnDefaultNullForWhereSetup(int inputValue)
	{
		const string returnValue = nameof(returnValue);
		var setup = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(inputValue, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnDefaultForWhereSetup(int inputValue)
	{
		const int returnValue = 423455;
		var setup = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<int, int>(setup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(inputValue, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowForAnySetup()
	{
		const string errorMessage = nameof(errorMessage);
		var setup = It<int>.Any();

		var fixture = CreateFixture<int, string>(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int input = 12345678;
		Action actual = () => fixture.Execute(input, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowForValueSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => fixture.Execute(setupValue, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void NotThrowForValueSetup()
	{
		const string errorMessage = nameof(errorMessage);
		const int setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		const int inputValue = -123245;
		fixture.Execute(inputValue, out _);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ThrowForWhereSetup(int inputValue)
	{
		const string errorMessage = nameof(errorMessage);
		var setup = It<int>.Where(x => x <= 10);

		var fixture = CreateFixture<int, string>(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => fixture.Execute(inputValue, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void NotThrowForWhereSetup(int inputValue)
	{
		const string errorMessage = nameof(errorMessage);
		var setup = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<int, string>(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		fixture.Execute(inputValue, out _);
	}

	[Fact]
	public void PrioritiseWhereOverAnyReturns1()
	{
		const string returnValue1 = nameof(returnValue1);
		var setup1 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		var setup2 = It<int>.Where(static x => x > 10);

		var fixture = CreateFixture<int, string>(setup1);
		fixture.SetupParameter(setup1);
		fixture.Returns(returnValue1);

		fixture.SetupParameter(setup2);
		fixture.Returns(returnValue2);

		const int inputValue = 10;
		var hasValue = fixture.Execute(inputValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue1, actual);
	}

	[Fact]
	public void PrioritiseWhereOverAnyReturns2()
	{
		const string returnValue1 = nameof(returnValue1);
		var setup1 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		var setup2 = It<int>.Where(static x => x > 10);

		var fixture = CreateFixture<int, string>(setup1);
		fixture.SetupParameter(setup1);
		fixture.Returns(returnValue1);

		fixture.SetupParameter(setup2);
		fixture.Returns(returnValue2);

		const int inputValue = 12345678;
		var hasValue = fixture.Execute(inputValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue2, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns1()
	{
		const string returnValue1 = nameof(returnValue1);
		var setup1 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		var setup2 = It<int>.Where(static x => x > 10);

		const int setupValue3 = 12345678;
		const string returnValue3 = nameof(returnValue3);
		var setup3 = It<int>.Value(setupValue3);

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(setup1);
		fixture.Returns(returnValue1);

		fixture.SetupParameter(setup2);
		fixture.Returns(returnValue2);

		fixture.SetupParameter(setup3);
		fixture.Returns(returnValue3);

		const int input = 10;
		var hasValue = fixture.Execute(input, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue1, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns2()
	{
		const string returnValue1 = nameof(returnValue1);
		var setup1 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		var setup2 = It<int>.Where(static x => x > 10);

		const int setupValue3 = 12345678;
		const string returnValue3 = nameof(returnValue3);
		var setup3 = It<int>.Value(setupValue3);

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(setup1);
		fixture.Returns(returnValue1);

		fixture.SetupParameter(setup2);
		fixture.Returns(returnValue2);

		fixture.SetupParameter(setup3);
		fixture.Returns(returnValue3);

		const int input = 11;
		var hasValue = fixture.Execute(input, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue2, actual);
	}

	[Fact]
	public void PrioritiseValueOverWhereReturns3()
	{
		const string returnValue1 = nameof(returnValue1);
		var setup1 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		var setup2 = It<int>.Where(static x => x > 10);

		const int setupValue3 = 12345678;
		const string returnValue3 = nameof(returnValue3);
		var setup3 = It<int>.Value(setupValue3);

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(setup1);
		fixture.Returns(returnValue1);

		fixture.SetupParameter(setup2);
		fixture.Returns(returnValue2);

		fixture.SetupParameter(setup3);
		fixture.Returns(returnValue3);

		var hasValue = fixture.Execute(setupValue3, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue3, actual);
	}

	[Fact]
	public void PrioritiseWhereOverAnyThrows1()
	{
		const string errorMessage1 = nameof(errorMessage1);
		var setup1 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		var setup2 = It<int>.Where(static x => x > 10);

		var fixture = CreateFixture<int, string>(setup1);
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
		fixture.Throws(new InvalidCastException(errorMessage2));

		const int inputValue = 10;
		Action actual = () => fixture.Execute(inputValue, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void PrioritiseWhereOverAnyThrows2()
	{
		const string errorMessage1 = nameof(errorMessage1);
		var setup1 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		var setup2 = It<int>.Where(static x => x > 10);

		var fixture = CreateFixture<int, string>(setup1);
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
		fixture.Throws(new InvalidCastException(errorMessage2));

		const int inputValue = 12345678;
		Action actual = () => fixture.Execute(inputValue, out _);

		var exception = Assert.Throws<InvalidCastException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrows1()
	{
		const string errorMessage1 = nameof(errorMessage1);
		var setup1 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		var setup2 = It<int>.Where(static x => x > 10);

		const int setupValue3 = 12345678;
		const string errorMessage3 = nameof(errorMessage3);
		var setup3 = It<int>.Value(setupValue3);

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3);
		fixture.Throws(new ArrayTypeMismatchException(errorMessage3));

		const int inputValue = 10;
		Action actual = () => fixture.Execute(inputValue, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage1, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrows2()
	{
		const string errorMessage1 = nameof(errorMessage1);
		var setup1 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		var setup2 = It<int>.Where(static x => x > 10);

		const int setupValue3 = 12345678;
		const string errorMessage3 = nameof(errorMessage3);
		var setup3 = It<int>.Value(setupValue3);

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3);
		fixture.Throws(new ArrayTypeMismatchException(errorMessage3));

		const int inputValue = 11;
		Action actual = () => fixture.Execute(inputValue, out _);

		var exception = Assert.Throws<InvalidCastException>(actual);
		Assert.Equal(errorMessage2, exception.Message);
	}

	[Fact]
	public void PrioritiseValueOverWhereThrows3()
	{
		const string errorMessage1 = nameof(errorMessage1);
		var setup1 = It<int>.Any();

		const string errorMessage2 = nameof(errorMessage2);
		var setup2 = It<int>.Where(static x => x > 10);

		const int setupValue3 = 12345678;
		const string errorMessage3 = nameof(errorMessage3);
		var setup3 = It<int>.Value(setupValue3);

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(setup1);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3);
		fixture.Throws(new ArrayTypeMismatchException(errorMessage3));

		Action actual = () => fixture.Execute(setupValue3, out _);

		var exception = Assert.Throws<ArrayTypeMismatchException>(actual);
		Assert.Equal(errorMessage3, exception.Message);
	}

	[Fact]
	public void ReturnForAnySetupFunc()
	{
		var setup = It<int>.Any();

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static x => (x + 3).ToString());

		const int input = 12345678;
		var hasValue = fixture.Execute(input, out var actual);

		const string returnValue = "12345681";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ReturnForValueSetupFunc()
	{
		const int setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static x => (x + 3).ToString());

		var hasValue = fixture.Execute(setupValue, out var actual);

		const string returnValue = "12345681";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ReturnDefaultNullForValueSetupFunc()
	{
		const int setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static x => (x + 3).ToString());

		const int inputValue = -123245;
		var hasValue = fixture.Execute(inputValue, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void ReturnDefaultForValueSetupFunc()
	{
		const int setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, int>(setup);
		fixture.Returns(static x => x + 3);

		const int inputValue = -123245;
		var hasValue = fixture.Execute(inputValue, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void ReturnForWhereSetupFunc(int inputValue)
	{
		var setup = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static x => (x + 3).ToString());

		var hasValue = fixture.Execute(inputValue, out var actual);

		var returnValue = $"{inputValue + 3}";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnDefaultNullForWhereSetupFunc(int inputValue)
	{
		var setup = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static x => (x + 3).ToString());

		var hasValue = fixture.Execute(inputValue, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnDefaultForWhereSetupFunc(int inputValue)
	{
		var setup = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<int, int>(setup);
		fixture.Returns(static x => x + 3);

		var hasValue = fixture.Execute(inputValue, out var actual);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void InvokeCallbackForAny()
	{
		var setup = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback(x => callbackValue = x + 1);

		const int inputValue = 12345678;
		var hasValue = fixture.Execute(inputValue, out _);

		Assert.False(hasValue);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForAnyBeforeReturns()
	{
		const string returnValue = nameof(returnValue);
		var setup = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback(x => callbackValue = x + 1);
		fixture.And();
		fixture.Returns(returnValue);

		const int inputValue = 12345678;
		var hasValue = fixture.Execute(inputValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForAnyBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		var setup = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Throws(new Exception(expectedMessage));
		fixture.And();
		fixture.Callback(x => callbackValue = x + 1);

		const int inputValue = 12345678;
		Action actual = () => fixture.Execute(inputValue, out _);

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

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback(x => callbackValue = x + 1);

		var hasValue = fixture.Execute(setupValue, out _);

		Assert.False(hasValue);
		Assert.Equal(setupValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForValueBeforeReturns()
	{
		const string returnValue = nameof(returnValue);
		const int setupValue = 12345678;
		var setup = It<int>.Value(setupValue);
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback(x => callbackValue = x + 1);
		fixture.And();
		fixture.Returns(returnValue);

		const int inputValue = 12345678;
		var hasValue = fixture.Execute(inputValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForValueBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		const int setupValue = 12345678;
		var setup = It<int>.Value(setupValue);
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback(x => callbackValue = x + 1);
		fixture.And();
		fixture.Throws(new Exception(expectedMessage));

		const int inputValue = 12345678;
		Action actual = () => fixture.Execute(inputValue, out _);

		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(expectedMessage, exception.Message);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForWhere()
	{
		var setup = It<int>.Where(x => x > 10);
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback(x => callbackValue = x + 1);

		const int inputValue = 12345678;
		var hasValue = fixture.Execute(inputValue, out _);

		Assert.False(hasValue);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForWhereBeforeReturns()
	{
		const string returnValue = nameof(returnValue);
		var setup = It<int>.Where(x => x > 10);
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback(x => callbackValue = x + 1);
		fixture.And();
		fixture.Returns(returnValue);

		const int inputValue = 12345678;
		var hasValue = fixture.Execute(inputValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForWhereBeforeThrows()
	{
		const string expectedMessage = nameof(expectedMessage);
		var setup = It<int>.Where(x => x > 10);
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback(x => callbackValue = x + 1);
		fixture.And();
		fixture.Throws(new Exception(expectedMessage));

		const int inputValue = 12345678;
		Action actual = () => fixture.Execute(inputValue, out _);

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

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback(x => callbackValue = x + 1);

		const int inputValue = -64713;
		var hasValue = fixture.Execute(inputValue, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotInvokeCallbackForWhere()
	{
		var setup = It<int>.Where(x => x > 10);
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback(x => callbackValue = x + 1);

		const int inputValue = -64713;
		var hasValue = fixture.Execute(inputValue, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotDuplicateSameSetup()
	{
		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Callback(_ => { });
		fixture.Throws(new Exception());
		fixture.Callback(_ => { Debug.WriteLine("output"); });
		fixture.Throws(new Exception());

		const int expected = 1;
		var actual = GetSetupCount(fixture);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void InsertAllSetups()
	{
		var fixture = CreateFixture<int, string>();
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
	public void ReturnLastResultAny()
	{
		var fixture = CreateFixture<int, string>();

		fixture.SetupParameter(It<int>.Any());
		fixture.Returns("random text");

		const string returnValue = nameof(returnValue);
		fixture.SetupParameter(It<int>.Any());
		fixture.Returns(returnValue);

		const int inputValue = 12345678;
		var hasValue = fixture.Execute(inputValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ThrowLastExceptionAny()
	{
		var fixture = CreateFixture<int, string>();

		fixture.SetupParameter(It<int>.Any());
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameter(It<int>.Any());
		fixture.Throws(new NullReferenceException(expectedMessage));

		const int inputValue = 12345678;
		Action actual = () => fixture.Execute(inputValue, out _);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnLastResultWhere()
	{
		var fixture = CreateFixture<int, string>();

		fixture.SetupParameter(It<int>.Where(x => x > 10));
		fixture.Returns("random text");

		const string returnValue = nameof(returnValue);
		fixture.SetupParameter(It<int>.Where(x => x > 100));
		fixture.Returns(returnValue);

		const int inputValue = 12345678;
		var hasValue = fixture.Execute(inputValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ThrowLastExceptionWhere()
	{
		var fixture = CreateFixture<int, string>();

		fixture.SetupParameter(It<int>.Where(x => x > 10));
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameter(It<int>.Where(x => x > 100));
		fixture.Throws(new NullReferenceException(expectedMessage));

		const int inputValue = 12345678;
		Action actual = () => fixture.Execute(inputValue, out _);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnLastResultValue()
	{
		const int setupValue = 12345678;
		var fixture = CreateFixture<int, string>();

		fixture.SetupParameter(setupValue);
		fixture.Returns("random text");

		const string returnValue = nameof(returnValue);
		fixture.SetupParameter(setupValue);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(setupValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ThrowLastExceptionValue()
	{
		const int setupValue = 12345678;
		var fixture = CreateFixture<int, string>();

		fixture.SetupParameter(setupValue);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameter(setupValue);
		fixture.Throws(new NullReferenceException(expectedMessage));

		Action actual = () => fixture.Execute(setupValue, out _);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnDifferentValues()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);

		const int parameter = 1234567;
		var actual1 = fixture.Execute(parameter, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(parameter, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(parameter, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback1()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Callback(_ => callback++);
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);

		const int parameter = 1234567;
		var actual1 = fixture.Execute(parameter, out var returnValue1);
		Assert.False(actual1);
		Assert.Null(returnValue1);

		var actual2 = fixture.Execute(parameter, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue1, returnValue2);

		var actual3 = fixture.Execute(parameter, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);

		var actual4 = fixture.Execute(parameter, out var returnValue4);
		Assert.True(actual4);
		Assert.Equal(setupValue2, returnValue4);

		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback2()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Callback(_ => callback++);
		fixture.And();
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);

		const int parameter = 1234567;
		var actual1 = fixture.Execute(parameter, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(parameter, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(parameter, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);

		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback3()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Returns(setupValue1);
		fixture.Callback(_ => callback++);
		fixture.And();
		fixture.Returns(setupValue2);

		const int parameter = 1234567;
		var actual1 = fixture.Execute(parameter, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(parameter, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(parameter, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);

		const int expectedCallback = 2;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback4()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		int callback1 = 10, callback2 = 0;

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Callback(_ => callback1++);
		fixture.And();
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);
		fixture.And();
		fixture.Callback(_ => callback2++);

		const int parameter = 1234567;
		var actual1 = fixture.Execute(parameter, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(parameter, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(parameter, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);

		const int expectedCallback1 = 11, expectedCallback2 = 2;
		Assert.Equal(expectedCallback1, callback1);
		Assert.Equal(expectedCallback2, callback2);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		const int parameter = 1234567;
		Action actual1 = () => fixture.Execute(parameter, out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(parameter, out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(parameter, out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Callback(_ => callback++);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		const int parameter = 1234567;
		fixture.Execute(parameter, out _);

		Action actual2 = () => fixture.Execute(parameter, out _);
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		Action actual3 = () => fixture.Execute(parameter, out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Action actual4 = () => fixture.Execute(parameter, out _);
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

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Callback(_ => callback++);
		fixture.And();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		const int parameter = 1234567;
		Action actual1 = () => fixture.Execute(parameter, out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(parameter, out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(parameter, out _);
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

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Throws(new COMException(errorMessage1));
		fixture.Callback(_ => callback++);
		fixture.And();
		fixture.Throws(new NullReferenceException(errorMessage2));

		const int parameter = 1234567;
		Action actual1 = () => fixture.Execute(parameter, out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(parameter, out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(parameter, out _);
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

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Callback(_ => callback1++);
		fixture.And();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));
		fixture.And();
		fixture.Callback(_ => callback2++);

		const int parameter = 1234567;
		Action actual1 = () => fixture.Execute(parameter, out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(parameter, out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(parameter, out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback1 = 11, expectedCallback2 = 2;
		Assert.Equal(expectedCallback1, callback1);
		Assert.Equal(expectedCallback2, callback2);
	}

	[Fact]
	public void ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage);
		const int expected = 1234;

		var fixture = CreateFixture<int, int>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Returns(expected);
		fixture.Throws(new IndexOutOfRangeException(errorMessage));

		const int parameter = 1234567;
		var actual1 = fixture.Execute(parameter, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(expected, returnValue1);

		Action actual2 = () => fixture.Execute(parameter, out _);
		var exception2 = Assert.Throws<IndexOutOfRangeException>(actual2);
		Assert.Equal(errorMessage, exception2.Message);

		Action actual3 = () => fixture.Execute(parameter, out _);
		var exception3 = Assert.Throws<IndexOutOfRangeException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);
	}

	[Fact]
	public void ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage);
		const int expected = 1234;

		var fixture = CreateFixture<int, int>();
		fixture.SetupParameter(It<int>.Any());
		fixture.Throws(new IndexOutOfRangeException(errorMessage));
		fixture.Returns(expected);

		const int parameter = 1234567;
		Action actual1 = () => fixture.Execute(parameter, out _);
		var exception1 = Assert.Throws<IndexOutOfRangeException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		var actual2 = fixture.Execute(parameter, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(expected, returnValue2);

		var actual3 = fixture.Execute(parameter, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(expected, returnValue3);
	}
}
