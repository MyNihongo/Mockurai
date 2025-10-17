// // ReSharper disable AccessToDisposedClosure
//
// namespace MyNihongo.Mock.Sample.TaskServiceTests;
//
// public sealed class ReturnRecordAsyncShould : TaskServiceTestsBase
// {
// 	[Fact]
// 	public async Task ThrowWithoutSetup()
// 	{
// 		using var cts = new CancellationTokenSource();
//
// 		Func<Task> actual = () => CreateFixture()
// 			.ReturnRecordAsync(cts.Token);
//
// 		var exception = await Assert.ThrowsAsync<NullReferenceException>(actual);
// 		Assert.Equal("ITaskDependencyService.ReturnRecordAsync() method has not been set up", exception.Message);
// 	}
//
// 	[Fact]
// 	public async Task ReturnValueWithSetup()
// 	{
// 		using var cts = new CancellationTokenSource();
//
// 		TaskDependencyServiceMock
// 			.SetupReturnRecordAsync(cts.Token)
// 			.Returns(new RecordReturn(
// 				Age: 12,
// 				Name: "Okayama Issei",
// 				DateOfBirth: new DateOnly(2025, 6, 18)
// 			));
//
// 		var actual = await CreateFixture()
// 			.ReturnRecordAsync(cts.Token);
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
// 		TaskDependencyServiceMock
// 			.SetupReturnRecordAsync(ctsSetup.Token)
// 			.Returns(new RecordReturn(
// 				Age: 12,
// 				Name: "Okayama Issei",
// 				DateOfBirth: new DateOnly(2025, 6, 18)
// 			));
//
// 		Func<Task> actual = () => CreateFixture()
// 			.ReturnRecordAsync(ctsInput.Token);
//
// 		var exception = await Assert.ThrowsAsync<NullReferenceException>(actual);
// 		Assert.Equal("ITaskDependencyService.ReturnRecordAsync() method has not been set up", exception.Message);
// 	}
//
// 	[Fact]
// 	public async Task ThrowWithSetup()
// 	{
// 		const string errorMessage = nameof(errorMessage);
// 		using var cts = new CancellationTokenSource();
//
// 		TaskDependencyServiceMock
// 			.SetupReturnRecordAsync(cts.Token)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Func<Task> actual = () => CreateFixture()
// 			.ReturnRecordAsync(cts.Token);
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
// 		TaskDependencyServiceMock
// 			.SetupReturnRecordAsync(ctsSetup.Token)
// 			.Throws(new InvalidOperationException(errorMessage));
//
// 		Func<Task> actual = () => CreateFixture()
// 			.ReturnRecordAsync(ctsInput.Token);
//
// 		var exception = await Assert.ThrowsAsync<NullReferenceException>(actual);
// 		Assert.Equal("ITaskDependencyService.ReturnRecordAsync() method has not been set up", exception.Message);
// 	}
// }
