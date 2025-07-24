namespace MyNihongo.Mock.Abstractions.Tests.Core.TimesTests;

public sealed class AtLeastShould
{
	[Theory]
	[InlineData(3)]
	[InlineData(4)]
	public void BeTrueIfEqualsOrGreater(int predicateValue)
	{
		const int inputValue = 3;

		var actual = Times.AtLeast(inputValue)
			.Predicate(predicateValue);

		Assert.True(actual);
	}

	[Fact]
	public void BeFalseIfLess()
	{
		const int inputValue = 3,
			predicateValue = 2;

		var actual = Times.AtLeast(inputValue)
			.Predicate(predicateValue);

		Assert.False(actual);
	}
	
	[Theory]
	[InlineData(0)]
	[InlineData(2)]
	public void HaveStringMultipleTense(int inputValue)
	{
		var actual = Times.AtLeast(inputValue)
			.ToString();

		var expected = $"at least {inputValue} times";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void HaveStringSingleTense()
	{
		const int inputValue = 1;

		var actual = Times.AtLeast(inputValue)
			.ToString();

		const string expected = "at least 1 time";
		Assert.Equal(expected, actual);
	}

	[Fact]
	public void ThrowExceptionIfTimesNegative()
	{
		const int inputValue = -1;

		var actual = () => Times.AtLeast(inputValue)
			.ToString();

		const string exceptionMessage = "Count must not be negative, count=`-1` (Parameter 'count')";
		var exception = Assert.Throws<ArgumentException>(actual);
		Assert.Equal(exceptionMessage, exception.Message);
	}
}
