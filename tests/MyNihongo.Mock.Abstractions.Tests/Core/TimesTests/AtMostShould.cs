namespace MyNihongo.Mock.Abstractions.Tests.Core.TimesTests;

public sealed class AtMostShould
{
	[Theory]
	[InlineData(2)]
	[InlineData(3)]
	public void BeTrueIfEqualsOrLess(int predicateValue)
	{
		const int inputValue = 3;

		var actual = Times.AtMost(inputValue)
			.Predicate(predicateValue);

		Assert.True(actual);
	}

	[Fact]
	public void BeFalseIfGreater()
	{
		const int inputValue = 3,
			predicateValue = 4;

		var actual = Times.AtMost(inputValue)
			.Predicate(predicateValue);

		Assert.False(actual);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(2)]
	public void HaveStringMultipleTense(int inputValue)
	{
		var actual = Times.AtMost(inputValue)
			.ToString();

		var expected = $"at most {inputValue} times";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void HaveStringSingleTense()
	{
		const int inputValue = 1;

		var actual = Times.AtMost(inputValue)
			.ToString();

		const string expected = "at most 1 time";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowExceptionIfTimesNegative()
	{
		const int inputValue = -1;

		var actual = () => Times.AtMost(inputValue)
			.ToString();

		const string expectedMessage = "Count must not be negative, count=`-1` (Parameter 'count')";
		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
