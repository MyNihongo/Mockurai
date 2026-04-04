namespace MyNihongo.Mockurai.Abstractions.Tests.Collections.InvocationContainerTests;

public sealed class AddShould : InvocationContainerTestsBase
{
	[Fact]
	public void AddInAscendingOrder()
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 2 });
		fixture.Add(new Invocation { Index = 4 });
		fixture.Add(new Invocation { Index = 5 });

		var actual = fixture.ToArray();

		var expected = new Invocation[]
		{
			new() { Index = 2 },
			new() { Index = 4 },
			new() { Index = 5 },
		};
		Assert.Equivalent(expected, actual);
	}
	
	[Fact]
	public void AddInDescendingOrder()
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 5 });
		fixture.Add(new Invocation { Index = 4 });
		fixture.Add(new Invocation { Index = 2 });

		var actual = fixture.ToArray();

		var expected = new Invocation[]
		{
			new() { Index = 2 },
			new() { Index = 4 },
			new() { Index = 5 },
		};
		Assert.Equivalent(expected, actual);
	}

	[Fact]
	public void AddInMixedOrder()
	{
		var fixture = CreateFixture();
		fixture.Add(new Invocation { Index = 2 });
		fixture.Add(new Invocation { Index = 1 });
		fixture.Add(new Invocation { Index = 10, Label = "1" });
		fixture.Add(new Invocation { Index = 11, Label = "1" });
		fixture.Add(new Invocation { Index = 7, Label = "1" });
		fixture.Add(new Invocation { Index = 4 });
		fixture.Add(new Invocation { Index = 3 });
		fixture.Add(new Invocation { Index = 6 });
		fixture.Add(new Invocation { Index = 7, Label = "2" });
		fixture.Add(new Invocation { Index = 12 });
		fixture.Add(new Invocation { Index = 11, Label = "2" });
		fixture.Add(new Invocation { Index = 10, Label = "2" });

		var actual = fixture.ToArray();

		var expected = new Invocation[]
		{
			new() { Index = 1 },
			new() { Index = 2 },
			new() { Index = 3 },
			new() { Index = 4 },
			new() { Index = 6 },
			new() { Index = 7,  Label = "2" },
			new() { Index = 7, Label = "1" },
			new() { Index = 10, Label = "2" },
			new() { Index = 10, Label = "1" },
			new() { Index = 11, Label = "2" },
			new() { Index = 11, Label = "1" },
			new() { Index = 12 },
		};
		Assert.Equivalent(expected, actual);
	}
}
