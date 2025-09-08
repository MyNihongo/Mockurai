namespace MyNihongo.Mock.Sample;

[Obsolete("Will be generated")]
public sealed class PrimitiveDependencyServiceMock : IMock<IPrimitiveDependencyService>
{
	private Proxy? _proxy;
	public IPrimitiveDependencyService Object => _proxy ??= new Proxy(this);

	// Handler
	private PrimitiveHandler? _handler;
	private Invocation<PrimitiveHandler?>? _handlerAddInvocation;
	private Invocation<PrimitiveHandler?>? _handlerRemoveInvocation;

	public void RaiseHandler(in int value)
	{
		_handler?.Invoke(Object, value);
	}

	public void VerifyAddHandler(in PrimitiveHandler handler, in Times times)
	{
		_handlerAddInvocation ??= new Invocation<PrimitiveHandler?>("IPrimitiveDependencyService#Handler#add");
		_handlerAddInvocation.Verify(handler, times);
	}

	public long VerifyAddHandler(in PrimitiveHandler handler, in long index)
	{
		_handlerAddInvocation ??= new Invocation<PrimitiveHandler?>("IPrimitiveDependencyService#Handler#add");
		return _handlerAddInvocation.Verify(handler, index);
	}

	public void VerifyRemoveHandler(in PrimitiveHandler handler, in Times times)
	{
		_handlerRemoveInvocation ??= new Invocation<PrimitiveHandler?>("IPrimitiveDependencyService#Handler#remove");
		_handlerRemoveInvocation.Verify(handler, times);
	}

	public long VerifyRemoveHandler(in PrimitiveHandler handler, in long index)
	{
		_handlerRemoveInvocation ??= new Invocation<PrimitiveHandler?>("IPrimitiveDependencyService#Handler#remove");
		return _handlerRemoveInvocation.Verify(handler, index);
	}

	// HandlerEvent
	private EventHandler<string>? _handlerEvent;
	private Invocation<EventHandler<string>?>? _handlerEventAddInvocation;
	private Invocation<EventHandler<string>?>? _handlerEventRemoveInvocation;

	public void RaiseHandlerEvent(in string value)
	{
		_handlerEvent?.Invoke(Object, value);
	}

	public void VerifyAddHandlerEvent(in EventHandler<string> handler, in Times times)
	{
		_handlerEventAddInvocation ??= new Invocation<EventHandler<string>?>("IPrimitiveDependencyService#HandlerEvent#add");
		_handlerEventAddInvocation.Verify(handler, times);
	}

	public long VerifyAddHandlerEvent(in EventHandler<string> handler, in long index)
	{
		_handlerEventAddInvocation ??= new Invocation<EventHandler<string>?>("IPrimitiveDependencyService#HandlerEvent#add");
		return _handlerEventAddInvocation.Verify(handler, index);
	}

	public void VerifyRemoveHandlerEvent(in EventHandler<string> handler, in Times times)
	{
		_handlerEventRemoveInvocation ??= new Invocation<EventHandler<string>?>("IPrimitiveDependencyService#HandlerEvent#remove");
		_handlerEventRemoveInvocation.Verify(handler, times);
	}

	public long VerifyRemoveHandlerEvent(in EventHandler<string> handler, in long index)
	{
		_handlerEventRemoveInvocation ??= new Invocation<EventHandler<string>?>("IPrimitiveDependencyService#HandlerEvent#remove");
		return _handlerEventRemoveInvocation.Verify(handler, index);
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
		_getOnlyGetInvocation ??= new Invocation("IPrimitiveDependencyService#GetOnly#get");
		_getOnlyGetInvocation.Verify(times);
	}

	public long VerifyGetGetOnly(in long index)
	{
		_getOnlyGetInvocation ??= new Invocation("IPrimitiveDependencyService#GetOnly#get");
		return _getOnlyGetInvocation.Verify(index);
	}

	// SetOnly
	private SetupWithParameter<decimal>? _setOnlySet;
	private Invocation<decimal>? _setOnlySetInvocation;

	public SetupWithParameter<decimal> SetupSetSetOnly(in It<decimal> value)
	{
		_setOnlySet ??= new SetupWithParameter<decimal>();
		_setOnlySet.SetupParameter(value);
		return _setOnlySet;
	}

	public void VerifySetSetOnly(in It<decimal> value, in Times times)
	{
		_setOnlySetInvocation ??= new Invocation<decimal>("IPrimitiveDependencyService#SetOnly#set = {0}");
		_setOnlySetInvocation.Verify(value, times);
	}

	public long VerifySetSetOnly(in It<decimal> value, in long index)
	{
		_setOnlySetInvocation ??= new Invocation<decimal>("IPrimitiveDependencyService#SetOnly#set = {0}");
		return _setOnlySetInvocation.Verify(value, index);
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
		_getInitGetInvocation ??= new Invocation("IPrimitiveDependencyService#GetInit#get");
		_getInitGetInvocation.Verify(times);
	}

	public long VerifyGetGetInit(in long index)
	{
		_getInitGetInvocation ??= new Invocation("IPrimitiveDependencyService#GetInit#get");
		return _getInitGetInvocation.Verify(index);
	}

