namespace MyNihongo.Mock.Sample._Generated.InvocationWithSeveralParametersTests;

public sealed class VerifyPrimitiveShould : InvocationWithSeveralParametersTestsBase
{
	[Fact]
	public void VerifyAny()
	{
		var index = 0L;
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);
		fixture.Register(ref index, 234, 345);

		const int expected = 2;
		fixture.Verify(verify1, verify2, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyAnyWhereAll1()
	{
		var index = 0L;
		It<int> verify1 = It<int>.Where(x => x > 100), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);
		fixture.Register(ref index, 234, 345);

		const int expected = 2;
		fixture.Verify(verify1, verify2, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyAnyWhereAll2()
	{
		var index = 0L;
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Where(x => x > 200);

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);
		fixture.Register(ref index, 234, 345);

		const int expected = 2;
		fixture.Verify(verify1, verify2, Times.Exactly(expected));
	}

	[Fact]
	public void VerifyAnyWherePartial1()
	{
		var index = 0L;
		It<int> verify1 = It<int>.Where(x => x > 200), verify2 = It<int>.Any();

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);
		fixture.Register(ref index, 234, 345);

		fixture.Verify(verify1, verify2, Times.Once());
	}

	[Fact]
	public void VerifyAnyWherePartial2()
	{
		var index = 0L;
		It<int> verify1 = It<int>.Any(), verify2 = It<int>.Where(x => x > 300);

		var fixture = CreateFixturePrimitive();
		fixture.Register(ref index, 123, 234);
		fixture.Register(ref index, 234, 345);

		fixture.Verify(verify1, verify2, Times.Once());
	}
}
