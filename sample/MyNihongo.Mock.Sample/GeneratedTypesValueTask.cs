namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class ValueTaskDependencyServiceMock : IMock<IValueTaskDependencyService>
{
	private Proxy? _proxy;
	private SetupWithParameter<int>? _returnPrimitiveAsync;
	private SetupWithParameter<short?>? _returnPrimitiveNullableAsync;
	private SetupWithParameter<StructReturn>? _returnStructAsync;
	private SetupWithParameter<StructReturn?>? _returnStructNullableAsync;
	private SetupWithParameter<ClassReturn>? _returnClassAsync;
	private SetupWithParameter<ClassReturn?>? _returnClassNullableAsync;
	private SetupWithParameter<RecordReturn>? _returnRecordAsync;
	private SetupWithParameter<RecordReturn?>? _returnRecordNullableAsync;

	public SetupWithParameter<int> SetupReturnPrimitiveAsync(in CancellationToken ct)
	{
		_returnPrimitiveAsync ??= new SetupWithParameter<int>();

		var hashCode = ct.GetHashCode();
		_returnPrimitiveAsync.SetupParameters(hashCode);
		return _returnPrimitiveAsync;
	}

	public SetupWithParameter<short?> SetupReturnPrimitiveNullableAsync(in CancellationToken ct)
	{
		_returnPrimitiveNullableAsync ??= new SetupWithParameter<short?>();

		var hashCode = ct.GetHashCode();
		_returnPrimitiveNullableAsync.SetupParameters(hashCode);
		return _returnPrimitiveNullableAsync;
	}

	public SetupWithParameter<StructReturn> SetupReturnStructAsync(in CancellationToken ct)
	{
		_returnStructAsync ??= new SetupWithParameter<StructReturn>();

		var hashCode = ct.GetHashCode();
		_returnStructAsync.SetupParameters(hashCode);
		return _returnStructAsync;
	}

	public SetupWithParameter<StructReturn?> SetupReturnStructNullableAsync(in CancellationToken ct)
	{
		_returnStructNullableAsync ??= new SetupWithParameter<StructReturn?>();

		var hashCode = ct.GetHashCode();
		_returnStructNullableAsync.SetupParameters(hashCode);
		return _returnStructNullableAsync;
	}

	public SetupWithParameter<ClassReturn> SetupReturnClassAsync(in CancellationToken ct)
	{
		_returnClassAsync ??= new SetupWithParameter<ClassReturn>();

		var hashCode = ct.GetHashCode();
		_returnClassAsync.SetupParameters(hashCode);
		return _returnClassAsync;
	}

	public SetupWithParameter<ClassReturn?> SetupReturnClassNullableAsync(in CancellationToken ct)
	{
		_returnClassNullableAsync ??= new SetupWithParameter<ClassReturn?>();

		var hashCode = ct.GetHashCode();
		_returnClassNullableAsync.SetupParameters(hashCode);
		return _returnClassNullableAsync;
	}

	public SetupWithParameter<RecordReturn> SetupReturnRecordAsync(in CancellationToken ct)
	{
		_returnRecordAsync ??= new SetupWithParameter<RecordReturn>();

		var hashCode = ct.GetHashCode();
		_returnRecordAsync.SetupParameters(hashCode);
		return _returnRecordAsync;
	}

	public SetupWithParameter<RecordReturn?> SetupReturnRecordNullableAsync(in CancellationToken ct)
	{
		_returnRecordNullableAsync ??= new SetupWithParameter<RecordReturn?>();

		var hashCode = ct.GetHashCode();
		_returnRecordNullableAsync.SetupParameters(hashCode);
		return _returnRecordNullableAsync;
	}

	public IValueTaskDependencyService Object => _proxy ??= new Proxy(this);

	private sealed class Proxy : IValueTaskDependencyService
	{
		private readonly ValueTaskDependencyServiceMock _mock;

		public Proxy(in ValueTaskDependencyServiceMock mock)
		{
			_mock = mock;
		}

		public ValueTask InvokeAsync(CancellationToken ct = default)
		{
			return ValueTask.CompletedTask;
		}

		public ValueTask<int> ReturnPrimitiveAsync(CancellationToken ct = default)
		{
			var hashCode = ct.GetHashCode();
			return _mock._returnPrimitiveAsync?.TryInvoke(hashCode, out var returnValue) == true ? ValueTask.FromResult(returnValue) : throw new NullReferenceException("IValueTaskDependencyService#ReturnPrimitiveAsync() method has not been set up");
		}

		public ValueTask<short?> ReturnPrimitiveNullableAsync(CancellationToken ct = default)
		{
			var hashCode = ct.GetHashCode();
			return _mock._returnPrimitiveNullableAsync?.TryInvoke(hashCode, out var returnValue) == true ? ValueTask.FromResult(returnValue) : ValueTask.FromResult<short?>(null);
		}

		public ValueTask<StructReturn> ReturnStructAsync(CancellationToken ct = default)
		{
			var hashCode = ct.GetHashCode();
			return _mock._returnStructAsync?.TryInvoke(hashCode, out var returnValue) == true ? ValueTask.FromResult(returnValue) : throw new NullReferenceException("IValueTaskDependencyService#ReturnStructAsync() method has not been set up");
		}

		public ValueTask<StructReturn?> ReturnStructNullableAsync(CancellationToken ct = default)
		{
			var hashCode = ct.GetHashCode();
			return _mock._returnStructNullableAsync?.TryInvoke(hashCode, out var returnValue) == true ? ValueTask.FromResult(returnValue) : ValueTask.FromResult<StructReturn?>(null);
		}

		public ValueTask<ClassReturn> ReturnClassAsync(CancellationToken ct = default)
		{
			var hashCode = ct.GetHashCode();
			return _mock._returnClassAsync?.TryInvoke(hashCode, out var returnValue) == true ? ValueTask.FromResult(returnValue) : throw new NullReferenceException("IValueTaskDependencyService#ReturnClassAsync() method has not been set up");
		}

		public ValueTask<ClassReturn?> ReturnClassNullableAsync(CancellationToken ct = default)
		{
			var hashCode = ct.GetHashCode();
			return _mock._returnClassNullableAsync?.TryInvoke(hashCode, out var returnValue) == true ? ValueTask.FromResult(returnValue) : ValueTask.FromResult<ClassReturn?>(null);
		}

		public ValueTask<RecordReturn> ReturnRecordAsync(CancellationToken ct = default)
		{
			var hashCode = ct.GetHashCode();
			return _mock._returnRecordAsync?.TryInvoke(hashCode, out var returnValue) == true ? ValueTask.FromResult(returnValue) : throw new NullReferenceException("IValueTaskDependencyService#ReturnRecordAsync() method has not been set up");
		}

		public ValueTask<RecordReturn?> ReturnRecordNullableAsync(CancellationToken ct = default)
		{
			var hashCode = ct.GetHashCode();
			return _mock._returnRecordNullableAsync?.TryInvoke(hashCode, out var returnValue) == true ? ValueTask.FromResult(returnValue) : ValueTask.FromResult<RecordReturn?>(null);
		}
	}
}