	public SetupWithParameter<string> SetupSetGetInit(in It<string> value)
	{
		_getInitSet ??= new SetupWithParameter<string>();
		_getInitSet.SetupParameter(value);
		return _getInitSet;
	}

	public void VerifySetGetInit(in It<string> value, in Times times)
	{
		_getInitSetInvocation ??= new Invocation<string>("IPrimitiveDependencyService#GetInit#set = {0}");
		_getInitSetInvocation.Verify(value, times);
	}

	public long VerifySetGetInit(in It<string> value, in long index)
	{
		_getInitSetInvocation ??= new Invocation<string>("IPrimitiveDependencyService#GetInit#set = {0}");
		return _getInitSetInvocation.Verify(value, index);
	}

	// Invoke
	private Setup? _invoke;
	private Invocation? _invokeInvocation;

	public Setup SetupInvoke()
	{
		return _invoke ??= new Setup();
	}

	public void VerifyInvoke(in Times times)
	{
		_invokeInvocation ??= new Invocation("IPrimitiveDependencyService#Invoke()");
		_invokeInvocation.Verify(times);
	}

	public long VerifyInvoke(in long index)
	{
		_invokeInvocation ??= new Invocation("IPrimitiveDependencyService#Invoke()");
		return _invokeInvocation.Verify(index);
	}

	// InvokeWithParameter
	private SetupWithParameter<string>? _invokeWithParameter1;
	private Invocation<string>? _invokeWithParameterInvocation1;

	public SetupWithParameter<string> SetupInvokeWithParameter(in It<string> parameter)
	{
		_invokeWithParameter1 ??= new SetupWithParameter<string>();
		_invokeWithParameter1.SetupParameter(parameter);
		return _invokeWithParameter1;
	}

	public void VerifyInvokeWithParameter(in It<string> parameter, in Times times)
	{
		_invokeWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService#InvokeWithParameter({0})");
		_invokeWithParameterInvocation1.Verify(parameter, times);
	}

	public long VerifyInvokeWithParameter(in It<string> parameter, in long index)
	{
		_invokeWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService#InvokeWithParameter({0})");
		return _invokeWithParameterInvocation1.Verify(parameter, index);
	}

	// InvokeWithParameter
	private SetupWithParameter<int>? _invokeWithParameter2;
	private Invocation<int>? _invokeWithParameterInvocation2;

	public SetupWithParameter<int> SetupInvokeWithParameter(in It<int> parameter)
	{
		_invokeWithParameter2 ??= new SetupWithParameter<int>();
		_invokeWithParameter2.SetupParameter(parameter);
		return _invokeWithParameter2;
	}

	public void VerifyInvokeWithParameter(in It<int> parameter, in Times times)
	{
		_invokeWithParameterInvocation2 ??= new Invocation<int>("IPrimitiveDependencyService#InvokeWithParameter({0})");
		_invokeWithParameterInvocation2.Verify(parameter, times);
	}

	public long VerifyInvokeWithParameter(in It<int> parameter, in long index)
	{
		_invokeWithParameterInvocation2 ??= new Invocation<int>("IPrimitiveDependencyService#InvokeWithParameter({0})");
		return _invokeWithParameterInvocation2.Verify(parameter, index);
	}

	// InvokeWithParameter
	private SetupWithRefParameter<decimal>? _invokeWithParameter3;
	private Invocation<decimal>? _invokeWithParameterInvocation3;

	public SetupWithRefParameter<decimal> SetupInvokeWithParameter(in ItRef<decimal> parameter)
	{
		_invokeWithParameter3 ??= new SetupWithRefParameter<decimal>();
		_invokeWithParameter3.SetupParameter(parameter);
		return _invokeWithParameter3;
	}

	public void VerifyInvokeWithParameter(in ItRef<decimal> parameter, in Times times)
	{
		_invokeWithParameterInvocation3 ??= new Invocation<decimal>("IPrimitiveDependencyService#InvokeWithParameter(ref {0})", prefix: "ref");
		_invokeWithParameterInvocation3.Verify(parameter, times);
	}

	public long VerifyInvokeWithParameter(in ItRef<decimal> parameter, in long index)
	{
		_invokeWithParameterInvocation3 ??= new Invocation<decimal>("IPrimitiveDependencyService#InvokeWithParameter(ref {0})", prefix: "ref");
		return _invokeWithParameterInvocation3.Verify(parameter, index);
	}

	// InvokeWithSeveralParameters
	private SetupIntInt? _invokeWithSeveralParameters1;
	private InvocationIntInt? _invokeWithSeveralParametersInvocation1;

	public SetupIntInt SetupInvokeWithSeveralParameters(in It<int> parameter1, in It<int> parameter2)
	{
		_invokeWithSeveralParameters1 ??= new SetupIntInt();
		_invokeWithSeveralParameters1.SetupParameters(parameter1, parameter2);
		return _invokeWithSeveralParameters1;
	}

