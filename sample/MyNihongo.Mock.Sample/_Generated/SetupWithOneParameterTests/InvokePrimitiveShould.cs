namespace MyNihongo.Mock.Sample._Generated.SetupWithOneParameterTests;

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
	public void PrioritiseWhereOverAny()
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
	public void PrioritiseValueOverWhere()
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
}
