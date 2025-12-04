using System.Collections.Concurrent;

namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class PrimitiveDependencyServiceMock<T> : IMock<IPrimitiveDependencyService<T>>
{
	private readonly InvocationIndex.Counter _invocationIndex;
	private readonly Func<IEnumerable<IInvocationProvider?>> _invocationProviders;
	private Proxy? _proxy;
	public IPrimitiveDependencyService<T> Object => _proxy ??= new Proxy(this);

	public PrimitiveDependencyServiceMock(in InvocationIndex.Counter invocationIndex)
	{
		_invocationIndex = invocationIndex;
		_invocationProviders = GetInvocations;
	}

	// GetOnly
	private Setup<T>? _getOnlyGet;
	private Invocation? _getOnlyGetInvocation;

	public Setup<T> SetupGetGetOnly()
	{
		return _getOnlyGet ??= new Setup<T>();
	}

	public void VerifyGetGetOnly(in Times times)
	{
		_getOnlyGetInvocation ??= new Invocation($"IPrimitiveDependencyService<{typeof(T).Name}>.GetOnly.get");
		_getOnlyGetInvocation.Verify(times, _invocationProviders);
	}

	public long VerifyGetGetOnly(in long index)
	{
		_getOnlyGetInvocation ??= new Invocation($"IPrimitiveDependencyService<{typeof(T).Name}>.GetOnly.get");
		return _getOnlyGetInvocation.Verify(index, _invocationProviders);
	}

	// GetSet
	private Setup<T>? _getSetGet;
	private Invocation? _getSetGetInvocation;
	private SetupWithParameter<T>? _getSetSet;
	private Invocation<T>? _getSetSetInvocation;

	public Setup<T> SetupGetGetSet()
	{
		return _getSetGet ??= new Setup<T>();
	}

	public void VerifyGetGetSet(in Times times)
	{
		_getSetGetInvocation ??= new Invocation($"IPrimitiveDependencyService<{typeof(T).Name}>.GetSet.get");
		_getSetGetInvocation.Verify(times, _invocationProviders);
	}

	public long VerifyGetGetSet(in long index)
	{
		_getSetGetInvocation ??= new Invocation($"IPrimitiveDependencyService<{typeof(T).Name}>.GetSet.get");
		return _getSetGetInvocation.Verify(index, _invocationProviders);
	}

	public SetupWithParameter<T> SetupSetGetSet(in It<T> value)
	{
		_getSetSet ??= new SetupWithParameter<T>();
		_getSetSet.SetupParameter(value);
		return _getSetSet;
	}

	public void VerifySetGetSet(in It<T> value, in Times times)
	{
		_getSetSetInvocation ??= new Invocation<T>($"IPrimitiveDependencyService<{typeof(T).Name}>.GetSet.set = {{0}}");
		_getSetSetInvocation.Verify(value, times, _invocationProviders);
	}

	public long VerifySetGetSet(in It<T> value, in long index)
	{
		_getSetSetInvocation ??= new Invocation<T>($"IPrimitiveDependencyService<{typeof(T).Name}>.GetSet.set = {{0}}");
		return _getSetSetInvocation.Verify(value, index, _invocationProviders);
	}

	// InvokeWithParameter
	private SetupWithParameter<T>? _invokeWithParameter;
	private Invocation<T>? _invokeWithParameterInvocation;

	public SetupWithParameter<T> SetupInvokeWithParameter(in It<T> parameter)
	{
		_invokeWithParameter ??= new SetupWithParameter<T>();
		_invokeWithParameter.SetupParameter(parameter);
		return _invokeWithParameter;
	}

	public void VerifyInvokeWithParameter(in It<T> parameter, in Times times)
	{
		_invokeWithParameterInvocation ??= new Invocation<T>($"IPrimitiveDependencyService<{typeof(T).Name}>.InvokeWithParameter({{0}})");
		_invokeWithParameterInvocation.Verify(parameter, times, _invocationProviders);
	}

	public long VerifyInvokeWithParameter(in It<T> parameter, in long index)
	{
		_invokeWithParameterInvocation ??= new Invocation<T>($"IPrimitiveDependencyService<{typeof(T).Name}>.InvokeWithParameter({{0}})");
		return _invokeWithParameterInvocation.Verify(parameter, index, _invocationProviders);
	}

	// InvokeWithSeveralParameters
	private ConcurrentDictionary<Type, object>? _invokeWithSeveralParameters;
	private InvocationDictionary? _invokeWithSeveralParametersInvocation;

	public SetupT1T2<TParameter, T> SetupInvokeWithSeveralParameters<TParameter>(in It<TParameter> param1, in It<T> param2)
	{
		_invokeWithSeveralParameters ??= new ConcurrentDictionary<Type, object>();
		var invokeWithSeveralParameters = (SetupT1T2<TParameter, T>)_invokeWithSeveralParameters.GetOrAdd(typeof(TParameter), static _ => new SetupT1T2<TParameter, T>());
		invokeWithSeveralParameters.SetupParameters(param1, param2);
		return invokeWithSeveralParameters;
	}

	public void VerifyInvokeWithSeveralParameters<TParameter>(in It<TParameter> param1, in It<T> param2, in Times times)
	{
		_invokeWithSeveralParametersInvocation ??= new InvocationDictionary();
		var invokeWithSeveralParametersInvocation = (InvocationT1T2<TParameter, T>)_invokeWithSeveralParametersInvocation.GetOrAdd(typeof(TParameter), static type => new InvocationT1T2<TParameter, T>($"IPrimitiveDependencyService<{typeof(T).Name}>.InvokeWithSeveralParameters<{type.Name}>({{0}}, {{1}})"));
		invokeWithSeveralParametersInvocation.Verify(param1, param2, times, _invocationProviders);
	}

	public long VerifyInvokeWithSeveralParameters<TParameter>(in It<TParameter> param1, in It<T> param2, in long index)
	{
		_invokeWithSeveralParametersInvocation ??= new InvocationDictionary();
		var invokeWithSeveralParametersInvocation = (InvocationT1T2<TParameter, T>)_invokeWithSeveralParametersInvocation.GetOrAdd(typeof(TParameter), static type => new InvocationT1T2<TParameter, T>($"IPrimitiveDependencyService<{typeof(T).Name}>.InvokeWithSeveralParameters<{type.Name}>({{0}}, {{1}})"));
		return invokeWithSeveralParametersInvocation.Verify(param1, param2, index, _invocationProviders);
	}

	// Return
	private ConcurrentDictionary<Type, object>? _return;
	private InvocationDictionary? _returnInvocation;

	public Setup<TReturn> SetupReturn<TReturn>()
	{
		_return ??= new ConcurrentDictionary<Type, object>();
		return (Setup<TReturn>)_return.GetOrAdd(typeof(TReturn), static _ => new Setup<TReturn>());
	}

	public void VerifyReturn<TReturn>(in Times times)
	{
		_returnInvocation ??= new InvocationDictionary();
		var returnInvocation = (Invocation)_returnInvocation.GetOrAdd(typeof(TReturn), static type => new Invocation($"IPrimitiveDependencyService<{typeof(T).Name}>.Return<{type.Name}>()"));
		returnInvocation.Verify(times, _invocationProviders);
	}

	public long VerifyReturn<TReturn>(in long index)
	{
		_returnInvocation ??= new InvocationDictionary();
		var returnInvocation = (Invocation)_returnInvocation.GetOrAdd(typeof(TReturn), static type => new Invocation($"IPrimitiveDependencyService<{typeof(T).Name}>.Return<{type.Name}>()"));
		return returnInvocation.Verify(index, _invocationProviders);
	}

	// ReturnWithParameter
	private ConcurrentDictionary<Type, object>? _returnWithParameter;
	private InvocationDictionary<Type>? _returnWithParameterInvocation;

	public SetupWithParameter<T, TReturn> SetupReturnWithParameter<TReturn>(in It<T> parameter)
	{
		_returnWithParameter ??= new ConcurrentDictionary<Type, object>();
		var returnWithParameter = (SetupWithParameter<T, TReturn>)_returnWithParameter.GetOrAdd(typeof(TReturn), static _ => new SetupWithParameter<T, TReturn>());
		returnWithParameter.SetupParameter(parameter);
		return returnWithParameter;
	}

	public void VerifyReturnWithParameter<TReturn>(in It<T> parameter, in Times times)
	{
		_returnWithParameterInvocation ??= new InvocationDictionary<Type>();
		var returnWithParameterInvocation = (Invocation<T>)_returnWithParameterInvocation.GetOrAdd(typeof(TReturn), static x => new Invocation<T>($"IPrimitiveDependencyService<{typeof(T).Name}>.ReturnWithParameter<{x.Name}>({{0}})"));
		returnWithParameterInvocation.Verify(parameter, times, _invocationProviders);
	}

	public long VerifyReturnWithParameter<TReturn>(in It<T> parameter, in long index)
	{
		_returnWithParameterInvocation ??= new InvocationDictionary<Type>();
		var returnWithParameterInvocation = (Invocation<T>)_returnWithParameterInvocation.GetOrAdd(typeof(TReturn), static x => new Invocation<T>($"IPrimitiveDependencyService<{typeof(T).Name}>.ReturnWithParameter<{x.Name}>({{0}})"));
		return returnWithParameterInvocation.Verify(parameter, index, _invocationProviders);
	}

	// ReturnWithSeveralParameters
	private ConcurrentDictionary<(Type, Type), object>? _returnWithSeveralParameters;
	private InvocationDictionary<(Type, Type)>? _returnWithSeveralParametersInvocation;

	public SetupT1T2<TParameter1, TParameter2, T> SetupReturnWithSeveralParameters<TParameter1, TParameter2>(in It<TParameter1> param1, in It<TParameter2> param2)
	{
		var key = (typeof(TParameter1), typeof(TParameter2));
		_returnWithSeveralParameters ??= new ConcurrentDictionary<(Type, Type), object>();
		var returnWithSeveralParameters = (SetupT1T2<TParameter1, TParameter2, T>)_returnWithSeveralParameters.GetOrAdd(key, static _ => new SetupT1T2<TParameter1, TParameter2, T>());
		returnWithSeveralParameters.SetupParameters(param1, param2);
		return returnWithSeveralParameters;
	}

	public void VerifyReturnWithSeveralParameters<TParameter1, TParameter2>(in It<TParameter1> param1, in It<TParameter2> param2, in Times times)
	{
		var key = (typeof(TParameter1), typeof(TParameter2));
		_returnWithSeveralParametersInvocation ??= new InvocationDictionary<(Type, Type)>();
		var returnWithSeveralParametersInvocation = (InvocationT1T2<TParameter1, TParameter2>)_returnWithSeveralParametersInvocation.GetOrAdd(key, static x => new InvocationT1T2<TParameter1, TParameter2>($"IPrimitiveDependencyService<{typeof(T).Name}>.ReturnWithSeveralParameters<{x.Item1.Name}, {x.Item2.Name}>({{0}}, {{1}})"));
		returnWithSeveralParametersInvocation.Verify(param1, param2, times, _invocationProviders);
	}

	public long VerifyReturnWithSeveralParameters<TParameter1, TParameter2>(in It<TParameter1> parameter1, in It<TParameter2> parameter2, in long index)
	{
		var key = (typeof(TParameter1), typeof(TParameter2));
		_returnWithSeveralParametersInvocation ??= new InvocationDictionary<(Type, Type)>();
		var returnWithSeveralParametersInvocation = (InvocationT1T2<TParameter1, TParameter2>)_returnWithSeveralParametersInvocation.GetOrAdd(key, static x => new InvocationT1T2<TParameter1, TParameter2>($"IPrimitiveDependencyService<{typeof(T).Name}>.ReturnWithSeveralParameters<{x.Item1.Name}, {x.Item2.Name}>({{0}}, {{1}})"));
		return returnWithSeveralParametersInvocation.Verify(parameter1, parameter2, index, _invocationProviders);
	}

	public void VerifyNoOtherCalls()
	{
		_getOnlyGetInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_getSetGetInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_getSetSetInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_invokeWithParameterInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_invokeWithSeveralParametersInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_returnInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_returnWithParameterInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_returnWithSeveralParametersInvocation?.VerifyNoOtherCalls(_invocationProviders);
	}

	private IEnumerable<IInvocationProvider?> GetInvocations()
	{
		yield return _getOnlyGetInvocation;
		yield return _getSetGetInvocation;
		yield return _getSetSetInvocation;
		yield return _invokeWithParameterInvocation;
		yield return _invokeWithSeveralParametersInvocation;
		yield return _returnInvocation;
		yield return _returnWithParameterInvocation;
		yield return _returnWithSeveralParametersInvocation;
	}

	private sealed class Proxy : IPrimitiveDependencyService<T>
	{
		private readonly PrimitiveDependencyServiceMock<T> _mock;

		public Proxy(PrimitiveDependencyServiceMock<T> mock)
		{
			_mock = mock;
		}

		public T GetOnly
		{
			get
			{
				_mock._getOnlyGetInvocation ??= new Invocation($"IPrimitiveDependencyService<{typeof(T).Name}>.GetOnly.get");
				_mock._getOnlyGetInvocation.Register(_mock._invocationIndex);
				return _mock._getOnlyGet?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
		}

		public T GetSet
		{
			get
			{
				_mock._getSetGetInvocation ??= new Invocation($"IPrimitiveDependencyService<{typeof(T).Name}>.GetSet.get");
				_mock._getSetGetInvocation.Register(_mock._invocationIndex);
				return _mock._getSetGet?.Execute(out var returnValue) == true ? returnValue! : default!;
			}
			set
			{
				_mock._getSetSetInvocation ??= new Invocation<T>($"IPrimitiveDependencyService<{typeof(T).Name}>.GetSet.set = {{0}}");
				_mock._getSetSetInvocation.Register(_mock._invocationIndex, value);
				_mock._getSetSet?.Invoke(value);
			}
		}

		public void InvokeWithParameter(T parameter)
		{
			_mock._invokeWithParameterInvocation ??= new Invocation<T>($"IPrimitiveDependencyService<{typeof(T).Name}>.InvokeWithParameter({{0}})");
			_mock._invokeWithParameterInvocation.Register(_mock._invocationIndex, parameter);
			_mock._invokeWithParameter?.Invoke(parameter);
		}

		public void InvokeWithSeveralParameters<TParameter>(TParameter param1, T param2)
		{
			_mock._invokeWithSeveralParametersInvocation ??= new InvocationDictionary();
			var invokeWithSeveralParametersInvocation = (InvocationT1T2<TParameter, T>)_mock._invokeWithSeveralParametersInvocation.GetOrAdd(typeof(TParameter), static type => new InvocationT1T2<TParameter, T>($"IPrimitiveDependencyService<{typeof(T).Name}>.InvokeWithSeveralParameters<{type.Name}>({{0}}, {{1}})"));
			invokeWithSeveralParametersInvocation.Register(_mock._invocationIndex, param1, param2);
			((SetupT1T2<TParameter, T>?)_mock._invokeWithSeveralParameters?.GetValueOrDefault(typeof(TParameter)))?.Invoke(param1, param2);
		}

		public TReturn Return<TReturn>()
		{
			_mock._returnInvocation ??= new InvocationDictionary();
			var returnInvocation = (Invocation)_mock._returnInvocation.GetOrAdd(typeof(TReturn), static type => new Invocation($"IPrimitiveDependencyService<{typeof(T).Name}>.Return<{type.Name}>()"));
			returnInvocation.Register(_mock._invocationIndex);
			return ((Setup<TReturn>?)_mock._return?.GetValueOrDefault(typeof(TReturn)))?.Execute(out var returnValue) == true ? returnValue! : default!;
		}

		public TReturn ReturnWithParameter<TReturn>(T parameter)
		{
			_mock._returnWithParameterInvocation ??= new InvocationDictionary();
			var returnWithParameterInvocation = (Invocation<T>)_mock._returnWithParameterInvocation.GetOrAdd(typeof(TReturn), static type => new Invocation<T>($"IPrimitiveDependencyService<{typeof(T).Name}>.ReturnWithParameter<{type.Name}>({{0}})"));
			returnWithParameterInvocation.Register(_mock._invocationIndex, parameter);
			return ((Setup<TReturn>?)_mock._return?.GetValueOrDefault(typeof(TReturn)))?.Execute(out var returnValue) == true ? returnValue! : default!;
		}

		public T ReturnWithSeveralParameters<TParameter1, TParameter2>(TParameter1 param1, TParameter2 param2)
		{
			var key = (typeof(TParameter1), typeof(TParameter2));
			_mock._returnWithSeveralParametersInvocation ??= new InvocationDictionary<(Type, Type)>();
			var returnWithParameterInvocation = (InvocationT1T2<TParameter1, TParameter2>)_mock._returnWithSeveralParametersInvocation.GetOrAdd(key, static x => new InvocationT1T2<TParameter1, TParameter2>($"IPrimitiveDependencyService<{typeof(T).Name}>.ReturnWithSeveralParameters<{x.Item1.Name}, {x.Item2.Name}>({{0}}, {{1}})"));
			returnWithParameterInvocation.Register(_mock._invocationIndex, param1, param2);
			return ((SetupT1T2<TParameter1, TParameter2, T>?)_mock._returnWithSeveralParameters?.GetValueOrDefault(key))?.Execute(param1, param2, out var returnValue) == true ? returnValue! : default!;
		}
	}
}

[Obsolete("Will be generated")]
public static class PrimitiveDependencyServiceMockTEx
{
	// GetOnly
	public static ISetup<Action, T, Func<T?>> SetupGetGetOnly<T>(this IMock<IPrimitiveDependencyService<T>> @this) =>
		((PrimitiveDependencyServiceMock<T>)@this).SetupGetGetOnly();

	public static void VerifyGetGetOnly<T>(this IMock<IPrimitiveDependencyService<T>> @this, in Times times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyGetGetOnly(times);

	public static void VerifyGetGetOnly<T>(this IMock<IPrimitiveDependencyService<T>> @this, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyGetGetOnly(times());

	// GetSet
	public static ISetup<Action, T, Func<T?>> SetupGetGetSet<T>(this IMock<IPrimitiveDependencyService<T>> @this) =>
		((PrimitiveDependencyServiceMock<T>)@this).SetupGetGetSet();

	public static void VerifyGetGetSet<T>(this IMock<IPrimitiveDependencyService<T>> @this, in Times times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyGetGetSet(times);

	public static void VerifyGetGetSet<T>(this IMock<IPrimitiveDependencyService<T>> @this, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyGetGetSet(times());

	public static ISetup<Action<T>> SetupSetGetSet<T>(this IMock<IPrimitiveDependencyService<T>> @this, in It<T> value = default) =>
		((PrimitiveDependencyServiceMock<T>)@this).SetupSetGetSet(value);

	public static void VerifySetGetSet<T>(this IMock<IPrimitiveDependencyService<T>> @this, in It<T> value, in Times times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifySetGetSet(value, times);

	public static void VerifySetGetSet<T>(this IMock<IPrimitiveDependencyService<T>> @this, in It<T> value, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifySetGetSet(value, times());

	// InvokeWithParameter
	public static ISetup<Action<T>> SetupInvokeWithParameter<T>(this IMock<IPrimitiveDependencyService<T>> @this, in It<T> parameter = default) =>
		((PrimitiveDependencyServiceMock<T>)@this).SetupInvokeWithParameter(parameter);

	public static void VerifyInvokeWithParameter<T>(this IMock<IPrimitiveDependencyService<T>> @this, in It<T> parameter, in Times times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyInvokeWithParameter(parameter, times);

	public static void VerifyInvokeWithParameter<T>(this IMock<IPrimitiveDependencyService<T>> @this, in It<T> parameter, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyInvokeWithParameter(parameter, times());

	// InvokeWithSeveralParameters
	public static ISetup<Action<TParameter, T>> SetupInvokeWithSeveralParameters<T, TParameter>(this IMock<IPrimitiveDependencyService<T>> @this, in It<TParameter> parameter1 = default, in It<T> parameter2 = default) =>
		((PrimitiveDependencyServiceMock<T>)@this).SetupInvokeWithSeveralParameters(parameter1, parameter2);

	public static void VerifyInvokeWithSeveralParameters<T, TParameter>(this IMock<IPrimitiveDependencyService<T>> @this, in It<TParameter> parameter1, in It<T> parameter2, in Times times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times);

	public static void VerifyInvokeWithSeveralParameters<T, TParameter>(this IMock<IPrimitiveDependencyService<T>> @this, in It<TParameter> parameter1, in It<T> parameter2, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times());

	// Return
	public static ISetup<Action, T, Func<T?>> SetupReturn<T>(this IMock<IPrimitiveDependencyService<T>> @this) =>
		((PrimitiveDependencyServiceMock<T>)@this).SetupReturn<T>();

	public static void VerifyReturn<T>(this IMock<IPrimitiveDependencyService<T>> @this, in Times times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyReturn<T>(times);

	public static void VerifyReturn<T>(this IMock<IPrimitiveDependencyService<T>> @this, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyReturn<T>(times());

	// ReturnWithParameter
	public static ISetup<Action<T>, TReturn, Func<T, TReturn?>> SetupReturnWithParameter<T, TReturn>(this IMock<IPrimitiveDependencyService<T>> @this, in It<T> parameter = default) =>
		((PrimitiveDependencyServiceMock<T>)@this).SetupReturnWithParameter<TReturn>(parameter);

	public static void VerifyReturnWithParameter<T, TReturn>(this IMock<IPrimitiveDependencyService<T>> @this, in It<T> parameter, in Times times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyReturnWithParameter<TReturn>(parameter, times);

	public static void VerifyReturnWithParameter<T, TReturn>(this IMock<IPrimitiveDependencyService<T>> @this, in It<T> parameter, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyReturnWithParameter<TReturn>(parameter, times());

	// ReturnWithSeveralParameters
	public static ISetup<SetupT1T2<TParameter1, TParameter2, T>.CallbackDelegate, T, SetupT1T2<TParameter1, TParameter2, T>.ReturnsCallbackDelegate> SetupReturnWithSeveralParameters<T, TParameter1, TParameter2>(this IMock<IPrimitiveDependencyService<T>> @this, in It<TParameter1> parameter1 = default, in It<TParameter2> parameter2 = default) =>
		((PrimitiveDependencyServiceMock<T>)@this).SetupReturnWithSeveralParameters(parameter1, parameter2);

	public static void VerifyReturnWithSeveralParameters<T, TParameter1, TParameter2>(this IMock<IPrimitiveDependencyService<T>> @this, in It<TParameter1> parameter1, in It<TParameter2> parameter2, in Times times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times);

	public static void VerifyReturnWithSeveralParameters<T, TParameter1, TParameter2>(this IMock<IPrimitiveDependencyService<T>> @this, in It<TParameter1> parameter1, in It<TParameter2> parameter2, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times());

	public static void VerifyNoOtherCalls<T>(this IMock<IPrimitiveDependencyService<T>> @this) =>
		((PrimitiveDependencyServiceMock<T>)@this).VerifyNoOtherCalls();
}

[Obsolete("Will be generated")]
public static class PrimitiveDependencyServiceMockSequenceTEx
{
	// GetOnly
	public static void GetGetOnly<T>(this IMockSequence<IPrimitiveDependencyService<T>> @this)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock<T>)@this.Mock).VerifyGetGetOnly(@this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	// GetSet
	public static void GetGetSet<T>(this IMockSequence<IPrimitiveDependencyService<T>> @this)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock<T>)@this.Mock).VerifyGetGetSet(@this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void SetGetSet<T>(this IMockSequence<IPrimitiveDependencyService<T>> @this, in It<T> value)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock<T>)@this.Mock).VerifySetGetSet(value, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	// InvokeWithParameter
	public static void InvokeWithParameter<T>(this IMockSequence<IPrimitiveDependencyService<T>> @this, in It<T> parameter)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock<T>)@this.Mock).VerifyInvokeWithParameter(parameter, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	// InvokeWithSeveralParameters
	public static void InvokeWithSeveralParameters<T, TParameter>(this IMockSequence<IPrimitiveDependencyService<T>> @this, in It<TParameter> parameter1, in It<T> parameter2)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock<T>)@this.Mock).VerifyInvokeWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	// Return
	public static void Return<T, TReturn>(this IMockSequence<IPrimitiveDependencyService<T>> @this)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock<T>)@this.Mock).VerifyReturn<TReturn>(@this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	// ReturnWithParameter
	public static void ReturnWithParameter<T, TReturn>(this IMockSequence<IPrimitiveDependencyService<T>> @this, in It<T> parameter)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock<T>)@this.Mock).VerifyReturnWithParameter<TReturn>(parameter, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	// ReturnWithSeveralParameters
	public static void ReturnWithSeveralParameters<T, TParameter1, TParameter2>(this IMockSequence<IPrimitiveDependencyService<T>> @this, in It<TParameter1> parameter1, in It<TParameter2> parameter2)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock<T>)@this.Mock).VerifyReturnWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}
}