	public void VerifyInvokeWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in Times times)
	{
		_invokeWithSeveralParametersInvocation1 ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters({0}, {1})");
		_invokeWithSeveralParametersInvocation1.Verify(parameter1, parameter2, times);
	}

	public long VerifyInvokeWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in long index)
	{
		_invokeWithSeveralParametersInvocation1 ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters({0}, {1})");
		return _invokeWithSeveralParametersInvocation1.Verify(parameter1, parameter2, index);
	}

	// InvokeWithSeveralParameters
	private SetupRefIntInt? _invokeWithSeveralParameters2;
	private InvocationIntInt? _invokeWithSeveralParametersInvocation2;

	public SetupRefIntInt SetupInvokeWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2)
	{
		_invokeWithSeveralParameters2 ??= new SetupRefIntInt();
		_invokeWithSeveralParameters2.SetupParameters(parameter1, parameter2);
		return _invokeWithSeveralParameters2;
	}

	public void VerifyInvokeWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2, in Times times)
	{
		_invokeWithSeveralParametersInvocation2 ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters(ref {0}, {1})");
		_invokeWithSeveralParametersInvocation2.Verify(parameter1, parameter2, times);
	}

	public long VerifyInvokeWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2, in long index)
	{
		_invokeWithSeveralParametersInvocation2 ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters(ref {0}, {1})");
		return _invokeWithSeveralParametersInvocation2.Verify(parameter1, parameter2, index);
	}

	// InvokeWithSeveralParameters
	private SetupIntRefInt? _invokeWithSeveralParameters3;
	private InvocationIntInt? _invokeWithSeveralParametersInvocation3;

	public SetupIntRefInt SetupInvokeWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2)
	{
		_invokeWithSeveralParameters3 ??= new SetupIntRefInt();
		_invokeWithSeveralParameters3.SetupParameters(parameter1, parameter2);
		return _invokeWithSeveralParameters3;
	}

	public void VerifyInvokeWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2, in Times times)
	{
		_invokeWithSeveralParametersInvocation3 ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters({0}, ref {1})");
		_invokeWithSeveralParametersInvocation3.Verify(parameter1, parameter2, times);
	}

	public long VerifyInvokeWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2, in long index)
	{
		_invokeWithSeveralParametersInvocation3 ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters({0}, ref {1})");
		return _invokeWithSeveralParametersInvocation3.Verify(parameter1, parameter2, index);
	}

	// InvokeWithSeveralParameters
	private SetupRefIntRefInt? _invokeWithSeveralParameters4;
	private InvocationIntInt? _invokeWithSeveralParametersInvocation4;

	public SetupRefIntRefInt SetupInvokeWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2)
	{
		_invokeWithSeveralParameters4 ??= new SetupRefIntRefInt();
		_invokeWithSeveralParameters4.SetupParameters(parameter1, parameter2);
		return _invokeWithSeveralParameters4;
	}

	public void VerifyInvokeWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2, in Times times)
	{
		_invokeWithSeveralParametersInvocation4 ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters(ref {0}, ref {1})");
		_invokeWithSeveralParametersInvocation4.Verify(parameter1, parameter2, times);
	}

	public long VerifyInvokeWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2, in long index)
	{
		_invokeWithSeveralParametersInvocation4 ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters(ref {0}, ref {1})");
		return _invokeWithSeveralParametersInvocation4.Verify(parameter1, parameter2, index);
	}

	// Return
	private Setup<int>? _return;
	private Invocation? _returnInvocation;

	public Setup<int> SetupReturn()
	{
		return _return ??= new Setup<int>();
	}

	public void VerifyReturn(in Times times)
	{
		_returnInvocation ??= new Invocation("IPrimitiveDependencyService#Return()");
		_returnInvocation.Verify(times);
	}

	public long VerifyReturn(in long index)
	{
		_returnInvocation ??= new Invocation("IPrimitiveDependencyService#Return()");
		return _returnInvocation.Verify(index);
	}

	// ReturnWithParameter
	private SetupWithParameter<string, string>? _returnWithParameter1;
	private Invocation<string>? _returnWithParameterInvocation1;

	public SetupWithParameter<string, string> SetupReturnWithParameter(in It<string> parameter)
	{
		_returnWithParameter1 ??= new SetupWithParameter<string, string>();
		_returnWithParameter1.SetupParameter(parameter);
		return _returnWithParameter1;
	}

	public void VerifyReturnWithParameter(in It<string> parameter, in Times times)
	{
		_returnWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService#ReturnWithParameter({0})");
		_returnWithParameterInvocation1.Verify(parameter, times);
	}

	public long VerifyReturnWithParameter(in It<string> parameter, in long index)
	{
		_returnWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService#ReturnWithParameter({0})");
		return _returnWithParameterInvocation1.Verify(parameter, index);
	}

	// ReturnWithParameter
	private SetupWithRefParameter<double, int>? _returnWithParameter2;
	private Invocation<double>? _returnWithParameterInvocation2;

	public SetupWithRefParameter<double, int> SetupReturnWithParameter(in ItRef<double> parameter)
	{
		_returnWithParameter2 ??= new SetupWithRefParameter<double, int>();
		_returnWithParameter2.SetupParameter(parameter);
		return _returnWithParameter2;
	}

	public void VerifyReturnWithParameter(in ItRef<double> parameter, in Times times)
	{
		_returnWithParameterInvocation2 ??= new Invocation<double>("IPrimitiveDependencyService#ReturnWithParameter(ref {0})", prefix: "ref");
		_returnWithParameterInvocation2.Verify(parameter, times);
	}

	public long VerifyReturnWithParameter(in ItRef<double> parameter, in long index)
	{
		_returnWithParameterInvocation2 ??= new Invocation<double>("IPrimitiveDependencyService#ReturnWithParameter(ref {0})", prefix: "ref");
		return _returnWithParameterInvocation2.Verify(parameter, index);
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
		_returnWithSeveralParametersInvocation1 ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters({0}, {1})");
		_returnWithSeveralParametersInvocation1.Verify(parameter1, parameter2, times);
	}

	public long VerifyReturnWithSeveralParameters(in It<int> parameter1, in It<int> parameter2, in long index)
	{
		_returnWithSeveralParametersInvocation1 ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters({0}, {1})");
		return _returnWithSeveralParametersInvocation1.Verify(parameter1, parameter2, index);
	}

	// ReturnWithSeveralParameters
	private SetupRefIntInt<decimal>? _returnWithSeveralParameters2;
	private InvocationIntInt? _returnWithSeveralParametersInvocation2;

	public SetupRefIntInt<decimal> SetupReturnWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2)
	{
		_returnWithSeveralParameters2 ??= new SetupRefIntInt<decimal>();
		_returnWithSeveralParameters2.SetupParameters(parameter1, parameter2);
		return _returnWithSeveralParameters2;
	}

	public void VerifyReturnWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2, in Times times)
	{
		_returnWithSeveralParametersInvocation2 ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters(ref {0}, {1})");
		_returnWithSeveralParametersInvocation2.Verify(parameter1, parameter2, times);
	}

	public long VerifyReturnWithSeveralParameters(in ItRef<int> parameter1, in It<int> parameter2, in long index)
	{
		_returnWithSeveralParametersInvocation2 ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters(ref {0}, {1})");
		return _returnWithSeveralParametersInvocation2.Verify(parameter1, parameter2, index);
	}

	// ReturnWithSeveralParameters
	private SetupIntRefInt<decimal>? _returnWithSeveralParameters3;
	private InvocationIntInt? _returnWithSeveralParametersInvocation3;

	public SetupIntRefInt<decimal> SetupReturnWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2)
	{
		_returnWithSeveralParameters3 ??= new SetupIntRefInt<decimal>();
		_returnWithSeveralParameters3.SetupParameters(parameter1, parameter2);
		return _returnWithSeveralParameters3;
	}

	public void VerifyReturnWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2, in Times times)
	{
		_returnWithSeveralParametersInvocation3 ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters({0}, ref {1})");
		_returnWithSeveralParametersInvocation3.Verify(parameter1, parameter2, times);
	}

	public long VerifyReturnWithSeveralParameters(in It<int> parameter1, in ItRef<int> parameter2, in long index)
	{
		_returnWithSeveralParametersInvocation3 ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters({0}, ref {1})");
		return _returnWithSeveralParametersInvocation3.Verify(parameter1, parameter2, index);
	}

	// ReturnWithSeveralParameters
	private SetupRefIntRefInt<decimal>? _returnWithSeveralParameters4;
	private InvocationIntInt? _returnWithSeveralParametersInvocation4;

	public SetupRefIntRefInt<decimal> SetupReturnWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2)
	{
		_returnWithSeveralParameters4 ??= new SetupRefIntRefInt<decimal>();
		_returnWithSeveralParameters4.SetupParameters(parameter1, parameter2);
		return _returnWithSeveralParameters4;
	}

	public void VerifyReturnWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2, in Times times)
	{
		_returnWithSeveralParametersInvocation4 ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters(ref {0}, ref {1})");
		_returnWithSeveralParametersInvocation4.Verify(parameter1, parameter2, times);
	}

	public long VerifyReturnWithSeveralParameters(in ItRef<int> parameter1, in ItRef<int> parameter2, in long index)
	{
		_returnWithSeveralParametersInvocation4 ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters(ref {0}, ref {1})");
		return _returnWithSeveralParametersInvocation4.Verify(parameter1, parameter2, index);
	}

	public void VerifyNoOtherCalls()
	{
		_handlerAddInvocation?.VerifyNoOtherCalls();
		_handlerRemoveInvocation?.VerifyNoOtherCalls();
		_handlerEventAddInvocation?.VerifyNoOtherCalls();
		_handlerEventRemoveInvocation?.VerifyNoOtherCalls();
		_getOnlyGetInvocation?.VerifyNoOtherCalls();
		_setOnlySetInvocation?.VerifyNoOtherCalls();
		_getInitGetInvocation?.VerifyNoOtherCalls();
		_getInitSetInvocation?.VerifyNoOtherCalls();
		_invokeInvocation?.VerifyNoOtherCalls();
		_invokeWithParameterInvocation1?.VerifyNoOtherCalls();
		_invokeWithParameterInvocation2?.VerifyNoOtherCalls();
		_invokeWithParameterInvocation3?.VerifyNoOtherCalls();
		_invokeWithSeveralParametersInvocation1?.VerifyNoOtherCalls();
		_invokeWithSeveralParametersInvocation2?.VerifyNoOtherCalls();
		_invokeWithSeveralParametersInvocation3?.VerifyNoOtherCalls();
		_invokeWithSeveralParametersInvocation4?.VerifyNoOtherCalls();
		_returnInvocation?.VerifyNoOtherCalls();
		_returnWithParameterInvocation1?.VerifyNoOtherCalls();
		_returnWithParameterInvocation2?.VerifyNoOtherCalls();
		_returnWithSeveralParametersInvocation1?.VerifyNoOtherCalls();
		_returnWithSeveralParametersInvocation2?.VerifyNoOtherCalls();
		_returnWithSeveralParametersInvocation3?.VerifyNoOtherCalls();
		_returnWithSeveralParametersInvocation4?.VerifyNoOtherCalls();
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
				_mock._handlerAddInvocation ??= new Invocation<PrimitiveHandler?>("IPrimitiveDependencyService#Handler#add");
				_mock._handlerAddInvocation.Register(InvocationIndex.CounterValue, value);
				_mock._handler += value;
			}
			remove
			{
				_mock._handlerRemoveInvocation ??= new Invocation<PrimitiveHandler?>("IPrimitiveDependencyService#Handler#remove");
				_mock._handlerRemoveInvocation.Register(InvocationIndex.CounterValue, value);
				_mock._handler -= value;
			}
		}

		public event EventHandler<string>? HandlerEvent
		{
			add
			{
				_mock._handlerEventAddInvocation ??= new Invocation<EventHandler<string>?>("IPrimitiveDependencyService#HandlerEvent#add");
				_mock._handlerEventAddInvocation.Register(InvocationIndex.CounterValue, value);
				_mock._handlerEvent += value;
			}
			remove
			{
				_mock._handlerEventRemoveInvocation ??= new Invocation<EventHandler<string>?>("IPrimitiveDependencyService#HandlerEvent#remove");
				_mock._handlerEventRemoveInvocation.Register(InvocationIndex.CounterValue, value);
				_mock._handlerEvent -= value;
			}
		}

		public int GetOnly
		{
			get
			{
				_mock._getOnlyGetInvocation ??= new Invocation("IPrimitiveDependencyService#GetOnly#get");
				_mock._getOnlyGetInvocation.Register(InvocationIndex.CounterValue);
				return _mock._getOnlyGet?.Execute(out var returnValue) == true ? returnValue : 0;
			}
		}

		public decimal SetOnly
		{
			set
			{
				_mock._setOnlySetInvocation ??= new Invocation<decimal>("IPrimitiveDependencyService#SetOnly#set = {0}");
				_mock._setOnlySetInvocation.Register(InvocationIndex.CounterValue, value);
				_mock._setOnlySet?.Invoke(value);
			}
		}

		public string GetInit
		{
			get
			{
				_mock._getInitGetInvocation ??= new Invocation("IPrimitiveDependencyService#GetInit#get");
				_mock._getInitGetInvocation.Register(InvocationIndex.CounterValue);
				return _mock._getInitGet?.Execute(out var returnValue) == true ? returnValue! : string.Empty;
			}
			set
			{
				_mock._getInitSetInvocation ??= new Invocation<string>("IPrimitiveDependencyService#GetInit#set = {0}");
				_mock._getInitSetInvocation.Register(InvocationIndex.CounterValue, value);
				_mock._getInitSet?.Invoke(value);
			}
		}

		public void Invoke()
		{
			_mock._invokeInvocation ??= new Invocation("IPrimitiveDependencyService#Invoke()");
			_mock._invokeInvocation.Register(InvocationIndex.CounterValue);
			_mock._invoke?.Invoke();
		}

		public void InvokeWithParameter(in string parameter)
		{
			_mock._invokeWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService#InvokeWithParameter({0})");
			_mock._invokeWithParameterInvocation1.Register(InvocationIndex.CounterValue, parameter);
			_mock._invokeWithParameter1?.Invoke(parameter);
		}

		public void InvokeWithParameter(in int parameter)
		{
			_mock._invokeWithParameterInvocation2 ??= new Invocation<int>("IPrimitiveDependencyService#InvokeWithParameter({0})");
			_mock._invokeWithParameterInvocation2.Register(InvocationIndex.CounterValue, parameter);
			_mock._invokeWithParameter2?.Invoke(parameter);
		}

		public void InvokeWithParameter(ref decimal parameter)
		{
			_mock._invokeWithParameterInvocation3 ??= new Invocation<decimal>("IPrimitiveDependencyService#InvokeWithParameter(ref {0})", prefix: "ref");
			_mock._invokeWithParameterInvocation3.Register(InvocationIndex.CounterValue, parameter);
			_mock._invokeWithParameter3?.Invoke(ref parameter);
		}

		public void InvokeWithSeveralParameters(int parameter1, int parameter2)
		{
			_mock._invokeWithSeveralParametersInvocation1 ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters({0}, {1})");
			_mock._invokeWithSeveralParametersInvocation1.Register(InvocationIndex.CounterValue, parameter1, parameter2);
			_mock._invokeWithSeveralParameters1?.Invoke(parameter1, parameter2);
		}

		public void InvokeWithSeveralParameters(ref int parameter1, int parameter2)
		{
			_mock._invokeWithSeveralParametersInvocation2 ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters(ref {0}, {1})");
			_mock._invokeWithSeveralParametersInvocation2.Register(InvocationIndex.CounterValue, parameter1, parameter2);
			_mock._invokeWithSeveralParameters2?.Invoke(ref parameter1, parameter2);
		}

		public void InvokeWithSeveralParameters(int parameter1, ref int parameter2)
		{
			_mock._invokeWithSeveralParametersInvocation3 ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters({0}, ref {1})");
			_mock._invokeWithSeveralParametersInvocation3.Register(InvocationIndex.CounterValue, parameter1, parameter2);
			_mock._invokeWithSeveralParameters3?.Invoke(parameter1, ref parameter2);
		}

		public void InvokeWithSeveralParameters(ref int parameter1, ref int parameter2)
		{
			_mock._invokeWithSeveralParametersInvocation4 ??= new InvocationIntInt("IPrimitiveDependencyService#InvokeWithSeveralParameters(ref {0}, ref {1})");
			_mock._invokeWithSeveralParametersInvocation4.Register(InvocationIndex.CounterValue, parameter1, parameter2);
			_mock._invokeWithSeveralParameters4?.Invoke(ref parameter1, ref parameter2);
		}

		public int Return()
		{
			_mock._returnInvocation ??= new Invocation("IPrimitiveDependencyService#Return()");
			_mock._returnInvocation.Register(InvocationIndex.CounterValue);
			return _mock._return?.Execute(out var returnValue) == true ? returnValue : 0;
		}

		public string ReturnWithParameter(in string parameter)
		{
			_mock._returnWithParameterInvocation1 ??= new Invocation<string>("IPrimitiveDependencyService#ReturnWithParameter({0})");
			_mock._returnWithParameterInvocation1.Register(InvocationIndex.CounterValue, parameter);
			return _mock._returnWithParameter1?.Execute(parameter, out var returnValue) == true ? returnValue! : string.Empty;
		}

		public int ReturnWithParameter(ref double parameter)
		{
			_mock._returnWithParameterInvocation2 ??= new Invocation<double>("IPrimitiveDependencyService#ReturnWithParameter(ref {0})", prefix: "ref");
			_mock._returnWithParameterInvocation2.Register(InvocationIndex.CounterValue, parameter);
			return _mock._returnWithParameter2?.Execute(ref parameter, out var returnValue) == true ? returnValue! : 0;
		}

		public decimal ReturnWithSeveralParameters(int parameter1, int parameter2)
		{
			_mock._returnWithSeveralParametersInvocation1 ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters({0}, {1})");
			_mock._returnWithSeveralParametersInvocation1.Register(InvocationIndex.CounterValue, parameter1, parameter2);
			return _mock._returnWithSeveralParameters1?.Execute(parameter1, parameter2, out var returnValue) == true ? returnValue : 0m;
		}

		public decimal ReturnWithSeveralParameters(ref int parameter1, int parameter2)
		{
			_mock._returnWithSeveralParametersInvocation2 ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters(ref {0}, {1})");
			_mock._returnWithSeveralParametersInvocation2.Register(InvocationIndex.CounterValue, parameter1, parameter2);
			return _mock._returnWithSeveralParameters2?.Execute(ref parameter1, parameter2, out var returnValue) == true ? returnValue : 0m;
		}

		public decimal ReturnWithSeveralParameters(int parameter1, ref int parameter2)
		{
			_mock._returnWithSeveralParametersInvocation3 ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters({0}, ref {1})");
			_mock._returnWithSeveralParametersInvocation3.Register(InvocationIndex.CounterValue, parameter1, parameter2);
			return _mock._returnWithSeveralParameters3?.Execute(parameter1, ref parameter2, out var returnValue) == true ? returnValue : 0m;
		}

		public decimal ReturnWithSeveralParameters(ref int parameter1, ref int parameter2)
		{
			_mock._returnWithSeveralParametersInvocation4 ??= new InvocationIntInt("IPrimitiveDependencyService#ReturnWithSeveralParameters(ref {0}, ref {1})");
			_mock._returnWithSeveralParametersInvocation4.Register(InvocationIndex.CounterValue, parameter1, parameter2);
			return _mock._returnWithSeveralParameters4?.Execute(ref parameter1, ref parameter2, out var returnValue) == true ? returnValue : 0m;
		}
	}
}

