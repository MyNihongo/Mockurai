namespace MyNihongo.Mock.Sample.PrimitiveTypeServiceTests;

public sealed class VerifyMultipleShould : PrimitiveTypeServiceTestsBase
{
	[Fact]
	public async Task ThrowInvalidInvocation()
	{
		await CreateFixture()
			.InvokeAllAsync();

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
			- 7: IPrimitiveDependencyService.Invoke<Double>()
			- 8: IPrimitiveDependencyService.InvokeAsync()
			- 9: IPrimitiveDependencyService.InvokeWithParameter(345)
			- 10: IPrimitiveDependencyService.InvokeWithParameter("another value")
			- 11: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 12: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 13: IPrimitiveDependencyService.InvokeWithParameterAsync(98)
			- 14: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 15: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 16: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 17: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 18: IPrimitiveDependencyService.InvokeWithSeveralParameters<Decimal>(83256, 98)
			- 19: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(98, 99)
			- 20: IPrimitiveDependencyService.Return()
			- 21: IPrimitiveDependencyService.Return(out null)
			- 22: IPrimitiveDependencyService.Return<Decimal>()
			- 23: IPrimitiveDependencyService.ReturnAsync()
			- 24: IPrimitiveDependencyService.ReturnWithParameter("ret val")
			- 25: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 26: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 27: IPrimitiveDependencyService.ReturnWithParameterAsync("async value")
			- 28: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 29: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 30: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 31: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			- 32: IPrimitiveDependencyService.ReturnWithSeveralParameters<Int32, Single, Int16>(ref 98, 541)
			- 33: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(100, 101)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidInvocationOneParam()
	{
		await CreateFixture()
			.InvokeAllAsync();

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
			- 7: IPrimitiveDependencyService.Invoke<Double>()
			- 8: IPrimitiveDependencyService.InvokeAsync()
			- 9: IPrimitiveDependencyService.InvokeWithParameter(345)
			- 10: IPrimitiveDependencyService.InvokeWithParameter("another value")
			- 11: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 12: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 13: IPrimitiveDependencyService.InvokeWithParameterAsync(98)
			- 14: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 15: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 16: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 17: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 18: IPrimitiveDependencyService.InvokeWithSeveralParameters<Decimal>(83256, 98)
			- 19: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(98, 99)
			- 20: IPrimitiveDependencyService.Return()
			- 21: IPrimitiveDependencyService.Return(out null)
			- 22: IPrimitiveDependencyService.Return<Decimal>()
			- 23: IPrimitiveDependencyService.ReturnAsync()
			- 24: IPrimitiveDependencyService.ReturnWithParameter("ret val")
			- 25: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 26: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 27: IPrimitiveDependencyService.ReturnWithParameterAsync("async value")
			- 28: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 29: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 30: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 31: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			- 32: IPrimitiveDependencyService.ReturnWithSeveralParameters<Int32, Single, Int16>(ref 98, 541)
			- 33: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(100, 101)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidInvocationMultipleParams()
	{
		await CreateFixture()
			.InvokeAllAsync();

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
			- 7: IPrimitiveDependencyService.Invoke<Double>()
			- 8: IPrimitiveDependencyService.InvokeAsync()
			- 9: IPrimitiveDependencyService.InvokeWithParameter(345)
			- 10: IPrimitiveDependencyService.InvokeWithParameter("another value")
			- 11: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 12: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 13: IPrimitiveDependencyService.InvokeWithParameterAsync(98)
			- 14: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 15: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 16: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 17: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 18: IPrimitiveDependencyService.InvokeWithSeveralParameters<Decimal>(83256, 98)
			- 19: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(98, 99)
			- 20: IPrimitiveDependencyService.Return()
			- 21: IPrimitiveDependencyService.Return(out null)
			- 22: IPrimitiveDependencyService.Return<Decimal>()
			- 23: IPrimitiveDependencyService.ReturnAsync()
			- 24: IPrimitiveDependencyService.ReturnWithParameter("ret val")
			- 25: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 26: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 27: IPrimitiveDependencyService.ReturnWithParameterAsync("async value")
			- 28: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 29: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 30: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 31: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			- 32: IPrimitiveDependencyService.ReturnWithSeveralParameters<Int32, Single, Int16>(ref 98, 541)
			- 33: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(100, 101)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidSequence()
	{
		await CreateFixture()
			.InvokeAllAsync();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyServiceMock.InvokeWithSeveralParameters(ItRef<int>.Any(), 2);
			ctx.DependencyServiceMock.Invoke();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.Invoke() to be invoked at index 16, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.HandlerEvent.add
			- 2: IPrimitiveDependencyService.GetOnly.get
			- 3: IPrimitiveDependencyService.SetOnly.set = 123
			- 4: IPrimitiveDependencyService.GetInit.set = "value"
			- 5: IPrimitiveDependencyService.Invoke()
			- 6: IPrimitiveDependencyService.Invoke(out 0)
			- 7: IPrimitiveDependencyService.Invoke<Double>()
			- 8: IPrimitiveDependencyService.InvokeAsync()
			- 9: IPrimitiveDependencyService.InvokeWithParameter(345)
			- 10: IPrimitiveDependencyService.InvokeWithParameter("another value")
			- 11: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 12: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 13: IPrimitiveDependencyService.InvokeWithParameterAsync(98)
			- 14: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 15: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 16: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 17: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 18: IPrimitiveDependencyService.InvokeWithSeveralParameters<Decimal>(83256, 98)
			- 19: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(98, 99)
			- 20: IPrimitiveDependencyService.Return()
			- 21: IPrimitiveDependencyService.Return(out null)
			- 22: IPrimitiveDependencyService.Return<Decimal>()
			- 23: IPrimitiveDependencyService.ReturnAsync()
			- 24: IPrimitiveDependencyService.ReturnWithParameter("ret val")
			- 25: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 26: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 27: IPrimitiveDependencyService.ReturnWithParameterAsync("async value")
			- 28: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 29: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 30: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 31: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			- 32: IPrimitiveDependencyService.ReturnWithSeveralParameters<Int32, Single, Int16>(ref 98, 541)
			- 33: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(100, 101)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidSequenceOneParam()
	{
		await CreateFixture()
			.InvokeAllAsync();

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
			- 7: IPrimitiveDependencyService.Invoke<Double>()
			- 8: IPrimitiveDependencyService.InvokeAsync()
			- 9: IPrimitiveDependencyService.InvokeWithParameter(345)
			- 10: IPrimitiveDependencyService.InvokeWithParameter("another value")
			- 11: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 12: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 13: IPrimitiveDependencyService.InvokeWithParameterAsync(98)
			- 14: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 15: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 16: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 17: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 18: IPrimitiveDependencyService.InvokeWithSeveralParameters<Decimal>(83256, 98)
			- 19: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(98, 99)
			- 20: IPrimitiveDependencyService.Return()
			- 21: IPrimitiveDependencyService.Return(out null)
			- 22: IPrimitiveDependencyService.Return<Decimal>()
			- 23: IPrimitiveDependencyService.ReturnAsync()
			- 24: IPrimitiveDependencyService.ReturnWithParameter("ret val")
			- 25: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 26: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 27: IPrimitiveDependencyService.ReturnWithParameterAsync("async value")
			- 28: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 29: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 30: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 31: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			- 32: IPrimitiveDependencyService.ReturnWithSeveralParameters<Int32, Single, Int16>(ref 98, 541)
			- 33: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(100, 101)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidSequenceMultipleParams()
	{
		await CreateFixture()
			.InvokeAllAsync();

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
			- 7: IPrimitiveDependencyService.Invoke<Double>()
			- 8: IPrimitiveDependencyService.InvokeAsync()
			- 9: IPrimitiveDependencyService.InvokeWithParameter(345)
			- 10: IPrimitiveDependencyService.InvokeWithParameter("another value")
			- 11: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 12: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 13: IPrimitiveDependencyService.InvokeWithParameterAsync(98)
			- 14: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 15: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 16: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 17: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 18: IPrimitiveDependencyService.InvokeWithSeveralParameters<Decimal>(83256, 98)
			- 19: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(98, 99)
			- 20: IPrimitiveDependencyService.Return()
			- 21: IPrimitiveDependencyService.Return(out null)
			- 22: IPrimitiveDependencyService.Return<Decimal>()
			- 23: IPrimitiveDependencyService.ReturnAsync()
			- 24: IPrimitiveDependencyService.ReturnWithParameter("ret val")
			- 25: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 26: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 27: IPrimitiveDependencyService.ReturnWithParameterAsync("async value")
			- 28: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 29: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 30: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 31: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			- 32: IPrimitiveDependencyService.ReturnWithSeveralParameters<Int32, Single, Int16>(ref 98, 541)
			- 33: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(100, 101)
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
	public static async Task InvokeAllAsync(this IPrimitiveTypeService fixture)
	{
		var value = 1234m;
		var valueInt = 98;
		var valueDouble = 3488d;

		fixture.HandlerEvent += (_, _) => { };
		_ = fixture.GetOnly;
		_ = fixture.GetOnlyGeneric;
		fixture.SetOnly = 123m;
		fixture.GetInit = "value";
		fixture.Invoke();
		fixture.Invoke(out _);
		fixture.Invoke<double>();
		await fixture.InvokeAsync();
		fixture.InvokeWithParameter(345);
		fixture.InvokeWithParameter("another value");
		fixture.InvokeWithParameter(ref value);
		fixture.InvokeWithParameter<float>(123);
		await fixture.InvokeWithParameterAsync(valueInt);
		fixture.InvokeWithSeveralParameters(1, 2);
		fixture.InvokeWithSeveralParameters(ref valueInt, 2);
		fixture.InvokeWithSeveralParameters(1, ref valueInt);
		fixture.InvokeWithSeveralParameters(ref valueInt, ref valueInt);
		fixture.InvokeWithSeveralParameters(83256m, valueInt);
		await fixture.InvokeWithSeveralParametersAsync(valueInt, valueInt + 1);
		fixture.Return();
		fixture.Return(out _);
		fixture.Return<decimal>();
		await fixture.ReturnAsync();
		fixture.ReturnWithParameter("ret val");
		fixture.ReturnWithParameter(ref valueDouble);
		fixture.ReturnWithParameter<float, string>(123f);
		await fixture.ReturnWithParameterAsync("async value");
		fixture.ReturnWithSeveralParameters(1, 2);
		fixture.ReturnWithSeveralParameters(ref valueInt, 2);
		fixture.ReturnWithSeveralParameters(1, ref valueInt);
		fixture.ReturnWithSeveralParameters(ref valueInt, ref valueInt);
		fixture.ReturnWithSeveralParameters<int, float, short>(ref valueInt, 541f);
		await fixture.ReturnWithSeveralParametersAsync(valueInt + 2, valueInt + 3);
	}
}
