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
			- 1: IPrimitiveDependencyService#HandlerEvent#add
			- 2: IPrimitiveDependencyService#GetOnly#get
			- 3: IPrimitiveDependencyService#SetOnly#set = 123
			- 4: IPrimitiveDependencyService#GetInit#set = "value"
			- 5: IPrimitiveDependencyService#Invoke()
			- 6: IPrimitiveDependencyService#Invoke(out out 0)
			- 7: 345
			- 8: IPrimitiveDependencyService#InvokeWithParameter("another value")
			- 9: IPrimitiveDependencyService#InvokeWithParameter(ref ref 1234)
			- 10: IPrimitiveDependencyService#InvokeWithSeveralParameters(1, 2, )
			- 11: IPrimitiveDependencyService#InvokeWithSeveralParameters(ref 98, 2, )
			- 12: IPrimitiveDependencyService#InvokeWithSeveralParameters(1, 98, ref )
			- 13: IPrimitiveDependencyService#InvokeWithSeveralParameters(ref 98, 98, ref )
			- 14: IPrimitiveDependencyService#Return()
			- 15: IPrimitiveDependencyService#Return(out out null)
			- 16: IPrimitiveDependencyService#ReturnWithParameter("ret val")
			- 17: IPrimitiveDependencyService#ReturnWithParameter(ref ref 3488)
			- 18: IPrimitiveDependencyService#ReturnWithSeveralParameters(1, 2, )
			- 19: IPrimitiveDependencyService#ReturnWithSeveralParameters(ref 98, 2, )
			- 20: IPrimitiveDependencyService#ReturnWithSeveralParameters(1, 98, ref )
			- 21: IPrimitiveDependencyService#ReturnWithSeveralParameters(ref 98, 98, ref )
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}
