namespace MyNihongo.Mockurai.Abstractions.Tests.Core.TimesTests;

public sealed class OnceShould
{
	[Fact]
	public void BeTrueForExactMatch()
	{
		const int inputValue = 1;

		var actual = Times.Once()
			.Predicate(inputValue);

		Assert.True(actual);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(2)]
	public void BeFalseIfNotExactMatch(int predicateValue)
	{
		var actual = Times.Once()
			.Predicate(predicateValue);

		Assert.False(actual);
	}

	[Fact]
	public void HaveValidString()
	{
		var actual = Times.Once()
			.ToString();

		const string expected = "1 time";
		Assert.Equal(expected, actual);
	}
}
