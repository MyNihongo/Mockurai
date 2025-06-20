namespace MyNihongo.Mock.Sample._Generated.SetupWithParameterTests;

public sealed class InvokeShould : SetupWithParameterTestsBase
{
	[Theory]
	[InlineData(123, 124)]
	[InlineData(124, 123)]
	public void InvokeForPositive(int setupHashCode, int inputHashCode)
	{
		var fixture = CreateFixture();
		fixture.SetupParameters(setupHashCode);
		fixture.Throws(new Exception());

		fixture.Invoke(inputHashCode);
	}

	[Theory]
	[InlineData(-123, 124)]
	[InlineData(-124, 123)]
	[InlineData(123, -124)]
	[InlineData(124, -123)]
	public void InvokeForPositiveNegative(int setupHashCode, int inputHashCode)
	{
		var fixture = CreateFixture();
		fixture.SetupParameters(setupHashCode);
		fixture.Throws(new Exception());

		fixture.Invoke(inputHashCode);
	}

	[Theory]
	[InlineData(-123, -124)]
	[InlineData(-124, -123)]
	public void InvokeForNegative(int setupHashCode, int inputHashCode)
	{
		var fixture = CreateFixture();
		fixture.SetupParameters(setupHashCode);
		fixture.Throws(new Exception());

		fixture.Invoke(inputHashCode);
	}

	[Theory]
	[InlineData(124, 124)]
	[InlineData(-124, -124)]
	[InlineData(0, 0)]
	public void ThrowForEqual(int setupHashCode, int inputHashCode)
	{
		var fixture = CreateFixture();
		fixture.SetupParameters(setupHashCode);
		fixture.Throws(new Exception());

		var actual = () => fixture.Invoke(inputHashCode);
		Assert.Throws<Exception>(actual);
	}

	[Theory]
	[InlineData(123)]
	[InlineData(-123)]
	[InlineData(0)]
	public void ThrowForAny(int inputHashCode)
	{
		const int setupHashCode = 0;

		var fixture = CreateFixture();
		fixture.SetupParameters(setupHashCode);
		fixture.Throws(new Exception());

		var actual = () => fixture.Invoke(inputHashCode);
		Assert.Throws<Exception>(actual);
	}

	[Theory]
	[InlineData(123)]
	[InlineData(-123)]
	public void InvokeForZeroInput(int setupHashCode)
	{
		const int inputHashCode = 0;

		var fixture = CreateFixture();
		fixture.SetupParameters(setupHashCode);
		fixture.Throws(new Exception());

		fixture.Invoke(inputHashCode);
	}
}
