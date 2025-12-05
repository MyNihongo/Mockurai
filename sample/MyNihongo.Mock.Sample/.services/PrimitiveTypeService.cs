namespace MyNihongo.Mock.Sample;

internal sealed class PrimitiveTypeService : IPrimitiveTypeService
{
	private readonly IPrimitiveDependencyService _primitiveDependencyService;
	private readonly IPrimitiveDependencyService<string> _primitiveDependencyGenericService;

	public PrimitiveTypeService(
		IPrimitiveDependencyService primitiveDependencyService,
		IPrimitiveDependencyService<string> primitiveDependencyGenericService,
		bool subscribeToHandler)
	{
		_primitiveDependencyService = primitiveDependencyService;
		_primitiveDependencyGenericService = primitiveDependencyGenericService;

		if (subscribeToHandler)
			primitiveDependencyService.Handler += PrimitiveDependencyServiceOnHandler;
	}

	public event EventHandler<string>? HandlerEvent
	{
		add => _primitiveDependencyService.HandlerEvent += value;
		remove => _primitiveDependencyService.HandlerEvent -= value;
	}

	public int Sum { get; private set; }

	public int GetOnly => _primitiveDependencyService.GetOnly;

	public string GetOnlyGeneric => _primitiveDependencyGenericService.GetOnly;

	public decimal SetOnly
	{
		set => _primitiveDependencyService.SetOnly = value;
	}

	public string GetInit
	{
		get => _primitiveDependencyService.GetInit;
		set => _primitiveDependencyService.GetInit = value;
	}

	public string GetSetGeneric
	{
		get => _primitiveDependencyGenericService.GetSet;
		set => _primitiveDependencyGenericService.GetSet = value;
	}

	public void Invoke()
	{
		_primitiveDependencyService.Invoke();
	}

	public void Invoke(out int result)
	{
		_primitiveDependencyService.Invoke(out result);
	}

	public void Invoke<T>()
	{
		_primitiveDependencyService.Invoke<T>();
	}

	public async Task InvokeAsync()
	{
		await _primitiveDependencyService.InvokeAsync();
	}

	public void InvokeWithParameter(in string parameter)
	{
		_primitiveDependencyService.InvokeWithParameter(parameter);
	}

	public void InvokeWithParameter(in int parameter)
	{
		_primitiveDependencyService.InvokeWithParameter(parameter);
	}

	public void InvokeWithParameter(ref decimal parameter)
	{
		_primitiveDependencyService.InvokeWithParameter(ref parameter);
	}

	public void InvokeWithParameter<T>(T parameter)
	{
		_primitiveDependencyService.InvokeWithParameter(parameter);
	}

	public async Task InvokeWithParameterAsync(int parameter)
	{
		await _primitiveDependencyService.InvokeWithParameterAsync(parameter);
	}

	public void InvokeWithParameterGeneric(string parameter)
	{
		_primitiveDependencyGenericService.InvokeWithParameter(parameter);
	}

	public void InvokeWithSeveralParameters(int parameter1, int parameter2)
	{
		_primitiveDependencyService.InvokeWithSeveralParameters(parameter1, parameter2);
	}

	public void InvokeWithSeveralParameters(ref int parameter1, int parameter2)
	{
		_primitiveDependencyService.InvokeWithSeveralParameters(ref parameter1, parameter2);
	}

	public void InvokeWithSeveralParameters(int parameter1, ref int parameter2)
	{
		_primitiveDependencyService.InvokeWithSeveralParameters(parameter1, ref parameter2);
	}

	public void InvokeWithSeveralParameters(ref int parameter1, ref int parameter2)
	{
		_primitiveDependencyService.InvokeWithSeveralParameters(ref parameter1, ref parameter2);
	}

	public void InvokeWithSeveralParameters<T>(T parameter1, int parameter2)
	{
		_primitiveDependencyService.InvokeWithSeveralParameters(parameter1, parameter2);
	}

