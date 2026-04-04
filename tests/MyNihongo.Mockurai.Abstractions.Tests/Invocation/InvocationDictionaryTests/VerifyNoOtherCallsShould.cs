namespace MyNihongo.Mockurai.Abstractions.Tests.Invocation.InvocationDictionaryTests;

public sealed class VerifyNoOtherCallsShould
{
	[Fact]
	public void NotThrowIfEmpty()
	{
		new InvocationDictionary()
			.VerifyNoOtherCalls();

		new InvocationDictionary<(Type, Type)>()
			.VerifyNoOtherCalls();
	}

	[Fact]
	public void ThrowIfUnverifiedSingle()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation invocation = new("int");
		invocation.Register(index);

		var fixture = new InvocationDictionary();
		fixture.GetOrAdd(typeof(int), invocation);

		var actual = () => fixture.VerifyNoOtherCalls(() => [invocation]);

		const string expected =
			"""
			Expected int to be verified, but the following invocations have not been verified:
			- 1: int
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expected, exception.Message);
	}

	[Fact]
	public void ThrowIfUnverifiedSingleMultipleTypes()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation invocation = new("int-int");
		invocation.Register(index);

		var fixture = new InvocationDictionary<(Type, Type)>();
		fixture.GetOrAdd((typeof(int), typeof(int)), invocation);

		var actual = () => fixture.VerifyNoOtherCalls(() => [invocation]);

		const string expected =
			"""
			Expected int-int to be verified, but the following invocations have not been verified:
			- 1: int-int
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expected, exception.Message);
	}

	[Fact]
	public void NotThrowIfVerifiedSingle()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation invocation = new("int");
		invocation.Register(index);
		invocation.Verify(Times.Once());

		var fixture = new InvocationDictionary();
		fixture.GetOrAdd(typeof(int), invocation);
		fixture.VerifyNoOtherCalls(() => [invocation]);
	}

	[Fact]
	public void NotThrowIfVerifiedSingleMultipleTypes()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation invocation = new("int-int");
		invocation.Register(index);
		invocation.Verify(Times.Once());

		var fixture = new InvocationDictionary<(Type, Type)>();
		fixture.GetOrAdd((typeof(int), typeof(int)), invocation);
		fixture.VerifyNoOtherCalls(() => [invocation]);
	}

	[Fact]
	public void ThrowIfUnverifiedMultiple1()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation invocation1 = new("int"), invocation2 = new("string");
		invocation1.Register(index);
		invocation1.Verify(Times.Once());
		invocation2.Register(index);

		var fixture = new InvocationDictionary();
		fixture.GetOrAdd(typeof(int), invocation1);
		fixture.GetOrAdd(typeof(string), invocation2);

		var actual = () => fixture.VerifyNoOtherCalls(() => [invocation1, invocation2]);

		const string expected =
			"""
			Expected string to be verified, but the following invocations have not been verified:
			- 2: string
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expected, exception.Message);
	}

	[Fact]
	public void ThrowIfUnverifiedMultipleMultipleTypes1()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation invocation1 = new("int-int"), invocation2 = new("int-string");
		invocation1.Register(index);
		invocation1.Verify(Times.Once());
		invocation2.Register(index);

		var fixture = new InvocationDictionary<(Type, Type)>();
		fixture.GetOrAdd((typeof(int), typeof(int)), invocation1);
		fixture.GetOrAdd((typeof(int), typeof(string)), invocation2);

		var actual = () => fixture.VerifyNoOtherCalls(() => [invocation1, invocation2]);

		const string expected =
			"""
			Expected int-string to be verified, but the following invocations have not been verified:
			- 2: int-string
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expected, exception.Message);
	}

	[Fact]
	public void ThrowIfUnverifiedMultiple2()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation invocation1 = new("int"), invocation2 = new("string");
		invocation1.Register(index);
		invocation2.Register(index);
		invocation2.Verify(Times.Once());

		var fixture = new InvocationDictionary();
		fixture.GetOrAdd(typeof(int), invocation1);
		fixture.GetOrAdd(typeof(string), invocation2);

		var actual = () => fixture.VerifyNoOtherCalls(() => [invocation1, invocation2]);

		const string expected =
			"""
			Expected int to be verified, but the following invocations have not been verified:
			- 1: int
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expected, exception.Message);
	}

	[Fact]
	public void ThrowIfUnverifiedMultipleMultipleTypes2()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation invocation1 = new("int-int"), invocation2 = new("int-string");
		invocation1.Register(index);
		invocation2.Register(index);
		invocation2.Verify(Times.Once());

		var fixture = new InvocationDictionary<(Type, Type)>();
		fixture.GetOrAdd((typeof(int), typeof(int)), invocation1);
		fixture.GetOrAdd((typeof(int), typeof(string)), invocation2);

		var actual = () => fixture.VerifyNoOtherCalls(() => [invocation1, invocation2]);

		const string expected =
			"""
			Expected int-int to be verified, but the following invocations have not been verified:
			- 1: int-int
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expected, exception.Message);
	}

	[Fact]
	public void ThrowIfUnverifiedMultiple3()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation invocation1 = new("int"), invocation2 = new("string");
		invocation1.Register(index);
		invocation2.Register(index);

		var fixture = new InvocationDictionary();
		fixture.GetOrAdd(typeof(int), invocation1);
		fixture.GetOrAdd(typeof(string), invocation2);

		var actual = () => fixture.VerifyNoOtherCalls(() => [invocation1, invocation2]);

		const string expected =
			"""
			Expected string to be verified, but the following invocations have not been verified:
			- 1: int
			- 2: string
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expected, exception.Message);
	}

	[Fact]
	public void ThrowIfUnverifiedMultipleMultipleTypes3()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation invocation1 = new("int-int"), invocation2 = new("int-string");
		invocation1.Register(index);
		invocation2.Register(index);

		var fixture = new InvocationDictionary<(Type, Type)>();
		fixture.GetOrAdd((typeof(int), typeof(int)), invocation1);
		fixture.GetOrAdd((typeof(int), typeof(string)), invocation2);

		var actual = () => fixture.VerifyNoOtherCalls(() => [invocation1, invocation2]);

		const string expected =
			"""
			to be verified, but the following invocations have not been verified:
			- 1: int-int
			- 2: int-string
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.EndsWith(expected, exception.Message);
	}

	[Fact]
	public void NotThrowIfVerifiedMultiple3()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation invocation1 = new("int"), invocation2 = new("string");
		invocation1.Register(index);
		invocation1.Verify(Times.Once());
		invocation2.Register(index);
		invocation2.Verify(Times.Once());

		var fixture = new InvocationDictionary();
		fixture.GetOrAdd(typeof(int), invocation1);
		fixture.GetOrAdd(typeof(string), invocation2);

		fixture.VerifyNoOtherCalls(() => [invocation1, invocation2]);
	}

	[Fact]
	public void NotThrowIfVerifiedMultipleMultipleTypes()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation invocation1 = new("int-int"), invocation2 = new("int-string");
		invocation1.Register(index);
		invocation1.Verify(Times.Once());
		invocation2.Register(index);
		invocation2.Verify(Times.Once());

		var fixture = new InvocationDictionary<(Type, Type)>();
		fixture.GetOrAdd((typeof(int), typeof(int)), invocation1);
		fixture.GetOrAdd((typeof(int), typeof(string)), invocation2);

		fixture.VerifyNoOtherCalls(() => [invocation1, invocation2]);
	}
}
