using System.Collections.Concurrent;

namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class PrimitiveDependencyServiceMock : IMock<IPrimitiveDependencyService>
{
	private readonly InvocationIndex.Counter _invocationIndex;
	private readonly Func<IEnumerable<IInvocationProvider?>> _invocationProviders;
	private Proxy? _proxy;
	public IPrimitiveDependencyService Object => _proxy ??= new Proxy(this);

	public PrimitiveDependencyServiceMock(in InvocationIndex.Counter invocationIndex)
	{
		_invocationIndex = invocationIndex;
		_invocationProviders = GetInvocations;
	}

	// Handler
	private PrimitiveHandler? _handler;
	private Invocation<PrimitiveHandler?>? _handlerAddInvocation;
	private Invocation<PrimitiveHandler?>? _handlerRemoveInvocation;

	public void RaiseHandler(in int value)
	{
		_handler?.Invoke(Object, value);
	}

	public void VerifyAddHandler(in It<PrimitiveHandler?> handler, in Times times)
	{
		_handlerAddInvocation ??= new Invocation<PrimitiveHandler?>("IPrimitiveDependencyService.Handler.add");
		_handlerAddInvocation.Verify(handler.ValueSetup, times, _invocationProviders);
	}

	public long VerifyAddHandler(in It<PrimitiveHandler?> handler, in long index)
	{
		_handlerAddInvocation ??= new Invocation<PrimitiveHandler?>("IPrimitiveDependencyService.Handler.add");
		return _handlerAddInvocation.Verify(handler.ValueSetup, index, _invocationProviders);
	}

	public void VerifyRemoveHandler(in It<PrimitiveHandler?> handler, in Times times)
	{
		_handlerRemoveInvocation ??= new Invocation<PrimitiveHandler?>("IPrimitiveDependencyService.Handler.remove");
		_handlerRemoveInvocation.Verify(handler.ValueSetup, times, _invocationProviders);
	}

	public long VerifyRemoveHandler(in It<PrimitiveHandler?> handler, in long index)
	{
		_handlerRemoveInvocation ??= new Invocation<PrimitiveHandler?>("IPrimitiveDependencyService.Handler.remove");
		return _handlerRemoveInvocation.Verify(handler.ValueSetup, index, _invocationProviders);
	}

	// HandlerEvent
	private EventHandler<string>? _handlerEvent;
	private Invocation<EventHandler<string>?>? _handlerEventAddInvocation;
	private Invocation<EventHandler<string>?>? _handlerEventRemoveInvocation;

	public void RaiseHandlerEvent(in string value)
	{
		_handlerEvent?.Invoke(Object, value);
	}

	public void VerifyAddHandlerEvent(in It<EventHandler<string>?> handler, in Times times)
	{
		_handlerEventAddInvocation ??= new Invocation<EventHandler<string>?>("IPrimitiveDependencyService.HandlerEvent.add");
		_handlerEventAddInvocation.Verify(handler.ValueSetup, times, _invocationProviders);
	}

	public long VerifyAddHandlerEvent(in It<EventHandler<string>?> handler, in long index)
	{
		_handlerEventAddInvocation ??= new Invocation<EventHandler<string>?>("IPrimitiveDependencyService.HandlerEvent.add");
		return _handlerEventAddInvocation.Verify(handler.ValueSetup, index, _invocationProviders);
	}

	public void VerifyRemoveHandlerEvent(in It<EventHandler<string>?> handler, in Times times)
	{
		_handlerEventRemoveInvocation ??= new Invocation<EventHandler<string>?>("IPrimitiveDependencyService.HandlerEvent.remove");
		_handlerEventRemoveInvocation.Verify(handler.ValueSetup, times, _invocationProviders);
	}

	public long VerifyRemoveHandlerEvent(in It<EventHandler<string>?> handler, in long index)
	{
		_handlerEventRemoveInvocation ??= new Invocation<EventHandler<string>?>("IPrimitiveDependencyService.HandlerEvent.remove");
		return _handlerEventRemoveInvocation.Verify(handler.ValueSetup, index, _invocationProviders);
	}

	// GetOnly
	private Setup<int>? _getOnlyGet;
	private Invocation? _getOnlyGetInvocation;

	public Setup<int> SetupGetGetOnly()
	{
		return _getOnlyGet ??= new Setup<int>();
	}

	public void VerifyGetGetOnly(in Times times)
	{
		_getOnlyGetInvocation ??= new Invocation("IPrimitiveDependencyService.GetOnly.get");
		_getOnlyGetInvocation.Verify(times, _invocationProviders);
	}

	public long VerifyGetGetOnly(in long index)
	{
		_getOnlyGetInvocation ??= new Invocation("IPrimitiveDependencyService.GetOnly.get");
		return _getOnlyGetInvocation.Verify(index, _invocationProviders);
	}

	// SetOnly
	private SetupWithParameter<decimal>? _setOnlySet;
	private Invocation<decimal>? _setOnlySetInvocation;

	public SetupWithParameter<decimal> SetupSetSetOnly(in It<decimal> value)
	{
		_setOnlySet ??= new SetupWithParameter<decimal>();
		_setOnlySet.SetupParameter(value.ValueSetup);
		return _setOnlySet;
	}

	public void VerifySetSetOnly(in It<decimal> value, in Times times)
	{
		_setOnlySetInvocation ??= new Invocation<decimal>("IPrimitiveDependencyService.SetOnly.set = {0}");
		_setOnlySetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
	}

	public long VerifySetSetOnly(in It<decimal> value, in long index)
	{
		_setOnlySetInvocation ??= new Invocation<decimal>("IPrimitiveDependencyService.SetOnly.set = {0}");
		return _setOnlySetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
	}

	// GetInit
	private Setup<string>? _getInitGet;
	private Invocation? _getInitGetInvocation;
	private SetupWithParameter<string>? _getInitSet;
	private Invocation<string>? _getInitSetInvocation;

	public Setup<string> SetupGetGetInit()
	{
		return _getInitGet ??= new Setup<string>();
	}

	public void VerifyGetGetInit(in Times times)
	{
		_getInitGetInvocation ??= new Invocation("IPrimitiveDependencyService.GetInit.get");
		_getInitGetInvocation.Verify(times, _invocationProviders);
	}

	public long VerifyGetGetInit(in long index)
	{
		_getInitGetInvocation ??= new Invocation("IPrimitiveDependencyService.GetInit.get");
		return _getInitGetInvocation.Verify(index, _invocationProviders);
	}

	public SetupWithParameter<string> SetupSetGetInit(in It<string> value)
	{
		_getInitSet ??= new SetupWithParameter<string>();
		_getInitSet.SetupParameter(value.ValueSetup);
		return _getInitSet;
	}

	public void VerifySetGetInit(in It<string> value, in Times times)
	{
		_getInitSetInvocation ??= new Invocation<string>("IPrimitiveDependencyService.GetInit.set = {0}");
		_getInitSetInvocation.Verify(value.ValueSetup, times, _invocationProviders);
	}

	public long VerifySetGetInit(in It<string> value, in long index)
	{
		_getInitSetInvocation ??= new Invocation<string>("IPrimitiveDependencyService.GetInit.set = {0}");
		return _getInitSetInvocation.Verify(value.ValueSetup, index, _invocationProviders);
	}

	// Invoke
	private Setup? _invoke1;
	private Invocation? _invokeInvocation1;

	public Setup SetupInvoke()
	{
		return _invoke1 ??= new Setup();
	}

	public void VerifyInvoke(in Times times)
	{
		_invokeInvocation1 ??= new Invocation("IPrimitiveDependencyService.Invoke()");
		_invokeInvocation1.Verify(times, _invocationProviders);
	}

	public long VerifyInvoke(in long index)
	{
		_invokeInvocation1 ??= new Invocation("IPrimitiveDependencyService.Invoke()");
		return _invokeInvocation1.Verify(index, _invocationProviders);
	}

	// Invoke
	private SetupWithOutParameter<int>? _invoke2;
	private Invocation<int>? _invokeInvocation2;

	public SetupWithOutParameter<int> SetupInvoke(in ItOut<int> result)
	{
		return _invoke2 ??= new SetupWithOutParameter<int>();
	}

	public void VerifyInvoke(in ItOut<int> result, in Times times)
	{
		_invokeInvocation2 ??= new Invocation<int>("IPrimitiveDependencyService.Invoke({0})", prefix: "out");
		_invokeInvocation2.Verify(result.ValueSetup, times, _invocationProviders);
	}

	public long VerifyInvoke(in ItOut<int> result, in long index)
	{
		_invokeInvocation2 ??= new Invocation<int>("IPrimitiveDependencyService.Invoke({0})", prefix: "out");
		return _invokeInvocation2.Verify(result.ValueSetup, index, _invocationProviders);
	}

	// Invoke
	private ConcurrentDictionary<Type, Setup>? _invoke3;
	private InvocationDictionary? _invokeInvocation3;

	public Setup SetupInvoke<T>()
	{
		_invoke3 ??= new ConcurrentDictionary<Type, Setup>();
		return _invoke3.GetOrAdd(typeof(T), static _ => new Setup());
	}

	public void VerifyInvoke<T>(in Times times)
	{
		_invokeInvocation3 ??= new InvocationDictionary();
		var invokeInvocation3 = (Invocation)_invokeInvocation3.GetOrAdd(typeof(T), static type => new Invocation($"IPrimitiveDependencyService.Invoke<{type.Name}>()"));
		invokeInvocation3.Verify(times, _invocationProviders);
	}

	public long VerifyInvoke<T>(in long index)
	{
		_invokeInvocation3 ??= new InvocationDictionary();
		var invokeInvocation3 = (Invocation)_invokeInvocation3.GetOrAdd(typeof(T), static type => new Invocation($"IPrimitiveDependencyService.Invoke<{type.Name}>()"));
		return invokeInvocation3.Verify(index, _invocationProviders);
	}

	// InvokeAsync
	private Setup? _invokeAsync;
	private Invocation? _invokeAsyncInvocation;

	public Setup SetupInvokeAsync()
	{
		return _invokeAsync ??= new Setup();
	}

	public void VerifyInvokeAsync(in Times times)
	{
		_invokeAsyncInvocation ??= new Invocation("IPrimitiveDependencyService.InvokeAsync()");
		_invokeAsyncInvocation.Verify(times, _invocationProviders);
	}

	public long VerifyInvokeAsync(in long index)
	{
		_invokeAsyncInvocation ??= new Invocation("IPrimitiveDependencyService.InvokeAsync()");
		return _invokeAsyncInvocation.Verify(index, _invocationProviders);
	}

	// InvokeWithParameter
	private SetupWithParameter<string>? _invokeWithParameter1;
	private Invocation<string>? _invokeWithParameterInvocation1;

	public SetupWithParameter<string> SetupInvokeWithParameter(in It<string> parameter)
	{
		_invokeWithParameter1 ??= new SetupWithParameter<string>();
		_invokeWithParameter1.SetupParameter(parameter.ValueSetup);
		return _invokeWithParameter1;
	}

	public void VerifyInvokeWithParameter(in It<string> parameter, in Times times)
	{
		_invokeWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService.InvokeWithParameter({0})");
		_invokeWithParameterInvocation1.Verify(parameter.ValueSetup, times, _invocationProviders);
	}

	public long VerifyInvokeWithParameter(in It<string> parameter, in long index)
	{
		_invokeWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService.InvokeWithParameter({0})");
		return _invokeWithParameterInvocation1.Verify(parameter.ValueSetup, index, _invocationProviders);
	}

	// InvokeWithParameter
	private SetupWithParameter<int>? _invokeWithParameter2;
	private Invocation<int>? _invokeWithParameterInvocation2;

	public SetupWithParameter<int> SetupInvokeWithParameter(in It<int> parameter)
	{
		_invokeWithParameter2 ??= new SetupWithParameter<int>();
		_invokeWithParameter2.SetupParameter(parameter.ValueSetup);
		return _invokeWithParameter2;
	}

	public void VerifyInvokeWithParameter(in It<int> parameter, in Times times)
	{
		_invokeWithParameterInvocation2 ??= new Invocation<int>("IPrimitiveDependencyService.InvokeWithParameter({0})");
		_invokeWithParameterInvocation2.Verify(parameter.ValueSetup, times, _invocationProviders);
	}

	public long VerifyInvokeWithParameter(in It<int> parameter, in long index)
	{
		_invokeWithParameterInvocation2 ??= new Invocation<int>("IPrimitiveDependencyService.InvokeWithParameter({0})");
		return _invokeWithParameterInvocation2.Verify(parameter.ValueSetup, index, _invocationProviders);
	}

	// InvokeWithParameter
	private SetupWithRefParameter<decimal>? _invokeWithParameter3;
	private Invocation<decimal>? _invokeWithParameterInvocation3;

	public SetupWithRefParameter<decimal> SetupInvokeWithParameter(in ItRef<decimal> parameter)
	{
		_invokeWithParameter3 ??= new SetupWithRefParameter<decimal>();
		_invokeWithParameter3.SetupParameter(parameter.ValueSetup);
		return _invokeWithParameter3;
	}

	public void VerifyInvokeWithParameter(in ItRef<decimal> parameter, in Times times)
	{
		_invokeWithParameterInvocation3 ??= new Invocation<decimal>("IPrimitiveDependencyService.InvokeWithParameter({0})", prefix: "ref");
		_invokeWithParameterInvocation3.Verify(parameter.ValueSetup, times, _invocationProviders);
	}

	public long VerifyInvokeWithParameter(in ItRef<decimal> parameter, in long index)
	{
		_invokeWithParameterInvocation3 ??= new Invocation<decimal>("IPrimitiveDependencyService.InvokeWithParameter({0})", prefix: "ref");
		return _invokeWithParameterInvocation3.Verify(parameter.ValueSetup, index, _invocationProviders);
	}

	// InvokeWithParameter
	private ConcurrentDictionary<Type, object>? _invokeWithParameter4;
	private InvocationDictionary? _invokeWithParameterInvocation4;

	public SetupWithParameter<T> SetupInvokeWithParameter<T>(in It<T> parameter)
	{
		_invokeWithParameter4 ??= new ConcurrentDictionary<Type, object>();
		var invokeWithParameter4 = (SetupWithParameter<T>)_invokeWithParameter4.GetOrAdd(typeof(T), static _ => new SetupWithParameter<T>());
		invokeWithParameter4.SetupParameter(parameter.ValueSetup);
		return invokeWithParameter4;
	}

	public void VerifyInvokeWithParameter<T>(in It<T> parameter, in Times times)
	{
		_invokeWithParameterInvocation4 ??= new InvocationDictionary();
		var invokeWithParameterInvocation4 = (Invocation<T>)_invokeWithParameterInvocation4.GetOrAdd(typeof(T), static type => new Invocation<T>($"IPrimitiveDependencyService.InvokeWithParameter<{type.Name}>({{0}})"));
		invokeWithParameterInvocation4.Verify(parameter.ValueSetup, times, _invocationProviders);
	}

	public long VerifyInvokeWithParameter<T>(in It<T> parameter, in long index)
	{
		_invokeWithParameterInvocation4 ??= new InvocationDictionary();
		var invokeWithParameterInvocation4 = (Invocation<T>)_invokeWithParameterInvocation4.GetOrAdd(typeof(T), static type => new Invocation<T>($"IPrimitiveDependencyService.InvokeWithParameter<{type.Name}>({{0}})"));
		return invokeWithParameterInvocation4.Verify(parameter.ValueSetup, index, _invocationProviders);
	}

	// InvokeWithParameterAsync
	private SetupWithParameter<int>? _invokeWithParameterAsync;
	private Invocation<int>? _invokeWithParameterAsyncInvocation;

	public SetupWithParameter<int> SetupInvokeWithParameterAsync(in It<int> parameter)
	{
		_invokeWithParameterAsync ??= new SetupWithParameter<int>();
		_invokeWithParameterAsync.SetupParameter(parameter.ValueSetup);
		return _invokeWithParameterAsync;
	}

	public void VerifyInvokeWithParameterAsync(in It<int> parameter, in Times times)
	{
		_invokeWithParameterAsyncInvocation ??= new Invocation<int>("IPrimitiveDependencyService.InvokeWithParameterAsync({0})");
		_invokeWithParameterAsyncInvocation.Verify(parameter.ValueSetup, times, _invocationProviders);
	}

	public long VerifyInvokeWithParameterAsync(in It<int> parameter, in long index)
	{
		_invokeWithParameterAsyncInvocation ??= new Invocation<int>("IPrimitiveDependencyService.InvokeWithParameterAsync({0})");
		return _invokeWithParameterAsyncInvocation.Verify(parameter.ValueSetup, index, _invocationProviders);
	}

	// InvokeWithSeveralParameters
	private SetupIntInt? _invokeWithSeveralParameters1;
	private InvocationIntInt? _invokeWithSeveralParametersInvocation1;

	public SetupIntInt SetupInvokeWithSeveralParameters(in It<int> parameter1, in It<int> parameter2)
	{
		_invokeWithSeveralParameters1 ??= new SetupIntInt();
		_invokeWithSeveralParameters1.SetupParameters(parameter1.ValueSetup, parameter2.ValueSetup);
		return _invokeWithSeveralParameters1;
	}

	public void VerifyInvokeWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in Times times)
	{
		_invokeWithSeveralParametersInvocation1 ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParameters({0}, {1})");
		_invokeWithSeveralParametersInvocation1.Verify(parameter1.ValueSetup, parameter2.ValueSetup, times, _invocationProviders);
	}

	public long VerifyInvokeWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in long index)
	{
		_invokeWithSeveralParametersInvocation1 ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParameters({0}, {1})");
		return _invokeWithSeveralParametersInvocation1.Verify(parameter1.ValueSetup, parameter2.ValueSetup, index, _invocationProviders);
	}

	// InvokeWithSeveralParameters
	private SetupRefIntInt? _invokeWithSeveralParameters2;
	private InvocationIntInt? _invokeWithSeveralParametersInvocation2;

	public SetupRefIntInt SetupInvokeWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2)
	{
		_invokeWithSeveralParameters2 ??= new SetupRefIntInt();
		_invokeWithSeveralParameters2.SetupParameters(parameter1.ValueSetup, parameter2.ValueSetup);
		return _invokeWithSeveralParameters2;
	}

	public void VerifyInvokeWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2, in Times times)
	{
		_invokeWithSeveralParametersInvocation2 ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParameters({0}, {1})", prefix1: "ref");
		_invokeWithSeveralParametersInvocation2.Verify(parameter1.ValueSetup, parameter2.ValueSetup, times, _invocationProviders);
	}

	public long VerifyInvokeWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2, in long index)
	{
		_invokeWithSeveralParametersInvocation2 ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParameters({0}, {1})", prefix1: "ref");
		return _invokeWithSeveralParametersInvocation2.Verify(parameter1.ValueSetup, parameter2.ValueSetup, index, _invocationProviders);
	}

	// InvokeWithSeveralParameters
	private SetupIntRefInt? _invokeWithSeveralParameters3;
	private InvocationIntInt? _invokeWithSeveralParametersInvocation3;

	public SetupIntRefInt SetupInvokeWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2)
	{
		_invokeWithSeveralParameters3 ??= new SetupIntRefInt();
		_invokeWithSeveralParameters3.SetupParameters(parameter1.ValueSetup, parameter2.ValueSetup);
		return _invokeWithSeveralParameters3;
	}

	public void VerifyInvokeWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2, in Times times)
	{
		_invokeWithSeveralParametersInvocation3 ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParameters({0}, {1})", prefix2: "ref");
		_invokeWithSeveralParametersInvocation3.Verify(parameter1.ValueSetup, parameter2.ValueSetup, times, _invocationProviders);
	}

	public long VerifyInvokeWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2, in long index)
	{
		_invokeWithSeveralParametersInvocation3 ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParameters({0}, {1})", prefix2: "ref");
		return _invokeWithSeveralParametersInvocation3.Verify(parameter1.ValueSetup, parameter2.ValueSetup, index, _invocationProviders);
	}

	// InvokeWithSeveralParameters
	private SetupRefIntRefInt? _invokeWithSeveralParameters4;
	private InvocationIntInt? _invokeWithSeveralParametersInvocation4;

	public SetupRefIntRefInt SetupInvokeWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2)
	{
		_invokeWithSeveralParameters4 ??= new SetupRefIntRefInt();
		_invokeWithSeveralParameters4.SetupParameters(parameter1.ValueSetup, parameter2.ValueSetup);
		return _invokeWithSeveralParameters4;
	}

	public void VerifyInvokeWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2, in Times times)
	{
		_invokeWithSeveralParametersInvocation4 ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParameters({0}, {1})", prefix1: "ref", prefix2: "ref");
		_invokeWithSeveralParametersInvocation4.Verify(parameter1.ValueSetup, parameter2.ValueSetup, times, _invocationProviders);
	}

	public long VerifyInvokeWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2, in long index)
	{
		_invokeWithSeveralParametersInvocation4 ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParameters({0}, {1})", prefix1: "ref", prefix2: "ref");
		return _invokeWithSeveralParametersInvocation4.Verify(parameter1.ValueSetup, parameter2.ValueSetup, index, _invocationProviders);
	}

	// InvokeWithSeveralParameters
	private ConcurrentDictionary<Type, object>? _invokeWithSeveralParameters5;
	private InvocationDictionary? _invokeWithSeveralParametersInvocation5;

	public SetupT1Int<T> SetupInvokeWithSeveralParameters<T>(in It<T> parameter1, in It<int> parameter2)
	{
		_invokeWithSeveralParameters5 ??= new ConcurrentDictionary<Type, object>();
		var invokeWithSeveralParameters5 = (SetupT1Int<T>)_invokeWithSeveralParameters5.GetOrAdd(typeof(T), static _ => new SetupT1Int<T>());
		invokeWithSeveralParameters5.SetupParameters(parameter1, parameter2);
		return invokeWithSeveralParameters5;
	}

	public void VerifyInvokeWithSeveralParameters<T>(in It<T> parameter1, in It<int> parameter2, in Times times)
	{
		_invokeWithSeveralParametersInvocation5 ??= new InvocationDictionary();
		var invokeWithSeveralParametersInvocation5 = (InvocationT1Int<T>)_invokeWithSeveralParametersInvocation5.GetOrAdd(typeof(T), static type => new InvocationT1Int<T>($"IPrimitiveDependencyService.InvokeWithSeveralParameters<{type.Name}>({{0}}, {{1}})"));
		invokeWithSeveralParametersInvocation5.Verify(parameter1, parameter2, times, _invocationProviders);
	}

	public long VerifyInvokeWithSeveralParameters<T>(in It<T> parameter1, in It<int> parameter2, in long index)
	{
		_invokeWithSeveralParametersInvocation5 ??= new InvocationDictionary();
		var invokeWithSeveralParametersInvocation5 = (InvocationT1Int<T>)_invokeWithSeveralParametersInvocation5.GetOrAdd(typeof(T), static type => new InvocationT1Int<T>($"IPrimitiveDependencyService.InvokeWithSeveralParameters<{type.Name}>({{0}}, {{1}})"));
		return invokeWithSeveralParametersInvocation5.Verify(parameter1.ValueSetup, parameter2.ValueSetup, index, _invocationProviders);
	}

	// InvokeWithSeveralParametersAsync
	private SetupIntInt? _invokeWithSeveralParametersAsync;
	private InvocationIntInt? _invokeWithSeveralParametersAsyncInvocation;

	public SetupIntInt SetupInvokeWithSeveralParametersAsync(in It<int> parameter1, in It<int> parameter2)
	{
		_invokeWithSeveralParametersAsync ??= new SetupIntInt();
		_invokeWithSeveralParametersAsync.SetupParameters(parameter1.ValueSetup, parameter2.ValueSetup);
		return _invokeWithSeveralParametersAsync;
	}

	public void VerifyInvokeWithSeveralParametersAsync(in It<int> parameter1, in It<int> parameter2, in Times times)
	{
		_invokeWithSeveralParametersAsyncInvocation ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParametersAsync({0}, {1})");
		_invokeWithSeveralParametersAsyncInvocation.Verify(parameter1.ValueSetup, parameter2.ValueSetup, times, _invocationProviders);
	}

	public long VerifyInvokeWithSeveralParametersAsync(in It<int> parameter1, in It<int> parameter2, in long index)
	{
		_invokeWithSeveralParametersAsyncInvocation ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParametersAsync({0}, {1})");
		return _invokeWithSeveralParametersAsyncInvocation.Verify(parameter1.ValueSetup, parameter2.ValueSetup, index, _invocationProviders);
	}

	// Return
	private Setup<int>? _return1;
	private Invocation? _returnInvocation1;

	public Setup<int> SetupReturn()
	{
		return _return1 ??= new Setup<int>();
	}

	public void VerifyReturn(in Times times)
	{
		_returnInvocation1 ??= new Invocation("IPrimitiveDependencyService.Return()");
		_returnInvocation1.Verify(times, _invocationProviders);
	}

	public long VerifyReturn(in long index)
	{
		_returnInvocation1 ??= new Invocation("IPrimitiveDependencyService.Return()");
		return _returnInvocation1.Verify(index, _invocationProviders);
	}

	// Return
	private SetupWithOutParameter<string, bool>? _return2;
	private Invocation<string>? _returnInvocation2;

	public SetupWithOutParameter<string, bool> SetupReturn(in ItOut<string> result)
	{
		return _return2 ??= new SetupWithOutParameter<string, bool>();
	}

	public void VerifyReturn(in ItOut<string> result, in Times times)
	{
		_returnInvocation2 ??= new Invocation<string>("IPrimitiveDependencyService.Return({0})", prefix: "out");
		_returnInvocation2.Verify(result.ValueSetup, times, _invocationProviders);
	}

	public long VerifyReturn(in ItOut<string> result, in long index)
	{
		_returnInvocation2 ??= new Invocation<string>("IPrimitiveDependencyService.Return({0})", prefix: "out");
		return _returnInvocation2.Verify(result.ValueSetup, index, _invocationProviders);
	}

	// Return
	private ConcurrentDictionary<Type, object>? _return3;
	private InvocationDictionary? _returnInvocation3;

	public Setup<T> SetupReturn<T>()
	{
		_return3 ??= new ConcurrentDictionary<Type, object>();
		return (Setup<T>)_return3.GetOrAdd(typeof(T), static _ => new Setup<T>());
	}

	public void VerifyReturn<T>(in Times times)
	{
		_returnInvocation3 ??= new InvocationDictionary();
		var returnInvocation3 = (Invocation)_returnInvocation3.GetOrAdd(typeof(T), static type => new Invocation($"IPrimitiveDependencyService.Return<{type.Name}>()"));
		returnInvocation3.Verify(times, _invocationProviders);
	}

	public long VerifyReturn<T>(in long index)
	{
		_returnInvocation3 ??= new InvocationDictionary();
		var returnInvocation3 = (Invocation)_returnInvocation3.GetOrAdd(typeof(T), static type => new Invocation($"IPrimitiveDependencyService.Return<{type.Name}>()"));
		return returnInvocation3.Verify(index, _invocationProviders);
	}

	// ReturnAsync
	private Setup<bool>? _returnAsync;
	private Invocation? _returnAsyncInvocation;

	public Setup<bool> SetupReturnAsync()
	{
		return _returnAsync ??= new Setup<bool>();
	}

	public void VerifyReturnAsync(in Times times)
	{
		_returnAsyncInvocation ??= new Invocation("IPrimitiveDependencyService.ReturnAsync()");
		_returnAsyncInvocation.Verify(times, _invocationProviders);
	}

	public long VerifyReturnAsync(in long index)
	{
		_returnAsyncInvocation ??= new Invocation("IPrimitiveDependencyService.ReturnAsync()");
		return _returnAsyncInvocation.Verify(index, _invocationProviders);
	}

	// ReturnWithParameter
	private SetupWithParameter<string, string>? _returnWithParameter1;
	private Invocation<string>? _returnWithParameterInvocation1;

	public SetupWithParameter<string, string> SetupReturnWithParameter(in It<string> parameter)
	{
		_returnWithParameter1 ??= new SetupWithParameter<string, string>();
		_returnWithParameter1.SetupParameter(parameter.ValueSetup);
		return _returnWithParameter1;
	}

	public void VerifyReturnWithParameter(in It<string> parameter, in Times times)
	{
		_returnWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService.ReturnWithParameter({0})");
		_returnWithParameterInvocation1.Verify(parameter.ValueSetup, times, _invocationProviders);
	}

	public long VerifyReturnWithParameter(in It<string> parameter, in long index)
	{
		_returnWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService.ReturnWithParameter({0})");
		return _returnWithParameterInvocation1.Verify(parameter.ValueSetup, index, _invocationProviders);
	}

	// ReturnWithParameter
	private SetupWithRefParameter<double, int>? _returnWithParameter2;
	private Invocation<double>? _returnWithParameterInvocation2;

	public SetupWithRefParameter<double, int> SetupReturnWithParameter(in ItRef<double> parameter)
	{
		_returnWithParameter2 ??= new SetupWithRefParameter<double, int>();
		_returnWithParameter2.SetupParameter(parameter.ValueSetup);
		return _returnWithParameter2;
	}

	public void VerifyReturnWithParameter(in ItRef<double> parameter, in Times times)
	{
		_returnWithParameterInvocation2 ??= new Invocation<double>("IPrimitiveDependencyService.ReturnWithParameter({0})", prefix: "ref");
		_returnWithParameterInvocation2.Verify(parameter.ValueSetup, times, _invocationProviders);
	}

	public long VerifyReturnWithParameter(in ItRef<double> parameter, in long index)
	{
		_returnWithParameterInvocation2 ??= new Invocation<double>("IPrimitiveDependencyService.ReturnWithParameter({0})", prefix: "ref");
		return _returnWithParameterInvocation2.Verify(parameter.ValueSetup, index, _invocationProviders);
	}

	// ReturnWithParameter
	private ConcurrentDictionary<(Type, Type), object>? _returnWithParameter3;
	private InvocationDictionary<(Type, Type)>? _returnWithParameterInvocation3;

	public SetupWithParameter<TParameter, TReturn> SetupReturnWithParameter<TParameter, TReturn>(in It<TParameter> parameter)
	{
		_returnWithParameter3 ??= new ConcurrentDictionary<(Type, Type), object>();
		var returnWithParameter3 = (SetupWithParameter<TParameter, TReturn>)_returnWithParameter3.GetOrAdd((typeof(TParameter), typeof(TReturn)), static _ => new SetupWithParameter<TParameter, TReturn>());
		returnWithParameter3.SetupParameter(parameter.ValueSetup);
		return returnWithParameter3;
	}

	public void VerifyReturnWithParameter<TParameter, TReturn>(in It<TParameter> parameter, in Times times)
	{
		_returnWithParameterInvocation3 ??= new InvocationDictionary<(Type, Type)>();
		var returnWithParameterInvocation3 = (Invocation<TParameter>)_returnWithParameterInvocation3.GetOrAdd((typeof(TParameter), typeof(TReturn)), static x => new Invocation<TParameter>($"IPrimitiveDependencyService.ReturnWithParameter<{x.Item1.Name}, {x.Item2.Name}>({{0}})"));
		returnWithParameterInvocation3.Verify(parameter.ValueSetup, times, _invocationProviders);
	}

	public long VerifyReturnWithParameter<TParameter, TReturn>(in It<TParameter> parameter, in long index)
	{
		_returnWithParameterInvocation3 ??= new InvocationDictionary<(Type, Type)>();
		var returnWithParameterInvocation3 = (Invocation<TParameter>)_returnWithParameterInvocation3.GetOrAdd((typeof(TParameter), typeof(TReturn)), static x => new Invocation<TParameter>($"IPrimitiveDependencyService.ReturnWithParameter<{x.Item1.Name}, {x.Item2.Name}>({{0}})"));
		return returnWithParameterInvocation3.Verify(parameter.ValueSetup, index, _invocationProviders);
	}

	// ReturnWithParameter
	private SetupWithParameter<string, int>? _returnWithParameterAsync;
	private Invocation<string>? _returnWithParameterAsyncInvocation;

	public SetupWithParameter<string, int> SetupReturnWithParameterAsync(in It<string> parameter)
	{
		_returnWithParameterAsync ??= new SetupWithParameter<string, int>();
		_returnWithParameterAsync.SetupParameter(parameter.ValueSetup);
		return _returnWithParameterAsync;
	}

	public void VerifyReturnWithParameterAsync(in It<string> parameter, in Times times)
	{
		_returnWithParameterAsyncInvocation ??= new Invocation<string>("IPrimitiveDependencyService.ReturnWithParameterAsync({0})");
		_returnWithParameterAsyncInvocation.Verify(parameter.ValueSetup, times, _invocationProviders);
	}

	public long VerifyReturnWithParameterAsync(in It<string> parameter, in long index)
	{
		_returnWithParameterAsyncInvocation ??= new Invocation<string>("IPrimitiveDependencyService.ReturnWithParameterAsync({0})");
		return _returnWithParameterAsyncInvocation.Verify(parameter.ValueSetup, index, _invocationProviders);
	}

	// ReturnWithSeveralParameters
	private SetupIntInt<decimal>? _returnWithSeveralParameters1;
	private InvocationIntInt? _returnWithSeveralParametersInvocation1;

	public SetupIntInt<decimal> SetupReturnWithSeveralParameters(in It<int> parameter1, in It<int> parameter2)
	{
		_returnWithSeveralParameters1 ??= new SetupIntInt<decimal>();
		_returnWithSeveralParameters1.SetupParameters(parameter1, parameter2);
		return _returnWithSeveralParameters1;
	}

	public void VerifyReturnWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in Times times)
	{
		_returnWithSeveralParametersInvocation1 ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParameters({0}, {1})");
		_returnWithSeveralParametersInvocation1.Verify(parameter1.ValueSetup, parameter2.ValueSetup, times, _invocationProviders);
	}

	public long VerifyReturnWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in long index)
	{
		_returnWithSeveralParametersInvocation1 ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParameters({0}, {1})");
		return _returnWithSeveralParametersInvocation1.Verify(parameter1.ValueSetup, parameter2.ValueSetup, index, _invocationProviders);
	}

	// ReturnWithSeveralParameters
	private SetupRefIntInt<decimal>? _returnWithSeveralParameters2;
	private InvocationIntInt? _returnWithSeveralParametersInvocation2;

	public SetupRefIntInt<decimal> SetupReturnWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2)
	{
		_returnWithSeveralParameters2 ??= new SetupRefIntInt<decimal>();
		_returnWithSeveralParameters2.SetupParameters(parameter1.ValueSetup, parameter2.ValueSetup);
		return _returnWithSeveralParameters2;
	}

	public void VerifyReturnWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2, in Times times)
	{
		_returnWithSeveralParametersInvocation2 ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParameters({0}, {1})", prefix1: "ref");
		_returnWithSeveralParametersInvocation2.Verify(parameter1.ValueSetup, parameter2.ValueSetup, times, _invocationProviders);
	}

	public long VerifyReturnWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2, in long index)
	{
		_returnWithSeveralParametersInvocation2 ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParameters({0}, {1})", prefix1: "ref");
		return _returnWithSeveralParametersInvocation2.Verify(parameter1.ValueSetup, parameter2.ValueSetup, index, _invocationProviders);
	}

	// ReturnWithSeveralParameters
	private SetupIntRefInt<decimal>? _returnWithSeveralParameters3;
	private InvocationIntInt? _returnWithSeveralParametersInvocation3;

	public SetupIntRefInt<decimal> SetupReturnWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2)
	{
		_returnWithSeveralParameters3 ??= new SetupIntRefInt<decimal>();
		_returnWithSeveralParameters3.SetupParameters(parameter1.ValueSetup, parameter2.ValueSetup);
		return _returnWithSeveralParameters3;
	}

	public void VerifyReturnWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2, in Times times)
	{
		_returnWithSeveralParametersInvocation3 ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParameters({0}, {1})", prefix2: "ref");
		_returnWithSeveralParametersInvocation3.Verify(parameter1.ValueSetup, parameter2.ValueSetup, times, _invocationProviders);
	}

	public long VerifyReturnWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2, in long index)
	{
		_returnWithSeveralParametersInvocation3 ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParameters({0}, {1})", prefix2: "ref");
		return _returnWithSeveralParametersInvocation3.Verify(parameter1.ValueSetup, parameter2.ValueSetup, index, _invocationProviders);
	}

	// ReturnWithSeveralParameters
	private SetupRefIntRefInt<decimal>? _returnWithSeveralParameters4;
	private InvocationIntInt? _returnWithSeveralParametersInvocation4;

	public SetupRefIntRefInt<decimal> SetupReturnWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2)
	{
		_returnWithSeveralParameters4 ??= new SetupRefIntRefInt<decimal>();
		_returnWithSeveralParameters4.SetupParameters(parameter1.ValueSetup, parameter2.ValueSetup);
		return _returnWithSeveralParameters4;
	}

	public void VerifyReturnWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2, in Times times)
	{
		_returnWithSeveralParametersInvocation4 ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParameters({0}, {1})", prefix1: "ref", prefix2: "ref");
		_returnWithSeveralParametersInvocation4.Verify(parameter1.ValueSetup, parameter2.ValueSetup, times, _invocationProviders);
	}

	public long VerifyReturnWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2, in long index)
	{
		_returnWithSeveralParametersInvocation4 ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParameters({0}, {1})", prefix1: "ref", prefix2: "ref");
		return _returnWithSeveralParametersInvocation4.Verify(parameter1.ValueSetup, parameter2.ValueSetup, index, _invocationProviders);
	}

	// ReturnWithSeveralParameters
	private ConcurrentDictionary<(Type, Type, Type), object>? _returnWithSeveralParameters5;
	private InvocationDictionary<(Type, Type, Type)>? _returnWithSeveralParametersInvocation5;

	public SetupRefT1T2<TParameter1, TParameter2, TReturn> SetupReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(in ItRef<TParameter1> parameter1, in It<TParameter2> parameter2)
	{
		var key = (typeof(TParameter1), typeof(TParameter2), typeof(TReturn));
		_returnWithSeveralParameters5 ??= new ConcurrentDictionary<(Type, Type, Type), object>();
		var returnWithSeveralParameters5 = (SetupRefT1T2<TParameter1, TParameter2, TReturn>)_returnWithSeveralParameters5.GetOrAdd(key, static _ => new SetupRefT1T2<TParameter1, TParameter2, TReturn>());
		returnWithSeveralParameters5.SetupParameters(parameter1.ValueSetup, parameter2.ValueSetup);
		return returnWithSeveralParameters5;
	}

	public void VerifyReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(in ItRef<TParameter1> parameter1, in It<TParameter2> parameter2, in Times times)
	{
		var key = (typeof(TParameter1), typeof(TParameter2), typeof(TReturn));
		_returnWithSeveralParametersInvocation5 ??= new InvocationDictionary<(Type, Type, Type)>();
		var returnWithSeveralParametersInvocation5 = (InvocationT1T2<TParameter1, TParameter2>)_returnWithSeveralParametersInvocation5.GetOrAdd(key, static x => new InvocationT1T2<TParameter1, TParameter2>($"IPrimitiveDependencyService.ReturnWithSeveralParameters<{x.Item1.Name}, {x.Item2.Name}, {x.Item3.Name}>({{0}}, {{1}})", prefix1: "ref"));
		returnWithSeveralParametersInvocation5.Verify(parameter1.ValueSetup, parameter2.ValueSetup, times, _invocationProviders);
	}

	public long VerifyReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(in ItRef<TParameter1> parameter1, in It<TParameter2> parameter2, in long index)
	{
		var key = (typeof(TParameter1), typeof(TParameter2), typeof(TReturn));
		_returnWithSeveralParametersInvocation5 ??= new InvocationDictionary<(Type, Type, Type)>();
		var returnWithSeveralParametersInvocation5 = (InvocationT1T2<TParameter1, TParameter2>)_returnWithSeveralParametersInvocation5.GetOrAdd(key, static x => new InvocationT1T2<TParameter1, TParameter2>($"IPrimitiveDependencyService.ReturnWithSeveralParameters<{x.Item1.Name}, {x.Item2.Name}, {x.Item3.Name}>({{0}}, {{1}})", prefix1: "ref"));
		return returnWithSeveralParametersInvocation5.Verify(parameter1.ValueSetup, parameter2.ValueSetup, index, _invocationProviders);
	}

	// ReturnWithSeveralParametersAsync
	private SetupIntInt<decimal>? _returnWithSeveralParametersAsync;
	private InvocationIntInt? _returnWithSeveralParametersAsyncInvocation;

	public SetupIntInt<decimal> SetupReturnWithSeveralParametersAsync(in It<int> parameter1, in It<int> parameter2)
	{
		_returnWithSeveralParametersAsync ??= new SetupIntInt<decimal>();
		_returnWithSeveralParametersAsync.SetupParameters(parameter1, parameter2);
		return _returnWithSeveralParametersAsync;
	}

	public void VerifyReturnWithSeveralParametersAsync(in It<int> parameter1, in It<int> parameter2, in Times times)
	{
		_returnWithSeveralParametersAsyncInvocation ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParametersAsync({0}, {1})");
		_returnWithSeveralParametersAsyncInvocation.Verify(parameter1.ValueSetup, parameter2.ValueSetup, times, _invocationProviders);
	}

	public long VerifyReturnWithSeveralParametersAsync(in It<int> parameter1, in It<int> parameter2, in long index)
	{
		_returnWithSeveralParametersAsyncInvocation ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParametersAsync({0}, {1})");
		return _returnWithSeveralParametersAsyncInvocation.Verify(parameter1.ValueSetup, parameter2.ValueSetup, index, _invocationProviders);
	}

	public void VerifyNoOtherCalls()
	{
		_handlerAddInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_handlerRemoveInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_handlerEventAddInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_handlerEventRemoveInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_getOnlyGetInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_setOnlySetInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_getInitGetInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_getInitSetInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_invokeInvocation1?.VerifyNoOtherCalls(_invocationProviders);
		_invokeInvocation2?.VerifyNoOtherCalls(_invocationProviders);
		_invokeInvocation3?.VerifyNoOtherCalls(_invocationProviders);
		_invokeAsyncInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_invokeWithParameterInvocation1?.VerifyNoOtherCalls(_invocationProviders);
		_invokeWithParameterInvocation2?.VerifyNoOtherCalls(_invocationProviders);
		_invokeWithParameterInvocation3?.VerifyNoOtherCalls(_invocationProviders);
		_invokeWithParameterInvocation4?.VerifyNoOtherCalls(_invocationProviders);
		_invokeWithParameterAsyncInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_invokeWithSeveralParametersInvocation1?.VerifyNoOtherCalls(_invocationProviders);
		_invokeWithSeveralParametersInvocation2?.VerifyNoOtherCalls(_invocationProviders);
		_invokeWithSeveralParametersInvocation3?.VerifyNoOtherCalls(_invocationProviders);
		_invokeWithSeveralParametersInvocation4?.VerifyNoOtherCalls(_invocationProviders);
		_invokeWithSeveralParametersInvocation5?.VerifyNoOtherCalls(_invocationProviders);
		_invokeWithSeveralParametersAsyncInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_returnInvocation1?.VerifyNoOtherCalls(_invocationProviders);
		_returnInvocation2?.VerifyNoOtherCalls(_invocationProviders);
		_returnInvocation3?.VerifyNoOtherCalls(_invocationProviders);
		_returnAsyncInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_returnWithParameterInvocation1?.VerifyNoOtherCalls(_invocationProviders);
		_returnWithParameterInvocation2?.VerifyNoOtherCalls(_invocationProviders);
		_returnWithParameterInvocation3?.VerifyNoOtherCalls(_invocationProviders);
		_returnWithParameterAsyncInvocation?.VerifyNoOtherCalls(_invocationProviders);
		_returnWithSeveralParametersInvocation1?.VerifyNoOtherCalls(_invocationProviders);
		_returnWithSeveralParametersInvocation2?.VerifyNoOtherCalls(_invocationProviders);
		_returnWithSeveralParametersInvocation3?.VerifyNoOtherCalls(_invocationProviders);
		_returnWithSeveralParametersInvocation4?.VerifyNoOtherCalls(_invocationProviders);
		_returnWithSeveralParametersInvocation5?.VerifyNoOtherCalls(_invocationProviders);
		_returnWithSeveralParametersAsyncInvocation?.VerifyNoOtherCalls(_invocationProviders);
	}

	private IEnumerable<IInvocationProvider?> GetInvocations()
	{
		yield return _handlerAddInvocation;
		yield return _handlerRemoveInvocation;
		yield return _handlerEventAddInvocation;
		yield return _handlerEventRemoveInvocation;
		yield return _getOnlyGetInvocation;
		yield return _setOnlySetInvocation;
		yield return _getInitGetInvocation;
		yield return _getInitSetInvocation;
		yield return _invokeInvocation1;
		yield return _invokeInvocation2;
		yield return _invokeInvocation3;
		yield return _invokeAsyncInvocation;
		yield return _invokeWithParameterInvocation1;
		yield return _invokeWithParameterInvocation2;
		yield return _invokeWithParameterInvocation3;
		yield return _invokeWithParameterInvocation4;
		yield return _invokeWithParameterAsyncInvocation;
		yield return _invokeWithSeveralParametersInvocation1;
		yield return _invokeWithSeveralParametersInvocation2;
		yield return _invokeWithSeveralParametersInvocation3;
		yield return _invokeWithSeveralParametersInvocation4;
		yield return _invokeWithSeveralParametersInvocation5;
		yield return _invokeWithSeveralParametersAsyncInvocation;
		yield return _returnInvocation1;
		yield return _returnInvocation2;
		yield return _returnInvocation3;
		yield return _returnAsyncInvocation;
		yield return _returnWithParameterInvocation1;
		yield return _returnWithParameterInvocation2;
		yield return _returnWithParameterInvocation3;
		yield return _returnWithParameterAsyncInvocation;
		yield return _returnWithSeveralParametersInvocation1;
		yield return _returnWithSeveralParametersInvocation2;
		yield return _returnWithSeveralParametersInvocation3;
		yield return _returnWithSeveralParametersInvocation4;
		yield return _returnWithSeveralParametersInvocation5;
		yield return _returnWithSeveralParametersAsyncInvocation;
	}

	private sealed class Proxy : IPrimitiveDependencyService
	{
		private readonly PrimitiveDependencyServiceMock _mock;

		public Proxy(PrimitiveDependencyServiceMock mock)
		{
			_mock = mock;
		}

		public event PrimitiveHandler? Handler
		{
			add
			{
				_mock._handlerAddInvocation ??= new Invocation<PrimitiveHandler?>("IPrimitiveDependencyService.Handler.add");
				_mock._handlerAddInvocation.Register(_mock._invocationIndex, value);
				_mock._handler += value;
			}
			remove
			{
				_mock._handlerRemoveInvocation ??= new Invocation<PrimitiveHandler?>("IPrimitiveDependencyService.Handler.remove");
				_mock._handlerRemoveInvocation.Register(_mock._invocationIndex, value);
				_mock._handler -= value;
			}
		}

		public event EventHandler<string>? HandlerEvent
		{
			add
			{
				_mock._handlerEventAddInvocation ??= new Invocation<EventHandler<string>?>("IPrimitiveDependencyService.HandlerEvent.add");
				_mock._handlerEventAddInvocation.Register(_mock._invocationIndex, value);
				_mock._handlerEvent += value;
			}
			remove
			{
				_mock._handlerEventRemoveInvocation ??= new Invocation<EventHandler<string>?>("IPrimitiveDependencyService.HandlerEvent.remove");
				_mock._handlerEventRemoveInvocation.Register(_mock._invocationIndex, value);
				_mock._handlerEvent -= value;
			}
		}

		public int GetOnly
		{
			get
			{
				_mock._getOnlyGetInvocation ??= new Invocation("IPrimitiveDependencyService.GetOnly.get");
				_mock._getOnlyGetInvocation.Register(_mock._invocationIndex);
				return _mock._getOnlyGet?.Execute(out var returnValue) == true ? returnValue : 0;
			}
		}

		public decimal SetOnly
		{
			set
			{
				_mock._setOnlySetInvocation ??= new Invocation<decimal>("IPrimitiveDependencyService.SetOnly.set = {0}");
				_mock._setOnlySetInvocation.Register(_mock._invocationIndex, value);
				_mock._setOnlySet?.Invoke(value);
			}
		}

		public string GetInit
		{
			get
			{
				_mock._getInitGetInvocation ??= new Invocation("IPrimitiveDependencyService.GetInit.get");
				_mock._getInitGetInvocation.Register(_mock._invocationIndex);
				return _mock._getInitGet?.Execute(out var returnValue) == true ? returnValue! : string.Empty;
			}
			set
			{
				_mock._getInitSetInvocation ??= new Invocation<string>("IPrimitiveDependencyService.GetInit.set = {0}");
				_mock._getInitSetInvocation.Register(_mock._invocationIndex, value);
				_mock._getInitSet?.Invoke(value);
			}
		}

		public void Invoke()
		{
			_mock._invokeInvocation1 ??= new Invocation("IPrimitiveDependencyService.Invoke()");
			_mock._invokeInvocation1.Register(_mock._invocationIndex);
			_mock._invoke1?.Invoke();
		}

		public void Invoke(out int result)
		{
			_mock._invokeInvocation2 ??= new Invocation<int>("IPrimitiveDependencyService.Invoke({0})", prefix: "out");
			_mock._invokeInvocation2.Register(_mock._invocationIndex, 0);
			if (_mock._invoke2 is not null)
				_mock._invoke2.Invoke(out result);
			else
				result = 0;
		}

		public void Invoke<T>()
		{
			_mock._invokeInvocation3 ??= new InvocationDictionary();
			var invokeInvocation3 = (Invocation)_mock._invokeInvocation3.GetOrAdd(typeof(T), static type => new Invocation($"IPrimitiveDependencyService.Invoke<{type.Name}>()"));
			invokeInvocation3.Register(_mock._invocationIndex);
			_mock._invoke3?.GetValueOrDefault(typeof(T))?.Invoke();
		}

		public Task InvokeAsync()
		{
			_mock._invokeAsyncInvocation ??= new Invocation("IPrimitiveDependencyService.InvokeAsync()");
			_mock._invokeAsyncInvocation.Register(_mock._invocationIndex);
			_mock._invokeAsync?.Invoke();
			return Task.CompletedTask;
		}

		public void InvokeWithParameter(in string parameter)
		{
			_mock._invokeWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService.InvokeWithParameter({0})");
			_mock._invokeWithParameterInvocation1.Register(_mock._invocationIndex, parameter);
			_mock._invokeWithParameter1?.Invoke(parameter);
		}

		public void InvokeWithParameter(in int parameter)
		{
			_mock._invokeWithParameterInvocation2 ??= new Invocation<int>("IPrimitiveDependencyService.InvokeWithParameter({0})");
			_mock._invokeWithParameterInvocation2.Register(_mock._invocationIndex, parameter);
			_mock._invokeWithParameter2?.Invoke(parameter);
		}

		public void InvokeWithParameter(ref decimal parameter)
		{
			_mock._invokeWithParameterInvocation3 ??= new Invocation<decimal>("IPrimitiveDependencyService.InvokeWithParameter({0})", prefix: "ref");
			_mock._invokeWithParameterInvocation3.Register(_mock._invocationIndex, parameter);
			_mock._invokeWithParameter3?.Invoke(ref parameter);
		}

		public void InvokeWithParameter<T>(T parameter)
		{
			_mock._invokeWithParameterInvocation4 ??= new InvocationDictionary();
			var invokeWithParameterInvocation4 = (Invocation<T>)_mock._invokeWithParameterInvocation4.GetOrAdd(typeof(T), static type => new Invocation<T>($"IPrimitiveDependencyService.InvokeWithParameter<{type.Name}>({{0}})"));
			invokeWithParameterInvocation4.Register(_mock._invocationIndex, parameter);
			((SetupWithParameter<T>?)_mock._invokeWithParameter4?.GetValueOrDefault(typeof(T)))?.Invoke(parameter);
		}

		public Task InvokeWithParameterAsync(int parameter)
		{
			_mock._invokeWithParameterAsyncInvocation ??= new Invocation<int>("IPrimitiveDependencyService.InvokeWithParameterAsync({0})");
			_mock._invokeWithParameterAsyncInvocation.Register(_mock._invocationIndex, parameter);
			_mock._invokeWithParameterAsync?.Invoke(parameter);
			return Task.CompletedTask;
		}

		public void InvokeWithSeveralParameters(int parameter1, int parameter2)
		{
			_mock._invokeWithSeveralParametersInvocation1 ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParameters({0}, {1})");
			_mock._invokeWithSeveralParametersInvocation1.Register(_mock._invocationIndex, parameter1, parameter2);
			_mock._invokeWithSeveralParameters1?.Invoke(parameter1, parameter2);
		}

		public void InvokeWithSeveralParameters(ref int parameter1, int parameter2)
		{
			_mock._invokeWithSeveralParametersInvocation2 ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParameters({0}, {1})", prefix1: "ref");
			_mock._invokeWithSeveralParametersInvocation2.Register(_mock._invocationIndex, parameter1, parameter2);
			_mock._invokeWithSeveralParameters2?.Invoke(ref parameter1, parameter2);
		}

		public void InvokeWithSeveralParameters(int parameter1, ref int parameter2)
		{
			_mock._invokeWithSeveralParametersInvocation3 ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParameters({0}, {1})", prefix2: "ref");
			_mock._invokeWithSeveralParametersInvocation3.Register(_mock._invocationIndex, parameter1, parameter2);
			_mock._invokeWithSeveralParameters3?.Invoke(parameter1, ref parameter2);
		}

		public void InvokeWithSeveralParameters(ref int parameter1, ref int parameter2)
		{
			_mock._invokeWithSeveralParametersInvocation4 ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParameters({0}, {1})", prefix1: "ref", prefix2: "ref");
			_mock._invokeWithSeveralParametersInvocation4.Register(_mock._invocationIndex, parameter1, parameter2);
			_mock._invokeWithSeveralParameters4?.Invoke(ref parameter1, ref parameter2);
		}

		public void InvokeWithSeveralParameters<T>(T parameter1, int parameter2)
		{
			_mock._invokeWithSeveralParametersInvocation5 ??= new InvocationDictionary();
			var invokeWithSeveralParametersInvocation5 = (InvocationT1Int<T>)_mock._invokeWithSeveralParametersInvocation5.GetOrAdd(typeof(T), static type => new InvocationT1Int<T>($"IPrimitiveDependencyService.InvokeWithSeveralParameters<{type.Name}>({{0}}, {{1}})"));
			invokeWithSeveralParametersInvocation5.Register(_mock._invocationIndex, parameter1, parameter2);
			((SetupT1Int<T>?)_mock._invokeWithSeveralParameters5?.GetValueOrDefault(typeof(T)))?.Invoke(parameter1, parameter2);
		}

		public Task InvokeWithSeveralParametersAsync(int parameter1, int parameter2)
		{
			_mock._invokeWithSeveralParametersAsyncInvocation ??= new InvocationIntInt("IPrimitiveDependencyService.InvokeWithSeveralParametersAsync({0}, {1})");
			_mock._invokeWithSeveralParametersAsyncInvocation.Register(_mock._invocationIndex, parameter1, parameter2);
			_mock._invokeWithSeveralParametersAsync?.Invoke(parameter1, parameter2);
			return Task.CompletedTask;
		}

		public int Return()
		{
			_mock._returnInvocation1 ??= new Invocation("IPrimitiveDependencyService.Return()");
			_mock._returnInvocation1.Register(_mock._invocationIndex);
			return _mock._return1?.Execute(out var returnValue) == true ? returnValue : 0;
		}

		public bool Return(out string result)
		{
			_mock._returnInvocation2 ??= new Invocation<string>("IPrimitiveDependencyService.Return({0})", prefix: "out");
			_mock._returnInvocation2.Register(_mock._invocationIndex, null!);
			if (_mock._return2 is not null)
				return _mock._return2.Execute(out result!, out var returnValue) ? returnValue : false;

			result = null!;
			return false;
		}

		public T Return<T>()
		{
			_mock._returnInvocation3 ??= new InvocationDictionary();
			var returnInvocation3 = (Invocation)_mock._returnInvocation3.GetOrAdd(typeof(T), static type => new Invocation($"IPrimitiveDependencyService.Return<{type.Name}>()"));
			returnInvocation3.Register(_mock._invocationIndex);
			return ((Setup<T>?)_mock._return3?.GetValueOrDefault(typeof(T)))?.Execute(out var returnValue) == true ? returnValue! : default!;
		}

		public ValueTask<bool> ReturnAsync()
		{
			_mock._returnAsyncInvocation ??= new Invocation("IPrimitiveDependencyService.ReturnAsync()");
			_mock._returnAsyncInvocation.Register(_mock._invocationIndex);
			return _mock._returnAsync?.Execute(out var returnValue) == true ? ValueTask.FromResult(returnValue) : ValueTask.FromResult(false);
		}

		public string ReturnWithParameter(in string parameter)
		{
			_mock._returnWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService.ReturnWithParameter({0})");
			_mock._returnWithParameterInvocation1.Register(_mock._invocationIndex, parameter);
			return _mock._returnWithParameter1?.Execute(parameter, out var returnValue) == true ? returnValue! : string.Empty;
		}

		public int ReturnWithParameter(ref double parameter)
		{
			_mock._returnWithParameterInvocation2 ??= new Invocation<double>("IPrimitiveDependencyService.ReturnWithParameter({0})", prefix: "ref");
			_mock._returnWithParameterInvocation2.Register(_mock._invocationIndex, parameter);
			return _mock._returnWithParameter2?.Execute(ref parameter, out var returnValue) == true ? returnValue! : 0;
		}

		public TReturn ReturnWithParameter<TParameter, TReturn>(TParameter parameter)
		{
			var key = (typeof(TParameter), typeof(TReturn));
			_mock._returnWithParameterInvocation3 ??= new InvocationDictionary<(Type, Type)>();
			var returnWithParameterInvocation3 = (Invocation<TParameter>)_mock._returnWithParameterInvocation3.GetOrAdd(key, static x => new Invocation<TParameter>($"IPrimitiveDependencyService.ReturnWithParameter<{x.Item1.Name}, {x.Item2.Name}>({{0}})"));
			returnWithParameterInvocation3.Register(_mock._invocationIndex, parameter);
			return ((SetupWithParameter<TParameter, TReturn>?)_mock._returnWithParameter3?.GetValueOrDefault(key))?.Execute(parameter, out var returnValue) == true ? returnValue! : default!;
		}

		public ValueTask<int> ReturnWithParameterAsync(string parameter)
		{
			_mock._returnWithParameterAsyncInvocation ??= new Invocation<string>("IPrimitiveDependencyService.ReturnWithParameterAsync({0})");
			_mock._returnWithParameterAsyncInvocation.Register(_mock._invocationIndex, parameter);
			return _mock._returnWithParameterAsync?.Execute(parameter, out var returnValue) == true ? ValueTask.FromResult(returnValue) : ValueTask.FromResult(0);
		}

		public decimal ReturnWithSeveralParameters(int parameter1, int parameter2)
		{
			_mock._returnWithSeveralParametersInvocation1 ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParameters({0}, {1})");
			_mock._returnWithSeveralParametersInvocation1.Register(_mock._invocationIndex, parameter1, parameter2);
			return _mock._returnWithSeveralParameters1?.Execute(parameter1, parameter2, out var returnValue) == true ? returnValue : 0m;
		}

		public decimal ReturnWithSeveralParameters(ref int parameter1, int parameter2)
		{
			_mock._returnWithSeveralParametersInvocation2 ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParameters({0}, {1})", prefix1: "ref");
			_mock._returnWithSeveralParametersInvocation2.Register(_mock._invocationIndex, parameter1, parameter2);
			return _mock._returnWithSeveralParameters2?.Execute(ref parameter1, parameter2, out var returnValue) == true ? returnValue : 0m;
		}

		public decimal ReturnWithSeveralParameters(int parameter1, ref int parameter2)
		{
			_mock._returnWithSeveralParametersInvocation3 ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParameters({0}, {1})", prefix2: "ref");
			_mock._returnWithSeveralParametersInvocation3.Register(_mock._invocationIndex, parameter1, parameter2);
			return _mock._returnWithSeveralParameters3?.Execute(parameter1, ref parameter2, out var returnValue) == true ? returnValue : 0m;
		}

		public decimal ReturnWithSeveralParameters(ref int parameter1, ref int parameter2)
		{
			_mock._returnWithSeveralParametersInvocation4 ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParameters({0}, {1})", prefix1: "ref", prefix2: "ref");
			_mock._returnWithSeveralParametersInvocation4.Register(_mock._invocationIndex, parameter1, parameter2);
			return _mock._returnWithSeveralParameters4?.Execute(ref parameter1, ref parameter2, out var returnValue) == true ? returnValue : 0m;
		}

		public TReturn ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref TParameter1 parameter1, TParameter2 parameter2)
		{
			var key = (typeof(TParameter1), typeof(TParameter2), typeof(TReturn));
			_mock._returnWithSeveralParametersInvocation5 ??= new InvocationDictionary<(Type, Type, Type)>();
			var returnWithParameterInvocation5 = (InvocationT1T2<TParameter1, TParameter2>)_mock._returnWithSeveralParametersInvocation5.GetOrAdd(key, static x => new InvocationT1T2<TParameter1, TParameter2>($"IPrimitiveDependencyService.ReturnWithSeveralParameters<{x.Item1.Name}, {x.Item2.Name}, {x.Item3.Name}>({{0}}, {{1}})", prefix1: "ref"));
			returnWithParameterInvocation5.Register(_mock._invocationIndex, parameter1, parameter2);
			return ((SetupRefT1T2<TParameter1, TParameter2, TReturn>?)_mock._returnWithSeveralParameters5?.GetValueOrDefault(key))?.Execute(ref parameter1, parameter2, out var returnValue) == true ? returnValue! : default!;
		}

		public ValueTask<decimal> ReturnWithSeveralParametersAsync(int parameter1, int parameter2)
		{
			_mock._returnWithSeveralParametersAsyncInvocation ??= new InvocationIntInt("IPrimitiveDependencyService.ReturnWithSeveralParametersAsync({0}, {1})");
			_mock._returnWithSeveralParametersAsyncInvocation.Register(_mock._invocationIndex, parameter1, parameter2);
			return _mock._returnWithSeveralParametersAsync?.Execute(parameter1, parameter2, out var returnValue) == true ? ValueTask.FromResult(returnValue) : ValueTask.FromResult(0m);
		}
	}
}

