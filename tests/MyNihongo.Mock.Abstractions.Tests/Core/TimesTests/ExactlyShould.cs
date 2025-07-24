namespace MyNihongo.Mock.Abstractions.Tests.Core.TimesTests;

public sealed class ExactlyShould
{
	[Fact]
	public void BeTrueForExactMatch()
	{
		const int inputValue = 123;

		var actual = Times.Exactly(inputValue)
			.Predicate(inputValue);

		Assert.True(actual);
	}

	[Theory]
	[InlineData(1)]
	[InlineData(3)]
	[InlineData(-2)]
	public void BeFalseIfNotExactMatch(int predicateValue)
	{
		const int inputValue = 2;

		var actual = Times.Exactly(inputValue)
			.Predicate(predicateValue);

		Assert.False(actual);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(2)]
	public void HaveStringMultipleTense(int inputValue)
	{
		var actual = Times.Exactly(inputValue)
			.ToString();

		var expected = $"{inputValue} times";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void HaveStringSingleTense()
	{
		const int inputValue = 1;

		var actual = Times.Exactly(inputValue)
			.ToString();

		const string expected = "1 time";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowExceptionIfTimesNegative()
	{
		const int inputValue = -1;

		var actual = () => Times.Exactly(inputValue)
			.ToString();

		const string exceptionMessage = "Count must not be negative, count=`-1` (Parameter 'count')";
		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}
}
