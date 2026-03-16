namespace MyNihongo.Mock.Models;

internal readonly ref struct ParameterSplit(ImmutableArray<IParameterSymbol> inputParameters, ImmutableArray<IParameterSymbol> outputParameters)
{
	public readonly ImmutableArray<IParameterSymbol> InputParameters = inputParameters, OutputParameters = outputParameters;
}