	public async Task InvokeWithSeveralParametersAsync(int parameter1, int parameter2)
	{
		await _primitiveDependencyService.InvokeWithSeveralParametersAsync(parameter1, parameter2);
	}

	public void InvokeWithSeveralParametersGeneric<TParameter>(TParameter parameter1, string parameter2)
	{
		_primitiveDependencyGenericService.InvokeWithSeveralParameters(parameter1, parameter2);
	}

	public decimal Return()
	{
		var shopCount = _primitiveDependencyService.Return();
		return shopCount * 1000m;
	}

	public bool Return(out string result)
	{
		return _primitiveDependencyService.Return(out result);
	}

	public T Return<T>()
	{
		return _primitiveDependencyService.Return<T>();
	}

	public async ValueTask<bool> ReturnAsync()
	{
		return await _primitiveDependencyService.ReturnAsync();
	}

	public T ReturnGeneric<T>()
	{
		return _primitiveDependencyGenericService.Return<T>();
	}

	public string ReturnWithParameter(in string parameter)
	{
		var name = _primitiveDependencyService.ReturnWithParameter(parameter);
		return $"name:{name},age:32";
	}

	public int ReturnWithParameter(ref double parameter)
	{
		var result = _primitiveDependencyService.ReturnWithParameter(ref parameter);
		return result + 1;
	}

	public TReturn ReturnWithParameter<TParameter, TReturn>(TParameter parameter)
	{
		return _primitiveDependencyService.ReturnWithParameter<TParameter, TReturn>(parameter);
	}

	public async ValueTask<int> ReturnWithParameterAsync(string parameter)
	{
		return await _primitiveDependencyService.ReturnWithParameterAsync(parameter);
	}

	public TReturn ReturnWithParameterGeneric<TReturn>(string parameter)
	{
		return _primitiveDependencyGenericService.ReturnWithParameter<TReturn>(parameter);
	}

	public double ReturnWithSeveralParameters(int parameter1, int parameter2)
	{
		var spending = _primitiveDependencyService.ReturnWithSeveralParameters(parameter1, parameter2);
		var spendingDouble = Convert.ToDouble(spending);

		return Math.Pow(spendingDouble, 2d);
	}

	public double ReturnWithSeveralParameters(ref int parameter1, int parameter2)
	{
		var spending = _primitiveDependencyService.ReturnWithSeveralParameters(ref parameter1, parameter2);
		var spendingDouble = Convert.ToDouble(spending);

		return Math.Pow(spendingDouble, 2d);
	}

	public double ReturnWithSeveralParameters(int parameter1, ref int parameter2)
	{
		var spending = _primitiveDependencyService.ReturnWithSeveralParameters(parameter1, ref parameter2);
		var spendingDouble = Convert.ToDouble(spending);

		return Math.Pow(spendingDouble, 2d);
	}

	public double ReturnWithSeveralParameters(ref int parameter1, ref int parameter2)
	{
		var spending = _primitiveDependencyService.ReturnWithSeveralParameters(ref parameter1, ref parameter2);
		var spendingDouble = Convert.ToDouble(spending);

		return Math.Pow(spendingDouble, 2d);
	}

	public TReturn ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(ref TParameter1 parameter1,
		TParameter2 parameter2)
	{
		return _primitiveDependencyService.ReturnWithSeveralParameters<TParameter1, TParameter2, TReturn>(
			ref parameter1, parameter2);
	}

	public async ValueTask<decimal> ReturnWithSeveralParametersAsync(int parameter1, int parameter2)
	{
		return await _primitiveDependencyService.ReturnWithSeveralParametersAsync(parameter1, parameter2);
	}

	public void Dispose()
	{
		_primitiveDependencyService.Handler -= PrimitiveDependencyServiceOnHandler;
	}

	public void PrimitiveDependencyServiceOnHandler(object sender, int value)
	{
		Sum += value;
	}
}