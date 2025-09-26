namespace MyNihongo.Mock;

public static class InvocationProviderEx
{
	public static IEnumerable<string> GetStrings(this Func<IEnumerable<IInvocationProvider?>> @this)
	{
		return @this
			.GetInvocations()
			.GetStrings();
	}

	public static IEnumerable<IInvocation> GetInvocations(this Func<IEnumerable<IInvocationProvider?>> @this)
	{
		return @this.Invoke()
			.SelectMany(static x => x?.GetInvocations() ?? []);
	}

	public static IEnumerable<string> GetStrings(this IEnumerable<IInvocation> @this)
	{
		return @this
			.Select(static x => x.GetString());
	}

	public static string GetString(this IInvocation @this)
	{
		return !string.IsNullOrEmpty(@this.Snapshot)
			? $"{@this.Index}: {@this.Snapshot}"
			: @this.Index.ToString();
	}
}
