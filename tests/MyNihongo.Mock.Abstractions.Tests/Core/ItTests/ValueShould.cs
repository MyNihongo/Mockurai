namespace MyNihongo.Mock.Abstractions.Tests.Core.ItTests;

public sealed class ValueShould
{
	[Fact]
	public void BeTrueForSameValue()
	{
		const int inputValue = 123;

		var actual = It<int>.Value(inputValue)
			.ValueSetup.Check(inputValue);

		Assert.True(actual);
	}

	[Theory]
	[InlineData(320)]
	[InlineData(322)]
	public void BeFalseForDifferentValue(int inputValue)
	{
		const int setupValue = 321;

		var actual = It<int>.Value(setupValue)
			.ValueSetup.Check(inputValue);

		Assert.False(actual);
	}
}
