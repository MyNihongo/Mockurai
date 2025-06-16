namespace MyNihongo.Mock.Sample;

public interface IClassTypeService
{
	decimal ReturnWithoutParameters();

	string ReturnWithOneParameter(in ClassParameter1 parameter);

	double ReturnWithMultipleParameters(in ClassParameter1 parameter1, in ClassParameter2 parameter2);
}
