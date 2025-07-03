namespace MyNihongo.Mock.Sample._Generated.SetupReturnsTests;

public sealed class ExecutePrimitiveShould : SetupReturnsTestsBase
{
	[Fact]
	public void ReturnDefault()
	{
		var fixture = CreateFixture<int>();
		var actual = fixture.Execute(out var returnValue);

		const int expected = 0;
		Assert.True(actual);
		Assert.Equal(expected, returnValue);
	}

	[Fact]
	public void ReturnNull()
	{
		var fixture = CreateFixture<int?>();
		var actual = fixture.Execute(out var returnValue);

		Assert.True(actual);
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
}