[Obsolete("Will be generated")]
public static class ValueTaskDependencyServiceMockEx
{
	public static ISetup<int> SetupReturnPrimitiveAsync(this IMock<IValueTaskDependencyService> @this, in CancellationToken ct) =>
		((ValueTaskDependencyServiceMock)@this).SetupReturnPrimitiveAsync(ct);

	public static ISetup<short?> SetupReturnPrimitiveNullableAsync(this IMock<IValueTaskDependencyService> @this, in CancellationToken ct) =>
		((ValueTaskDependencyServiceMock)@this).SetupReturnPrimitiveNullableAsync(ct);

	public static ISetup<StructReturn> SetupReturnStructAsync(this IMock<IValueTaskDependencyService> @this, in CancellationToken ct) =>
		((ValueTaskDependencyServiceMock)@this).SetupReturnStructAsync(ct);

	public static ISetup<StructReturn?> SetupReturnStructNullableAsync(this IMock<IValueTaskDependencyService> @this, in CancellationToken ct) =>
		((ValueTaskDependencyServiceMock)@this).SetupReturnStructNullableAsync(ct);

	public static ISetup<ClassReturn> SetupReturnClassAsync(this IMock<IValueTaskDependencyService> @this, in CancellationToken ct) =>
		((ValueTaskDependencyServiceMock)@this).SetupReturnClassAsync(ct);

	public static ISetup<ClassReturn?> SetupReturnClassNullableAsync(this IMock<IValueTaskDependencyService> @this, in CancellationToken ct) =>
		((ValueTaskDependencyServiceMock)@this).SetupReturnClassNullableAsync(ct);

	public static ISetup<RecordReturn> SetupReturnRecordAsync(this IMock<IValueTaskDependencyService> @this, in CancellationToken ct) =>
		((ValueTaskDependencyServiceMock)@this).SetupReturnRecordAsync(ct);

	public static ISetup<RecordReturn?> SetupReturnRecordNullableAsync(this IMock<IValueTaskDependencyService> @this, in CancellationToken ct) =>
		((ValueTaskDependencyServiceMock)@this).SetupReturnRecordNullableAsync(ct);
}
