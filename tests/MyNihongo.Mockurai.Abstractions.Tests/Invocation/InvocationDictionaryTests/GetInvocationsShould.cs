namespace MyNihongo.Mockurai.Abstractions.Tests.Invocation.InvocationDictionaryTests;

public sealed class GetInvocationsShould
{
	[Fact]
	public void ReturnSingleType()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation intInvocation = new("int"), stringInvocation = new("string");
		intInvocation.Register(index);
		stringInvocation.Register(index);
		intInvocation.Register(index);

		var fixture = new InvocationDictionary();
		fixture.GetOrAdd(typeof(int), intInvocation);
		fixture.GetOrAdd(typeof(string), stringInvocation);

		var actual = fixture.GetInvocations()
			.Select(x => x.ToString());

		var expected = new[] { "1: int", "2: string", "3: int" };
		Assert.Equivalent(expected, actual);
	}

	[Fact]
	public void NotOverwriteSingleType()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation intInvocation1 = new("int1"), intInvocation2 = new("int2"), stringInvocation = new("string");
		intInvocation1.Register(index);
		stringInvocation.Register(index);
		intInvocation1.Register(index);
		intInvocation2.Register(index);
		intInvocation2.Register(index);
		intInvocation2.Register(index);

		var fixture = new InvocationDictionary();
		fixture.GetOrAdd(typeof(int), intInvocation1);
		fixture.GetOrAdd(typeof(string), stringInvocation);
		fixture.GetOrAdd(typeof(int), intInvocation2);

		var actual = fixture.GetInvocations()
			.Select(x => x.ToString());

		var expected = new[] { "1: int1", "2: string", "3: int1" };
		Assert.Equivalent(expected, actual);
	}

	[Fact]
	public void ReturnMultipleTypes()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation intIntInvocation = new("int-int"),
			intStringInvocation = new("int-string"),
			stringIntInvocation = new("string-int"),
			stringStringInvocation = new("string-string");

		intIntInvocation.Register(index);
		intStringInvocation.Register(index);
		intIntInvocation.Register(index);
		stringIntInvocation.Register(index);
		stringIntInvocation.Register(index);
		stringStringInvocation.Register(index);

		var fixture = new InvocationDictionary<(Type, Type)>();
		fixture.GetOrAdd((typeof(int), typeof(int)), intIntInvocation);
		fixture.GetOrAdd((typeof(int), typeof(string)), intStringInvocation);
		fixture.GetOrAdd((typeof(string), typeof(int)), stringIntInvocation);
		fixture.GetOrAdd((typeof(string), typeof(string)), stringStringInvocation);

		var actual = fixture.GetInvocations()
			.Select(x => x.ToString());

		var expected = new[] { "1: int-int", "2: int-string", "3: int-int", "4: string-int", "5: string-int", "6: string-string" };
		Assert.Equivalent(expected, actual);
	}

	[Fact]
	public void NotOverwriteMultipleTypes()
	{
		var index = new InvocationIndex.Counter();

		Mockurai.Invocation intIntInvocation1 = new("int-int-1"),
			intIntInvocation2 = new("int-int-2"),
			intStringInvocation1 = new("int-string-1"),
			intStringInvocation2 = new("int-string-2"),
			stringIntInvocation1 = new("string-int-1"),
			stringIntInvocation2 = new("string-int-2"),
			stringStringInvocation1 = new("string-string-1"),
			stringStringInvocation2 = new("string-string-2");

		intIntInvocation1.Register(index);
		intStringInvocation1.Register(index);
		intIntInvocation1.Register(index);
		stringIntInvocation1.Register(index);
		stringIntInvocation1.Register(index);
		stringStringInvocation1.Register(index);
		intIntInvocation2.Register(index);
		intStringInvocation2.Register(index);
		stringIntInvocation2.Register(index);
		stringStringInvocation2.Register(index);

		var fixture = new InvocationDictionary<(Type, Type)>();
		fixture.GetOrAdd((typeof(int), typeof(int)), intIntInvocation1);
		fixture.GetOrAdd((typeof(int), typeof(string)), intStringInvocation1);
		fixture.GetOrAdd((typeof(string), typeof(int)), stringIntInvocation1);
		fixture.GetOrAdd((typeof(string), typeof(string)), stringStringInvocation1);

		fixture.GetOrAdd((typeof(int), typeof(int)), intIntInvocation2);
		fixture.GetOrAdd((typeof(int), typeof(string)), intStringInvocation2);
		fixture.GetOrAdd((typeof(string), typeof(int)), stringIntInvocation2);
		fixture.GetOrAdd((typeof(string), typeof(string)), stringStringInvocation2);

		var actual = fixture.GetInvocations()
			.Select(x => x.ToString());

		var expected = new[] { "1: int-int-1", "2: int-string-1", "3: int-int-1", "4: string-int-1", "5: string-int-1", "6: string-string-1" };
		Assert.Equivalent(expected, actual);
	}
}
