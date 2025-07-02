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
}
