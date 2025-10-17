namespace MyNihongo.Mock.Abstractions.Tests.Collections.InvocationContainerTests;

public sealed class TryGetItemAtShould : InvocationContainerTestsBase
{
	[Fact]
	public void ReturnNextElementStart()
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 2 });
		fixture.Add(new Invocation { Index = 3 });
		fixture.Add(new Invocation { Index = 7 });
		fixture.Add(new Invocation { Index = 10 });

		var actual = fixture.TryGetItemAt(index: 0);

		var expected = new Invocation { Index = 2 };
		Assert.Equivalent(expected, actual);
	}

	[Fact]
	public void ReturnNextElementMiddle()
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 1 });
		fixture.Add(new Invocation { Index = 3 });
		fixture.Add(new Invocation { Index = 7 });
		fixture.Add(new Invocation { Index = 10 });

		var actual = fixture.TryGetItemAt(index: 4);

		var expected = new Invocation { Index = 7 };
		Assert.Equivalent(expected, actual);
	}

	[Fact]
	public void ReturnNextElementEnd()
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 1 });
		fixture.Add(new Invocation { Index = 3 });
		fixture.Add(new Invocation { Index = 7 });
		fixture.Add(new Invocation { Index = 10 });

		var actual = fixture.TryGetItemAt(index: 10);

		var expected = new Invocation { Index = 10 };
		Assert.Equivalent(expected, actual);
	}

	[Fact]
	public void ReturnNullIfNotFound()
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 1 });
		fixture.Add(new Invocation { Index = 3 });
		fixture.Add(new Invocation { Index = 7 });
		fixture.Add(new Invocation { Index = 10 });

		var actual = fixture.TryGetItemAt(index: 100);
		Assert.Null(actual);
	}
}
