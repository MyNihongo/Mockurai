namespace MyNihongo.Mockurai.Models;

internal readonly ref struct ParameterSplit(ImmutableArray<ParameterSplit.Item> inputParameters, ImmutableArray<ParameterSplit.Item> outputParameters)
{
	public readonly ImmutableArray<Item> InputParameters = inputParameters, OutputParameters = outputParameters;

	public readonly struct Item(IParameterSymbol parameter, int index)
	{
		public readonly IParameterSymbol Parameter = parameter;
		public readonly int Index = index;
	}
}
