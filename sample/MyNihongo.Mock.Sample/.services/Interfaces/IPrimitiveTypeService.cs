namespace MyNihongo.Mock.Sample;

public interface IPrimitiveTypeService
{
	decimal Return();

	string ReturnWithOneParameter(in string parameter);

	double ReturnWithMultipleParameters(in int parameter1, in int parameter2);
}
