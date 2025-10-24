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
		fixture.Callback(() => counter++);
		fixture.Returns(setupValue);

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
		fixture.Callback(() => counter++);
		fixture.Throws(new IndexOutOfRangeException(errorMessage));

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

	[Fact]
	public void ReturnDifferentValues()
	{
		const int setupValue1 = 123, setupValue2 = 234;

		var fixture = CreateFixture<int>();
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);

		var actual1 = fixture.Execute(out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback1()
	{
		const int setupValue1 = 123, setupValue2 = 234;
		var callback = 0;

		var fixture = CreateFixture<int>();
		fixture.Callback(() => callback++);
		fixture.Returns(setupValue1);
		fixture.Returns(setupValue2);

		var actual1 = fixture.Execute(out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);

		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback2()
	{
		const int setupValue1 = 123, setupValue2 = 234;
		var callback = 0;

		var fixture = CreateFixture<int>();
		fixture.Returns(setupValue1);
		fixture.Callback(() => callback++);
		fixture.Returns(setupValue2);

		var actual1 = fixture.Execute(out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);

		const int expectedCallback = 2;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ReturnDifferentValuesWithCallback3()
	{
		const int setupValue1 = 123, setupValue2 = 234;
		int callback1 = 10, callback2 = 0;

		var fixture = CreateFixture<int>();
		fixture.Callback(() => callback1++);
		fixture.Returns(setupValue1);
		fixture.Callback(() => callback2++);
		fixture.Returns(setupValue2);

		var actual1 = fixture.Execute(out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(setupValue1, returnValue1);

		var actual2 = fixture.Execute(out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(setupValue2, returnValue2);

		var actual3 = fixture.Execute(out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(setupValue2, returnValue3);

		const int expectedCallback1 = 11, expectedCallback2 = 2;
		Assert.Equal(expectedCallback1, callback1);
		Assert.Equal(expectedCallback2, callback2);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		var fixture = CreateFixture<int>();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		Action actual1 = () => fixture.Execute(out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<int>();
		fixture.Callback(() => callback++);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		Action actual1 = () => fixture.Execute(out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<int>();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Callback(() => callback++);
		fixture.Throws(new NullReferenceException(errorMessage2));

		Action actual1 = () => fixture.Execute(out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback = 2;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		int callback1 = 10, callback2 = 0;

		var fixture = CreateFixture<int>();
		fixture.Callback(() => callback1++);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Callback(() => callback2++);
		fixture.Throws(new NullReferenceException(errorMessage2));

		Action actual1 = () => fixture.Execute(out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		Action actual2 = () => fixture.Execute(out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		Action actual3 = () => fixture.Execute(out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback1 = 11, expectedCallback2 = 2;
		Assert.Equal(expectedCallback1, callback1);
		Assert.Equal(expectedCallback2, callback2);
	}

	[Fact]
	public void ThrowExceptionWithReturn()
	{
		const string errorMessage = nameof(errorMessage);
		const int expected = 1234;

		var fixture = CreateFixture<int>();
		fixture.Returns(expected);
		fixture.Throws(new IndexOutOfRangeException(errorMessage));

		var actual1 = fixture.Execute(out var returnValue1);
		Assert.True(actual1);
		Assert.Equal(expected, returnValue1);

		Action actual2 = () => fixture.Execute(out _);
		var exception2 = Assert.Throws<IndexOutOfRangeException>(actual2);
		Assert.Equal(errorMessage, exception2.Message);

		Action actual3 = () => fixture.Execute(out _);
		var exception3 = Assert.Throws<IndexOutOfRangeException>(actual3);
		Assert.Equal(errorMessage, exception3.Message);
	}

	[Fact]
	public void ReturnWithThrowException()
	{
		const string errorMessage = nameof(errorMessage);
		const int expected = 1234;

		var fixture = CreateFixture<int>();
		fixture.Throws(new IndexOutOfRangeException(errorMessage));
		fixture.Returns(expected);

		Action actual1 = () => fixture.Execute(out _);
		var exception1 = Assert.Throws<IndexOutOfRangeException>(actual1);
		Assert.Equal(errorMessage, exception1.Message);

		var actual2 = fixture.Execute(out var returnValue2);
		Assert.True(actual2);
		Assert.Equal(expected, returnValue2);

		var actual3 = fixture.Execute(out var returnValue3);
		Assert.True(actual3);
		Assert.Equal(expected, returnValue3);
	}
}
