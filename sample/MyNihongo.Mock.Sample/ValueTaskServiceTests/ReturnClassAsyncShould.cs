// // ReSharper disable AccessToDisposedClosure
//
// namespace MyNihongo.Mock.Sample.ValueTaskServiceTests;
//
// public sealed class ReturnClassAsyncShould : ValueTaskServiceTestsBase
// {
// 	[Fact]
// 	public async Task ThrowWithoutSetup()
// 	{
// 		using var cts = new CancellationTokenSource();
//
// 		Func<Task> actual = async () => await CreateFixture()
// 			.ReturnClassAsync(cts.Token);
//
// 		var exception = await Assert.ThrowsAsync<NullReferenceException>(actual);
// 		Assert.Equal("IValueTaskDependencyService#ReturnClassAsync() method has not been set up", exception.Message);
// 	}
//
// 	[Fact]
// 	public async Task ReturnValueWithSetup()
// 	{
// 		using var cts = new CancellationTokenSource();
//
// 		ValueTaskDependencyServiceMock
// 			.SetupReturnClassAsync(cts.Token)
// 			.Returns(new ClassReturn
// 			{
// 				Age = 12,
// 				Name = "Okayama Issei",
// 				DateOfBirth = new DateOnly(2025, 6, 18),
// 			});
//
// 		var actual = await CreateFixture()
// 			.ReturnClassAsync(cts.Token);
//
// 		const string expected = "Okayama Issei is 12 years old, born in 2025";
// 		Assert.Equal(expected, actual);
// 	}
//
// 	[Fact]
// 	public async Task ThrowWithSetupAnotherInstance()
// 	{
// 		using var ctsSetup = new CancellationTokenSource(TimeSpan.FromSeconds(1d));
// 		using var ctsInput = new CancellationTokenSource();
//
// 		ValueTaskDependencyServiceMock
// 			.SetupReturnClassAsync(ctsSetup.Token)
// 			.Returns(new ClassReturn
// 			{
// 				Age = 12,
// 				Name = "Okayama Issei",
// 				DateOfBirth = new DateOnly(2025, 6, 18),
// 			});
//
// 		Func<Task> actual = async () => await CreateFixture()
// 			.ReturnClassAsync(ctsInput.Token);
//
// 		var exception = await Assert.ThrowsAsync<NullReferenceException>(actual);
// 		Assert.Equal("IValueTaskDependencyService#ReturnClassAsync() method has not been set up", exception.Message);
// 	}
//
// 	[Fact]
// 	public async Task ThrowWithSetup()
// 	{
// 		const string errorMessage = nameof(errorMessage);
// 		using var cts = new CancellationTokenSource();
//
// 		ValueTaskDependencyServiceMock
// 			.SetupReturnClassAsync(cts.Token)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Func<Task> actual = async () => await CreateFixture()
// 			.ReturnClassAsync(cts.Token);
//
// 		var exception = await Assert.ThrowsAsync<InvalidOperationException>(actual);
// 		Assert.Equal(errorMessage, exception.Message);
// 	}
//
// 	[Fact]
// 	public async Task ThrowForThrowsWithSetupAnotherInstance()
// 	{
// 		const string errorMessage = nameof(errorMessage);
// 		using var ctsSetup = new CancellationTokenSource(TimeSpan.FromSeconds(1d));
// 		using var ctsInput = new CancellationTokenSource();
//
// 		ValueTaskDependencyServiceMock
// 			.SetupReturnClassAsync(ctsSetup.Token)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Func<Task> actual = async () => await CreateFixture()
// 			.ReturnClassAsync(ctsInput.Token);
//
// 		var exception = await Assert.ThrowsAsync<NullReferenceException>(actual);
// 		Assert.Equal("IValueTaskDependencyService#ReturnClassAsync() method has not been set up", exception.Message);
// 	}
// }