[Obsolete("Will be generated")]
public static class PrimitiveDependencyServiceMockEx
{
	public static void RaiseHandler(this IMock<IPrimitiveDependencyService> @this, in int value) =>
		((PrimitiveDependencyServiceMock)@this).RaiseHandler(value);

	public static void VerifyAddHandler(this IMock<IPrimitiveDependencyService> @this, in PrimitiveHandler handler, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyAddHandler(handler, times);

	public static void VerifyAddHandler(this IMock<IPrimitiveDependencyService> @this, in PrimitiveHandler handler, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyAddHandler(handler, times());

	public static void VerifyRemoveHandler(this IMock<IPrimitiveDependencyService> @this, in PrimitiveHandler handler, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyRemoveHandler(handler, times);

	public static void VerifyRemoveHandler(this IMock<IPrimitiveDependencyService> @this, in PrimitiveHandler handler, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyRemoveHandler(handler, times());

	public static void RaiseHandlerEvent(this IMock<IPrimitiveDependencyService> @this, in string value) =>
		((PrimitiveDependencyServiceMock)@this).RaiseHandlerEvent(value);

	public static void VerifyAddHandlerEvent(this IMock<IPrimitiveDependencyService> @this, in EventHandler<string> handler, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyAddHandlerEvent(handler, times);

	public static void VerifyAddHandlerEvent(this IMock<IPrimitiveDependencyService> @this, in EventHandler<string> handler, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyAddHandlerEvent(handler, times());

	public static void VerifyRemoveHandlerEvent(this IMock<IPrimitiveDependencyService> @this, in EventHandler<string> handler, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyRemoveHandlerEvent(handler, times);

	public static void VerifyRemoveHandlerEvent(this IMock<IPrimitiveDependencyService> @this, in EventHandler<string> handler, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyRemoveHandlerEvent(handler, times());

	public static ISetup<int> SetupGetGetOnly(this IMock<IPrimitiveDependencyService> @this) =>
		((PrimitiveDependencyServiceMock)@this).SetupGetGetOnly();

	public static void VerifyGetGetOnly(this IMock<IPrimitiveDependencyService> @this, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyGetGetOnly(times);

	public static void VerifyGetGetOnly(this IMock<IPrimitiveDependencyService> @this, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyGetGetOnly(times());

	public static ISetup SetupSetSetOnly(this IMock<IPrimitiveDependencyService> @this, in It<decimal> value) =>
		((PrimitiveDependencyServiceMock)@this).SetupSetSetOnly(value);

	public static void VerifySetSetOnly(this IMock<IPrimitiveDependencyService> @this, in It<decimal> value, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifySetSetOnly(value, times);

	public static void VerifySetSetOnly(this IMock<IPrimitiveDependencyService> @this, in It<decimal> value, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifySetSetOnly(value, times());

	public static ISetup<string> SetupGetGetInit(this IMock<IPrimitiveDependencyService> @this) =>
		((PrimitiveDependencyServiceMock)@this).SetupGetGetInit();

	public static void VerifyGetGetInit(this IMock<IPrimitiveDependencyService> @this, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyGetGetInit(times);

	public static void VerifyGetGetInit(this IMock<IPrimitiveDependencyService> @this, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyGetGetInit(times());

	public static ISetup SetupSetGetInit(this IMock<IPrimitiveDependencyService> @this, in It<string> value) =>
		((PrimitiveDependencyServiceMock)@this).SetupSetGetInit(value);

	public static void VerifySetGetInit(this IMock<IPrimitiveDependencyService> @this, in It<string> value, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifySetGetInit(value, times);

	public static void VerifySetGetInit(this IMock<IPrimitiveDependencyService> @this, in It<string> value, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifySetGetInit(value, times());

	public static ISetup SetupInvoke(this IMock<IPrimitiveDependencyService> @this) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvoke();

	public static void VerifyInvoke(this IMock<IPrimitiveDependencyService> @this, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvoke(times);

	public static void VerifyInvoke(this IMock<IPrimitiveDependencyService> @this, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvoke(times());

	public static ISetup SetupInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter = default) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);

	public static void VerifyInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times);

	public static void VerifyInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times());

	public static ISetup SetupInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter = default) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);

	public static void VerifyInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times);

	public static void VerifyInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times());

	public static ISetup SetupInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in ItRef<decimal> parameter = default) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(parameter);

	public static ISetup SetupInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, ref decimal parameter) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithParameter(ItRef<decimal>.Value(parameter));

	public static void VerifyInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in ItRef<decimal> parameter, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times);

	public static void VerifyInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, in ItRef<decimal> parameter, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(parameter, times());

	public static void VerifyInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, ref decimal parameter, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(ItRef<decimal>.Value(parameter), times);

	public static void VerifyInvokeWithParameter(this IMock<IPrimitiveDependencyService> @this, ref decimal parameter, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithParameter(ItRef<decimal>.Value(parameter), times());

	public static ISetup SetupInvokeWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1 = default, in It<int> parameter2 = default) =>
		((PrimitiveDependencyServiceMock)@this).SetupInvokeWithSeveralParameters(parameter1, parameter2);

	public static void VerifyInvokeWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times);

	public static void VerifyInvokeWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyInvokeWithSeveralParameters(parameter1, parameter2, times());

	public static ISetup<int> SetupReturn(this IMock<IPrimitiveDependencyService> @this) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturn();

	public static void VerifyReturn(this IMock<IPrimitiveDependencyService> @this, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturn(times);

	public static void VerifyReturn(this IMock<IPrimitiveDependencyService> @this, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturn(times());

	public static ISetup<string> SetupReturnWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturnWithParameter(parameter);

	public static void VerifyReturnWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(parameter, times);

	public static void VerifyReturnWithParameter(this IMock<IPrimitiveDependencyService> @this, in It<string> parameter, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(parameter, times());

	public static ISetup<int> SetupReturnWithParameter(this IMock<IPrimitiveDependencyService> @this, in ItRef<double> parameter) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturnWithParameter(parameter);

	public static ISetup<int> SetupReturnWithParameter(this IMock<IPrimitiveDependencyService> @this, ref double parameter) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturnWithParameter(ItRef<double>.Value(parameter));

	public static void VerifyReturnWithParameter(this IMock<IPrimitiveDependencyService> @this, in ItRef<double> parameter, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(parameter, times);

	public static void VerifyReturnWithParameter(this IMock<IPrimitiveDependencyService> @this, in ItRef<double> parameter, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(parameter, times());

	public static void VerifyReturnWithParameter(this IMock<IPrimitiveDependencyService> @this, ref double parameter, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(ItRef<double>.Value(parameter), times);

	public static void VerifyReturnWithParameter(this IMock<IPrimitiveDependencyService> @this, ref double parameter, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturnWithParameter(ItRef<double>.Value(parameter), times());

	public static ISetup<decimal> SetupReturnWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2) =>
		((PrimitiveDependencyServiceMock)@this).SetupReturnWithSeveralParameters(parameter1, parameter2);

	public static void VerifyReturnWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2, in Times times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times);

	public static void VerifyReturnWithSeveralParameters(this IMock<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2, in Func<Times> times) =>
		((PrimitiveDependencyServiceMock)@this).VerifyReturnWithSeveralParameters(parameter1, parameter2, times());

	public static void VerifyNoOtherCalls(this IMock<IPrimitiveDependencyService> @this) =>
		((PrimitiveDependencyServiceMock)@this).VerifyNoOtherCalls();
}

[Obsolete("Will be generated")]
public static class PrimitiveDependencyServiceMockSequenceEx
{
	public static void AddHandler(this IMockSequence<IPrimitiveDependencyService> @this, PrimitiveHandler value)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyAddHandler(value, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void RemoveHandler(this IMockSequence<IPrimitiveDependencyService> @this, PrimitiveHandler value)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyRemoveHandler(value, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void AddHandlerEvent(this IMockSequence<IPrimitiveDependencyService> @this, EventHandler<string> value)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyAddHandlerEvent(value, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void RemoveHandlerEvent(this IMockSequence<IPrimitiveDependencyService> @this, EventHandler<string> value)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyRemoveHandlerEvent(value, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void GetGetOnly(this IMockSequence<IPrimitiveDependencyService> @this)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyGetGetOnly(@this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void SetSetOnly(this IMockSequence<IPrimitiveDependencyService> @this, in It<decimal> value)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifySetSetOnly(value, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void GetGetInit(this IMockSequence<IPrimitiveDependencyService> @this)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyGetGetInit(@this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void SetGetInit(this IMockSequence<IPrimitiveDependencyService> @this, in It<string> value)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifySetGetInit(value, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void Invoke(this IMockSequence<IPrimitiveDependencyService> @this)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvoke(@this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void InvokeWithParameter(this IMockSequence<IPrimitiveDependencyService> @this, in It<string> parameter)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithParameter(parameter, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void InvokeWithParameter(this IMockSequence<IPrimitiveDependencyService> @this, in It<int> parameter)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithParameter(parameter, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void InvokeWithParameter(this IMockSequence<IPrimitiveDependencyService> @this, in ItRef<decimal> parameter)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithParameter(parameter, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void InvokeWithParameter(this IMockSequence<IPrimitiveDependencyService> @this, ref decimal parameter)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithParameter(ItRef<decimal>.Value(parameter), @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void InvokeWithSeveralParameters(this IMockSequence<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyInvokeWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void Return(this IMockSequence<IPrimitiveDependencyService> @this)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturn(@this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void ReturnWithParameter(this IMockSequence<IPrimitiveDependencyService> @this, in It<string> parameter)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithParameter(parameter, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void ReturnWithParameter(this IMockSequence<IPrimitiveDependencyService> @this, in ItRef<double> parameter)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithParameter(parameter, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void ReturnWithParameter(this IMockSequence<IPrimitiveDependencyService> @this, ref double parameter)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithParameter(ItRef<double>.Value(parameter), @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}

	public static void ReturnWithSeveralParameters(this IMockSequence<IPrimitiveDependencyService> @this, in It<int> parameter1, in It<int> parameter2)
	{
		var nextIndex = ((PrimitiveDependencyServiceMock)@this.Mock).VerifyReturnWithSeveralParameters(parameter1, parameter2, @this.VerifyIndex);
		@this.VerifyIndex.Set(nextIndex);
	}
}
