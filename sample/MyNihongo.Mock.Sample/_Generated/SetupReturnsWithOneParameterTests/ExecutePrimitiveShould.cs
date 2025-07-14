namespace MyNihongo.Mock.Sample._Generated.SetupReturnsWithOneParameterTests;

public sealed class ExecutePrimitiveShould : SetupReturnsWithOneParameterTestsBase
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

		const string expectedReturnValue = "12345681";
		Assert.True(hasValue);
		Assert.Equal(expectedReturnValue, actual);
	}

	[Fact]
	public void ReturnForValueSetupFunc()
	{
		const int setupValue = 12345678;
		It<int> setup = setupValue;

		var fixture = CreateFixture<int, string>(setup);
		fixture.Returns(static x => (x + 3).ToString());

		var hasValue = fixture.Execute(setupValue, out var actual);

		const string expectedReturnValue = "12345681";
		Assert.True(hasValue);
		Assert.Equal(expectedReturnValue, actual);
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

		var expectedReturnValue = $"{inputValue + 3}";
		Assert.True(hasValue);
		Assert.Equal(expectedReturnValue, actual);
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
}
