namespace MyNihongo.Mock.Abstractions.Tests.Core.TimesTests;

public sealed class NeverShould
{
	[Fact]
	public void BeTrueForExactMatch()
	{
		const int inputValue = 0;

		var actual = Times.Never()
			.Predicate(inputValue);

		Assert.True(actual);
	}

	[Theory]
	[InlineData(1)]
	[InlineData(2)]
	public void BeFalseIfNotExactMatch(int predicateValue)
	{
		var actual = Times.Never()
			.Predicate(predicateValue);

		Assert.False(actual);
	}

	[Fact]
	public void HaveValidString()
	{
		var actual = Times.Never()
			.ToString();

		const string expected = "0 times";
		Assert.Equal(expected, actual);
	}
}