public static partial class MockExtensions
{
	extension(IMock<IPrimitiveDependencyService> @this)
	{
		// Handler
		public void RaiseHandler(in int value) =>
			((PrimitiveDependencyServiceMock)@this).RaiseHandler(value);

		public void VerifyAddHandler(in It<PrimitiveHandler?> handler, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyAddHandler(handler, times);

		public void VerifyAddHandler(in It<PrimitiveHandler?> handler, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyAddHandler(handler, times());

		public void VerifyRemoveHandler(in It<PrimitiveHandler?> handler, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyRemoveHandler(handler, times);

		public void VerifyRemoveHandler(in It<PrimitiveHandler?> handler, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyRemoveHandler(handler, times());

		// HandlerEvent
		public void RaiseHandlerEvent(in string value) =>
			((PrimitiveDependencyServiceMock)@this).RaiseHandlerEvent(value);

		public void VerifyAddHandlerEvent(in It<EventHandler<string>?> handler, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyAddHandlerEvent(handler, times);

		public void VerifyAddHandlerEvent(in It<EventHandler<string>?> handler, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyAddHandlerEvent(handler, times());

		public void VerifyRemoveHandlerEvent(in It<EventHandler<string>?> handler, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyRemoveHandlerEvent(handler, times);

		public void VerifyRemoveHandlerEvent(in It<EventHandler<string>?> handler, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyRemoveHandlerEvent(handler, times());

		// GetOnly
		public ISetup<Action, int, Func<int>> SetupGetGetOnly() =>
			((PrimitiveDependencyServiceMock)@this).SetupGetGetOnly();

		public void VerifyGetGetOnly(in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyGetGetOnly(times);

		public void VerifyGetGetOnly(in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyGetGetOnly(times());

		// SetOnly
		public ISetup<Action<decimal>> SetupSetSetOnly(in It<decimal> value = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupSetSetOnly(value);

		public void VerifySetSetOnly(in It<decimal> value, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifySetSetOnly(value, times);

		public void VerifySetSetOnly(in It<decimal> value, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifySetSetOnly(value, times());

		// GetInit
		public ISetup<Action, string, Func<string?>> SetupGetGetInit() =>
			((PrimitiveDependencyServiceMock)@this).SetupGetGetInit();

		public void VerifyGetGetInit(in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyGetGetInit(times);

		public void VerifyGetGetInit(in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyGetGetInit(times());

		public ISetup<Action<string>> SetupSetGetInit(in It<string> value = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupSetGetInit(value);

		public void VerifySetGetInit(in It<string> value, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifySetGetInit(value, times);

		public void VerifySetGetInit(in It<string> value, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifySetGetInit(value, times());

		// Invoke
		public ISetup<Action> SetupInvoke() =>
			((PrimitiveDependencyServiceMock)@this).SetupInvoke();

		public void VerifyInvoke(in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvoke(times);

		public void VerifyInvoke(in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvoke(times());

		// Invoke
		public ISetup<ActionOut<int>> SetupInvoke(in ItOut<int> result) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvoke(result);

		public void VerifyInvoke(in ItOut<int> result, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvoke(result, times);

		public void VerifyInvoke(in ItOut<int> result, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvoke(result, times());

		// Invoke
		public ISetup<Action> SetupInvoke<T>() =>
			((PrimitiveDependencyServiceMock)@this).SetupInvoke<T>();

		public void VerifyInvoke<T>(in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvoke<T>(times);

		public void VerifyInvoke<T>(in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvoke<T>(times());

		// InvokeAsync
		public ISetup<Action> SetupInvokeAsync() =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeAsync();

		public void VerifyInvokeAsync(in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeAsync(times);

		public void VerifyInvokeAsync(in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeAsync(times());

		// InvokeWithParameter
		public ISetup<Action<string>> SetupInvokeWithParameter(in It<string> parameter = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);

		public void VerifyInvokeWithParameter(in It<string> parameter, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times);

		public void VerifyInvokeWithParameter(in It<string> parameter, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times());

		// InvokeWithParameter
		public ISetup<Action<int>> SetupInvokeWithParameter(in It<int> parameter = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);

		public void VerifyInvokeWithParameter(in It<int> parameter, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times);

		public void VerifyInvokeWithParameter(in It<int> parameter, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times());

		// InvokeWithParameter
		public ISetup<ActionRef<decimal>> SetupInvokeWithParameter(in ItRef<decimal> parameter = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);

		public ISetup<ActionRef<decimal>> SetupInvokeWithParameter(ref decimal parameter) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(ItRef<decimal>.Value(parameter));

		public void VerifyInvokeWithParameter(in ItRef<decimal> parameter, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times);

		public void VerifyInvokeWithParameter(in ItRef<decimal> parameter, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times());

		public void VerifyInvokeWithParameter(ref decimal parameter, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(ItRef<decimal>.Value(parameter), times);

		public void VerifyInvokeWithParameter(ref decimal parameter, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(ItRef<decimal>.Value(parameter), times());

		// InvokeWithParameter
		public ISetup<Action<T>> SetupInvokeWithParameter<T>(in It<T> parameter = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);

		public void VerifyInvokeWithParameter<T>(in It<T> parameter, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times);

		public void VerifyInvokeWithParameter<T>(in It<T> parameter, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times());

		// InvokeWithParameterAsync
		public ISetup<Action<int>> SetupInvokeWithParameterAsync(in It<int> parameter = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameterAsync(parameter);

		public void VerifyInvokeWithParameterAsync(in It<int> parameter, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameterAsync(parameter, times);

		public void VerifyInvokeWithParameterAsync(in It<int> parameter, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameterAsync(parameter, times());

		// InvokeWithSeveralParameters
		public ISetup<Action<int, int>> SetupInvokeWithSeveralParameters(in It<int> parameter1 = default, in It<int> parameter2 = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithSeveralParameters(parameter1, parameter2);

		public void VerifyInvokeWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times);

		public void VerifyInvokeWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times());

		// InvokeWithSeveralParameters
		public ISetup<SetupRefIntInt.CallbackDelegate> SetupInvokeWithSeveralParameters(in ItRef<int> parameter1 = default, in It<int> parameter2 = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithSeveralParameters(parameter1, parameter2);

		public SetupRefIntInt SetupInvokeWithSeveralParameters(ref int parameter1, in It<int> parameter2 = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithSeveralParameters(ItRef<int>.Value(parameter1), parameter2);

		public void VerifyInvokeWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times);

		public void VerifyInvokeWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times());

		public void VerifyInvokeWithSeveralParameters(ref int parameter1, in It<int> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(ItRef<int>.Value(parameter1), parameter2, times);

		public void VerifyInvokeWithSeveralParameters(ref int parameter1, in It<int> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(ItRef<int>.Value(parameter1), parameter2, times());

		// InvokeWithSeveralParameters
		public ISetup<SetupIntRefInt.CallbackDelegate> SetupInvokeWithSeveralParameters(in It<int> parameter1 = default, in ItRef<int> parameter2 = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithSeveralParameters(parameter1, parameter2);

		public ISetup<SetupIntRefInt.CallbackDelegate> SetupInvokeWithSeveralParameters(It<int> parameter1, ref int parameter2) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithSeveralParameters(parameter1, ItRef<int>.Value(parameter2));

		public void VerifyInvokeWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times);

		public void VerifyInvokeWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times());

		public void VerifyInvokeWithSeveralParameters(in It<int> parameter1, ref int parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, ItRef<int>.Value(parameter2), times);

		public void VerifyInvokeWithSeveralParameters(in It<int> parameter1, ref int parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, ItRef<int>.Value(parameter2), times());

		// InvokeWithSeveralParameters
		public ISetup<SetupRefIntRefInt.CallbackDelegate> SetupInvokeWithSeveralParameters(in ItRef<int> parameter1 = default, in ItRef<int> parameter2 = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithSeveralParameters(parameter1, parameter2);

		public ISetup<SetupRefIntRefInt.CallbackDelegate> SetupInvokeWithSeveralParameters(ref int parameter1, ref int parameter2) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithSeveralParameters(ItRef<int>.Value(parameter1), ItRef<int>.Value(parameter2));

		public void VerifyInvokeWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times);

		public void VerifyInvokeWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times());

		public void VerifyInvokeWithSeveralParameters(ref int parameter1, ref int parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(ItRef<int>.Value(parameter1), ItRef<int>.Value(parameter2), times);

		public void VerifyInvokeWithSeveralParameters(ref int parameter1, ref int parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(ItRef<int>.Value(parameter1), ItRef<int>.Value(parameter2), times());

		// InvokeWithSeveralParameters
		public ISetup<Action<T, int>> SetupInvokeWithSeveralParameters<T>(in It<T> parameter1 = default, in It<int> parameter2 = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithSeveralParameters(parameter1, parameter2);

		public void VerifyInvokeWithSeveralParameters<T>(in It<T> parameter1, in It<int> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times);

		public void VerifyInvokeWithSeveralParameters<T>(in It<T> parameter1, in It<int> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times());

		// InvokeWithSeveralParameters
		public ISetup<Action<int, int>> SetupInvokeWithSeveralParametersAsync(in It<int> parameter1 = default, in It<int> parameter2 = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupInvokeWithSeveralParametersAsync(parameter1, parameter2);

		public void VerifyInvokeWithSeveralParametersAsync(in It<int> parameter1, in It<int> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParametersAsync(parameter1, parameter2, times);

		public void VerifyInvokeWithSeveralParametersAsync(in It<int> parameter1, in It<int> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParametersAsync(parameter1, parameter2, times());

		// Return
		public ISetup<Action, int, Func<int>> SetupReturn() =>
			((PrimitiveDependencyServiceMock)@this).SetupReturn();

		public void VerifyReturn(in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturn(times);

		public void VerifyReturn(in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturn(times());

		// Return
		public ISetup<ActionOut<string>, bool, FuncOut<string, bool>> SetupReturn(in ItOut<string> result) =>
			((PrimitiveDependencyServiceMock)@this).SetupReturn(result);

		public void VerifyReturn(in ItOut<string> result, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturn(result, times);

		public void VerifyReturn(in ItOut<string> result, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturn(result, times());

		// Return
		public ISetup<Action, T, Func<T?>> SetupReturn<T>() =>
			((PrimitiveDependencyServiceMock)@this).SetupReturn<T>();

		public void VerifyReturn<T>(in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturn<T>(times);

		public void VerifyReturn<T>(in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturn<T>(times());

		// ReturnAsync
		public ISetup<Action, bool, Func<bool>> SetupReturnAsync() =>
			((PrimitiveDependencyServiceMock)@this).SetupReturnAsync();

		public void VerifyReturnAsync(in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnAsync(times);

		public void VerifyReturnAsync(in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnAsync(times());

		// ReturnWithParameter
		public ISetup<Action<string>, string, Func<string, string?>> SetupReturnWithParameter(in It<string> parameter = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupReturnWithParameter(parameter);

		public void VerifyReturnWithParameter(in It<string> parameter, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(parameter, times);

		public void VerifyReturnWithParameter(in It<string> parameter, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(parameter, times());

		// ReturnWithParameter
		public ISetup<ActionRef<double>, int, FuncRef<double, int>> SetupReturnWithParameter(in ItRef<double> parameter = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupReturnWithParameter(parameter);

		public ISetup<ActionRef<double>, int, FuncRef<double, int>> SetupReturnWithParameter(ref double parameter) =>
			((PrimitiveDependencyServiceMock)@this).SetupReturnWithParameter(ItRef<double>.Value(parameter));

		public void VerifyReturnWithParameter(in ItRef<double> parameter, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(parameter, times);

		public void VerifyReturnWithParameter(in ItRef<double> parameter, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(parameter, times());

		public void VerifyReturnWithParameter(ref double parameter, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(ItRef<double>.Value(parameter), times);

		public void VerifyReturnWithParameter(ref double parameter, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(ItRef<double>.Value(parameter), times());

		// ReturnWithParameter
		public ISetup<Action<TParameter>, TReturn, Func<TParameter, TReturn?>> SetupReturnWithParameter<TParameter, TReturn>(in It<TParameter> parameter = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupReturnWithParameter<TParameter, TReturn>(parameter);

		public void VerifyReturnWithParameter<TParameter, TReturn>(in It<TParameter> parameter, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter<TParameter, TReturn>(parameter, times);

		public void VerifyReturnWithParameter<TParameter, TReturn>(in It<TParameter> parameter, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter<TParameter, TReturn>(parameter, times());

		// ReturnWithParameterAsync
		public ISetup<Action<string>, int, Func<string, int>> SetupReturnWithParameterAsync(in It<string> parameter = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupReturnWithParameterAsync(parameter);

		public void VerifyReturnWithParameterAsync(in It<string> parameter, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameterAsync(parameter, times);

		public void VerifyReturnWithParameterAsync(in It<string> parameter, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameterAsync(parameter, times());

		// ReturnWithSeveralParameters
		public ISetup<Action<int, int>, decimal, Func<int, int, decimal>> SetupReturnWithSeveralParameters(in It<int> parameter1 = default, in It<int> parameter2 = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupReturnWithSeveralParameters(parameter1, parameter2);

		public void VerifyReturnWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times);

		public void VerifyReturnWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times());

		// ReturnWithSeveralParameters
		public ISetup<SetupRefIntInt<decimal>.CallbackDelegate, decimal, SetupRefIntInt<decimal>.ReturnsCallbackDelegate> SetupReturnWithSeveralParameters(in ItRef<int> parameter1 = default, in It<int> parameter2 = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupReturnWithSeveralParameters(parameter1, parameter2);

		public void VerifyReturnWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times);

		public void VerifyReturnWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times());

		public void VerifyReturnWithSeveralParameters(ref int parameter1, in It<int> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(ItRef<int>.Value(parameter1), parameter2, times);

		public void VerifyReturnWithSeveralParameters(ref int parameter1, in It<int> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(ItRef<int>.Value(parameter1), parameter2, times());

		// ReturnWithSeveralParameters
		public ISetup<SetupIntRefInt<decimal>.CallbackDelegate, decimal, SetupIntRefInt<decimal>.ReturnsCallbackDelegate> SetupReturnWithSeveralParameters(in It<int> parameter1 = default, in ItRef<int> parameter2 = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupReturnWithSeveralParameters(parameter1, parameter2);

		public void VerifyReturnWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times);

		public void VerifyReturnWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times());

		public void VerifyReturnWithSeveralParameters(in It<int> parameter1, ref int parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, ItRef<int>.Value(parameter2), times);

		public void VerifyReturnWithSeveralParameters(in It<int> parameter1, ref int parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, ItRef<int>.Value(parameter2), times());

		// ReturnWithSeveralParameters
		public ISetup<SetupRefIntRefInt<decimal>.CallbackDelegate, decimal, SetupRefIntRefInt<decimal>.ReturnsCallbackDelegate> SetupReturnWithSeveralParameters(in ItRef<int> parameter1 = default, in ItRef<int> parameter2 = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupReturnWithSeveralParameters(parameter1, parameter2);

		public void VerifyReturnWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times);

		public void VerifyReturnWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times());

		public void VerifyReturnWithSeveralParameters(ref int parameter1, ref int parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(ItRef<int>.Value(parameter1), ItRef<int>.Value(parameter2), times);

		public void VerifyReturnWithSeveralParameters(ref int parameter1, ref int parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(ItRef<int>.Value(parameter1), ItRef<int>.Value(parameter2), times());

		// ReturnWithSeveralParameters
		public ISetup<SetupRefT1T2<TParameter1, TParameter2, TReturn>.CallbackDelegate, TReturn, SetupRefT1T2<TParameter1, TParameter2, TReturn>.ReturnsCallbackDelegate> SetupReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(in ItRef<TParameter1> parameter1 = default, in It<TParameter2> parameter2 = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(parameter1, parameter2);

		public void VerifyReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(in ItRef<TParameter1> parameter1, in It<TParameter2> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(parameter1, parameter2, times);

		public void VerifyReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(in ItRef<TParameter1> parameter1, in It<TParameter2> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(parameter1, parameter2, times());

		public void VerifyReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref TParameter1 parameter1, in It<TParameter2> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ItRef<TParameter1>.Value(parameter1), parameter2, times);

		public void VerifyReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref TParameter1 parameter1, in It<TParameter2> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ItRef<TParameter1>.Value(parameter1), parameter2, times());

		// ReturnWithSeveralParameters
		public ISetup<Action<int, int>, decimal, Func<int, int, decimal>> SetupReturnWithSeveralParametersAsync(in It<int> parameter1 = default, in It<int> parameter2 = default) =>
			((PrimitiveDependencyServiceMock)@this).SetupReturnWithSeveralParametersAsync(parameter1, parameter2);

		public void VerifyReturnWithSeveralParametersAsync(in It<int> parameter1, in It<int> parameter2, in Times times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParametersAsync(parameter1, parameter2, times);

		public void VerifyReturnWithSeveralParametersAsync(in It<int> parameter1, in It<int> parameter2, in Func<Times> times) =>
			((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParametersAsync(parameter1, parameter2, times());

		public void VerifyNoOtherCalls() =>
			((PrimitiveDependencyServiceMock)@this).VerifyNoOtherCalls();
	}
}

public static partial class MockSequenceExtensions
{
	extension(IMockSequence<IPrimitiveDependencyService> @this)
	{
		// Handler
		public void AddHandler(PrimitiveHandler value)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyAddHandler(value, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		public void RemoveHandler(PrimitiveHandler value)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyRemoveHandler(value, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// HandlerEvent
		public void AddHandlerEvent(EventHandler<string> value)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyAddHandlerEvent(value, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		public void RemoveHandlerEvent(EventHandler<string> value)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyRemoveHandlerEvent(value, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// GetOnly
		public void GetGetOnly()
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyGetGetOnly(@this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// SetOnly
		public void SetSetOnly(in It<decimal> value)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifySetSetOnly(value, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// GetInit
		public void GetGetInit()
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyGetGetInit(@this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		public void SetGetInit(in It<string> value)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifySetGetInit(value, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// Invoke
		public void Invoke()
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// Invoke
		public void Invoke(in ItOut<int> result)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvoke(result, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// Invoke
		public void Invoke<T>()
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvoke<T>(@this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// Invoke
		public void InvokeAsync()
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeAsync(@this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// InvokeWithParameter
		public void InvokeWithParameter(in It<string> parameter)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithParameter(parameter, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// InvokeWithParameter
		public void InvokeWithParameter(in It<int> parameter)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithParameter(parameter, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// InvokeWithParameter
		public void InvokeWithParameter(in ItRef<decimal> parameter)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithParameter(parameter, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		public void InvokeWithParameter(ref decimal parameter)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithParameter(ItRef<decimal>.Value(parameter), @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// InvokeWithParameter
		public void InvokeWithParameter<T>(in It<T> parameter)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithParameter(parameter, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// InvokeWithParameterAsync
		public void InvokeWithParameterAsync(in It<int> parameter)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithParameterAsync(parameter, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// InvokeWithSeveralParameters
		public void InvokeWithSeveralParameters(in It<int> parameter1, in It<int> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// InvokeWithSeveralParameters
		public void InvokeWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		public void InvokeWithSeveralParameters(ref int parameter1, in It<int> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithSeveralParameters(ItRef<int>.Value(parameter1), parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// InvokeWithSeveralParameters
		public void InvokeWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		public void InvokeWithSeveralParameters(in It<int> parameter1, ref int parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithSeveralParameters(parameter1, ItRef<int>.Value(parameter2), @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// InvokeWithSeveralParameters
		public void InvokeWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		public void InvokeWithSeveralParameters(ref int parameter1, ref int parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithSeveralParameters(ItRef<int>.Value(parameter1), ItRef<int>.Value(parameter2), @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// InvokeWithSeveralParameters
		public void InvokeWithSeveralParameters<T>(in It<T> parameter1, in It<int> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// InvokeWithSeveralParameters
		public void InvokeWithSeveralParametersAsync(in It<int> parameter1, in It<int> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithSeveralParametersAsync(parameter1, parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// Return
		public void Return()
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturn(@this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// Return
		public void Return(in ItOut<string> result)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturn(result, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// Return
		public void Return<T>()
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturn<T>(@this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// ReturnAsync
		public void ReturnAsync()
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnAsync(@this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// ReturnWithParameter
		public void ReturnWithParameter(in It<string> parameter)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithParameter(parameter, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// ReturnWithParameter
		public void ReturnWithParameter(in ItRef<double> parameter)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithParameter(parameter, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		public void ReturnWithParameter(ref double parameter)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithParameter(ItRef<double>.Value(parameter), @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// ReturnWithParameter
		public void ReturnWithParameter<TParameter, TReturn>(in It<TParameter> parameter)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithParameter<TParameter, TReturn>(parameter, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// ReturnWithParameterAsync
		public void ReturnWithParameterAsync(in It<string> parameter)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithParameterAsync(parameter, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// ReturnWithSeveralParameters
		public void ReturnWithSeveralParameters(in It<int> parameter1, in It<int> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// ReturnWithSeveralParameters
		public void ReturnWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		public void ReturnWithSeveralParameters(ref int parameter1, in It<int> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithSeveralParameters(ItRef<int>.Value(parameter1), parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// ReturnWithSeveralParameters
		public void ReturnWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		public void ReturnWithSeveralParameters(in It<int> parameter1, ref int parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithSeveralParameters(parameter1, ItRef<int>.Value(parameter2), @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// ReturnWithSeveralParameters
		public void ReturnWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		public void ReturnWithSeveralParameters(ref int parameter1, ref int parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithSeveralParameters(ItRef<int>.Value(parameter1), ItRef<int>.Value(parameter2), @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// ReturnWithSeveralParameters
		public void ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(in ItRef<TParameter1> parameter1, in It<TParameter2> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(parameter1, parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		public void ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref TParameter1 parameter1, in It<TParameter2> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ItRef<TParameter1>.Value(parameter1), parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}

		// ReturnWithSeveralParametersAsync
		public void ReturnWithSeveralParametersAsync(in It<int> parameter1, in It<int> parameter2)
		{
			var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithSeveralParametersAsync(parameter1, parameter2, @this.VerifyIndex);
			@this.VerifyIndex.Set(nextIndex);
		}
	}
}
