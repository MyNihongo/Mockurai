namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class VerifyMultipleShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public void ThrowInvalidInvocation()
	{
		CreateFixture()
			.InvokeAll();

		var actual = () => DependencyServiceMock
			.VerifyInvoke(Times.Exactly(100));

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Invoke() to be called 100 times, but instead it was called 1 time.
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
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidInvocationOneParam()
	{
		CreateFixture()
			.InvokeAll();

		var actual = () => DependencyServiceMock
			.VerifyInvokeWithParameter(8374646, Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(8374646) to be called 1 time, but instead it was called 0 times.
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
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidInvocationMultipleParams()
	{
		CreateFixture()
			.InvokeAll();

		var actual = () => DependencyServiceMock
			.VerifyInvokeWithSeveralParameters(8374646, 2843253, Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(8374646, 2843253) to be called 1 time, but instead it was called 0 times.
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
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowInvalidSequence()
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
	public void ThrowInvalidSequenceOneParam()
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
	public void ThrowInvalidSequenceMultipleParams()
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

	[Fact]
	public void ThrowVerifyNoOtherCalls()
	{
		var fixture = CreateFixture();
		fixture.Invoke();
		fixture.InvokeWithParameter("param");
		fixture.InvokeWithSeveralParameters(123, 321);

		var actual = VerifyNoOtherCalls;

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Invoke() to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.Invoke()
			- 2: IPrimitiveDependencyService.InvokeWithParameter("param")
			- 3: IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsOneParam()
	{
		var fixture = CreateFixture();
		fixture.InvokeWithParameter("param");
		fixture.InvokeWithSeveralParameters(123, 321);

		var actual = VerifyNoOtherCalls;

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(String) to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.InvokeWithParameter("param")
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsMultipleParams()
	{
		var input = 43321;

		var fixture = CreateFixture();
		fixture.InvokeWithSeveralParameters(123, 321);
		fixture.InvokeWithSeveralParameters(ref input, 321);
		fixture.ReturnWithSeveralParameters(123, ref input);
		fixture.ReturnWithSeveralParameters(ref input, ref input);

		var actual = VerifyNoOtherCalls;

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(Int32, Int32) to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 43321, 321)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters(123, ref 43321)
			- 4: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 43321, ref 43321)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
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
