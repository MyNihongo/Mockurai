namespace MyNihongo.Mock.Sample;

internal sealed class PrimitiveTypeService : IPrimitiveTypeService
{
	private readonly IPrimitiveDependencyService _primitiveDependencyService;

	public PrimitiveTypeService(IPrimitiveDependencyService primitiveDependencyService)
	{
		_primitiveDependencyService = primitiveDependencyService;
	}

	public decimal Return()
	{
		var shopCount = _primitiveDependencyService.Return();
		return shopCount * 1000m;
	}

	public string ReturnWithOneParameter(in string parameter)
	{
		var name = _primitiveDependencyService.ReturnWithParameter(parameter);
		return $"name:{name},age:32";
	}

	public double ReturnWithMultipleParameters(in int parameter1, in int parameter2)
	{
		var spending = _primitiveDependencyService.ReturnWithMultipleParameters(parameter1, parameter2);
		var spendingDouble = Convert.ToDouble(spending);

		return Math.Pow(spendingDouble, 2d);
	}
}
