namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class VerifyMultipleShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ThrowInvalidSequenceMultipleCallsVerify()
	{
		CreateFixture()
			.InvokeAll();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(ItRef<int>.Any(), 2);
			ctx.DependencyServiceMock.Invoke();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Invoke() to be invoked at index 12, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.HandlerEvent.add
			- 2: IPrimitiveDependencyService.GetOnly.get
			- 3: IPrimitiveDependencyService.SetOnly.set = 123
			- 4: IPrimitiveDependencyService.GetInit.set = "value"
			- 5: IPrimitiveDependencyService.Invoke()
			- 6: IPrimitiveDependencyService.Invoke(out 0)
			- 7: IPrimitiveDependencyService.InvokeWithParameter(345)
			- 8: IPrimitiveDependencyService.InvokeWithParameter("another value")
			- 9: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 10: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 11: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 12: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 13: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 14: IPrimitiveDependencyService.Return()
			- 15: IPrimitiveDependencyService.Return(out null)
			- 16: IPrimitiveDependencyService.ReturnWithParameter("ret val")
			- 17: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 18: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 19: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 20: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 21: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
	
	[Fact]
	public void ThrowInvalidSequenceMultipleCallsVerifyOneParam()
	{
		CreateFixture()
			.InvokeAll();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Invoke();
			ctx.DependencyServiceMock.InvokeWithParameter(8374646);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(8374646) to be invoked at index 6, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.HandlerEvent.add
			- 2: IPrimitiveDependencyService.GetOnly.get
			- 3: IPrimitiveDependencyService.SetOnly.set = 123
			- 4: IPrimitiveDependencyService.GetInit.set = "value"
			- 5: IPrimitiveDependencyService.Invoke()
			- 6: IPrimitiveDependencyService.Invoke(out 0)
			- 7: IPrimitiveDependencyService.InvokeWithParameter(345)
			- 8: IPrimitiveDependencyService.InvokeWithParameter("another value")
			- 9: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 10: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 11: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 12: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 13: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 14: IPrimitiveDependencyService.Return()
			- 15: IPrimitiveDependencyService.Return(out null)
			- 16: IPrimitiveDependencyService.ReturnWithParameter("ret val")
			- 17: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 18: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 19: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 20: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 21: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequenceMultipleCallsVerifyMultipleParams()
	{
		CreateFixture()
			.InvokeAll();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.Invoke();
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(8374646, 2843253);
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(8374646, 2843253) to be invoked at index 6, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.HandlerEvent.add
			- 2: IPrimitiveDependencyService.GetOnly.get
			- 3: IPrimitiveDependencyService.SetOnly.set = 123
			- 4: IPrimitiveDependencyService.GetInit.set = "value"
			- 5: IPrimitiveDependencyService.Invoke()
			- 6: IPrimitiveDependencyService.Invoke(out 0)
			- 7: IPrimitiveDependencyService.InvokeWithParameter(345)
			- 8: IPrimitiveDependencyService.InvokeWithParameter("another value")
			- 9: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 10: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 11: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 12: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 13: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 14: IPrimitiveDependencyService.Return()
			- 15: IPrimitiveDependencyService.Return(out null)
			- 16: IPrimitiveDependencyService.ReturnWithParameter("ret val")
			- 17: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 18: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 19: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 20: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 21: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}
}

file static class PrimitiveTypeServiceEx
{
	public static void InvokeAll(this IPrimitiveTypeService fixture)
	{
		var value = 1234m;
		var valueInt = 98;
		var valueDouble = 3488d;

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
	}
}
