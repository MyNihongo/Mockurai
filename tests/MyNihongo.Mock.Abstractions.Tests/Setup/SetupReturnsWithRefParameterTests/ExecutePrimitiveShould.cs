namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupReturnsWithRefParameterTests;

public sealed class ExecutePrimitiveShould : SetupReturnsWithRefParameterTestsBase
{
	[Fact]
	public void ReturnForAnySetup()
	{
		const string returnValue = nameof(returnValue);
		var setup = It<int>.Any();

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(returnValue);

		var inputValue = 12345678;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ReturnForValueSetup()
	{
		const string returnValue = nameof(returnValue);
		var setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(ref setupValue, out var actual);

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

		var inputValue = -123245;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

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

		var inputValue = -123245;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

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

		var hasValue = fixture.Execute(ref inputValue, out var actual);

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

		var hasValue = fixture.Execute(ref inputValue, out var actual);

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

		var hasValue = fixture.Execute(ref inputValue, out var actual);

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

		var inputValue = 12345678;
		Action actual = () => fixture.Execute(ref inputValue, out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowForValueSetup()
	{
		const string errorMessage = nameof(errorMessage);
		var setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Throws(new InvalidOperationException(errorMessage));

		Action actual = () => fixture.Execute(ref setupValue, out _);

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

		var inputValue = -123245;
		fixture.Execute(ref inputValue, out _);
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

		Action actual = () => fixture.Execute(ref inputValue, out _);

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

		fixture.Execute(ref inputValue, out _);
	}

	[Fact]
	public void PrioritiseWhereOverAnyReturns1()
	{
		const string returnValue1 = nameof(returnValue1);
		var setup1 = It<int>.Any();

		const string returnValue2 = nameof(returnValue2);
		var setup2 = It<int>.Where(static x => x > 10);

		var fixture = CreateFixture<int, string>(setup1);
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Returns(returnValue2);

		var inputValue = 10;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

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
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Returns(returnValue2);

		var inputValue = 12345678;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

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
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameter(setup3.ValueSetup);
		fixture.Returns(returnValue3);

		var inputValue = 10;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

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
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameter(setup3.ValueSetup);
		fixture.Returns(returnValue3);

		var inputValue = 11;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

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

		var setupValue3 = 12345678;
		const string returnValue3 = nameof(returnValue3);
		var setup3 = It<int>.Value(setupValue3);

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Returns(returnValue1);

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Returns(returnValue2);

		fixture.SetupParameter(setup3.ValueSetup);
		fixture.Returns(returnValue3);

		var hasValue = fixture.Execute(ref setupValue3, out var actual);

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
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage2));

		var inputValue = 10;
		Action actual = () => fixture.Execute(ref inputValue, out _);

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
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage2));

