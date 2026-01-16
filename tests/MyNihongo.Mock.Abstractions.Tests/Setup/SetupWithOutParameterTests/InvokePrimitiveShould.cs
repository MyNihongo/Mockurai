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
		fixture.Callback((out x) => x = counter++);
		fixture.And();
		fixture.Throws(new IndexOutOfRangeException(errorMessage));

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
		fixture.Callback((out x) => x = counter++);
		fixture.Invoke(out _);

		const int expected = 1;
		Assert.Equal(expected, counter);
	}

	[Fact]
	public void ReturnValueInCallback()
	{
		const int expected = 12345;

		var fixture = CreateFixture<int>();
		fixture.Callback((out x) => x = expected);
		fixture.Invoke(out var result);

		Assert.Equal(expected, result);
	}

	[Fact]
	public void ThrowDifferentExceptions()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);

		var fixture = CreateFixture<int>();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		var actual1 = () => fixture.Invoke(out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);
	}
	
	[Fact]
	public void ThrowDifferentExceptionsWithCallback1()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<int>();
		fixture.Callback((out x) => x = callback++);
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));
		
		fixture.Invoke(out _);

		var actual2 = () => fixture.Invoke(out _);
		var exception2 = Assert.Throws<COMException>(actual2);
		Assert.Equal(errorMessage1, exception2.Message);

		var actual3 = () => fixture.Invoke(out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);
		
		var actual4 = () => fixture.Invoke(out _);
		var exception4 = Assert.Throws<NullReferenceException>(actual4);
		Assert.Equal(errorMessage2, exception4.Message);

		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback2()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<int>();
		fixture.Callback((out x) => x = callback++);
		fixture.And();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));

		var actual1 = () => fixture.Invoke(out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback = 1;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback3()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		var callback = 0;

		var fixture = CreateFixture<int>();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Callback((out x) => x = callback++);
		fixture.And();
		fixture.Throws(new NullReferenceException(errorMessage2));

		var actual1 = () => fixture.Invoke(out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback = 2;
		Assert.Equal(expectedCallback, callback);
	}

	[Fact]
	public void ThrowDifferentExceptionsWithCallback4()
	{
		const string errorMessage1 = nameof(errorMessage1), errorMessage2 = nameof(errorMessage2);
		int callback1 = 10, callback2 = 0;

		var fixture = CreateFixture<int>();
		fixture.Callback((out x) => x = callback1++);
		fixture.And();
		fixture.Throws(new COMException(errorMessage1));
		fixture.Throws(new NullReferenceException(errorMessage2));
		fixture.And();
		fixture.Callback((out x) => x = callback2++);

		var actual1 = () => fixture.Invoke(out _);
		var exception1 = Assert.Throws<COMException>(actual1);
		Assert.Equal(errorMessage1, exception1.Message);

		var actual2 = () => fixture.Invoke(out _);
		var exception2 = Assert.Throws<NullReferenceException>(actual2);
		Assert.Equal(errorMessage2, exception2.Message);

		var actual3 = () => fixture.Invoke(out _);
		var exception3 = Assert.Throws<NullReferenceException>(actual3);
		Assert.Equal(errorMessage2, exception3.Message);

		const int expectedCallback1 = 11, expectedCallback2 = 2;
		Assert.Equal(expectedCallback1, callback1);
		Assert.Equal(expectedCallback2, callback2);
	}
}
