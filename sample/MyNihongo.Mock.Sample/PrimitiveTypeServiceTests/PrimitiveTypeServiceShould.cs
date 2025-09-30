namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class PrimitiveTypeServiceShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ThrowInvalidSequenceMultipleCalls()
	{
		var value = 1234m;
		var valueInt = 98;
		var valueDouble = 3488d;

		var fixture = CreateFixture();
		fixture.HandlerEvent += (_, _) => { };
		_ = fixture.GetOnly;
		fixture.SetOnly = 123m;
		fixture.GetInit = "value";
		fixture.Invoke();
		fixture.Invoke(out _);
		fixture.InvokeWithParameter(345);
		fixture.InvokeWithParameter("another value");
		fixture.InvokeWithParameter(ref value);
		fixture.InvokeWithSeveralParameters(1, 2);
		fixture.InvokeWithSeveralParameters(ref valueInt, 2);
		fixture.InvokeWithSeveralParameters(1, ref valueInt);
		fixture.InvokeWithSeveralParameters(ref valueInt, ref valueInt);
		fixture.Return();
		fixture.Return(out _);
		fixture.ReturnWithParameter("ret val");
		fixture.ReturnWithParameter(ref valueDouble);
		fixture.ReturnWithSeveralParameters(1, 2);
		fixture.ReturnWithSeveralParameters(ref valueInt, 2);
		fixture.ReturnWithSeveralParameters(1, ref valueInt);
		fixture.ReturnWithSeveralParameters(ref valueInt, ref valueInt);

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Invoke();
			ctx.DependencyServiceMock.InvokeWithParameter(8374646);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService#InvokeWithParameter(8374646) to be invoked at index 6, but it has not been called.
			Performed invocations:
			- 1: System.EventHandler`1[System.String]
			- 2
			- 3: 123
			- 4: "value"
			- 5
			- 6: out 0
			- 8: "another value"
			- 7: 345
			- 9: ref 1234
			- 10: 1, 2
			- 11: 98, 2
			- 12: 1, 98
			- 13: 98, 98
			- 14
			- 15: out null
			- 16: "ret val"
			- 17: ref 3488
			- 18: 1, 2
			- 19: 98, 2
			- 20: 1, 98
			- 21: 98, 98
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
