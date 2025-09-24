namespace MyNihongo.Mock.Sample;

internal sealed class PrimitiveTypeService : IPrimitiveTypeService
{
	private readonly IPrimitiveDependencyService _primitiveDependencyService;

	public PrimitiveTypeService(IPrimitiveDependencyService primitiveDependencyService, bool subscribeToHandler)
	{
		_primitiveDependencyService = primitiveDependencyService;

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

	public decimal SetOnly
	{
		set => _primitiveDependencyService.SetOnly = value;
	}

	public string GetInit
	{
		get => _primitiveDependencyService.GetInit;
		set => _primitiveDependencyService.GetInit = value;
	}

	public void Invoke()
	{
		_primitiveDependencyService.Invoke();
	}

	public void Invoke(out int result)
	{
		_primitiveDependencyService.Invoke(out result);
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

	public decimal Return()
	{
		var shopCount = _primitiveDependencyService.Return();
		return shopCount * 1000m;
	}

	public bool Return(out string result)
	{
		return _primitiveDependencyService.Return(out result);
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

	public void Dispose()
	{
		_primitiveDependencyService.Handler -= PrimitiveDependencyServiceOnHandler;
	}

	public void PrimitiveDependencyServiceOnHandler(object sender, int value)
	{
		Sum += value;
	}
}
