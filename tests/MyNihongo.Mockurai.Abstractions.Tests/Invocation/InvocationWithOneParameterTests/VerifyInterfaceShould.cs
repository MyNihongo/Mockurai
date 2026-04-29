namespace MyNihongo.Mockurai.Abstractions.Tests.Invocation.InvocationWithOneParameterTests;

public sealed class VerifyInterfaceShould : InvocationWithOneParameterTestsBase
{
	[Fact]
	public void VerifyEquivalent()
	{
		var input = new Class
		{
			Count = 1,
			Name = "Okayama Issei",
		};

		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<IInterface>();
		fixture.Register(index, input);

		var verify = It<IInterface>.Equivalent(input);
		fixture.Verify(verify.ValueSetup, Times.Once());
	}

	[Fact]
	public void ThrowIfNotVerifiedEquivalent()
	{
		const int number = 1;
		const string name = "Okayama Issei";

		var index = new InvocationIndex.Counter();

		var fixture = CreateFixture<IInterface>();
		fixture.Register(index, new Class
		{
			Count = number,
			Name = name,
		});

		var actual = () =>
		{
			var verify = It<IInterface>.Equivalent(new Class
			{
				Count = number + 1,
				Name = name,
			});
			fixture.Verify(verify.ValueSetup, Times.Once());
		};

		const string expectedMessage =
			"""
			Expected MyClass.MyMethod({"Count":2,"Name":"Okayama Issei"}) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: MyClass.MyMethod({"Count":1,"Name":"Okayama Issei"})
			  - Count:
			    expected: 2
			    actual: 1
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	private interface IInterface
	{
		int Count { get; }

		string Name { get; }
	}

	public sealed class Class : IInterface
	{
		public int Count { get; init; }
		public required string Name { get; init; }
	}
}