		var inputValue = 12345678;
		Action actual = () => fixture.Execute(ref inputValue, out _);

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
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3.ValueSetup);
		fixture.Throws(new ArrayTypeMismatchException(errorMessage3));

		var inputValue = 10;
		Action actual = () => fixture.Execute(ref inputValue, out _);

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
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3.ValueSetup);
		fixture.Throws(new ArrayTypeMismatchException(errorMessage3));

		var inputValue = 11;
		Action actual = () => fixture.Execute(ref inputValue, out _);

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

		var setupValue3 = 12345678;
		const string errorMessage3 = nameof(errorMessage3);
		var setup3 = It<int>.Value(setupValue3);

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(setup1.ValueSetup);
		fixture.Throws(new InvalidOperationException(errorMessage1));

		fixture.SetupParameter(setup2.ValueSetup);
		fixture.Throws(new InvalidCastException(errorMessage2));

		fixture.SetupParameter(setup3.ValueSetup);
		fixture.Throws(new ArrayTypeMismatchException(errorMessage3));

		Action actual = () => fixture.Execute(ref setupValue3, out _);

		var exception = Assert.Throws<ArrayTypeMismatchException>(actual);
		Assert.Equal(errorMessage3, exception.Message);
	}

	[Fact]
	public void ReturnForAnySetupFunc()
	{
		var setup = It<int>.Any();

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static (ref int x) => (x + 3).ToString());

		var inputValue = 12345678;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

		const string returnValue = "12345681";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void SetValueInReturnForAnySetupFunc()
	{
		var setup = It<int>.Any();

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static (ref int x) => (x += 3).ToString());

		var inputValue = 12345678;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

		const int expected = 12345681;
		const string returnValue = "12345681";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(expected, inputValue);
	}

	[Fact]
	public void ReturnForValueSetupFunc()
	{
		var setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static (ref int x) => (x + 3).ToString());

		var hasValue = fixture.Execute(ref setupValue, out var actual);

		const string returnValue = "12345681";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void SetValueInReturnForValueSetupFunc()
	{
		var setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static (ref int x) => (x += 3).ToString());

		var hasValue = fixture.Execute(ref setupValue, out var actual);

		const int expected = 12345681;
		const string returnValue = "12345681";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(expected, setupValue);
	}

	[Fact]
	public void ReturnDefaultNullForValueSetupFunc()
	{
		const int setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static (ref int x) => (x + 3).ToString());

		var inputValue = -123245;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Fact]
	public void NotSetValueInReturnForValueSetupFunc()
	{
		const int setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static (ref int x) => (x += 3).ToString());

		var inputValue = -123245;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

		const int expected = -123245;
		Assert.False(hasValue);
		Assert.Null(actual);
		Assert.Equal(expected, inputValue);
	}

	[Fact]
	public void ReturnDefaultForValueSetupFunc()
	{
		const int setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, int>(setup);
		fixture.Returns(static (ref int x) => x + 3);

		var inputValue = -123245;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

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
		fixture.Returns(static (ref int x) => (x + 3).ToString());

		var hasValue = fixture.Execute(ref inputValue, out var actual);

		var returnValue = $"{inputValue + 3}";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Theory]
	[InlineData(-1)]
	[InlineData(0)]
	[InlineData(10)]
	public void SetValueInReturnForWhereSetupFunc(int inputValue)
	{
		var expected = inputValue + 3;
		var setup = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static (ref int x) => (x += 3).ToString());

		var hasValue = fixture.Execute(ref inputValue, out var actual);

		var returnValue = $"{expected}";
		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
		Assert.Equal(expected, inputValue);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnDefaultNullForWhereSetupFunc(int inputValue)
	{
		var setup = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static (ref int x) => (x + 3).ToString());

		var hasValue = fixture.Execute(ref inputValue, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void NotSetValueInReturnForWhereSetupFunc(int inputValue)
	{
		var expected = inputValue;
		var setup = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static (ref int x) => (x += 3).ToString());

		var hasValue = fixture.Execute(ref inputValue, out var actual);

		Assert.False(hasValue);
		Assert.Null(actual);
		Assert.Equal(expected, inputValue);
	}

	[Theory]
	[InlineData(11)]
	[InlineData(100)]
	public void ReturnDefaultForWhereSetupFunc(int inputValue)
	{
		var setup = It<int>.Where(static x => x <= 10);

		var fixture = CreateFixture<int, int>(setup);
		fixture.Returns(static (ref int x) => x + 3);

		var hasValue = fixture.Execute(ref inputValue, out var actual);

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
		fixture.Callback((ref int x) => callbackValue = x + 1);

		var inputValue = 12345678;
		var hasValue = fixture.Execute(ref inputValue, out _);

		Assert.False(hasValue);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void SetValueInCallbackForAny()
	{
		var setup = It<int>.Any();

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback((ref int x) => x++);

		var inputValue = 12345678;
		var hasValue = fixture.Execute(ref inputValue, out _);

		const int expected = 12345679;
		Assert.False(hasValue);
		Assert.Equal(expected, inputValue);
	}

	[Fact]
	public void InvokeCallbackForAnyBeforeReturns()
	{
		const string returnValue = nameof(returnValue);
		var setup = It<int>.Any();
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback((ref int x) => callbackValue = x + 1);
		fixture.And();
		fixture.Returns(returnValue);

		var inputValue = 12345678;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

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
		fixture.Callback((ref int x) => callbackValue = x + 1);

		var inputValue = 12345678;
		Action actual = () => fixture.Execute(ref inputValue, out _);

		var exception = Assert.Throws<Exception>(actual);
		Assert.Equal(expectedMessage, exception.Message);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void InvokeCallbackForValue()
	{
		var setupValue = 12345678;
		var setup = It<int>.Value(setupValue);
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback((ref int x) => callbackValue = x + 1);

		var hasValue = fixture.Execute(ref setupValue, out _);

		Assert.False(hasValue);
		Assert.Equal(setupValue + 1, callbackValue);
	}

	[Fact]
	public void ReturnValueInCallbackForValue()
	{
		var setupValue = 12345678;
		var setup = It<int>.Value(setupValue);

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback((ref int x) => x++);

		var hasValue = fixture.Execute(ref setupValue, out _);

		const int expected = 12345679;
		Assert.False(hasValue);
		Assert.Equal(expected, setupValue);
	}

	[Fact]
	public void InvokeCallbackForValueBeforeReturns()
	{
		const string returnValue = nameof(returnValue);
		const int setupValue = 12345678;
		var setup = It<int>.Value(setupValue);
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback((ref int x) => callbackValue = x + 1);
		fixture.And();
		fixture.Returns(returnValue);

		var inputValue = 12345678;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

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
		fixture.Callback((ref int x) => callbackValue = x + 1);
		fixture.And();
		fixture.Throws(new Exception(expectedMessage));

		var inputValue = 12345678;
		Action actual = () => fixture.Execute(ref inputValue, out _);

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
		fixture.Callback((ref int x) => callbackValue = x + 1);

		var inputValue = 12345678;
		var hasValue = fixture.Execute(ref inputValue, out _);

		Assert.False(hasValue);
		Assert.Equal(inputValue + 1, callbackValue);
	}

	[Fact]
	public void ReturnValueInCallbackForWhere()
	{
		var setup = It<int>.Where(x => x > 10);

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback((ref int x) => x++);

		var inputValue = 12345678;
		var hasValue = fixture.Execute(ref inputValue, out _);

		const int expected = 12345679;
		Assert.False(hasValue);
		Assert.Equal(expected, inputValue);
	}

	[Fact]
	public void InvokeCallbackForWhereBeforeReturns()
	{
		const string returnValue = nameof(returnValue);
		var setup = It<int>.Where(x => x > 10);
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback((ref int x) => callbackValue = x + 1);
		fixture.And();
		fixture.Returns(returnValue);

		var inputValue = 12345678;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

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
		fixture.Callback((ref int x) => callbackValue = x + 1);
		fixture.And();
		fixture.Throws(new Exception(expectedMessage));

		var inputValue = 12345678;
		Action actual = () => fixture.Execute(ref inputValue, out _);

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
		fixture.Callback((ref int x) => callbackValue = x + 1);

		var inputValue = -64713;
		var hasValue = fixture.Execute(ref inputValue, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForValue()
	{
		const int setupValue = 12345678;
		var setup = It<int>.Value(setupValue);

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback((ref int x) => x++);

		var inputValue = -64713;
		var hasValue = fixture.Execute(ref inputValue, out _);

		const int expected = -64713;
		Assert.False(hasValue);
		Assert.Equal(expected, inputValue);
	}

	[Fact]
	public void NotInvokeCallbackForWhere()
	{
		var setup = It<int>.Where(x => x > 10);
		var callbackValue = 0;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback((ref int x) => callbackValue = x + 1);

		var inputValue = -64713;
		var hasValue = fixture.Execute(ref inputValue, out _);

		const int expected = 0;
		Assert.False(hasValue);
		Assert.Equal(expected, callbackValue);
	}

	[Fact]
	public void NotSetValueInCallbackForWhere()
	{
		var setup = It<int>.Where(x => x > 10);

		var fixture = CreateFixture<int, string>(setup);
		fixture.Callback((ref int x) => x++);

		var inputValue = -64713;
		var hasValue = fixture.Execute(ref inputValue, out _);

		const int expected = -64713;
		Assert.False(hasValue);
		Assert.Equal(expected, inputValue);
	}

	[Fact]
	public void NotDuplicateSameSetup()
	{
		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback((ref int _) => { });
		fixture.Throws(new Exception());
		fixture.Callback((ref int _) => { Debug.WriteLine("output"); });
		fixture.Throws(new Exception());

		const int expected = 1;
		var actual = GetSetupCount(fixture);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void InsertAllSetups()
	{
		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback((ref int _) => { });

		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Throws(new Exception());

		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback((ref int _) => { Debug.WriteLine("output"); });

		const int expected = 3;
		var actual = GetSetupCount(fixture);
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ReturnLastResultAny()
	{
		var fixture = CreateFixture<int, string>();

		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Returns("random text");

		const string returnValue = nameof(returnValue);
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Returns(returnValue);

		var inputValue = 12345678;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ThrowLastExceptionAny()
	{
		var fixture = CreateFixture<int, string>();

		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		var inputValue = 12345678;
		Action actual = () => fixture.Execute(ref inputValue, out _);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnLastResultWhere()
	{
		var fixture = CreateFixture<int, string>();

		fixture.SetupParameter(It<int>.Where(x => x > 10).ValueSetup);
		fixture.Returns("random text");

		const string returnValue = nameof(returnValue);
		fixture.SetupParameter(It<int>.Where(x => x > 100).ValueSetup);
		fixture.Returns(returnValue);

		var inputValue = 12345678;
		var hasValue = fixture.Execute(ref inputValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ThrowLastExceptionWhere()
	{
		var fixture = CreateFixture<int, string>();

		fixture.SetupParameter(It<int>.Where(x => x > 10).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameter(It<int>.Where(x => x > 100).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		var inputValue = 12345678;
		Action actual = () => fixture.Execute(ref inputValue, out _);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnLastResultValue()
	{
		var setupValue = 12345678;
		var fixture = CreateFixture<int, string>();

		fixture.SetupParameter(It<int>.Value(setupValue).ValueSetup);
		fixture.Returns("random text");

		const string returnValue = nameof(returnValue);
		fixture.SetupParameter(It<int>.Value(setupValue).ValueSetup);
		fixture.Returns(returnValue);

		var hasValue = fixture.Execute(ref setupValue, out var actual);

		Assert.True(hasValue);
		Assert.Equal(returnValue, actual);
	}

	[Fact]
	public void ThrowLastExceptionValue()
	{
		var setupValue = 12345678;
		var fixture = CreateFixture<int, string>();

		fixture.SetupParameter(It<int>.Value(setupValue).ValueSetup);
		fixture.Throws(new Exception("random text"));

		const string expectedMessage = nameof(expectedMessage);
		fixture.SetupParameter(It<int>.Value(setupValue).ValueSetup);
		fixture.Throws(new NullReferenceException(expectedMessage));

		Action actual = () => fixture.Execute(ref setupValue, out _);

		var exception = Assert.Throws<NullReferenceException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ReturnDifferentValues()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);

		var parameter = 1234;
		var actual1 = fixture.Execute(ref parameter, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(ref parameter, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(ref parameter, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback1()
	{
		const string setupValue1 = nameof(setupValue1), setupValue2 = nameof(setupValue2);
		var callback = 0;

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback((ref int value) => value = callback++);
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);

		var parameter = 1234;
		var actual1 = fixture.Execute(ref parameter, out var returnValue1);
		Assert.False(actual1);
		Assert.Null(returnValue1);

		var actual2 = fixture.Execute(ref parameter, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue1, returnValue2);

		var actual3 = fixture.Execute(ref parameter, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);

		var actual4 = fixture.Execute(ref parameter, out var returnValue4);
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
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback((ref int value) => value = callback++);
		fixture.And();
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);

		var parameter = 1234;
		var actual1 = fixture.Execute(ref parameter, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(ref parameter, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(ref parameter, out var returnValue3);
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
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Returns(setupValue1);
		fixture.Callback((ref int value) => value = callback++);
		fixture.And();
		fixture.Returns(setupValue2);

		var parameter = 1234;
		var actual1 = fixture.Execute(ref parameter, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(ref parameter, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(ref parameter, out var returnValue3);
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
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback((ref int value) => value = callback1++);
		fixture.And();
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);
		fixture.And();
		fixture.Callback((ref int value) => value = callback2++);

		var parameter = 1234;
		var actual1 = fixture.Execute(ref parameter, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(ref parameter, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(ref parameter, out var returnValue3);
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
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		var parameter = 1234;
		Action actual1 = () => fixture.Execute(ref parameter, out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(ref parameter, out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(ref parameter, out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<int, string>();
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback((ref int value) => value = callback++);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		var parameter = 1234;
		fixture.Execute(ref parameter, out _);

		Action actual2 = () => fixture.Execute(ref parameter, out _);
		var exception1 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual3 = () => fixture.Execute(ref parameter, out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		Action actual4 = () => fixture.Execute(ref parameter, out _);
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
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback((ref int value) => value = callback++);
		fixture.And();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		var parameter = 1234;
		Action actual1 = () => fixture.Execute(ref parameter, out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(ref parameter, out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(ref parameter, out _);
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
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Callback((ref int value) => value = callback++);
		fixture.And();
		fixture.Throws(new NullReferenceException(errorMessage2));

		var parameter = 1234;
		Action actual1 = () => fixture.Execute(ref parameter, out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(ref parameter, out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(ref parameter, out _);
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
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Callback((ref int value) => value = callback1++);
		fixture.And();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));
		fixture.And();
		fixture.Callback((ref int value) => value = callback2++);

		var parameter = 1234;
		Action actual1 = () => fixture.Execute(ref parameter, out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(ref parameter, out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(ref parameter, out _);
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
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Returns(expected);
		fixture.Throws(new IndexOutOfRangeException(errorMessage));

		var parameter = 1234;
		var actual1 = fixture.Execute(ref parameter, out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(expected, returnValue1);

		Action actual2 = () => fixture.Execute(ref parameter, out _);
		var exception2 = Assert.Throws<IndexOutOfRangeException>(actual2);
		Assert.Equal(errorMessage, exception2.Message);

		Action actual3 = () => fixture.Execute(ref parameter, out _);
		var exception3 = Assert.Throws<IndexOutOfRangeException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);
	}

	[Fact]
	public void ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage);
		const int expected = 1234;

		var fixture = CreateFixture<int, int>();
		fixture.SetupParameter(It<int>.Any().ValueSetup);
		fixture.Throws(new IndexOutOfRangeException(errorMessage));
		fixture.Returns(expected);

		var parameter = 1234;
		var actual1 = () =>
		{
			var parameter1 = 1234;
			fixture.Execute(ref parameter1, out _);
		};
		var exception1 = Assert.Throws<IndexOutOfRangeException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		var actual2 = fixture.Execute(ref parameter, out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(expected, returnValue2);

		var actual3 = fixture.Execute(ref parameter, out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(expected, returnValue3);
	}
}
