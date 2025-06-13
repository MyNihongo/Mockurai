namespace MyNihongo.Mock.Sample;

public interface IPrimitiveTypeService
{
	decimal ReturnWithoutParameters();

	CustomerModel ReturnWithOneParameter(in string parameter);

	double ReturnWithMultipleParameters(in int parameter1, in int parameter2);
}
