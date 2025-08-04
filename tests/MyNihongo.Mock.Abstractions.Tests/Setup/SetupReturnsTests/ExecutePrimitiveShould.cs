namespace MyNihongo.Mock.Abstractions.Tests.Setup.SetupReturnsTests;

public sealed class ExecutePrimitiveShould : SetupReturnsTestsBase
{
	[Fact]
	public void ReturnDefault()
	{
		var fixture = CreateFixture<int>();
		var actual = fixture.Execute(out var returnValue);

		const int expected = 0;
		Assert.False(actual);
		Assert.Equal(expected, returnValue);
	}

	[Fact]
	public void ReturnNull()
	{
		var fixture = CreateFixture<int?>();
		var actual = fixture.Execute(out var returnValue);

		Assert.False(actual);
		Assert.Null(returnValue);
	}

	[Fact]
	public void ReturnValue()
	{
		const int setupValue = 123;

		var fixture = CreateFixture<int>();
		fixture.Returns(setupValue);
		var actual = fixture.Execute(out var returnValue);

		Assert.True(actual);
		Assert.Equal(setupValue, returnValue);
	}

	[Fact]
	public void ReturnNullValue()
	{
		int? setupValue = null;

		var fixture = CreateFixture<int?>();
		fixture.Returns(setupValue);
		var actual = fixture.Execute(out var returnValue);

		Assert.True(actual);
		Assert.Equal(setupValue, returnValue);
	}

	[Fact]
	public void ThrowException()
	{
		const string errorMessage = nameof(errorMessage);

		var fixture = CreateFixture<int>();
		fixture.Throws(new IndexOutOfRangeException(errorMessage));

		Action actual = () => fixture.Execute(out _);

		var exception = Assert.Throws<IndexOutOfRangeException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage);

		var fixture = CreateFixture<int>();
		fixture.Returns(1234);
		fixture.Throws(new IndexOutOfRangeException(errorMessage));

		Action actual = () => fixture.Execute(out _);

		var exception = Assert.Throws<IndexOutOfRangeException>(actual);
		Assert.Equal(errorMessage, exception.Message);
	}

	[Fact]
	public void ReturnValueFunc()
	{
		const int setupValue = 123;

		var fixture = CreateFixture<int>();
		fixture.Returns(() => setupValue + 2);
		var actual = fixture.Execute(out var returnValue);

		Assert.True(actual);
		Assert.Equal(setupValue + 2, returnValue);
	}

	[Fact]
	public void ReturnNullValueFunc()
	{
		int? setupValue = null;

		var fixture = CreateFixture<int?>();
		fixture.Returns(() => setupValue);
		var actual = fixture.Execute(out var returnValue);

		Assert.True(actual);
		Assert.Equal(setupValue, returnValue);
	}

	[Fact]
	public void InvokeCallbackBeforeReturns()
	{
		const int setupValue = 123;
		var counter = 0;

		var fixture = CreateFixture<int>();
		fixture.Returns(setupValue);
		fixture.Callback(() => counter++);

		var hasValue = fixture.Execute(out var actual);

		const int expected = 1;
		Assert.True(hasValue);
		Assert.Equal(expected, counter);
		Assert.Equal(setupValue, actual);
	}

	[Fact]
	public void InvokeCallbackBeforeThrows()
	{
		const string errorMessage = nameof(errorMessage);
		var counter = 0;

		var fixture = CreateFixture<int>();
		fixture.Throws(new IndexOutOfRangeException(errorMessage));
		fixture.Callback(() => counter++);

		Action actual = () => fixture.Execute(out _);

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
		fixture.Callback(() => counter++);

		var hasValue = fixture.Execute(out _);

		const int expected = 1;
		Assert.False(hasValue);
		Assert.Equal(expected, counter);
	}
}
