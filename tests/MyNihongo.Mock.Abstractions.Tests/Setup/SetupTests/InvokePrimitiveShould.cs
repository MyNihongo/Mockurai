namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupTests;

public sealed class InvokePrimitiveShould : SetupTestsBase
{
	[Fact]
	public void ThrowException()
	{
		const string errorMessage = nameof(errorMessage);

		var fixture = CreateFixture();
		fixture.Throws(new IndexOutOfRangeException(errorMessage));

		var actual = () => fixture.Invoke();

		var exception = Assert.Throws<IndexOutOfRangeException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void NotThrowException()
	{
		CreateFixture()
			.Invoke();
	}

	[Fact]
	public void InvokeCallbackBeforeThrows()
	{
		const string errorMessage = nameof(errorMessage);
		var counter = 0;

		var fixture = CreateFixture();
		fixture.Throws(new IndexOutOfRangeException(errorMessage));
		fixture.Callback(() => counter++);

		var actual = () => fixture.Invoke();

		const int expected = 1;
		var exception = Assert.Throws<IndexOutOfRangeException>(actual);
		Assert.Equal(errorMessage, exception.Message);
		Assert.Equal(expected, counter);
	}

	[Fact]
	public void InvokeCallback()
	{
		var counter = 0;

		var fixture = CreateFixture();
		fixture.Callback(() => counter++);
		fixture.Invoke();

		const int expected = 1;
		Assert.Equal(expected, counter);
	}
}
