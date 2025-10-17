namespace MyNihongo.Mock;

public static class InvocationProviderEx
{
	public static IEnumerable<string> GetStrings(this Func<IEnumerable<IInvocationProvider?>> @this)
	{
		return @this
			.GetInvocations()
			.Select(static x => x.ToString());
	}

	public static IEnumerable<IInvocation> GetInvocations(this Func<IEnumerable<IInvocationProvider?>> @this)
	{
		return @this.Invoke()
			.SelectMany(static x => x?.GetInvocations() ?? [])
			.OrderBy(static x => x.Index);
	}
}
