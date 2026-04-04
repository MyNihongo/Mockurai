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
			- 1: IPrimitiveDependencyService.HandlerEvent.add += System.EventHandler`1[System.String]
			- 2: IPrimitiveDependencyService.GetOnly.get
			- 4: IPrimitiveDependencyService.SetOnly.set = 123
			- 5: IPrimitiveDependencyService.GetInit.set = "value"
			- 7: IPrimitiveDependencyService.Invoke()
			- 8: IPrimitiveDependencyService.Invoke(out 0)
			- 9: IPrimitiveDependencyService.Invoke<Double>()
			- 10: IPrimitiveDependencyService.InvokeAsync()
			- 11: IPrimitiveDependencyService.InvokeWithParameter(in 345)
			- 12: IPrimitiveDependencyService.InvokeWithParameter(in "another value")
			- 13: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 14: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 15: IPrimitiveDependencyService.InvokeWithParameterAsync(98)
			- 17: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 18: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 19: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 20: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 21: IPrimitiveDependencyService.InvokeWithSeveralParameters<Decimal>(83256, 98)
			- 22: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(98, 99)
			- 24: IPrimitiveDependencyService.Return()
			- 25: IPrimitiveDependencyService.Return(out null)
			- 26: IPrimitiveDependencyService.Return<Decimal>()
			- 27: IPrimitiveDependencyService.ReturnAsync()
			- 29: IPrimitiveDependencyService.ReturnWithParameter(in "ret val")
			- 30: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 31: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 32: IPrimitiveDependencyService.ReturnWithParameterAsync("async value")
			- 34: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 35: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 36: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 37: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			- 38: IPrimitiveDependencyService.ReturnWithSeveralParameters<Int32, Single, Int16>(ref 98, 541)
			- 39: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(100, 101)
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
			.VerifyInvokeWithParameter(ItIn<int>.Value(8374646), Times.Once);

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(in 8374646) to be called 1 time, but instead it was called 0 times.
			Performed invocations:
			- 1: IPrimitiveDependencyService.HandlerEvent.add += System.EventHandler`1[System.String]
			- 2: IPrimitiveDependencyService.GetOnly.get
			- 4: IPrimitiveDependencyService.SetOnly.set = 123
			- 5: IPrimitiveDependencyService.GetInit.set = "value"
			- 7: IPrimitiveDependencyService.Invoke()
			- 8: IPrimitiveDependencyService.Invoke(out 0)
			- 9: IPrimitiveDependencyService.Invoke<Double>()
			- 10: IPrimitiveDependencyService.InvokeAsync()
			- 11: IPrimitiveDependencyService.InvokeWithParameter(in 345)
			- 12: IPrimitiveDependencyService.InvokeWithParameter(in "another value")
			- 13: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 14: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 15: IPrimitiveDependencyService.InvokeWithParameterAsync(98)
			- 17: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 18: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 19: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 20: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 21: IPrimitiveDependencyService.InvokeWithSeveralParameters<Decimal>(83256, 98)
			- 22: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(98, 99)
			- 24: IPrimitiveDependencyService.Return()
			- 25: IPrimitiveDependencyService.Return(out null)
			- 26: IPrimitiveDependencyService.Return<Decimal>()
			- 27: IPrimitiveDependencyService.ReturnAsync()
			- 29: IPrimitiveDependencyService.ReturnWithParameter(in "ret val")
			- 30: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 31: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 32: IPrimitiveDependencyService.ReturnWithParameterAsync("async value")
			- 34: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 35: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 36: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 37: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			- 38: IPrimitiveDependencyService.ReturnWithSeveralParameters<Int32, Single, Int16>(ref 98, 541)
			- 39: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(100, 101)
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
			- 1: IPrimitiveDependencyService.HandlerEvent.add += System.EventHandler`1[System.String]
			- 2: IPrimitiveDependencyService.GetOnly.get
			- 4: IPrimitiveDependencyService.SetOnly.set = 123
			- 5: IPrimitiveDependencyService.GetInit.set = "value"
			- 7: IPrimitiveDependencyService.Invoke()
			- 8: IPrimitiveDependencyService.Invoke(out 0)
			- 9: IPrimitiveDependencyService.Invoke<Double>()
			- 10: IPrimitiveDependencyService.InvokeAsync()
			- 11: IPrimitiveDependencyService.InvokeWithParameter(in 345)
			- 12: IPrimitiveDependencyService.InvokeWithParameter(in "another value")
			- 13: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 14: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 15: IPrimitiveDependencyService.InvokeWithParameterAsync(98)
			- 17: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 18: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 19: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 20: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 21: IPrimitiveDependencyService.InvokeWithSeveralParameters<Decimal>(83256, 98)
			- 22: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(98, 99)
			- 24: IPrimitiveDependencyService.Return()
			- 25: IPrimitiveDependencyService.Return(out null)
			- 26: IPrimitiveDependencyService.Return<Decimal>()
			- 27: IPrimitiveDependencyService.ReturnAsync()
			- 29: IPrimitiveDependencyService.ReturnWithParameter(in "ret val")
			- 30: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 31: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 32: IPrimitiveDependencyService.ReturnWithParameterAsync("async value")
			- 34: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 35: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 36: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 37: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			- 38: IPrimitiveDependencyService.ReturnWithSeveralParameters<Int32, Single, Int16>(ref 98, 541)
			- 39: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(100, 101)
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
			Expected IPrimitiveDependencyService.Invoke() to be invoked at index 19, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.HandlerEvent.add += System.EventHandler`1[System.String]
			- 2: IPrimitiveDependencyService.GetOnly.get
			- 4: IPrimitiveDependencyService.SetOnly.set = 123
			- 5: IPrimitiveDependencyService.GetInit.set = "value"
			- 7: IPrimitiveDependencyService.Invoke()
			- 8: IPrimitiveDependencyService.Invoke(out 0)
			- 9: IPrimitiveDependencyService.Invoke<Double>()
			- 10: IPrimitiveDependencyService.InvokeAsync()
			- 11: IPrimitiveDependencyService.InvokeWithParameter(in 345)
			- 12: IPrimitiveDependencyService.InvokeWithParameter(in "another value")
			- 13: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 14: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 15: IPrimitiveDependencyService.InvokeWithParameterAsync(98)
			- 17: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 18: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 19: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 20: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 21: IPrimitiveDependencyService.InvokeWithSeveralParameters<Decimal>(83256, 98)
			- 22: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(98, 99)
			- 24: IPrimitiveDependencyService.Return()
			- 25: IPrimitiveDependencyService.Return(out null)
			- 26: IPrimitiveDependencyService.Return<Decimal>()
			- 27: IPrimitiveDependencyService.ReturnAsync()
			- 29: IPrimitiveDependencyService.ReturnWithParameter(in "ret val")
			- 30: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 31: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 32: IPrimitiveDependencyService.ReturnWithParameterAsync("async value")
			- 34: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 35: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 36: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 37: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			- 38: IPrimitiveDependencyService.ReturnWithSeveralParameters<Int32, Single, Int16>(ref 98, 541)
			- 39: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(100, 101)
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
			ctx.DependencyServiceMock.InvokeWithParameter(ItIn<int>.Value(8374646));
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.InvokeWithParameter(in 8374646) to be invoked at index 8, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.HandlerEvent.add += System.EventHandler`1[System.String]
			- 2: IPrimitiveDependencyService.GetOnly.get
			- 4: IPrimitiveDependencyService.SetOnly.set = 123
			- 5: IPrimitiveDependencyService.GetInit.set = "value"
			- 7: IPrimitiveDependencyService.Invoke()
			- 8: IPrimitiveDependencyService.Invoke(out 0)
			- 9: IPrimitiveDependencyService.Invoke<Double>()
			- 10: IPrimitiveDependencyService.InvokeAsync()
			- 11: IPrimitiveDependencyService.InvokeWithParameter(in 345)
			- 12: IPrimitiveDependencyService.InvokeWithParameter(in "another value")
			- 13: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 14: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 15: IPrimitiveDependencyService.InvokeWithParameterAsync(98)
			- 17: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 18: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 19: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 20: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 21: IPrimitiveDependencyService.InvokeWithSeveralParameters<Decimal>(83256, 98)
			- 22: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(98, 99)
			- 24: IPrimitiveDependencyService.Return()
			- 25: IPrimitiveDependencyService.Return(out null)
			- 26: IPrimitiveDependencyService.Return<Decimal>()
			- 27: IPrimitiveDependencyService.ReturnAsync()
			- 29: IPrimitiveDependencyService.ReturnWithParameter(in "ret val")
			- 30: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 31: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 32: IPrimitiveDependencyService.ReturnWithParameterAsync("async value")
			- 34: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 35: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 36: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 37: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			- 38: IPrimitiveDependencyService.ReturnWithSeveralParameters<Int32, Single, Int16>(ref 98, 541)
			- 39: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(100, 101)
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
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(8374646, 2843253) to be invoked at index 8, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.HandlerEvent.add += System.EventHandler`1[System.String]
			- 2: IPrimitiveDependencyService.GetOnly.get
			- 4: IPrimitiveDependencyService.SetOnly.set = 123
			- 5: IPrimitiveDependencyService.GetInit.set = "value"
			- 7: IPrimitiveDependencyService.Invoke()
			- 8: IPrimitiveDependencyService.Invoke(out 0)
			- 9: IPrimitiveDependencyService.Invoke<Double>()
			- 10: IPrimitiveDependencyService.InvokeAsync()
			- 11: IPrimitiveDependencyService.InvokeWithParameter(in 345)
			- 12: IPrimitiveDependencyService.InvokeWithParameter(in "another value")
			- 13: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 14: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 15: IPrimitiveDependencyService.InvokeWithParameterAsync(98)
			- 17: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 18: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 19: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 20: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 21: IPrimitiveDependencyService.InvokeWithSeveralParameters<Decimal>(83256, 98)
			- 22: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(98, 99)
			- 24: IPrimitiveDependencyService.Return()
			- 25: IPrimitiveDependencyService.Return(out null)
			- 26: IPrimitiveDependencyService.Return<Decimal>()
			- 27: IPrimitiveDependencyService.ReturnAsync()
			- 29: IPrimitiveDependencyService.ReturnWithParameter(in "ret val")
			- 30: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 31: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 32: IPrimitiveDependencyService.ReturnWithParameterAsync("async value")
			- 34: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 35: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 36: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 37: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			- 38: IPrimitiveDependencyService.ReturnWithSeveralParameters<Int32, Single, Int16>(ref 98, 541)
			- 39: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(100, 101)
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
			- 2: IPrimitiveDependencyService.InvokeWithParameter(in "param")
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
			Expected IPrimitiveDependencyService.InvokeWithParameter(in String) to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.InvokeWithParameter(in "param")
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
			Expected IPrimitiveDependencyService.InvokeWithSeveralParameters(int, int) to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService.InvokeWithSeveralParameters(123, 321)
			- 2: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 43321, 321)
			- 3: IPrimitiveDependencyService.ReturnWithSeveralParameters(123, ref 43321)
			- 4: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 43321, ref 43321)
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidInvocationGeneric()
	{
		await CreateFixture()
			.InvokeAllAsync();

		var actual = () => DependencyGenericServiceMock
			.VerifyInvokeWithParameter(It<string>.Any(), Times.Exactly(100));

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.InvokeWithParameter(any) to be called 100 times, but instead it was called 1 time.
			Performed invocations:
			- 3: IPrimitiveDependencyService<String>.GetOnly.get
			- 6: IPrimitiveDependencyService<String>.GetSet.set = "new value"
			- 16: IPrimitiveDependencyService<String>.InvokeWithParameter("generic value")
			- 23: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<Single>(123, "another value")
			- 28: IPrimitiveDependencyService<String>.Return<Int16>()
			- 33: IPrimitiveDependencyService<String>.ReturnWithParameter<Decimal>("generic value")
			- 40: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Decimal, Int32>(123, 321)
			""";
		var exception = Assert.Throws<MockVerifyCountException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidSequenceGeneric()
	{
		await CreateFixture()
			.InvokeAllAsync();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.SetGetSet("new value");
			ctx.DependencyServiceMock.Invoke();
			ctx.DependencyGenericServiceMock.InvokeWithParameter(It<string>.Any());
			ctx.DependencyGenericServiceMock.GetGetOnly();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.GetOnly.get to be invoked at index 17, but it has not been called.
			Performed invocations:
			- 3: IPrimitiveDependencyService<String>.GetOnly.get
			- 6: IPrimitiveDependencyService<String>.GetSet.set = "new value"
			- 16: IPrimitiveDependencyService<String>.InvokeWithParameter("generic value")
			- 23: IPrimitiveDependencyService<String>.InvokeWithSeveralParameters<Single>(123, "another value")
			- 28: IPrimitiveDependencyService<String>.Return<Int16>()
			- 33: IPrimitiveDependencyService<String>.ReturnWithParameter<Decimal>("generic value")
			- 40: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Decimal, Int32>(123, 321)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public void ThrowVerifyNoOtherCallsGeneric()
	{
		var fixture = CreateFixture();
		fixture.InvokeWithParameterGeneric("some value");
		fixture.ReturnGeneric<decimal>();
		fixture.GetSetGeneric = "new value";
		fixture.ReturnWithSeveralParametersGeneric(12, "fix");

		var actual = VerifyNoOtherCalls;

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService<String>.GetSet.set = String to be verified, but the following invocations have not been verified:
			- 1: IPrimitiveDependencyService<String>.InvokeWithParameter("some value")
			- 2: IPrimitiveDependencyService<String>.Return<Decimal>()
			- 3: IPrimitiveDependencyService<String>.GetSet.set = "new value"
			- 4: IPrimitiveDependencyService<String>.ReturnWithSeveralParameters<Int32, String>(12, "fix")
			""";
		var exception = Assert.Throws<MockUnverifiedException>(actual);
		Assert.Equal(expectedMessage, exception.Message);
	}

	[Fact]
	public async Task ThrowInvalidSequenceMultipleDependencies()
	{
		await CreateFixture()
			.InvokeAllAsync();

		var actual = () => VerifyInSequence(static ctx =>
		{
			ctx.DependencyGenericServiceMock.SetGetSet("new value");
			ctx.DependencyServiceMock.GetGetOnly();
		});

		const string expectedMessage =
			"""
			Expected IPrimitiveDependencyService.GetOnly.get to be invoked at index 7, but it has not been called.
			Performed invocations:
			- 1: IPrimitiveDependencyService.HandlerEvent.add += System.EventHandler`1[System.String]
			- 2: IPrimitiveDependencyService.GetOnly.get
			- 4: IPrimitiveDependencyService.SetOnly.set = 123
			- 5: IPrimitiveDependencyService.GetInit.set = "value"
			- 7: IPrimitiveDependencyService.Invoke()
			- 8: IPrimitiveDependencyService.Invoke(out 0)
			- 9: IPrimitiveDependencyService.Invoke<Double>()
			- 10: IPrimitiveDependencyService.InvokeAsync()
			- 11: IPrimitiveDependencyService.InvokeWithParameter(in 345)
			- 12: IPrimitiveDependencyService.InvokeWithParameter(in "another value")
			- 13: IPrimitiveDependencyService.InvokeWithParameter(ref 1234)
			- 14: IPrimitiveDependencyService.InvokeWithParameter<Single>(123)
			- 15: IPrimitiveDependencyService.InvokeWithParameterAsync(98)
			- 17: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, 2)
			- 18: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, 2)
			- 19: IPrimitiveDependencyService.InvokeWithSeveralParameters(1, ref 98)
			- 20: IPrimitiveDependencyService.InvokeWithSeveralParameters(ref 98, ref 98)
			- 21: IPrimitiveDependencyService.InvokeWithSeveralParameters<Decimal>(83256, 98)
			- 22: IPrimitiveDependencyService.InvokeWithSeveralParametersAsync(98, 99)
			- 24: IPrimitiveDependencyService.Return()
			- 25: IPrimitiveDependencyService.Return(out null)
			- 26: IPrimitiveDependencyService.Return<Decimal>()
			- 27: IPrimitiveDependencyService.ReturnAsync()
			- 29: IPrimitiveDependencyService.ReturnWithParameter(in "ret val")
			- 30: IPrimitiveDependencyService.ReturnWithParameter(ref 3488)
			- 31: IPrimitiveDependencyService.ReturnWithParameter<Single, String>(123)
			- 32: IPrimitiveDependencyService.ReturnWithParameterAsync("async value")
			- 34: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, 2)
			- 35: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, 2)
			- 36: IPrimitiveDependencyService.ReturnWithSeveralParameters(1, ref 98)
			- 37: IPrimitiveDependencyService.ReturnWithSeveralParameters(ref 98, ref 98)
			- 38: IPrimitiveDependencyService.ReturnWithSeveralParameters<Int32, Single, Int16>(ref 98, 541)
			- 39: IPrimitiveDependencyService.ReturnWithSeveralParametersAsync(100, 101)
			""";
		var exception = Assert.Throws<MockVerifySequenceOutOfRangeException>(actual);
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
		fixture.GetSetGeneric = "new value";
		fixture.Invoke();
		fixture.Invoke(out _);
		fixture.Invoke<double>();
		await fixture.InvokeAsync();
		fixture.InvokeWithParameter(345);
		fixture.InvokeWithParameter("another value");
		fixture.InvokeWithParameter(ref value);
		fixture.InvokeWithParameter<float>(123);
		await fixture.InvokeWithParameterAsync(valueInt);
		fixture.InvokeWithParameterGeneric("generic value");
		fixture.InvokeWithSeveralParameters(1, 2);
		fixture.InvokeWithSeveralParameters(ref valueInt, 2);
		fixture.InvokeWithSeveralParameters(1, ref valueInt);
		fixture.InvokeWithSeveralParameters(ref valueInt, ref valueInt);
		fixture.InvokeWithSeveralParameters(83256m, valueInt);
		await fixture.InvokeWithSeveralParametersAsync(valueInt, valueInt + 1);
		fixture.InvokeWithSeveralParametersGeneric(123f, "another value");
		fixture.Return();
		fixture.Return(out _);
		fixture.Return<decimal>();
		await fixture.ReturnAsync();
		fixture.ReturnGeneric<short>();
		fixture.ReturnWithParameter("ret val");
		fixture.ReturnWithParameter(ref valueDouble);
		fixture.ReturnWithParameter<float, string>(123f);
		await fixture.ReturnWithParameterAsync("async value");
		fixture.ReturnWithParameterGeneric<decimal>("generic value");
		fixture.ReturnWithSeveralParameters(1, 2);
		fixture.ReturnWithSeveralParameters(ref valueInt, 2);
		fixture.ReturnWithSeveralParameters(1, ref valueInt);
		fixture.ReturnWithSeveralParameters(ref valueInt, ref valueInt);
		fixture.ReturnWithSeveralParameters<int, float, short>(ref valueInt, 541f);
		await fixture.ReturnWithSeveralParametersAsync(valueInt + 2, valueInt + 3);
		fixture.ReturnWithSeveralParametersGeneric(123m, 321);
	}
}
