namespace MyNihongo.Mock.Abstractions.Tests.Core.ItTests;

public sealed class WhereShould
{
	[Theory]
	[InlineData(11)]
	[InlineData(1234567)]
	public void BeTrueForValidValues(int inputValue)
	{
		var actual = It<int>.Where(x => x > 10)
			.ValueSetup.Check(inputValue);

		Assert.True(actual);
	}

	[Theory]
	[InlineData(10)]
	[InlineData(1)]
	[InlineData(0)]
	[InlineData(-11)]
	public void BeFalseForValidValues(int inputValue)
	{
		var actual = It<int>.Where(x => x > 10)
			.ValueSetup.Check(inputValue);

		Assert.False(actual);
	}
}
