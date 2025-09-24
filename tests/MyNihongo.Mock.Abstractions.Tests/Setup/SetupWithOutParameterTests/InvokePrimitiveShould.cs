namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupWithOutParameterTests;

public sealed class InvokePrimitiveShould : SetupWithOutParameterTestsBase
{
	[Fact]
	public void ThrowException()
	{
		const string errorMessage = nameof(errorMessage);

		var fixture = CreateFixture<int>();
		fixture.Throws(new InvalidOperationException(errorMessage));

		var actual = () => fixture.Invoke(out _);

		var exception = Assert.Throws<InvalidOperationException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void NotThrowException()
	{
		CreateFixture<int>()
			.Invoke(out _);
	}

	[Fact]
	public void InvokeCallbackBeforeThrows()
	{
		const string errorMessage = nameof(errorMessage);
		var counter = 0;

		var fixture = CreateFixture<int>();
		fixture.Throws(new IndexOutOfRangeException(errorMessage));
		fixture.Callback((out int x) => x = counter++);

		var actual = () => fixture.Invoke(out _);

		const int expected = 1;
		var exception = Assert.Throws<IndexOutOfRangeException>(actual);
		Assert.Equal(errorMessage, exception.Message);
		Assert.Equal(expected, counter);
	}

	[Fact]
	public void InvokeCallback()
	{
		var counter = 0;

		var fixture = CreateFixture<int>();
		fixture.Callback((out int x) => x = counter++);
		fixture.Invoke(out _);

		const int expected = 1;
		Assert.Equal(expected, counter);
	}

	[Fact]
	public void ReturnValueInCallback()
	{
		const int expected = 12345;

		var fixture = CreateFixture<int>();
		fixture.Callback((out int x) => x = expected);
		fixture.Invoke(out var result);

		Assert.Equal(expected, result);
	}
}
