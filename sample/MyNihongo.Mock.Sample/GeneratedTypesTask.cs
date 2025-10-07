// namespace MyNihongo.Mock.Sample;
//
// [Obsolete("Will be generated")]
// public sealed class TaskDependencyServiceMock : IMock<ITaskDependencyService>
// {
// 	private Proxy? _proxy;
// 	private SetupWithParameter? _invokeAsync;
// 	private SetupWithParameter<int>? _returnPrimitiveAsync;
// 	private SetupWithParameter<short?>? _returnPrimitiveNullableAsync;
// 	private SetupWithParameter<StructReturn>? _returnStructAsync;
// 	private SetupWithParameter<StructReturn?>? _returnStructNullableAsync;
// 	private SetupWithParameter<ClassReturn>? _returnClassAsync;
// 	private SetupWithParameter<ClassReturn?>? _returnClassNullableAsync;
// 	private SetupWithParameter<RecordReturn>? _returnRecordAsync;
// 	private SetupWithParameter<RecordReturn?>? _returnRecordNullableAsync;
//
// 	public SetupWithParameter SetupInvokeAsync(in It<CancellationToken> ct)
// 	{
// 		_invokeAsync ??= new SetupWithParameter();
//
// 		var hashCode = ct.GetHashCode();
// 		_invokeAsync.SetupParameters(hashCode);
// 		return _invokeAsync;
// 	}
//
// 	public SetupWithParameter<int> SetupReturnPrimitiveAsync(in It<CancellationToken> ct)
// 	{
// 		_returnPrimitiveAsync ??= new SetupWithParameter<int>();
//
// 		var hashCode = ct.GetHashCode();
// 		_returnPrimitiveAsync.SetupParameters(hashCode);
// 		return _returnPrimitiveAsync;
// 	}
//
// 	public SetupWithParameter<short?> SetupReturnPrimitiveNullableAsync(in It<CancellationToken> ct)
// 	{
// 		_returnPrimitiveNullableAsync ??= new SetupWithParameter<short?>();
//
// 		var hashCode = ct.GetHashCode();
// 		_returnPrimitiveNullableAsync.SetupParameters(hashCode);
// 		return _returnPrimitiveNullableAsync;
// 	}
//
// 	public SetupWithParameter<StructReturn> SetupReturnStructAsync(in It<CancellationToken> ct)
// 	{
// 		_returnStructAsync ??= new SetupWithParameter<StructReturn>();
//
// 		var hashCode = ct.GetHashCode();
// 		_returnStructAsync.SetupParameters(hashCode);
// 		return _returnStructAsync;
// 	}
//
// 	public SetupWithParameter<StructReturn?> SetupReturnStructNullableAsync(in It<CancellationToken> ct)
// 	{
// 		_returnStructNullableAsync ??= new SetupWithParameter<StructReturn?>();
//
// 		var hashCode = ct.GetHashCode();
// 		_returnStructNullableAsync.SetupParameters(hashCode);
// 		return _returnStructNullableAsync;
// 	}
//
// 	public SetupWithParameter<ClassReturn> SetupReturnClassAsync(in It<CancellationToken> ct)
// 	{
// 		_returnClassAsync ??= new SetupWithParameter<ClassReturn>();
//
// 		var hashCode = ct.GetHashCode();
// 		_returnClassAsync.SetupParameters(hashCode);
// 		return _returnClassAsync;
// 	}
//
// 	public SetupWithParameter<ClassReturn?> SetupReturnClassNullableAsync(in It<CancellationToken> ct)
// 	{
// 		_returnClassNullableAsync ??= new SetupWithParameter<ClassReturn?>();
//
// 		var hashCode = ct.GetHashCode();
// 		_returnClassNullableAsync.SetupParameters(hashCode);
// 		return _returnClassNullableAsync;
// 	}
//
// 	public SetupWithParameter<RecordReturn> SetupReturnRecordAsync(in It<CancellationToken> ct)
// 	{
// 		_returnRecordAsync ??= new SetupWithParameter<RecordReturn>();
//
// 		var hashCode = ct.GetHashCode();
// 		_returnRecordAsync.SetupParameters(hashCode);
// 		return _returnRecordAsync;
// 	}
//
// 	public SetupWithParameter<RecordReturn?> SetupReturnRecordNullableAsync(in It<CancellationToken> ct)
// 	{
// 		_returnRecordNullableAsync ??= new SetupWithParameter<RecordReturn?>();
//
// 		var hashCode = ct.GetHashCode();
// 		_returnRecordNullableAsync.SetupParameters(hashCode);
// 		return _returnRecordNullableAsync;
// 	}
//
// 	public ITaskDependencyService Object => _proxy ??= new Proxy(this);
//
// 	private sealed class Proxy : ITaskDependencyService
// 	{
// 		private readonly TaskDependencyServiceMock _mock;
//
// 		public Proxy(in TaskDependencyServiceMock mock)
// 		{
// 			_mock = mock;
// 		}
//
// 		public Task InvokeAsync(CancellationToken ct = default)
// 		{
// 			var hashCode = ct.GetHashCode();
// 			_mock._invokeAsync?.Invoke(hashCode);
// 			return Task.CompletedTask;
// 		}
//
// 		public Task<int> ReturnPrimitiveAsync(CancellationToken ct = default)
// 		{
// 			var hashCode = ct.GetHashCode();
// 			return _mock._returnPrimitiveAsync?.TryInvoke(hashCode, out var returnValue) == true ? Task.FromResult(returnValue) : throw new NullReferenceException("ITaskDependencyService.ReturnPrimitiveAsync() method has not been set up");
// 		}
//
// 		public Task<short?> ReturnPrimitiveNullableAsync(CancellationToken ct = default)
// 		{
// 			var hashCode = ct.GetHashCode();
// 			return _mock._returnPrimitiveNullableAsync?.TryInvoke(hashCode, out var returnValue) == true ? Task.FromResult(returnValue) : Task.FromResult<short?>(null);
// 		}
//
// 		public Task<StructReturn> ReturnStructAsync(CancellationToken ct = default)
// 		{
// 			var hashCode = ct.GetHashCode();
// 			return _mock._returnStructAsync?.TryInvoke(hashCode, out var returnValue) == true ? Task.FromResult(returnValue) : throw new NullReferenceException("ITaskDependencyService.ReturnStructAsync() method has not been set up");
// 		}
//
// 		public Task<StructReturn?> ReturnStructNullableAsync(CancellationToken ct = default)
// 		{
// 			var hashCode = ct.GetHashCode();
// 			return _mock._returnStructNullableAsync?.TryInvoke(hashCode, out var returnValue) == true ? Task.FromResult(returnValue) : Task.FromResult<StructReturn?>(null);
// 		}
//
// 		public Task<ClassReturn> ReturnClassAsync(CancellationToken ct = default)
// 		{
// 			var hashCode = ct.GetHashCode();
// 			return _mock._returnClassAsync?.TryInvoke(hashCode, out var returnValue) == true ? Task.FromResult(returnValue) : throw new NullReferenceException("ITaskDependencyService.ReturnClassAsync() method has not been set up");
// 		}
//
// 		public Task<ClassReturn?> ReturnClassNullableAsync(CancellationToken ct = default)
// 		{
// 			var hashCode = ct.GetHashCode();
// 			return _mock._returnClassNullableAsync?.TryInvoke(hashCode, out var returnValue) == true ? Task.FromResult(returnValue) : Task.FromResult<ClassReturn?>(null);
// 		}
//
// 		public Task<RecordReturn> ReturnRecordAsync(CancellationToken ct = default)
// 		{
// 			var hashCode = ct.GetHashCode();
// 			return _mock._returnRecordAsync?.TryInvoke(hashCode, out var returnValue) == true ? Task.FromResult(returnValue) : throw new NullReferenceException("ITaskDependencyService.ReturnRecordAsync() method has not been set up");
// 		}
//
// 		public Task<RecordReturn?> ReturnRecordNullableAsync(CancellationToken ct = default)
// 		{
// 			var hashCode = ct.GetHashCode();
// 			return _mock._returnRecordNullableAsync?.TryInvoke(hashCode, out var returnValue) == true ? Task.FromResult(returnValue) : Task.FromResult<RecordReturn?>(null);
// 		}
// 	}
// }
//
// [Obsolete("Will be generated")]
// public static class TaskDependencyServiceMockEx
// {
// 	public static ISetup SetupInvokeAsync(this IMock<ITaskDependencyService> @this, in It<CancellationToken> ct) =>
// 		((TaskDependencyServiceMock)@this).SetupInvokeAsync(ct);
//
// 	public static ISetup<int> SetupReturnPrimitiveAsync(this IMock<ITaskDependencyService> @this, in It<CancellationToken> ct) =>
// 		((TaskDependencyServiceMock)@this).SetupReturnPrimitiveAsync(ct);
//
// 	public static ISetup<short?> SetupReturnPrimitiveNullableAsync(this IMock<ITaskDependencyService> @this, in It<CancellationToken> ct) =>
// 		((TaskDependencyServiceMock)@this).SetupReturnPrimitiveNullableAsync(ct);
//
// 	public static ISetup<StructReturn> SetupReturnStructAsync(this IMock<ITaskDependencyService> @this, in It<CancellationToken> ct) =>
// 		((TaskDependencyServiceMock)@this).SetupReturnStructAsync(ct);
//
// 	public static ISetup<StructReturn?> SetupReturnStructNullableAsync(this IMock<ITaskDependencyService> @this, in It<CancellationToken> ct) =>
// 		((TaskDependencyServiceMock)@this).SetupReturnStructNullableAsync(ct);
//
// 	public static ISetup<ClassReturn> SetupReturnClassAsync(this IMock<ITaskDependencyService> @this, in It<CancellationToken> ct) =>
// 		((TaskDependencyServiceMock)@this).SetupReturnClassAsync(ct);
//
// 	public static ISetup<ClassReturn?> SetupReturnClassNullableAsync(this IMock<ITaskDependencyService> @this, in It<CancellationToken> ct) =>
// 		((TaskDependencyServiceMock)@this).SetupReturnClassNullableAsync(ct);
//
// 	public static ISetup<RecordReturn> SetupReturnRecordAsync(this IMock<ITaskDependencyService> @this, in It<CancellationToken> ct) =>
// 		((TaskDependencyServiceMock)@this).SetupReturnRecordAsync(ct);
//
// 	public static ISetup<RecordReturn?> SetupReturnRecordNullableAsync(this IMock<ITaskDependencyService> @this, in It<CancellationToken> ct) =>
// 		((TaskDependencyServiceMock)@this).SetupReturnRecordNullableAsync(ct);
// }
